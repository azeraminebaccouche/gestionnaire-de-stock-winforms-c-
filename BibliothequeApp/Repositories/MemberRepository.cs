using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.Sqlite;
using BibliothequeApp.DataAccess;
using BibliothequeApp.Entities;

namespace BibliothequeApp.Repositories
{
    /// <summary>
    /// Implementation of IMemberRepository using SQLite.
    /// </summary>
    public class MemberRepository : IMemberRepository
    {
        private readonly DatabaseContext _dbContext;

        public MemberRepository()
        {
            _dbContext = DatabaseContext.Instance;
        }

        public async Task<Member?> GetByIdAsync(int id)
        {
            using var connection = _dbContext.GetConnection();
            await connection.OpenAsync();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Members WHERE Id = @Id";
            command.Parameters.AddWithValue("@Id", id);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Member
                {
                    Id = reader.GetInt32(0),
                    FirstName = reader.GetString(1),
                    LastName = reader.GetString(2),
                    Email = reader.GetString(3),
                    PhoneNumber = reader.GetString(4),
                    Address = reader.IsDBNull(5) ? string.Empty : reader.GetString(5),
                    MembershipExpiryDate = DateTime.Parse(reader.GetString(6)),
                    IsActive = reader.GetInt32(7) == 1,
                    CurrentBooksBorrowed = reader.GetInt32(8)
                };
            }
            return null;
        }

        public async Task<IEnumerable<Member>> GetAllAsync()
        {
            var members = new List<Member>();
            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Members";

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            members.Add(MapReaderToMember(reader));
                        }
                    }
                }
            }
            return members;
        }

        public async Task<IEnumerable<Member>> SearchByNameAsync(string name)
        {
            var members = new List<Member>();
            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Members WHERE FirstName LIKE @Name OR LastName LIKE @Name";
                    command.Parameters.AddWithValue("@Name", $"%{name}%");

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            members.Add(MapReaderToMember(reader));
                        }
                    }
                }
            }
            return members;
        }

        public async Task<IEnumerable<Member>> SearchByEmailAsync(string email)
        {
            var members = new List<Member>();
            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Members WHERE Email LIKE @Email";
                    command.Parameters.AddWithValue("@Email", $"%{email}%");

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            members.Add(MapReaderToMember(reader));
                        }
                    }
                }
            }
            return members;
        }

        public async Task<int> AddAsync(Member member)
        {
            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        INSERT INTO Members (FirstName, LastName, Email, PhoneNumber, Address, RegistrationDate, 
                            MembershipExpiryDate, IsActive, MaxBooksAllowed, CurrentBooksBorrowed)
                        VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Address, @RegistrationDate,
                            @MembershipExpiryDate, @IsActive, @MaxBooksAllowed, @CurrentBooksBorrowed);
                        SELECT last_insert_rowid();";

                    command.Parameters.AddWithValue("@FirstName", member.FirstName);
                    command.Parameters.AddWithValue("@LastName", member.LastName);
                    command.Parameters.AddWithValue("@Email", member.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", member.PhoneNumber ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Address", member.Address ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@RegistrationDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.Parameters.AddWithValue("@MembershipExpiryDate", member.MembershipExpiryDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.Parameters.AddWithValue("@IsActive", member.IsActive ? 1 : 0);
                    command.Parameters.AddWithValue("@MaxBooksAllowed", member.MaxBooksAllowed);
                    command.Parameters.AddWithValue("@CurrentBooksBorrowed", member.CurrentBooksBorrowed);

                    return Convert.ToInt32(await command.ExecuteScalarAsync());
                }
            }
        }

        public async Task<bool> UpdateAsync(Member member)
        {
            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        UPDATE Members 
                        SET FirstName = @FirstName, LastName = @LastName, Email = @Email, 
                            PhoneNumber = @PhoneNumber, Address = @Address, 
                            MembershipExpiryDate = @MembershipExpiryDate, IsActive = @IsActive,
                            MaxBooksAllowed = @MaxBooksAllowed, CurrentBooksBorrowed = @CurrentBooksBorrowed
                        WHERE Id = @Id";

                    command.Parameters.AddWithValue("@Id", member.Id);
                    command.Parameters.AddWithValue("@FirstName", member.FirstName);
                    command.Parameters.AddWithValue("@LastName", member.LastName);
                    command.Parameters.AddWithValue("@Email", member.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", member.PhoneNumber ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Address", member.Address ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@MembershipExpiryDate", member.MembershipExpiryDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.Parameters.AddWithValue("@IsActive", member.IsActive ? 1 : 0);
                    command.Parameters.AddWithValue("@MaxBooksAllowed", member.MaxBooksAllowed);
                    command.Parameters.AddWithValue("@CurrentBooksBorrowed", member.CurrentBooksBorrowed);

                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM Members WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", id);
                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        public async Task<bool> UpdateBooksBorrowedAsync(int id, int booksBorrowed)
        {
            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE Members SET CurrentBooksBorrowed = @BooksBorrowed WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@BooksBorrowed", booksBorrowed);
                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        public async Task<bool> UpdateStatusAsync(int id, bool isActive)
        {
            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE Members SET IsActive = @IsActive WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@IsActive", isActive ? 1 : 0);
                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        private Member MapReaderToMember(SqliteDataReader reader)
        {
            return new Member
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                PhoneNumber = reader.IsDBNull(reader.GetOrdinal("PhoneNumber")) ? string.Empty : reader.GetString(reader.GetOrdinal("PhoneNumber")),
                Address = reader.IsDBNull(reader.GetOrdinal("Address")) ? string.Empty : reader.GetString(reader.GetOrdinal("Address")),
                MembershipExpiryDate = DateTime.Parse(reader.GetString(reader.GetOrdinal("MembershipExpiryDate"))),
                IsActive = reader.GetInt32(reader.GetOrdinal("IsActive")) == 1,
                MaxBooksAllowed = reader.GetInt32(reader.GetOrdinal("MaxBooksAllowed")),
                CurrentBooksBorrowed = reader.GetInt32(reader.GetOrdinal("CurrentBooksBorrowed"))
            };
        }
    }
} 
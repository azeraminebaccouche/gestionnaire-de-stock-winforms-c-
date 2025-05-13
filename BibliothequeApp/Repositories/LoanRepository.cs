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
    /// Implementation of ILoanRepository using SQLite.
    /// </summary>
    public class LoanRepository : ILoanRepository
    {
        private readonly DatabaseContext _dbContext;

        public LoanRepository()
        {
            _dbContext = DatabaseContext.Instance;
        }

        public async Task<Loan> GetByIdAsync(int id)
        {
            using var connection = _dbContext.GetConnection();
            await connection.OpenAsync();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Loans WHERE Id = @Id";
            command.Parameters.AddWithValue("@Id", id);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Loan
                {
                    Id = reader.GetInt32(0),
                    BookId = reader.GetInt32(1),
                    MemberId = reader.GetInt32(2),
                    BorrowDate = DateTime.Parse(reader.GetString(3)),
                    DueDate = DateTime.Parse(reader.GetString(4)),
                    ReturnDate = reader.IsDBNull(5) ? null : DateTime.Parse(reader.GetString(5)),
                    IsReturned = reader.GetInt32(6) == 1,
                    FineAmount = reader.GetDecimal(7),
                    IsFinePaid = reader.GetInt32(8) == 1,
                    Notes = reader.IsDBNull(9) ? string.Empty : reader.GetString(9)
                };
            }
            
            throw new KeyNotFoundException($"Loan with ID {id} not found");
        }

        public async Task<IEnumerable<Loan>> GetAllAsync()
        {
            var loans = new List<Loan>();
            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Loans";

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            loans.Add(MapReaderToLoan(reader));
                        }
                    }
                }
            }
            return loans;
        }

        public async Task<IEnumerable<Loan>> GetActiveLoansAsync()
        {
            var loans = new List<Loan>();
            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Loans WHERE IsReturned = 0";

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            loans.Add(MapReaderToLoan(reader));
                        }
                    }
                }
            }
            return loans;
        }

        public async Task<IEnumerable<Loan>> GetOverdueLoansAsync()
        {
            var loans = new List<Loan>();
            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Loans WHERE IsReturned = 0 AND DueDate < @CurrentDate";
                    command.Parameters.AddWithValue("@CurrentDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            loans.Add(MapReaderToLoan(reader));
                        }
                    }
                }
            }
            return loans;
        }

        public async Task<IEnumerable<Loan>> GetMemberLoansAsync(int memberId)
        {
            var loans = new List<Loan>();
            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Loans WHERE MemberId = @MemberId";
                    command.Parameters.AddWithValue("@MemberId", memberId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            loans.Add(MapReaderToLoan(reader));
                        }
                    }
                }
            }
            return loans;
        }

        public async Task<IEnumerable<Loan>> GetBookLoansAsync(int bookId)
        {
            var loans = new List<Loan>();
            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Loans WHERE BookId = @BookId";
                    command.Parameters.AddWithValue("@BookId", bookId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            loans.Add(MapReaderToLoan(reader));
                        }
                    }
                }
            }
            return loans;
        }

        public async Task<int> AddAsync(Loan loan)
        {
            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        INSERT INTO Loans (BookId, MemberId, BorrowDate, DueDate, ReturnDate, 
                            IsReturned, FineAmount, IsFinePaid)
                        VALUES (@BookId, @MemberId, @BorrowDate, @DueDate, @ReturnDate,
                            @IsReturned, @FineAmount, @IsFinePaid);
                        SELECT last_insert_rowid();";

                    command.Parameters.AddWithValue("@BookId", loan.BookId);
                    command.Parameters.AddWithValue("@MemberId", loan.MemberId);
                    command.Parameters.AddWithValue("@BorrowDate", loan.BorrowDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.Parameters.AddWithValue("@DueDate", loan.DueDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.Parameters.AddWithValue("@ReturnDate", loan.ReturnDate?.ToString("yyyy-MM-dd HH:mm:ss") ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IsReturned", loan.IsReturned ? 1 : 0);
                    command.Parameters.AddWithValue("@FineAmount", loan.FineAmount);
                    command.Parameters.AddWithValue("@IsFinePaid", loan.IsFinePaid ? 1 : 0);

                    return Convert.ToInt32(await command.ExecuteScalarAsync());
                }
            }
        }

        public async Task<bool> UpdateAsync(Loan loan)
        {
            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        UPDATE Loans 
                        SET BookId = @BookId, MemberId = @MemberId, BorrowDate = @BorrowDate,
                            DueDate = @DueDate, ReturnDate = @ReturnDate, IsReturned = @IsReturned,
                            FineAmount = @FineAmount, IsFinePaid = @IsFinePaid
                        WHERE Id = @Id";

                    command.Parameters.AddWithValue("@Id", loan.Id);
                    command.Parameters.AddWithValue("@BookId", loan.BookId);
                    command.Parameters.AddWithValue("@MemberId", loan.MemberId);
                    command.Parameters.AddWithValue("@BorrowDate", loan.BorrowDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.Parameters.AddWithValue("@DueDate", loan.DueDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.Parameters.AddWithValue("@ReturnDate", loan.ReturnDate?.ToString("yyyy-MM-dd HH:mm:ss") ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@IsReturned", loan.IsReturned ? 1 : 0);
                    command.Parameters.AddWithValue("@FineAmount", loan.FineAmount);
                    command.Parameters.AddWithValue("@IsFinePaid", loan.IsFinePaid ? 1 : 0);

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
                    command.CommandText = "DELETE FROM Loans WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", id);
                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        public async Task<bool> ReturnBookAsync(int id, DateTime returnDate, decimal fineAmount)
        {
            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        UPDATE Loans 
                        SET ReturnDate = @ReturnDate, IsReturned = 1, FineAmount = @FineAmount
                        WHERE Id = @Id";

                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@ReturnDate", returnDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.Parameters.AddWithValue("@FineAmount", fineAmount);

                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        private Loan MapReaderToLoan(SqliteDataReader reader)
        {
            return new Loan
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                BookId = reader.GetInt32(reader.GetOrdinal("BookId")),
                MemberId = reader.GetInt32(reader.GetOrdinal("MemberId")),
                BorrowDate = DateTime.Parse(reader.GetString(reader.GetOrdinal("BorrowDate"))),
                DueDate = DateTime.Parse(reader.GetString(reader.GetOrdinal("DueDate"))),
                ReturnDate = reader.IsDBNull(reader.GetOrdinal("ReturnDate")) ? null : DateTime.Parse(reader.GetString(reader.GetOrdinal("ReturnDate"))),
                IsReturned = reader.GetInt32(reader.GetOrdinal("IsReturned")) == 1,
                FineAmount = reader.GetDecimal(reader.GetOrdinal("FineAmount")),
                IsFinePaid = reader.GetInt32(reader.GetOrdinal("IsFinePaid")) == 1
            };
        }
    }
} 
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
    /// Implementation of IBookRepository using SQLite.
    /// </summary>
    public class BookRepository : IBookRepository
    {
        private readonly DatabaseContext _dbContext;

        public BookRepository()
        {
            _dbContext = DatabaseContext.Instance;
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            using var connection = _dbContext.GetConnection();
            await connection.OpenAsync();
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Books WHERE Id = @Id";
            command.Parameters.AddWithValue("@Id", id);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Book
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Author = reader.GetString(2),
                    ISBN = reader.GetString(3),
                    PublicationYear = reader.GetInt32(4),
                    Publisher = reader.GetString(5),
                    TotalCopies = reader.GetInt32(6),
                    AvailableCopies = reader.GetInt32(7),
                    DateAdded = DateTime.Parse(reader.GetString(8)),
                    Category = reader.IsDBNull(9) ? string.Empty : reader.GetString(9),
                    Description = reader.IsDBNull(10) ? string.Empty : reader.GetString(10)
                };
            }
            return null;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            var books = new List<Book>();
            using (var connection = _dbContext.GetConnection())
            {
                try
                {
                    await connection.OpenAsync();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM Books";

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                books.Add(MapReaderToBook(reader));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving books: {ex.Message}", ex);
                }
            }
            return books;
        }

        public async Task<IEnumerable<Book>> SearchByTitleAsync(string title)
        {
            var books = new List<Book>();
            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Books WHERE Title LIKE @Title";
                    command.Parameters.AddWithValue("@Title", $"%{title}%");

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            books.Add(MapReaderToBook(reader));
                        }
                    }
                }
            }
            return books;
        }

        public async Task<IEnumerable<Book>> SearchByAuthorAsync(string author)
        {
            var books = new List<Book>();
            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Books WHERE Author LIKE @Author";
                    command.Parameters.AddWithValue("@Author", $"%{author}%");

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            books.Add(MapReaderToBook(reader));
                        }
                    }
                }
            }
            return books;
        }

        public async Task<int> AddAsync(Book book)
        {
            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        INSERT INTO Books (Title, Author, ISBN, PublicationYear, Publisher, TotalCopies, AvailableCopies, DateAdded, Category, Description)
                        VALUES (@Title, @Author, @ISBN, @PublicationYear, @Publisher, @TotalCopies, @AvailableCopies, @DateAdded, @Category, @Description);
                        SELECT last_insert_rowid();";

                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@Author", book.Author);
                    command.Parameters.AddWithValue("@ISBN", book.ISBN);
                    command.Parameters.AddWithValue("@PublicationYear", book.PublicationYear);
                    command.Parameters.AddWithValue("@Publisher", book.Publisher);
                    command.Parameters.AddWithValue("@TotalCopies", book.TotalCopies);
                    command.Parameters.AddWithValue("@AvailableCopies", book.AvailableCopies);
                    command.Parameters.AddWithValue("@DateAdded", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.Parameters.AddWithValue("@Category", book.Category);
                    command.Parameters.AddWithValue("@Description", book.Description);

                    return Convert.ToInt32(await command.ExecuteScalarAsync());
                }
            }
        }

        public async Task<bool> UpdateAsync(Book book)
        {
            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        UPDATE Books 
                        SET Title = @Title, Author = @Author, ISBN = @ISBN, PublicationYear = @PublicationYear,
                            Publisher = @Publisher, TotalCopies = @TotalCopies, AvailableCopies = @AvailableCopies,
                            Category = @Category, Description = @Description
                        WHERE Id = @Id";

                    command.Parameters.AddWithValue("@Id", book.Id);
                    command.Parameters.AddWithValue("@Title", book.Title);
                    command.Parameters.AddWithValue("@Author", book.Author);
                    command.Parameters.AddWithValue("@ISBN", book.ISBN);
                    command.Parameters.AddWithValue("@PublicationYear", book.PublicationYear);
                    command.Parameters.AddWithValue("@Publisher", book.Publisher);
                    command.Parameters.AddWithValue("@TotalCopies", book.TotalCopies);
                    command.Parameters.AddWithValue("@AvailableCopies", book.AvailableCopies);
                    command.Parameters.AddWithValue("@Category", book.Category);
                    command.Parameters.AddWithValue("@Description", book.Description);

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
                    command.CommandText = "DELETE FROM Books WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", id);
                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        public async Task<bool> UpdateAvailabilityAsync(int id, int availableCopies)
        {
            using (var connection = _dbContext.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE Books SET AvailableCopies = @AvailableCopies WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@AvailableCopies", availableCopies);
                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
        }

        private Book MapReaderToBook(SqliteDataReader reader)
        {
            try
            {
                return new Book
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Title = reader.GetString(reader.GetOrdinal("Title")),
                    Author = reader.GetString(reader.GetOrdinal("Author")),
                    ISBN = reader.IsDBNull(reader.GetOrdinal("ISBN")) ? string.Empty : reader.GetString(reader.GetOrdinal("ISBN")),
                    PublicationYear = reader.IsDBNull(reader.GetOrdinal("PublicationYear")) ? 0 : reader.GetInt32(reader.GetOrdinal("PublicationYear")),
                    Publisher = reader.IsDBNull(reader.GetOrdinal("Publisher")) ? string.Empty : reader.GetString(reader.GetOrdinal("Publisher")),
                    TotalCopies = reader.GetInt32(reader.GetOrdinal("TotalCopies")),
                    AvailableCopies = reader.GetInt32(reader.GetOrdinal("AvailableCopies")),
                    DateAdded = DateTime.Parse(reader.GetString(reader.GetOrdinal("DateAdded"))),
                    Category = reader.IsDBNull(reader.GetOrdinal("Category")) ? string.Empty : reader.GetString(reader.GetOrdinal("Category")),
                    Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? string.Empty : reader.GetString(reader.GetOrdinal("Description"))
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error mapping book data: {ex.Message}", ex);
            }
        }
    }
} 
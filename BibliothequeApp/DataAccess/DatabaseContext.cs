using System;
using System.Data.SQLite;
using System.IO;
using Microsoft.Data.Sqlite;

namespace BibliothequeApp.DataAccess
{
    public class DatabaseContext
    {
        private static DatabaseContext _instance = null!;
        private static readonly object _lock = new object();
        private readonly string _connectionString;
        private readonly string _dbPath;

        private DatabaseContext()
        {
            _dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Library.db");
            _connectionString = $"Data Source={_dbPath}";
            InitializeDatabase();
        }

        public static DatabaseContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new DatabaseContext();
                        }
                    }
                }
                return _instance;
            }
        }

        public SqliteConnection GetConnection()
        {
            return new SqliteConnection(_connectionString);
        }

        private void InitializeDatabase()
        {
            if (!File.Exists(_dbPath))
            {
                SQLiteConnection.CreateFile(_dbPath);
                CreateTables();
                SeedData();
            }
        }

        private void CreateTables()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    // Create Books table
                    command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Books (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Title TEXT NOT NULL,
                            Author TEXT NOT NULL,
                            ISBN TEXT UNIQUE,
                            PublicationYear INTEGER,
                            Publisher TEXT,
                            TotalCopies INTEGER NOT NULL,
                            AvailableCopies INTEGER NOT NULL,
                            DateAdded TEXT NOT NULL,
                            Category TEXT,
                            Description TEXT
                        );";
                    command.ExecuteNonQuery();

                    // Create Members table
                    command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Members (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            FirstName TEXT NOT NULL,
                            LastName TEXT NOT NULL,
                            Email TEXT UNIQUE NOT NULL,
                            PhoneNumber TEXT,
                            Address TEXT,
                            RegistrationDate TEXT NOT NULL,
                            MembershipExpiryDate TEXT NOT NULL,
                            IsActive INTEGER NOT NULL,
                            MaxBooksAllowed INTEGER NOT NULL,
                            CurrentBooksBorrowed INTEGER NOT NULL
                        );";
                    command.ExecuteNonQuery();

                    // Create Loans table
                    command.CommandText = @"
                        CREATE TABLE IF NOT EXISTS Loans (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            BookId INTEGER NOT NULL,
                            MemberId INTEGER NOT NULL,
                            BorrowDate TEXT NOT NULL,
                            DueDate TEXT NOT NULL,
                            ReturnDate TEXT,
                            IsReturned INTEGER NOT NULL,
                            FineAmount REAL NOT NULL,
                            IsFinePaid INTEGER NOT NULL,
                            Notes TEXT,
                            FOREIGN KEY (BookId) REFERENCES Books(Id),
                            FOREIGN KEY (MemberId) REFERENCES Members(Id)
                        );";
                    command.ExecuteNonQuery();
                }
            }
        }

        private void SeedData()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    // Add sample books
                    command.CommandText = @"
                        INSERT INTO Books (Title, Author, ISBN, PublicationYear, Publisher, TotalCopies, AvailableCopies, DateAdded, Category, Description)
                        VALUES 
                        ('The Great Gatsby', 'F. Scott Fitzgerald', '9780743273565', 1925, 'Scribner', 5, 5, datetime('now'), 'Fiction', 'A story of the fabulously wealthy Jay Gatsby'),
                        ('1984', 'George Orwell', '9780451524935', 1949, 'Signet Classic', 3, 3, datetime('now'), 'Science Fiction', 'A dystopian social science fiction novel');";
                    command.ExecuteNonQuery();

                    // Add sample members
                    command.CommandText = @"
                        INSERT INTO Members (FirstName, LastName, Email, PhoneNumber, Address, RegistrationDate, MembershipExpiryDate, IsActive, MaxBooksAllowed, CurrentBooksBorrowed)
                        VALUES 
                        ('John', 'Doe', 'john.doe@email.com', '1234567890', '123 Main St', datetime('now'), datetime('now', '+1 year'), 1, 5, 0),
                        ('Jane', 'Smith', 'jane.smith@email.com', '0987654321', '456 Oak St', datetime('now'), datetime('now', '+1 year'), 1, 5, 0);";
                    command.ExecuteNonQuery();
                }
            }
        }
    }
} 
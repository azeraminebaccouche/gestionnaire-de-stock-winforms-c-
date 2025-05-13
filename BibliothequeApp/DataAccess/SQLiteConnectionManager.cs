using System;
using System.Data.SQLite;
using System.IO;

namespace BibliothequeApp.DataAccess
{
    /// <summary>
    /// Singleton class to manage SQLite database connection.
    /// </summary>
    public class SQLiteConnectionManager
    {
        private static SQLiteConnectionManager _instance = null!;
        private static readonly object _lock = new object();
        private readonly string _connectionString;

        private SQLiteConnectionManager()
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "library.db");
            _connectionString = $"Data Source={dbPath};Version=3;";
            InitializeDatabase();
        }

        public static SQLiteConnectionManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new SQLiteConnectionManager();
                        }
                    }
                }
                return _instance;
            }
        }

        public SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(_connectionString);
        }

        private void InitializeDatabase()
        {
            if (!File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "library.db")))
            {
                SQLiteConnection.CreateFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "library.db"));
                using (var connection = GetConnection())
                {
                    connection.Open();
                    using (var command = new SQLiteCommand(connection))
                    {
                        // Create Books table
                        command.CommandText = @"
                            CREATE TABLE Books (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                Title TEXT NOT NULL,
                                Author TEXT NOT NULL,
                                ISBN TEXT NOT NULL,
                                PublishedYear INTEGER NOT NULL,
                                AvailabilityStatus INTEGER NOT NULL
                            );";
                        command.ExecuteNonQuery();

                        // Create Members table
                        command.CommandText = @"
                            CREATE TABLE Members (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                Name TEXT NOT NULL,
                                Email TEXT NOT NULL,
                                PhoneNumber TEXT NOT NULL,
                                SubscriptionStatus INTEGER NOT NULL
                            );";
                        command.ExecuteNonQuery();

                        // Create Loans table
                        command.CommandText = @"
                            CREATE TABLE Loans (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                BookId INTEGER NOT NULL,
                                MemberId INTEGER NOT NULL,
                                LoanDate TEXT NOT NULL,
                                DueDate TEXT NOT NULL,
                                ReturnDate TEXT,
                                FOREIGN KEY (BookId) REFERENCES Books(Id),
                                FOREIGN KEY (MemberId) REFERENCES Members(Id)
                            );";
                        command.ExecuteNonQuery();

                        // Insert seed data for Books
                        command.CommandText = @"
                            INSERT INTO Books (Title, Author, ISBN, PublishedYear, AvailabilityStatus)
                            VALUES ('The Great Gatsby', 'F. Scott Fitzgerald', '978-0743273565', 1925, 1),
                                   ('To Kill a Mockingbird', 'Harper Lee', '978-0446310789', 1960, 1);";
                        command.ExecuteNonQuery();

                        // Insert seed data for Members
                        command.CommandText = @"
                            INSERT INTO Members (Name, Email, PhoneNumber, SubscriptionStatus)
                            VALUES ('John Doe', 'john.doe@example.com', '123-456-7890', 1),
                                   ('Jane Smith', 'jane.smith@example.com', '098-765-4321', 1);";
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
} 
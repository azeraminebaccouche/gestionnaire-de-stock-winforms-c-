using System;
using System.Collections.Generic;

namespace BibliothequeApp.Entities
{
    /// <summary>
    /// Represents a book in the library.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Unique identifier for the book.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Title of the book.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Author of the book.
        /// </summary>
        public string Author { get; set; } = string.Empty;

        /// <summary>
        /// ISBN number of the book.
        /// </summary>
        public string ISBN { get; set; } = string.Empty;

        /// <summary>
        /// Year the book was published.
        /// </summary>
        public int PublicationYear { get; set; }

        /// <summary>
        /// Publisher of the book.
        /// </summary>
        public string Publisher { get; set; } = string.Empty;

        /// <summary>
        /// Total number of copies of the book.
        /// </summary>
        public int TotalCopies { get; set; }

        /// <summary>
        /// Number of available copies of the book.
        /// </summary>
        public int AvailableCopies { get; set; }

        /// <summary>
        /// Date the book was added to the library.
        /// </summary>
        public DateTime DateAdded { get; set; }

        /// <summary>
        /// Category of the book.
        /// </summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// Description of the book.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Quantity of the book.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Loans associated with the book.
        /// </summary>
        public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
} 
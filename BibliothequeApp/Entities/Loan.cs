using System;

namespace BibliothequeApp.Entities
{
    /// <summary>
    /// Represents a loan record in the library.
    /// </summary>
    public class Loan
    {
        /// <summary>
        /// Unique identifier for the loan.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ID of the book being loaned.
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// ID of the member who borrowed the book.
        /// </summary>
        public int MemberId { get; set; }

        /// <summary>
        /// Date when the book was loaned.
        /// </summary>
        public DateTime BorrowDate { get; set; }

        /// <summary>
        /// Date when the book is due to be returned.
        /// </summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Date when the book was returned (null if not yet returned).
        /// </summary>
        public DateTime? ReturnDate { get; set; }

        /// <summary>
        /// Indicates whether the book has been returned.
        /// </summary>
        public bool IsReturned { get; set; }

        /// <summary>
        /// Amount of fine charged for the loan.
        /// </summary>
        public decimal FineAmount { get; set; }

        /// <summary>
        /// Indicates whether the fine has been paid.
        /// </summary>
        public bool IsFinePaid { get; set; }

        /// <summary>
        /// Notes related to the loan.
        /// </summary>
        public string Notes { get; set; } = string.Empty;

        // Navigation properties
        public virtual Book Book { get; set; } = null!;
        public virtual Member Member { get; set; } = null!;
    }
} 
using System.Collections.Generic;
using System.Threading.Tasks;
using BibliothequeApp.Entities;

namespace BibliothequeApp.Repositories
{
    /// <summary>
    /// Interface for book repository operations.
    /// </summary>
    public interface IBookRepository
    {
        /// <summary>
        /// Retrieves all books.
        /// </summary>
        /// <returns>A list of all books.</returns>
        Task<IEnumerable<Book>> GetAllAsync();

        /// <summary>
        /// Retrieves a book by its ID.
        /// </summary>
        /// <param name="id">The ID of the book.</param>
        /// <returns>The book with the specified ID, or null if not found.</returns>
        Task<Book?> GetByIdAsync(int id);

        /// <summary>
        /// Searches for books by title.
        /// </summary>
        /// <param name="title">The title to search for.</param>
        /// <returns>A list of books matching the title.</returns>
        Task<IEnumerable<Book>> SearchByTitleAsync(string title);

        /// <summary>
        /// Searches for books by author.
        /// </summary>
        /// <param name="author">The author to search for.</param>
        /// <returns>A list of books matching the author.</returns>
        Task<IEnumerable<Book>> SearchByAuthorAsync(string author);

        /// <summary>
        /// Adds a new book.
        /// </summary>
        /// <param name="book">The book to add.</param>
        /// <returns>The ID of the added book.</returns>
        Task<int> AddAsync(Book book);

        /// <summary>
        /// Updates an existing book.
        /// </summary>
        /// <param name="book">The book to update.</param>
        /// <returns>True if the update was successful, false otherwise.</returns>
        Task<bool> UpdateAsync(Book book);

        /// <summary>
        /// Deletes a book by its ID.
        /// </summary>
        /// <param name="id">The ID of the book to delete.</param>
        /// <returns>True if the deletion was successful, false otherwise.</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Updates the availability of a book.
        /// </summary>
        /// <param name="id">The ID of the book to update.</param>
        /// <param name="availableCopies">The new number of available copies.</param>
        /// <returns>True if the update was successful, false otherwise.</returns>
        Task<bool> UpdateAvailabilityAsync(int id, int availableCopies);
    }
} 
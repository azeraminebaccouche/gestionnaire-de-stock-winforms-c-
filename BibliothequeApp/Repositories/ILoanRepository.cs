using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibliothequeApp.Entities;

namespace BibliothequeApp.Repositories
{
    /// <summary>
    /// Interface for loan repository operations.
    /// </summary>
    public interface ILoanRepository
    {
        /// <summary>
        /// Retrieves a loan by its ID.
        /// </summary>
        /// <param name="id">The ID of the loan.</param>
        /// <returns>The loan with the specified ID, or null if not found.</returns>
        Task<Loan> GetByIdAsync(int id);

        /// <summary>
        /// Retrieves all loans.
        /// </summary>
        /// <returns>A list of all loans.</returns>
        Task<IEnumerable<Loan>> GetAllAsync();

        /// <summary>
        /// Retrieves active loans.
        /// </summary>
        /// <returns>A list of active loans.</returns>
        Task<IEnumerable<Loan>> GetActiveLoansAsync();

        /// <summary>
        /// Retrieves overdue loans.
        /// </summary>
        /// <returns>A list of overdue loans.</returns>
        Task<IEnumerable<Loan>> GetOverdueLoansAsync();

        /// <summary>
        /// Retrieves loans for a specific member.
        /// </summary>
        /// <param name="memberId">The ID of the member.</param>
        /// <returns>A list of loans for the specified member.</returns>
        Task<IEnumerable<Loan>> GetMemberLoansAsync(int memberId);

        /// <summary>
        /// Retrieves loans for a specific book.
        /// </summary>
        /// <param name="bookId">The ID of the book.</param>
        /// <returns>A list of loans for the specified book.</returns>
        Task<IEnumerable<Loan>> GetBookLoansAsync(int bookId);

        /// <summary>
        /// Adds a new loan.
        /// </summary>
        /// <param name="loan">The loan to add.</param>
        /// <returns>The ID of the added loan.</returns>
        Task<int> AddAsync(Loan loan);

        /// <summary>
        /// Updates an existing loan.
        /// </summary>
        /// <param name="loan">The loan to update.</param>
        /// <returns>True if the update was successful, false otherwise.</returns>
        Task<bool> UpdateAsync(Loan loan);

        /// <summary>
        /// Deletes a loan by its ID.
        /// </summary>
        /// <param name="id">The ID of the loan to delete.</param>
        /// <returns>True if the deletion was successful, false otherwise.</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Returns a book and calculates the fine amount.
        /// </summary>
        /// <param name="id">The ID of the loan.</param>
        /// <param name="returnDate">The date the book was returned.</param>
        /// <param name="fineAmount">The calculated fine amount.</param>
        /// <returns>True if the return was successful, false otherwise.</returns>
        Task<bool> ReturnBookAsync(int id, DateTime returnDate, decimal fineAmount);
    }
} 
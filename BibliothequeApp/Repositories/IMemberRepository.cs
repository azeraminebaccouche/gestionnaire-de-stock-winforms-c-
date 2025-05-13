using System.Collections.Generic;
using System.Threading.Tasks;
using BibliothequeApp.Entities;

namespace BibliothequeApp.Repositories
{
    /// <summary>
    /// Interface for member repository operations.
    /// </summary>
    public interface IMemberRepository
    {
        /// <summary>
        /// Retrieves all members.
        /// </summary>
        /// <returns>A list of all members.</returns>
        Task<IEnumerable<Member>> GetAllAsync();

        /// <summary>
        /// Retrieves a member by their ID.
        /// </summary>
        /// <param name="id">The ID of the member.</param>
        /// <returns>The member with the specified ID, or null if not found.</returns>
        Task<Member?> GetByIdAsync(int id);

        /// <summary>
        /// Searches for members by name.
        /// </summary>
        /// <param name="name">The name to search for.</param>
        /// <returns>A list of members matching the search criteria.</returns>
        Task<IEnumerable<Member>> SearchByNameAsync(string name);

        /// <summary>
        /// Searches for members by email.
        /// </summary>
        /// <param name="email">The email to search for.</param>
        /// <returns>A list of members matching the search criteria.</returns>
        Task<IEnumerable<Member>> SearchByEmailAsync(string email);

        /// <summary>
        /// Adds a new member.
        /// </summary>
        /// <param name="member">The member to add.</param>
        /// <returns>The ID of the added member.</returns>
        Task<int> AddAsync(Member member);

        /// <summary>
        /// Updates an existing member.
        /// </summary>
        /// <param name="member">The member to update.</param>
        /// <returns>True if the update was successful, false otherwise.</returns>
        Task<bool> UpdateAsync(Member member);

        /// <summary>
        /// Deletes a member by their ID.
        /// </summary>
        /// <param name="id">The ID of the member to delete.</param>
        /// <returns>True if the deletion was successful, false otherwise.</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Updates the number of books borrowed by a member.
        /// </summary>
        /// <param name="id">The ID of the member.</param>
        /// <param name="booksBorrowed">The new number of books borrowed.</param>
        /// <returns>True if the update was successful, false otherwise.</returns>
        Task<bool> UpdateBooksBorrowedAsync(int id, int booksBorrowed);

        /// <summary>
        /// Updates the status of a member.
        /// </summary>
        /// <param name="id">The ID of the member.</param>
        /// <param name="isActive">The new status of the member.</param>
        /// <returns>True if the update was successful, false otherwise.</returns>
        Task<bool> UpdateStatusAsync(int id, bool isActive);
    }
} 
using System;
using System.Collections.Generic;

namespace BibliothequeApp.Entities
{
    /// <summary>
    /// Represents a member of the library.
    /// </summary>
    public class Member
    {
        /// <summary>
        /// Unique identifier for the member.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// First name of the member.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Last name of the member.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Email address of the member.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Phone number of the member.
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Address of the member.
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Maximum number of books allowed to be borrowed by the member.
        /// </summary>
        public int MaxBooksAllowed { get; set; } = 3;

        /// <summary>
        /// Membership expiry date of the member.
        /// </summary>
        public DateTime MembershipExpiryDate { get; set; }

        /// <summary>
        /// Subscription status (true if active, false if inactive).
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Current number of books borrowed by the member.
        /// </summary>
        public int CurrentBooksBorrowed { get; set; }

        public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
} 
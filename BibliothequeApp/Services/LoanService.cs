using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibliothequeApp.Entities;
using BibliothequeApp.Repositories;

namespace BibliothequeApp.Services
{
    /// <summary>
    /// Service class for handling loan-related business logic.
    /// </summary>
    public class LoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMemberRepository _memberRepository;
        private const decimal DAILY_FINE_RATE = 1.00m;
        private const int LOAN_DURATION_DAYS = 14;

        public LoanService(ILoanRepository loanRepository, IBookRepository bookRepository, IMemberRepository memberRepository)
        {
            _loanRepository = loanRepository;
            _bookRepository = bookRepository;
            _memberRepository = memberRepository;
        }

        public async Task<Loan> GetLoanByIdAsync(int id)
        {
            var loan = await _loanRepository.GetByIdAsync(id);
            if (loan == null)
                throw new KeyNotFoundException($"Loan with ID {id} not found");
            return loan;
        }

        public async Task<IEnumerable<Loan>> GetAllLoansAsync()
        {
            return await _loanRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Loan>> GetActiveLoansAsync()
        {
            var loans = await _loanRepository.GetAllAsync();
            return loans.Where(l => !l.IsReturned);
        }

        public async Task<IEnumerable<Loan>> GetOverdueLoansAsync()
        {
            var loans = await _loanRepository.GetAllAsync();
            return loans.Where(l => !l.IsReturned && l.DueDate < DateTime.Now);
        }

        public async Task<IEnumerable<Loan>> GetMemberLoansAsync(int memberId)
        {
            var loans = await _loanRepository.GetAllAsync();
            return loans.Where(l => l.MemberId == memberId);
        }

        public async Task<IEnumerable<Loan>> GetBookLoansAsync(int bookId)
        {
            var loans = await _loanRepository.GetAllAsync();
            return loans.Where(l => l.BookId == bookId);
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
                throw new KeyNotFoundException($"Book with ID {id} not found");
            return book;
        }

        public async Task<Member> GetMemberByIdAsync(int id)
        {
            var member = await _memberRepository.GetByIdAsync(id);
            if (member == null)
                throw new KeyNotFoundException($"Member with ID {id} not found");
            return member;
        }

        public async Task<Loan> CreateLoanAsync(int bookId, int memberId, DateTime dueDate)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            var member = await _memberRepository.GetByIdAsync(memberId);

            if (book == null || member == null)
            {
                throw new ArgumentException("Book or member not found.");
            }

            if (book.AvailableCopies <= 0)
            {
                throw new InvalidOperationException("Book is not available for loan.");
            }

            if (!member.IsActive)
            {
                throw new InvalidOperationException("Member is not active.");
            }

            if (member.CurrentBooksBorrowed >= member.MaxBooksAllowed)
            {
                throw new InvalidOperationException("Member has reached maximum number of books allowed.");
            }

            var loan = new Loan
            {
                BookId = bookId,
                MemberId = memberId,
                BorrowDate = DateTime.Now,
                DueDate = dueDate,
                IsReturned = false,
                FineAmount = 0,
                IsFinePaid = true
            };

            await _loanRepository.AddAsync(loan);
            await _bookRepository.UpdateAvailabilityAsync(bookId, book.AvailableCopies - 1);
            await _memberRepository.UpdateBooksBorrowedAsync(memberId, member.CurrentBooksBorrowed + 1);

            return loan;
        }

        public async Task<Loan> ReturnLoanAsync(int loanId)
        {
            var loan = await _loanRepository.GetByIdAsync(loanId);
            if (loan == null)
            {
                throw new ArgumentException("Loan not found.");
            }

            if (loan.IsReturned)
            {
                throw new InvalidOperationException("Book has already been returned.");
            }

            var book = await _bookRepository.GetByIdAsync(loan.BookId);
            if (book == null)
            {
                throw new InvalidOperationException("Book information not found.");
            }
            
            book.AvailableCopies++;
            await _bookRepository.UpdateAsync(book);

            loan.IsReturned = true;
            loan.ReturnDate = DateTime.Now;
            
            // Ensure ReturnDate is not null before using Value
            DateTime returnDate = loan.ReturnDate ?? DateTime.Now;
            loan.FineAmount = CalculateFine(loan.DueDate, returnDate);

            await _loanRepository.UpdateAsync(loan);
            return loan;
        }

        public async Task<Loan> ReturnBookAsync(int loanId)
        {
            return await ReturnLoanAsync(loanId);
        }

        public async Task<Loan> DeleteLoanAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid loan ID", nameof(id));

            var loan = await _loanRepository.GetByIdAsync(id);
            if (loan == null)
                throw new ArgumentException("Loan not found", nameof(id));

            await _loanRepository.DeleteAsync(id);
            return loan;
        }

        public static decimal CalculateFine(DateTime dueDate, DateTime returnDate)
        {
            if (returnDate <= dueDate)
            {
                return 0;
            }

            var daysOverdue = (returnDate - dueDate).Days;
            return daysOverdue * DAILY_FINE_RATE;
        }
    }
} 
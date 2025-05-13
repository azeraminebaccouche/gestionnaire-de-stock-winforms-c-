using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibliothequeApp.Entities;
using BibliothequeApp.Repositories;

namespace BibliothequeApp.Services
{
    /// <summary>
    /// Service class for handling book-related business logic.
    /// </summary>
    public class BookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
                throw new KeyNotFoundException($"Book with ID {id} not found");
            return book;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Book>> SearchBooksByTitleAsync(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty", nameof(title));

            return await _bookRepository.SearchByTitleAsync(title);
        }

        public async Task<IEnumerable<Book>> SearchBooksByAuthorAsync(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
                throw new ArgumentException("Author cannot be empty", nameof(author));

            return await _bookRepository.SearchByAuthorAsync(author);
        }

        public async Task<int> AddBookAsync(Book book)
        {
            ValidateBook(book);
            return await _bookRepository.AddAsync(book);
        }

        public async Task<bool> UpdateBookAsync(Book book)
        {
            ValidateBook(book);
            return await _bookRepository.UpdateAsync(book);
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid book ID", nameof(id));

            return await _bookRepository.DeleteAsync(id);
        }

        public async Task<bool> UpdateBookAvailabilityAsync(int id, int availableCopies)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid book ID", nameof(id));

            if (availableCopies < 0)
                throw new ArgumentException("Available copies cannot be negative", nameof(availableCopies));

            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
                throw new KeyNotFoundException($"Book with ID {id} not found");

            if (availableCopies > book.TotalCopies)
                throw new ArgumentException("Available copies cannot exceed total copies", nameof(availableCopies));

            return await _bookRepository.UpdateAvailabilityAsync(id, availableCopies);
        }

        private void ValidateBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            if (string.IsNullOrWhiteSpace(book.Title))
                throw new ArgumentException("Title is required", nameof(book.Title));

            if (string.IsNullOrWhiteSpace(book.Author))
                throw new ArgumentException("Author is required", nameof(book.Author));

            if (book.TotalCopies < 0)
                throw new ArgumentException("Total copies cannot be negative", nameof(book.TotalCopies));

            if (book.AvailableCopies < 0)
                throw new ArgumentException("Available copies cannot be negative", nameof(book.AvailableCopies));

            if (book.AvailableCopies > book.TotalCopies)
                throw new ArgumentException("Available copies cannot exceed total copies", nameof(book.AvailableCopies));

            if (book.PublicationYear < 0 || book.PublicationYear > DateTime.Now.Year)
                throw new ArgumentException("Invalid publication year", nameof(book.PublicationYear));
        }
    }
} 
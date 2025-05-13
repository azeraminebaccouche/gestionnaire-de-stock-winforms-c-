using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibliothequeApp.Entities;
using BibliothequeApp.Repositories;

namespace BibliothequeApp.Services
{
    /// <summary>
    /// Service class for handling member-related business logic.
    /// </summary>
    public class MemberService
    {
        private readonly IMemberRepository _memberRepository;

        public MemberService(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<Member> GetMemberByIdAsync(int id)
        {
            var member = await _memberRepository.GetByIdAsync(id);
            if (member == null)
                throw new KeyNotFoundException($"Member with ID {id} not found");
            return member;
        }

        public async Task<IEnumerable<Member>> GetAllMembersAsync()
        {
            return await _memberRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Member>> SearchMembersByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty", nameof(name));

            return await _memberRepository.SearchByNameAsync(name);
        }

        public async Task<IEnumerable<Member>> SearchMembersByEmailAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty", nameof(email));

            return await _memberRepository.SearchByEmailAsync(email);
        }

        public async Task<int> AddMemberAsync(Member member)
        {
            ValidateMember(member);
            return await _memberRepository.AddAsync(member);
        }

        public async Task<bool> UpdateMemberAsync(Member member)
        {
            ValidateMember(member);
            return await _memberRepository.UpdateAsync(member);
        }

        public async Task<bool> DeleteMemberAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid member ID", nameof(id));

            return await _memberRepository.DeleteAsync(id);
        }

        public async Task<bool> UpdateBooksBorrowedAsync(int id, int booksBorrowed)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid member ID", nameof(id));

            if (booksBorrowed < 0)
                throw new ArgumentException("Books borrowed cannot be negative", nameof(booksBorrowed));

            var member = await _memberRepository.GetByIdAsync(id);
            if (member == null)
                throw new KeyNotFoundException($"Member with ID {id} not found");

            if (booksBorrowed > member.MaxBooksAllowed)
                throw new ArgumentException("Books borrowed cannot exceed maximum allowed", nameof(booksBorrowed));

            return await _memberRepository.UpdateBooksBorrowedAsync(id, booksBorrowed);
        }

        private void ValidateMember(Member member)
        {
            if (member == null)
                throw new ArgumentNullException(nameof(member));

            if (string.IsNullOrWhiteSpace(member.FirstName))
                throw new ArgumentException("First name is required", nameof(member.FirstName));

            if (string.IsNullOrWhiteSpace(member.LastName))
                throw new ArgumentException("Last name is required", nameof(member.LastName));

            if (string.IsNullOrWhiteSpace(member.Email))
                throw new ArgumentException("Email is required", nameof(member.Email));

            if (member.MaxBooksAllowed < 0)
                throw new ArgumentException("Maximum books allowed cannot be negative", nameof(member.MaxBooksAllowed));

            if (member.CurrentBooksBorrowed < 0)
                throw new ArgumentException("Current books borrowed cannot be negative", nameof(member.CurrentBooksBorrowed));

            if (member.CurrentBooksBorrowed > member.MaxBooksAllowed)
                throw new ArgumentException("Current books borrowed cannot exceed maximum allowed", nameof(member.CurrentBooksBorrowed));

            if (member.MembershipExpiryDate < DateTime.Now)
                throw new ArgumentException("Membership has expired", nameof(member.MembershipExpiryDate));
        }
    }
} 
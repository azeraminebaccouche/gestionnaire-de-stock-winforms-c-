using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BibliothequeApp.Entities;
using BibliothequeApp.Services;
using System.Threading.Tasks;

namespace BibliothequeApp.UI
{
    public partial class MainForm : Form
    {
        private readonly BookService _bookService;
        private readonly MemberService _memberService;
        private readonly LoanService _loanService;

        public MainForm(BookService bookService, MemberService memberService, LoanService loanService)
        {
            InitializeComponent();
            _bookService = bookService;
            _memberService = memberService;
            _loanService = loanService;
            
            // Initialize TabControl
            InitializeTabControl();
        }
        
        private void InitializeTabControl()
        {
            // Configure tabs and ensure only one tab is visible at a time
            tabControl.Dock = DockStyle.Fill;
            tabControl.Visible = true;
            
            // Initialize selected tab
            tabControl.SelectedIndex = 0;
            
            // Add event handler to ensure proper tab switching
            tabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
        }
        
        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update data when switching tabs
            RefreshTabData(tabControl.SelectedTab);
        }
        
        private async void RefreshTabData(TabPage selectedTab)
        {
            try
            {
                if (selectedTab == tabBooks)
                {
                    await RefreshBooksGrid();
                }
                else if (selectedTab == tabMembers)
                {
                    await RefreshMembersGrid();
                }
                else if (selectedTab == tabLoans)
                {
                    await RefreshLoansGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task RefreshAllData()
        {
            await RefreshBooksGrid();
            await RefreshMembersGrid();
            await RefreshLoansGrid();
        }

        private async Task RefreshBooksGrid()
        {
            try
            {
                var books = await _bookService.GetAllBooksAsync();
                dgvBooks.DataSource = new List<Book>(books);
                dgvBooks.Columns["Description"].Visible = false;
                dgvBooks.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading books: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task RefreshMembersGrid()
        {
            try
            {
                var members = await _memberService.GetAllMembersAsync();
                dgvMembers.DataSource = new List<Member>(members);
                dgvMembers.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading members: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task RefreshLoansGrid()
        {
            try
            {
                var loans = await _loanService.GetAllLoansAsync();
                dgvLoans.DataSource = new List<Loan>(loans);
                dgvLoans.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading loans: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Book Tab Buttons
        private async void btnAddBook_Click(object sender, EventArgs e)
        {
            using (var form = new BookForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        await _bookService.AddBookAsync(form.Book);
                        await RefreshBooksGrid();
                        MessageBox.Show("Book added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error adding book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async void btnUpdateBook_Click(object sender, EventArgs e)
        {
            if (dgvBooks.CurrentRow == null || dgvBooks.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Please select a book to update.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedBook = (Book)dgvBooks.CurrentRow.DataBoundItem;
            
            using (var editForm = new BookForm(selectedBook))
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        await _bookService.UpdateBookAsync(editForm.Book);
                        await RefreshBooksGrid();
                        MessageBox.Show("Book updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error updating book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async void btnDeleteBook_Click(object sender, EventArgs e)
        {
            if (dgvBooks.CurrentRow == null || dgvBooks.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Please select a book to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedBook = (Book)dgvBooks.CurrentRow.DataBoundItem;
            
            if (MessageBox.Show($"Are you sure you want to delete '{selectedBook.Title}'?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    await _bookService.DeleteBookAsync(selectedBook.Id);
                    await RefreshBooksGrid();
                    MessageBox.Show("Book deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region Member Tab Buttons
        private async void btnAddMember_Click(object sender, EventArgs e)
        {
            using (var form = new MemberForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        await _memberService.AddMemberAsync(form.Member);
                        await RefreshMembersGrid();
                        MessageBox.Show("Member added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error adding member: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async void btnUpdateMember_Click(object sender, EventArgs e)
        {
            if (dgvMembers.CurrentRow == null || dgvMembers.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Please select a member to update.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedMember = (Member)dgvMembers.CurrentRow.DataBoundItem;
            
            using (var editForm = new MemberForm(selectedMember))
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        await _memberService.UpdateMemberAsync(editForm.Member);
                        await RefreshMembersGrid();
                        MessageBox.Show("Member updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error updating member: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async void btnDeleteMember_Click(object sender, EventArgs e)
        {
            if (dgvMembers.CurrentRow == null || dgvMembers.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Please select a member to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedMember = (Member)dgvMembers.CurrentRow.DataBoundItem;
            
            if (MessageBox.Show($"Are you sure you want to delete {selectedMember.FirstName} {selectedMember.LastName}?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    await _memberService.DeleteMemberAsync(selectedMember.Id);
                    await RefreshMembersGrid();
                    MessageBox.Show("Member deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting member: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region Loan Tab Buttons
        private async void btnAddLoan_Click(object sender, EventArgs e)
        {
            var loanForm = new LoanForm(_bookService, _memberService);
            if (loanForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var dueDate = DateTime.Now.AddDays(14); // Default loan duration of 14 days
                    await _loanService.CreateLoanAsync(loanForm.BookId, loanForm.MemberId, dueDate);
                    await RefreshLoansGrid();
                    MessageBox.Show("Book loaned successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loaning book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnReturnBook_Click(object sender, EventArgs e)
        {
            if (dgvLoans.CurrentRow == null || dgvLoans.CurrentRow.DataBoundItem == null)
            {
                MessageBox.Show("Please select a loan to return.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedLoan = (Loan)dgvLoans.CurrentRow.DataBoundItem;
            
            try
            {
                await _loanService.ReturnBookAsync(selectedLoan.Id);
                await RefreshLoansGrid();
                MessageBox.Show("Book returned successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error returning book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Menu Items (keep for backward compatibility)
        private void addBookMenuItem_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabBooks;
            btnAddBook_Click(sender, e);
        }

        private async void editBookMenuItem_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabBooks;
            using (var form = new BookSelectionForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var book = await _bookService.GetBookByIdAsync(form.SelectedBookId);
                        if (book != null)
                        {
                            using (var editForm = new BookForm(book))
                            {
                                if (editForm.ShowDialog() == DialogResult.OK)
                                {
                                    await _bookService.UpdateBookAsync(editForm.Book);
                                    await RefreshBooksGrid();
                                    MessageBox.Show("Book updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error updating book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async void deleteBookMenuItem_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabBooks;
            using (var form = new BookSelectionForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (MessageBox.Show("Are you sure you want to delete this book?", "Confirm Delete",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            await _bookService.DeleteBookAsync(form.SelectedBookId);
                            await RefreshBooksGrid();
                            MessageBox.Show("Book deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void addMemberMenuItem_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabMembers;
            btnAddMember_Click(sender, e);
        }

        private async void editMemberMenuItem_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabMembers;
            using (var form = new MemberSelectionForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var member = await _memberService.GetMemberByIdAsync(form.SelectedMemberId);
                        if (member != null)
                        {
                            using (var editForm = new MemberForm(member))
                            {
                                if (editForm.ShowDialog() == DialogResult.OK)
                                {
                                    await _memberService.UpdateMemberAsync(editForm.Member);
                                    await RefreshMembersGrid();
                                    MessageBox.Show("Member updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error updating member: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async void deleteMemberMenuItem_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabMembers;
            using (var form = new MemberSelectionForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (MessageBox.Show("Are you sure you want to delete this member?", "Confirm Delete",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            await _memberService.DeleteMemberAsync(form.SelectedMemberId);
                            await RefreshMembersGrid();
                            MessageBox.Show("Member deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting member: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void borrowBookMenuItem_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabLoans;
            btnAddLoan_Click(sender, e);
        }

        private async void returnBookMenuItem_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabLoans;
            using (var form = new LoanSelectionForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        await _loanService.ReturnBookAsync(form.SelectedLoanId);
                        await RefreshLoansGrid();
                        MessageBox.Show("Book returned successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error returning book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void overdueBooksMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (var form = new OverdueBooksForm())
                {
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading overdue books: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void memberHistoryMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new MemberSelectionForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var historyForm = new MemberHistoryForm(form.SelectedMemberId))
                        {
                            historyForm.ShowDialog();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading member history: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void bookHistoryMenuItem_Click(object sender, EventArgs e)
        {
            using (var form = new BookSelectionForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var historyForm = new BookHistoryForm(form.SelectedBookId))
                        {
                            historyForm.ShowDialog();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading book history: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        #endregion

        private async void MainForm_Load(object sender, EventArgs e)
        {
            // Load all data for the first time
            await RefreshAllData();
        }
    }
} 
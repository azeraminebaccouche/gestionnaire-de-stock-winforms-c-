using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BibliothequeApp.Entities;
using BibliothequeApp.Services;
using BibliothequeApp.Repositories;

namespace BibliothequeApp.UI
{
    public partial class BookHistoryForm : Form
    {
        private readonly LoanService _loanService;
        private readonly int _bookId;

        public BookHistoryForm(int bookId)
        {
            InitializeComponent();
            _bookId = bookId;
            _loanService = new LoanService(
                new LoanRepository(),
                new BookRepository(),
                new MemberRepository()
            );
            LoadBookLoans();
        }

        private void InitializeComponent()
        {
            this.Text = "Book Loan History";
            this.Size = new System.Drawing.Size(900, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Create DataGridView
            var dataGridView = new DataGridView
            {
                Name = "bookLoansDataGridView",
                Location = new System.Drawing.Point(12, 12),
                Size = new System.Drawing.Size(860, 400),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            // Add columns
            dataGridView.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { Name = "MemberName", HeaderText = "Member", Width = 200 },
                new DataGridViewTextBoxColumn { Name = "BorrowDate", HeaderText = "Borrow Date", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "DueDate", HeaderText = "Due Date", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "ReturnDate", HeaderText = "Return Date", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "Status", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "FineAmount", HeaderText = "Fine Amount", Width = 100 }
            });

            // Create close button
            var closeButton = new Button
            {
                Text = "Close",
                DialogResult = DialogResult.Cancel,
                Location = new System.Drawing.Point(780, 420)
            };

            // Add controls to form
            this.Controls.AddRange(new Control[] { dataGridView, closeButton });
        }

        private async void LoadBookLoans()
        {
            try
            {
                var dataGridView = (DataGridView)Controls["bookLoansDataGridView"];
                dataGridView.Rows.Clear();

                var bookLoans = await _loanService.GetBookLoansAsync(_bookId);
                foreach (var loan in bookLoans)
                {
                    var member = await _loanService.GetMemberByIdAsync(loan.MemberId);
                    var status = loan.IsReturned ? "Returned" : (loan.DueDate < DateTime.Now ? "Overdue" : "Active");
                    var fineAmount = loan.IsReturned ? loan.FineAmount : (loan.DueDate < DateTime.Now ? LoanService.CalculateFine(loan.DueDate, DateTime.Now) : 0);

                    dataGridView.Rows.Add(
                        $"{member.FirstName} {member.LastName}",
                        loan.BorrowDate.ToShortDateString(),
                        loan.DueDate.ToShortDateString(),
                        loan.ReturnDate?.ToShortDateString() ?? "-",
                        status,
                        fineAmount.ToString("C")
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading book loans: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
} 
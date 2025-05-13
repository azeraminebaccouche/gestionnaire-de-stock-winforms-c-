using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BibliothequeApp.Entities;
using BibliothequeApp.Services;
using BibliothequeApp.Repositories;

namespace BibliothequeApp.UI
{
    public partial class OverdueBooksForm : Form
    {
        private readonly LoanService _loanService;

        public OverdueBooksForm()
        {
            InitializeComponent();
            _loanService = new LoanService(
                new LoanRepository(),
                new BookRepository(),
                new MemberRepository()
            );
            LoadOverdueLoans();
        }

        private void InitializeComponent()
        {
            this.Text = "Overdue Books";
            this.Size = new System.Drawing.Size(900, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Create DataGridView
            var dataGridView = new DataGridView
            {
                Name = "overdueLoansDataGridView",
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
                new DataGridViewTextBoxColumn { Name = "BookTitle", HeaderText = "Book", Width = 200 },
                new DataGridViewTextBoxColumn { Name = "MemberName", HeaderText = "Member", Width = 200 },
                new DataGridViewTextBoxColumn { Name = "BorrowDate", HeaderText = "Borrow Date", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "DueDate", HeaderText = "Due Date", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "DaysOverdue", HeaderText = "Days Overdue", Width = 100 },
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

        private async void LoadOverdueLoans()
        {
            try
            {
                var dataGridView = (DataGridView)Controls["overdueLoansDataGridView"];
                dataGridView.Rows.Clear();

                var overdueLoans = await _loanService.GetOverdueLoansAsync();
                foreach (var loan in overdueLoans)
                {
                    var book = await _loanService.GetBookByIdAsync(loan.BookId);
                    var member = await _loanService.GetMemberByIdAsync(loan.MemberId);
                    var daysOverdue = (DateTime.Now - loan.DueDate).Days;
                    var fineAmount = LoanService.CalculateFine(loan.DueDate, DateTime.Now);

                    dataGridView.Rows.Add(
                        book.Title,
                        $"{member.FirstName} {member.LastName}",
                        loan.BorrowDate.ToShortDateString(),
                        loan.DueDate.ToShortDateString(),
                        daysOverdue,
                        fineAmount.ToString("C")
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading overdue loans: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
} 
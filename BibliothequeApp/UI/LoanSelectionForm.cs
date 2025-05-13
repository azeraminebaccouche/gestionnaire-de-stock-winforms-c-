using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BibliothequeApp.Entities;
using BibliothequeApp.Services;
using BibliothequeApp.Repositories;

namespace BibliothequeApp.UI
{
    public partial class LoanSelectionForm : Form
    {
        private readonly LoanService _loanService;
        public int SelectedLoanId { get; private set; }

        public LoanSelectionForm()
        {
            InitializeComponent();
            _loanService = new LoanService(
                new LoanRepository(),
                new BookRepository(),
                new MemberRepository()
            );
            LoadActiveLoans();
        }

        private void InitializeComponent()
        {
            this.Text = "Select Loan to Return";
            this.Size = new System.Drawing.Size(900, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Create DataGridView
            var dataGridView = new DataGridView
            {
                Name = "loansDataGridView",
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
                new DataGridViewTextBoxColumn { Name = "Id", HeaderText = "ID", Width = 50 },
                new DataGridViewTextBoxColumn { Name = "BookTitle", HeaderText = "Book", Width = 200 },
                new DataGridViewTextBoxColumn { Name = "MemberName", HeaderText = "Member", Width = 200 },
                new DataGridViewTextBoxColumn { Name = "BorrowDate", HeaderText = "Borrow Date", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "DueDate", HeaderText = "Due Date", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "Status", Width = 100 }
            });

            // Create buttons
            var selectButton = new Button
            {
                Text = "Select",
                DialogResult = DialogResult.OK,
                Location = new System.Drawing.Point(680, 420)
            };
            selectButton.Click += SelectButton_Click;

            var cancelButton = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Location = new System.Drawing.Point(780, 420)
            };

            // Add controls to form
            this.Controls.AddRange(new Control[] { dataGridView, selectButton, cancelButton });
        }

        private async void LoadActiveLoans()
        {
            try
            {
                var loans = await _loanService.GetActiveLoansAsync();
                var dataGridView = (DataGridView)Controls["loansDataGridView"];
                dataGridView.Rows.Clear();

                foreach (var loan in loans)
                {
                    var book = await _loanService.GetBookByIdAsync(loan.BookId);
                    var member = await _loanService.GetMemberByIdAsync(loan.MemberId);
                    var status = loan.DueDate < DateTime.Now ? "Overdue" : "Active";

                    dataGridView.Rows.Add(
                        loan.Id,
                        book.Title,
                        $"{member.FirstName} {member.LastName}",
                        loan.BorrowDate.ToShortDateString(),
                        loan.DueDate.ToShortDateString(),
                        status
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading loans: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SelectButton_Click(object? sender, EventArgs e)
        {
            var dataGridView = (DataGridView)Controls["loansDataGridView"];
            if (dataGridView.SelectedRows.Count > 0)
            {
                SelectedLoanId = Convert.ToInt32(dataGridView.SelectedRows[0].Cells["Id"].Value);
            }
            else
            {
                MessageBox.Show("Please select a loan.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.None;
            }
        }
    }
} 
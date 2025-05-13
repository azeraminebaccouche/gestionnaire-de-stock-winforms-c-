using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BibliothequeApp.Entities;
using BibliothequeApp.Services;
using BibliothequeApp.Repositories;

namespace BibliothequeApp.UI
{
    public partial class BookSelectionForm : Form
    {
        private readonly BookService _bookService;
        public int SelectedBookId { get; private set; }

        public BookSelectionForm()
        {
            InitializeComponent();
            _bookService = new BookService(new BookRepository());
            LoadBooks();
        }

        private void InitializeComponent()
        {
            this.Text = "Select Book";
            this.Size = new System.Drawing.Size(800, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Create DataGridView
            var dataGridView = new DataGridView
            {
                Name = "booksDataGridView",
                Location = new System.Drawing.Point(12, 12),
                Size = new System.Drawing.Size(760, 400),
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
                new DataGridViewTextBoxColumn { Name = "Title", HeaderText = "Title", Width = 200 },
                new DataGridViewTextBoxColumn { Name = "Author", HeaderText = "Author", Width = 150 },
                new DataGridViewTextBoxColumn { Name = "ISBN", HeaderText = "ISBN", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "Category", HeaderText = "Category", Width = 100 },
                new DataGridViewTextBoxColumn { Name = "Quantity", HeaderText = "Quantity", Width = 80 }
            });

            // Create buttons
            var selectButton = new Button
            {
                Text = "Select",
                DialogResult = DialogResult.OK,
                Location = new System.Drawing.Point(580, 420)
            };
            selectButton.Click += SelectButton_Click;

            var cancelButton = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Location = new System.Drawing.Point(680, 420)
            };

            // Add controls to form
            this.Controls.AddRange(new Control[] { dataGridView, selectButton, cancelButton });
        }

        private async void LoadBooks()
        {
            try
            {
                var books = await _bookService.GetAllBooksAsync();
                var dataGridView = (DataGridView)Controls["booksDataGridView"];
                dataGridView.Rows.Clear();

                foreach (var book in books)
                {
                    dataGridView.Rows.Add(
                        book.Id,
                        book.Title,
                        book.Author,
                        book.ISBN,
                        book.Category,
                        book.Quantity
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading books: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SelectButton_Click(object? sender, EventArgs e)
        {
            var dataGridView = (DataGridView)Controls["booksDataGridView"];
            if (dataGridView.SelectedRows.Count > 0)
            {
                SelectedBookId = Convert.ToInt32(dataGridView.SelectedRows[0].Cells["Id"].Value);
            }
            else
            {
                MessageBox.Show("Please select a book.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.None;
            }
        }
    }
} 
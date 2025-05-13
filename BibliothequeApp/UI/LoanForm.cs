using System;
using System.Windows.Forms;
using BibliothequeApp.Services;

namespace BibliothequeApp.UI
{
    public partial class LoanForm : Form
    {
        private readonly BookService _bookService;
        private readonly MemberService _memberService;
        public int BookId { get; private set; }
        public int MemberId { get; private set; }

        public LoanForm(BookService bookService, MemberService memberService)
        {
            InitializeComponent();
            _bookService = bookService;
            _memberService = memberService;
            LoadBooks();
            LoadMembers();
        }

        private void InitializeComponent()
        {
            this.Text = "Create New Loan";
            this.Size = new System.Drawing.Size(600, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Create controls
            var bookLabel = new Label { Text = "Book:", Location = new System.Drawing.Point(20, 20) };
            var bookComboBox = new ComboBox
            {
                Name = "bookComboBox",
                Location = new System.Drawing.Point(120, 20),
                Width = 400,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            var memberLabel = new Label { Text = "Member:", Location = new System.Drawing.Point(20, 60) };
            var memberComboBox = new ComboBox
            {
                Name = "memberComboBox",
                Location = new System.Drawing.Point(120, 60),
                Width = 400,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            var dueDateLabel = new Label { Text = "Due Date:", Location = new System.Drawing.Point(20, 100) };
            var dueDatePicker = new DateTimePicker
            {
                Name = "dueDatePicker",
                Location = new System.Drawing.Point(120, 100),
                Width = 200,
                Format = DateTimePickerFormat.Short,
                MinDate = DateTime.Today.AddDays(1),
                Value = DateTime.Today.AddDays(14)
            };

            var okButton = new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Location = new System.Drawing.Point(120, 300)
            };
            okButton.Click += OkButton_Click;

            var cancelButton = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Location = new System.Drawing.Point(220, 300)
            };

            // Add controls to form
            this.Controls.AddRange(new Control[]
            {
                bookLabel, bookComboBox,
                memberLabel, memberComboBox,
                dueDateLabel, dueDatePicker,
                okButton, cancelButton
            });
        }

        private async void LoadBooks()
        {
            try
            {
                var books = await _bookService.GetAllBooksAsync();
                var bookComboBox = (ComboBox)Controls["bookComboBox"];
                bookComboBox.Items.Clear();

                foreach (var book in books)
                {
                    if (book.Quantity > 0)
                    {
                        bookComboBox.Items.Add(new BookItem { Id = book.Id, Title = $"{book.Title} by {book.Author}" });
                    }
                }

                if (bookComboBox.Items.Count > 0)
                {
                    bookComboBox.DisplayMember = "Title";
                    bookComboBox.ValueMember = "Id";
                    bookComboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading books: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void LoadMembers()
        {
            try
            {
                var members = await _memberService.GetAllMembersAsync();
                var memberComboBox = (ComboBox)Controls["memberComboBox"];
                memberComboBox.Items.Clear();

                foreach (var member in members)
                {
                    if (member.IsActive && member.CurrentBooksBorrowed < member.MaxBooksAllowed)
                    {
                        memberComboBox.Items.Add(new MemberItem
                        {
                            Id = member.Id,
                            Name = $"{member.FirstName} {member.LastName} ({member.Email})"
                        });
                    }
                }

                if (memberComboBox.Items.Count > 0)
                {
                    memberComboBox.DisplayMember = "Name";
                    memberComboBox.ValueMember = "Id";
                    memberComboBox.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading members: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OkButton_Click(object? sender, EventArgs e)
        {
            var bookComboBox = (ComboBox)Controls["bookComboBox"];
            var memberComboBox = (ComboBox)Controls["memberComboBox"];

            if (bookComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a book.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }

            if (memberComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a member.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }

            BookId = ((BookItem)bookComboBox.SelectedItem).Id;
            MemberId = ((MemberItem)memberComboBox.SelectedItem).Id;
        }

        private class BookItem
        {
            public int Id { get; set; }
            public string Title { get; set; } = string.Empty;
        }

        private class MemberItem
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
        }
    }
} 
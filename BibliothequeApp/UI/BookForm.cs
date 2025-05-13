using System;
using System.Windows.Forms;
using BibliothequeApp.Entities;

namespace BibliothequeApp.UI
{
    public partial class BookForm : Form
    {
        public Book Book { get; private set; } = new Book();

        public BookForm(Book? book = null)
        {
            InitializeComponent();
            if (book != null)
            {
                Book = book;
                LoadBookData();
            }
        }

        private void InitializeComponent()
        {
            this.Text = Book.Id == 0 ? "Add New Book" : "Edit Book";
            this.Size = new System.Drawing.Size(400, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Create controls
            var titleLabel = new Label { Text = "Title:", Location = new System.Drawing.Point(20, 20) };
            var titleTextBox = new TextBox { Name = "titleTextBox", Location = new System.Drawing.Point(120, 20), Width = 250 };

            var authorLabel = new Label { Text = "Author:", Location = new System.Drawing.Point(20, 60) };
            var authorTextBox = new TextBox { Name = "authorTextBox", Location = new System.Drawing.Point(120, 60), Width = 250 };

            var isbnLabel = new Label { Text = "ISBN:", Location = new System.Drawing.Point(20, 100) };
            var isbnTextBox = new TextBox { Name = "isbnTextBox", Location = new System.Drawing.Point(120, 100), Width = 250 };

            var publisherLabel = new Label { Text = "Publisher:", Location = new System.Drawing.Point(20, 140) };
            var publisherTextBox = new TextBox { Name = "publisherTextBox", Location = new System.Drawing.Point(120, 140), Width = 250 };

            var publicationYearLabel = new Label { Text = "Publication Year:", Location = new System.Drawing.Point(20, 180) };
            var publicationYearNumericUpDown = new NumericUpDown 
            { 
                Name = "publicationYearNumericUpDown",
                Location = new System.Drawing.Point(120, 180),
                Width = 100,
                Minimum = 1000,
                Maximum = DateTime.Now.Year,
                Value = DateTime.Now.Year
            };

            var categoryLabel = new Label { Text = "Category:", Location = new System.Drawing.Point(20, 220) };
            var categoryTextBox = new TextBox { Name = "categoryTextBox", Location = new System.Drawing.Point(120, 220), Width = 250 };

            var quantityLabel = new Label { Text = "Quantity:", Location = new System.Drawing.Point(20, 260) };
            var quantityNumericUpDown = new NumericUpDown
            {
                Name = "quantityNumericUpDown",
                Location = new System.Drawing.Point(120, 260),
                Width = 100,
                Minimum = 0,
                Maximum = 1000,
                Value = 1
            };

            var descriptionLabel = new Label { Text = "Description:", Location = new System.Drawing.Point(20, 300) };
            var descriptionTextBox = new TextBox
            {
                Name = "descriptionTextBox",
                Location = new System.Drawing.Point(120, 300),
                Width = 250,
                Height = 60,
                Multiline = true
            };

            var okButton = new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Location = new System.Drawing.Point(120, 400)
            };
            okButton.Click += OkButton_Click;

            var cancelButton = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Location = new System.Drawing.Point(220, 400)
            };

            // Add controls to form
            this.Controls.AddRange(new Control[]
            {
                titleLabel, titleTextBox,
                authorLabel, authorTextBox,
                isbnLabel, isbnTextBox,
                publisherLabel, publisherTextBox,
                publicationYearLabel, publicationYearNumericUpDown,
                categoryLabel, categoryTextBox,
                quantityLabel, quantityNumericUpDown,
                descriptionLabel, descriptionTextBox,
                okButton, cancelButton
            });
        }

        private void LoadBookData()
        {
            Controls["titleTextBox"].Text = Book.Title;
            Controls["authorTextBox"].Text = Book.Author;
            Controls["isbnTextBox"].Text = Book.ISBN;
            Controls["publisherTextBox"].Text = Book.Publisher;
            ((NumericUpDown)Controls["publicationYearNumericUpDown"]).Value = Book.PublicationYear;
            Controls["categoryTextBox"].Text = Book.Category;
            ((NumericUpDown)Controls["quantityNumericUpDown"]).Value = Book.Quantity;
            Controls["descriptionTextBox"].Text = Book.Description;
        }

        private void OkButton_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Controls["titleTextBox"].Text))
            {
                MessageBox.Show("Please enter a title.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }

            if (string.IsNullOrWhiteSpace(Controls["authorTextBox"].Text))
            {
                MessageBox.Show("Please enter an author.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }

            Book.Title = Controls["titleTextBox"].Text;
            Book.Author = Controls["authorTextBox"].Text;
            Book.ISBN = Controls["isbnTextBox"].Text;
            Book.Publisher = Controls["publisherTextBox"].Text;
            Book.PublicationYear = (int)((NumericUpDown)Controls["publicationYearNumericUpDown"]).Value;
            Book.Category = Controls["categoryTextBox"].Text;
            Book.Quantity = (int)((NumericUpDown)Controls["quantityNumericUpDown"]).Value;
            Book.Description = Controls["descriptionTextBox"].Text;
        }
    }
} 
using System;
using System.Windows.Forms;
using BibliothequeApp.Entities;

namespace BibliothequeApp.UI
{
    public partial class MemberForm : Form
    {
        public Member Member { get; private set; } = new Member();

        public MemberForm(Member? member = null)
        {
            InitializeComponent();
            if (member != null)
            {
                Member = member;
                LoadMemberData();
            }
        }

        private void InitializeComponent()
        {
            this.Text = Member.Id == 0 ? "Add New Member" : "Edit Member";
            this.Size = new System.Drawing.Size(400, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Create controls
            var firstNameLabel = new Label { Text = "First Name:", Location = new System.Drawing.Point(20, 20) };
            var firstNameTextBox = new TextBox { Name = "firstNameTextBox", Location = new System.Drawing.Point(120, 20), Width = 250 };

            var lastNameLabel = new Label { Text = "Last Name:", Location = new System.Drawing.Point(20, 60) };
            var lastNameTextBox = new TextBox { Name = "lastNameTextBox", Location = new System.Drawing.Point(120, 60), Width = 250 };

            var emailLabel = new Label { Text = "Email:", Location = new System.Drawing.Point(20, 100) };
            var emailTextBox = new TextBox { Name = "emailTextBox", Location = new System.Drawing.Point(120, 100), Width = 250 };

            var phoneLabel = new Label { Text = "Phone:", Location = new System.Drawing.Point(20, 140) };
            var phoneTextBox = new TextBox { Name = "phoneTextBox", Location = new System.Drawing.Point(120, 140), Width = 250 };

            var addressLabel = new Label { Text = "Address:", Location = new System.Drawing.Point(20, 180) };
            var addressTextBox = new TextBox
            {
                Name = "addressTextBox",
                Location = new System.Drawing.Point(120, 180),
                Width = 250,
                Height = 60,
                Multiline = true
            };

            var maxBooksLabel = new Label { Text = "Max Books:", Location = new System.Drawing.Point(20, 260) };
            var maxBooksNumericUpDown = new NumericUpDown
            {
                Name = "maxBooksNumericUpDown",
                Location = new System.Drawing.Point(120, 260),
                Width = 100,
                Minimum = 1,
                Maximum = 10,
                Value = 3
            };

            var expiryDateLabel = new Label { Text = "Expiry Date:", Location = new System.Drawing.Point(20, 300) };
            var expiryDatePicker = new DateTimePicker
            {
                Name = "expiryDatePicker",
                Location = new System.Drawing.Point(120, 300),
                Width = 200,
                Format = DateTimePickerFormat.Short,
                MinDate = DateTime.Today,
                Value = DateTime.Today.AddYears(1)
            };

            var isActiveLabel = new Label { Text = "Active:", Location = new System.Drawing.Point(20, 340) };
            var isActiveCheckBox = new CheckBox
            {
                Name = "isActiveCheckBox",
                Location = new System.Drawing.Point(120, 340),
                Checked = true
            };

            var okButton = new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Location = new System.Drawing.Point(120, 500)
            };
            okButton.Click += OkButton_Click;

            var cancelButton = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Location = new System.Drawing.Point(220, 500)
            };

            // Add controls to form
            this.Controls.AddRange(new Control[]
            {
                firstNameLabel, firstNameTextBox,
                lastNameLabel, lastNameTextBox,
                emailLabel, emailTextBox,
                phoneLabel, phoneTextBox,
                addressLabel, addressTextBox,
                maxBooksLabel, maxBooksNumericUpDown,
                expiryDateLabel, expiryDatePicker,
                isActiveLabel, isActiveCheckBox,
                okButton, cancelButton
            });
        }

        private void LoadMemberData()
        {
            Controls["firstNameTextBox"].Text = Member.FirstName;
            Controls["lastNameTextBox"].Text = Member.LastName;
            Controls["emailTextBox"].Text = Member.Email;
            Controls["phoneTextBox"].Text = Member.PhoneNumber;
            Controls["addressTextBox"].Text = Member.Address;
            ((NumericUpDown)Controls["maxBooksNumericUpDown"]).Value = Member.MaxBooksAllowed;
            ((DateTimePicker)Controls["expiryDatePicker"]).Value = Member.MembershipExpiryDate;
            ((CheckBox)Controls["isActiveCheckBox"]).Checked = Member.IsActive;
        }

        private void OkButton_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Controls["firstNameTextBox"].Text))
            {
                MessageBox.Show("Please enter a first name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }

            if (string.IsNullOrWhiteSpace(Controls["lastNameTextBox"].Text))
            {
                MessageBox.Show("Please enter a last name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }

            if (string.IsNullOrWhiteSpace(Controls["emailTextBox"].Text))
            {
                MessageBox.Show("Please enter an email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }

            Member.FirstName = Controls["firstNameTextBox"].Text;
            Member.LastName = Controls["lastNameTextBox"].Text;
            Member.Email = Controls["emailTextBox"].Text;
            Member.PhoneNumber = Controls["phoneTextBox"].Text;
            Member.Address = Controls["addressTextBox"].Text;
            Member.MaxBooksAllowed = (int)((NumericUpDown)Controls["maxBooksNumericUpDown"]).Value;
            Member.MembershipExpiryDate = ((DateTimePicker)Controls["expiryDatePicker"]).Value;
            Member.IsActive = ((CheckBox)Controls["isActiveCheckBox"]).Checked;
        }
    }
} 
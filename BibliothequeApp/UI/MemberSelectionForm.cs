using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BibliothequeApp.Entities;
using BibliothequeApp.Services;
using BibliothequeApp.Repositories;

namespace BibliothequeApp.UI
{
    public partial class MemberSelectionForm : Form
    {
        private readonly MemberService _memberService;
        public int SelectedMemberId { get; private set; }

        public MemberSelectionForm()
        {
            InitializeComponent();
            _memberService = new MemberService(new MemberRepository());
            LoadMembers();
        }

        private void InitializeComponent()
        {
            this.Text = "Select Member";
            this.Size = new System.Drawing.Size(800, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Create DataGridView
            var dataGridView = new DataGridView
            {
                Name = "membersDataGridView",
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
                new DataGridViewTextBoxColumn { Name = "FirstName", HeaderText = "First Name", Width = 150 },
                new DataGridViewTextBoxColumn { Name = "LastName", HeaderText = "Last Name", Width = 150 },
                new DataGridViewTextBoxColumn { Name = "Email", HeaderText = "Email", Width = 200 },
                new DataGridViewTextBoxColumn { Name = "Phone", HeaderText = "Phone", Width = 150 },
                new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "Status", Width = 100 }
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

        private async void LoadMembers()
        {
            try
            {
                var members = await _memberService.GetAllMembersAsync();
                var dataGridView = (DataGridView)Controls["membersDataGridView"];
                dataGridView.Rows.Clear();

                foreach (var member in members)
                {
                    dataGridView.Rows.Add(
                        member.Id,
                        member.FirstName,
                        member.LastName,
                        member.Email,
                        member.PhoneNumber,
                        member.IsActive ? "Active" : "Inactive"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading members: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SelectButton_Click(object? sender, EventArgs e)
        {
            var dataGridView = (DataGridView)Controls["membersDataGridView"];
            if (dataGridView.SelectedRows.Count > 0)
            {
                SelectedMemberId = Convert.ToInt32(dataGridView.SelectedRows[0].Cells["Id"].Value);
            }
            else
            {
                MessageBox.Show("Please select a member.", "Selection Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.None;
            }
        }
    }
} 
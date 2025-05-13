namespace BibliothequeApp.UI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.booksMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addBookMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editBookMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteBookMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.membersMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addMemberMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMemberMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteMemberMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loansMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.borrowBookMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.returnBookMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.overdueBooksMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memberHistoryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bookHistoryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabBooks = new System.Windows.Forms.TabPage();
            this.tabMembers = new System.Windows.Forms.TabPage();
            this.tabLoans = new System.Windows.Forms.TabPage();
            this.dgvBooks = new System.Windows.Forms.DataGridView();
            this.dgvMembers = new System.Windows.Forms.DataGridView();
            this.dgvLoans = new System.Windows.Forms.DataGridView();
            this.btnAddBook = new System.Windows.Forms.Button();
            this.btnUpdateBook = new System.Windows.Forms.Button();
            this.btnDeleteBook = new System.Windows.Forms.Button();
            this.btnAddMember = new System.Windows.Forms.Button();
            this.btnUpdateMember = new System.Windows.Forms.Button();
            this.btnDeleteMember = new System.Windows.Forms.Button();
            this.btnAddLoan = new System.Windows.Forms.Button();
            this.btnReturnBook = new System.Windows.Forms.Button();
            this.menuStrip.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabBooks.SuspendLayout();
            this.tabMembers.SuspendLayout();
            this.tabLoans.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMembers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoans)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.booksMenuItem,
                this.membersMenuItem,
                this.loansMenuItem,
                this.reportsMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // booksMenuItem
            // 
            this.booksMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.addBookMenuItem,
                this.editBookMenuItem,
                this.deleteBookMenuItem});
            this.booksMenuItem.Name = "booksMenuItem";
            this.booksMenuItem.Size = new System.Drawing.Size(51, 20);
            this.booksMenuItem.Text = "Books";
            // 
            // addBookMenuItem
            // 
            this.addBookMenuItem.Name = "addBookMenuItem";
            this.addBookMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addBookMenuItem.Text = "Add Book";
            this.addBookMenuItem.Click += new System.EventHandler(this.addBookMenuItem_Click);
            // 
            // editBookMenuItem
            // 
            this.editBookMenuItem.Name = "editBookMenuItem";
            this.editBookMenuItem.Size = new System.Drawing.Size(180, 22);
            this.editBookMenuItem.Text = "Edit Book";
            this.editBookMenuItem.Click += new System.EventHandler(this.editBookMenuItem_Click);
            // 
            // deleteBookMenuItem
            // 
            this.deleteBookMenuItem.Name = "deleteBookMenuItem";
            this.deleteBookMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deleteBookMenuItem.Text = "Delete Book";
            this.deleteBookMenuItem.Click += new System.EventHandler(this.deleteBookMenuItem_Click);
            // 
            // membersMenuItem
            // 
            this.membersMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.addMemberMenuItem,
                this.editMemberMenuItem,
                this.deleteMemberMenuItem});
            this.membersMenuItem.Name = "membersMenuItem";
            this.membersMenuItem.Size = new System.Drawing.Size(69, 20);
            this.membersMenuItem.Text = "Members";
            // 
            // addMemberMenuItem
            // 
            this.addMemberMenuItem.Name = "addMemberMenuItem";
            this.addMemberMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addMemberMenuItem.Text = "Add Member";
            this.addMemberMenuItem.Click += new System.EventHandler(this.addMemberMenuItem_Click);
            // 
            // editMemberMenuItem
            // 
            this.editMemberMenuItem.Name = "editMemberMenuItem";
            this.editMemberMenuItem.Size = new System.Drawing.Size(180, 22);
            this.editMemberMenuItem.Text = "Edit Member";
            this.editMemberMenuItem.Click += new System.EventHandler(this.editMemberMenuItem_Click);
            // 
            // deleteMemberMenuItem
            // 
            this.deleteMemberMenuItem.Name = "deleteMemberMenuItem";
            this.deleteMemberMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deleteMemberMenuItem.Text = "Delete Member";
            this.deleteMemberMenuItem.Click += new System.EventHandler(this.deleteMemberMenuItem_Click);
            // 
            // loansMenuItem
            // 
            this.loansMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.borrowBookMenuItem,
                this.returnBookMenuItem});
            this.loansMenuItem.Name = "loansMenuItem";
            this.loansMenuItem.Size = new System.Drawing.Size(50, 20);
            this.loansMenuItem.Text = "Loans";
            // 
            // borrowBookMenuItem
            // 
            this.borrowBookMenuItem.Name = "borrowBookMenuItem";
            this.borrowBookMenuItem.Size = new System.Drawing.Size(180, 22);
            this.borrowBookMenuItem.Text = "Borrow Book";
            this.borrowBookMenuItem.Click += new System.EventHandler(this.borrowBookMenuItem_Click);
            // 
            // returnBookMenuItem
            // 
            this.returnBookMenuItem.Name = "returnBookMenuItem";
            this.returnBookMenuItem.Size = new System.Drawing.Size(180, 22);
            this.returnBookMenuItem.Text = "Return Book";
            this.returnBookMenuItem.Click += new System.EventHandler(this.returnBookMenuItem_Click);
            // 
            // reportsMenuItem
            // 
            this.reportsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.overdueBooksMenuItem,
                this.memberHistoryMenuItem,
                this.bookHistoryMenuItem});
            this.reportsMenuItem.Name = "reportsMenuItem";
            this.reportsMenuItem.Size = new System.Drawing.Size(59, 20);
            this.reportsMenuItem.Text = "Reports";
            // 
            // overdueBooksMenuItem
            // 
            this.overdueBooksMenuItem.Name = "overdueBooksMenuItem";
            this.overdueBooksMenuItem.Size = new System.Drawing.Size(180, 22);
            this.overdueBooksMenuItem.Text = "Overdue Books";
            this.overdueBooksMenuItem.Click += new System.EventHandler(this.overdueBooksMenuItem_Click);
            // 
            // memberHistoryMenuItem
            // 
            this.memberHistoryMenuItem.Name = "memberHistoryMenuItem";
            this.memberHistoryMenuItem.Size = new System.Drawing.Size(180, 22);
            this.memberHistoryMenuItem.Text = "Member History";
            this.memberHistoryMenuItem.Click += new System.EventHandler(this.memberHistoryMenuItem_Click);
            // 
            // bookHistoryMenuItem
            // 
            this.bookHistoryMenuItem.Name = "bookHistoryMenuItem";
            this.bookHistoryMenuItem.Size = new System.Drawing.Size(180, 22);
            this.bookHistoryMenuItem.Text = "Book History";
            this.bookHistoryMenuItem.Click += new System.EventHandler(this.bookHistoryMenuItem_Click);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabBooks);
            this.tabControl.Controls.Add(this.tabMembers);
            this.tabControl.Controls.Add(this.tabLoans);
            this.tabControl.Location = new System.Drawing.Point(0, 27);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(800, 423);
            this.tabControl.TabIndex = 1;
            this.tabControl.Visible = true;
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            
            // tabBooks
            // 
            this.tabBooks.Controls.Add(this.btnDeleteBook);
            this.tabBooks.Controls.Add(this.btnUpdateBook);
            this.tabBooks.Controls.Add(this.btnAddBook);
            this.tabBooks.Controls.Add(this.dgvBooks);
            this.tabBooks.Location = new System.Drawing.Point(4, 22);
            this.tabBooks.Name = "tabBooks";
            this.tabBooks.Padding = new System.Windows.Forms.Padding(3);
            this.tabBooks.Size = new System.Drawing.Size(792, 397);
            this.tabBooks.TabIndex = 0;
            this.tabBooks.Text = "Books";
            this.tabBooks.UseVisualStyleBackColor = true;
            
            // tabMembers
            // 
            this.tabMembers.Controls.Add(this.btnDeleteMember);
            this.tabMembers.Controls.Add(this.btnUpdateMember);
            this.tabMembers.Controls.Add(this.btnAddMember);
            this.tabMembers.Controls.Add(this.dgvMembers);
            this.tabMembers.Location = new System.Drawing.Point(4, 22);
            this.tabMembers.Name = "tabMembers";
            this.tabMembers.Padding = new System.Windows.Forms.Padding(3);
            this.tabMembers.Size = new System.Drawing.Size(792, 397);
            this.tabMembers.TabIndex = 1;
            this.tabMembers.Text = "Members";
            this.tabMembers.UseVisualStyleBackColor = true;
            
            // tabLoans
            // 
            this.tabLoans.Controls.Add(this.btnReturnBook);
            this.tabLoans.Controls.Add(this.btnAddLoan);
            this.tabLoans.Controls.Add(this.dgvLoans);
            this.tabLoans.Location = new System.Drawing.Point(4, 22);
            this.tabLoans.Name = "tabLoans";
            this.tabLoans.Padding = new System.Windows.Forms.Padding(3);
            this.tabLoans.Size = new System.Drawing.Size(792, 397);
            this.tabLoans.TabIndex = 2;
            this.tabLoans.Text = "Loans";
            this.tabLoans.UseVisualStyleBackColor = true;
            
            // dgvBooks
            // 
            this.dgvBooks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBooks.Location = new System.Drawing.Point(6, 6);
            this.dgvBooks.Name = "dgvBooks";
            this.dgvBooks.Size = new System.Drawing.Size(780, 350);
            this.dgvBooks.TabIndex = 0;
            
            // dgvMembers
            // 
            this.dgvMembers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMembers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMembers.Location = new System.Drawing.Point(6, 6);
            this.dgvMembers.Name = "dgvMembers";
            this.dgvMembers.Size = new System.Drawing.Size(780, 350);
            this.dgvMembers.TabIndex = 0;
            
            // dgvLoans
            // 
            this.dgvLoans.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLoans.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLoans.Location = new System.Drawing.Point(6, 6);
            this.dgvLoans.Name = "dgvLoans";
            this.dgvLoans.Size = new System.Drawing.Size(780, 350);
            this.dgvLoans.TabIndex = 0;
            
            // btnAddBook
            // 
            this.btnAddBook.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddBook.Location = new System.Drawing.Point(6, 362);
            this.btnAddBook.Name = "btnAddBook";
            this.btnAddBook.Size = new System.Drawing.Size(75, 23);
            this.btnAddBook.TabIndex = 1;
            this.btnAddBook.Text = "Add Book";
            this.btnAddBook.UseVisualStyleBackColor = true;
            this.btnAddBook.Click += new System.EventHandler(this.btnAddBook_Click);
            
            // btnUpdateBook
            // 
            this.btnUpdateBook.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpdateBook.Location = new System.Drawing.Point(87, 362);
            this.btnUpdateBook.Name = "btnUpdateBook";
            this.btnUpdateBook.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateBook.TabIndex = 2;
            this.btnUpdateBook.Text = "Update";
            this.btnUpdateBook.UseVisualStyleBackColor = true;
            this.btnUpdateBook.Click += new System.EventHandler(this.btnUpdateBook_Click);
            
            // btnDeleteBook
            // 
            this.btnDeleteBook.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteBook.Location = new System.Drawing.Point(168, 362);
            this.btnDeleteBook.Name = "btnDeleteBook";
            this.btnDeleteBook.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteBook.TabIndex = 3;
            this.btnDeleteBook.Text = "Delete";
            this.btnDeleteBook.UseVisualStyleBackColor = true;
            this.btnDeleteBook.Click += new System.EventHandler(this.btnDeleteBook_Click);
            
            // btnAddMember
            // 
            this.btnAddMember.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddMember.Location = new System.Drawing.Point(6, 362);
            this.btnAddMember.Name = "btnAddMember";
            this.btnAddMember.Size = new System.Drawing.Size(75, 23);
            this.btnAddMember.TabIndex = 1;
            this.btnAddMember.Text = "Add Member";
            this.btnAddMember.UseVisualStyleBackColor = true;
            this.btnAddMember.Click += new System.EventHandler(this.btnAddMember_Click);
            
            // btnUpdateMember
            // 
            this.btnUpdateMember.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnUpdateMember.Location = new System.Drawing.Point(87, 362);
            this.btnUpdateMember.Name = "btnUpdateMember";
            this.btnUpdateMember.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateMember.TabIndex = 2;
            this.btnUpdateMember.Text = "Update";
            this.btnUpdateMember.UseVisualStyleBackColor = true;
            this.btnUpdateMember.Click += new System.EventHandler(this.btnUpdateMember_Click);
            
            // btnDeleteMember
            // 
            this.btnDeleteMember.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteMember.Location = new System.Drawing.Point(168, 362);
            this.btnDeleteMember.Name = "btnDeleteMember";
            this.btnDeleteMember.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteMember.TabIndex = 3;
            this.btnDeleteMember.Text = "Delete";
            this.btnDeleteMember.UseVisualStyleBackColor = true;
            this.btnDeleteMember.Click += new System.EventHandler(this.btnDeleteMember_Click);
            
            // btnAddLoan
            // 
            this.btnAddLoan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddLoan.Location = new System.Drawing.Point(6, 362);
            this.btnAddLoan.Name = "btnAddLoan";
            this.btnAddLoan.Size = new System.Drawing.Size(85, 23);
            this.btnAddLoan.TabIndex = 1;
            this.btnAddLoan.Text = "Borrow Book";
            this.btnAddLoan.UseVisualStyleBackColor = true;
            this.btnAddLoan.Click += new System.EventHandler(this.btnAddLoan_Click);
            
            // btnReturnBook
            // 
            this.btnReturnBook.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReturnBook.Location = new System.Drawing.Point(97, 362);
            this.btnReturnBook.Name = "btnReturnBook";
            this.btnReturnBook.Size = new System.Drawing.Size(85, 23);
            this.btnReturnBook.TabIndex = 2;
            this.btnReturnBook.Text = "Return Book";
            this.btnReturnBook.UseVisualStyleBackColor = true;
            this.btnReturnBook.Click += new System.EventHandler(this.btnReturnBook_Click);
            
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Clear();
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "Library Management System";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabBooks.ResumeLayout(false);
            this.tabMembers.ResumeLayout(false);
            this.tabLoans.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMembers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoans)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem booksMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addBookMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editBookMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteBookMenuItem;
        private System.Windows.Forms.ToolStripMenuItem membersMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addMemberMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editMemberMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteMemberMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loansMenuItem;
        private System.Windows.Forms.ToolStripMenuItem borrowBookMenuItem;
        private System.Windows.Forms.ToolStripMenuItem returnBookMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem overdueBooksMenuItem;
        private System.Windows.Forms.ToolStripMenuItem memberHistoryMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bookHistoryMenuItem;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabBooks;
        private System.Windows.Forms.TabPage tabMembers;
        private System.Windows.Forms.TabPage tabLoans;
        private System.Windows.Forms.DataGridView dgvBooks;
        private System.Windows.Forms.DataGridView dgvMembers;
        private System.Windows.Forms.DataGridView dgvLoans;
        private System.Windows.Forms.Button btnDeleteBook;
        private System.Windows.Forms.Button btnUpdateBook;
        private System.Windows.Forms.Button btnAddBook;
        private System.Windows.Forms.Button btnDeleteMember;
        private System.Windows.Forms.Button btnUpdateMember;
        private System.Windows.Forms.Button btnAddMember;
        private System.Windows.Forms.Button btnReturnBook;
        private System.Windows.Forms.Button btnAddLoan;
    }
} 
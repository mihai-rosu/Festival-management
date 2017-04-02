namespace festivalc.gui
{
    partial class FestivalForm
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
            this.spectacolDataGridView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.searchDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.srcDataGridView = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cumparatorTextBox = new System.Windows.Forms.TextBox();
            this.cantitateTextBox = new System.Windows.Forms.TextBox();
            this.cumparaButton = new System.Windows.Forms.Button();
            this.logoutButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.spectacolDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.srcDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // spectacolDataGridView
            // 
            this.spectacolDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.spectacolDataGridView.Location = new System.Drawing.Point(12, 12);
            this.spectacolDataGridView.Name = "spectacolDataGridView";
            this.spectacolDataGridView.Size = new System.Drawing.Size(443, 282);
            this.spectacolDataGridView.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(115, 312);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cauta spectacole";
            // 
            // searchDateTimePicker
            // 
            this.searchDateTimePicker.Location = new System.Drawing.Point(84, 346);
            this.searchDateTimePicker.Name = "searchDateTimePicker";
            this.searchDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.searchDateTimePicker.TabIndex = 2;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(144, 372);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonSearch.TabIndex = 3;
            this.buttonSearch.Text = "Cauta";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // srcDataGridView
            // 
            this.srcDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.srcDataGridView.Location = new System.Drawing.Point(461, 12);
            this.srcDataGridView.Name = "srcDataGridView";
            this.srcDataGridView.Size = new System.Drawing.Size(424, 354);
            this.srcDataGridView.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label2.Location = new System.Drawing.Point(584, 371);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "Rezultatele cautarii";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.label3.Location = new System.Drawing.Point(923, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 24);
            this.label3.TabIndex = 6;
            this.label3.Text = "Cumpara bilet";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label4.Location = new System.Drawing.Point(891, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Cumparator";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label5.Location = new System.Drawing.Point(891, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Cantitate";
            // 
            // cumparatorTextBox
            // 
            this.cumparatorTextBox.Location = new System.Drawing.Point(979, 63);
            this.cumparatorTextBox.Name = "cumparatorTextBox";
            this.cumparatorTextBox.Size = new System.Drawing.Size(84, 20);
            this.cumparatorTextBox.TabIndex = 9;
            // 
            // cantitateTextBox
            // 
            this.cantitateTextBox.Location = new System.Drawing.Point(979, 98);
            this.cantitateTextBox.Name = "cantitateTextBox";
            this.cantitateTextBox.Size = new System.Drawing.Size(84, 20);
            this.cantitateTextBox.TabIndex = 10;
            // 
            // cumparaButton
            // 
            this.cumparaButton.Location = new System.Drawing.Point(948, 124);
            this.cumparaButton.Name = "cumparaButton";
            this.cumparaButton.Size = new System.Drawing.Size(75, 23);
            this.cumparaButton.TabIndex = 11;
            this.cumparaButton.Text = "Cumpara";
            this.cumparaButton.UseVisualStyleBackColor = true;
            this.cumparaButton.Click += new System.EventHandler(this.cumparaButton_Click);
            // 
            // logoutButton
            // 
            this.logoutButton.Location = new System.Drawing.Point(988, 400);
            this.logoutButton.Name = "logoutButton";
            this.logoutButton.Size = new System.Drawing.Size(75, 23);
            this.logoutButton.TabIndex = 12;
            this.logoutButton.Text = "Logout";
            this.logoutButton.UseVisualStyleBackColor = true;
            this.logoutButton.Click += new System.EventHandler(this.logoutButton_Click);
            // 
            // FestivalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 435);
            this.Controls.Add(this.logoutButton);
            this.Controls.Add(this.cumparaButton);
            this.Controls.Add(this.cantitateTextBox);
            this.Controls.Add(this.cumparatorTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.srcDataGridView);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.searchDateTimePicker);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.spectacolDataGridView);
            this.Name = "FestivalForm";
            this.Text = "FestivalForm";
            ((System.ComponentModel.ISupportInitialize)(this.spectacolDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.srcDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView spectacolDataGridView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker searchDateTimePicker;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.DataGridView srcDataGridView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox cumparatorTextBox;
        private System.Windows.Forms.TextBox cantitateTextBox;
        private System.Windows.Forms.Button cumparaButton;
        private System.Windows.Forms.Button logoutButton;
    }
}
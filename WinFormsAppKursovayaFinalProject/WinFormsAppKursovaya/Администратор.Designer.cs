namespace WinFormsAppKursovaya
{
    partial class Администратор
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridViewUsers = new System.Windows.Forms.DataGridView();
            this.DeleteUser = new System.Windows.Forms.Button();
            this.SaveUser = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.aurorizationlabel = new System.Windows.Forms.Label();
            this.buttonAllTables = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Controls.Add(this.buttonAllTables);
            this.panel1.Controls.Add(this.dataGridViewUsers);
            this.panel1.Controls.Add(this.DeleteUser);
            this.panel1.Controls.Add(this.SaveUser);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(535, 396);
            this.panel1.TabIndex = 1;
            // 
            // dataGridViewUsers
            // 
            this.dataGridViewUsers.AllowUserToAddRows = false;
            this.dataGridViewUsers.AllowUserToDeleteRows = false;
            this.dataGridViewUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUsers.Location = new System.Drawing.Point(29, 87);
            this.dataGridViewUsers.Name = "dataGridViewUsers";
            this.dataGridViewUsers.RowTemplate.Height = 25;
            this.dataGridViewUsers.Size = new System.Drawing.Size(484, 192);
            this.dataGridViewUsers.TabIndex = 8;
            // 
            // DeleteUser
            // 
            this.DeleteUser.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DeleteUser.Location = new System.Drawing.Point(362, 336);
            this.DeleteUser.Name = "DeleteUser";
            this.DeleteUser.Size = new System.Drawing.Size(151, 47);
            this.DeleteUser.TabIndex = 7;
            this.DeleteUser.Text = "Удалить";
            this.DeleteUser.UseVisualStyleBackColor = true;
            this.DeleteUser.Click += new System.EventHandler(this.DeleteUser_Click);
            // 
            // SaveUser
            // 
            this.SaveUser.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SaveUser.Location = new System.Drawing.Point(195, 336);
            this.SaveUser.Name = "SaveUser";
            this.SaveUser.Size = new System.Drawing.Size(151, 47);
            this.SaveUser.TabIndex = 3;
            this.SaveUser.Text = "Сохранить";
            this.SaveUser.UseVisualStyleBackColor = true;
            this.SaveUser.Click += new System.EventHandler(this.SaveUser_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel2.Controls.Add(this.aurorizationlabel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(535, 81);
            this.panel2.TabIndex = 0;
            // 
            // aurorizationlabel
            // 
            this.aurorizationlabel.AutoSize = true;
            this.aurorizationlabel.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.aurorizationlabel.Location = new System.Drawing.Point(100, 25);
            this.aurorizationlabel.Name = "aurorizationlabel";
            this.aurorizationlabel.Size = new System.Drawing.Size(340, 36);
            this.aurorizationlabel.TabIndex = 0;
            this.aurorizationlabel.Text = "Панель администратора";
            // 
            // buttonAllTables
            // 
            this.buttonAllTables.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonAllTables.Location = new System.Drawing.Point(29, 336);
            this.buttonAllTables.Name = "buttonAllTables";
            this.buttonAllTables.Size = new System.Drawing.Size(151, 45);
            this.buttonAllTables.TabIndex = 9;
            this.buttonAllTables.Text = "Все таблицы";
            this.buttonAllTables.UseVisualStyleBackColor = true;
            this.buttonAllTables.Click += new System.EventHandler(this.buttonAllTables_Click);
            // 
            // Администратор
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 396);
            this.Controls.Add(this.panel1);
            this.Name = "Администратор";
            this.Text = "Администратор";
            this.Load += new System.EventHandler(this.Администратор_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUsers)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private DataGridView dataGridViewUsers;
        private Button DeleteUser;
        private Button SaveUser;
        private Panel panel2;
        private Label aurorizationlabel;
        private Button buttonAllTables;
    }
}
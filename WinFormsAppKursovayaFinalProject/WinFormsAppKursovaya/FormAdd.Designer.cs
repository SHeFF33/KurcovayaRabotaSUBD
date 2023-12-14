namespace WinFormsAppKursovaya
{
    partial class FormAdd
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
            this.pictureBoxPhones = new System.Windows.Forms.PictureBox();
            this.Save = new System.Windows.Forms.Button();
            this.textBoxSalling = new System.Windows.Forms.TextBox();
            this.textBoxPurchase = new System.Windows.Forms.TextBox();
            this.textBoxSpecific = new System.Windows.Forms.TextBox();
            this.textBoxModel = new System.Windows.Forms.TextBox();
            this.textBoxBrand = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPhones)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.panel1.Controls.Add(this.pictureBoxPhones);
            this.panel1.Controls.Add(this.Save);
            this.panel1.Controls.Add(this.textBoxSalling);
            this.panel1.Controls.Add(this.textBoxPurchase);
            this.panel1.Controls.Add(this.textBoxSpecific);
            this.panel1.Controls.Add(this.textBoxModel);
            this.panel1.Controls.Add(this.textBoxBrand);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(429, 570);
            this.panel1.TabIndex = 0;
            // 
            // pictureBoxPhones
            // 
            this.pictureBoxPhones.Location = new System.Drawing.Point(130, 287);
            this.pictureBoxPhones.Name = "pictureBoxPhones";
            this.pictureBoxPhones.Size = new System.Drawing.Size(182, 185);
            this.pictureBoxPhones.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPhones.TabIndex = 15;
            this.pictureBoxPhones.TabStop = false;
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(143, 511);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(150, 47);
            this.Save.TabIndex = 10;
            this.Save.Text = "Добавить";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // textBoxSalling
            // 
            this.textBoxSalling.Location = new System.Drawing.Point(113, 236);
            this.textBoxSalling.Name = "textBoxSalling";
            this.textBoxSalling.Size = new System.Drawing.Size(220, 23);
            this.textBoxSalling.TabIndex = 9;
            // 
            // textBoxPurchase
            // 
            this.textBoxPurchase.Location = new System.Drawing.Point(113, 207);
            this.textBoxPurchase.Name = "textBoxPurchase";
            this.textBoxPurchase.Size = new System.Drawing.Size(220, 23);
            this.textBoxPurchase.TabIndex = 8;
            // 
            // textBoxSpecific
            // 
            this.textBoxSpecific.Location = new System.Drawing.Point(113, 178);
            this.textBoxSpecific.Name = "textBoxSpecific";
            this.textBoxSpecific.Size = new System.Drawing.Size(220, 23);
            this.textBoxSpecific.TabIndex = 7;
            // 
            // textBoxModel
            // 
            this.textBoxModel.Location = new System.Drawing.Point(113, 149);
            this.textBoxModel.Name = "textBoxModel";
            this.textBoxModel.Size = new System.Drawing.Size(220, 23);
            this.textBoxModel.TabIndex = 6;
            // 
            // textBoxBrand
            // 
            this.textBoxBrand.Location = new System.Drawing.Point(113, 120);
            this.textBoxBrand.Name = "textBoxBrand";
            this.textBoxBrand.Size = new System.Drawing.Size(220, 23);
            this.textBoxBrand.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 236);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 15);
            this.label6.TabIndex = 4;
            this.label6.Text = "Цена продажи:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 210);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(87, 15);
            this.label5.TabIndex = 3;
            this.label5.Text = "Цена покупки:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 181);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "Характеристики:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Модель:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Производитель:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(429, 100);
            this.panel2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(54, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(308, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "Добавление новой записи";
            // 
            // FormAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 570);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FormAdd";
            this.Text = "FormAdd";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPhones)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Button Save;
        private TextBox textBoxSalling;
        private TextBox textBoxPurchase;
        private TextBox textBoxSpecific;
        private TextBox textBoxModel;
        private TextBox textBoxBrand;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Panel panel2;
        private Label label1;
        private PictureBox pictureBoxPhones;
    }
}
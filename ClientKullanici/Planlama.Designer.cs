namespace ClientKullanici
{
    partial class Planlama
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
            this.btnCreate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbİsTip = new System.Windows.Forms.ComboBox();
            this.nmrcMiktar = new System.Windows.Forms.NumericUpDown();
            this.lblError = new System.Windows.Forms.Label();
            this.lstIsEmir = new System.Windows.Forms.ListView();
            this.lstMakina = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnGetir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nmrcMiktar)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(628, 418);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(134, 32);
            this.btnCreate.TabIndex = 0;
            this.btnCreate.Text = "İş Emri Oluştur";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(584, 335);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Miktar:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(581, 281);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "İş Tipi: ";
            // 
            // cmbİsTip
            // 
            this.cmbİsTip.FormattingEnabled = true;
            this.cmbİsTip.Location = new System.Drawing.Point(641, 281);
            this.cmbİsTip.Name = "cmbİsTip";
            this.cmbİsTip.Size = new System.Drawing.Size(121, 24);
            this.cmbİsTip.TabIndex = 4;
            // 
            // nmrcMiktar
            // 
            this.nmrcMiktar.Location = new System.Drawing.Point(642, 333);
            this.nmrcMiktar.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nmrcMiktar.Name = "nmrcMiktar";
            this.nmrcMiktar.Size = new System.Drawing.Size(120, 22);
            this.nmrcMiktar.TabIndex = 5;
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(640, 379);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 17);
            this.lblError.TabIndex = 6;
            // 
            // lstIsEmir
            // 
            this.lstIsEmir.Location = new System.Drawing.Point(26, 45);
            this.lstIsEmir.Name = "lstIsEmir";
            this.lstIsEmir.Size = new System.Drawing.Size(264, 421);
            this.lstIsEmir.TabIndex = 7;
            this.lstIsEmir.UseCompatibleStateImageBehavior = false;
            // 
            // lstMakina
            // 
            this.lstMakina.Location = new System.Drawing.Point(342, 45);
            this.lstMakina.Name = "lstMakina";
            this.lstMakina.Size = new System.Drawing.Size(436, 145);
            this.lstMakina.TabIndex = 8;
            this.lstMakina.UseCompatibleStateImageBehavior = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(26, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 18);
            this.label3.TabIndex = 9;
            this.label3.Text = "İş Emirleri";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(342, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 18);
            this.label4.TabIndex = 10;
            this.label4.Text = "Makinalar";
            // 
            // btnGetir
            // 
            this.btnGetir.Location = new System.Drawing.Point(628, 475);
            this.btnGetir.Name = "btnGetir";
            this.btnGetir.Size = new System.Drawing.Size(134, 30);
            this.btnGetir.TabIndex = 11;
            this.btnGetir.Text = "VerileriAl";
            this.btnGetir.UseVisualStyleBackColor = true;
            this.btnGetir.Click += new System.EventHandler(this.btnGetir_Click);
            // 
            // Planlama
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 532);
            this.Controls.Add(this.btnGetir);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lstMakina);
            this.Controls.Add(this.lstIsEmir);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.nmrcMiktar);
            this.Controls.Add(this.cmbİsTip);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCreate);
            this.Name = "Planlama";
            this.Text = "Planlama";
            this.Load += new System.EventHandler(this.Planlama_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nmrcMiktar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbİsTip;
        private System.Windows.Forms.NumericUpDown nmrcMiktar;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.ListView lstIsEmir;
        private System.Windows.Forms.ListView lstMakina;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnGetir;
    }
}
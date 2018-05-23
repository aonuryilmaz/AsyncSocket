namespace ServerSocket
{
    partial class Form1
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
            this.lstClients = new System.Windows.Forms.ListBox();
            this.lstProgram = new System.Windows.Forms.ListBox();
            this.txtChat = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblClientStatus = new System.Windows.Forms.Label();
            this.lblConnectionCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstClients
            // 
            this.lstClients.FormattingEnabled = true;
            this.lstClients.ItemHeight = 16;
            this.lstClients.Location = new System.Drawing.Point(13, 13);
            this.lstClients.Name = "lstClients";
            this.lstClients.Size = new System.Drawing.Size(195, 244);
            this.lstClients.TabIndex = 0;
            // 
            // lstProgram
            // 
            this.lstProgram.FormattingEnabled = true;
            this.lstProgram.ItemHeight = 16;
            this.lstProgram.Location = new System.Drawing.Point(235, 13);
            this.lstProgram.Name = "lstProgram";
            this.lstProgram.Size = new System.Drawing.Size(201, 244);
            this.lstProgram.TabIndex = 1;
            // 
            // txtChat
            // 
            this.txtChat.Location = new System.Drawing.Point(13, 292);
            this.txtChat.Name = "txtChat";
            this.txtChat.Size = new System.Drawing.Size(423, 22);
            this.txtChat.TabIndex = 2;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(13, 355);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(423, 41);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "Gönder";
            this.btnSend.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(23, 401);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 17);
            this.lblStatus.TabIndex = 4;
            // 
            // lblClientStatus
            // 
            this.lblClientStatus.AutoSize = true;
            this.lblClientStatus.Location = new System.Drawing.Point(232, 401);
            this.lblClientStatus.Name = "lblClientStatus";
            this.lblClientStatus.Size = new System.Drawing.Size(0, 17);
            this.lblClientStatus.TabIndex = 5;
            // 
            // lblConnectionCount
            // 
            this.lblConnectionCount.AutoSize = true;
            this.lblConnectionCount.Location = new System.Drawing.Point(373, 403);
            this.lblConnectionCount.Name = "lblConnectionCount";
            this.lblConnectionCount.Size = new System.Drawing.Size(0, 17);
            this.lblConnectionCount.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 427);
            this.Controls.Add(this.lblConnectionCount);
            this.Controls.Add(this.lblClientStatus);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtChat);
            this.Controls.Add(this.lstProgram);
            this.Controls.Add(this.lstClients);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstClients;
        private System.Windows.Forms.ListBox lstProgram;
        private System.Windows.Forms.TextBox txtChat;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblClientStatus;
        private System.Windows.Forms.Label lblConnectionCount;
    }
}


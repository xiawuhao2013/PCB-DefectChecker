namespace DefectChecker.View
{
    partial class ErrorMessage
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
            this.message = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // message
            // 
            this.message.Dock = System.Windows.Forms.DockStyle.Fill;
            this.message.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.message.Location = new System.Drawing.Point(0, 0);
            this.message.Multiline = true;
            this.message.Name = "message";
            this.message.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.message.Size = new System.Drawing.Size(800, 450);
            this.message.TabIndex = 1;
            this.message.Text = "hhhh";
            // 
            // ErrorMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.message);
            this.Name = "ErrorMessage";
            this.Text = "ErrorMessage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox message;
    }
}
namespace DefectChecker.View
{
    partial class ConfigView
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxDataDir = new System.Windows.Forms.TextBox();
            this.buttonSelectDataDir = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxModelDir = new System.Windows.Forms.TextBox();
            this.buttonSelectModelDir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据目录";
            // 
            // textBoxDataDir
            // 
            this.textBoxDataDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDataDir.Location = new System.Drawing.Point(78, 49);
            this.textBoxDataDir.Name = "textBoxDataDir";
            this.textBoxDataDir.ReadOnly = true;
            this.textBoxDataDir.Size = new System.Drawing.Size(360, 21);
            this.textBoxDataDir.TabIndex = 1;
            // 
            // buttonSelectDataDir
            // 
            this.buttonSelectDataDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelectDataDir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonSelectDataDir.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSelectDataDir.Location = new System.Drawing.Point(444, 49);
            this.buttonSelectDataDir.Name = "buttonSelectDataDir";
            this.buttonSelectDataDir.Size = new System.Drawing.Size(33, 23);
            this.buttonSelectDataDir.TabIndex = 2;
            this.buttonSelectDataDir.Text = "...";
            this.buttonSelectDataDir.UseVisualStyleBackColor = true;
            this.buttonSelectDataDir.Click += new System.EventHandler(this.buttonSelectDataDir_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "模板目录";
            // 
            // textBoxModelDir
            // 
            this.textBoxModelDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxModelDir.Location = new System.Drawing.Point(76, 86);
            this.textBoxModelDir.Name = "textBoxModelDir";
            this.textBoxModelDir.ReadOnly = true;
            this.textBoxModelDir.Size = new System.Drawing.Size(360, 21);
            this.textBoxModelDir.TabIndex = 1;
            // 
            // buttonSelectModelDir
            // 
            this.buttonSelectModelDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelectModelDir.AutoSize = true;
            this.buttonSelectModelDir.Location = new System.Drawing.Point(444, 84);
            this.buttonSelectModelDir.Name = "buttonSelectModelDir";
            this.buttonSelectModelDir.Size = new System.Drawing.Size(33, 23);
            this.buttonSelectModelDir.TabIndex = 2;
            this.buttonSelectModelDir.Text = "...";
            this.buttonSelectModelDir.UseVisualStyleBackColor = true;
            this.buttonSelectModelDir.Click += new System.EventHandler(this.buttonSelectModelDir_Click);
            // 
            // ConfigView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonSelectModelDir);
            this.Controls.Add(this.buttonSelectDataDir);
            this.Controls.Add(this.textBoxModelDir);
            this.Controls.Add(this.textBoxDataDir);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ConfigView";
            this.Size = new System.Drawing.Size(549, 320);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDataDir;
        private System.Windows.Forms.Button buttonSelectDataDir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxModelDir;
        private System.Windows.Forms.Button buttonSelectModelDir;
    }
}

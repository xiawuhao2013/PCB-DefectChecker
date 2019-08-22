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
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonReload = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonSelectSaveFile = new System.Windows.Forms.Button();
            this.textBoxDataBaseDir = new System.Windows.Forms.TextBox();
            this.checkBoxIsJump = new System.Windows.Forms.CheckBox();
            this.upDownDilationNum = new System.Windows.Forms.NumericUpDown();
            this.upDownWindowNum = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxDataBaseName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.upDownDilationNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownWindowNum)).BeginInit();
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
            this.label2.Location = new System.Drawing.Point(17, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "模板目录";
            // 
            // textBoxModelDir
            // 
            this.textBoxModelDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxModelDir.Location = new System.Drawing.Point(78, 90);
            this.textBoxModelDir.Name = "textBoxModelDir";
            this.textBoxModelDir.ReadOnly = true;
            this.textBoxModelDir.Size = new System.Drawing.Size(360, 21);
            this.textBoxModelDir.TabIndex = 1;
            // 
            // buttonSelectModelDir
            // 
            this.buttonSelectModelDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelectModelDir.AutoSize = true;
            this.buttonSelectModelDir.Location = new System.Drawing.Point(444, 90);
            this.buttonSelectModelDir.Name = "buttonSelectModelDir";
            this.buttonSelectModelDir.Size = new System.Drawing.Size(33, 23);
            this.buttonSelectModelDir.TabIndex = 2;
            this.buttonSelectModelDir.Text = "...";
            this.buttonSelectModelDir.UseVisualStyleBackColor = true;
            this.buttonSelectModelDir.Click += new System.EventHandler(this.buttonSelectModelDir_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(444, 277);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "保存";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonReload
            // 
            this.buttonReload.Location = new System.Drawing.Point(344, 277);
            this.buttonReload.Name = "buttonReload";
            this.buttonReload.Size = new System.Drawing.Size(75, 23);
            this.buttonReload.TabIndex = 4;
            this.buttonReload.Text = "重置";
            this.buttonReload.UseVisualStyleBackColor = true;
            this.buttonReload.Click += new System.EventHandler(this.buttonReload_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(300, 188);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "膨胀系数";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(300, 227);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "窗口数量";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "保存路径";
            // 
            // buttonSelectSaveFile
            // 
            this.buttonSelectSaveFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelectSaveFile.AutoSize = true;
            this.buttonSelectSaveFile.Location = new System.Drawing.Point(444, 135);
            this.buttonSelectSaveFile.Name = "buttonSelectSaveFile";
            this.buttonSelectSaveFile.Size = new System.Drawing.Size(33, 23);
            this.buttonSelectSaveFile.TabIndex = 2;
            this.buttonSelectSaveFile.Text = "...";
            this.buttonSelectSaveFile.UseVisualStyleBackColor = true;
            this.buttonSelectSaveFile.Click += new System.EventHandler(this.buttonSelectSaveFile_Click);
            // 
            // textBoxDataBaseDir
            // 
            this.textBoxDataBaseDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDataBaseDir.Location = new System.Drawing.Point(78, 137);
            this.textBoxDataBaseDir.Name = "textBoxDataBaseDir";
            this.textBoxDataBaseDir.ReadOnly = true;
            this.textBoxDataBaseDir.Size = new System.Drawing.Size(360, 21);
            this.textBoxDataBaseDir.TabIndex = 1;
            // 
            // checkBoxIsJump
            // 
            this.checkBoxIsJump.AutoSize = true;
            this.checkBoxIsJump.Location = new System.Drawing.Point(74, 230);
            this.checkBoxIsJump.Name = "checkBoxIsJump";
            this.checkBoxIsJump.Size = new System.Drawing.Size(132, 16);
            this.checkBoxIsJump.TabIndex = 7;
            this.checkBoxIsJump.Text = "是否跳过已标注数据";
            this.checkBoxIsJump.UseVisualStyleBackColor = true;
            // 
            // upDownDilationNum
            // 
            this.upDownDilationNum.Location = new System.Drawing.Point(357, 182);
            this.upDownDilationNum.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.upDownDilationNum.Name = "upDownDilationNum";
            this.upDownDilationNum.Size = new System.Drawing.Size(120, 21);
            this.upDownDilationNum.TabIndex = 9;
            this.upDownDilationNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // upDownWindowNum
            // 
            this.upDownWindowNum.Location = new System.Drawing.Point(357, 225);
            this.upDownWindowNum.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.upDownWindowNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.upDownWindowNum.Name = "upDownWindowNum";
            this.upDownWindowNum.Size = new System.Drawing.Size(120, 21);
            this.upDownWindowNum.TabIndex = 10;
            this.upDownWindowNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 188);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "数据库名";
            // 
            // textBoxDataBaseName
            // 
            this.textBoxDataBaseName.Location = new System.Drawing.Point(78, 185);
            this.textBoxDataBaseName.Name = "textBoxDataBaseName";
            this.textBoxDataBaseName.Size = new System.Drawing.Size(100, 21);
            this.textBoxDataBaseName.TabIndex = 11;
            // 
            // ConfigView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxDataBaseName);
            this.Controls.Add(this.upDownWindowNum);
            this.Controls.Add(this.upDownDilationNum);
            this.Controls.Add(this.checkBoxIsJump);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonReload);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonSelectSaveFile);
            this.Controls.Add(this.buttonSelectModelDir);
            this.Controls.Add(this.buttonSelectDataDir);
            this.Controls.Add(this.textBoxDataBaseDir);
            this.Controls.Add(this.textBoxModelDir);
            this.Controls.Add(this.textBoxDataDir);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ConfigView";
            this.Size = new System.Drawing.Size(549, 320);
            ((System.ComponentModel.ISupportInitialize)(this.upDownDilationNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownWindowNum)).EndInit();
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
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonReload;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonSelectSaveFile;
        private System.Windows.Forms.TextBox textBoxDataBaseDir;
        private System.Windows.Forms.CheckBox checkBoxIsJump;
        private System.Windows.Forms.NumericUpDown upDownDilationNum;
        private System.Windows.Forms.NumericUpDown upDownWindowNum;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxDataBaseName;
    }
}

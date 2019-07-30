namespace DefectChecker.View
{
    partial class MainForm
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panelOfDisplayView = new System.Windows.Forms.Panel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panelConfigView = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.ItemSize = new System.Drawing.Size(100, 30);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1284, 761);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Tag = "";
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(34, 4);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1246, 753);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "工作台";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panelOfDisplayView);
            this.tabPage2.Location = new System.Drawing.Point(34, 4);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(1246, 753);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "图像";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panelOfDisplayView
            // 
            this.panelOfDisplayView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOfDisplayView.Location = new System.Drawing.Point(0, 0);
            this.panelOfDisplayView.Margin = new System.Windows.Forms.Padding(0);
            this.panelOfDisplayView.Name = "panelOfDisplayView";
            this.panelOfDisplayView.Size = new System.Drawing.Size(1246, 753);
            this.panelOfDisplayView.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.panelConfigView);
            this.tabPage3.Location = new System.Drawing.Point(34, 4);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1246, 753);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "参数设置";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // panelConfigView
            // 
            this.panelConfigView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelConfigView.Location = new System.Drawing.Point(0, 0);
            this.panelConfigView.Margin = new System.Windows.Forms.Padding(0);
            this.panelConfigView.Name = "panelConfigView";
            this.panelConfigView.Size = new System.Drawing.Size(1246, 753);
            this.panelConfigView.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 761);
            this.Controls.Add(this.tabControl1);
            this.MinimumSize = new System.Drawing.Size(1300, 800);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panelConfigView;
        private System.Windows.Forms.Panel panelOfDisplayView;
    }
}


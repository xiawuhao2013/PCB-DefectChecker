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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.TimeTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panelLogView = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.labelCpuUsage = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelMemoryUsage = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.logButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.menuButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.显示运行日志ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示运行情况ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonMin = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonResize = new System.Windows.Forms.Button();
            this.panelConfigView = new System.Windows.Forms.Panel();
            this.panelDisplayView = new System.Windows.Forms.Panel();
            this.panelDataBaseView = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TimeTimer
            // 
            this.TimeTimer.Enabled = true;
            this.TimeTimer.Interval = 1000;
            this.TimeTimer.Tick += new System.EventHandler(this.TimeTimer_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.panelLogView);
            this.panel1.Controls.Add(this.statusStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 35);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1018, 730);
            this.panel1.TabIndex = 23;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(815, 701);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(807, 671);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "工作台";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panelDisplayView);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(807, 671);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "图像";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.panelConfigView);
            this.tabPage3.Location = new System.Drawing.Point(4, 26);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(807, 671);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "参数设置";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.panelDataBaseView);
            this.tabPage4.Location = new System.Drawing.Point(4, 26);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(807, 671);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "数据整理";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(815, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 701);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // panelLogView
            // 
            this.panelLogView.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelLogView.Location = new System.Drawing.Point(818, 0);
            this.panelLogView.Name = "panelLogView";
            this.panelLogView.Size = new System.Drawing.Size(200, 701);
            this.panelLogView.TabIndex = 2;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelCpuUsage,
            this.labelMemoryUsage,
            this.toolStripStatusLabel1,
            this.labelTime,
            this.logButton,
            this.menuButton});
            this.statusStrip1.Location = new System.Drawing.Point(0, 701);
            this.statusStrip1.Margin = new System.Windows.Forms.Padding(3);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1018, 29);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // labelCpuUsage
            // 
            this.labelCpuUsage.AutoSize = false;
            this.labelCpuUsage.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.labelCpuUsage.ForeColor = System.Drawing.Color.Blue;
            this.labelCpuUsage.Name = "labelCpuUsage";
            this.labelCpuUsage.Size = new System.Drawing.Size(93, 24);
            this.labelCpuUsage.Text = "CPU: 10%";
            // 
            // labelMemoryUsage
            // 
            this.labelMemoryUsage.AutoSize = false;
            this.labelMemoryUsage.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.labelMemoryUsage.ForeColor = System.Drawing.Color.Blue;
            this.labelMemoryUsage.Name = "labelMemoryUsage";
            this.labelMemoryUsage.Size = new System.Drawing.Size(124, 24);
            this.labelMemoryUsage.Text = "Memory: 10%";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(551, 24);
            this.toolStripStatusLabel1.Spring = true;
            // 
            // labelTime
            // 
            this.labelTime.AutoSize = false;
            this.labelTime.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F);
            this.labelTime.ForeColor = System.Drawing.Color.Blue;
            this.labelTime.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(163, 27);
            this.labelTime.Text = "2017/01/20 09:00:00";
            // 
            // logButton
            // 
            this.logButton.AutoSize = false;
            this.logButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.logButton.Image = ((System.Drawing.Image)(resources.GetObject("logButton.Image")));
            this.logButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.logButton.Name = "logButton";
            this.logButton.ShowDropDownArrow = false;
            this.logButton.Size = new System.Drawing.Size(36, 27);
            this.logButton.Text = "日志";
            this.logButton.Click += new System.EventHandler(this.logButton_Click);
            // 
            // menuButton
            // 
            this.menuButton.AutoSize = false;
            this.menuButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.menuButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.menuButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.显示运行日志ToolStripMenuItem,
            this.显示运行情况ToolStripMenuItem});
            this.menuButton.Image = ((System.Drawing.Image)(resources.GetObject("menuButton.Image")));
            this.menuButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.menuButton.Name = "menuButton";
            this.menuButton.ShowDropDownArrow = false;
            this.menuButton.Size = new System.Drawing.Size(36, 27);
            this.menuButton.Text = "菜单";
            this.menuButton.ToolTipText = "菜单";
            // 
            // 显示运行日志ToolStripMenuItem
            // 
            this.显示运行日志ToolStripMenuItem.Name = "显示运行日志ToolStripMenuItem";
            this.显示运行日志ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.显示运行日志ToolStripMenuItem.Text = "显示运行日志";
            // 
            // 显示运行情况ToolStripMenuItem
            // 
            this.显示运行情况ToolStripMenuItem.Name = "显示运行情况ToolStripMenuItem";
            this.显示运行情况ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.显示运行情况ToolStripMenuItem.Text = "显示运行情况";
            // 
            // buttonMin
            // 
            this.buttonMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMin.BackColor = System.Drawing.Color.Transparent;
            this.buttonMin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonMin.BackgroundImage")));
            this.buttonMin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonMin.FlatAppearance.BorderSize = 0;
            this.buttonMin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMin.Location = new System.Drawing.Point(889, 0);
            this.buttonMin.Name = "buttonMin";
            this.buttonMin.Size = new System.Drawing.Size(45, 32);
            this.buttonMin.TabIndex = 1;
            this.buttonMin.UseVisualStyleBackColor = false;
            this.buttonMin.Click += new System.EventHandler(this.buttonMin_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.BackColor = System.Drawing.Color.Transparent;
            this.buttonClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonClose.BackgroundImage")));
            this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonClose.FlatAppearance.BorderSize = 0;
            this.buttonClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Location = new System.Drawing.Point(979, 0);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Padding = new System.Windows.Forms.Padding(3);
            this.buttonClose.Size = new System.Drawing.Size(45, 32);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonResize
            // 
            this.buttonResize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonResize.BackColor = System.Drawing.Color.Transparent;
            this.buttonResize.BackgroundImage = global::DefectChecker.Properties.Resources.restore;
            this.buttonResize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonResize.FlatAppearance.BorderSize = 0;
            this.buttonResize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.buttonResize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonResize.Location = new System.Drawing.Point(934, 0);
            this.buttonResize.Name = "buttonResize";
            this.buttonResize.Size = new System.Drawing.Size(45, 32);
            this.buttonResize.TabIndex = 1;
            this.buttonResize.UseVisualStyleBackColor = false;
            this.buttonResize.Click += new System.EventHandler(this.buttonResize_Click);
            // 
            // panelConfigView
            // 
            this.panelConfigView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelConfigView.Location = new System.Drawing.Point(3, 3);
            this.panelConfigView.Name = "panelConfigView";
            this.panelConfigView.Size = new System.Drawing.Size(801, 665);
            this.panelConfigView.TabIndex = 0;
            // 
            // panelDisplayView
            // 
            this.panelDisplayView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDisplayView.Location = new System.Drawing.Point(3, 3);
            this.panelDisplayView.Name = "panelDisplayView";
            this.panelDisplayView.Size = new System.Drawing.Size(801, 665);
            this.panelDisplayView.TabIndex = 0;
            // 
            // panelDataBaseView
            // 
            this.panelDataBaseView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDataBaseView.Location = new System.Drawing.Point(3, 3);
            this.panelDataBaseView.Name = "panelDataBaseView";
            this.panelDataBaseView.Size = new System.Drawing.Size(801, 665);
            this.panelDataBaseView.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.ControlBox = false;
            this.Controls.Add(this.buttonMin);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonResize);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(3, 35, 3, 3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer TimeTimer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonMin;
        private System.Windows.Forms.Button buttonResize;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripDropDownButton menuButton;
        private System.Windows.Forms.ToolStripStatusLabel labelTime;
        private System.Windows.Forms.ToolStripStatusLabel labelCpuUsage;
        private System.Windows.Forms.ToolStripStatusLabel labelMemoryUsage;
        private System.Windows.Forms.ToolStripMenuItem 显示运行日志ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 显示运行情况ToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton logButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panelLogView;
        private System.Windows.Forms.Panel panelDisplayView;
        private System.Windows.Forms.Panel panelConfigView;
        private System.Windows.Forms.Panel panelDataBaseView;
    }
}


namespace DefectChecker.View
{
    partial class DisplayView
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemOfchangeDisplayNum = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemOfAAA = new System.Windows.Forms.ToolStripMenuItem();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.aqDisplay1 = new AqVision.Controls.AqDisplay();
            this.aqDisplay2 = new AqVision.Controls.AqDisplay();
            this.panelOfInfo = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.menuStrip1.Size = new System.Drawing.Size(1248, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemOfchangeDisplayNum,
            this.ToolStripMenuItemOfAAA});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // ToolStripMenuItemOfchangeDisplayNum
            // 
            this.ToolStripMenuItemOfchangeDisplayNum.Name = "ToolStripMenuItemOfchangeDisplayNum";
            this.ToolStripMenuItemOfchangeDisplayNum.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItemOfchangeDisplayNum.Text = "设置显示数量";
            this.ToolStripMenuItemOfchangeDisplayNum.Click += new System.EventHandler(this.ToolStripMenuItemOfchangeDisplayNum_Click);
            // 
            // ToolStripMenuItemOfAAA
            // 
            this.ToolStripMenuItemOfAAA.Name = "ToolStripMenuItemOfAAA";
            this.ToolStripMenuItemOfAAA.Size = new System.Drawing.Size(148, 22);
            this.ToolStripMenuItemOfAAA.Text = "显示侧边栏";
            this.ToolStripMenuItemOfAAA.Click += new System.EventHandler(this.ToolStripMenuItemOfAAA_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panelOfInfo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1248, 676);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.splitContainer1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1148, 676);
            this.panel2.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1148, 676);
            this.splitContainer1.SplitterDistance = 476;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.aqDisplay2);
            this.splitContainer2.Size = new System.Drawing.Size(476, 676);
            this.splitContainer2.SplitterDistance = 276;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.aqDisplay1);
            this.splitContainer3.Size = new System.Drawing.Size(476, 276);
            this.splitContainer3.SplitterDistance = 180;
            this.splitContainer3.TabIndex = 0;
            // 
            // aqDisplay1
            // 
            this.aqDisplay1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.aqDisplay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aqDisplay1.GroupName = "";
            this.aqDisplay1.Image = null;
            this.aqDisplay1.IsAddDynamicPoint = false;
            this.aqDisplay1.IsBeginAddImageMask = false;
            this.aqDisplay1.IsBeginDrawDynamicPolygon = false;
            this.aqDisplay1.IsInteractiveFlag = true;
            this.aqDisplay1.IsSaveResultImage = false;
            this.aqDisplay1.IsScrollBar = true;
            this.aqDisplay1.IsShowCenterLine = false;
            this.aqDisplay1.IsShowStatusBar = false;
            this.aqDisplay1.IsTransformRGB = false;
            this.aqDisplay1.IsUsedEraser = false;
            this.aqDisplay1.Location = new System.Drawing.Point(0, 0);
            this.aqDisplay1.Margin = new System.Windows.Forms.Padding(0);
            this.aqDisplay1.Name = "aqDisplay1";
            this.aqDisplay1.OriginMaskImage = null;
            this.aqDisplay1.Radius = 1F;
            this.aqDisplay1.Size = new System.Drawing.Size(292, 276);
            this.aqDisplay1.TabIndex = 0;
            // 
            // aqDisplay2
            // 
            this.aqDisplay2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.aqDisplay2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aqDisplay2.GroupName = "";
            this.aqDisplay2.Image = null;
            this.aqDisplay2.IsAddDynamicPoint = false;
            this.aqDisplay2.IsBeginAddImageMask = false;
            this.aqDisplay2.IsBeginDrawDynamicPolygon = false;
            this.aqDisplay2.IsInteractiveFlag = true;
            this.aqDisplay2.IsSaveResultImage = false;
            this.aqDisplay2.IsScrollBar = true;
            this.aqDisplay2.IsShowCenterLine = false;
            this.aqDisplay2.IsShowStatusBar = false;
            this.aqDisplay2.IsTransformRGB = false;
            this.aqDisplay2.IsUsedEraser = false;
            this.aqDisplay2.Location = new System.Drawing.Point(0, 0);
            this.aqDisplay2.Margin = new System.Windows.Forms.Padding(0);
            this.aqDisplay2.Name = "aqDisplay2";
            this.aqDisplay2.OriginMaskImage = null;
            this.aqDisplay2.Radius = 1F;
            this.aqDisplay2.Size = new System.Drawing.Size(476, 396);
            this.aqDisplay2.TabIndex = 0;
            // 
            // panelOfInfo
            // 
            this.panelOfInfo.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelOfInfo.Location = new System.Drawing.Point(1148, 0);
            this.panelOfInfo.Margin = new System.Windows.Forms.Padding(0);
            this.panelOfInfo.Name = "panelOfInfo";
            this.panelOfInfo.Size = new System.Drawing.Size(100, 676);
            this.panelOfInfo.TabIndex = 0;
            // 
            // DisplayView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "DisplayView";
            this.Size = new System.Drawing.Size(1248, 700);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemOfchangeDisplayNum;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemOfAAA;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelOfInfo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private AqVision.Controls.AqDisplay aqDisplay1;
        private AqVision.Controls.AqDisplay aqDisplay2;
    }
}

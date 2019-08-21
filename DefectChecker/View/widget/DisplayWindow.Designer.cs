namespace DefectChecker.View.widget
{
    partial class DisplayWindow
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.aqDisplayOfCheck = new AqVision.Controls.AqDisplay();
            this.labelOfCheckInfo = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.labelOfCheckTitle = new System.Windows.Forms.Label();
            this.aqDisplayOfModel = new AqVision.Controls.AqDisplay();
            this.panel5 = new System.Windows.Forms.Panel();
            this.labelOfModelTitle = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.labelOfCheckInfo);
            this.panel3.Controls.Add(this.splitContainer1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(600, 492);
            this.panel3.TabIndex = 2;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.aqDisplayOfCheck);
            this.splitContainer1.Panel1.Controls.Add(this.panel4);
            this.splitContainer1.Panel1MinSize = 0;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.aqDisplayOfModel);
            this.splitContainer1.Panel2.Controls.Add(this.panel5);
            this.splitContainer1.Panel2MinSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(600, 492);
            this.splitContainer1.SplitterDistance = 300;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 0;
            // 
            // aqDisplayOfCheck
            // 
            this.aqDisplayOfCheck.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.aqDisplayOfCheck.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aqDisplayOfCheck.GroupName = "";
            this.aqDisplayOfCheck.Image = null;
            this.aqDisplayOfCheck.IsAddDynamicPoint = false;
            this.aqDisplayOfCheck.IsBeginAddImageMask = false;
            this.aqDisplayOfCheck.IsBeginDrawDynamicPolygon = false;
            this.aqDisplayOfCheck.IsInteractiveFlag = true;
            this.aqDisplayOfCheck.IsSaveResultImage = false;
            this.aqDisplayOfCheck.IsScrollBar = true;
            this.aqDisplayOfCheck.IsShowCenterLine = false;
            this.aqDisplayOfCheck.IsShowStatusBar = false;
            this.aqDisplayOfCheck.IsTransformRGB = false;
            this.aqDisplayOfCheck.IsUsedEraser = false;
            this.aqDisplayOfCheck.Location = new System.Drawing.Point(0, 36);
            this.aqDisplayOfCheck.Margin = new System.Windows.Forms.Padding(2);
            this.aqDisplayOfCheck.Name = "aqDisplayOfCheck";
            this.aqDisplayOfCheck.OriginMaskImage = null;
            this.aqDisplayOfCheck.Radius = 1F;
            this.aqDisplayOfCheck.Size = new System.Drawing.Size(300, 456);
            this.aqDisplayOfCheck.TabIndex = 12;
            // 
            // labelOfCheckInfo
            // 
            this.labelOfCheckInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelOfCheckInfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelOfCheckInfo.Location = new System.Drawing.Point(0, 456);
            this.labelOfCheckInfo.Name = "labelOfCheckInfo";
            this.labelOfCheckInfo.Size = new System.Drawing.Size(600, 36);
            this.labelOfCheckInfo.TabIndex = 1;
            this.labelOfCheckInfo.Text = "label1";
            this.labelOfCheckInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.Control;
            this.panel4.Controls.Add(this.labelOfCheckTitle);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(300, 36);
            this.panel4.TabIndex = 10;
            // 
            // labelOfCheckTitle
            // 
            this.labelOfCheckTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelOfCheckTitle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelOfCheckTitle.Location = new System.Drawing.Point(0, 0);
            this.labelOfCheckTitle.Name = "labelOfCheckTitle";
            this.labelOfCheckTitle.Size = new System.Drawing.Size(300, 36);
            this.labelOfCheckTitle.TabIndex = 0;
            this.labelOfCheckTitle.Text = "结果图";
            this.labelOfCheckTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // aqDisplayOfModel
            // 
            this.aqDisplayOfModel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.aqDisplayOfModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aqDisplayOfModel.GroupName = "";
            this.aqDisplayOfModel.Image = null;
            this.aqDisplayOfModel.IsAddDynamicPoint = false;
            this.aqDisplayOfModel.IsBeginAddImageMask = false;
            this.aqDisplayOfModel.IsBeginDrawDynamicPolygon = false;
            this.aqDisplayOfModel.IsInteractiveFlag = true;
            this.aqDisplayOfModel.IsSaveResultImage = false;
            this.aqDisplayOfModel.IsScrollBar = true;
            this.aqDisplayOfModel.IsShowCenterLine = false;
            this.aqDisplayOfModel.IsShowStatusBar = false;
            this.aqDisplayOfModel.IsTransformRGB = false;
            this.aqDisplayOfModel.IsUsedEraser = false;
            this.aqDisplayOfModel.Location = new System.Drawing.Point(0, 36);
            this.aqDisplayOfModel.Margin = new System.Windows.Forms.Padding(2);
            this.aqDisplayOfModel.Name = "aqDisplayOfModel";
            this.aqDisplayOfModel.OriginMaskImage = null;
            this.aqDisplayOfModel.Radius = 1F;
            this.aqDisplayOfModel.Size = new System.Drawing.Size(299, 456);
            this.aqDisplayOfModel.TabIndex = 9;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.panel5.Controls.Add(this.labelOfModelTitle);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(299, 36);
            this.panel5.TabIndex = 7;
            // 
            // labelOfModelTitle
            // 
            this.labelOfModelTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelOfModelTitle.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelOfModelTitle.Location = new System.Drawing.Point(0, 0);
            this.labelOfModelTitle.Name = "labelOfModelTitle";
            this.labelOfModelTitle.Size = new System.Drawing.Size(299, 36);
            this.labelOfModelTitle.TabIndex = 1;
            this.labelOfModelTitle.Text = "OK图";
            this.labelOfModelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DisplayWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Name = "DisplayWindow";
            this.Size = new System.Drawing.Size(600, 492);
            this.panel3.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private AqVision.Controls.AqDisplay aqDisplayOfCheck;
        private System.Windows.Forms.Panel panel4;
        private AqVision.Controls.AqDisplay aqDisplayOfModel;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label labelOfCheckTitle;
        private System.Windows.Forms.Label labelOfModelTitle;
        private System.Windows.Forms.Label labelOfCheckInfo;
    }
}

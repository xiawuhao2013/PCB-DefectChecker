namespace DefectChecker.View
{
    partial class DispalyViewOfCells
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
            this.tableLayoutPanelOfDefectCells = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // tableLayoutPanelOfDefectCells
            // 
            this.tableLayoutPanelOfDefectCells.ColumnCount = 2;
            this.tableLayoutPanelOfDefectCells.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelOfDefectCells.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelOfDefectCells.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelOfDefectCells.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelOfDefectCells.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelOfDefectCells.Name = "tableLayoutPanelOfDefectCells";
            this.tableLayoutPanelOfDefectCells.RowCount = 2;
            this.tableLayoutPanelOfDefectCells.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelOfDefectCells.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelOfDefectCells.Size = new System.Drawing.Size(468, 293);
            this.tableLayoutPanelOfDefectCells.TabIndex = 0;
            // 
            // DispalyViewOfCells
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanelOfDefectCells);
            this.Name = "DispalyViewOfCells";
            this.Size = new System.Drawing.Size(468, 293);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelOfDefectCells;
    }
}

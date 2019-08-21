using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aqrose.Framework.Utility.MessageManager;
using Aqrose.Framework.Utility.Tools;

namespace DefectChecker.View
{
    public partial class MainForm : Form
    {
        private MessagesView _uiMessagesView;
        private ConfigView _uiConfigView = new ConfigView();
        private DisplayView _uiDisplayView = new DisplayView();
        private DataBaseView _uiDataBaseView = new DataBaseView();

        private bool _isLogDisplay = true;

        private delegate void AcquisitionFinishedDelegate(object objUserparam);
        private delegate void CaptureFinishedDelegate(object objUserparam, Bitmap bitmap);

        public MainForm()
        {
            InitializeComponent();

            WindowState = FormWindowState.Maximized;
            this.buttonResize.BackgroundImage = global::DefectChecker.Properties.Resources.restore;

            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);

            InitDisplay();
        }
        private const int cGrip = 5;      // Grip size
        private const int cCaption = 32;   // Caption bar height;

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rc = new Rectangle(0, 0, this.ClientSize.Width, cCaption);
            e.Graphics.FillRectangle(Brushes.LightBlue, rc);

            Image image = global::DefectChecker.Properties.Resources.AQlogoBlueSmall;
            Point point = new Point(13, 4);
            e.Graphics.DrawImage(image, point);

            var font = new Font("微软雅黑", 12F);
            e.Graphics.DrawString("DefectChecker", font, Brushes.Black, 68, 8);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {
                Point pos = new Point(m.LParam.ToInt32());
                pos = this.PointToClient(pos);

                if (pos.X <= cGrip && pos.Y <= cGrip)
                {
                    m.Result = (IntPtr)13;
                    return;
                }
                if (pos.X >= this.ClientSize.Width - cGrip && pos.Y <= cGrip)
                {
                    m.Result = (IntPtr)14;
                    return;
                }
                if (pos.X <= cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)16;
                    return;
                }
                if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)17;
                    return;
                }
                if (pos.X <= cGrip)
                {
                    m.Result = (IntPtr)10;
                    return;
                }
                if (pos.X >= this.ClientSize.Width - cGrip)
                {
                    m.Result = (IntPtr)11;
                    return;
                }
                if (pos.Y <= cGrip)
                {
                    m.Result = (IntPtr)12;
                    return;
                }
                if (pos.Y >= this.ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)15;
                    return;
                }

                if (pos.Y < cCaption)
                {
                    m.Result = (IntPtr)2;  // HTCAPTION
                    return;
                }
            }
            base.WndProc(ref m);
        }

        private void InitDisplay()
        {
            _uiMessagesView = new MessagesView();
            _uiMessagesView.Dock = DockStyle.Fill;
            this.panelLogView.Controls.Add(_uiMessagesView);
            this.panelLogView.Visible = false;
            this._isLogDisplay = false;

            _uiConfigView.Dock = DockStyle.Fill;
            this.panelConfigView.Controls.Add(_uiConfigView);

            _uiDataBaseView.Dock = DockStyle.Fill;
            this.panelDataBaseView.Controls.Add(_uiDataBaseView);

            _uiDisplayView.Dock = DockStyle.Fill;
            this.panelDisplayView.Controls.Add(_uiDisplayView);
            this.tabControl1.SelectedIndex = 1;

            return;
        }

        #region 窗体事件
        // MainForm事件
        
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
        }

        private void TimeTimer_Tick(object sender, EventArgs e)
        {
            labelTime.Text = DateTime.Now.ToString("G");
        }
        #endregion
        
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonResize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
                this.buttonResize.BackgroundImage = global::DefectChecker.Properties.Resources.max;
            }
            else if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
                this.buttonResize.BackgroundImage = global::DefectChecker.Properties.Resources.restore;
            }
        }

        private void buttonMin_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Maximized)
            {
                this.buttonResize.BackgroundImage = global::DefectChecker.Properties.Resources.restore;
            }
            else if (WindowState == FormWindowState.Normal)
            {
                this.buttonResize.BackgroundImage = global::DefectChecker.Properties.Resources.max;
            }
        }

        private void logButton_Click(object sender, EventArgs e)
        {
            if (_isLogDisplay)
            {
                this.panelLogView.Visible = false;
                _isLogDisplay = false;
            }
            else
            {
                this.panelLogView.Visible = true;
                _isLogDisplay = true;
            }
        }
    }
}

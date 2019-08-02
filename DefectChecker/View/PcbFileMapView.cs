using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DefectChecker.View
{
    public partial class PcbFileMapView : UserControl
    {
        public string Product { get { return @"料 号："; } }
        public string Batch { get { return @"批量号："; } }
        public string Board { get { return @"板 号："; } }
        public string Side { get { return @"层 面："; } }
        public string Group { get { return @"当前组号："; } }
        public string Defect { get { return @"当前图像："; } }

        public Dictionary<string, string> InfoMap = new Dictionary<string, string>();
        public PcbFileMapView()
        {
            InitializeComponent();
            InitializeInfoMap();
            RefreshInfoMap();

        }

        private void InitializeInfoMap()
        {
            InfoMap.Clear();
            InfoMap[Product] = "";
            InfoMap[Batch] = "";
            InfoMap[Board] = "";
            InfoMap[Side] = "";
            InfoMap[Group] = "";
            InfoMap[Defect] = "";

            return;
        }

        public void RefreshInfoMap()
        {
            this.label1.Text = Product + InfoMap[Product];
            this.label2.Text = Batch + InfoMap[Batch];
            this.label3.Text = Board + InfoMap[Board];
            this.label4.Text = Side + InfoMap[Side];
            this.label5.Text = Group + InfoMap[Group];
            this.label6.Text = Defect + InfoMap[Defect];

            return;
        }
    }
}

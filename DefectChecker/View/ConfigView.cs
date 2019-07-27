using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aqrose.Framework.Utility.Tools;
using System.IO;
using DefectChecker.DeviceModule.MachVision;

namespace DefectChecker.View
{
    public partial class ConfigView : UserControl
    {
        private string _dataDir;
        private string _modelDir;

        public ConfigView()
        {
            InitializeComponent();
            LoadConfig();
        }

        private void LoadConfig()
        {
            XmlParameter xmlParameter = new XmlParameter();
            xmlParameter.ReadParameter(Application.StartupPath + @"\ParamFile.xml");
            _dataDir = xmlParameter.GetParamData("DataDir");
            _modelDir = xmlParameter.GetParamData("ModelDir");

            this.textBoxDataDir.Text = _dataDir;
            this.textBoxModelDir.Text = _modelDir;
        }

        private void SaveConfig()
        {
            XmlParameter xmlParameter = new XmlParameter();
            xmlParameter.Add("DataDir", _dataDir);
            xmlParameter.Add("ModelDir", _modelDir);
            xmlParameter.WriteParameter(Application.StartupPath + @"\ParamFile.xml");
        }

        private void buttonSelectDataDir_Click(object sender, EventArgs e)
        {
            _dataDir = DirctoryChoose("DataDir");
            this.textBoxDataDir.Text = _dataDir;
            SaveConfig();
        }

        private void buttonSelectModelDir_Click(object sender, EventArgs e)
        {
            _modelDir = DirctoryChoose("ModelDir");
            this.textBoxModelDir.Text = _modelDir;
            SaveConfig();
        }

        private string DirctoryChoose(string dataDir)
        {
            if (!File.Exists(dataDir))
            {
                dataDir = Application.StartupPath;
            }

            var dialog = new FolderBrowserDialog();
            dialog.Description = "请选择数据文件夹";
            dialog.SelectedPath = dataDir;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                dataDir = dialog.SelectedPath;
            }
            else
            {
                dataDir = "";
            }

            return dataDir;
        }

        // LABEL: for test.
        private void button1_Click(object sender, EventArgs e)
        {
            DeviceMachVision deviceOfMachVision = new DeviceMachVision();

            deviceOfMachVision.GetProductList(out var productList);
            deviceOfMachVision.GetBatchList(productList[0], out var batchList);
            deviceOfMachVision.GetBoardList(productList[0], batchList[0], out var boardList);
            deviceOfMachVision.GetTemplateWholeImgA(out var bitmapA);
            deviceOfMachVision.GetTemplateWholeImgB(out var bitmapB);
            return;
        }
    }
}

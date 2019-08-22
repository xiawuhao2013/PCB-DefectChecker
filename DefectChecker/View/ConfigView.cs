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
        private const string _fileProjectSetting= @"\config\ProjectSetting.xml";
        private string _dataDir;
        private string _modelDir;
        private string _dataBaseDir;
        private string _dataBaseName;
        private int _dilationPixel;
        private int _displayWindowNum;
        private bool _isJumpMarkedData;

        public ConfigView()
        {
            InitializeComponent();
            LoadConfig();
        }

        private void LoadConfig()
        {
            string configPath = Application.StartupPath + "/config/";
            if (false == Directory.Exists(configPath))
            {
                Directory.CreateDirectory(configPath);
            }

            string str;
            XmlParameter xmlParameter = new XmlParameter();
            xmlParameter.ReadParameter(Application.StartupPath + _fileProjectSetting);
            _dataDir = xmlParameter.GetParamData("DataDir");
            _modelDir = xmlParameter.GetParamData("ModelDir");
            _dataBaseDir = xmlParameter.GetParamData("DataBaseDir");
            _dataBaseName = xmlParameter.GetParamData("DataBaseName");
            str = xmlParameter.GetParamData("DilationPixel");
            int.TryParse(str, out _dilationPixel);
            str = xmlParameter.GetParamData("DisplayWindowNum");
            int.TryParse(str, out _displayWindowNum);
            str = xmlParameter.GetParamData("IsJumpMarkedData");
            bool.TryParse(str, out _isJumpMarkedData);

            this.textBoxDataDir.Text = _dataDir;
            this.textBoxModelDir.Text = _modelDir;
            this.textBoxDataBaseDir.Text = _dataBaseDir;
            this.textBoxDataBaseName.Text = _dataBaseName;
            if (_dilationPixel < this.upDownDilationNum.Minimum)
            {
                _dilationPixel = (int)this.upDownDilationNum.Minimum;
            }
            else if (_dilationPixel > this.upDownDilationNum.Maximum)
            {
                _dilationPixel = (int)this.upDownDilationNum.Maximum;
            }
            this.upDownDilationNum.Value = _dilationPixel;

            if (_displayWindowNum < this.upDownWindowNum.Minimum)
            {
                _displayWindowNum = (int)this.upDownWindowNum.Minimum;
            }
            else if (_displayWindowNum > this.upDownWindowNum.Maximum)
            {
                _displayWindowNum = (int)this.upDownWindowNum.Maximum;
            }
            this.upDownWindowNum.Value = _displayWindowNum;

            this.checkBoxIsJump.Checked = _isJumpMarkedData;
        }

        private void SaveConfig()
        {
            _dataDir = this.textBoxDataDir.Text;
            _modelDir = this.textBoxModelDir.Text;
            _dataBaseDir = this.textBoxDataBaseDir.Text;
            _dataBaseName = this.textBoxDataBaseName.Text;
            _dilationPixel = (int)this.upDownDilationNum.Value;
            _displayWindowNum = (int) this.upDownWindowNum.Value;
            _isJumpMarkedData = this.checkBoxIsJump.Checked;

            XmlParameter xmlParameter = new XmlParameter();
            xmlParameter.Add("DataDir", _dataDir);
            xmlParameter.Add("ModelDir", _modelDir);
            xmlParameter.Add("DataBaseDir", _dataBaseDir);
            xmlParameter.Add("DataBaseName", _dataBaseName);
            xmlParameter.Add("DilationPixel", _dilationPixel);
            xmlParameter.Add("DisplayWindowNum", _displayWindowNum);
            xmlParameter.Add("IsJumpMarkedData", _isJumpMarkedData);
            xmlParameter.WriteParameter(Application.StartupPath + _fileProjectSetting);
        }

        private void buttonSelectDataDir_Click(object sender, EventArgs e)
        {
            _dataDir = DirctoryChoose("DataDir");
            this.textBoxDataDir.Text = _dataDir;
        }

        private void buttonSelectModelDir_Click(object sender, EventArgs e)
        {
            _modelDir = DirctoryChoose("ModelDir");
            this.textBoxModelDir.Text = _modelDir;
        }

        private void buttonSelectSaveFile_Click(object sender, EventArgs e)
        {
            _dataBaseDir = DirctoryChoose("DataBaseDir");
            this.textBoxDataBaseDir.Text = _dataBaseDir;
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveConfig();
        }

        private void buttonReload_Click(object sender, EventArgs e)
        {
            LoadConfig();
        }
    }
}

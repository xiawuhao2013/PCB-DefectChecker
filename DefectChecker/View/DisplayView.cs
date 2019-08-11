using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DefectChecker.DataBase;
using DefectChecker.DefectDataStructure;

namespace DefectChecker.View
{
    public partial class DisplayView : UserControl
    {
        private DataBaseManager _dataBaseManager;

        public DisplayView()
        {
            InitializeComponent();
            DataBaseInit();
        }

        private void DataBaseInit()
        {
            _dataBaseManager = new DataBaseManager();
        }
    }
}

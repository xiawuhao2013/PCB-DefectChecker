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
        private MarkDataBase _dataBase;

        public DisplayView()
        {
            InitializeComponent();
            DataBaseInit();
        }

        private void DataBaseInit()
        {
            _dataBaseManager = new DataBaseManager();
            _dataBase = _dataBaseManager.DataBase;

            //comboBoxProduct.DataSource = _dataBase.ProductNameList;
            //comboBoxProduct.SelectedText = _dataBase.ProductName;
            
            var mode = DataSourceUpdateMode.OnPropertyChanged | DataSourceUpdateMode.OnValidation;
            comboBoxProduct.DataBindings.Add(new Binding("DataSource", _dataBase, "ProductNameList", true, mode));
            comboBoxProduct.DataBindings.Add(new Binding("Text", _dataBase, "ProductName", true, mode));

            comboBoxBatch.DataBindings.Add(new Binding("DataSource", _dataBase, "BatchNameList", true, mode));
            comboBoxBatch.DataBindings.Add(new Binding("Text", _dataBase, "BatchName", true, mode));

            comboBoxBoard.DataBindings.Add(new Binding("DataSource", _dataBase, "BoardNameList", true, mode));
            comboBoxBoard.DataBindings.Add(new Binding("Text", _dataBase, "BoardName", true, mode));

            comboBoxSide.DataBindings.Add(new Binding("DataSource", _dataBase, "SideNameList", true, mode));
            comboBoxSide.DataBindings.Add(new Binding("Text", _dataBase, "SideName", true, mode));

            comboBoxShot.DataBindings.Add(new Binding("DataSource", _dataBase, "ShotNameList", true, mode));
            comboBoxShot.DataBindings.Add(new Binding("Text", _dataBase, "ShotName", true, mode));

            comboBoxDefect.DataBindings.Add(new Binding("DataSource", _dataBase, "DefectNameList", true, mode));
            comboBoxDefect.DataBindings.Add(new Binding("Text", _dataBase, "DefectName", true, mode));
        }
    }
}

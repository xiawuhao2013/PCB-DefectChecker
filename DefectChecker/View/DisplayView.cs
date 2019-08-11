﻿using System;
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

            ComboBoxRefresh();
        }

        private void ComboBoxRefresh()
        {
            this.comboBoxProduct.TextChanged -= new System.EventHandler(this.comboBoxProduct_TextChanged);
            this.comboBoxBatch.TextChanged -= new System.EventHandler(this.comboBoxBatch_TextChanged);
            this.comboBoxBoard.TextChanged -= new System.EventHandler(this.comboBoxBoard_TextChanged);
            this.comboBoxSide.TextChanged -= new System.EventHandler(this.comboBoxSide_TextChanged);
            this.comboBoxShot.TextChanged -= new System.EventHandler(this.comboBoxShot_TextChanged);
            this.comboBoxDefect.TextChanged -= new System.EventHandler(this.comboBoxDefect_TextChanged);

            comboBoxProduct.DataSource = _dataBase.ProductNameList;
            comboBoxProduct.Text = _dataBase.ProductName;

            comboBoxBatch.DataSource = _dataBase.BatchNameList;
            comboBoxBatch.Text = _dataBase.BatchName;

            comboBoxBoard.DataSource = _dataBase.BoardNameList;
            comboBoxBoard.Text = _dataBase.BoardName;

            comboBoxSide.DataSource = _dataBase.SideNameList;
            comboBoxSide.Text = _dataBase.SideName;

            comboBoxShot.DataSource = _dataBase.ShotNameList;
            comboBoxShot.Text = _dataBase.ShotName;

            comboBoxDefect.DataSource = _dataBase.DefectNameList;
            comboBoxDefect.Text = _dataBase.DefectName;
            
            this.comboBoxProduct.TextChanged += new System.EventHandler(this.comboBoxProduct_TextChanged);
            this.comboBoxBatch.TextChanged += new System.EventHandler(this.comboBoxBatch_TextChanged);
            this.comboBoxBoard.TextChanged += new System.EventHandler(this.comboBoxBoard_TextChanged);
            this.comboBoxSide.TextChanged += new System.EventHandler(this.comboBoxSide_TextChanged);
            this.comboBoxShot.TextChanged += new System.EventHandler(this.comboBoxShot_TextChanged);
            this.comboBoxDefect.TextChanged += new System.EventHandler(this.comboBoxDefect_TextChanged);
        }

        private void comboBoxProduct_TextChanged(object sender, EventArgs e)
        {
            string productName = comboBoxProduct.Text;
            _dataBase.UpdateProductName(productName);
            ComboBoxRefresh();
        }

        private void comboBoxBatch_TextChanged(object sender, EventArgs e)
        {
            string batchName = comboBoxBatch.Text;
            _dataBase.UpdateBatchName(batchName);
            ComboBoxRefresh();
        }

        private void comboBoxBoard_TextChanged(object sender, EventArgs e)
        {
            string boardName = comboBoxBoard.Text;
            _dataBase.UpdateBoardName(boardName);
            ComboBoxRefresh();
        }

        private void comboBoxSide_TextChanged(object sender, EventArgs e)
        {
            string sideName = comboBoxSide.Text;
            _dataBase.UpdateSideName(sideName);
            ComboBoxRefresh();
        }

        private void comboBoxShot_TextChanged(object sender, EventArgs e)
        {
            string shotName = comboBoxShot.Text;
            _dataBase.UpdateShotName(shotName);
            ComboBoxRefresh();
        }

        private void comboBoxDefect_TextChanged(object sender, EventArgs e)
        {
            string defectName = comboBoxDefect.Text;
            _dataBase.UpdateDefectName(defectName);
            ComboBoxRefresh();
        }
    }
}

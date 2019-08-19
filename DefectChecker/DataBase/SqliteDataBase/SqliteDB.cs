using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DefectChecker.DataBase.SqliteDataBase
{
    class SqliteDB
    {
        private string _dataBaseName;
        private string _tableName;
        public SqliteDB(string dataBaseName, string tableName)
        {
            _dataBaseName = dataBaseName;
            _tableName = tableName;
            CreateDateBaseAndTable();
        }

        private void CreateDateBaseAndTable()
        {
            // string productName, string batchName, string boardName, string sideName,
            // string shotName, string defectName, EMarkDataType markType
            string sqlCmd = "CREATE TABLE IF NOT EXISTS " + _tableName + "(Time TIMESTAMP default (datetime('now', 'localtime')), Product varchar(50), Batch varchar(50), Board varchar(50), Side varchar(50), Shot varchar(50), Defect varchar(50), MarkInfo varchar(50));";
            SqliteCMD(sqlCmd);
        }

        private bool SqliteCMD(string sqlCmd)
        {
            string dbPath = "Data Source = " + Application.StartupPath + "\\" + _dataBaseName + ".db";
            SQLiteConnection connect = new SQLiteConnection(dbPath);
            connect.Open();
            
            SQLiteCommand cmdCreateTable = new SQLiteCommand(sqlCmd, connect);
            cmdCreateTable.ExecuteNonQuery();
            connect.Close();

            return true;
        }

        private bool InsertMarkDataInfo(MarkDataInfo dataInfo)
        {
            string sqlCmd = "insert into ";
            sqlCmd += _tableName;
            sqlCmd += " (Product, Batch, Board, Side, Shot, Defect, MarkInfo)";
            sqlCmd += " values(";
            sqlCmd += "'" + dataInfo.ProductName + "', ";
            sqlCmd += "'" + dataInfo.BatchName + "', ";
            sqlCmd += "'" + dataInfo.BoardName + "', ";
            sqlCmd += "'" + dataInfo.SideName + "', ";
            sqlCmd += "'" + dataInfo.ShotName + "', ";
            sqlCmd += "'" + dataInfo.DefectName + "', ";
            sqlCmd += "'" + dataInfo.MarksToString() + "'";
            sqlCmd += ");";
            return SqliteCMD(sqlCmd);
        }

        private bool UpdateMarkDataInfo(MarkDataInfo dataInfo)
        {
            string sqlCmd = "update ";
            sqlCmd += _tableName;
            sqlCmd += " set MarkInfo='" + dataInfo.MarksToString() + 
                      "', Time=datetime('now','localtime') where ";
            sqlCmd += "Product='" + dataInfo.ProductName + "' ";
            sqlCmd += "and Batch='" + dataInfo.BatchName + "' ";
            sqlCmd += "and Board='" + dataInfo.BoardName + "' ";
            sqlCmd += "and Side='" + dataInfo.SideName + "' ";
            sqlCmd += "and Shot='" + dataInfo.ShotName + "' ";
            sqlCmd += "and Defect='" + dataInfo.DefectName + "';";
            return SqliteCMD(sqlCmd);
        }

        public bool WriteMarkDataInfo(MarkDataInfo dataInfo)
        {
            MarkDataInfo markDataTemp = new MarkDataInfo(dataInfo);
            if (ReadMarkDataType(ref markDataTemp))
            {
                return UpdateMarkDataInfo(dataInfo);
            }
            else
            {
                return InsertMarkDataInfo(dataInfo);
            }
        }

        public bool ReadMarkDataType(ref MarkDataInfo dataInfo)
        {
            string sqlCmd = "select * from " + _tableName + " where ";
            sqlCmd += "Product='" + dataInfo.ProductName + "' ";
            sqlCmd += "and Batch='" + dataInfo.BatchName + "' ";
            sqlCmd += "and Board='" + dataInfo.BoardName + "' ";
            sqlCmd += "and Side='" + dataInfo.SideName + "' ";
            sqlCmd += "and Shot='" + dataInfo.ShotName + "' ";
            sqlCmd += "and Defect='" + dataInfo.DefectName + "';";

            string dbPath = "Data Source = " + Application.StartupPath + "\\" + _dataBaseName + ".db";
            SQLiteConnection connect = new SQLiteConnection(dbPath);
            connect.Open();

            SQLiteCommand cmdCreateTable = new SQLiteCommand(sqlCmd, connect);
            SQLiteDataReader reader = cmdCreateTable.ExecuteReader();
            bool isOK;
            if (reader.Read())
            {
                string marksString = (string) reader["MarkInfo"];
                dataInfo.SetMarksByString(marksString);
                isOK = true;

                // multi line is the same data, return false
                while (reader.Read())
                {
                    // if not read out the data, database may be locked.
                }
            }
            else
            {
                dataInfo.SetMarksByString("");
                isOK = false;
            }

            connect.Close();
            return isOK;
        }
    }
}

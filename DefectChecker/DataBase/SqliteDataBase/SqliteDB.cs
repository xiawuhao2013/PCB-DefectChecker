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
            string dbPath = "Data Source = " + Application.StartupPath +"\\"+ _dataBaseName + ".db";
            SQLiteConnection connect = new SQLiteConnection(dbPath);
            connect.Open();

            string sql = "CREATE TABLE IF NOT EXISTS "+_tableName+"(Name varchar(50), ID varchar(50));";
            SQLiteCommand cmdCreateTable = new SQLiteCommand(sql, connect);
            cmdCreateTable.ExecuteNonQuery();
            connect.Close();
        }
    }
}

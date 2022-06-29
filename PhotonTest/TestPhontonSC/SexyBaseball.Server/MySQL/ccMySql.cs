using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace PHPServiceServer.MySQL
{
    public class ccMySql
    {
        private object _oLock = new object();

        private static ccMySql _Instance = null;
        //public static ccMySql GetInstance()
        //{
        //    if (_Instance == null)
        //    {
        //        _Instance = new ccMySql();
        //    }

        //    return _Instance;
        //}

        //数据库连接字符串
        public string _strConn = "";
        private MySqlConnection _MySqlConnection;

        public void f_Connect(string strHost, string strDb, string strUser, string strPwd)
        {
            _strConn = string.Format("Database='{0}';Data Source='{1}';User Id='{2}';Password='{3}';charset='utf8';pooling=true", strDb, strHost, strUser, strPwd);
            _MySqlConnection = new MySqlConnection(_strConn);

            _MySqlConnection.Open();
        }

        private void TestMySQL()
        {
            if (!_MySqlConnection.Ping())
            {
                MessageBox.DEBUG("MYSQL Ping 超时");
                _MySqlConnection.Close();
                _MySqlConnection.Open();
            }
        }

        public int ExecuteNonQuery(string ppSQL)
        {
            lock (_oLock)
            {
                try
                {
                    TestMySQL();
                    MySqlCommand mysqlCommand = new MySqlCommand(ppSQL, _MySqlConnection);
                    return mysqlCommand.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    MessageBox.DEBUG("ExecuteNonQuery Error:" + ex.ToString());
                    return -2;
                }
                //finally
                //{
                //    _MySqlConnection.Close();
                //}
                return 0;
            }            
        }

        public DataTable ExecuteDataTable(string sql)
        {
            lock (_oLock)
            {
                try
                {
                    TestMySQL();
                    MySqlCommand cmd = new MySqlCommand(sql, _MySqlConnection);
                    DataSet dataset = new DataSet();//dataset放执行后的数据集合
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    adapter.Fill(dataset);
                    cmd.Dispose();
                    return dataset.Tables[0];
                }
                catch (MySqlException ex)
                {
                    MessageBox.DEBUG("ExecuteDataTable Error:" + ex.ToString());
                }
                return null;
            }
        }

        public void f_Close()
        {
            _MySqlConnection.Close();
        }


    }

}

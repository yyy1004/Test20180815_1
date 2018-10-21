using System;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Threading;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace DataService
{
    /// <summary>
    /// 20060207
    /// Add By Joker
    /// 数据操作公共层
    /// 提供一些存取数据库的接口函数，保持一个数据库链接。
    /// </summary>
    public sealed class DataAccess
    {

        /// <summary>
        /// ole的连接方式
        /// </summary>
        public string szOleConn;

        /// <summary>
        /// 构造器
        /// </summary>
        public DataAccess()
        {
            this.szOleConn = ConfigurationManager.ConnectionStrings["MysqlConnectionString"].ConnectionString;
        }

        #region 利用默认的连接串oleConString 和 数据库组件oleConn操作数据库
        /// <summary>
        /// 根据给定的连接字符串执行sql语句来返回具有数据的只读向前的Reader
        /// </summary>
        /// <param name="strSql">提取数据的sql语句</param>
        /// <param name="strCon">数据库连接字符串</param>
        /// <returns>返回含有数据的reader</returns>
        private MySqlDataReader getDataReader(string strSql)
        {
            MySqlDataReader reader = null;
            try
            {
                MySqlConnection con = new MySqlConnection(this.szOleConn);

                con.Open();

                MySqlCommand cmd = new MySqlCommand(strSql, con);

                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            }
            catch (Exception er)
            {
                Debug.WriteLine(er.ToString());
                //LogErrInfo.WriteErrInfo("数据访问层.txt",er.ToString());;	
                throw er;
            }

            return reader;
        }

        /// <summary>
        /// 执行SQL查询，返回结果的第一行第一列
        /// </summary>
        /// <param name="sql">要执行的SQL语句</param>
        /// <returns>查询结果</returns>
        public object GetValue(string sql)
        {

            MySqlConnection myConnection = new MySqlConnection(this.szOleConn);
            myConnection.Open();

            MySqlCommand myCommand = myConnection.CreateCommand();

            try
            {
                myCommand.CommandText = sql;
                object o = myCommand.ExecuteScalar();
                return o;
            }

            catch (Exception er)
            {
                Debug.WriteLine(er.ToString());
                throw er;
            }
            finally
            {
                myConnection.Close();
            }
        }



        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="strSql">需要执行的sql语句</param>
        /// <param name="strCon">ole连接字符串</param>
        /// <returns>成功就返回真，否则就返回假</returns>
        public bool ExecSql(string strSql)
        {
            bool bSucceeded = false;
            MySqlConnection myConnection = new MySqlConnection(this.szOleConn);
            myConnection.Open();

            MySqlCommand myCommand = myConnection.CreateCommand();
            MySqlTransaction myTrans;

            myTrans = myConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            myCommand.Connection = myConnection;
            myCommand.Transaction = myTrans;

            try
            {
                myCommand.CommandText = strSql;
                int ii=myCommand.ExecuteNonQuery();
                if (ii>0)//2018年8月30日修改
                {
                myTrans.Commit();
                bSucceeded = true;
                }

            }
            catch (Exception e)
            {

                Debug.WriteLine(e.ToString());
                //LogErrInfo.WriteErrInfo("数据访问层.txt",e.ToString());;	

                try
                {
                    myTrans.Rollback();
                }
                catch (MySqlException er)
                {
                    if (myTrans.Connection != null)
                    {
                        Debug.WriteLine(er.ToString());
                        //LogErrInfo.WriteErrInfo("数据访问层.txt",er.ToString());;	
                    }
                }
            }
            finally
            {
                myConnection.Close();
            }
            return bSucceeded;
        }

        /// <summary>
        /// 执行SQL语句 返回一个数据集
        /// </summary>
        /// <param name="sql">要执行的SQL语句</param>
        /// <returns></returns>	
        public DataSet GetDataset(string sql)
        {
            MySqlConnection OleConnection = new MySqlConnection(this.szOleConn);
            OleConnection.Open();

            MySqlDataAdapter OleAdapter = new MySqlDataAdapter(sql, this.szOleConn);
            DataSet myDataSet = new DataSet();

            try
            {

                OleAdapter.Fill(myDataSet);
                return myDataSet;
            }
            catch (Exception er)
            {
                throw er;
            }
            finally
            {
                OleConnection.Close();
            }
        }



        /// <summary>
        /// 执行SQL语句 返回一个数据集
        /// </summary>
        /// <param name="sql">要执行的SQL语句</param>
        /// <returns></returns>	
        public DataTable GetTable(string sql)
        {
            MySqlConnection OleConnection = new MySqlConnection(this.szOleConn);
            OleConnection.Open();

            MySqlDataAdapter OleAdapter = new MySqlDataAdapter(sql, this.szOleConn);
            OleAdapter.SelectCommand.CommandTimeout = 0;
            DataSet myDataSet = new DataSet();

            try
            {
                OleAdapter.Fill(myDataSet);
                return myDataSet.Tables[0];
            }
            catch (Exception er)
            {
                throw er;
            }
            finally
            {
                OleConnection.Close();
            }
        }

        #endregion

        #region 公共的加密接口
        /// <summary>
        /// 密文处理
        /// </summary>
        /// <param name="CodeString">密文</param>
        /// <returns>加密后的字符串</returns>
        public string EncryptCode(string strCode)
        {
            byte[] byCode = ASCIIEncoding.ASCII.GetBytes(strCode);
            byte[] byKey = ASCIIEncoding.ASCII.GetBytes("JrscSoft");
            byte[] byResult = new byte[strCode.Length];

           long j = 0;
            for (int i = 0; i < strCode.Length; i++)
            {
                j = j == byKey.Length ? 1 : j++;
                byResult[i] = (byte)(byCode[i] ^ byKey[j]);
            }

            return ASCIIEncoding.ASCII.GetString(byResult, 0, byResult.Length);
        }
        #endregion


    }

}

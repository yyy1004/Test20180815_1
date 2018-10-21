using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Jin.DataService;
using System.Data.OleDb;



namespace Jin.BusinessService

{
    public class JPJKC
    {
     

        DataService.DataService dService = new DataService.DataService();


        #region 井相关基本信息获取

       /// <summary>
       /// 读取井信息
       /// </summary>
       /// <param name="?"></param>
       /// <returns></returns>

        public static DataTable getPJInfo(string KC,string PX)
        {
            DataService.DataService dCurService = new Jin.DataService.DataService();

            string strSql = "select  * FROM 井开次效益汇总 where 开次='" + KC + "'  order by " + PX + "  ";
       
            return dCurService.GetOleTable(strSql);
        }
        public static DataTable getPJInfo(string KC, string Filter, string PX)
        {
            DataService.DataService dCurService = new Jin.DataService.DataService();

            string strSql = "select  * FROM 井开次效益汇总     where 开次='" + KC + "' and " + Filter + " order by " + PX + "  ";
       
            return dCurService.GetOleTable(strSql);
        }

        public static double getPJZSInfo(string KC)
        {


            double count = 0;
            string strSql = string.Format("select  AVG(平均钻速) FROM 井开次效益汇总 where 开次='" + KC + "'");

            Jin.DataService.DataService dService = new Jin.DataService.DataService();

            object obj = dService.GetValue(strSql.ToString());
            if (obj != null && !string.IsNullOrEmpty(obj.ToString().Trim()))
            {

                count = Convert.ToDouble(obj.ToString().Trim());
                return count;

            }
            else
                return count;

        }
        public static double getPJCBInfo(string KC)
        {


            double count = 0;
            string strSql = string.Format("select  AVG(单位进尺成本) FROM 井开次效益汇总 where 开次='" + KC + "'");

            Jin.DataService.DataService dService = new Jin.DataService.DataService();

            object obj = dService.GetValue(strSql.ToString());
            if (obj != null && !string.IsNullOrEmpty(obj.ToString().Trim()))
            {

                count = Convert.ToDouble(obj.ToString().Trim());
                return count;

            }
            else
                return count;

        }
        public static long getJSInfo(string KC)
        {


           long count = 0;
           string strSql = string.Format("select  count(*) FROM 井开次效益汇总 where 开次='" + KC + "'");

            Jin.DataService.DataService dService = new Jin.DataService.DataService();

            object obj = dService.GetValue(strSql.ToString());
            if (obj != null && !string.IsNullOrEmpty(obj.ToString().Trim()))
            {

                count = Convert.ToInt64(obj.ToString().Trim());
                return count;

            }
            else
                return count;

        }

        public static double getPJZSInfo2(string KC)
        {


            double count = 0;
            string strSql = string.Format("select  AVG(平均钻速) FROM 井开次效益汇总 where 新工艺名称='" + KC + "'");

            Jin.DataService.DataService dService = new Jin.DataService.DataService();

            object obj = dService.GetValue(strSql.ToString());
            if (obj != null && !string.IsNullOrEmpty(obj.ToString().Trim()))
            {

                count = Convert.ToDouble(obj.ToString().Trim());
                return count;

            }
            else
                return count;

        }
        public static double getPJCBInfo2(string KC)
        {


            double count = 0;
            string strSql = string.Format("select  AVG(单位进尺成本) FROM 井开次效益汇总 where 新工艺名称='" + KC + "'");

            Jin.DataService.DataService dService = new Jin.DataService.DataService();

            object obj = dService.GetValue(strSql.ToString());
            if (obj != null && !string.IsNullOrEmpty(obj.ToString().Trim()))
            {

                count = Convert.ToDouble(obj.ToString().Trim());
                return count;

            }
            else
                return count;

        }
        public static long getJSInfo2(string KC)
        {


            long count = 0;
            string strSql = string.Format("select  count(*) FROM 井开次效益汇总 where 新工艺名称='" + KC + "'");

            Jin.DataService.DataService dService = new Jin.DataService.DataService();

            object obj = dService.GetValue(strSql.ToString());
            if (obj != null && !string.IsNullOrEmpty(obj.ToString().Trim()))
            {

                count = Convert.ToInt64(obj.ToString().Trim());
                return count;

            }
            else
                return count;

        }


        public static double getPJZSInfo(string KC,string Filter)
        {


            double count = 0;
            string strSql = string.Format("select AVG(平均钻速)  FROM 井开次效益汇总  where 开次='" + KC + "'and " + Filter + "");

            Jin.DataService.DataService dService = new Jin.DataService.DataService();

            object obj = dService.GetValue(strSql.ToString());
            if (obj != null && !string.IsNullOrEmpty(obj.ToString().Trim()))
            {

                count = Convert.ToDouble(obj.ToString().Trim());
                return count;

            }
            else
                return count;

        }
        public static double getPJCBInfo(string KC, string Filter)
        {


            double count = 0;
            string strSql = string.Format("select AVG(单位进尺成本) FROM 井开次效益汇总  where 开次='" + KC + "'and " + Filter + "");

            Jin.DataService.DataService dService = new Jin.DataService.DataService();

            object obj = dService.GetValue(strSql.ToString());
            if (obj != null && !string.IsNullOrEmpty(obj.ToString().Trim()))
            {

                count = Convert.ToDouble(obj.ToString().Trim());
                return count;

            }
            else
                return count;

        }

        public static long getJSInfo(string KC, string Filter)
        {


            long  count = 0;
            string strSql = string.Format("select count(*) FROM 井开次效益汇总 where 开次='" + KC + "'and " + Filter + "");

            Jin.DataService.DataService dService = new Jin.DataService.DataService();

            object obj = dService.GetValue(strSql.ToString());
            if (obj != null && !string.IsNullOrEmpty(obj.ToString().Trim()))
            {

                count = Convert.ToInt32(obj.ToString().Trim());
                return count;

            }
            else
                return count;

        }
 


        #endregion

    



        /// <summary>
        /// 执行一条SQL 语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static bool ControlRole(string sql)
        {
            DataService.DataService dCurService = new Jin.DataService.DataService();

            string strSql = string.Format(sql);

            bool bSuced = false;
            bSuced = dCurService.ExecOleSql(strSql);
            return bSuced;
        }

       
    }
}

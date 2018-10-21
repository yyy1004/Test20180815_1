using System;
using System.Collections.Generic;
using System.Text;
using System.Data;



namespace Jin.BusinessService

{
    public class XJXX
    {
     

        DataService.DataService dService = new DataService.DataService();


        #region 井相关基本信息获取

       /// <summary>
       /// 读取井信息
       /// </summary>
       /// <param name="?"></param>
       /// <returns></returns>
       
        public static DataTable getJBBXXInfo(long count, long page)
        {
            DataService.DataService dCurService = new Jin.DataService.DataService();
            long count2 = (page - 1) * count;
            string strSql = "select top " + count + " * FROM 新井基本数据 where 状态='新井' order by 井号 ";
            if (page > 1)
                strSql = "select top " + count + " * FROM 新井基本数据  where  状态='新井'and 井号>(select max(井号) from(select top " + count2 + " 井号 FROM 新井基本数据 状态='新井' order by 井号)) order by 井号  ";

            return dCurService.GetOleTable(strSql);
        }
        public static DataTable getJBBXXInfo(long count, long page ,string Filter)
        {
            DataService.DataService dCurService = new Jin.DataService.DataService();
            long count2 = (page - 1) * count;
            string strSql = "select top " + count + " * FROM 新井基本数据   where (" + Filter + ")and 状态='新井' order by 井号";
            if (page > 1)
                strSql = "select top " + count + " * FROM 新井基本数据   where (" + Filter + ")and 状态='新井' and 井号>(select max(井号) from(select top " + count2 + " 井号 FROM 新井基本数据 where (" + Filter + ")and 状态='新井' order by 井号)) order by 井号  ";

            return dCurService.GetOleTable(strSql);
        }

        public static long getJBBXXInfo()
        {

           
            long count = 0;
            string strSql = string.Format("Select count(*) FROM 新井基本数据");

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
        public static long getJBBXXInfo(string Filter)
        {

            
            long count = 0;
            string strSql = string.Format("Select count(*) FROM 新井基本数据 where (" + Filter + ")and 状态='新井'");

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

        public static long getTABLENUM(string T,string JH)
        {


            long count = 0;
            string strSql = string.Format("Select count(*) FROM " + T + " where 井号='" + JH + "'");

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


        public static DataTable getJXXInfo(string JH)
        {
            DataService.DataService dCurService = new Jin.DataService.DataService();

            string strSql = "select * FROM 井基本和井深信息 where 井号='" + JH + "'";

            return dCurService.GetOleTable(strSql);

        }


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


        #region 井相关基本信息获取

        /// <summary>
        /// 读取文档信息
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>

        public static DataTable getWDXXInfo(long count, long page)
        {
            DataService.DataService dCurService = new Jin.DataService.DataService();
            long count2 = (page - 1) * count;
            string strSql = "select top " + count + " * FROM 文档库  order by 井号 ";
            if (page > 1)
                strSql = "select top " + count + " * FROM 文档库  where  井号>(select max(井号) from(select top " + count2 + " 井号 FROM 文档库)) order by 井号  ";

            return dCurService.GetOleTable(strSql);
        }
        public static DataTable getWDXXInfo(long count, long page, string Filter)
        {
            DataService.DataService dCurService = new Jin.DataService.DataService();
            long count2 = (page - 1) * count;
            string strSql = "select top " + count + " * FROM 文档库  where (" + Filter + ") order by 井号";
            if (page > 1)
                strSql = "select top " + count + " * FROM 文档库   where (" + Filter + ") and 井号>(select max(井号) from(select top " + count2 + " 井号 FROM 文档库 where (" + Filter + ")and order by 井号)) order by 井号  ";

            return dCurService.GetOleTable(strSql);
        }

        public static long getWDXXInfo()
        {


            long count = 0;
            string strSql = string.Format("Select count(*) FROM 文档库");

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
        public static long getWDXXInfo(string Filter)
        {


            long count = 0;
            string strSql = string.Format("Select count(*) FROM 文档库 where (" + Filter + ")");

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


    }
}

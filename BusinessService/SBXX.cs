using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Jin.DataService;
using System.Data.OleDb;



namespace Jin.BusinessService

{
    public class SBXX
    {

        DataService.DataService dService = new DataService.DataService();


   
     
        /// <summary>
        /// 读取设备基本信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSBinform(long count,long  page)
        {
            DataService.DataService dCurService = new Jin.DataService.DataService();
            long  count2 = (page - 1) * count;
            string strSql = "select top "+count+" * FROM SB_INFOR  order by 行号";
              if(page>1)
                  strSql = "select top " + count + " * FROM SB_INFOR  where 行号>(select max(行号) from(select top " + count2 + " 行号 FROM SB_INFOR  order by 行号)) order by 行号  ";

            return dCurService.GetOleTable(strSql);
        }


        public static long GetSBinform()
        {

            bool bSeced = false;
            long count = 0;
            string strSql = string.Format("Select count(*) FROM SB_INFOR");

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
        public static long GetSBinform(string Filter)
        {

            bool bSeced = false;
            long count = 0;
            string strSql = string.Format("Select count(*) FROM SB_INFOR where (" + Filter + ")");

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


        public static DataTable GetSBinform(string Filter, long count, long page)
        {
            DataService.DataService dCurService = new Jin.DataService.DataService();
            long count2 = (page - 1) * count;
            string strSql = "select top " + count + " * FROM SB_INFOR where (" + Filter + ") order by 行号  ";

             if(page>1)
                 strSql = "select top " + count + " * FROM SB_INFOR where (" + Filter + ") and  行号>(select max(行号) from(select top " + count2 + " 行号 FROM SB_INFOR where (" + Filter + ")  order by 行号))   ";

           
            return dCurService.GetOleTable(strSql);
        }
        public static DataTable GetSBinform2(string SB_ID)
        {
            DataService.DataService dCurService = new Jin.DataService.DataService();

            string strSql = "select * FROM SB_INFOR where SB_ID='" + SB_ID + "'";

            return dCurService.GetOleTable(strSql);
        }

        public static DataTable GetSBinform3(string SB_ID)
        {
            DataService.DataService dCurService = new Jin.DataService.DataService();

            string strSql = "select * FROM SB_INFOR where SB_ZJID='" + SB_ID + "'";

            return dCurService.GetOleTable(strSql);
        }

       /// <summary>
       /// 修改设备信息
       /// </summary>
       /// <param name="SB_ID"></param>
       /// <param name="JXNR"></param>
       /// <param name="JXJG"></param>
       /// <param name="JXFY"></param>
       /// <param name="BYNR"></param>
       /// <param name="BYFY"></param>
       /// <param name="BYJG"></param>
       /// <param name="date_by1"></param>
       /// <param name="date_by2"></param>
       /// <returns></returns>
        public static bool CHANGSBXX(string SB_NAME, string SB_ID, string BM_NAME, string SB_ZT, string SB_XH, string SB_ZZS, string SB_ZJID, string SB_ZJNAME, string SB_BZ, string SB_CCID, string SB_PIC, string SB_WZ, string SB_NF, string SB_FZR)
        {


            DataService.DataService dCurService = new DataService.DataService();



            string strSql = string.Format("update SB_INFOR SET SB_NAME='{0}',BM_NAME='{2}',SB_ZT='{3}', SB_XH='{4}', SB_ZZS='{5}', SB_ZJID='{6}',SB_ZJNAME='{7}', SB_BZ='{8}', SB_CCID='{9}', SB_PIC='{10}', SB_WZ='{11}', SB_NF='{12}',SB_FZR='{13}' Where SB_ID='{1}'", SB_NAME, SB_ID, BM_NAME, SB_ZT, SB_XH, SB_ZZS, SB_ZJID, SB_ZJNAME, SB_BZ, SB_CCID, SB_PIC, SB_WZ, SB_NF, SB_FZR);
            bool bSuced = false;
            bSuced = dCurService.ExecOleSql(strSql);
            return bSuced;


        }
      /// <summary>
      /// 添加设备信息
      /// </summary>
      /// <param name="SB_NAME"></param>
      /// <param name="SB_ID"></param>
      /// <param name="BM_NAME"></param>
      /// <param name="SB_ZT"></param>
      /// <param name="SB_XH"></param>
      /// <param name="SB_ZZS"></param>
      /// <param name="SB_ZJID"></param>
      /// <param name="SB_ZJNAME"></param>
      /// <param name="SB_BZ"></param>
      /// <param name="SB_CCID"></param>
      /// <param name="SB_PIC"></param>
      /// <param name="SB_WZ"></param>
      /// <param name="SB_NF"></param>
      /// <param name="SB_FZR"></param>
      /// <returns></returns>
        public static bool insertSB(string SB_NAME, string SB_ID, string BM_NAME, string SB_ZT, string SB_XH, string SB_ZZS, string SB_ZJID, string SB_ZJNAME, string SB_BZ, string SB_CCID, string SB_PIC, string SB_WZ, string SB_NF, string SB_FZR)
        {

            DataService.DataService dCurService = new DataService.DataService();



            string strSql = string.Format("insert into SB_INFOR (SB_NAME, SB_ID,BM_NAME,SB_ZT, SB_XH, SB_ZZS, SB_ZJID,SB_ZJNAME, SB_BZ, SB_CCID, SB_PIC, SB_WZ, SB_NF,SB_FZR) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}') ", SB_NAME, SB_ID, BM_NAME, SB_ZT, SB_XH, SB_ZZS, SB_ZJID, SB_ZJNAME, SB_BZ, SB_CCID, SB_PIC, SB_WZ, SB_NF, SB_FZR);
            bool bSuced = false;
            bSuced = dCurService.ExecOleSql(strSql);
            return bSuced;


        }

        public static bool insertSBWD(string SB_ID, string wdm, string scsj, string bz, string wdlx)
        {

            DataService.DataService dCurService = new DataService.DataService();



            string strSql = string.Format("INSERT INTO [SBWD] ([SB_ID], [文档名], [上传时间], [备注], [类型]) VALUES ('{0}','{1}','{2}','{3}','{4}') ", SB_ID, wdm, scsj, bz, wdlx);
            bool bSuced = false;
            bSuced = dCurService.ExecOleSql(strSql);
            return bSuced;


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


        /// <summary>
        /// 返回备件信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetBJXX2()
        {

            return new Jin.DataService.DataService().GetOleTable("select * from BJXX where BJ_ZT='闲置' ");
        }
        public static DataTable GetBJXX3()
        {

            return new Jin.DataService.DataService().GetOleTable("select * from BJXX where BJ_SL=0 ");
        }
        public static DataTable GetBJXX()
        {

            return new Jin.DataService.DataService().GetOleTable("select * from BJXX  ");
        }
        public static DataTable GetBJXX(string bj_name)
        {
            //return new Jin.DataService.DataService().GetOleTable("select b.*,r.* from BJXX b,BJRK r where r.bj_id = b.bj_id and  b.bj_name='" +bj_name+"'");

            return new Jin.DataService.DataService().GetOleTable("select * from BJXX  where  " + bj_name + "");
        }
        public static DataTable GetBJXX2(string bj_ID)
        {
           
            return new Jin.DataService.DataService().GetOleTable("select * from BJXX  where BJ_ID='" + bj_ID + "'");
        }
        //select count(* ) from bjxx t group by bj_name,bj_xh
        public static DataTable GetBJkcXX()
        {
            return new Jin.DataService.DataService().GetOleTable("select t.bj_name,t.bj_xh,count(* )as sl ,MAX(T.BJ_ID) AS BJ_ID,MAX(T.BZ) AS BZ,MAX(T.BJ_CS) AS BJ_CS,MAX(T.LYR) AS LYR ,MAX(T.BJ_SCS) AS BJ_SCS, MAX(T.JG) AS JG  from bjxx t WHERE BJ_ZT='闲置' group by bj_name,bj_xh ORDER BY SL ");
        }


        public static DataTable GetBJkcXX(string bj_name, string bj_xh)
        {
            string sql = "select t.bj_name,t.bj_xh,count(* )as sl from bjxx t WHERE BJ_ZT='闲置'  group by bj_name,bj_xh having bj_name= '" + bj_name + "' and bj_xh= '" + bj_xh + "'";
            return new Jin.DataService.DataService().GetOleTable(sql);
        }


        public static DataTable GetBJkcXX(string bj_name)
        {
            string sql="select t.bj_name,t.bj_xh,count(* )as sl ,MAX(T.BJ_ID) AS BJ_ID,MAX(T.BZ) AS BZ,MAX(T.BJ_CS) AS BJ_CS,MAX(T.LYR) AS LYR ,MAX(T.BJ_SCS) AS BJ_SCS ,MAX(T.JG) AS JG from bjxx t WHERE BJ_ZT='闲置'and  " + bj_name + " group by bj_name,bj_xh ORDER BY SL ";
        
            return new Jin.DataService.DataService().GetOleTable(sql);
        }


        public static DataTable GetCRKJLinform()
        {
            DataService.DataService dCurService = new Jin.DataService.DataService();

            string strSql = "select * FROM BJCRJL ";

            return dCurService.GetOleTable(strSql);
        }
        public static DataTable GetCRKJLinform(string Filter)
        {
            DataService.DataService dCurService = new Jin.DataService.DataService();

            string strSql = "select * FROM BJCRJL where (" + Filter + ")";

            return dCurService.GetOleTable(strSql);
        }


        /// <summary>
        /// NEW
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static DataTable GetBJXXNEW(long ID)
        {

            return new Jin.DataService.DataService().GetOleTable("select * from BJXX  where 行号=" + ID + " ");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataService;
using System.Data;
using System.Web.Security;
using System.IO;
using System.Web.SessionState;
using Common;
using Newtonsoft.Json;
using JrscSoft.Common;
using System.Collections;
using Model;
using System.Threading;

namespace Web.JZ
{
    public partial class jz1 : System.Web.UI.Page
    {
        public static string JZBH = "gj001";//机组编号
        public static string jzname = "";//机组名称
                                         // public int menuid = 0;
                                         // public int status = 0;// 机组状态 0: 默认1:绿色 2：黄色：3:红色
                                         //   public string[] CDVAL = new string[8] { "0", "0", "0", "0", "0", "0", "0", "0" };
                                         //  public string[] CDNAME = new string[8] { "", "", "", "", "", "", "", "" };
        protected void Page_Load(object sender, EventArgs e)
        {
            //首次加载 
            if (!IsPostBack)
            {

                int menuid = Convert.ToInt32(Request.QueryString["node"].ToString());//从url里传过来的参数。菜单id
                jzname = MenuHelper.GetMenuNameById(menuid);//根据id获取其对应的名称。因为在机组页面，也即机组名称。
                string sql = "SELECT 机组编号 FROM `web`.`机组表` where 机组名称=\"" + jzname + "\" ";
                DataAccess data = new DataAccess();
                JZBH = data.GetValue(sql).ToString();//赋值机组编号
                Web.File.FileHandler.jzidstr = JZBH;//删除待定
                                                    //  Web.JZ.JZHandler.tableName = JZBH;
                                                    //sql = "SELECT 状态 FROM `web`.`机组表` where 机组名称=\"" + jzname + "\" ";
                                                    //status = Convert.ToInt16(data.GetValue(sql).ToString());
                                                    //   BindJZCD();
                                                    //  new Thread(Go).Start();
                                                    // this.Timer1.Enabled = true;
            }
        }
        #region 对测点名测点值进行赋值。
        /// <summary>
        /// 对测点名称和测点值进行赋值。
        /// </summary>
        //public void BindJZCD()
        //{
        //    DataTable dTable = null;
        //    DataView dView = null;
        //    string sql = "SELECT * FROM `web`.`" + JZBH + "` ORDER BY ID DESC  limit 1 ";

        //    dTable = data.GetTable(sql);
        //    int colnum = dTable.Columns.Count;
        //    if (dTable.Rows.Count == 1)
        //    {
        //        if (colnum > 4)
        //        {

        //            CDNAME[0] = dTable.Columns[4].ColumnName;
        //            CDVAL[0] = dTable.Rows[0][4].ToString();
        //        }


        //        if (colnum > 5)
        //        {

        //            CDNAME[1] = dTable.Columns[5].ColumnName;
        //            CDVAL[1] = dTable.Rows[0][5].ToString();
        //        }

        //        if (colnum > 6)
        //        {

        //            CDNAME[2] = dTable.Columns[6].ColumnName;
        //            CDVAL[2] = dTable.Rows[0][6].ToString();
        //        }

        //        if (colnum > 7)
        //        {

        //            CDNAME[3] = dTable.Columns[7].ColumnName;
        //            CDVAL[3] = dTable.Rows[0][7].ToString();
        //        }

        //        if (colnum > 8)
        //        {

        //            CDNAME[4] = dTable.Columns[8].ColumnName;
        //            CDVAL[4] = dTable.Rows[0][8].ToString();
        //        }

        //        if (colnum > 9)
        //        {

        //            CDNAME[5] = dTable.Columns[9].ColumnName;
        //            CDVAL[5] = dTable.Rows[0][9].ToString();
        //        }

        //        if (colnum > 10)
        //        {

        //            CDNAME[6] = dTable.Columns[10].ColumnName;
        //            CDVAL[6] = dTable.Rows[0][10].ToString();
        //        }

        //        if (colnum > 11)
        //        {

        //            CDNAME[7] = dTable.Columns[11].ColumnName;
        //            CDVAL[7] = dTable.Rows[0][11].ToString();
        //        }
        //    }

        //}

        //private void Go()
        //{
        //    while (true)
        //    {
        //        Thread.Sleep(10000);
        //        BindJZCD();
        //    }
        //}
        //protected void Timer1_Tick(object sender, EventArgs e)
        //{
        //    BindJZCD();
        //    // this.Label1.Text = (Convert.ToInt16(this.Label1.Text) + 1).ToString();
        //    // this.Label2.Text=(Convert.ToInt16( this.Label2.Text)+1).ToString();

        //} 
        #endregion
    }
}
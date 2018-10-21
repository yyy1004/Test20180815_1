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
        DataAccess data = new DataAccess();

       public string JZBH = "10001";
        public int menuid=0;
        public int status = 0;// 机组状态 0: 默认1:绿色 2：黄色：3:红色
        public string[] CDVAL = new string[8] { "0", "0", "0", "0", "0", "0", "0", "0" };
        public  string[] CDNAME = new string[8] { "", "", "", "", "", "", "", "" };
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                menuid = Convert.ToInt32(Request.QueryString["node"].ToString());
               string jzmc = MenuHelper.GetMenuNameById(menuid);
               string sql = "SELECT 机组编号 FROM `web`.`机组表` where 机组名称=\"" + jzmc + "\" ";
               JZBH = data.GetValue(sql).ToString();
               Web.File.FileHandler.jzidstr = JZBH;
                sql = "SELECT 状态 FROM `web`.`机组表` where 机组名称=\"" + jzmc + "\" ";
                status= Convert.ToInt16( data.GetValue(sql).ToString());
                //btnFirst.Text = "最首页";
                //btnPrev.Text = "前一页";
                //btnNext.Text = "下一页";
                //btnLast.Text = "最后页";
                //this.GridUser.PageSize = 20;
                ////数据选择过滤
                //ViewState["Filter"] = "";


                BindJZCD();
             new Thread(Go).Start();

               // this.Timer1.Enabled = true;
            }
        }
        public void BindJZCD()
        {
            //string JZBH = "10001";
            DataTable dTable = null;
            DataView dView = null;
            string sql = "SELECT * FROM `web`.`" + JZBH + "` ORDER BY ID DESC  limit 1 ";
            
            dTable = data.GetTable(sql);
            int colnum = dTable.Columns.Count;
            if (dTable.Rows.Count == 1)
            {
            if (colnum > 4)
            {
                
                CDNAME[0]= dTable.Columns[4].ColumnName;
                CDVAL[0] = dTable.Rows[0][4].ToString();
            }
           

            if (colnum > 5)
            {
               
                CDNAME[1] = dTable.Columns[5].ColumnName;
                CDVAL[1] = dTable.Rows[0][5].ToString();
            }
            
            if (colnum > 6)
            {
                
                CDNAME[2] = dTable.Columns[6].ColumnName;
                CDVAL[2] = dTable.Rows[0][6].ToString();
            }
           
            if (colnum > 7)
            {
                
                CDNAME[3] = dTable.Columns[7].ColumnName;
                CDVAL[3] = dTable.Rows[0][7].ToString();
            }
           
            if (colnum > 8)
            {
                
                CDNAME[4] = dTable.Columns[8].ColumnName;
                CDVAL[4] = dTable.Rows[0][8].ToString();
            }
           
            if (colnum > 9)
            {
                
                CDNAME[5] = dTable.Columns[9].ColumnName;
                CDVAL[5] = dTable.Rows[0][9].ToString();
            }
            
            if (colnum > 10)
            {
               
                CDNAME[6] = dTable.Columns[10].ColumnName;
                CDVAL[6] = dTable.Rows[0][10].ToString();
            }
           
            if (colnum > 11)
            {
                
                CDNAME[7] = dTable.Columns[11].ColumnName;
                CDVAL[7] = dTable.Rows[0][11].ToString();
            }
            }
           
        }


        private void Go()
        {
            while (true)
            {
                Thread.Sleep(60000);
                BindJZCD();

            }

        }


        protected void Timer1_Tick(object sender, EventArgs e)
        {
            BindJZCD();
           // this.Label1.Text = (Convert.ToInt16(this.Label1.Text) + 1).ToString();
            // this.Label2.Text=(Convert.ToInt16( this.Label2.Text)+1).ToString();
           
        }
    }
}
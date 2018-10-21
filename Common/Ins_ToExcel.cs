using System;
using System.Data;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Collections;
using System.Web.UI;
using System.Web;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Security;
using System.Text;

namespace JrscSoft.Common
{
    public class Ins_ToExcel
    {
        public Ins_ToExcel()
        { 
        
        }
        public void ToExcel(System.Web.UI.WebControls.DataGrid DataGrid2Excel, string FileName, string Title, string Head)
        {
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);

            FrontDecorator(hw);
            if (Title != "")
                hw.Write(Title + "<br>");
            if (Head != "")
                hw.Write(Head + "<br>");

            DataGrid2Excel.EnableViewState = false;
            DataGrid2Excel.RenderControl(hw);

            RearDecorator(hw);

            System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            response.Clear();
            response.Buffer = true;
            response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            response.ContentType = "application/vnd.ms-excel";
            response.Write("<meta   http-equiv=Content-Type   content=text/html;charset=GB2312>");
            string file = HttpUtility.UrlEncode(FileName + ".xls", System.Text.Encoding.GetEncoding("GB2312"));
            response.AddHeader("Content-Disposition", "attachment; filename=" + file);
            response.Charset = "GB2312";
            response.Write(sw.ToString());
            response.End();
        }
        private static void RearDecorator(HtmlTextWriter writer)
        {
            writer.WriteEndTag("Body");
            writer.WriteEndTag("HTML");
        }

        //入口
        public void ToExcel(DataTable dt, string FileName)
        {
            System.Web.UI.WebControls.DataGrid dgTemp = new System.Web.UI.WebControls.DataGrid();
            dgTemp.DataSource = dt;
            dgTemp.DataBind();
            ToExcel(dgTemp, FileName, "", "");
        }
        public void ToExcel(DataTable dt, string FileName, string Title, string Head)
        {
            System.Web.UI.WebControls.DataGrid dgTemp = new System.Web.UI.WebControls.DataGrid();
            dgTemp.DataSource = dt;
            dgTemp.DataBind();
            ToExcel(dgTemp, FileName,  Title,  Head);
        }
        //FrontDecorator方法
        private static void FrontDecorator(HtmlTextWriter writer)
        {
            writer.WriteFullBeginTag("HTML");
            writer.WriteFullBeginTag("Head");
            writer.WriteEndTag("Head");
            writer.WriteFullBeginTag("Body");
        }


        //// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="dt">表</param>
        public static void InitExcel(DataTable dtt)
        {
            DataGrid dgExport = null;
            StringWriter strWriter = null;
            HtmlTextWriter htmlWriter = null;
            System.Data.DataTable dt = dtt;

            if (dt != null)
            {
                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                HttpContext.Current.Response.Write("<meta   http-equiv=Content-Type   content=text/html;charset=GB2312>");
                HttpContext.Current.Response.Charset = "";
                strWriter = new StringWriter();
                htmlWriter = new HtmlTextWriter(strWriter);

                dgExport = new DataGrid();
                dgExport.DataSource = dt.DefaultView;
                dgExport.AllowPaging = false;
                dgExport.HeaderStyle.ForeColor = System.Drawing.Color.Blue;
                dgExport.HeaderStyle.BackColor = System.Drawing.Color.Red;
                dgExport.DataBind();

                dgExport.RenderControl(htmlWriter);
                HttpContext.Current.Response.Write(strWriter.ToString());
                HttpContext.Current.Response.End();
            }
        }

    }
}
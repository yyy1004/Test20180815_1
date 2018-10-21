using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Data;
using DataService;
using System.Collections;
using Model;

namespace JrscSoft.Common
{
    public class WebUI
    {
        public WebUI()
        { }

        public static void ShowNoResultRow(GridViewRow gr)
        {
            int columnCount = gr.Cells.Count;
            gr.Cells.Clear();
            gr.Cells.Add(new TableCell());
            gr.Cells[0].ColumnSpan = columnCount;
            gr.Cells[0].Style.Add("text-align", "center");
            gr.Cells[0].Text = "<font color='red'>没有任何查询结果</font>";
        }
    }

}

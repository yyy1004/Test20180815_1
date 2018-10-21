using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using DataService;



namespace Web.JZ
{
    public partial class jzzzt : System.Web.UI.Page
    {
        Bitmap bitmap;
        Graphics graphics;
        Color color1;
        Color color2;
        Color color3;
        Color color4;
        DataAccess data = new DataAccess();

        //public string JZBH = "CS001";
        public string JZBH = "GJ001";
        public int status = 0;// 机组状态 0: 默认1:绿色 2：黄色：3:红色
        public float[] CDVAL = new float[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
        public string[] CDNAME = new string[8] { "", "", "", "", "", "", "", "" };
        public int[] bjz = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
        public int[] yjz = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
        protected void Page_Load(object sender, EventArgs e)
        {
            color1 = Color.FromKnownColor(KnownColor.Green);
            color2 = Color.FromKnownColor(KnownColor.Yellow);
            color3 = Color.FromKnownColor(KnownColor.Red);
            color4 = Color.FromKnownColor(KnownColor.Gray);
            bitmap = new Bitmap(600, 600);
            graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            Font font2 = new Font("宋体", 15, FontStyle.Bold);
           JZBH=Web.File.FileHandler.jzidstr;

            // graphics.DrawString("机组运行状态：", font2, new SolidBrush(Color.Black),  20, 20);
            // graphics.DrawString(DateTime.Now.ToString(), font2, new SolidBrush(Color.Red), 20, 20);
            string sql2 = "SELECT 状态 FROM `web`.`机组表` where 机组编号=\"" + JZBH + "\" ";
            int status = Convert.ToInt16(data.GetValue(sql2).ToString());
            string sql3 = "SELECT CONCAT(测试日期,' ',测试时间) FROM `web`.`"+JZBH+"` order by ID  limit 1 ";//升序
            string newtime = data.GetValue(sql3).ToString();
            if (DateTime.Now.AddMinutes(-5) > Convert.ToDateTime(newtime))
                status = 4;
            Pen pen = new Pen(color1, 20);
            if (status == 1) { pen = new Pen(color1, 20); graphics.DrawString("机组运行正常：", font2, new SolidBrush(Color.Black), 20, 20); }
            if (status == 2) { pen = new Pen(color2, 20); graphics.DrawString("机组有警告：", font2, new SolidBrush(Color.Black), 20, 20); }
            if (status == 3) { pen = new Pen(color3, 20); graphics.DrawString("机组运行故障：", font2, new SolidBrush(Color.Black), 20, 20); }
            if (status == 4) { pen = new Pen(color4, 20); graphics.DrawString("机组停机（断网）：", font2, new SolidBrush(Color.Black), 20, 20); }
            //Rectangle rectty = new Rectangle(210,20, 20, 20);
            Rectangle rectty = new Rectangle(280, 20, 20, 20);
            graphics.DrawEllipse(pen, rectty);
            DataTable dTable = null;
            string sql = "SELECT * FROM `web`.`" + JZBH + "` ORDER BY ID   limit 1 ";//升序
           
            dTable = data.GetTable(sql);
            int colnum = dTable.Columns.Count;
            string sqlgetjingjiezhi = "select 预警值,报警值 from 测点表  where 机组编号='" + JZBH + "' ";
            DataTable dTablevalue = null;
            dTablevalue= data.GetTable(sqlgetjingjiezhi);
            int ii = 0;
            for (ii = 0; ii < dTablevalue.Rows.Count; ii++)
            {
                yjz[ii] = Int32.Parse(dTablevalue.Rows[ii][0].ToString());
                bjz[ii]= Int32.Parse(dTablevalue.Rows[ii][1].ToString());

            }

                int startX = 30;
            int addx = 120;
            if (dTable.Rows.Count == 1)
            {
                if (colnum > 4)
                {

                    CDNAME[0] = dTable.Columns[4].ColumnName;
                    CDVAL[0] = float.Parse(dTable.Rows[0][4].ToString());
                    drawrect2(CDNAME[0], yjz[0], bjz[0], bjz[0]+5, CDVAL[0], startX, 250, 4);
                }


                if (colnum > 5)
                {
                    startX = startX + addx;
                    CDNAME[1] = dTable.Columns[5].ColumnName;
                    CDVAL[1] = float.Parse(dTable.Rows[0][5].ToString());
                    drawrect(CDNAME[1], yjz[1], bjz[1], bjz[1] + 5, CDVAL[1], startX, 250, 4);
                }

                if (colnum > 6)
                {
                    startX = startX + addx;
                    CDNAME[2] = dTable.Columns[6].ColumnName;
                    CDVAL[2] = float.Parse(dTable.Rows[0][6].ToString());
                    drawrect(CDNAME[2], yjz[2], bjz[2], bjz[2] + 5, CDVAL[2], startX, 250, 4);
                }

                if (colnum > 7)
                {
                    startX = startX + addx;
                    CDNAME[3] = dTable.Columns[7].ColumnName;
                    CDVAL[3] = float.Parse(dTable.Rows[0][7].ToString());
                    drawrect(CDNAME[3], yjz[3], bjz[3], bjz[3] + 5, CDVAL[3], startX, 250, 4);
                }

                if (colnum > 8)
                {
                    startX = 30;
                    CDNAME[4] = dTable.Columns[8].ColumnName;
                    CDVAL[4] = float.Parse(dTable.Rows[0][8].ToString());
                    drawrect2(CDNAME[4], yjz[4], bjz[4], bjz[4] + 5, CDVAL[4], startX, 500, 4);
                }

                if (colnum > 9)
                {
                    startX = startX + addx;
                    CDNAME[5] = dTable.Columns[9].ColumnName;
                    CDVAL[5] = float.Parse(dTable.Rows[0][9].ToString());
                    drawrect(CDNAME[5], yjz[5], bjz[5], bjz[5] + 5, CDVAL[5], startX, 500, 4);
                }

                if (colnum > 10)
                {
                    startX = startX + addx;
                    CDNAME[6] = dTable.Columns[10].ColumnName;
                    CDVAL[6] = float.Parse(dTable.Rows[0][10].ToString());
                    drawrect(CDNAME[6], yjz[6], bjz[6], bjz[6] + 5, CDVAL[6], startX, 500, 4);
                }

                if (colnum > 11)
                {
                    startX = startX + addx;
                    CDNAME[7] = dTable.Columns[11].ColumnName;
                    CDVAL[7] = float.Parse(dTable.Rows[0][11].ToString());
                    drawrect(CDNAME[7], yjz[7], bjz[7], bjz[7] + 5, CDVAL[7], startX, 500, 4);
                }
            }
            //drawrect("5", 20, 30, 35, CDVAL[3], 310, 200, 4);
            //drawrect("6", 20, 30, 35, CDVAL[4], 30, 450, 4);
            //drawrect("7", 20, 30, 35, CDVAL[5], 100, 450, 4);
            //drawrect("8", 20, 30, 35, CDVAL[6], 170, 450, 4);
            //drawrect("9", 20, 30, 35, CDVAL[7], 240, 450, 4);
            //drawrect("10", 75, 90, 100, CDVAL[7], 310, 450, 2);

            Response.ContentType = "image/gif";//声明输出流对象为gif图像，这个可能可以不要。  
            bitmap.Save(Response.OutputStream, ImageFormat.Gif);   //将bmp输出到页面，这个是关键，配合  



            //System.IO.MemoryStream ms = new System.IO.MemoryStream();
            //bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            //Response.ClearContent();
            //Response.ContentType = "image/Jpeg";
            //Response.BinaryWrite(ms.ToArray());


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">测点名称</param>
        /// <param name="Vmid1">报警1</param>
        /// <param name="Vmid2">报警2</param>
        /// <param name="Vmax">最大值</param>
        /// <param name="value">实时值</param>
        /// <param name="px">开始坐标X</param>
        /// <param name="py">开始坐标Y</param>
        /// <param name="bs">倍数</param>
        public void drawrect2(string name, int Vmid1, int Vmid2, int Vmax, float value, int px, int py, int bs)
        {
            string text = name;
            int mid1 = Vmid1 * bs;
            int mid2 = Vmid2 * bs;
            int max = Vmax * bs;
            int val = (int)Math.Round(value, 0) * bs;
            int y = py;
            int x = px;
            Font font = new Font("宋体", 8);
            Rectangle rect = new Rectangle(x, y - mid1, 10, mid1); // 创建一个矩形(x,y,width,height)
            Brush b = new SolidBrush(color1);
            b = new SolidBrush(color1);
            rect = new Rectangle(x, y - mid1, 10, mid1);
            graphics.FillRectangle(b, rect);
            b = new SolidBrush(color2);
            rect = new Rectangle(x, y - mid2, 10, mid2 - mid1);
            graphics.FillRectangle(b, rect);
            b = new SolidBrush(color3);
            rect = new Rectangle(x, y - max, 10, max - mid2);
            graphics.FillRectangle(b, rect);
            graphics.DrawString("0", font, new SolidBrush(Color.Black), x - 20, y - 10);
            graphics.DrawString(text, font, new SolidBrush(Color.Black), x - 10, y + 5);
            graphics.DrawString(Vmid1.ToString(), font, new SolidBrush(Color.Black), x - 20, y - mid1 - 10);
            graphics.DrawString(Vmid2.ToString(), font, new SolidBrush(Color.Black), x - 20, y - mid2 - 10);
            graphics.DrawString(Vmax.ToString(), font, new SolidBrush(Color.Black), x - 20, y - max - 10);
            font = new Font("宋体", 8, FontStyle.Bold);
            graphics.DrawString(value.ToString("F2"), font, new SolidBrush(Color.Black), x + 10, y - val - 15);
            if (val < mid1)
                b = new SolidBrush(color1);
            else if (val < mid2)
                b = new SolidBrush(color2);
            else
                b = new SolidBrush(color3);



            rect = new Rectangle(x + 20, y - val, 30, val);
            graphics.FillRectangle(b, rect);

        }

        public void drawrect(string name, int Vmid1, int Vmid2, int Vmax, float value, int px, int py, int bs)
        {
            string text = name;
            int mid1 = Vmid1 * bs;
            int mid2 = Vmid2 * bs;
            int max = Vmax * bs;
            int val = (int)Math.Round(value, 0) * bs;
            int y = py;
            int x = px;
            Font font = new Font("宋体", 8);
            Rectangle rect = new Rectangle(x, y - mid1, 10, mid1); // 创建一个矩形(x,y,width,height)
            Brush b = new SolidBrush(color1);
            //b = new SolidBrush(color1);
            //rect = new Rectangle(x, y - mid1, 10, mid1);
            //graphics.FillRectangle(b, rect);
            //b = new SolidBrush(color2);
            //rect = new Rectangle(x, y - mid2, 10, mid2 - mid1);
            //graphics.FillRectangle(b, rect);
            //b = new SolidBrush(color3);
            //rect = new Rectangle(x, y - max, 10, max - mid2);
            //graphics.FillRectangle(b, rect);
            //graphics.DrawString("0", font, new SolidBrush(Color.Black), x - 20, y - 10);
            graphics.DrawString(text, font, new SolidBrush(Color.Black), x - 10, y + 5);
            //graphics.DrawString(Vmid1.ToString(), font, new SolidBrush(Color.Black), x - 20, y - mid1 - 10);
            //graphics.DrawString(Vmid2.ToString(), font, new SolidBrush(Color.Black), x - 20, y - mid2 - 10);
            //graphics.DrawString(Vmax.ToString(), font, new SolidBrush(Color.Black), x - 20, y - max - 10);
            font = new Font("宋体", 8, FontStyle.Bold);
            graphics.DrawString(value.ToString("F2"), font, new SolidBrush(Color.Black), x + 10, y - val - 15);
            if (val < mid1)
                b = new SolidBrush(color1);
            else if (val < mid2)
                b = new SolidBrush(color2);
            else
                b = new SolidBrush(color3);



            rect = new Rectangle(x + 20, y - val, 30, val);
            graphics.FillRectangle(b, rect);

        }

    }
}
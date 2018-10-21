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
namespace Web
{
    /// <summary>
    /// GetMenu 的摘要说明
    /// </summary>
    public class GetMenu : IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        /// 连接数据库
        /// </summary>
        DataAccess data = new DataAccess();

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                Hashtable menuHashT = MenuHelper.GetAllMenu();//菜单表
                string result = "";
                //if (context.Session["目录"] == null || context.Session["目录"].ToString().Trim() == "")
                if (true)
                {

                    string sql = "";

                    Tree<string> root = new Tree<string>();//root：管理员 一级菜单。

                    DataTable dTable = null;

                    Tree<string> lev3JT = new Tree<string>();//lev3JT 为当前操作集团


                    Tree<string> lev4GC = new Tree<string>();//lev4GC 为当前操作工厂


                    Tree<string> lev2Mon, lev2sysMana = null;//n1:设备监测 二级菜单  lev2sysMana:系统管理 二级菜单

                    try
                    {
                        //在.ashx中引用 session 必须继承IReadOnlySessionState/IRequiresSessionState,否则无法获取Session对象
                        switch (context.Session["角色"].ToString())
                        {
                            case "管理员":
                                root.Data = "管理员";
                                root.Open = true;

                                lev2Mon = new Tree<string>();
                                lev2Mon.Data = "设备监测";
                                lev2Mon.Open = true;
                                root.AddNode(lev2Mon);

                                lev2sysMana = new Tree<string>();
                                lev2sysMana.Data = "系统管理";
                                lev2sysMana.Open = true;
                                root.AddNode(lev2sysMana);

                                Tree<string> lev31YHGL = new Tree<string>();
                                lev31YHGL.Data = "用户管理";
                                lev2sysMana.AddNode(lev31YHGL);

                                Tree<string> lev32JTGL = new Tree<string>();
                                lev32JTGL.Data = "集团管理";
                                lev2sysMana.AddNode(lev32JTGL);

                                Tree<string> lev33GCGL = new Tree<string>();
                                lev33GCGL.Data = "工厂管理";
                                lev2sysMana.AddNode(lev33GCGL);

                                Tree<string> lev34JZGL = new Tree<string>();
                                lev34JZGL.Data = "机组管理";
                                lev2sysMana.AddNode(lev34JZGL);

                                Tree<string> lev35CDGL = new Tree<string>();
                                lev35CDGL.Data = "测点管理";
                                lev2sysMana.AddNode(lev35CDGL);

                                Tree<string> lev36WDGL = new Tree<string>();
                                lev36WDGL.Data = "文档管理";
                                lev2sysMana.AddNode(lev36WDGL);

                                Tree<string> lev37ZXGL = new Tree<string>();
                                lev37ZXGL.Data = "咨询管理";
                                lev2sysMana.AddNode(lev37ZXGL);

                                sql = "SELECT * FROM `web`.`menue`";

                                dTable = data.GetTable(sql);
                                if (dTable.Rows.Count > 0)
                                {
                                    for (int i = 0; i < dTable.Rows.Count; i++)
                                    {
                                        string jt = dTable.Rows[i]["集团名称"].ToString();
                                        //不存在集团 ,就添加
                                        if (jt != "" && !Tree<string>.Exists(lev2Mon, jt)) //ChildrenNode相当于是node的子节点
                                        {

                                            Tree<string> tempNode = new Tree<string>();//tempNode 为临时变量，将新的集团加入到设备监测中
                                            tempNode.Data = jt;
                                            lev2Mon.AddNode(tempNode);
                                            lev3JT = tempNode;//将tempNode赋值给当前操作集团lev3JT
                                        }

                                        string gc = dTable.Rows[i]["工厂名称"].ToString();
                                        //不存在工厂 添加
                                        if (gc != "" && !Tree<string>.Exists(lev3JT, gc))
                                        {
                                            //node2为临时变量，将新的工厂加入到集团中
                                            Tree<string> tempNode = new Tree<string>();
                                            tempNode.Data = gc;//赋值 工厂名称
                                            lev3JT.AddNode(tempNode);//添加到 lev3JT中
                                            lev4GC = tempNode;
                                        }
                                        //添加机组菜单
                                        sql = "SELECT * FROM `web`.`机组表` where `工厂名称`='" + lev4GC.Data + "'";

                                        DataTable tableJZwhere = data.GetTable(sql);
                                        for (int j = 0; j < tableJZwhere.Rows.Count; j++)
                                        {
                                            string jz = tableJZwhere.Rows[j]["机组名称"].ToString();
                                            //不存在机组
                                            if (jz != "" && !Tree<string>.Exists(lev4GC, jz))
                                            {
                                                Tree<string> tempNode = new Tree<string>();//定义临时节点
                                                tempNode.Data = jz;//将机组赋值给节点的Data属性
                                                lev4GC.AddNode(tempNode);//将这个临时节点添加到第四级节点（工厂）。
                                                //因为不需要在其下面添加新的节点。所以不需要将这里的临时变量赋值保存起来。
                                            }
                                        }
                                    }
                                }
                                break;
                            case "集团负责人":
                                sql = "SELECT * FROM `web`.`用户表` where 用户名='" + context.Session["用户名"].ToString() + "'";
                                dTable = data.GetTable(sql);
                                root.Data = dTable.Rows[0]["集团名称"].ToString();
                                root.Open = true;

                                sql = "SELECT * FROM `web`.`menue`";
                                ////整理菜单
                                dTable = data.GetTable(sql);
                                if (dTable.Rows.Count > 0)
                                {
                                    for (int i = 0; i < dTable.Rows.Count; i++)
                                    {
                                        if (dTable.Rows[i]["集团名称"].ToString().Equals(root.Data))
                                        {
                                            string gc = dTable.Rows[i]["工厂名称"].ToString();
                                            //不存在工厂
                                            if (gc != "" && !Tree<string>.Exists(root, gc))
                                            {
                                                Tree<string> node1 = new Tree<string>();
                                                node1.Data = gc;
                                                root.AddNode(node1);
                                                lev3JT = node1;
                                            }

                                            sql = "SELECT * FROM `web`.`机组表` where `工厂名称`='" + lev3JT.Data + "'";
                                            ////整理菜单
                                            DataTable dTable2 = data.GetTable(sql);
                                            for (int j = 0; j < dTable2.Rows.Count; j++)
                                            {
                                                string jz = dTable2.Rows[j]["机组名称"].ToString();
                                                //不存在机组
                                                if (jz != "" && !Tree<string>.Exists(lev3JT, jz))
                                                {
                                                    Tree<string> node3 = new Tree<string>();
                                                    node3.Data = jz;
                                                    lev3JT.AddNode(node3);
                                                }
                                            }
                                        }
                                    }
                                }
                                break;
                            case "工厂负责人":
                                sql = "SELECT * FROM `web`.`用户表` where 用户名='" + context.Session["用户名"].ToString() + "'";
                                dTable = data.GetTable(sql);
                                root.Data = dTable.Rows[0]["工厂名称"].ToString();
                                root.Open = true;

                                sql = "SELECT * FROM `web`.`机组表` where `工厂名称`='" + root.Data + "'";
                                ////整理菜单
                                DataTable dTable3 = data.GetTable(sql);
                                for (int j = 0; j < dTable3.Rows.Count; j++)
                                {
                                    string jz = dTable3.Rows[j]["机组名称"].ToString();
                                    //不存在机组
                                    if (jz != "" && !Tree<string>.Exists(root, jz))
                                    {
                                        Tree<string> node3 = new Tree<string>();
                                        node3.Data = jz;
                                        root.AddNode(node3);
                                    }
                                }
                                break;
                            case "机组人员":
                                sql = "SELECT * FROM `web`.`机组表` where `负责人`='" + context.Session["用户名"].ToString() + "'";
                                ////整理菜单
                                dTable = data.GetTable(sql);
                                if (dTable.Rows.Count > 0)
                                {
                                    root.Data = "机组列表";
                                    for (int i = 0; i < dTable.Rows.Count; i++)
                                    {
                                        Tree<string> node1 = new Tree<string>();
                                        node1.Data = dTable.Rows[i]["机组名称"].ToString();
                                        root.AddNode(node1);
                                    }
                                }
                                break;
                            case "专家":
                                root.Data = "信息列表";
                                root.Open = true;
                                Tree<string> zjnode = new Tree<string>();
                                zjnode.Data = "机组列表";
                                root.AddNode(zjnode);
                                Tree<string> wdnode = new Tree<string>();
                                wdnode.Data = "文档管理";
                                root.AddNode(wdnode);
                                //查找该专家所有未完成咨询，包括未处理和正在处理的咨询。
                                sql = "SELECT 机组编号 FROM `web`.`咨询专家表` where `专家名`='" + context.Session["用户名"].ToString() + "' and 状态<2";
                                ////整理菜单
                                dTable = data.GetTable(sql);
                                if (dTable.Rows.Count > 0)
                                {
                                    for (int i = 0; i < dTable.Rows.Count; i++)
                                    {
                                        string jzid = dTable.Rows[i]["机组编号"].ToString();
                                        Tree<string> node1 = new Tree<string>();
                                        node1.URL = "/ZX/Index.html";//URL

                                        //获取机组名称
                                        sql = "SELECT 机组名称 FROM `web`.`机组表` where 机组编号='" + jzid + "'";
                                        node1.Data = (string)data.GetValue(sql);
                                        zjnode.AddNode(node1);
                                    }
                                }
                                break;
                            default:
                                break;
                        }

                        //遍历树，设置URL和是否打开标志,还可以设置icon标示。从根菜单开始
                        Tree<string>.SetMenuUrl(root, menuHashT);//改成无返回值的函数。

                        // result = "[" + Tree<string>.ToJson(root) + "]";
                        result = string.Format("[{0}]", Tree<string>.ToJson(root));
                      //  context.Session["目录"] = result;//将结构树目录保存到session中 不用保存
                    }
                    catch (Exception ex)
                    {
                        context.Response.ContentType = "text/plain";
                        context.Response.Write(ex.Message);
                    }
                }
                //else
                //{
                //    result = context.Session["目录"].ToString();
                //}
                context.Response.ContentType = "text/plain";
                context.Response.Write(result);
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.Message);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
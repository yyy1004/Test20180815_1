using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model;
using Common;
using System.Data;
using DataService;
using Newtonsoft.Json;
using System.Web.UI;
using System.Web.SessionState;
using System.Web.Script.Serialization;


namespace Web.User
{
    /// <summary>
    /// UserHandler 的摘要说明
    /// </summary>
    public class UserHandler : IHttpHandler, IRequiresSessionState
    {
        private static DataAccess data = new DataAccess();

        public void ProcessRequest(HttpContext context)
        {
            // 如果表单是以 POST 方式提交的，则服务器端必须以 context.Request.Form[name] 或者 context.Request[name] 来获取；  
            // 表单元素必须要有 name 属性，因为Form[] 中的索引就是 name 属性的值；  
            // 注意： ajax提交请求时，需要设置data属性，后台才能够接收到表单中的数据，
            // 例如 data: $('#ff').serialize() 代表将form表单中的数据序列化传输。
            //string userName = context.Request.Form["txt"];  
            //string userPwd = context.Request.Form["pwd"];  

            // 如果表单元素以 GET 方式提交，则服务器端必须以 Request.QueryString[] 来获取，索引仍是name属性的值。  
            // 以 GET 方式提交的时候，会在浏览器的地址栏显示提交的内容。  
            //string userName = context.Request.QueryString["txt"];
            //string userPwd = context.Request.QueryString["pwd"];  

            UserInfo user1 = new UserInfo();
            ResultInfo result = new ResultInfo();
            string action = context.Request["action"].ToString();
            context.Response.ContentType = "text/plain";

            switch (action)
            {
                case "Add":
                    try
                    {
                        user1.Id = context.Request.Form["Id"];
                        user1.Name = context.Request.Form["Name"];
                        user1.Password = context.Request.Form["Password"];
                        user1.Email = context.Request.Form["Email"];
                        user1.Phonenumber = context.Request.Form["Phonenumber"];
                        user1.JT = context.Request.Form["JT"];
                        user1.GC = context.Request.Form["GC"];
                        user1.Role = context.Request.Form["Role"];
                        result.Success = Add(user1);
                        result.Message = "添加用户信息" + ((result.Success == true) ? "成功" : "失败") + "!";
                    }
                    catch (Exception ex)
                    {
                        result.Success = false;
                        result.Message = "异常:" + ex.Message;
                    }
                    string jss = JsonHelper<ResultInfo>.ObjectToJsonString(result);
                    context.Response.Write(jss);
                    context.Response.End();
                    break;
                case "GetAll":
                    int page = int.Parse(context.Request.Form["page"]);
                    int rows = int.Parse(context.Request.Form["rows"]);
                    context.Response.Write(GetAll(page, rows));
                    break;
                case "GetAllUName":
                    context.Response.Write(GetAllUName());
                    break;
                case "GetAllJZBossName":
                    context.Response.Write(GetAllJZBossName());
                    break;
                case "GetAllZJUName":
                    context.Response.Write(GetAllZJUName());
                    break;
                case "GetUser":
                    string name1 = context.Request["Name"];
                    context.Response.Write(GetUser(name1));
                    break;
                case "Update":
                    try
                    {
                        user1 = new UserInfo();//这里还要声明吗
                        user1.Id = context.Request["Id"];
                        user1.Name = context.Request.Form["Name"];
                        user1.Password = context.Request.Form["Password"];
                        user1.Email = context.Request.Form["Email"];
                        user1.Phonenumber = context.Request.Form["Phonenumber"];
                        user1.JT = context.Request.Form["JT"];
                        user1.GC = context.Request.Form["GC"];
                        user1.Role = context.Request.Form["Role"];
                        user1.Note = context.Request.Form["Note"];
                        result.Success = Update(user1);
                        result.Message = "更新用户信息" + ((result.Success == true) ? "成功" : "失败") + "!";
                    }
                    catch (Exception ex)
                    {
                        result.Success = false;
                        result.Message = "异常:" + ex.Message;
                    }
                    jss = JsonHelper<ResultInfo>.ObjectToJsonString(result);
                    context.Response.Write(jss);//"{\"Success\":\"false\",\"Message\":\"更新用户信息失败!\"}"
                    break;
                case "Delete":
                    try
                    {
                        string id = context.Request["Id"];
                        string[] ids = id.Split(',');
                        for (int i = 0; i < ids.Length; i++)
                        {
                            result.Success = Delete(ids[i]);
                            if (result.Success == false)
                            {
                                break;
                            }
                        }
                        result.Message = "删除" + ids.Length + "条用户信息" + ((result.Success == true) ? "成功" : "失败") + "!";
                    }
                    catch (Exception ex)
                    {
                        result.Success = false;
                        result.Message = "异常:" + ex.Message;
                    }
                    jss = JsonHelper<ResultInfo>.ObjectToJsonString(result);
                    context.Response.Write(jss);
                    context.Response.End();
                    break;
                case "UpdatePwd":
                    try
                    {
                        string userpwd = context.Request.Form["oldpwd"];
                        string newpwd = context.Request.Form["newpwd"];
                        string rnewpwd = context.Request.Form["rnewpwd"];
                        string uid = context.Session["id"].ToString();
                        string uname = context.Session["用户名"].ToString();
                        if (newpwd != rnewpwd)//输入的新密码两次不一致
                        {
                            result.Success = false;
                            result.Message = "输入密码两次不一次，请重新输入！";
                        }
                        else if (newpwd == userpwd)//两次密码相同
                        {
                            result.Success = false;
                            result.Message = "输入新密码和原密码相同，请重新输入！";
                        }
                        else if (uid != "")//Session失效
                        {
                            string userinfo = GetUser(uname);
                            string databasepwd = JsonConvert.DeserializeObject<IDictionary<string, object>>(userinfo)["Password"].ToString();

                            if (databasepwd == userpwd)
                            {
                                user1.Password = newpwd;
                                user1.Id = uid;
                                result.Success = UpdatePwd(user1);
                                result.Message = string.Format("修改密码{0}！", (result.Success == true) ? "成功" : "失败");
                            }
                            else
                            {
                                result.Success = false;
                                result.Message = "输入的原密码不正确！";
                           
                            }
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = "登陆时间过长，请重新登陆！";
                        }
                    }
                    catch (Exception ex)
                    {
                        result.Success = false;
                        result.Message = "异常:" + ex.Message;
                    }
                    jss = JsonHelper<ResultInfo>.ObjectToJsonString(result);
                    context.Response.Write(jss);
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// 保存用户的信息
        /// </summary>
        /// <param name="user"></param>
        public bool Add(UserInfo user)
        {
            string sql = "INSERT INTO `web`.`用户表`(`用户编号`, `用户名`, `密码`, `电子邮箱`, `电话号码`, `角色名称`, `集团名称`, `工厂名称`, `备注`) VALUES (" +
                user.Id + ", '" +
                user.Name + "', '" +
                 user.Password + "', '" +
                 user.Email + "', '" +
                 user.Phonenumber + "', '" +
                 user.Role + "', '" +
                 user.JT + "', '" +
                 user.GC + "', '" +
                 user.Note + "')";
            return data.ExecSql(sql);
        }

        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns>用户信息列表的json字符串形式</returns>
        public string GetAll(int page, int rows)
        {
            int start = (page - 1) * rows;
            int end = page * rows;
            List<UserInfo> users = new List<UserInfo>();
            string sql = "SELECT * FROM `web`.`用户表`";
            string where = "limit " + start + "," + end;
            Dictionary<string, object> d = new Dictionary<string, object>();
            //获取数据总数(注意是总数,不是一页中数据的条数)
            DataTable dTable = data.GetTable(sql);
            d.Add("total", data.GetTable(sql).Rows.Count);
            //获取page页的数据
            dTable = dTable.AsEnumerable().Skip((page - 1) * rows).Take(rows).CopyToDataTable();
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                UserInfo user = new UserInfo();
                user.Id = dTable.Rows[i]["用户编号"].ToString();
                user.Name = dTable.Rows[i]["用户名"].ToString();
                user.Password = dTable.Rows[i]["密码"].ToString();
                user.Email = dTable.Rows[i]["电子邮箱"].ToString();
                user.Phonenumber = dTable.Rows[i]["电话号码"].ToString();
                user.Role = dTable.Rows[i]["角色名称"].ToString();
                user.JT = dTable.Rows[i]["集团名称"].ToString();
                user.GC = dTable.Rows[i]["工厂名称"].ToString();
                user.Note = dTable.Rows[i]["备注"].ToString();

                users.Add(user);
            }
            d.Add("rows", users);

            return JsonConvert.SerializeObject(d);
        }

        /// <summary>
        /// 获取所有用户名【目录】
        /// </summary>
        /// <returns>用户信息列表的json字符串形式</returns>
        public string GetAllUName()
        {
            SelectorHelper shelper = new SelectorHelper();

            string sql = "SELECT * FROM `web`.`用户表`";
            DataTable dTable = data.GetTable(sql);
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                string name = dTable.Rows[i]["用户名"].ToString();
                if (i == 0)
                {
                    shelper.AddItem(name, name, true);
                }
                else
                {
                    shelper.AddItem(name, name, false);
                }
            }
            return shelper.ToString();
        }

        /// <summary>
        /// 获取所有用户名【目录】
        /// </summary>
        /// <returns>用户信息列表的json字符串形式</returns>
        public string GetAllJZBossName()
        {
            SelectorHelper shelper = new SelectorHelper();

            string sql = "SELECT * FROM `web`.`用户表` where 角色名称='机组人员'";
            DataTable dTable = data.GetTable(sql);
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                string name = dTable.Rows[i]["用户名"].ToString();
                if (i == 0)
                {
                    shelper.AddItem(name, name, true);
                }
                else
                {
                    shelper.AddItem(name, name, false);
                }
            }
            return shelper.ToString();
        }

        /// <summary>
        /// 获取所有专家名【目录】
        /// </summary>
        /// <returns>用户信息列表的json字符串形式</returns>
        public string GetAllZJUName()
        {
            SelectorHelper shelper = new SelectorHelper();

            string sql = "SELECT * FROM `web`.`用户表` where 角色名称='专家'";
            DataTable dTable = data.GetTable(sql);
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                string name = dTable.Rows[i]["用户名"].ToString();
                if (i == 0)
                {
                    shelper.AddItem(name, name, true);
                }
                else
                {
                    shelper.AddItem(name, name, false);
                }
            }
            return shelper.ToString();
        }

        /// <summary>
        /// 获取指定的用户信息
        /// </summary>
        /// <param name="uname"></param>
        /// <returns></returns>
        public static string GetUser(string uname)
        {
            string sql = "SELECT * FROM `web`.`用户表` where 用户名='" + uname + "'";

            DataTable dTable = data.GetTable(sql);
            UserInfo user = new UserInfo();
            user.Id = dTable.Rows[0]["用户编号"].ToString();
            user.Name = dTable.Rows[0]["用户名"].ToString();
            user.Password = dTable.Rows[0]["密码"].ToString();
            user.Email = dTable.Rows[0]["电子邮箱"].ToString();
            user.Phonenumber = dTable.Rows[0]["电话号码"].ToString();
            user.Role = dTable.Rows[0]["角色名称"].ToString();
            user.JT = dTable.Rows[0]["集团名称"].ToString();
            user.GC = dTable.Rows[0]["工厂名称"].ToString();
            user.Note = dTable.Rows[0]["备注"].ToString();

            return JsonHelper<UserInfo>.ObjectToJsonString(user);
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>更新是否成功</returns>
        public bool Delete(string id)
        {
            string sql = "Delete from `web`.`用户表` WHERE `用户编号` = " + id;
            return data.ExecSql(sql);
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>更新是否成功</returns>
        public bool Update(UserInfo user)
        {
            string sql = "Update `web`.`用户表` SET " +
                "`用户名` = '" + user.Name + "'," +
                "`密码` = '" + user.Password + "'," +
                "`电子邮箱` = '" + user.Email + "'," +
                "`电话号码` = '" + user.Phonenumber + "'," +
                "`角色名称` = '" + user.Role + "'," +
                "`集团名称` = '" + user.JT + "'," +
                "`工厂名称` = '" + user.GC + "'," +
                "`备注` = '" + user.Note + "'  WHERE `用户编号` = " + user.Id;
            return data.ExecSql(sql);
        }

        public bool UpdatePwd(UserInfo user)
        {
            string sql = string.Format(@"UPDATE `web`.`用户表`
                                    SET `密码` = '{0}'
                                    WHERE
	                                    `用户编号` = {1}", user.Password, user.Id);
            return data.ExecSql(sql);
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
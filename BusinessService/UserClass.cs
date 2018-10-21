using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Jin.DataService;
using System.Data.OleDb;



namespace Jin.BusinessService

{
    public class UserClass
    {
        public static string license (string cpu,string disk)
        {
            string cpur = cpu.Substring(1,5);
            string diskr = disk.Substring(1, 5);
            string license = string.Concat(cpur, diskr);
            string code="";
            foreach (char c in license)
            {
                code += Convert.ToChar((int)c + 1);
            }


            return code;
        }


        DataService.DataService dService = new DataService.DataService();

        #region 用户登陆验证

        /// <summary>
        /// 判断用户是否是第一次登陆该系统
        /// 
        /// 如果是则记下该用户的MAC地址
        /// 如果不是则进行MAC地址的验证
        /// </summary>
        /// <param name="szUserCode"></param>
        /// <param name="szUserPWD"></param>
        /// <param name="szUserMac"></param>
        /// <param name="szLoginInfo"></param>
        /// <returns></returns>
        public static bool LoginProjectDB(string szUserCode, string szUserPWD)
        {

            string strSql = string.Format("Select password From user Where username='{0}'", szUserCode);

            DataService.DataService dCurService = new DataService.DataService();

            DataTable dCurUserTable = dCurService.GetOleTable(strSql);

            string szPwd = "";
            if (dCurUserTable.Rows.Count > 0)
            {
                //if( UserIsDeleted(szUserCode) )
                //{
                //    szLoginInfo = "该帐号已经被删除，请联系管理员重新建立该帐号" ;
                //    return false;
                //}
                //if( UserIsLocked(szUserCode))
                //{
                //    szLoginInfo = "该帐号已经被锁定，请联系管理员为该帐号解除锁定" ;
                //    return false;
                //}
                //szMac = dCurUserTable.Rows[0]["Mac"].ToString();
                //if( szMac.Length == 0 )
                //{
                //    strSql = string.Format("Update SysUser Set  Mac='{0}' Where UserCode='{1}'",szUserMac,szUserCode);

                //    if( dCurService.ExecOleSql(strSql))
                //    {
                //        szLoginInfo = "欢迎您首次登陆该系统," ;
                //    }
                //}
                //else
                //{
                //    szNoVerify = dCurUserTable.Rows[0]["NoMacVerify"].ToString().Trim();
                //    if( szNoVerify != "1")
                //    {
                //        if( szMac != szUserMac )
                //        {
                //            szLoginInfo += "该MAC地址不符合原始登陆记载,请联系管理确认清除该地址" ;
                //            return false;
                //        }
                //    }
                //}
                szPwd = dCurUserTable.Rows[0]["password"].ToString();
                if (szPwd != szUserPWD)
                {
                    return false;
                }

            }
            else
            {

                return false;
            }

            //return LogAdminService.AddLogUserLogin(szUserCode,IP,szUserMac);
            return true;
        }
        #endregion

        #region 用户相关基本信息获取

       /// <summary>
       /// 读取用户信息
       /// </summary>
       /// <param name="?"></param>
       /// <returns></returns>
        public static DataTable getCurUserInfo(string UserName)
        {
            DataService.DataService dCurService = new Jin.DataService.DataService();

            string strSql = "select * FROM USER_infor where user_name='"+UserName+"'";

            return dCurService.GetOleTable(strSql);
        
        }

        /// <summary>
        /// 注册新的用户
        /// </summary>
        /// <param name="szUserCode">用户代码</param>
        /// <param name="szUserName">用户姓名</param>
        /// <param name="szCreator">创建者</param>
        /// <param name="szDepID">归属部门</param>
        /// <param name="szDescription">用户描述</param>
        /// <param name="szInitMap">起始地图</param>
        /// <returns></returns>
        public static bool RegisterUser( long szUserCode, string szUserName, string szPWD, string szPhone, string szEmail, string szUserDep, string szUserDesc)
        {
            string strSql = string.Format("insert into USER_INFOR (USER_ID,USER_NAME,PAS,PHONE_NUMBER,EMAIL,GROUP_NAME,BZ) values({0},'{1}','{2}','{3}','{4}','{5}','{6}')", szUserCode, szUserName, szPWD, szPhone, szEmail, szUserDep, szUserDesc);

            DataService.DataService dCurService = new DataService.DataService();

            bool bSuced = false;
            bSuced = dCurService.ExecOleSql(strSql);

            return bSuced;

        }
     
        /// <summary>
        /// 管理员更新用户信息
        /// 如果部门信息不是临时部门的话就将用户解除锁定
        /// 如果部门信息是临时部门的话将用户锁定
        /// </summary>
        /// <param name="szUserName"></param>
        /// <param name="szPassword"></param>
        /// <param name="szDescription"></param>
        /// <param name="szCreator"></param>
        /// <param name="szCreateDate"></param>
        /// <param name="szMAC"></param>
        /// <param name="szDepment"></param>
        /// <param name="szIninNavMap"></param>
        /// <returns></returns>
        public static bool UpdateUserInfo(string szUserName, string szPWD, string szPhone, string szEmail, string szUserDep, string szUserDesc,  long szUserCode)
        {

            string strSql = string.Format("Update  user_infor Set user_NAME='{0}',PAS='{1}',PHONE_NUMBER='{2}',EMAIL='{3}',group_name='{4}',bz='{5}'  Where USER_id={6}", szUserName, szPWD, szPhone, szEmail, szUserDep, szUserDesc, szUserCode);

            DataService.DataService dCurService = new DataService.DataService();

            bool bSuced = false;

            bSuced = dCurService.ExecOleSql(strSql);

    

            return bSuced;
        }


        /// <summary>
        /// 授权用户角色
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static bool GrantUserRole(long uid,string UserName,long RoleId)
        {
            DataService.DataService dCurService = new Jin.DataService.DataService();

            string strSql = string.Format("insert into user_role (user_id,username,ROLE_ID) values ({0},'{1}',{2})", uid,UserName, RoleId);

            bool bSuced = false;
            bSuced = dCurService.ExecOleSql(strSql);
            return bSuced;
        }
        public static bool UpdateUserrole( long szUserCode, string szUserName,  long RoleId)
        {
            DataService.DataService dCurService = new Jin.DataService.DataService();

            string strSql = string.Format("Update  user_role set username='{1}',ROLE_ID={2} where user_id={0}", szUserCode, szUserName, RoleId);

            bool bSuced = false;
            bSuced = dCurService.ExecOleSql(strSql);
            return bSuced;
        
        }

        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="szUserCode"></param>
        /// <returns></returns>
        public static bool IsUserExist( long szUserCode, string username)
        {
            string strSql = "Select Count(*) From user_infor Where USER_Name='" + username + "'or user_id=" + szUserCode + "";
            object oNum = null;
            DataService.DataService dCurService = new DataService.DataService();

            oNum = dCurService.GetValue(strSql);

            if (oNum != null)
            {
                if (Convert.ToInt32(oNum) < 1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除用户
        /// 需要继续做修改的地方
        /// 1、模块权限表
        /// 2、角色用户表
        /// </summary>
        /// <param name="szUserCode"></param>
        /// <returns></returns>
        public static bool DeleteUser(string szUserCode, out string strErrInfo)
        {
            strErrInfo = "";

                strErrInfo = "已成功将该用户删除";

                string strSql = string.Format("Delete From user_infor  Where User_Name='{0}'", szUserCode);

                DataService.DataService dCurService = new DataService.DataService();

                bool bSuced = false;
                bSuced = dCurService.ExecOleSql(strSql);
                strSql = string.Format("Delete From user_role  Where UserName='{0}'", szUserCode);
                bSuced = dCurService.ExecOleSql(strSql);
                return bSuced;
            

        }
        /// <summary>
        /// 得到当前用户表所有用户信息
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static DataTable getUsersInfo()
        {
            DataService.DataService dCurService = new Jin.DataService.DataService();

            string strSql = "SELECT USER_INFOR.*, USER_ROLE.ROLE_ID, ROLE.ROLE_NAME FROM (USER_INFOR LEFT JOIN USER_ROLE ON USER_INFOR.USER_ID = USER_ROLE.USER_ID) LEFT JOIN ROLE ON USER_ROLE.ROLE_ID = ROLE.ROLE_ID ";

            return dCurService.GetOleTable(strSql);
        }
        /// <summary>
        /// 得到当前用户表姓名或用户名相同的记录表
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static DataTable getUsersInfo(string szFilter)
        {
            DataService.DataService dCurService = new Jin.DataService.DataService();

            string strSql = "SELECT USER_INFOR.*, USER_ROLE.ROLE_ID, ROLE.ROLE_NAME FROM (USER_INFOR LEFT JOIN USER_ROLE ON USER_INFOR.USER_ID = USER_ROLE.USER_ID) LEFT JOIN ROLE ON USER_ROLE.ROLE_ID = ROLE.ROLE_ID  WHERE (" + szFilter + ") ";

            return dCurService.GetOleTable(strSql);
        }



        /// <summary>
        /// 得到用户的角色
        /// </summary>
        /// <param name="szUserCode"></param>
        /// <returns></returns>
        public static string RoleId(string szUserCode)
        {
            string strSql = "Select ROLE_ID From user_role Where username='" + szUserCode + "'";

            object oNum = null;
            DataService.DataService dCurService = new DataService.DataService();

            oNum = dCurService.GetValue(strSql);

            if (oNum != null)
            {
                return oNum.ToString();
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 得到角色信息
        /// </summary>
        /// <param name="szUserCode"></param>
        /// <returns></returns>
        public static DataTable GetRole()
        {
            string strSql = "Select ROLE_ID,role_name From role";

            DataService.DataService dCurService = new DataService.DataService();

            return dCurService.GetOleTable(strSql);
        }
        public static DataSet GetRoleinfor()
        {
            string strSql = "Select ROLE_ID,role_name From role";

            DataService.DataService dCurService = new DataService.DataService();

            return dCurService.GetOleDataset (strSql);
        }
       
        /// <summary>
        /// 查询菜单树
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public static DataSet GetMenu(long RoleId)
        {
            string sql = "SELECT a.ROLE_ID,a.Description, c.* FROM role a ,ROLE_MENU b,menu c WHERE a.ROLE_ID = " + RoleId + "and a.ROLE_ID = b.ROLE_ID and b.NODEID = c.NODEID Order By PARENTID,c.ORDERID";
            DataService.DataService dService = new Jin.DataService.DataService();
            return dService.GetOleDataset(sql);
        }



        #endregion


        #region 角色相关信息
        /// <summary>
        /// 得到角色信息
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static DataTable getDtRoleInfo()
        {
            DataService.DataService dCurService = new Jin.DataService.DataService();

            string strSql = string.Format("select ROLE_ID,role_name from role order by ROLE_ID");

            return dCurService.GetOleTable(strSql);
        }

        /// <summary>
        /// 得到角色信息
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static DataTable getDtRoleInfo( long roleid)
        {
            DataService.DataService dCurService = new Jin.DataService.DataService();

            string strSql = string.Format("select ROLE_ID,role_name from role where ROLE_ID=" + roleid);

            return dCurService.GetOleTable(strSql);
        }

        /// <summary>
        /// 判断角色名是否存在
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public static bool IsRolename(string roleName)
        { 

                      bool bSeced = false;
           long  count =0;
                      string strSql = string.Format("Select count(*) From role Where role_Name='{0}'", roleName);

          Jin.DataService.DataService dService = new Jin.DataService.DataService();

          object obj = dService.GetValue(strSql.ToString());
          if (obj != null && !string.IsNullOrEmpty(obj.ToString().Trim()))
          {

              count = Convert.ToInt32(obj.ToString().Trim());
              if (count >= 1)
                  bSeced = true;
              else
                  bSeced = false;

          }

            return bSeced;
        }

        /// <summary>
        /// 判断父节点是否存在角色信息中
        /// </summary>
        /// <param name="Menuid"></param>
        /// <returns></returns>
        public static bool IsPARENTID( long Menuid, long roleid)
        {
            Jin.DataService.DataService dService = new Jin.DataService.DataService();
 
            bool bSeced = false;
           long PARENTID = Convert.ToInt32(Menuid / 10);
           long  count =0;
            string sql = " select count(*) FROM ROLE_MENU b,menu c WHERE b.ROLE_ID = " + roleid + "and b.NODEID = c.NODEID and PARENTID= " + PARENTID + " ";
            object obj = dService.GetValue(sql.ToString());
          if (obj != null && !string.IsNullOrEmpty(obj.ToString().Trim()))
          {
              count = Convert.ToInt32(obj.ToString().Trim());
              if (count >= 1)
                  bSeced = true;
              else
                  bSeced = false;
            }
          return bSeced;
        }
        /// <summary>
        /// 角色增加
        /// </summary>
        /// <param name="szUserCode"></param>
        /// <param name="szRole"></param>
        /// <returns></returns>
        public static bool AddRole(string roleName)
        {
            bool bSeced = false;
            string strSql = string.Format("Insert Into role(Role_name) Values('{0}')", roleName);

          Jin.DataService.DataService dService = new Jin.DataService.DataService();

            bSeced = dService.ExecOleSql(strSql);

            return bSeced;
        }
        /// <summary>
        /// 得到菜单信息
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static DataTable getMenuInfo()
        {
            DataService.DataService dCurService = new Jin.DataService.DataService();

            string strSql = "select NODEID,TEXT  from menu where TREELEVEL=2  ORDER BY NODEID";

            return dCurService.GetOleTable(strSql);
        }
        /// <summary>
        /// 得到角色菜单信息
        /// </summary>
        /// <returns></returns>
        public static DataTable getMenuInfo( long RoleId)
        {
            DataService.DataService dCurService = new Jin.DataService.DataService();
            string sql = "SELECT c.NODEID, c.text FROM ROLE_MENU b,menu c WHERE b.ROLE_ID = " + RoleId + "and b.NODEID = c.NODEID and c.TREELEVEL=2 Order By c.NODEID";
            return dCurService.GetOleTable(sql);
        }
        /// <summary>
        /// 授予用户选中的权限
        /// </summary>
        /// <param name="szUserCode"></param>
        /// <param name="szRole"></param>
        /// <returns></returns>
        public static bool AddRoleMenu( long RoleId,  long MenuId)
        {
            bool bSeced = false;
            string strSql = string.Format("Insert Into role_menu(ROLE_ID,NODEID) Values({0},{1})", RoleId, MenuId);

           Jin.DataService.DataService dService = new Jin.DataService.DataService();

            bSeced = dService.ExecOleSql(strSql);

            return bSeced;
        }
        /// <summary>
        /// 删除用户选中的权限
        /// </summary>
        /// <param name="szUserCode"></param>
        /// <param name="szRole"></param>
        /// <returns></returns>
        public static bool DelRoleMenu( long RoleId,  long MenuId)
        {
            bool bSeced = false;
            string strSql = "delete from ROLE_MENU where ROLE_ID =" + RoleId + " and NODEID =" + MenuId;

          Jin.DataService.DataService dService = new Jin.DataService.DataService();

            bSeced = dService.ExecOleSql(strSql);

            return bSeced;
        }
        #endregion

          #region 单位相关
        /// <summary>
        /// 得到单位列表
        /// </summary>
        /// <returns></returns>
        public static DataSet Getdepatmentinfor()
        {
            string strSql = "Select * From department";

            DataService.DataService dCurService = new DataService.DataService();

            return dCurService.GetOleDataset(strSql);
        }
        public static DataSet Getdepatmentinfor(string Filter)
        {
            string strSql = "Select * From department where (" + Filter + ") ";

            DataService.DataService dCurService = new DataService.DataService();

            return dCurService.GetOleDataset(strSql);
        }
/// <summary>
/// 删除单位信息
/// </summary>
/// <param name="DeleteBM"></param>
/// <returns></returns>
        public static bool DeleteDepart(string DeleteBM)
        {
            string strSql = string.Format("Delete From department  Where BM_Name='{0}'", DeleteBM);

            DataService.DataService dCurService = new DataService.DataService();

            bool bSuced = false;
            bSuced = dCurService.ExecOleSql(strSql);
            if (bSuced)
            {
                strSql = string.Format("update USER_INFOR set group_name=' '  Where group_name='{0}'", DeleteBM);
                bSuced = dCurService.ExecOleSql(strSql);
            }
            return bSuced;
        
        }

        /// <summary>
        /// 读取单位信息
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public static DataTable getBMInfo(string BM_Name)
        {
            DataService.DataService dCurService = new Jin.DataService.DataService();

            string strSql = "select * FROM department where bm_name='" + BM_Name + "'";

            return dCurService.GetOleTable(strSql);

        }
        /// <summary>
        /// 更新单位信息
        /// </summary>
        /// <param name="BM_fzr"></param>
        /// <param name="BM_tel"></param>
        /// <param name="BM_JS"></param>
        /// <param name="BM_Name"></param>
        /// <returns></returns>
        public static bool UpdateDEPInfo(string BM_fzr,string  BM_tel,string BM_JS, string BM_Name)
        {
        DataService.DataService dCurService = new Jin.DataService.DataService();

        string strSql = "update department set BM_fzr='" + BM_fzr + "', BM_tel='" + BM_tel + "', BM_JS='" + BM_JS  +"' where bm_name='" + BM_Name + "'";
        
         bool bSuced = false;
            bSuced = dCurService.ExecOleSql(strSql);
              return bSuced;
        }
     
            
            /// <summary>
        ///    添加单位信息
            /// </summary>
            /// <param name="BM_fzr"></param>
            /// <param name="BM_tel"></param>
            /// <param name="BM_JS"></param>
            /// <param name="BM_Name"></param>
            /// <returns></returns>

            public static bool AddDEPInfo(string BM_fzr,string  BM_tel,string BM_JS, string BM_Name)
        {
        DataService.DataService dCurService = new Jin.DataService.DataService();

        string strSql = "insert into department (bm_name,BM_fzr, BM_tel,BM_JS) values('" + BM_Name + "','" + BM_fzr + "','" + BM_tel + "', '" + BM_JS + "')";
        
         bool bSuced = false;
            bSuced = dCurService.ExecOleSql(strSql);
              return bSuced;
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

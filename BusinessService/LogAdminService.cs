using System;
using System.Data;
using System.Diagnostics;
using JrscSoft.DataService;
using System.Data.OracleClient;
namespace JrscSoft.BusinessService
{
	/// <summary>
	/// 日志管理维护
	/// </summary>
	public class LogAdminService
	{
		public LogAdminService()
		{
		}

		#region 登陆日志、数据日志、模块日志和系统操作添加
		/// <summary>
		/// 记载用户使用数据日志
		/// </summary>
		/// <param name="szUserCode">用户代码</param>
		/// <param name="IP">用户ip地址</param>
		/// <param name="MAC">用户物理网卡地址</param>
		/// <param name="DATATYPE">数据类型</param>
		/// <param name="DATA">数据内容</param>
		/// <param name="OPERATION">0:浏览; 1:修改;2:下载;</param>
		/// <param name="SQL">操作语句</param>
		public static bool AddLogDataLog(string szUserCode,string IP,string MAC,string DATATYPE,string DATA,string OPERATION,string SQL)
		{
			string strSql = string.Format("Insert Into syslogdatalog(UserCode,IP,MAC,OPERATIONDATE,DATATYPE,DATA,OPERATION,SQL) values('{0}','{1}','{2}',sysdate,'{3}','{4}','{5}','{6}')",szUserCode,IP,MAC,DATATYPE,DATA,OPERATION,SQL);
			
			DataService.DataService dCurService = new DataService.DataService();

			return dCurService.ExecOraSql(strSql);

		}

		/// <summary>
		/// 记载用户登陆日志
		/// </summary>
		/// <param name="szUserCode">用户代码</param>
		/// <param name="IP">用户ip地址</param>
		/// <param name="MAC">用户物理网卡地址</param>
		public static bool AddLogUserLogin(string szUserCode,string IP,string MAC)
		{
			string strSql = string.Format("Insert Into sysloguserlogin(UserCode,IP,MAC,LOGINTIME) values('{0}','{1}','{2}',sysdate)",szUserCode,IP,MAC);

			DataService.DataService dCurService = new DataService.DataService();

            return dCurService.ExecOraSql(strSql);
		}

		/// <summary>
		/// 记载模块日志启动运行时间
		/// </summary>
		/// <param name="szUserCode">用户代码</param>
		/// <param name="IP">用户ip地址</param>
		/// <param name="MAC">用户物理网卡地址</param>
		public static bool AddSysLogModule(string szUserCode,string IP,string MAC,int ModuleID)
		{
			string strSql = string.Format("Insert Into SysLogModule(UserCode,IP,MAC,RUNTIME,MODULE) values('{0}','{1}','{2}',sysdate,{3})",szUserCode,IP,MAC,ModuleID);

			DataService.DataService dCurService = new DataService.DataService();

			return dCurService.ExecOraSql(strSql);
		}

		/// <summary>
		/// 记载模块日志启动运行时间
		/// </summary>
		/// <param name="szUserCode">用户代码</param>
		/// <param name="IP">用户ip地址</param>
		/// <param name="MAC">用户物理网卡地址</param>
		public static bool AddSysLogModule(string szUserCode,string IP,string MAC,string ModuleFunName)
		{
			string strSql = string.Format("Insert Into SysLogModule(UserCode,IP,MAC,RUNTIME,MODULE) values('{0}','{1}','{2}',sysdate,'{3}')",szUserCode,IP,MAC,ModuleFunName);

			DataService.DataService dCurService = new DataService.DataService();

			return dCurService.ExecOraSql(strSql);
		}

		/// <summary>
		/// 记载系统和数据库管理员系统管理日志
		/// </summary>
		/// <param name="szUserCode">用户代码</param>
		/// <param name="IP">用户ip地址</param>
		/// <param name="MAC">用户物理网卡地址</param>
		public static bool AddSysLogManagement(string szUserCode,string IP,string MAC,string OBJECT,string OPERATION,string SQL)
		{
			string strSql = string.Format("Insert Into SysLogManagement(UserCode,IP,MAC,OPERATIONDATE,OBJECT,OPERATION,SQL) values('{0}','{1}','{2}',sysdate,'{3}','{4}','{5}')",szUserCode,IP,MAC,OBJECT,OPERATION,SQL);

			DataService.DataService dCurService = new DataService.DataService();

			return dCurService.ExecOraSql(strSql);
		}
		#endregion 

		#region 用户登陆日志
		/// <summary>
		/// 全部得到用户登陆日志
		/// </summary>
		/// <returns></returns>
		public DataTable GetLogUserLogin()
		{
			string strSql = string.Format("select b.usercode,a.name as UserName,b.ip,b.mac,b.logintime from sysuser a,sysloguserlogin b where a.usercode=b.usercode");

			DataService.DataService dCurService = new DataService.DataService();
            
			DataTable dTable = dCurService.GetTable(strSql);
			return dTable ;
		}

		/// <summary>
		/// 得到用户登陆日志中MAC地址
		/// </summary>
		/// <returns></returns>
		public DataTable GetUserMacFromLoginLog()
		{
			string strSql = string.Format("select distinct mac From sysloguserlogin ") ;

			DataService.DataService dCurService = new DataService.DataService();
            
			DataTable dTable = dCurService.GetTable(strSql);
			return dTable ;
		}

		/// <summary>
		/// 得到用户登陆日志用户组
		/// </summary>
		/// <returns></returns>
		public DataTable GetUsersFromLoginLog()
		{
			string strSql = string.Format("select distinct b.usercode,a.name as UserName From sysuser a,sysloguserlogin b where a.usercode=b.usercode") ;

			DataService.DataService dCurService = new DataService.DataService();
            
			DataTable dTable = dCurService.GetTable(strSql);
			return dTable ;
		}

		/// <summary>
		/// 条件过滤得到用户登陆日志
		/// </summary>
		/// <returns></returns>
		public DataTable GetLogUserLogin(string szFilter)
		{
			string strSql = string.Format("select b.usercode,a.name as UserName,b.ip,b.mac,b.logintime from sysuser a,sysloguserlogin b where a.usercode=b.usercode") + " and  " + szFilter;

			DataService.DataService dCurService = new DataService.DataService();
            
			DataTable dTable = dCurService.GetTable(strSql);
			return dTable ;
		}
		#endregion

		#region 用户数据日志
		/// <summary>
		/// 得到用户数据日志用户组
		/// </summary>
		/// <returns></returns>
		public DataTable GetUsersFromDataLog()
		{
			string strSql = string.Format("select distinct b.usercode,a.name as UserName From sysuser a,syslogdatalog b where a.usercode=b.usercode") ;

			DataService.DataService dCurService = new DataService.DataService();
            
			DataTable dTable = dCurService.GetTable(strSql);
			return dTable ;
		}

		/// <summary>
		/// 得到用户数据日志MAC地址组
		/// </summary>
		/// <returns></returns>
		public DataTable GetUserMacFromDataLog()
		{
			string strSql = string.Format("select distinct mac From syslogdatalog ") ;

			DataService.DataService dCurService = new DataService.DataService();
            
			DataTable dTable = dCurService.GetTable(strSql);
			return dTable ;
		}

		/// <summary>
		/// 得到用户数据日志MAC地址组
		/// </summary>
		/// <returns></returns>
		public DataTable GetUserDataTypeFromDataLog()
		{
			string strSql = string.Format("select distinct DATATYPE From syslogdatalog ") ;

			DataService.DataService dCurService = new DataService.DataService();
            
			DataTable dTable = dCurService.GetTable(strSql);
			return dTable ;
		}

		/// <summary>
		/// 条件过滤得到用户数据使用日志
		/// </summary>
		/// <returns></returns>
		public DataTable GetLogDataLog(string szFilter)
		{
			string strSql = string.Format("select a.usercode,a.name as username,b.ip,b.mac,b.operationdate,b.datatype,b.data,b.operation,b.sql from sysuser a,syslogdatalog b where a.usercode=b.usercode")  + " and  " + szFilter;

			DataService.DataService dCurService = new DataService.DataService();
            
			DataTable dTable = dCurService.GetTable(strSql);
			return dTable ;
		}

		/// <summary>
		/// 得到用户数据使用日志
		/// </summary>
		/// <returns></returns>
		public DataTable GetLogDataLog()
		{
			string strSql = string.Format("select a.usercode,a.name as username,b.ip,b.mac,b.operationdate,b.datatype,b.data,b.operation,b.sql from sysuser a,syslogdatalog b where a.usercode=b.usercode");

			DataService.DataService dCurService = new DataService.DataService();
            
			DataTable dTable = dCurService.GetTable(strSql);
			return dTable ;
		}
		#endregion

		#region 登陆次数登记
		/// <summary>
		/// 用户登陆次数
		/// </summary>
		/// <param name="szUserCode"></param>
		/// <returns></returns>
		public int GetUserLoginCount(string szUserCode)
		{
			string strSql = string.Format("Select count(*) From sysloguserlogin Where UserCode='{0}'",szUserCode);

			DataService.DataService dCurService = new DataService.DataService();

			object o = dCurService.GetValue(strSql);
			if( o != null )
				return Convert.ToInt32(o);
			else
				return 0;

		}
		#endregion 
	}
}

using System;
using System.Data;
using System.Diagnostics;
using JrscSoft.DataService;
using System.Data.OracleClient;
namespace JrscSoft.BusinessService
{
	/// <summary>
	/// ��־����ά��
	/// </summary>
	public class LogAdminService
	{
		public LogAdminService()
		{
		}

		#region ��½��־��������־��ģ����־��ϵͳ�������
		/// <summary>
		/// �����û�ʹ��������־
		/// </summary>
		/// <param name="szUserCode">�û�����</param>
		/// <param name="IP">�û�ip��ַ</param>
		/// <param name="MAC">�û�����������ַ</param>
		/// <param name="DATATYPE">��������</param>
		/// <param name="DATA">��������</param>
		/// <param name="OPERATION">0:���; 1:�޸�;2:����;</param>
		/// <param name="SQL">�������</param>
		public static bool AddLogDataLog(string szUserCode,string IP,string MAC,string DATATYPE,string DATA,string OPERATION,string SQL)
		{
			string strSql = string.Format("Insert Into syslogdatalog(UserCode,IP,MAC,OPERATIONDATE,DATATYPE,DATA,OPERATION,SQL) values('{0}','{1}','{2}',sysdate,'{3}','{4}','{5}','{6}')",szUserCode,IP,MAC,DATATYPE,DATA,OPERATION,SQL);
			
			DataService.DataService dCurService = new DataService.DataService();

			return dCurService.ExecOraSql(strSql);

		}

		/// <summary>
		/// �����û���½��־
		/// </summary>
		/// <param name="szUserCode">�û�����</param>
		/// <param name="IP">�û�ip��ַ</param>
		/// <param name="MAC">�û�����������ַ</param>
		public static bool AddLogUserLogin(string szUserCode,string IP,string MAC)
		{
			string strSql = string.Format("Insert Into sysloguserlogin(UserCode,IP,MAC,LOGINTIME) values('{0}','{1}','{2}',sysdate)",szUserCode,IP,MAC);

			DataService.DataService dCurService = new DataService.DataService();

            return dCurService.ExecOraSql(strSql);
		}

		/// <summary>
		/// ����ģ����־��������ʱ��
		/// </summary>
		/// <param name="szUserCode">�û�����</param>
		/// <param name="IP">�û�ip��ַ</param>
		/// <param name="MAC">�û�����������ַ</param>
		public static bool AddSysLogModule(string szUserCode,string IP,string MAC,int ModuleID)
		{
			string strSql = string.Format("Insert Into SysLogModule(UserCode,IP,MAC,RUNTIME,MODULE) values('{0}','{1}','{2}',sysdate,{3})",szUserCode,IP,MAC,ModuleID);

			DataService.DataService dCurService = new DataService.DataService();

			return dCurService.ExecOraSql(strSql);
		}

		/// <summary>
		/// ����ģ����־��������ʱ��
		/// </summary>
		/// <param name="szUserCode">�û�����</param>
		/// <param name="IP">�û�ip��ַ</param>
		/// <param name="MAC">�û�����������ַ</param>
		public static bool AddSysLogModule(string szUserCode,string IP,string MAC,string ModuleFunName)
		{
			string strSql = string.Format("Insert Into SysLogModule(UserCode,IP,MAC,RUNTIME,MODULE) values('{0}','{1}','{2}',sysdate,'{3}')",szUserCode,IP,MAC,ModuleFunName);

			DataService.DataService dCurService = new DataService.DataService();

			return dCurService.ExecOraSql(strSql);
		}

		/// <summary>
		/// ����ϵͳ�����ݿ����Աϵͳ������־
		/// </summary>
		/// <param name="szUserCode">�û�����</param>
		/// <param name="IP">�û�ip��ַ</param>
		/// <param name="MAC">�û�����������ַ</param>
		public static bool AddSysLogManagement(string szUserCode,string IP,string MAC,string OBJECT,string OPERATION,string SQL)
		{
			string strSql = string.Format("Insert Into SysLogManagement(UserCode,IP,MAC,OPERATIONDATE,OBJECT,OPERATION,SQL) values('{0}','{1}','{2}',sysdate,'{3}','{4}','{5}')",szUserCode,IP,MAC,OBJECT,OPERATION,SQL);

			DataService.DataService dCurService = new DataService.DataService();

			return dCurService.ExecOraSql(strSql);
		}
		#endregion 

		#region �û���½��־
		/// <summary>
		/// ȫ���õ��û���½��־
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
		/// �õ��û���½��־��MAC��ַ
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
		/// �õ��û���½��־�û���
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
		/// �������˵õ��û���½��־
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

		#region �û�������־
		/// <summary>
		/// �õ��û�������־�û���
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
		/// �õ��û�������־MAC��ַ��
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
		/// �õ��û�������־MAC��ַ��
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
		/// �������˵õ��û�����ʹ����־
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
		/// �õ��û�����ʹ����־
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

		#region ��½�����Ǽ�
		/// <summary>
		/// �û���½����
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

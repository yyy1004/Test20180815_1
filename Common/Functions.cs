using System;
using System.Configuration;
using System.Collections.Specialized;
using System.Web;
using System.Web.SessionState;
using System.IO;
using System.Web.Mail;

//using Model;

namespace JrscSoft.Common
{
	/// <summary>
	/// ���ú��������ض���Ӧ�ó����޹أ�����������á�
	/// </summary>
	/// <remarks>gwd, 2004-04-12</remarks>
	public sealed class Functions
	{
	
		/// <summary>
		/// Since this class provides only static methods, make the default constructor private to prevent 
		/// instances from being created with "new Functions()"
		/// </summary>
		private Functions() {}

		public static void Debug(string msg)
		{
			System.Diagnostics.Debug.WriteLine(msg);
		}
    
		#region property
    
		public static string ApplicationDirectory
		{
			get 
			{
				return Functions.GetValidDirectoryName(HttpContext.Current.Request.ApplicationPath);
			}
		}
    
		#endregion propery

		#region ������Ч�Լ�������ת��

		/// <summary>
		/// ���һ���ַ��������Ƿ�Ϊnull����string.Empty.
		/// </summary>
		/// <remarks>gwd, 2004-04-12</remarks>
		/// <param name="str">Ҫ�����ַ���</param>
		/// <returns>
		/// true -- null or string.Empty
		/// false -- str.length > 0
		/// </returns>
		public static bool IsValidString(string str)
		{
			return !(str == null || str == string.Empty);
		}

		/// <summary>
		/// ����ַ����Ƿ�Ϊ�գ�������Чֵ��
		/// </summary>
		/// <param name="toCheck">������ַ���</param>
		/// <returns>�����ַ���</returns>
		public static string ParseStr(object toCheck, string defaultValue)
		{
			if(toCheck == null)
				return defaultValue;
			else
				return toCheck.ToString();
		}
		public static string ParseStr(object toCheck)
		{
			return Functions.ParseStr(toCheck, "");
		}
    
		/// <summary>
		/// ��stringת��Ϊint����������ʱ������ȱʡֵ��
		/// </summary>
		/// <param name="toParse">Ҫת��Ϊ���ֵ��ַ���</param>
		/// <param name="defaultValue">ȱʡ��ֵ</param>
		/// <returns>����</returns>
		public static int ParseInt(string toParse, int defaultValue)
		{
			try
			{
				return int.Parse(toParse);
			}
			catch
			{
				return defaultValue;
			}
		}
		public static int ParseInt(string toParse)
		{
			return Functions.ParseInt(toParse, 0);
		}

		/// <summary>
		/// ��stringת��Ϊfloat����������ʱ������ȱʡֵ��
		/// </summary>
		/// <param name="toParse">Ҫת��Ϊ���ֵ��ַ���</param>
		/// <param name="defaultValue">ȱʡ��ֵ</param>
		/// <returns>float</returns>
		public static float ParseFlt(string toParse, float defaultValue)
		{
			try
			{
				return float.Parse(toParse);
			}
			catch
			{
				return defaultValue;
			}
		}
		public static float ParseFlt(string toParse)
		{
			return Functions.ParseFlt(toParse, 0F);
		}

		/// <summary>
		/// ��stringת��Ϊdouble����������ʱ������ȱʡֵ��
		/// </summary>
		/// <param name="toParse">Ҫת��Ϊ���ֵ��ַ���</param>
		/// <param name="defaultValue">ȱʡ��ֵ</param>
		/// <returns>double</returns>
		public static double ParseDbl(string toParse, double defaultValue)
		{
			try
			{
				return double.Parse(toParse);
			}
			catch
			{
				return defaultValue;
			}
		}
		public static double ParseDbl(string toParse)
		{
			return Functions.ParseDbl(toParse, 0D);
		}

		/// <summary>
		/// ��stringת��ΪDateTime����������ʱ������ȱʡֵ��
		/// </summary>
		/// <param name="toParse"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		public static DateTime ParseDt(string toParse, DateTime defaultValue)
		{
			try
			{
				return DateTime.Parse(toParse);
			}
			catch
			{
				return defaultValue;
			}
		}
		/// <summary>
		/// ��stringת��ΪDateTime����������ʱ������DateTime("1900-1-1")��
		/// </summary>
		/// <param name="toParse"></param>
		/// <returns></returns>
		public static DateTime ParseDt(string toParse)
		{
			return Functions.ParseDt(toParse, DateTime.Parse("1900-1-1"));
		}

		/// <summary>
		/// ��bool.TrueString��bool.FalseStringת��Ϊbool������ִ��int.Parse(toParse)>0?true:false����������ʱ������defaultValue��
		/// </summary>
		/// <param name="toParse"></param>
		/// <param name="defaultValue"></param>
		/// <returns></returns>
		public static bool ParseBln(string toParse, bool defaultValue)
		{
			if(toParse.Trim().ToLower()==bool.TrueString.ToLower() || toParse.Trim().ToLower()==bool.FalseString.ToLower())
				return bool.Parse(toParse);
			else
			{
				try
				{
					return (int.Parse(toParse)>0?true:false);
				}
				catch
				{
					return defaultValue;
				}
			}
		}
		public static bool ParseBln(string toParse)
		{
			return Functions.ParseBln(toParse, true);
		}

		#endregion

		/// <summary>
		/// �õ���Ч��·����
		/// </summary>
		/// <param name="directoryName">Ҫ����·����</param>
		/// <returns>��Ч��·����</returns>
		public static string GetValidDirectoryName(string directoryName)
		{
			if (directoryName.EndsWith("/"))
				return directoryName;
			else
				return directoryName + "/";
		}

		/// <summary>
		/// ���ı���Ŀ�ֵת�����㣬���������е�ֵ���ͼ���
		/// </summary>
		/// <param name="controlText">�ı�������</param>
		/// <param name="flag">����Ҫת����int���ͻ���decimal���ͣ�������������flag����1��ֵ��������ת����int</param>
		/// <returns></returns>
		public static int ConvertTextValue(string controlText,int flag)
		{
			try
			{
				if(controlText=="")
				{
					return 0;
				}
				else
				{
					return Convert.ToInt32(controlText);
				}
			}
			catch
			{
				return 0;
			}
		}

		/// <summary>
		/// ���ı���Ŀ�ֵת�����㣬���������е�ֵ���ͼ���
		/// </summary>
		/// <param name="controlText">�ı�������</param>
		/// <param name="flag">����Ҫת����int���ͻ���decimal���ͣ�������������flagֵ����1.1����decimal��ת����decimal</param>
		/// <returns></returns>
		public static decimal ConvertTextValue(string controlText,double flag)
		{
			try
			{
				if(controlText=="")
				{
					return 0;
				}
				else
				{
					return Convert.ToDecimal(controlText);
				}
			}
			catch
			{
				return 0;
			}
		}
    
    
		#region ��ȡ��appSettings�������
    
		/// <summary>
		/// ��ȡWeb.config�ļ��С�appSettings����stringֵ��
		/// </summary>
		/// <remarks>gwd, 2003-04-12</remarks>
		/// <param name="keyName">������</param>
		/// <param name="defaultValue">����Ĭ��ֵ</param>
		/// <returns>��ֵ</returns>
		public static string GetAppConfigString(string keyName, string defaultValue)
		{
			NameValueCollection appSettings = ConfigurationSettings.AppSettings;
			if (appSettings != null)
			{
				string keyValue = appSettings[keyName];
				if (Functions.IsValidString(keyValue))
					return keyValue;
			}
		  
			return defaultValue;
		}

		/// <summary>
		/// ��ȡWeb.config�ļ��С�appSettings����boolֵ��
		/// </summary>
		/// <remarks>gwd, 2003-04-12</remarks>
		/// <param name="keyName">������</param>
		/// <param name="defaultValue">����Ĭ��ֵ</param>
		/// <returns>��ֵ</returns>		
		public static bool GetAppConfigBool(string keyName, bool defaultValue)
		{
			NameValueCollection appSettings = ConfigurationSettings.AppSettings;
			if (appSettings != null)
			{
				try
				{
					bool keyValue = bool.Parse(appSettings[keyName]);
					return keyValue;
				}
				catch {}
			}
		  
			return defaultValue;
		}
		
		/// <summary>
		/// ��ȡWeb.config�ļ��С�appSettings����intֵ��
		/// </summary>
		/// <remarks>gwd, 2003-04-12</remarks>
		/// <param name="keyName">������</param>
		/// <param name="defaultValue">����Ĭ��ֵ</param>
		/// <returns>��ֵ</returns>
		public static int GetAppConfigInt(string keyName, int defaultValue)
		{
			NameValueCollection appSettings = ConfigurationSettings.AppSettings;
			if (appSettings != null)
			{
				try
				{
					int keyValue = int.Parse(appSettings[keyName]);
					return keyValue;
				}
				catch {}
			}
		  
			return defaultValue;
		}
    
		#endregion ��ȡ��appSettings�������
    
		#region ��ȡ�Զ��������

		/// <summary>
		/// ��ȡWeb.config�ļ����Զ������õ�stringֵ��
		/// </summary>
		/// <remarks>gwd, 2003-04-12</remarks>
		/// <param name="configName">����������</param>
		/// <param name="keyName">������</param>
		/// <param name="defaultValue">����Ĭ��ֵ</param>
		/// <returns>��ֵ</returns>    
		public static string GetConfigString(string configName, string keyName, string defaultValue)
		{
			NameValueCollection configSettings = ConfigurationSettings.GetConfig(configName) as NameValueCollection;
			if (configSettings != null)
			{
				string keyValue = configSettings[keyName];
				if (Functions.IsValidString(keyValue))
					return keyValue;
			}
      
			return defaultValue;
		}
    
		/// <summary>
		/// ��ȡWeb.config�ļ����Զ������õ�boolֵ��
		/// </summary>
		/// <remarks>gwd, 2003-04-12</remarks>
		/// <param name="configName">����������</param>
		/// <param name="keyName">������</param>
		/// <param name="defaultValue">����Ĭ��ֵ</param>
		/// <returns>��ֵ</returns> 
		public static bool GetConfigBool(string configName, string keyName, bool defaultValue)
		{
			NameValueCollection configSettings = ConfigurationSettings.GetConfig(configName) as NameValueCollection;
			if (configSettings != null)
			{
				try
				{
					bool keyValue = bool.Parse(configSettings[keyName]);
					return keyValue;
				}
				catch {}
			}
		  
			return defaultValue;
		}
		
		/// <summary>
		/// ��ȡWeb.config�ļ����Զ������õ�intֵ��
		/// </summary>
		/// <remarks>gwd, 2003-04-12</remarks>
		/// <param name="configName">����������</param>
		/// <param name="keyName">������</param>
		/// <param name="defaultValue">����Ĭ��ֵ</param>
		/// <returns>��ֵ</returns> 
		public static int GetConfigInt(string configName, string keyName, int defaultValue)
		{
			NameValueCollection configSettings = ConfigurationSettings.GetConfig(configName) as NameValueCollection;
			if (configSettings != null)
			{
				try
				{
					int keyValue = int.Parse(configSettings[keyName]);
					return keyValue;
				}
				catch {}
			}
		  
			return defaultValue;
		}
    
		#endregion ��ȡ�Զ��������

		#region ���������ֵĲ���������Ȩ�޲�����
		/// <summary>
		/// ��A���Ƿ����B
		/// ��ν����������
		/// A����1��λ��B�ж�Ӧ�Ŀ�����0��1
		/// A����0��λ��B�ж�Ӧ�ı�����0
		/// ע�������ж�Ȩ�޵İ�����ϵ
		/// </summary>
		/// <param name="A">A</param>
		/// <param name="B">B</param>
		/// <returns>������true����������false</returns>
		public static bool IsAContainB(int A,int B)
		{
			if ( A< B ) return false;
			for (int i=1;i<=A;i<<=1)
			{
				if( ((B&i)!=0) && ((A&i)==0) ) return false;
			}
			return true;
		}
		/// <summary>
		/// ������������λ���򡱵õ��µ�����
		/// ע�����ڽ��ϲ�Ȩ����Ͻ�ɫ
		/// </summary>
		/// <param name="A"></param>
		/// <param name="B"></param>
		/// <returns></returns>
		public static int GetTotalValue(int A, int B)
		{
			if( A < 0 || B < 0) return 0;
			return A|B;
		}
		/// <summary>
		/// ��A�а�λ��B��ȥ
		/// ��ν����ȥ����
		/// ���A����B�Ļ����ʹ�A�н�B�е�1ȫ����ȥ
		/// </summary>
		/// <param name="A">A</param>
		/// <param name="B">B</param>
		/// <returns></returns>
		public static int SeperateBFromA(int A,int B)
		{
			if (!IsAContainB(A,B)) return A;
			return A-B;
		}
		#endregion ���������ֵĲ���������Ȩ�޲�����


		#region ʱ������

		#region ������ͼ��ȵı�ŷ��ض�Ӧ���ȵ���ֹ����
		/// <summary>
		/// ������ͼ��ȵı�ŷ��ض�Ӧ���ȵ���ֹ����
		/// </summary>
		/// <param name="year">��</param>
		/// <param name="id">����</param>
		/// <param name="Date_s">��ʼʱ��</param>
		/// <param name="Date_e">����ʱ��</param>
		public static void GetJiduRangByID(int year,int id,out string Date_s,out string Date_e)
		{
			if (year==0)
			{
				Date_s="1900-1-1";
				Date_e="1900-1-1";
				return;
			}

			switch (id)
			{
				case 1:
				{
					Date_s=Convert.ToString(year)+"-1-1";
					Date_e=Convert.ToString(year)+"-4-1";
					break;
				}
				case 2:
				{
					Date_s=Convert.ToString(year)+"-4-1";
					Date_e=Convert.ToString(year)+"-7-1";
					break;
				}
				case 3:
				{
					Date_s=Convert.ToString(year)+"-7-1";
					Date_e=Convert.ToString(year)+"-10-1";
					break;
				}
				case 4:
				{
					Date_s=Convert.ToString(year)+"-10-1";
					Date_e=Convert.ToString(year+1)+"-1-1";
					break;					
				}
				default:
				{
					Date_s=Convert.ToString(year)+"-1-1";
					Date_e=Convert.ToString(year)+"-4-1";
					break;				
				}				
			}//switch			
			
			
		}
		
		/// <summary>
		/// ������ͼ��ȵı�ŷ��ض�Ӧ���ȵ���ֹ����
		/// </summary>
		/// <param name="year">��</param>
		/// <param name="id">����</param>
		/// <param name="Date_s">��ʼʱ��</param>
		/// <param name="Date_e">����ʱ��</param>
		public static void GetJiduRangByID(int year,int id,out DateTime Date_s,out DateTime Date_e)
		{
			string sDate_s,sDate_e;
			GetJiduRangByID(year,id,out sDate_s,out sDate_e);
			Date_s=Convert.ToDateTime(sDate_s);
			Date_e=Convert.ToDateTime(sDate_e);
		}
		#endregion ������ͼ��ȵı�ŷ��ض�Ӧ���ȵ���ֹ����

		public static int CompareTwoDate(DateTime d1, DateTime d2)
		{
			string s1 = d1.ToString("yyyy-MM-dd HH:mm:ss");
			string s2 = d2.ToString("yyyy-MM-dd HH:mm:ss");

			if(s1 == s2) return 0;

			s1 = s1.Replace("00:00:00", "23:59:59");

			return DateTime.Parse(s1).CompareTo(DateTime.Parse(s2));
		}



		#endregion ʱ������

		#region �������
		
		/// <summary>
		/// �й�����ת��׼������ʽ�ַ���
		/// </summary>
		/// <param name="s">�й�����ֵ�ַ���</param>
		/// <returns></returns>
		public static string ConvertToStandIntTypedString(string s)
		{
			if(s.Equals("")) return "0";
			string[] ss = s.TrimStart('��').Split(',');
			System.Collections.IEnumerator ie = ss.GetEnumerator();
			string res = "";
			while (ie.MoveNext())
			{
				res +=ie.Current.ToString();
			}
			return res;			
		}

		internal static string FormatRmb(double rmb, string format)
		{
			return rmb.ToString(format);
		}

		internal static string FormatRmb(float rmb, string format)
		{
			return rmb.ToString(format);
		}

		#endregion �������


		#region ���ݿ�����͵�ת��
		public static int CleanDBInt(object drvalue)
		{
			return drvalue==DBNull.Value?0:(Int32)drvalue;
		}
		public static string CleanDBString(object drvalue)
		{
			return drvalue==DBNull.Value?"":(String)drvalue;
		}
		public static double CleanDBDouble(object drvalue)
		{
			return drvalue==DBNull.Value?0:(Double)drvalue;
		}
		public static DateTime CleanDBDateTime(object drvalue)
		{
			return drvalue==DBNull.Value?Convert.ToDateTime("1900-1-1"):(DateTime)drvalue;
		}
		#endregion 


		public static string GetValidSQLString(string sql)
		{
			return sql.Replace("'","''");
		}





		//�����������
		public static string MakePassword( int pwdlen)
		{
			string randomchars = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			string pwdchars = randomchars;
			string tmpstr = "";
			int iRandNum;
			Random rnd = new Random();
			for(int i=0;i<pwdlen;i++)
			{
				iRandNum = rnd.Next(pwdchars.Length);
				tmpstr += pwdchars[iRandNum];
			}
			return tmpstr;

		}


		#region URL�������� Zlk @2008-3-30
		/// <summary>
		/// �õ�����������ַ���ֵ
		/// </summary>
		/// <param name="keyword">key</param>
		/// <returns></returns>
		public static string  GetQueryString(string keyword)
		{
			string value = HttpContext.Current.Request.QueryString[keyword];
			if (! IsValidString(value))
				return null;

			return value;
            
		}
		/// <summary>
		/// �õ������������������ֵ
		/// </summary>
		/// <param name="keyword">key</param>
		/// <returns></returns>
		public static int GetIntQueryString(string keyword)
		{
			string  value = GetQueryString(keyword);
			try
			{
				return int.Parse(value);
			}
			catch (Exception)
			{
				throw new Exception("����û��ֵ���߲�����ֵ����");
			}
		}
		/// <summary>
		/// ����û�в���ֵ������
		/// </summary>
		/// <param name="keys">����key����</param>
		public static void EndWithEmptyURLParms(params string[] keys)
		{
			foreach (string key in keys)
			{
				EndWithAEmptyURLParm(key);
			}
		}
		/// <summary>
		/// ����û�в���ֵ������
		/// </summary>
		/// <param name="key">����key</param>
		private static void EndWithAEmptyURLParm(string key)
		{
			string parmValue = HttpContext.Current.Request[key];
			if(! IsValidString(parmValue))
				HttpContext.Current.Response.End();
		}

		#endregion

		#region �ı��ļ�����

		public static void GenerateTxtFile(string content, string filename)
		{
			FileInfo  myfile = new  FileInfo(filename);
			if(myfile.Exists)//������ھ�ɾ��
			{
				if(myfile.Attributes.ToString().IndexOf("ReadOnly")!= -1  )
				{
					myfile.Attributes = FileAttributes.Normal;
				}
				File.Delete(filename) ;
				FileInfo mf = new  FileInfo(filename);
			}
			StreamWriter sw = new StreamWriter(filename );
			sw.WriteLine(content);  
				
			sw.Flush();
			sw.Close();				

		}

		#endregion

		#region ��ʽ������
		public static string FormatRmb(double rmb)
		{
			return Functions.FormatRmb(rmb, "F");
		}

		public static string FormatRmb(float rmb)
		{
			return Functions.FormatRmb(rmb, "F");
		}

		public static string FormatDate(DateTime date)
		{
			return date.ToString("yyyy-MM-dd");
		}

		public static string FormatDate(string date)
		{
			return Convert.ToDateTime(date).ToString("yyyy-MM-dd");
		}

		public static string FormatDateTime(DateTime date)
		{
			return date.ToString("yyyy-MM-dd HH:mm");
		}

		public static string FormatDateTime(string date)
		{
			return Convert.ToDateTime(date).ToString("yyyy-MM-dd HH:mm");
		}

		public static bool CheckTwoDate(System.Web.UI.WebControls.TextBox d1, System.Web.UI.WebControls.TextBox d2, string title1, string title2, PageBase page)
		{
			DateTime date1, date2;

			date1 = DateTime.Parse("1900-1-1");
			if(d1.Text.Trim().Length > 0)
			{
				date1 = Functions.ParseDt(d1.Text);
				if(date1.Year <= 1990)
				{
					page.GetAlertMsg(string.Format("����ȷ��д{0}������ĸ�ʽ:{1}��", title1, FormatDate(DateTime.Now)));
					page.SetFocus("", d1.ClientID);
					return false;
				}
			}
      
			if(d2.Text.Trim().Length > 0)
			{
				date2 = Functions.ParseDt(d2.Text);
				if(date2.Year <= 1990)
				{
					page.GetAlertMsg(string.Format("����ȷ��д{0}������ĸ�ʽ: {1}��", title2, FormatDate(DateTime.Now)));
					page.SetFocus("", d2.ClientID);
					return false;
				}
				else if(date2.CompareTo(date1) < 0)
				{
					page.GetAlertMsg(string.Format("{0}Ӧ����{1}֮��", title2, title1));
					page.SetFocus("", d2.ClientID);
					return false;
				}
			}

			return true;
		}
		#endregion

        /// <summary>
        /// �����ʼ�����
        /// </summary>
        /// <param name="SmtpServer">smtp������ ���磺"smtp.163.com"</param>
        /// <param name="PortNo">smtp�������˿ںţ� Ĭ�� 25</param>
        /// <param name="UserName">smtp�������û��� ���磺zhaoxianfa</param>
        /// <param name="PassWord">smtp�������û����룺 ���磺 xxxxx</param>
        /// <param name="Sender">�����������ַ ���磺zhaoxianfa@163.com</param>
        /// <param name="Receiver">�ռ��������ַ ���磺 xxx@163.com</param>
        /// <param name="Subject">�ʼ����� ���磺 ���¹����� </param>
        /// <param name="Content">�ʼ����� ���磺 ʵ������20000</param>
        /// <param name="FileList">�����ļ��б� ����Ϊ�ļ��ķ������������·��</param>
        /// <returns>�ɹ�����true�����򷵻�false</returns>
        public static bool SendMail(string SmtpServer, int PortNo, string UserName, string PassWord, string Sender, string Receiver, string Subject, string Content, System.Collections.Generic.List<string> FileList)
        {
            try
            {
                System.Net.Mail.MailMessage myMail = new System.Net.Mail.MailMessage();
                myMail = new System.Net.Mail.MailMessage(Sender, Receiver, Subject, Content);
                if (FileList != null)
                {
                    for (int i = 0; i < FileList.Count; i++)
                    {
                        string file = FileList[i].ToString();
                        System.Net.Mail.Attachment myAttachment = new System.Net.Mail.Attachment(file, System.Net.Mime.MediaTypeNames.Application.Octet);
                        System.Net.Mime.ContentDisposition disposition = myAttachment.ContentDisposition;
                        disposition.CreationDate = System.IO.File.GetCreationTime(file);
                        disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
                        disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
                        myMail.Attachments.Add(myAttachment);
                    }
                }
                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(SmtpServer, PortNo);
                client.Credentials = new System.Net.NetworkCredential(UserName, PassWord);

                client.Send(myMail);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}


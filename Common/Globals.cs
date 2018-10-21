using System;



namespace JrscSoft.Common
{
	/// <summary>
	/// ��Ӧ�ó�������Ĺ������������ԡ�
	/// </summary>
	/// <remarks>gwd, 2004-04-12</remarks>
	public sealed class Globals
	{
		/// <summary>
		/// Since this class provides only static methods, make the default constructor private to prevent 
		/// instances from being created with "new Globals()"
		/// </summary>
		private Globals() {}
		
		#region private functions
		
		#endregion private functions
		
		#region properties
		
		/// <summary>
		/// ���ݿ������ַ�����
		/// </summary>
		public static string ConnectionString
		{
			get { return Functions.GetAppConfigString("ConnectionString", string.Empty); }
		}
		/// <summary>
		/// Ӧ�ó�����
		/// </summary>
		public static string AppTitle
		{
			get { return Functions.GetAppConfigString("AppTitle", string.Empty); }
		}
		/// <summary>
		/// ��ʱ�ļ�·��
		/// </summary>
		public static string TempFile
		{
			get
			{
				return (Functions.GetValidDirectoryName(Functions.GetAppConfigString("TempFile","/"))).TrimEnd(new char[]{'/'});
			}
		}		





		/// <summary>
		/// �õ�����·������
		/// ��Ϊ��Ŀ¼������Ϊ��
		/// </summary>
		public static string AppVirtualDictionaryName
		{
			get{return Functions.GetValidDirectoryName(Functions.GetAppConfigString("AppVirtualDictionaryName", "")); }
		}
		/// <summary>
		/// Ӧ�ó����Ŀ¼
		/// </summary>
		public static string AppRoot
		{
			get 
			{
				/*
				string path = Functions.ApplicationDirectory;
				if (path == "/")
				  return Functions.GetValidDirectoryName(Functions.GetAppConfigString("AppRoot", "/"));
				else
				  return path;
				*/
        
				// ��ȫ�������ļ�����ϵͳ�ĸ�Ŀ¼��
				return Functions.GetValidDirectoryName(Functions.GetAppConfigString("AppRoot", "/"));
			}
		}
		/// <summary>
		/// Ƥ����ʽ��δ���ã�
		/// </summary>
		public static string Skin
		{
			get { return Functions.GetAppConfigString("Skin", "default"); }
		}
 
		#endregion properties


		
	}
}

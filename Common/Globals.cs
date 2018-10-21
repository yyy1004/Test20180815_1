using System;



namespace JrscSoft.Common
{
	/// <summary>
	/// 本应用程序所需的公共函数和属性。
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
		/// 数据库连接字符串。
		/// </summary>
		public static string ConnectionString
		{
			get { return Functions.GetAppConfigString("ConnectionString", string.Empty); }
		}
		/// <summary>
		/// 应用程序名
		/// </summary>
		public static string AppTitle
		{
			get { return Functions.GetAppConfigString("AppTitle", string.Empty); }
		}
		/// <summary>
		/// 临时文件路径
		/// </summary>
		public static string TempFile
		{
			get
			{
				return (Functions.GetValidDirectoryName(Functions.GetAppConfigString("TempFile","/"))).TrimEnd(new char[]{'/'});
			}
		}		





		/// <summary>
		/// 得到虚拟路径名称
		/// 若为跟目录则设置为空
		/// </summary>
		public static string AppVirtualDictionaryName
		{
			get{return Functions.GetValidDirectoryName(Functions.GetAppConfigString("AppVirtualDictionaryName", "")); }
		}
		/// <summary>
		/// 应用程序根目录
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
        
				// 完全由配置文件决定系统的根目录。
				return Functions.GetValidDirectoryName(Functions.GetAppConfigString("AppRoot", "/"));
			}
		}
		/// <summary>
		/// 皮肤样式（未采用）
		/// </summary>
		public static string Skin
		{
			get { return Functions.GetAppConfigString("Skin", "default"); }
		}
 
		#endregion properties


		
	}
}

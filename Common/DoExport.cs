using System;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Specialized;
using System.Text;

namespace JrscSoft.Common
{
	/// <summary>
	/// DoExport 的摘要说明。
	/// </summary>
	public class DoExport
	{
		public DoExport()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		private string configFileName;
		private string title;
		private StringCollection heads = new StringCollection();
		private string dirPrefix=@"../archives/export/";

		/// <summary>
		/// 相关的配置文件的文件名
		/// </summary>
		public string ConfigFileName
		{
			get
			{
				return configFileName;
			}
			set
			{
				configFileName=System.Web.HttpContext.Current.Server.MapPath(value);
			}
		}

		//解析配置文件，读出表头
		private void ParseHead()
		{
			XmlTextReader reader = new XmlTextReader(configFileName);
			try
			{
				// 将reader移动到File标签
				if(reader.MoveToContent() == XmlNodeType.Element && reader.LocalName.Trim().Equals("File"))
				{
					while(reader.Read())
					{
						if(reader.NodeType==XmlNodeType.EndElement && reader.LocalName.Trim()=="File")
							break;
                
						if(reader.NodeType != XmlNodeType.Element)
							continue;
                
						switch(reader.LocalName.Trim().ToLower())
						{
							case "title":
								this.title=reader.ReadString();
								break;
							case "column":
								string head = reader.GetAttribute("name");
								heads.Add(head);
								break;						
						}					
					}
				}
				else
				{					
					throw new ApplicationException(string.Format("配置文件（{0}）格式不正确！", this.configFileName));
				}
			}
			catch(Exception ex)
			{
				ex.ToString();
			}
			finally
			{
				if(reader!=null)
				{
					reader.Close();
				}
			}
		}

		/// <summary>
		/// 导出文件
		/// </summary>
		/// <param name="sql">查询内容用的sql语句</param>
		/// <param name="dir">导出时标识模块的中间层目录名</param>
		/// <param name="fileName">文件名</param>
		/// <returns>如果成功，返回文件的相对路径；否则返回空字符串</returns>
		public string Export(string sql,string dir,string fileName)
		{
			if(!fileName.ToLower().EndsWith(".csv"))
			{
				fileName=string.Format("{0}.csv",fileName);
			}
            
			string virPath=string.Format(@"{0}{1}/{2}",this.dirPrefix,dir,fileName);
			string fullPath = System.Web.HttpContext.Current.Server.MapPath(virPath);

			string fullDir=Path.GetDirectoryName(fullPath);
			
			//创建目录
			if(!Directory.Exists(fullDir))
			{
				Directory.CreateDirectory(fullDir);
			}

			//创建文件
			if(File.Exists(fullPath))
			{
				File.Delete(fullPath);
			}

			StreamWriter sw =null;

			try
			{

				//声明文件流
				FileStream flStream = new FileStream(fullPath,FileMode.OpenOrCreate,FileAccess.Write);
			    
				//使用Unicode码解析汉字
				Encoding code = Encoding.Default;
				BufferedStream bufStream = new BufferedStream(flStream);
				sw=new StreamWriter(bufStream,code);

			    //导出表头
				this.WriteHead(sw);

				//导出内容
				this.WriteContent(sw,sql);

				sw.Flush();

				return virPath;
			}
			catch(Exception ex)
			{
				ex.ToString();
				return string.Empty;
				
			}
			finally
			{
				if(sw!=null)
					sw.Close();
			}
		}

		/// <summary>
		/// 导出表头
		/// </summary>
		/// <param name="sw"></param>
		private void WriteHead(StreamWriter sw)
		{
			this.ParseHead();
			sw.WriteLine(DoComma(this.title));

			string headline="";
			foreach(string head in this.heads)
			{
				headline=string.Format("{0},{1}",headline,DoComma(head));
			}
            
			if(headline.StartsWith(","))
			{
				headline=headline.Substring(1);
			}

			sw.WriteLine(headline);
		}

		/// <summary>
		/// 导出数据内容
		/// </summary>
		/// <param name="sw"></param>
		/// <param name="sql"></param>
		private void WriteContent(StreamWriter sw, string sql)
		{
			DataSet ds =SqlHelper.ExecuteDataset(Globals.ConnectionString,CommandType.Text,sql);
			
			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				string row = "";
				object[] cells = dr.ItemArray;
				foreach(object cell in cells)
				{
					row=string.Format("{0},{1}",row,DoComma(cell.ToString()));
				}

				if(row.StartsWith(","))
				{
					row=row.Substring(1);
				}

				sw.WriteLine(row);
			}
		}

		/// <summary>
		/// 处理英文逗号
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string DoComma(string input)
		{
			return input.Replace(",","，");
		}
	}
}

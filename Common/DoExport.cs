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
	/// DoExport ��ժҪ˵����
	/// </summary>
	public class DoExport
	{
		public DoExport()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		private string configFileName;
		private string title;
		private StringCollection heads = new StringCollection();
		private string dirPrefix=@"../archives/export/";

		/// <summary>
		/// ��ص������ļ����ļ���
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

		//���������ļ���������ͷ
		private void ParseHead()
		{
			XmlTextReader reader = new XmlTextReader(configFileName);
			try
			{
				// ��reader�ƶ���File��ǩ
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
					throw new ApplicationException(string.Format("�����ļ���{0}����ʽ����ȷ��", this.configFileName));
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
		/// �����ļ�
		/// </summary>
		/// <param name="sql">��ѯ�����õ�sql���</param>
		/// <param name="dir">����ʱ��ʶģ����м��Ŀ¼��</param>
		/// <param name="fileName">�ļ���</param>
		/// <returns>����ɹ��������ļ������·�������򷵻ؿ��ַ���</returns>
		public string Export(string sql,string dir,string fileName)
		{
			if(!fileName.ToLower().EndsWith(".csv"))
			{
				fileName=string.Format("{0}.csv",fileName);
			}
            
			string virPath=string.Format(@"{0}{1}/{2}",this.dirPrefix,dir,fileName);
			string fullPath = System.Web.HttpContext.Current.Server.MapPath(virPath);

			string fullDir=Path.GetDirectoryName(fullPath);
			
			//����Ŀ¼
			if(!Directory.Exists(fullDir))
			{
				Directory.CreateDirectory(fullDir);
			}

			//�����ļ�
			if(File.Exists(fullPath))
			{
				File.Delete(fullPath);
			}

			StreamWriter sw =null;

			try
			{

				//�����ļ���
				FileStream flStream = new FileStream(fullPath,FileMode.OpenOrCreate,FileAccess.Write);
			    
				//ʹ��Unicode���������
				Encoding code = Encoding.Default;
				BufferedStream bufStream = new BufferedStream(flStream);
				sw=new StreamWriter(bufStream,code);

			    //������ͷ
				this.WriteHead(sw);

				//��������
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
		/// ������ͷ
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
		/// ������������
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
		/// ����Ӣ�Ķ���
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string DoComma(string input)
		{
			return input.Replace(",","��");
		}
	}
}

using System;

namespace JrscSoft.Common
{
	[Serializable]
	public class TabSel
	{
	
		private string strTName="";  //���ر���룬���磺  v_ajcj01
		private string strColList="";//���б������磺 jh,pd,yqt
		private string strWhere="";  //where ���� �����磺 jh like '��%' and djsd>1000 
		private string strOrder="";  //�������������磺 djsd desc 
        private string strWELL="";
		public const string m_Desc = " desc ";
		public const string m_Asc = " asc ";
	
		/// <summary>
		/// ���ر����
		/// </summary>
		public string m_TName
		{
			get
			{
				return strTName;
			}
			set
			{
				strTName = value;
			}
		}

		/// <summary>
		/// ���б�
		/// </summary>
		public string m_ColList
		{
			get
			{
				return strColList;
			}
			set
			{
				strColList = value;
			}
		}

		/// <summary>
		/// Where����
		/// </summary>
		public string m_Where
		{
			get
			{
				return strWhere;
			}
			set
			{
				strWhere = value;
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
		public string m_Order
		{
			get
			{
				return strOrder;
			}
			set
			{
				strOrder = value;
			}
		}

		/// <summary>
		/// ����������
		/// </summary>
		public string m_WELL
		{
			get
			{
				return strWELL;
			}
			set
			{
				strWELL = value;
			}
		}


		/// <summary>
		/// ȡ������������д���
		/// </summary>
		/// <returns></returns>
		public string getSortColCode()
		{
		
			string strReturn="";
			if (strOrder.Trim ().Length >0)
			{
				//strReturn= strOrder.Substring (0,strOrder.IndexOf(""));			
				strReturn= strOrder.ToUpper().Replace(m_Desc.ToUpper (),"").Trim ();
			}
			return strReturn;		
		}

		/// <summary>
		/// �����ǽ���
		/// </summary>
		/// <returns>1����  0���� -1δ��ֵ</returns>
		public int getSortIfAsc()
		{
			int iReturn=-1;
			if (strOrder.Trim ().Length >0)
			{
				if (strOrder.ToUpper().IndexOf (m_Desc.ToUpper ()) >0)
					iReturn = 0;
				else if (strOrder.ToUpper().IndexOf (m_Asc.ToUpper ()) >0)
					iReturn = 1;
			}
			return iReturn;			
		}

		/// <summary>
		/// ȡ����ȫ�Ĳ�ѯ��䣬���磺select jh,pd from ajcj01 where pd='����ľ' order by jh
		/// </summary>
		/// <returns></returns>
		public string getSelSqlAll()
		{
			string strReturn="";
			if (strColList == "")  // if 11
				strReturn ="";
			else   // if 11
			{
				if (strWhere == "")   // if 22
				{
					if (strOrder == "")
						strReturn = "select " + strColList +" from " +strTName;
					else
						strReturn = "select " + strColList +" from " +strTName + " order by " + strOrder ;
				}
				else  // if 22
				{
					if (strOrder == "")
						strReturn = "select " + strColList +" from " +strTName +" where " + strWhere;
					else
						strReturn = "select " + strColList +" from " +strTName +" where " + strWhere+ " order by " + strOrder ;
				
				}  // if 22
			
			}  // if 11
			return strReturn;
		}

		/// <summary>
		/// ȡ�����磺select jh,pd  from az01
		/// ���ַ��������治��where ��order by����
		/// </summary>
		/// <returns></returns>
		public string getSelSqlSimple()
		{
			string strReturn="";
			if (strColList == "")  // if 11
				strReturn ="";
			else   // if 11
				strReturn = "select " + strColList +" from " +strTName;
			return strReturn;		
		}


		/// <summary>
		/// ȡ�����磺select jh,pd  from az01 where jh='��1'
		/// ���ַ���������ֻ��where ������order by����
		/// </summary>
		/// <returns></returns>
		public string getSelSqlWhere()
		{
			string strReturn="";
			if (strColList == "")  // if 11
				strReturn ="";
			else   // if 11
			{
				if (strWhere == "") 
					strReturn = "select " + strColList +" from " +strTName;
				else  
					strReturn = "select " + strColList +" from " +strTName +" where " + strWhere;
			}

			return strReturn;		
		}


		/// <summary>
		/// ȡ�����磺select jh,pd  from az01 order by jh
		/// ���ַ��������治��where ��ֻ��order by����
		/// </summary>
		/// <returns></returns>
		public string getSelSqlOrder()
		{
			string strReturn="";
			if (strColList == "")  // if 11
				strReturn ="";
			else   // if 11
			{
				if (strOrder == "") 
					strReturn = "select " + strColList +" from " +strTName;
				else  
					strReturn = "select " + strColList +" from " +strTName + " order by " + strOrder ;
			}

			return strReturn;		
		}


		//	public class TabSel
	}
}

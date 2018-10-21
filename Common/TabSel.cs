using System;

namespace JrscSoft.Common
{
	[Serializable]
	public class TabSel
	{
	
		private string strTName="";  //记载表代码，形如：  v_ajcj01
		private string strColList="";//列列表串，形如： jh,pd,yqt
		private string strWhere="";  //where 条件 ，形如： jh like '古%' and djsd>1000 
		private string strOrder="";  //排序条件，形如： djsd desc 
        private string strWELL="";
		public const string m_Desc = " desc ";
		public const string m_Asc = " asc ";
	
		/// <summary>
		/// 记载表代码
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
		/// 列列表串
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
		/// Where条件
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
		/// 排序条件
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
		/// 井条件限制
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
		/// 取得用于排序的列代码
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
		/// 升序还是降序
		/// </summary>
		/// <returns>1升序  0降序 -1未附值</returns>
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
		/// 取得完全的查询语句，形如：select jh,pd from ajcj01 where pd='塔里木' order by jh
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
		/// 取得形如：select jh,pd  from az01
		/// 的字符串。后面不带where 和order by条件
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
		/// 取得形如：select jh,pd  from az01 where jh='古1'
		/// 的字符串。后面只带where ，不带order by条件
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
		/// 取得形如：select jh,pd  from az01 order by jh
		/// 的字符串。后面不带where ，只带order by条件
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

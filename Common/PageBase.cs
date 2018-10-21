using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Data;
using System.Collections;
using System.Xml;
using System.Text;

namespace JrscSoft.Common
{
	/// <summary>
	/// PageBase ��ժҪ˵����
	/// </summary>
	public class PageBase:Page
	{
		public PageBase()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			try
			{
				baseUrl =String.Format("{0}/",Context.Request.ApplicationPath);
			}
			catch
			{
			}
		}

	    public static string baseUrl;
		protected static string LoginMsg = "�ȴ�ʱ�����,�����µ�¼��";    //Session����ʱ��ʾ����Ϣ
		protected static string NoRightMsg = "����Ȩ�޲��㣬���ܷ��ʱ�ҳ�棡";      //Ȩ�޲���ʱ��ʾ����Ϣ
		protected static string NoRightUrl = "../InitialPage.aspx";              //Ȩ�޲���ʱĬ��ת����ҳ
        /// <summary>
		/// ����Ŀ¼�ĸ�·��
		/// </summary>
		public static string BaseUrl
		{
			get
			{
				return baseUrl;
			}
		}

		#region BindControl&Property
		/// <summary>
		/// Binds an object's properties to <see cref="Control"/>s with the same ID as the propery name. �����������ֵ���ֵ��ؼ���.
		/// </summary>
		/// <param name="obj">The object whose properties are being bound to forms Controls</param>
		/// <param name="container">The control in which the form Controls reside (usually a Page or ContainerControl)</param>
		public static void BindObjectToControls(object obj, Control container) 
		{
			if (obj == null) return;
			
			// Get the properties of the business object
			//
			Type objType = obj.GetType();
			PropertyInfo[] objPropertiesArray = objType.GetProperties();

			foreach (PropertyInfo objProperty in objPropertiesArray) 
			{
				if(objProperty.GetValue(obj,null)!=null)
				{

					Control control = container.FindControl(objProperty.Name);
				
					// handle ListControls (DropDownList, CheckBoxList, RadioButtonList)
					//�ж��Ƿ�����������ͬ���Ŀؼ�
					if(control!=null)
					{
						if (control is ListControl) 
						{
							ListControl listControl = (ListControl) control;
							string propertyValue = objProperty.GetValue(obj, null).ToString();
							ListItem listItem = listControl.Items.FindByValue(propertyValue);
							if (listItem != null) listItem.Selected = true;
					
						} 
						else 
						{
							// get the properties of the control
							//
							Type controlType = control.GetType();
							PropertyInfo[] controlPropertiesArray = controlType.GetProperties();

							// test for common properties
							//
							bool success = false;
							success = FindAndSetControlProperty(obj, objProperty, control, controlPropertiesArray, "Checked", typeof(bool) );
							
							if (!success) 
								success = FindAndSetControlProperty(obj, objProperty, control, controlPropertiesArray, "SelectedDate", typeof(DateTime) );

							if (!success) 
								success = FindAndSetControlProperty(obj, objProperty, control, controlPropertiesArray, "Value", typeof(String) );
							
							if (!success) 
								success = FindAndSetControlProperty(obj, objProperty, control, controlPropertiesArray, "Text", typeof(String) );
							
						}
					}
			    }
			}
		}

    public static void BindFieldsToControls(object obj, Control container) 
    {
      if (obj == null) return;
			
      // Get the properties of the business object
      //
      Type objType = obj.GetType();
      FieldInfo[] objPropertiesArray = objType.GetFields();

      foreach (FieldInfo objProperty in objPropertiesArray) 
      {
        if(objProperty.GetValue(obj) != null)
        {

          Control control = container.FindControl(objProperty.Name);
				
          // handle ListControls (DropDownList, CheckBoxList, RadioButtonList)
          //�ж��Ƿ�����������ͬ���Ŀؼ�
          if(control!=null)
          {
            if (control is ListControl) 
            {
              ListControl listControl = (ListControl) control;
              string propertyValue = objProperty.GetValue(obj).ToString();
              ListItem listItem = listControl.Items.FindByValue(propertyValue);
              if (listItem != null) listItem.Selected = true;
					
            } 
            else 
            {
              // get the properties of the control
              //
              Type controlType = control.GetType();
              PropertyInfo[] controlPropertiesArray = controlType.GetProperties();

              // test for common properties
              //
              bool success = false;
              success = FindAndSetControlProperty(obj, objProperty, control, controlPropertiesArray, "Checked", typeof(bool) );
							
              if (!success) 
                success = FindAndSetControlProperty(obj, objProperty, control, controlPropertiesArray, "SelectedDate", typeof(DateTime) );

              if (!success) 
                success = FindAndSetControlProperty(obj, objProperty, control, controlPropertiesArray, "Value", typeof(String) );
							
              if (!success) 
                success = FindAndSetControlProperty(obj, objProperty, control, controlPropertiesArray, "Text", typeof(String) );
							
            }
          }
        }
      }
    }
		
		/// <summary>
		/// Looks for a property name and type on a control and attempts to set it to the value in an object's property 
		/// of the same name.
		/// </summary>
		/// <param name="obj">The object whose properties are being retrieved</param>
		/// <param name="objProperty">The property of the object being retrieved</param>
		/// <param name="control">The control whose ID matches the object's property name.</param>
		/// <param name="controlPropertiesArray">An array of the control's properties</param>
		/// <param name="propertyName">The name of the Control property being set</param>
		/// <param name="type">The correct type for the Control property</param>
		/// <returns>Boolean for whether the property was found and set</returns>
		private static bool FindAndSetControlProperty(object obj, PropertyInfo objProperty, Control control, PropertyInfo[] controlPropertiesArray, string propertyName, Type type) 
		{
			// iterate through control properties
			//
			foreach (PropertyInfo controlProperty in controlPropertiesArray) 
			{
				// check for matching name and type
				//
				if (controlProperty.Name == propertyName && controlProperty.PropertyType == type) 
				{
					// set the control's property to the business object property value
					//
					controlProperty.SetValue(control, Convert.ChangeType( objProperty.GetValue(obj, null), type) , null);
					return true;
				}
			}
			return false;
		}

    private static bool FindAndSetControlProperty(object obj, FieldInfo objProperty, Control control, PropertyInfo[] controlPropertiesArray, string propertyName, Type type) 
    {
      // iterate through control properties
      //
      foreach (PropertyInfo controlProperty in controlPropertiesArray) 
      {
        // check for matching name and type
        //
        if (controlProperty.Name == propertyName && controlProperty.PropertyType == type) 
        {
          // set the control's property to the business object property value
          //
          controlProperty.SetValue(control, Convert.ChangeType( objProperty.GetValue(obj), type), null);
          return true;
        }
      }
      return false;
    }

		/// <summary>
		/// Binds your the values in <see cref="Control"/>s to a business object.���ؼ���ֵ���µ����������ֵ��.
		/// </summary>
		/// <param name="obj">The object whose properties are being bound to Control values</param>
		/// <param name="container">The control in which the form Controls reside (usually a Page or ContainerControl)</param>
		public static void BindControlsToObject(object obj, Control container) 
		{
			if (obj == null) return;
			
			// Get the properties of the business object
			//			
			Type objType = obj.GetType();
			PropertyInfo[] objPropertiesArray = objType.GetProperties();

			foreach (PropertyInfo objProperty in objPropertiesArray) 
			{
				Control control = container.FindControl(objProperty.Name);
				if(control != null)
				{
					if (control is ListControl) 
					{
						ListControl listControl = (ListControl) control;
						if (listControl.SelectedItem != null)
							objProperty.SetValue(obj, Convert.ChangeType(listControl.SelectedItem.Value,objProperty.PropertyType), null);

					} 
					else 
					{
						// get the properties of the control
						//
						Type controlType = control.GetType();
						PropertyInfo[] controlPropertiesArray = controlType.GetProperties();

						// test for common properties
						//
						bool success = false;
						success = FindAndGetControlProperty(obj, objProperty, control, controlPropertiesArray, "Checked", typeof(bool) );

						if (!success) 
							success = FindAndGetControlProperty(obj, objProperty, control, controlPropertiesArray, "SelectedDate", typeof(DateTime) );

						if (!success) 
							success = FindAndGetControlProperty(obj, objProperty, control, controlPropertiesArray, "Value", typeof(String) );

						if (!success) 
							success = FindAndGetControlProperty(obj, objProperty, control, controlPropertiesArray, "Text", typeof(String) );
					}
				}
			}
		}

		/// <summary>
		/// Looks for a property name and type on a control and attempts to set it to the value in an object's property 
		/// of the same name.
		/// </summary>
		/// <param name="obj">The object whose properties are being set</param>
		/// <param name="objProperty">The property of the object being set</param>
		/// <param name="control">The control whose ID matches the object's property name.</param>
		/// <param name="controlPropertiesArray">An array of the control's properties</param>
		/// <param name="propertyName">The name of the Control property being retrieved</param>
		/// <param name="type">The correct type for the Control property</param>
		/// <returns>Boolean for whether the property was found and retrieved</returns>
		private static bool FindAndGetControlProperty(object obj, PropertyInfo objProperty, Control control, PropertyInfo[] controlPropertiesArray, string propertyName, Type type) 
		{
			// iterate through control properties
			//
			foreach (PropertyInfo controlProperty in controlPropertiesArray) 
			{
				// check for matching name and type
				//
				if (controlProperty.Name == propertyName && controlProperty.PropertyType == type) 
				{
					// set the control's property to the business object property value
					//
					try 
					{	
						//�����ı����е������ַ���
						if(control is TextBox)
							controlProperty.SetValue(control,CleanString(controlProperty.GetValue(control,null).ToString()),null);
						
						objProperty.SetValue(obj,Convert.ChangeType(controlProperty.GetValue(control,null),objProperty.PropertyType), null);
						//�����ı����е������ַ���
//						if(control is TextBox)
//                          objProperty.SetValue(obj,CleanString(objProperty.GetValue(obj,null).ToString()),null);
						return true;
					} 
					catch(Exception ex)
					{
						// the data from the form control could not be converted to objProperty.PropertyType
						//
						ex.ToString();
						return false;
					}
				}
			}
			return false;
		}

		/// <summary>
		/// Binds an object's properties to <see cref="Control"/>s with the same ID as the propery name. �����������ֵ���ֵ��ؼ���.
		/// </summary>
		/// <param name="obj">The object whose properties are being bound to forms Controls</param>
		/// <param name="container">The control in which the form Controls reside (usually a Page or ContainerControl)</param>
		public static void BindObjectToTableRow(CollectionBase cb, HtmlTable tb, string fileName) 
		{
			if (cb == null) return;
			
			// Get the properties of the business object
			//
			//��ȡ�е���Ϣ
            PreViewColumnCollection pcc =(new PageBase()).GetColumns(fileName);
			//���ɱ�ͷ
			HtmlTableRow th =new HtmlTableRow();
			th.BgColor="#ffffff";
			foreach(PreViewColumn column in pcc)
			{
				HtmlTableCell tc = new HtmlTableCell();
				tc.Width=column.Width.ToString();
				tc.Align="center";
				tc.InnerHtml=column.Caption;
				tc.NoWrap=true;
				th.Cells.Add(tc);
			}
			tb.Rows.Add(th);

			//���ɱ������
			foreach(object obj in cb)
			{
				HtmlTableRow tr =new HtmlTableRow();
				tr.BgColor="#ffffff";

				Type objType = obj.GetType();
				PropertyInfo[] objPropertiesArray = objType.GetProperties();		

				
				foreach(PreViewColumn column in pcc)
				{
					foreach (PropertyInfo objProperty in objPropertiesArray)
					{
						if(objProperty.Name==column.ID)
						{
							string propertyValue = objProperty.GetValue(obj, null).ToString();
							if(column.Fomat=="num")
							{
								propertyValue=(double.Parse(propertyValue)).ToString("n2");
							}

							HtmlTableCell tc = new HtmlTableCell();
							tc.InnerText=propertyValue;
							tc.Align=column.Align;
							tc.Width=column.Width.ToString();
					
							tr.Cells.Add(tc);

						}
					}			
					tb.Rows.Add(tr);
				}
			}
		}

		/// <summary>
		/// Binds an object's properties to <see cref="Control"/>s with the same ID as the propery name. �����������ֵ���ֵ��ؼ���.
		/// </summary>
		/// <param name="obj">The object whose properties are being bound to forms Controls</param>
		/// <param name="container">The control in which the form Controls reside (usually a Page or ContainerControl)</param>
		public static void BindObjectToTableRow(CollectionBase cb,Table tb, string fileName) 
		{
			if (cb == null) return;
			
			// Get the properties of the business object
			//
			//��ȡ�е���Ϣ
			PreViewColumnCollection pcc =(new PageBase()).GetColumns(fileName);
			//���ɱ�ͷ
			TableRow th =new TableRow();
			th.CssClass="tr_item";
			foreach(PreViewColumn column in pcc)
			{
				TableCell tc = new TableCell();
				if((object)column.Width!=null)
				{
					if(column.Unit=="%")
					{
						tc.Width=Unit.Percentage((double)column.Width);
					}
					else
					{
						tc.Width=Unit.Pixel(column.Width);
					}
				}			
				tc.HorizontalAlign=HorizontalAlign.Center;
				tc.Text=column.Caption;
				tc.Wrap=false;
				tc.BorderWidth=Unit.Pixel(1);
				th.Cells.Add(tc);
			}
			tb.Rows.Add(th);

			//���ɱ������
			foreach(object obj in cb)
			{
				TableRow tr =new TableRow();
				tr.CssClass="tr_item";

				Type objType = obj.GetType();
				PropertyInfo[] objPropertiesArray = objType.GetProperties();		

				
				foreach(PreViewColumn column in pcc)
				{
					foreach (PropertyInfo objProperty in objPropertiesArray)
					{
						if(objProperty.Name==column.ID)
						{
							string propertyValue = objProperty.GetValue(obj, null).ToString();
							if(column.Fomat=="num")
							{
								propertyValue=(double.Parse(propertyValue)).ToString("F");
							}

							TableCell tc = new TableCell();
							tc.Text=propertyValue;
							if(column.Align!=null && column.Align!="")
							{
								switch(column.Align)
								{
									case "left":
										tc.HorizontalAlign=HorizontalAlign.Left;
										break;
									case "center":
										tc.HorizontalAlign=HorizontalAlign.Center;
										break;
									case "right":
										tc.HorizontalAlign=HorizontalAlign.Right;
										break;
								}
							}

							if((object)column.Width!=null)
							{
								if(column.Unit=="%")
								{
									tc.Width=Unit.Percentage((double)column.Width);
								}
								else
								{
									tc.Width=Unit.Pixel(column.Width);
								}
							}			
					        tc.BorderWidth=Unit.Pixel(1);
							tr.Cells.Add(tc);

						}
					}			
					tb.Rows.Add(tr);
				}
			}
		}
		#endregion

 
		
		/// <summary>
		/// �����ݰ󶨵�ʱ��������������URL����
		/// </summary>
		/// <param name="e">�¼�����</param>
		/// <param name="cellPos">Cell��Ԫ����Row���е�λ��</param>
		/// <param name="controlPos">HyperLinkColumn��Cell�ڵ�λ��</param>
		/// <param name="paras">�ɱ���������б�</param>
		/// <remarks>ʹ��ʱ����key��value��˳����ɵ�string���飬��new string[]{"key1","value1","key2","value2"}</remarks>
		public static void AddHyperLinkParams(DataGridItemEventArgs e,int cellPos,int controlPos,params string[] paras)
		{
			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				HyperLink a = e.Item.Cells[cellPos].Controls[controlPos] as HyperLink;
				if(a != null)
				{
					StringBuilder url = new StringBuilder(a.NavigateUrl);
					for (int i = 0; i < paras.Length / 2; i++)
					{
						url.AppendFormat("&{0}={1}", paras[2*i], paras[2*i+1]);
						a.NavigateUrl = url.ToString();
					}
					
				}
			}
			
		}
	
		
	public static void BindFieldsToTableRow(CollectionBase cb, Table tb, string fileName) 
    {
      if (cb == null) return;
			
      // Get the properties of the business object
      //
      //��ȡ�е���Ϣ
      PreViewColumnCollection pcc =(new PageBase()).GetColumns(fileName);
      //���ɱ�ͷ
      TableRow th =new TableRow();
      th.CssClass="tr_item";
      foreach(PreViewColumn column in pcc)
      {
        TableCell tc = new TableCell();
        if((object)column.Width!=null)
        {
          if(column.Unit=="%")
          {
            tc.Width=Unit.Percentage((double)column.Width);
          }
          else
          {
            tc.Width=Unit.Pixel(column.Width);
          }
        }			
        tc.HorizontalAlign=HorizontalAlign.Center;
        tc.Text=column.Caption;
        tc.Wrap=false;
        tc.BorderWidth=Unit.Pixel(1);
        th.Cells.Add(tc);
      }
      tb.Rows.Add(th);

      //���ɱ������
      foreach(object obj in cb)
      {
        TableRow tr =new TableRow();
        tr.CssClass="tr_item";

        Type objType = obj.GetType();
        FieldInfo[] objPropertiesArray = objType.GetFields();		

				
        foreach(PreViewColumn column in pcc)
        {
          foreach (FieldInfo objProperty in objPropertiesArray)
          {
            if(objProperty.Name==column.ID)
            {
              string propertyValue = objProperty.GetValue(obj).ToString();
              if(column.Fomat=="num")
              {
                propertyValue=(double.Parse(propertyValue)).ToString("F");
              }

              TableCell tc = new TableCell();
              tc.Text=propertyValue;
              if(column.Align!=null && column.Align!="")
              {
                switch(column.Align)
                {
                  case "left":
                    tc.HorizontalAlign=HorizontalAlign.Left;
                    break;
                  case "center":
                    tc.HorizontalAlign=HorizontalAlign.Center;
                    break;
                  case "right":
                    tc.HorizontalAlign=HorizontalAlign.Right;
                    break;
                }
              }

              if((object)column.Width!=null)
              {
                if(column.Unit=="%")
                {
                  tc.Width=Unit.Percentage((double)column.Width);
                }
                else
                {
                  tc.Width=Unit.Pixel(column.Width);
                }
              }			
              tc.BorderWidth=Unit.Pixel(1);
              tr.Cells.Add(tc);

            }
          }			
          tb.Rows.Add(tr);
        }
      }
    }

		#region ��ʾ��Ϣ

		/// <summary>
		/// �����ɹ�������ʾ��Ϣ,����ʾ��Ϣ���ڹرպ󸸴���ת���µ�Url
		/// </summary>
		/// <param name="msg">��ʾ��Ϣ</param>
		/// <param name="url">��ʾ��Ϣ���ڹرպ󸸴���ת�����Url</param>
		public void GetSuccessMsg(string msg,string url)
		{
			Alert("�����ɹ�",msg,url);
		}

		/// <summary>
		/// �����ɹ��󵯳�����ʾ��Ϣ,��ʾ��Ϣ���ڹرպ󸸴��岻��
		/// </summary>
		/// <param name="msg">��ʾ��Ϣ</param>
		public void GetSuccessMsg(string msg)
		{
			//GetSuccessMsg(msg,"");
			this.GetAlertMsg(msg);
     	}
        
		/// <summary>
		/// ����ʧ�ܺ󵯳���ʾ��Ϣ������ʾ��Ϣ���ڹرպ󸸴���ת���µ�Url
		/// </summary>
		/// <param name="msg">��ʾ��Ϣ</param>
		/// <param name="url">��ʾ��Ϣ���ڹرպ󸸴���ת�����Url</param>
		public void GetErrMsg(string msg,string url)
		{
			Alert("����ʧ��",msg,url);
		}

		/// <summary>
		/// ����ʧ�ܺ󵯳�����ʾ��Ϣ,��ʾ��Ϣ���ڹرպ󸸴��岻��
		/// </summary>
		/// <param name="msg">��ʾ��Ϣ</param>
		public void GetErrMsg(string msg)
		{
			//GetErrMsg(msg,"");
			this.GetAlertMsg(msg);
		}

		/// <summary>
		/// ������ʾ��Ϣ����,ʹ��Alert.aspxҳ��
		/// </summary>
		/// <param name="title">����</param>
		/// <param name="msg">��ʾ����</param>
		/// <param name="url">���ص�ַ��URL,�������Ŀ¼�����·��</param>
		public void Alert(string title,string msg,string url)
		{
			this.RegisterClientScriptBlock("alert","<script>alert('"+msg+"');window.location='"+url+"';</script>");
			//this.RegisterClientScriptBlock("alert",String.Format("<script>Openwin('{0}Alertpage.aspx?message={1}&title={2}&returnUrl={3}',260,80);</script>",BaseUrl,Server.UrlEncode(msg),Server.UrlEncode(title),Server.UrlEncode(url)));
//			this.RegisterClientScriptBlock("alert",String.Format("<script>OpenModalWin('{0}Alertpage.aspx?message={1}&title={2}&returnUrl={3}',300,150);</script>",BaseUrl,Server.UrlEncode(msg),Server.UrlEncode(title),Server.UrlEncode(url)));
//			string fullUrl=Server.HtmlEncode(String.Format("{0}Alertpage.aspx?message={1}&title={2}&returnUrl={3}",BaseUrl,msg,title,url));
//			this.RegisterClientScriptBlock("alert",String.Format("<script>Openwin('{0}',260,80);</script>",fullUrl));
		}

		/// <summary>
		/// ������ʾ��Ϣ����,ʹ��һ���Alert
		/// </summary>
		/// <param name="msg">��ʾ������</param>
		public void GetAlertMsg(string msg)
		{
			this.RegisterClientScriptBlock("alert",String.Format("<script>alert('{0}');</script>",msg));
		}

		/// <summary>
		/// ������ʾ��Ϣ���ڣ����ر��Ӵ���
		/// </summary>
		/// <param name="msg">��ʾ������</param>
		public void GetMsgAndClose(string msg)
		{
			this.RegisterClientScriptBlock("alert","<script>alert('"+msg+"');window.close();</script>");
		}	

		/// <summary>
		/// ������ʾ��Ϣ���ڣ��ر��Ӵ��ڣ�֮�󸸴���ת��urlָ���ҳ��
		/// </summary>
		/// <param name="msg">��ʾ������</param>
		/// <param name="url">������Ҫת���ҳ��</param>
		public void GetMsgAndClose(string msg,string url)
		{
			this.RegisterClientScriptBlock("alert","<script>alert('"+msg+"');window.close();opener.navigate('"+url+"');</script>");
		}
	
		public void GetMsgAndRefresh(string msg,string ctl)
		{
			this.RegisterClientScriptBlock("alertAndRefresh","<script>alert('"+msg+"');window.close();opener.document.all['"+ctl+"'].click();</script>");
		}

		#endregion

		/// <summary>
		/// ����������ַ���
		/// </summary>
		/// <param name="input">������ַ���</param>
		/// <returns>���������ַ���</returns>
		public static string CleanString(string input)
		{
			string output = input.Replace("'","''");
			output = output.Replace("--","��");
			return output;
		}

		#region �����ҳ������
		/// <summary>
		/// ����datagrid����ʱ���б������ʾ
		/// </summary>
		/// <param name="arr">������ʽ���ɵ��ַ�������</param>
		/// <param name="myDg">��Ҫִ�����������datagrid</param>
		/// <param name="index">�����е�����</param>
		public void ConfigColumnTitle(string[] arr,DataGrid myDg,int index)
		{
			// �޸���һ�������еı���
			foreach(DataGridColumn column in myDg.Columns)
			{
				if(!column.Visible || column.HeaderText.Length==0) continue;
				else if((column.HeaderText[column.HeaderText.Length-1]=='��') || (column.HeaderText[column.HeaderText.Length-1]=='��'))
				{
					column.HeaderText = column.HeaderText.Substring(0, column.HeaderText.Length-1);
					break;
				}
			}

			// ���������б���
			if(arr[1].Trim().ToLower()=="desc")
				myDg.Columns[index].HeaderText += "��";
			else
				myDg.Columns[index].HeaderText += "��";

			// ���������е�SortExpression
			arr[1] = arr[1].Trim().ToLower()=="desc"?"ASC":"DESC";
			myDg.Columns[index].SortExpression = string.Join(" ", arr);

		}

		/// <summary>
		/// ע���ҳʱ�õ��Ŀͻ��˽ű�
		/// </summary>
		/// <param name="myDg">��Ҫִ�л�ҳ������datagrid</param>
		public void RegisterPageChangeScript(DataGrid myDg)
		{
			System.Text.StringBuilder sb=new System.Text.StringBuilder("<script Language=\"Javascript\"><!--\n");
			sb.Append("var el=document.all;");
			sb.Append(myDg.ClientID);
			sb.Append(".scrollIntoView(true);");
			sb.Append("<");
			sb.Append("/");
			sb.Append("script>");
			if(!Page.IsStartupScriptRegistered("scrollScript"))
				Page.RegisterStartupScript("scrollScript",sb.ToString());	
		}
		#endregion

		#region ����Session
		/// <summary>
		//Session����,���µ��򵽵�¼ҳ��
		/// </summary>
		public void  ReloadLogin(string Url)
		{

			if(HttpContext.Current.Session["UserName"] == null )
			{

				string str;
				str="<script language='javascript'>";  
				str+=" window.alert('" + LoginMsg + "');";
				str+="parent.location.href='"+Url+"'";
				str+="</script>";
				Response.Write(str);
				Response.End();
			}
		}

        /// <summary>
        /// ����Ŀר�ô���ʯ�ͻ㽻,���ÿ��
        /// </summary>
        /// <param name="Url"></param>
        public void ReloadFrameLogin(string Url)
        {
            if (HttpContext.Current.Session["UserName"] == null)
            {
                string str;
                str = "<script language='javascript'>";
                str += " window.alert('" + LoginMsg + "');";
                str += "window.parent.parent.location.href='" + Url + "'";
                str += "</script>";
                Response.Write(str);
                Response.End();
            }
        }

        /// <summary>
        /// ����Ŀר�ô���ʯ�ͻ㽻,���ÿ��
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="iFrame">frameǶ�ײ�Σ�Ĭ��Ϊ1�㣬0Ϊ��ǰҳ����ʹ�ÿ�ܵ�ǰҳ����ת��</param>
        public void ReloadFrameLogin(string Url,string iFrame)
        {
            if (HttpContext.Current.Session["UserName"] == null)
            {
                string str;
                str = "<script language='javascript'>";
                str += " window.alert('" + LoginMsg + "');";
                switch (iFrame)
                { 
                    case "0":
                        str += "window.parent.location.href='" + Url + "'";
                        break;
                    case "1":
                        str += "window.parent.parent.location.href='" + Url + "'";
                        break;
                    case "2":
                        str += "window.parent.parent.parent.location.href='" + Url + "'";
                        break;
                    case "3":
                        str += "window.parent.parent.parent.parent.location.href='" + Url + "'";
                        break;
                    default:
                        str += "window.parent.parent.location.href='" + Url + "'";
                        break;
                }
                str += "</script>";
                Response.Write(str);
                Response.End();
            }
        }

        /// <summary>
        /// Ŀ¼���𣬵�ǰΪ0��һ������1,2,3,4
        /// </summary>
        public void ReloadLogin(int PathNum)
        {
            switch (PathNum)
            {
                case 0:
                    ReloadFrameLogin("default.aspx");
                    break;
                case 1:
                    ReloadFrameLogin("../default.aspx");
                    break;
                case 2:
                    ReloadFrameLogin("../../default.aspx");
                    break;
                case 3:
                    ReloadFrameLogin("../../../default.aspx");
                    break;
                case 4:
                    ReloadFrameLogin("../../../../default.aspx");
                    break;
            }

        }
        /// <summary>
        /// ��ת����½ҳ ����Ŀר�ô���ʯ�ͻ㽻,���ÿ��
        /// </summary>
        /// <param name="PathNum">Ŀ¼���𣬵�ǰΪ0��һ������1,2,3,4</param>
        /// <param name="iFrame">frameǶ�ײ�Σ�Ĭ��Ϊ1�㣬0Ϊ��ǰҳ����ʹ�ÿ�ܵ�ǰҳ����ת��</param>
        public void ReloadLogin(int PathNum, string iFrame)
        {
            string sUrl = "";

            switch (PathNum)
            {
                case 0:
                    sUrl = "default.aspx";
                    break;
                case 1:
                    sUrl = "../default.aspx";
                    break;
                case 2:
                    sUrl = "../../default.aspx";
                    break;
                case 3:
                    sUrl = "../../../default.aspx";
                    break;
                case 4:
                    sUrl = "../../../../default.aspx";
                    break;
                default:
                    sUrl = "default.aspx";
                    break;
            }
           ReloadFrameLogin(sUrl, iFrame);

        }

		/// <summary>
		//Session����,���µ��򵽵�¼ҳ�棬Ĭ�����,
		/// </summary>
		public void  ReloadLogin()
		{
			//ReloadLogin("default.aspx");
            ReloadFrameLogin("default.aspx");
		}

		/// <summary>
		//Session���ڹرմ��ڣ������Ӵ���
		/// </summary>
		public void  ReloadLoginChild()
		{
            if (HttpContext.Current.Session["UserName"] == null)
			{
				string str;
				str="<script language='javascript'>";  
				str+=" window.alert('" + LoginMsg + "');";
				str+="window.close();opener.window.parent.navigate('../default.aspx')";				
				str+="</script>";
				Response.Write(str);
				Response.End();
			}
		}
		#endregion

		public PreViewColumnCollection GetColumns(string fileName)
		{
			fileName=string.Format("~/{0}/{1}.xml",Functions.GetAppConfigString("ConfigFilePath","config"),fileName);
			fileName=Server.MapPath(fileName);
			XmlTextReader reader = new XmlTextReader(fileName);
			// ��reader�ƶ���Where��ǩ
			if(reader.MoveToContent() == XmlNodeType.Element && reader.LocalName.Trim().Equals("table"))
			{
                PreViewColumnCollection collection =new PreViewColumnCollection();
				while(reader.Read())
				{
					if(reader.NodeType != XmlNodeType.Element)
						continue;
                    PreViewColumn column =new PreViewColumn();
					switch(reader.LocalName.Trim())
					{
						case "td":
							column.ID=reader.GetAttribute("id");
							if(reader.GetAttribute("width")!=null && reader.GetAttribute("width")!="")
							{
								column.Width=int.Parse(reader.GetAttribute("width"));
							}

							if(reader.GetAttribute("unit")!=null && reader.GetAttribute("unit")!="")
							{
								column.Unit=reader.GetAttribute("unit");
							}

							if(reader.GetAttribute("align")!=null && reader.GetAttribute("align")!="")
							{
								column.Align=reader.GetAttribute("align");
							}

							if(reader.GetAttribute("fomat")!=null && reader.GetAttribute("fomat")!="")
							{
								column.Fomat=reader.GetAttribute("fomat");	
							}
							column.Caption=reader.GetAttribute("caption");	
							break;
					}

					if(column.ID!=null&& column.ID!="")
						collection.Add(column);				
										
				}
        reader.Close();
				return collection;
			}
			else
			{
				if(reader != null) reader.Close();
				throw new ApplicationException(string.Format("�����ļ���{0}����ʽ����ȷ��", fileName));
			}
		
		}

		/// <summary>
		/// ע��ű�
		/// </summary>
		/// <param name="scriptName"></param>
		/// <param name="controlName"></param>
		public void SetFocus(string scriptName, string controlName)
		{
			string script = "<script language='javascript'"+">\n"
				+ "document.all." + controlName + ".focus();\n"
				+ "</scri"+"pt>\n";
			if(!this.IsStartupScriptRegistered(scriptName))
				this.RegisterStartupScript(scriptName, script);

		}

		/// <summary>
		/// ע��򿪵����������ļ��Ľű�
		/// </summary>
		/// <param name="url">�����ļ���·��</param>
		protected void OpenReport(string scriptName,string url)
		{
			string script=string.Format("<script>OpenwinFull('{0}');</script>",url);
			if(!this.IsStartupScriptRegistered(scriptName))
				this.RegisterStartupScript(scriptName, script);

		}

		/// <summary>
		/// �رմ���
		/// </summary>
		public void CloseWin()
		{
			this.RegisterClientScriptBlock("closewin","<script>window.close();</script>");
		}

		public void BindDropDownList(DropDownList ddl,CollectionBase collection,string textfield, string valuefield, string currentvalue)
		{
			ddl.DataSource = collection;
			ddl.DataTextField = textfield;
			ddl.DataValueField = valuefield;
			ddl.DataBind();

			if(ddl.Items.FindByValue(currentvalue)!=null)
				ddl.SelectedValue = currentvalue;
		}
        /// <summary>
        /// �Ѵ�����Ϣд��c�̸�Ŀ¼�µ�Error.log��
        /// 
        /// </summary>
        /// ���ַ�
        /// <param name="ErrorInfo">������Ϣ</param>
        public static void WriteErrorLog(string ErrorInfo)
        {
            System.IO.TextWriter writer = System.IO.File.AppendText("c:\\Error.log");
            writer.WriteLine("{0}: ������Ϣ�� {1} .",
                DateTime.Now, ErrorInfo);
            writer.Close();
        }
	}
}

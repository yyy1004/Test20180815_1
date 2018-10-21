<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jz2.aspx.cs" Inherits="Web.JZ.jz" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>机组信息浏览</title>
    <link  id="MyCss" href="../COMMON/CSS/Default.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="../COMMON/JS/changerows.js"  type="text/css"></script>
<meta http-equiv="pragma" content="no-cache" />
    <script src="/Scripts/loading.js" type="text/javascript"></script>
    <link href="/EasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="/EasyUI/themes/gray/easyui.css" rel="stylesheet" type="text/css" />
    <script src="/EasyUI/jquery.min.js" type="text/javascript"></script>
    <script src="/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/Scripts/distinguishBrowser.js" type="text/javascript"></script>
    <script src="../EasyUI/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <!--单文件导入-->
    <script src="/Echarts/echarts-all.js" type="text/javascript"></script>
    <script src="/Echarts/chart/gauge.js" type="text/javascript"></script>
   <script type="text/javascript">
       var dom = document.getElementById("myechart1");
       var myChart = echarts.init(dom);
       var app = {};
       option = null;
       option = {
           tooltip: {
               formatter: "{a} <br/>{b} : {c}%"
           },
           toolbox: {
               feature: {
                   restore: {},
                   saveAsImage: {}
               }
           },
           series: [
        {
            name: '温度',
            type: 'gauge',
            detail: { formatter: '{value}%' },
            data: [{ value: 50, name: '完成率'}],
            min: '0',
            max: '45',
            axisLine: {
                show: true,
                lineStyle: { // 属性lineStyle控制线条样式
                    color: [ //表盘颜色
                             [0.5, "#DA462C"], //0-50%处的颜色
                             [0.7, "#FF9618"], //51%-70%处的颜色
                              [0.9, "#FFED44"], //70%-90%处的颜色
                              [1, "#20AE51"]//90%-100%处的颜色
                          ],
                    width: 30//表盘宽度
                }
            }
        }

    ]
       };

       setInterval(function () {
           option.series[0].data[0].value = (Math.random() * 50).toFixed(2) - 0;
           myChart.setOption(option, true);
       }, 20000);
       ;
       if (option && typeof option === "object") {
           myChart.setOption(option, true);
       }

   </script>
</head>
<body>
    <form id="form1" runat="server" class="table_bgcolor">
    <div>
     <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center" id="Table1" class="table_bordercolor">
     <tr>
     <td width="100">
     <table  width="100"   >
     <tr><td class="style1">1</td></tr>
     <tr><td><asp:Label ID="Label1" runat="server" Text="Label"></asp:Label><asp:Label ID="Label2" runat="server" Text="Label"></asp:Label> 
      <div id="container" style="height: 100%"></div>
    
     </td></tr>
         
     <tr><td><asp:Label ID="Label3" runat="server" Text="Label"></asp:Label><asp:Label ID="Label4" runat="server" Text="Label"></asp:Label></td></tr>
      <tr><td><asp:Label ID="Label5" runat="server" Text="Label"></asp:Label><asp:Label ID="Label6" runat="server" Text="Label"></asp:Label></td></tr>
     <tr><td><asp:Label ID="Label7" runat="server" Text="Label"></asp:Label><asp:Label ID="Label8" runat="server" Text="Label"></asp:Label></td></tr>
     <tr><td><asp:Label ID="Label9" runat="server" Text="Label"></asp:Label><asp:Label ID="Label10" runat="server" Text="Label"></asp:Label></td></tr>
       <tr><td><asp:Label ID="Label11" runat="server" Text="Label"></asp:Label><asp:Label ID="Label12" runat="server" Text="Label"></asp:Label></td></tr>
     <tr><td><asp:Label ID="Label13" runat="server" Text="Label"></asp:Label><asp:Label ID="Label14" runat="server" Text="Label"></asp:Label></td></tr>
      <tr><td><asp:Label ID="Label15" runat="server" Text="Label"></asp:Label><asp:Label ID="Label16" runat="server" Text="Label"></asp:Label></td></tr>
    
     </table>
     </td>
     <td>
         <asp:Image ID="Image1" runat="server"  Width="400" Height="400"  ImageUrl="~/Images/齿轮箱传动简图.jpg"/>
         </td>
        
     
     </tr>
     <tr>
     <td class="table_bgcolor" colspan="2">

     <%-- <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>--%>
        <asp:GridView ID="GridUser" runat="server" AutoGenerateColumns="False"  Width="100%" 
                 BorderWidth="1px" CellPadding="3" DataKeyNames="ID" OnRowDataBound="GridUser_RowDataBound"
                 PagerStyle-HorizontalAlign="right"  >
                <%-- OnPageIndexChanging="GridUser_PageIndexChanging"  BorderStyle="None" 
                 OnRowDeleting="GridUser_RowDeleting" OnRowDataBound="GridUser_RowDataBound" AllowPaging="True" --%>
                
                    <HeaderStyle HorizontalAlign="Center"  Font-Bold="True" BackColor="AliceBlue"/>
                        
                        <Columns>
                            <asp:BoundField HeaderText="ID" DataField="ID">
                            </asp:BoundField >
                            
                              <asp:BoundField HeaderText="机组编号" DataField="机组编号">
                            </asp:BoundField >
                           
                            <asp:BoundField HeaderText="机组名称" DataField="机组名称">
                            </asp:BoundField >
                            <asp:BoundField HeaderText="时间" DataField="时间">
                            </asp:BoundField >
                            <asp:BoundField HeaderText="状态" DataField="状态">
                            </asp:BoundField >
                            <asp:BoundField HeaderText="状态描述" DataField="状态描述">
                            </asp:BoundField >
                          <%--   <asp:TemplateField HeaderText=" 角色授权" SortExpression="role_NAME"> 
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" Width="100px" /> 
                            <ItemTemplate>  
                                 <a  href='RoleAssignment.aspx?UserName=<%# DataBinder.Eval(Container, "DataItem.USER_NAME") %>&UserId=<%# DataBinder.Eval(Container, "DataItem.USER_ID") %>&rolename= <%# DataBinder.Eval(Container, "DataItem.role_NAME")%>'>
                                                            【<%# DataBinder.Eval(Container, "DataItem.role_NAME")%>】
                                  </a> 
                            </ItemTemplate>  
                                 <FooterStyle BackColor="Red" />
                            </asp:TemplateField> --%>
                          <%--  <asp:TemplateField HeaderText="修改">
                                <HeaderStyle HorizontalAlign="Center" Width="100px" />
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                                <ItemTemplate>                            
                                    <asp:LinkButton ID="ChangeUser" runat="server" Width="40px" ForeColor="red" CommandName="Change"  OnClick ="lBtnAlter_Click" Text="修改">
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>  --%>    
                            
                     <%--  <asp:TemplateField HeaderText="修改"> 
                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                            <ItemStyle HorizontalAlign="Center" Width="50px" /> 
                            <ItemTemplate>  
                                 <a  href='ChangeUser.aspx?UserName=<%# DataBinder.Eval(Container, "DataItem.USER_NAME") %> '>
                                                            【修改】
                                                        
                                  </a> 
                            </ItemTemplate>  
                                 <FooterStyle BackColor="Red" Wrap="False" />
                            </asp:TemplateField>
                                              
                            <asp:TemplateField HeaderText="删除"> 
                            <HeaderStyle HorizontalAlign="Center" Width="100px" />
                            <ItemStyle HorizontalAlign="Center" Width="100px" /> 
                            <ItemTemplate>  
                                <asp:LinkButton ID="lbtnDel" runat="server" ForeColor="red"  CausesValidation="False" CommandName="Delete"  
                                    Text="删除" OnClientClick='<%#  "if (!confirm(\"你确定要删除" + Eval("USER_NAME").ToString() + "吗?\")) return false;"%>'>
                                </asp:LinkButton>  
                            </ItemTemplate>  
                            </asp:TemplateField> --%>
                             

                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <PagerSettings Visible="False" />
                    </asp:GridView>  
                
                    <br />
                <%--    <asp:label id="lblUserCount" runat="server" Width="130px" Height="16px" Font-Size="9pt"></asp:label><FONT face="宋体">&nbsp; 
				&nbsp; </FONT>
				<asp:label id="lblPageCount" runat="server" Font-Size="9pt" Font-Bold="True"></asp:label><FONT face="宋体">&nbsp;</FONT>
				<asp:label id="lblCurrentIndex" runat="server" Font-Size="9pt" Font-Bold="True"></asp:label><FONT face="宋体">&nbsp;</FONT>
				<asp:linkbutton id="btnFirst" runat="server" ForeColor="navy" CommandArgument="0" Font-size="9pt" OnClick="PagerButtonClick"></asp:linkbutton><FONT face="宋体">&nbsp;</FONT>
				<asp:linkbutton id="btnPrev" runat="server" ForeColor="navy" CommandArgument="prev" Font-size="9pt" OnClick="PagerButtonClick"></asp:linkbutton><FONT face="宋体">&nbsp;</FONT>
				<asp:linkbutton id="btnNext" runat="server" ForeColor="navy" CommandArgument="next" Font-size="9pt" OnClick="PagerButtonClick"></asp:linkbutton><FONT face="宋体">&nbsp;</FONT>
				<asp:linkbutton id="btnLast" runat="server" ForeColor="navy" CommandArgument="last" Font-size="9pt" OnClick="PagerButtonClick"></asp:linkbutton><FONT face="宋体">&nbsp;
                    </FONT>--%>
 <%--</ContentTemplate> 
   </asp:UpdatePanel>--%>
     
     </td>
     
     </tr>
     </table>
    </div>
    </form>
</body>
</html>


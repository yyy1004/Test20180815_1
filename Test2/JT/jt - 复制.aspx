<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jt.aspx.cs" Inherits="Web.JT.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
    <meta http-equiv="pragma" content="no-cache" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
    <script src="/Scripts/loading.js" type="text/javascript"></script>
    <link href="/EasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="/EasyUI/themes/gray/easyui.css" rel="stylesheet" type="text/css" />
    <script src="/EasyUI/jquery.min.js" type="text/javascript"></script>
    <script src="/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/Scripts/distinguishBrowser.js" type="text/javascript"></script>
    <script src="../EasyUI/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=3mAGhMVE0iZtUsxG1Nwevn4cuIDhgEg5"></script>
    <style type="text/css">
        body, html, #allmap {
            width: 100%;
            /*height: 830px;
            overflow: hidden;*/
            margin: 0;
            font-family: "微软雅黑";
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" runat="server">
            <%--  集团图片与百度地图--%>
            <tr>
                <%--图片--%>
                <td width="500px">
                    <div class="imageshow" onclick="ShowPicBtn()">
                        <asp:Image ID="Imagegc" runat="server" Width="500px" Height="400px" />
                    </div>
                    <%--图片选择与保存--%>
                    <div id="picsavebtn" style="display: block" runat="server">
                        <asp:Panel ID="Panel1" runat="server" Width="100%" Visible="true">
                            <asp:FileUpload ID="FileUpload1" runat="server" onchange="readAsDataURL()" />
                            <asp:Button ID="Button1"
                                runat="server" Text="保存" OnClick="Button1_Click" />
                        </asp:Panel>
                    </div>
                    <script type="text/javascript">
                        function readAsDataURL() {
                            var file = document.getElementById('FileUpload1').files[0];
                            var reader = new FileReader();
                            //  alert('<%=Web.gc.WebForm1.GCname %>.png');
                            reader.readAsDataURL(file);//将文件读取为 DataURL
                            reader.onload = function (e) {//文件读取成功完成时触发
                                var result = document.getElementById('Imagegc');
                                result.src = this.result;//this.result 是reader的属性。
                            };
                        }
                    </script>

                    <script type="text/javascript">
                        function ShowPicBtn() {
                            document.getElementById('picsavebtn').style.display = 'block';
                        };
                    </script>
                </td>
                <%--百度地图--%>
                <td width="100%" align="left">
                    <div id="allmap" style="width: 100%; height: 400px;"></div>
                </td>
            </tr>
            <%--集团工厂一览表--%>
            <tr>
                <td width="50%" valign="top" align="right" colspan="2">
                    <table class="table_bordercolor" cellpadding="5" width="100%" border="1" cellspacing="0" style="border-color: #f3f3f3">
                        <tr>

                            <td height="25" class="table_titlebgcolor" align="center" bgcolor="#3cb6e5">
                                <b>集团工厂一览表</b>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:GridView ID="GridUser" runat="server" AutoGenerateColumns="False" Width="100%"
                                    BorderWidth="1px" CellPadding="3" DataKeyNames="工厂名称" OnRowDataBound="GridUser_RowDataBound"
                                    PagerStyle-HorizontalAlign="right">
                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="True" BackColor="AliceBlue" />

                                    <Columns>

                                        <asp:TemplateField HeaderText=" 工厂名称" SortExpression="工厂名称">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                            <ItemTemplate>
                                                <a href='/GC/gc.aspx?node=<%# DataBinder.Eval(Container, "DataItem.菜单号") %>' style="text-decoration: none;">【<%# DataBinder.Eval(Container, "DataItem.工厂名称")%>】
                                                </a>
                                            </ItemTemplate>
                                            <FooterStyle BackColor="Red" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="总机组数" DataField="总机组数"></asp:BoundField>
                                        <asp:BoundField HeaderText="工作机组数" DataField="工作机组数"></asp:BoundField>
                                        <asp:BoundField HeaderText="报警故障机组数" DataField="报警故障机组数"></asp:BoundField>
                                        <asp:BoundField HeaderText="菜单号" DataField="菜单号" Visible="False"></asp:BoundField>

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
                                    <RowStyle ForeColor="#000066" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Center" />
                                    <PagerSettings Visible="False" />
                                </asp:GridView>

                                <br />

                            </td>
                        </tr>
                    </table>




                </td>

            </tr>
        </table>
    </form>
</body>
</html>
<%--百度地图API--%>
<script type="text/javascript">
    // 百度地图API功能
    var map = new BMap.Map("allmap");    // 创建Map实例
    map.centerAndZoom(new BMap.Point(112.338058, 30.304716), 5);  // 初始化地图,设置中心点坐标和地图级别
    //添加地图类型控件
    map.addControl(new BMap.MapTypeControl({
        mapTypes: [
          BMAP_NORMAL_MAP,
          BMAP_HYBRID_MAP
        ]
    }));
    map.setCurrentCity("荆州");          // 设置地图显示的城市 此项是必须设置的
    map.enableScrollWheelZoom(true);     //开启鼠标滚轮缩放

    var x = 112.338058;
    var y = 30.304716;
    var ggPoint = new BMap.Point(x, y);
    //添加gps marker和label
    var markergg = new BMap.Marker(ggPoint);
    map.addOverlay(markergg); //添加GPS marker
    var labelgg = new BMap.Label("荆州市巨鲸传动有限公司", { offset: new BMap.Size(20, -10) });
    markergg.setLabel(labelgg); //添加GPS label

    //      markergg = new BMap.Marker(new BMap.Point(112.225231, 30.341209))
    //      map.addOverlay(markergg);
    //      labelgg = new BMap.Label("长江大学", { offset: new BMap.Size(20, -10) });
    //      markergg.setLabel(labelgg);

    map.addEventListener("click", function (e) {
        //alert('woshi');
        map.centerAndZoom(new BMap.Point(e.point.lng, e.point.lat), map.getZoom() + 1);
    });


    var jtn = new Array();//集团名称
    var jd = new Array();//经度
    var wd = new Array();//维度
      <%
    for (int k = 0; k < wd.Count; k++)
    {
         %>
    jtn.push("<%=jtname[k]%>");
    jd.push("<%=jd[k]%>");
    wd.push("<%=wd[k]%>");
        <%
    }
         %>
    // alert(jtn[0]);
    var i;
    for (i = 0; i < 5; i++) {
        ggPoint = new BMap.Point(jd[i], wd[i]);
        markergg = new BMap.Marker(ggPoint);
        map.addOverlay(markergg);//向地图添加标记。
        labelgg = new BMap.Label(jtn[i], { offset: new BMap.Size(20, -10) });
        markergg.setLabel(labelgg);
    }
</script>

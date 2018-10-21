<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GC.aspx.cs" Inherits="Web.gc.WebForm1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
    <meta http-equiv="pragma" content="no-cache" />
    <link id="MyCss" href="/COMMON/CSS/Default.css" rel="stylesheet" type="text/css" />
    <%--    <script src="/Scripts/loading.js" type="text/javascript"></script>--%>
    <link href="/EasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="/EasyUI/themes/gray/easyui.css" rel="stylesheet" type="text/css" />
    <script src="/EasyUI/jquery.min.js" type="text/javascript"></script>
    <script src="/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/Scripts/distinguishBrowser.js" type="text/javascript"></script>
    <script src="../EasyUI/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        //保存图片
        //$(function () {
        //    $.post("/JZ/picHandler.ashx?action=SavePic", function (data) {
        //        //var data = eval('(' + data + ')');
        //        var data = JSON.parse(data);
        //        var $str = data.pichtml;
        //       //alert("我是立即执行函数");
        //        //$str = preg_replace("/\\\\u([0-9a-f]{3,4})/i", "&#x\\1;", $str);
        //        //$str = html_entity_decode($str, null, 'UTF-8');
        //        $("#picHTMLwrapper").html("").append($str);
        //    });
        //});

        function saveFile11() {
            var filename=<%=GCname%>;//菜单名称
            var uploadFilename = document.getElementById('File').value;//要上传的文件名

            var  savePath = "~/pic/";//要保存的路径
            var saveName = filename + ".png";//保存的文件名。
            var  pathToCheck = savePath + saveName;  //保存的文件名和后缀名
            //  alert(uploadFilename);
            if (uploadFilename==null||uploadFilename=="") {
                alert("文件名为空！！")
    
            }
            else {
                              
            }
        }



    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" runat="server">
            <tr>
                <%--左侧图像部分--%>
                <%--<td height="50%">--%>
                <%--                    <div id="picpic"></div>
                    <div>
                        <input type="file" value="选择" />
                    </div>
                    <div>
                        <button onclick="imageUpload()" >上传</button>
                    </div>
                    <script type="text/javascript">

                       function imageUpload() {
                            $("#picpic").html('<img src="/pic/工厂A1.png" alt="Alternate Text" />')
                        }
                    </script>--%>
                <%--                                <asp:Image ID="Imagegc" runat="server" Width="500px" Height="400px" />
                    <asp:Panel ID="Panel1" runat="server" Width="100%" Visible="false">
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                        <asp:Button ID="Button1" runat="server" Text="上传" OnClick="Button1_Click" />
                    </asp:Panel>--%>
                <%--</td>--%>

                <td style="width: 532px; padding-right: 10px; padding-top: 10px; position: relative;">
                    <%--width="532px" height="402px"--%>
                    <div id="picHTML"></div>
                    <script type="text/javascript">
                        document.getElementById('picHTML').innerHTML='<object  type="text/html" data="/getPicture.aspx"  style="width:532px;height:435px;margin-bottom:10px;" ></object>';
                    </script>
                    <div>
                        <div style="width: 100%;">
                            <!--Echart图表区域-->
                            <div id="myechart" style="height: 300px; width: 528px; border: 1px solid #ccc">
                            </div>
                            <!--单文件导入-->
                        </div>
                    </div>
                    <%-- <asp:Button ID="Button1" runat="server" Text="弹窗1" OnClick="Button1_Click1" />--%>
                </td>
                <%--      <td>
                    <table id="pictest" title="图片"  url="/JZ/testHandler.ashx?action=savePic1"></table>
                </td>--%>
                <%--右侧数据部分--%>
                <td rowspan="2" valign="top" align="left" style="width: 100%; padding-top: 10px">
                    <div>
                        开始时间：
                <input id="dd1" type="text" class="easyui-datebox startdate" />&nbsp;&nbsp;&nbsp; 结束时间：
                <input id="dd2" type="text" class="easyui-datebox enddate" />
                        <input id="bt1" type="button" class="icon-search" onclick="s()" />
                        <script src="../JS/date.js" type="text/javascript"></script>
                        <table id="dg" class="easyui-datagrid" title="设备监测记录" url="/JZ/testHandler.ashx?action=GetAll"
                            width="98%" pagination="true" rownumbers="true" fitcolumns="true" singleselect="true">
                            <thead>
                                <tr>
                                    <th field="id" width="100" data-options="align:'center'">编号
                                    </th>
                                    <th field="jzname" width="150" data-options="align:'center',formatter:gzformatter">机组名称
                                    </th>

                                    <th field="jzid" width="130" data-options="align:'center'">机组编号
                                    </th>
                                    <th field="stime" width="100" data-options="align:'center'">监测时间
                                    </th>
                                    <%--        <th field="ms" width="100" data-options="align:'center'">描述
                                    </th>--%>
                                    <th field="status" width="100" data-options="align:'center',formatter:stateformater,styler:statestyler">状态
                                    </th>
                                    <th field="cdname" width="100" data-options="align:'center'">测点名称
                                    </th>
                                    <th field="cdvalue" width="100" data-options="align:'center'">测点值
                                    </th>
                                    <th field="mid" width="100" hidden="true" data-options="align:'center'">菜单号
                                    </th>
                                </tr>
                            </thead>
                        </table>

                    </div>
                </td>
            </tr>
            <%--         <tr>
                <td>
                           <div style="width: 100%;">
                        <!--Echart图表区域-->
                        <div id="myechart" style="height: 300px; width: 550px;">
                        </div>
                        <!--单文件导入-->

                    </div>
                </td>
            </tr>--%>
        </table>
    </form>

    <script src="/Echarts/echarts-all.js" type="text/javascript"></script>
    <!--配置Echart-->
    <script type="text/javascript">
        var dgtotal = 0;
        var dgwarn = 0;
        var dgstop = 0;

        //更新echart数据
        var myChart = echarts.init(document.getElementById('myechart'));
        //准备数据
        var option = {
            tooltip: {
                show: true
            },
            legend: {//图例名
                data: ['']
            },
            xAxis: [
                {
                    type: 'category',
                    data: ["总机组数", "正常机组", "报警机组", "故障机组"]
                }
            ],
            yAxis: [
                {
                    type: 'value'
                }
            ],
            series: [
                {
                    "name": "工厂",
                    "type": "bar",
                    itemStyle: {
                        normal: {
                            color: function (params) {
                                // build a color map as your need.
                                var colorList = [
          '#0000FF', '#00FF00', '#FFFF00', '#FF0000'
                                ];
                                return colorList[params.dataIndex]
                            },
                            label: {
                                show: true,
                                position: 'top',
                                formatter: '{c}'
                            }
                        }
                    },

                    "data": [<%=jzzs%>,<%=jzzc%>, <%=jzbj%>, <%=jztj%>]
                }
            ]
        };
            //加载数据
            myChart.setOption(option);

    </script>
    <script type="text/javascript">

        function s() {
            $.ajax({
                url: "/JZ/testHandler.ashx?action=GetAllGCJZt",
                type: 'POST',
                data: {
                    gcname: "<%=GCname%>",
                    page: $("#dg").datagrid('options').pageNumber,
                    rows: $("#dg").datagrid('options').pageSize,
                    Start: $('#dd1').datebox('getValue'),
                    End: $('#dd3').datebox('getValue')
                },
                success: function (data) {
                    $.messager.progress('close');
                    data = eval('(' + data + ')');      //将一个json字符串解析成js对象
                    $("#dg").datagrid('loadData', data);
                }

            });
        }
        //状态 转文字
        function stateformater(val, row, index) {
            var status = row.status;
            var res = '';
            if (status == 1) {
                return "<font color='white'>正常</font>";
            } else if (status == 2) {
                return "<font color='black'>报警</font>";
            } else if (status == 3) {
                return "<font color='white'>故障</font>"
            }
        }
        //状态  样式 背景颜色
        function statestyler(val,row,index) {
            var status =row.status;
            if (status==1) {
                return"background-color:blue";
            }else if (status==2) {
                return "background-color:yellow";
            }else {
        return "background-color:red"
            }
        }

    </script>
    <script type="text/javascript">
        //查看详情
        function gzformatter(value, row, index) {
            return "<a href='/JZ/jz.aspx?node=" + row.mid + "' >" + row.jzname + "</a>";
        }

    </script>
</body>
</html>

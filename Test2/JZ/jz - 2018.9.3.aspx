<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jz - 2018.9.3.aspx.cs" Inherits="Web.JZ.jz1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../CSS/jzstatus.css" rel="stylesheet" />
    <meta http-equiv="pragma" content="no-cache" />

    <script src="/Scripts/loading.js" type="text/javascript"></script>
    <link href="/EasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="/EasyUI/themes/gray/easyui.css" rel="stylesheet" type="text/css" />
    <link href="/CSS/jz.css" rel="stylesheet" type="text/css" />
    <script src="/EasyUI/jquery.min.js" type="text/javascript"></script>
    <script src="/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/Scripts/distinguishBrowser.js" type="text/javascript"></script>
    <script src="../EasyUI/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="../JS/jz.js" type="text/javascript"></script>

    <style type="text/css">
        /*清除浮动*/
        .clearfix::after {
            content: "";
            clear: both;
            display: block;
        }

        .aspNetHidden {
            width: 500px;
        }
    </style>

</head>
<body>
    <form id="form1">
        <div class="jz-wrap clearfix">
            <div class="jz-con">
                <%--切换标签--%>
                <div class="easyui-tabs123" id="tabs" style="width: 100%; height: auto;">

                                        <%--机组表复制--%>
                    <div title="运行柱状图" class="jz-instruction" style="padding: 30px;">
                        <div id="right-data1" style="width: 100%;">
                            <div id="jzzzt2" style="width: 800px; margin: auto;border:1px solid red;">
                                      <div class="text-wrap1" style="border:1px solid green;background-image:url(/Images/jz-3D-zhouchen1.png);background-repeat:no-repeat;background-size:80% 60%;background-position:center;">
                                    <div class="top">
                                        <div class="status1 tops status-all easyui-draggable" data-options="handle:'#title1'">
                                            <div class="wrap-name" id="title1">
                                                <div class="name" id="ss1-name">Null</div>
                                            </div>
                                            <div class="wrap-value">
                                                <div class="number-father">
                                                    <div class="number">
                                                        <span>1</span>
                                                    </div>
                                                </div>
                                                <div class="value-father " id="value-father1">
                                                    <span class="value" id="ss1-value">Null</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="status2  tops  status-all easyui-draggable" data-options="handle:'#title2'">
                                            <div class="wrap-name" id="title2">
                                                <div class="name" id="ss2-name">Null</div>
                                            </div>
                                            <div class="wrap-value">
                                                <div class="number-father">
                                                    <div class="number">
                                                        <span>2</span>
                                                    </div>
                                                </div>
                                                <div class="value-father">
                                                    <span class="value" id="ss2-value">Null</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="status3  tops  status-all easyui-draggable" data-options="handle:'#title3'">
                                            <div class="wrap-name" id="title3">
                                                <div class="name" id="ss3-name">Null</div>
                                            </div>
                                            <div class="wrap-value">
                                                <div class="number-father">
                                                    <div class="number">
                                                        <span>3</span>
                                                    </div>
                                                </div>
                                                <div class="value-father">
                                                    <span class="value" id="ss3-value">Null</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="status4  tops  status-all easyui-draggable" data-options="handle:'#title4'">
                                            <div class="wrap-name" id="title4">
                                                <div class="name" id="ss4-name">Null</div>
                                            </div>
                                            <div class="wrap-value">
                                                <div class="number-father">
                                                    <div class="number">
                                                        <span>4</span>
                                                    </div>
                                                </div>
                                                <div class="value-father">
                                                    <span class="value" id="ss4-value">Null</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="bottom">
                                        <div class="status5 bottoms status-all easyui-draggable" data-options="handle:'#title5'">
                                            <div class="wrap-name" id="title5">
                                                <div class="name" id="ss5-name">Null</div>
                                            </div>
                                            <div class="wrap-value">
                                                <div class="number-father">
                                                    <div class="number">
                                                        <span>5</span>
                                                    </div>
                                                </div>
                                                <div class="value-father">
                                                    <span class="value" id="ss5-value">Null</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="status6 bottoms status-all easyui-draggable" data-options="handle:'#title6'">
                                            <div class="wrap-name" id="title6">
                                                <div class="name" id="ss6-name">Null</div>
                                            </div>
                                            <div class="wrap-value">
                                                <div class="number-father">
                                                    <div class="number">
                                                        <span>6</span>
                                                    </div>
                                                </div>
                                                <div class="value-father">
                                                    <span class="value" id="ss6-value">Null</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="status7 bottoms status-all easyui-draggable" data-options="handle:'#title7'">
                                            <div class="wrap-name" id="title7">
                                                <div class="name" id="ss7-name">Null</div>
                                            </div>
                                            <div class="wrap-value">
                                                <div class="number-father">
                                                    <div class="number">
                                                        <span>7</span>
                                                    </div>
                                                </div>
                                                <div class="value-father">
                                                    <span class="value" id="ss7-value">Null</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="status8 bottoms status-all easyui-draggable" data-options="handle:'#title8'">
                                            <div class="wrap-name" id="title8">
                                                <div class="name" id="ss8-name">Null</div>
                                            </div>
                                            <div class="wrap-value">
                                                <div class="number-father">
                                                    <div class="number">
                                                        <span>8</span>
                                                    </div>
                                                </div>
                                                <div class="value-father">
                                                    <span class="value" id="ss8-value">Null</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%--机组数据刷新--%>
                            <script type="text/javascript">
                                document.getElementById("jzzzt").innerHTML = '<object type="text/html" data="/JZ/jzzzt.aspx" width="100%" height="600px;"></object>';
                                $(function () {
                                    setInterval("GetJZZZTHtml()", 5000);
                                });
                                function GetJZZZTHtml() {
                                    document.getElementById("jzzzt").innerHTML = '<object type="text/html" data="/JZ/jzzzt.aspx" width="100%" height="600px;"></object>';
                                }
                            </script>
                            <script type="text/javascript">
                                document.getElementById("jzzzt1").innerHTML = '<object type="text/html" data="/JZ/jzzzt.aspx" width="100%" height="600px;"></object>';
                                $(function () {
                                    setInterval("GetJZZZTHtml()", 5000);
                                });
                                function GetJZZZTHtml() {
                                    document.getElementById("jzzzt1").innerHTML = '<object type="text/html" data="/JZ/jzzzt.aspx" width="100%" height="600px;"></object>';
                                }
                            </script>
                        </div>
                    </div>

                    <%--  实时数据--%>
                    <div class="tabs-wrap" title="设备运行记录" style="padding: 10px;">
                        <div class="wrap1-data">
                            <div class="number-con">
                                <div class="jz-img-wrap">
                                    <img src="../Images/jz-3D-zhouchen1.png" alt="" />
                                </div>
                                <div class="text-wrap">
                                    <%--                        <div class="easyui-draggable11"  id="div2" onmousedown="mouseDown(this,event)" onmousemove="mouseMove(event)" onmouseup="mouseUp(event)" style="position:absolute;z-index:1000; width:200px;height:150px;background:#fafafa;border:1px solid #ccc"></div>--%>
                                    <div class="top">
                                        <div class="status9 tops status-all easyui-draggable" data-options="handle:'#title9'">
                                            <%--onmousedown="mouseDown(this,event)" onmousemove="mouseMove(event)" onmouseup="mouseUp(event)" >--%>
                                            <div class="wrap-name" id="title9">
                                                <div class="name" id="ss9-name">启停</div>
                                            </div>
                                            <div class="wrap-value">
                                                <div class="number-father">
                                                    <div class="number">
                                                        <span>9</span>
                                                    </div>
                                                </div>
                                                <div class="value-father">
                                                    <span class="value" id="ss9-value">Null</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="status1 tops status-all easyui-draggable" data-options="handle:'#title1'">
                                            <%--onmousedown="mouseDown(this,event)" onmousemove="mouseMove(event)" onmouseup="mouseUp(event)" >--%>
                                            <div class="wrap-name" id="title1">
                                                <div class="name" id="ss1-name">Null</div>
                                            </div>
                                            <div class="wrap-value">
                                                <div class="number-father">
                                                    <div class="number">
                                                        <span>1</span>
                                                    </div>
                                                </div>
                                                <div class="value-father " id="value-father1">
                                                    <span class="value" id="ss1-value">Null</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="status2  tops  status-all easyui-draggable" data-options="handle:'#title2'">
                                            <div class="wrap-name" id="title2">
                                                <div class="name" id="ss2-name">Null</div>
                                            </div>
                                            <div class="wrap-value">
                                                <div class="number-father">
                                                    <div class="number">
                                                        <span>2</span>
                                                    </div>
                                                </div>
                                                <div class="value-father">
                                                    <span class="value" id="ss2-value">Null</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="status3  tops  status-all easyui-draggable" data-options="handle:'#title3'">
                                            <div class="wrap-name" id="title3">
                                                <div class="name" id="ss3-name">Null</div>
                                            </div>
                                            <div class="wrap-value">
                                                <div class="number-father">
                                                    <div class="number">
                                                        <span>3</span>
                                                    </div>
                                                </div>
                                                <div class="value-father">
                                                    <span class="value" id="ss3-value">Null</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="status4  tops  status-all easyui-draggable" data-options="handle:'#title4'">
                                            <div class="wrap-name" id="title4">
                                                <div class="name" id="ss4-name">Null</div>
                                            </div>
                                            <div class="wrap-value">
                                                <div class="number-father">
                                                    <div class="number">
                                                        <span>4</span>
                                                    </div>
                                                </div>
                                                <div class="value-father">
                                                    <span class="value" id="ss4-value">Null</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="bottom">


                                        <div class="status10 bottoms status-all easyui-draggable" data-options="handle:'#title10'">
                                            <div class="wrap-name" id="title10">
                                                <div class="name" id="ss10-name">转速</div>
                                            </div>
                                            <div class="wrap-value">
                                                <div class="number-father">
                                                    <div class="number">
                                                        <span>0</span>
                                                    </div>
                                                </div>
                                                <div class="value-father">
                                                    <span class="value" id="ss10-value">Null</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="status5 bottoms status-all easyui-draggable" data-options="handle:'#title5'">
                                            <div class="wrap-name" id="title5">
                                                <div class="name" id="ss5-name">Null</div>
                                            </div>
                                            <div class="wrap-value">
                                                <div class="number-father">
                                                    <div class="number">
                                                        <span>5</span>
                                                    </div>
                                                </div>
                                                <div class="value-father">
                                                    <span class="value" id="ss5-value">Null</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="status6 bottoms status-all easyui-draggable" data-options="handle:'#title6'">
                                            <div class="wrap-name" id="title6">
                                                <div class="name" id="ss6-name">Null</div>
                                            </div>
                                            <div class="wrap-value">
                                                <div class="number-father">
                                                    <div class="number">
                                                        <span>6</span>
                                                    </div>
                                                </div>
                                                <div class="value-father">
                                                    <span class="value" id="ss6-value">Null</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="status7 bottoms status-all easyui-draggable" data-options="handle:'#title7'">
                                            <div class="wrap-name" id="title7">
                                                <div class="name" id="ss7-name">Null</div>
                                            </div>
                                            <div class="wrap-value">
                                                <div class="number-father">
                                                    <div class="number">
                                                        <span>7</span>
                                                    </div>
                                                </div>
                                                <div class="value-father">
                                                    <span class="value" id="ss7-value">Null</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="status8 bottoms status-all easyui-draggable" data-options="handle:'#title8'">
                                            <div class="wrap-name" id="title8">
                                                <div class="name" id="ss8-name">Null</div>
                                            </div>
                                            <div class="wrap-value">
                                                <div class="number-father">
                                                    <div class="number">
                                                        <span>8</span>
                                                    </div>
                                                </div>
                                                <div class="value-father">
                                                    <span class="value" id="ss8-value">Null</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <%--   机组运行记录--%>
                    <div title="设备运行记录" style="padding: 10px; width: auto;">
                        开始时间：
                                    <input id="dd1" type="text" class="easyui-datebox startdate" value="2011-11-11" style="width: 120px;" />&nbsp;&nbsp;&nbsp; 结束时间：
                                    <input id="dd2" type="text" class="easyui-datebox enddate" value="2011-11-11" style="width: 120px;" />
                        <input id="bt1" type="button" class="icon-search" onclick="s()" />
                        <%-- 下载数据--%>
                        <a href="#" class="easyui-linkbutton" plain="true" onclick="toExcel()" data-options="iconCls: 'icon-save'">下载测点数据</a>
                        <%-- 设备运行记录--%>
                        <table id="dg" class="easyui-datagrid" title="设备运行记录" url="/JZ/testHandler.ashx?action=GetAlljz"
                            width="98%" pagination="true" rownumbers="true" fitcolumns="true" singleselect="true">
                            <thead>
                                <tr>
                                    <th field="id" width="100" data-options="align:'center'">编号
                                    </th>
                                    <th field="jzname" width="150" data-options="align:'center'">机组名称
                                    </th>
                                    <th field="jzid" width="130" data-options="align:'center'">机组编号
                                    </th>
                                    <th field="stime" width="100" data-options="align:'center'">提交时间
                                    </th>
                                    <th field="ms" width="100" data-options="align:'center'">描述
                                    </th>
                                    <th field="status" width="100" data-options="align:'center'">状态
                                    </th>
                                    <th field="cdname" width="100" data-options="align:'center'">测点名称
                                    </th>
                                    <th field="cdvalue" width="100" data-options="align:'center'">测点值
                                    </th>
                                    <th field="mid" width="100" hidden="true" data-options="align:'center' ">菜单号
                                    </th>
                                </tr>
                            </thead>
                        </table>
                    </div>
                    <script src="../JS/date.js" type="text/javascript"></script>
                    <%-- 机组文档--%>
                    <div title="机组文档" style="padding: 0px; width: auto">
                        <div id="newDiv"></div>
                        <script type="text/javascript">
                            document.getElementById("newDiv").innerHTML = '<object type="text/html" data="/File/Show2.html" width="100%" height="500px"></object>';
                        </script>
                    </div>
                    <%--机组图表--%>
                    <div title="运行柱状图" class="jz-instruction" style="padding: 30px;">
                        <div id="right-data" style="width: 100%;">
                            <div id="jzzzt" style="width: 600px; margin: auto;"></div>
                            <%--机组数据刷新--%>
                            <script type="text/javascript">
                                document.getElementById("jzzzt").innerHTML = '<object type="text/html" data="/JZ/jzzzt.aspx" width="100%" height="600px;"></object>';
                                $(function () {
                                    setInterval("GetJZZZTHtml()", 5000);
                                });
                                function GetJZZZTHtml() {
                                    document.getElementById("jzzzt").innerHTML = '<object type="text/html" data="/JZ/jzzzt.aspx" width="100%" height="600px;"></object>';
                                }
                            </script>
                        </div>
                    </div>

                </div>
                <%--自定义tab--%>
                <script type="text/javascript">
                    //初始化
                    $(function () {
                        $("#tabs").tabs({
                            plain: false,
                            boder: false,
                            width: $(window).width(),
                            height: $(window).height()
                        })
                    });
                    //监测浏览器
                    $(window).resize(function () {
                        $('#tabs').tabs({
                            plain: false,
                            boder: false,
                            width: $(window).width(),
                            height: $(window).height(),
                        }).tabs('resize');
                    })
                </script>
            </div>
        </div>
    </form>

    <script type="text/javascript">
        //数据导出
        function toExcel() {
            $.ajax({
                url: '/JZ/JZHandler.ashx',
                type: 'POST',
                data: {
                    action: 'ToExcel',
                    JZId: '<%=JZBH%>',
                    Start: $('#dd1').datebox('getValue'),
                    End: $('#dd2').datebox('getValue')
                },
                beforeSend: function () {
                    $.messager.progress({
                        text: '正在处理中...'
                    })
                },
                success: function (data) {
                    $.messager.progress('close');
                    //                    if (getBrowserType() == "Chrome") {
                    //                        location.href = data; //谷歌文件下载方式
                    //                    } else {
                    //                        window.open(data); //ie文件下载方式
                    //                    }
                    download(data);
                }
            });
        }
        //下载文件
        function download(url) {
            var iframe = document.createElement("iframe")
            iframe.style.display = "none";
            iframe.src = url;
            document.body.appendChild(iframe);
        }

        function s() {
            $.ajax({
                url: "/JZ/testHandler.ashx?action=GetAllJZt",
                type: 'POST',
                data: {
                    gcname: "<%=JZBH%>",
                    page: $("#dg").datagrid('options').pageNumber,
                    rows: $("#dg").datagrid('options').pageSize,
                    Start: $('#dd1').datebox('getValue'),
                    End: $('#dd2').datebox('getValue')
                },
                success: function (data) {
                    $.messager.progress('close');
                    data = eval('(' + data + ')');      //将一个json字符串解析成js对象
                    $("#dg").datagrid('loadData', data);
                }

            });
        }

    </script>
    <script src="../JS/MouseM.js" type="text/javascript"></script>
</body>
</html>

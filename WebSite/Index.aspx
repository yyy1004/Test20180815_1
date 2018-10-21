<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Web.Index1" %>
<!DOCTYPE html>
<html>
<head>
    <title>后台管理系统</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="/Scripts/loading.js" type="text/javascript"></script>
    <link href="/EasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="/EasyUI/themes/gray/easyui.css" rel="stylesheet" type="text/css" />
    <script src="/EasyUI/jquery.min.js" type="text/javascript"></script>
    <script src="/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/Scripts/distinguishBrowser.js" type="text/javascript"></script>
    <script src="EasyUI/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        //树形菜单的点击事件
        $(function () {
            $("#treeMenu").tree({
                onClick: function (node) {
                    if (node.attributes !== undefined && node.attributes.url !== undefined) {
                        if (node.attributes.url != '') {
                            //跳转到url指定的页面
                            url = node.attributes.url + "?node=" + node.id;
                            //使iframe刷新
                            document.getElementById('myiframe').src = url;
                        }
                    }
                }
            });
            //周期性调用日期更新函数
            setInterval("GetTime()", 1000);
        });
        //获取系统时间
        function GetTime() {
            var mon, day, now, hour, min, ampm, time, str, tz, end, beg, sec;
            mon = new Array("1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "9月", "10月", "11月", "12月");
            day = new Array("周日", "周一", "周二", "周三", "周四", "周五", "周六");
            now = new Date();
            hour = now.getHours();
            min = now.getMinutes();
            sec = now.getSeconds();
            if (hour < 10) {
                hour = "0" + hour;
            }
            if (min < 10) {
                min = "0" + min;
            }
            if (sec < 10) {
                sec = "0" + sec;
            }
            $("#Timer").html(now.getFullYear() + "年" + mon[now.getMonth()] + now.getDate() + "日, " + day[now.getDay()]
            + ", " + hour + ":" + min + ":" + sec);

        }
    </script>
</head>
<body>
    <div class="easyui-layout" style="width: 99.9%; height: 700px;">
        <div region="north" split="true" style="height: 150px; padding: 10px 10px; width: 100%;
            position: relative">
            <div style="position: absolute; right: 10px; bottom: 10px">
                <span id="Timer"></span>【<%=Session["角色"]%>:<%=Session["用户名"]%>】
            </div>
        </div>
        <div region="west" split="true" title="菜单" style="width: 270px; padding: 10px 20px;">
            <ul class="easyui-tree" id="treeMenu" data-options="url:'/Menu/GetMenu.ashx',method:'get',animate:true,lines:true">
            </ul>
        </div>
        <div id="content" region="center" title="详情" style="padding: 5px; width: 100%">
            <iframe id="myiframe" name="myiframe" iframe src="Index.html" width="100%" height="98%" frameborder="0">
            </iframe>
        </div>
    </div>
</body>
</html>
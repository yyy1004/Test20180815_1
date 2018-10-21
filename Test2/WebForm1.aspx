<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Web.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
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
                    $(function () {
                        $.post("/JZ/picHandler.ashx?action=SavePic", function (data) {
                            //var data = eval('(' + data + ')');
                            var data = JSON.parse(data);
                            $("#picHTML").html("").append(data.pichtml);
                        });
                    });
                </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
                        <div id="picHTML">
                    </div>
    </div>
    </form>
</body>
</html>

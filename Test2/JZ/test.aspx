<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="Web.JZ.test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>测试</title>
  <meta http-equiv="pragma" content="no-cache" />
    <script src="/Scripts/loading.js" type="text/javascript"></script>
    <link href="/EasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="/EasyUI/themes/gray/easyui.css" rel="stylesheet" type="text/css" />
    <script src="/EasyUI/jquery.min.js" type="text/javascript"></script>
    <script src="/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/Scripts/distinguishBrowser.js" type="text/javascript"></script>
    <script src="../EasyUI/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <table id="dg" class="easyui-datagrid" title="诊断报告" url="/JZ/testHandler.ashx?action=GetAll"
        width="98%" pagination="true" rownumbers="true" fitcolumns="true" singleselect="true">
        <thead>
            <tr>
                <th field="id" width="100" data-options="align:'center'">
                    编号
                </th>
                <th field="jzname" width="150" data-options="align:'center'">
                    机组名称
                </th>
                <th field="jzid" width="130" data-options="align:'center'">
                    机组编号
                </th>
                <th field="stime" width="100" data-options="align:'center'">
                    提交时间
                </th>
                <th field="ms" width="100" data-options="align:'center'">
                   描述
                </th>
                <th field="status" width="100" data-options="align:'center'">
                    状态
                </th>
            </tr>
        </thead>
    </table>
    
    </div>
    </form>
</body>
</html>

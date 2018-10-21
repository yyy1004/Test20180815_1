<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Web.Index1" %>

<!DOCTYPE html>
<html>
<head>
    <title>巨鲸齿轮箱在线监测系统</title>
    <link rel="shortcut icon" href="/Images/slo.ico" type="image/x-icon" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="CSS/indexAspx.css" rel="stylesheet" type="text/css" />
    <link href="/EasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="/EasyUI/themes/gray/easyui.css" rel="stylesheet" type="text/css" />

    <script src="/EasyUI/jquery.min.js" type="text/javascript"></script>
    <script src="/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/Scripts/distinguishBrowser.js" type="text/javascript"></script>
    <script src="EasyUI/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="JS/IndexAspx.js" type="text/javascript"></script>
    <script src="/Scripts/loading.js" type="text/javascript"></script>


    <%--退出按钮--%>
    <script type="text/javascript">
        function myexit() {
            document.getElementById("btn1hidden").click();
        };
        //打开对话框dialog
        function myinfo() {
            $('#dlg').dialog('open');
            $('#myform').form('clear');//清除表单内容
        }
        //设置dialog
        $(function () {
            $('#dlg').dialog({
                //width: 600,
                //height: 400,
                modal: true,
                iconCls: 'icon-save',
                resizable: true,
                modal: true,
                title: '修改密码',
                closed: true,//开始不打开
                cache: false,
                //href: 'get_content.php',
            });

            //对话框dialog水平居中
            $('#dlg').window('center');
        });
        //关闭对话框dialgo
        function closedlg() {
            $('#dlg').dialog('close');
        }
        //保存
        function saveinfo() {
            var url = '/User/UserHandler.ashx?action=UpdatePwd'
            $.ajax({
                url: url,
                type: 'POST',
                async: false,//同步请求   同步请求将锁住浏览器，用户其它操作必须等待请求完成才可以执行
                cache: false,//不需要缓存
                processData: false, //不要去处理发送的数据
                contentType: false,// 告诉jQuery不要去设置Content-Type请求头
                //data: {
                //    'Id': 100120,
                //    'Password': 1111,
                //},
                data: new FormData($('#myform')[0]),
                beforeSend: function () {
                    $.messager.progress({
                        text: '正在处理中...'
                    });
                    return $('#myform').form('validate');
                },
                success: function (data) {
                    $.messager.progress('close');
                    data = eval('(' + data + ')');      //将一个json字符串解析成js对象
                    $.messager.alert("操作提示", data.Message, 'info'); //显示后台信息
                    //$.messager.show({
                    //    title: '操作提示',
                    //    msg: data.Message
                    //});
                    if (data.Success) {
                        $('#dlg').dialog('close'); 	    //关闭弹出框
                    } else {
                        $("#myform").form('clear');
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(XMLHttpRequest.status);
                    alert(XMLHttpRequest.readyState);
                    alert(textStatus);
                }

            });
        }
    </script>

</head>
<body>
    <%--dialog--%>
    <div id="dlg" class="easyui-dialog" style="width: 450px; padding: 5px 25px" buttons="#dlg-btn">
        <form id="myform" method="post" enctype="multipart/form-data">
            <div style="margin: 5px 0px"><span>用户名：</span> <span id="uname"><%=Session["用户名"]%></span></div>
            <div style="margin-bottom: 8px">
                <input id="oldpwd" class="easyui-textbox" name="oldpwd" type="text" label="原密码：" style="width: 133%"
                    data-options="valueField:'id',textField:'text',required:true">
                <%--required:字段是否为必填项--%>
            </div>
            <div style="margin-bottom: 8px">
                <input id="newpwd" class="easyui-textbox" name="newpwd" type="text" label="新密码：" style="width: 133%"
                    data-options="valueField:'id',textField:'text',required:true">
            </div>
            <div style="margin-bottom: 8px">
                <input id="rnewpwd" class="easyui-textbox" name="rnewpwd" type="text" label="请再输入新密码：" style="width: 133%"
                    data-options="valueField:'id',textField:'text',required:true">
            </div>
        </form>
    </div>
    <%--改密码弹窗--%>
    <div id="dlg-btn" style="padding: 10px 10px">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveinfo()">保存</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="closedlg()">取消</a>
    </div>

    <div id="mylayout" class="easyui-layout" style="width: 100%; height: 100%;">
        <%--  头部区域--%>
        <div region="north" class="north-wrap" split="true" style="height: 100px; width: 100%; overflow: hidden;">
            <div class="head">
                <div class="head-jjimg">
                    <img src="/images/head-gc.png" alt="" />

                </div>
                <div class="head-color">
                    <div style="width: 820px;">
                        <div class="hd-logo-wrap">
                            <div class="hd-logo">
                                <img src="/images/JJ-logo.png" alt="Alternate Text" />
                            </div>
                        </div>
                        <div class="pipe"></div>
                        <div class="txt-wrap">
                            <div class="hd-txt">
                                <div class="hd-txt1">
                                    <div>齿轮箱在线监测系统</div>
                                </div>
                            </div>
                            <div class="hd-txten">
                                <div class="hed-txten1">
                                   ONLINE MONITORING SYSTEM FOR GEAR BOX 
                                </div>

                            </div>
                        </div>

                    </div>

                </div>
            </div>
        </div>
        <%--左边菜单区域--%>
        <div region="west" split="true" id="west-tree" title="菜单" style="width: 270px; padding: 10px 20px;">
            <ul id="treeMenu" data-options="url:'/Menu/GetMenu.ashx',method:'get',animate:true,lines:true">
                <%--//删除 class="easyui-tree"避免加载两次--%>
            </ul>
        </div>
        <%--右边内容区域--%>
        <div id="content" region="center" title="巨鲸在线监测" style="padding: 5px; /*height: 1800px; */ width: 100%">

            <%--退出与用户信息 按钮--%>
            <div class="infoandexit ">
                <div class="exit easyui-linkbutton" plain="true">
                    <a href="#" onclick="myexit()">
                        <span>退出</span>
                    </a>
                </div>
                <div class="info">
                    <a href="#" onclick="myinfo()" class="easyui-linkbutton" plain="true">
                        <span id="Timer1"></span>
                        <span class="role"><%=Session["角色"]%>：<%=Session["用户名"]%></span>
                    </a>
                </div>
            </div>

            <%--动态内容区--%>
            <iframe id="myiframe" name="myiframe" iframe src="Index.html" width="100%" height="99%" style="overflow: hidden;" frameborder="0"></iframe>
        </div>
        <%--页脚--%>
        <div class="footer" data-options="region:'south',border:true," style="width:100%;height:30px;">
            <div class="footer-wrap">
                <div class="con">
                    <%--         <div class="technical-support mymargin"><span>技术支持：长江大学</span></div>--%>
                    <div class="owner mymargin"><span>巨鲸传动 ©2018-2023  版权所有</span></div>
                    <a class="www mymargin" href="http://www.jujingcd.com.cn">www.jujingcd.com.cn</a>
                    <%--  <div class="tel mymargin">0716-8303811</div>--%>
                </div>

            </div>
        </div>

    </div>

    <%--服务器控件 隐藏--%>
    <form runat="server">
        <asp:Button ID="btn1hidden" runat="server" Text="Button" Style="display: none" Width="0" Height="0" OnClick="btn1hidden_Click" />
    </form>

</body>
</html>

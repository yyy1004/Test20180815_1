<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Show.aspx.cs" Inherits="Web.User.Show" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>用户列表</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content" region="center" title="用户列表" style="padding: 5px;">
        <table id="dg" class="easyui-datagrid" url="/User/UserHandler.ashx?action=GetAll"
            toolbar="#toolbar" rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th field="Id" width="50">
                        用户编号
                    </th>
                    <th field="Name" width="50">
                        用户名
                    </th>
                    <th field="Password" width="50">
                        密码
                    </th>
                    <th field="Email" width="50">
                        电子邮箱
                    </th>
                    <th field="Phonenumber" width="50">
                        电话号码
                    </th>
                    <th field="Role" width="50">
                        角色名称
                    </th>
                    <th field="JT" width="50">
                        集团名称
                    </th>
                    <th field="GC" width="50">
                        工厂名称
                    </th>
                    <th field="Note" width="50">
                        备注
                    </th>
                </tr>
            </thead>
        </table>
        <div id="toolbar">
            <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="addUser()">
                添加</a> <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="editUser()">
                    编辑</a> <a href="#" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="deleteUser()">
                        删除</a>
        </div>
        <%--对话框--%>
        <div id="dlg" class="easyui-dialog" style="width: 400px; padding: 10px 20px" closed="true"
            buttons="#dlg-buttons">
            <form id="fm" method="post">
            <div style="margin-bottom: 20px">
                <input id="uid" class="easyui-textbox" name="Id" type="text" style="width: 100%"
                    data-options="label:'用户编号:',required:true">
            </div>
            <div style="margin-bottom: 20px">
                <input id="uname" class="easyui-textbox" name="Name" style="width: 100%" data-options="label:'用户名:',required:true">
            </div>
            <div style="margin-bottom: 20px">
                <input id="upassword" class="easyui-textbox" name="Password" style="width: 100%"
                    data-options="label:'密码:',required:true">
            </div>
            <div style="margin-bottom: 20px">
                <input id="email" class="easyui-textbox" name="Email" style="width: 100%" data-options="label:'电子邮箱:',required:true,validType:'email'">
            </div>
            <div style="margin-bottom: 20px">
                <input id="phonenumber" class="easyui-textbox" name="Phonenumber" style="width: 100%"
                    data-options="label:'电话号码:',required:true">
            </div>
            <div style="margin-bottom: 20px">
                <input id="cc1" class="easyui-combobox" name="Role" label="角色" style="width: 100%"
                    data-options="valueField:'id',textField:'text',url:'/Role/RoleHandler.ashx?action=GetAll'">
            </div>
            <div style="margin-bottom: 20px">
                <input id="cc2" class="easyui-combobox" name="JT" label="所属集团" style="width: 100%"
                    data-options="valueField: 'id', textField: 'text', url: '/JT/JTHandler.ashx?action=GetAll',
                    onSelect: function(rec){
                        var url = '/GC/GCHandler.ashx?action=GetGC&jt=' + rec.id;
                        $('#cc3').combobox('reload', url);
                    }" />
            </div>
            <div style="margin-bottom: 20px">
                <input id="cc3" class="easyui-combobox" name="GC" label="所属工厂" style="width: 100%"
                    data-options="valueField:'id',textField:'text', url: '/GC/GCHandler.ashx?action=GetAll'">
            </div>
            </form>
        </div>
        <div id="dlg-buttons">
            <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveUser()">保存</a>
            <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">
                取消</a>
        </div>
        <script type="text/javascript">
            function editUser() {
                var row = $('#dg').datagrid('getSelected');
                if (row) {
                    $('#dlg').dialog('open').dialog('setTitle', '编辑用户信息');
                    $('#fm').form('load', row);
                }
                url1 = '/User/UserHandler.ashx?action=Update';
            }
            function addUser() {
                $('#dlg').dialog('open').dialog('setTitle', '添加用户信息');
                url1 = '/User/UserHandler.ashx?action=Add';
            }
            function saveUser() {
                var row = $('#dg').datagrid('getSelected');
                $('#fm').form('submit', {
                    url: url1,
                    onSubmit: function () {
                        //验证表单是否合法
                        return $(this).form('validate');
                    },
                    success: function (result) {
                        console.log(result);
                        if (result.errorMsg) {              //打印出错误信息
                            $.messager.show({
                                title: 'Error',
                                msg: result.errorMsg
                            });
                        } else {
                            $('#dlg').dialog('close'); 	    // 关闭弹出框
                            $('#dg').datagrid('reload');    // 重新载入数据
                        }
                    }
                });
            }
            function deleteUser() {
                var row = $('#dg').datagrid('getSelected');
                if (row) {
                    $.messager.confirm("确认删除", "您确定要删除当前用户[" + row.Name + "]的信息吗？", function (r) {
                        if (r) {
                            //用户确定删除
                            $.get('/User/UserHandler.ashx', { Id: row.Id, action: "Delete" }, function (res) {
                                if (res) {
                                    $("#dg").datagrid('reload');
                                } else {
                                    $.messager.show({
                                        title: '错误',
                                        msg: "删除当前用户失败！   " + res.errorMsg
                                    });
                                }
                            });
                        }
                    });
                }
            }
        </script>
    </div>
</asp:Content>

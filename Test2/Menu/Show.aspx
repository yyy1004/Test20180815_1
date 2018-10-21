<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Show.aspx.cs" Inherits="Web.Menu.Show" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>系统菜单管理</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content" region="center" title="菜单列表" style="padding: 5px;">
        <table id="dg" class="easyui-datagrid" url="/Menu/MenuHandler.ashx?action=GetAll"
            toolbar="#toolbar" rownumbers="true" fitcolumns="true" singleselect="true">
            <thead>
                <tr>
                    <th field="Id" width="50">
                        菜单号
                    </th>
                    <th field="Name" width="50">
                        菜单名称
                    </th>
                    <th field="ParentId" width="50">
                        父菜单号
                    </th>
                    <th field="Num" width="50">
                        菜单序号
                    </th>
                    <th field="Disc" width="50">
                        菜单描述
                    </th>
                    <th field="Url" width="50">
                        URL
                    </th>
                    <th field="Ico" width="50">
                        菜单图标
                    </th>
                    <th field="Level" width="50">
                        菜单等级
                    </th>
                </tr>
            </thead>
        </table>
        <div id="toolbar">
            <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="true" onclick="addMenu()">
                添加</a> <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="true" onclick="editMenu()">
                    编辑</a> <a href="#" class="easyui-linkbutton" iconcls="icon-remove" plain="true" onclick="deleteMenu()">
                        删除</a>
        </div>
        <%--对话框--%>
        <div id="dlg" class="easyui-dialog" style="width: 400px; padding: 10px 20px" closed="true"
            buttons="#dlg-buttons">
            <form id="fm" method="post">
            <div style="margin-bottom: 20px">
                <input id="Id" class="easyui-textbox" name="Id" type="text" style="width: 100%"
                    data-options="label:'菜单编号:',required:true">
            </div>
            <div style="margin-bottom: 20px">
                <input id="Name" class="easyui-textbox" name="Name" style="width: 100%" data-options="label:'菜单名称:',required:true">
            </div>
            <div style="margin-bottom: 20px">
                <input id="Parentid" class="easyui-textbox" name="ParentId" style="width: 100%" data-options="label:'父菜单号:',required:true">
            </div>
            <div style="margin-bottom: 20px">
                <input id="Num" class="easyui-textbox" name="Num" style="width: 100%" data-options="label:'菜单序号:',required:true">
            </div>
            <div style="margin-bottom: 20px">
                <input id="Disc" class="easyui-textbox" name="Disc" style="width: 100%" data-options="label:'菜单描述:',required:true">
            </div>
            <div style="margin-bottom: 20px">
                <input id="Url" class="easyui-textbox" name="Url" label="URL" style="width: 100%"
                    data-options="required:true">
            </div>
            <div style="margin-bottom: 20px">
                <input id="Ico" class="easyui-textbox" name="Ico" label="菜单图标" style="width: 100%"
                    data-options="required:true" />
            </div>
            <div style="margin-bottom: 20px">
                <input id="Level" class="easyui-textbox" name="Level" label="菜单等级" style="width: 100%"
                    data-options="required:true">
            </div>
            </form>
        </div>
        <div id="dlg-buttons">
            <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveMenu()">保存</a>
            <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">
                取消</a>
        </div>
        <script type="text/javascript">
            function editMenu() {
                var row = $('#dg').datagrid('getSelected');
                if (row) {
                    $('#dlg').dialog('open').dialog('setTitle', '编辑菜单信息');
                    $('#fm').form('load', row);
                }
                url1 = '/Menu/MenuHandler.ashx?action=Update';
            }
            function addMenu() {
                $('#dlg').dialog('open').dialog('setTitle', '添加菜单信息');
                url1 = '/Menu/MenuHandler.ashx?action=Add';
            }
            function saveMenu() {
                var row = $('#dg').datagrid('getSelected');
                $('#fm').form('submit', {
                    url: url1,//注意添加和修改的url不同
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
            function deleteMenu() {
                var row = $('#dg').datagrid('getSelected');
                if (row) {
                    $.messager.confirm("确认删除", "您确定要删除当前菜单[" + row.Name + "]的信息吗？", function (r) {
                        if (r) {
                            //用户确定删除
                            $.get('/Menu/MenuHandler.ashx', { Id: row.Id, action: "Delete" }, function (res) {
                                if (res) {
                                    $("#dg").datagrid('reload');
                                } else {
                                    $.messager.show({
                                        title: '错误',
                                        msg: "删除当前菜单失败！   " + res.errorMsg
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

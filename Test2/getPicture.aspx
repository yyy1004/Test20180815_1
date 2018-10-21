<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="getPicture.aspx.cs" Inherits="Web.getPicture" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%--    <link href="/EasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="/EasyUI/themes/gray/easyui.css" rel="stylesheet" type="text/css" />--%>
    <script src="/EasyUI/jquery.min.js" type="text/javascript"></script>
    <script src="/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/Scripts/distinguishBrowser.js" type="text/javascript"></script>
    <%--    <script src="../EasyUI/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>--%>
</head>
<body style="margin:0">
    <%-- <form id="form1" runat="server">--%>
    <div style="width: 100%; height: 412px;">
        <div style="width:532px;height:412px;">
            <%--图片显示HTML  开始--%>
            <div style="margin: 0 0 10px 0; width: 530px; height: 400px; border: 1px solid #ccc;">
                <img src="../pic//<%=Web.gc.WebForm1.GCname %>.png" alt="" id="img"  style="margin-top: 0px;width:530px;height:402px" onclick="showbtn()" />
            </div>

            <%--上传按钮HTML 开始--%>
            <div class="dopic" id="dopic" style="display:none ;width:530px;">
                <div class="uploadpic" style="display:inline-block"  >
                    <input id="File" type="file"  name="unFile" onchange="readAsDataURL()" accept="image/png, image/gif, image/jpg" title="sfasdf" style ="width: 100%" />
                </div>
                <div class="picsave" style="display:inline-block" >
                    <input type="button" value="保存" onclick="saveFile()" />
                </div>
            </div>

            <%--点击图片显示上传按钮--%>
            <script type="text/javascript">
                function showbtn() {
                    document.getElementById('dopic').style.display = 'block';
                };
            </script>

           <%--是否显示上传按钮（用ajax） 开始--%>
            <script type="text/javascript">
                $(function () {
                    $.post('/jz/pichandler.ashx?action=picexist', function (data) {
                        var data = JSON.parse(data);
                        if (!data.success) {
                            //隐藏上传标签 不占位置。
                            ///document.getElementById('dopic').style.display = "none";
                           document.getElementById('dopic').style.display = "inline";
                        }
                    });
                });
            </script>

            <%--  显示图片JS 开始--%>
            <script type="text/javascript">
                function readAsDataURL() {
                    var file = document.getElementById('File').files[0];
                    var reader = new FileReader();
                  //  alert('<%=Web.gc.WebForm1.GCname %>.png');
                    reader.readAsDataURL(file);//将文件读取为 DataURL
                    reader.onload = function (e) {//文件读取成功完成时触发
                        var result = document.getElementById('img');
                        result.src = this.result;//this.result 是reader的属性。
                        ////  saveFile();
                        ////function saveFile() {
                        ////    url1 = '/JZ/picHandler.ashx?action=UploadPic';
                        ////    //var File = $("#File111").val();
                        ////    //var File1 = $("input[name='File']").val();
                        ////    //var File2 = document.getElementById('File').value;
                        ////    //alert(File);
                        ////    //alert(File1);
                        ////    //alert(File2);
                        ////    //var name = $("#File111").attr("value");
                        ////    //alert(name);
                        ////    //var name1 = $("input[id='File111']").attr("value");
                        ////    //alert(name1);
                        ////    //var formdata = new FormData($('#form1')[0]);
                        ////    //alert(formdata.File);
                        ////    //alert(formdata.filename);
                        ////    //   $(" #test ").val();
                        ////    //  alert("我是上传按钮！！！");
                        ////    if (getBrowserType() == "Chrome") {
                        ////        // alert("我是上传按钮！(chrome!)");
                        ////        var formData = new FormData();
                        ////        formData.append('file', $('#File')[0].files[0]);
                        ////        //var Filename = document.getElementById('File').value;
                        ////        // alert(Filename);
                        ////        $.ajax({
                        ////            url: url1,//   要求为String类型的参数，（默认为当前页地址）发送请求的地址。
                        ////            async: false,// 要求为Boolean类型的参数，默认设置为true，所有请求均为异步请求。
                        ////            //如果需要发送同步请求，请将此选项设置为false。注意，同                            
                        ////            //步请求将锁住浏览器，用户其他操作必须等待请求完成才可以执行。
                        ////            type: "POST",// 要求为String类型的参数，请求方式（post或get）默认为get。
                        ////            cache: false, //上传文件不需要缓存
                        ////            processData: false, // 告诉jQuery不要去处理发送的数据
                        ////            //contentType: false, // 告诉jQuery不要去设置Content-Type请求头
                        ////            contentType: false,
                        ////            //dataType: "text",//纯文本字符串。

                        ////            //  data: { "File":"123"},
                        ////            //data: {Id:"3"},
                        ////            //data:JSON.stringify({File:"123321"}),
                        ////            // data: "{param1:'" + txtparam1 + "',param2:'" + txtparam2 + "'}",
                        ////            data: formData,
                        ////            //  data: { "File": File1 },
                        ////            //beforeSend: function () {
                        ////            //    return $('#fm').form('validate');
                        ////            //},
                        ////            //success: function (data1) {
                        ////            //  var data1 = JSON.parse(data1);
                        ////            // data1 = eval('(' + data1 + ')');      //将一个json字符串解析成js对象
                        ////            //  $.messager.alert("操作提示", data1.Message, 'info'); //显示后台信息
                        ////            // if (data1.Success) {
                        ////            // alert("图片上传成功!");
                        ////            //    // $('#dlg').dialog('close'); 	    //关闭弹出框
                        ////            //  //  $("#picHTMLwrapper").html("").append();
                        ////            //  //  $("#dg").datagrid('reload');
                        ////            //    }
                        ////            //}
                        ////            //,
                        ////            //error: function (XMLHttpRequest, textStatus, errorThrown) {
                        ////            //    alert(XMLHttpRequest.status);
                        ////            //    alert(XMLHttpRequest.readyState);
                        ////            //    alert(textStatus);
                        ////            //}
                        ////        });
                        ////    } else {
                        ////        $('#form1').form('submit', {
                        ////            url: url1, //注意添加和修改的url不同
                        ////            onSubmit: function () {
                        ////                //验证表单是否合法
                        ////                return $(this).form('validate');
                        ////            },
                        ////            success: function (res) {
                        ////                data = eval('(' + res + ')');      //将一个json字符串解析成js对象
                        ////                $.messager.alert("操作提示", data.Message, 'info'); //提示后台错误信息
                        ////                if (data.Success) {
                        ////                    $('#dlg').dialog('close'); 	    //关闭弹出框
                        ////                    $('#dd').datagrid('reload');    //重新加载数据
                        ////                }
                        ////            }
                        ////        });
                        ////    }
                        ////    return false;
                        ////}
                    }
                }
            </script>

            <%--图片上传 开始--%>
            <script type="text/javascript">
                function saveFile() {
                    url1 = '/JZ/picHandler.ashx?action=UploadPic';

                    //var File = $("#File111").val();
                    //var File1 = $("input[name='File']").val();
                    //var File2 = document.getElementById('File').value;
                    //alert(File);
                    //alert(File1);
                    //alert(File2);
                    //var name = $("#File111").attr("value");
                    //alert(name);
                    //var name1 = $("input[id='File111']").attr("value");
                    //alert(name1);
                    //var formdata = new FormData($('#form1')[0]);
                    //alert(formdata.File);
                    //alert(formdata.filename);
                    //   $(" #test ").val();
                    //  alert("我是上传按钮！！！");

                    //////if (getBrowserType() == "Chrome") {
                    //////    // alert("我是上传按钮！(chrome!)");
                    //////    var formData = new FormData();
                    //////    formData.append('file', $('#File')[0].files[0]);
                    //////    //var Filename = document.getElementById('File').value;
                    //////    // alert(Filename);
                    //////    $.ajax({
                    //////        url: url1,//   要求为String类型的参数，（默认为当前页地址）发送请求的地址。
                    //////        async: false,// 要求为Boolean类型的参数，默认设置为true，所有请求均为异步请求。
                    //////        //如果需要发送同步请求，请将此选项设置为false。注意，同                            
                    //////        //步请求将锁住浏览器，用户其他操作必须等待请求完成才可以执行。
                    //////        type: "POST",// 要求为String类型的参数，请求方式（post或get）默认为get。
                    //////        cache: false, //上传文件不需要缓存
                    //////        processData: false, // 告诉jQuery不要去处理发送的数据
                    //////        //contentType: false, // 告诉jQuery不要去设置Content-Type请求头
                    //////        contentType: false,
                    //////        //dataType: "text",//纯文本字符串。

                    //////        //  data: { "File":"123"},
                    //////        //data: {Id:"3"},
                    //////        //data:JSON.stringify({File:"123321"}),
                    //////        // data: "{param1:'" + txtparam1 + "',param2:'" + txtparam2 + "'}",
                    //////        data: formData,
                    //////        //  data: { "File": File1 },
                    //////        //beforeSend: function () {
                    //////        //    return $('#fm').form('validate');
                    //////        //},
                    //////        //success: function (data1) {
                    //////        //  var data1 = JSON.parse(data1);
                    //////        // data1 = eval('(' + data1 + ')');      //将一个json字符串解析成js对象
                    //////        //  $.messager.alert("操作提示", data1.Message, 'info'); //显示后台信息
                    //////        // if (data1.Success) {
                    //////        // alert("图片上传成功!");
                    //////        //    // $('#dlg').dialog('close'); 	    //关闭弹出框
                    //////        //  //  $("#picHTMLwrapper").html("").append();
                    //////        //  //  $("#dg").datagrid('reload');
                    //////        //    }
                    //////        //}
                    //////        //,
                    //////        //error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //////        //    alert(XMLHttpRequest.status);
                    //////        //    alert(XMLHttpRequest.readyState);
                    //////        //    alert(textStatus);
                    //////        //}
                    //////    });
                    //////} else {
                    //////    $('#form1').form('submit', {
                    //////        url: url1, //注意添加和修改的url不同
                    //////        onSubmit: function () {
                    //////            //验证表单是否合法
                    //////            return $(this).form('validate');
                    //////        },
                    //////        success: function (res) {
                    //////            data = eval('(' + res + ')');      //将一个json字符串解析成js对象
                    //////            $.messager.alert("操作提示", data.Message, 'info'); //提示后台错误信息
                    //////            if (data.Success) {
                    //////                $('#dlg').dialog('close'); 	    //关闭弹出框
                    //////                $('#dd').datagrid('reload');    //重新加载数据
                    //////            }
                    //////        }

                    //////    });
                    //////}

                    var formData = new FormData();
                    formData.append('file', $('#File')[0].files[0]);
                    $.ajax({
                        url: url1,//   要求为String类型的参数，（默认为当前页地址）发送请求的地址。
                        async: false,// 要求为Boolean类型的参数，默认设置为true，所有请求均为异步请求。
                        //如果需要发送同步请求，请将此选项设置为false。注意，同                            
                        //步请求将锁住浏览器，用户其他操作必须等待请求完成才可以执行。
                        type: "POST",// 要求为String类型的参数，请求方式（post或get）默认为get。
                        cache: false, //上传文件不需要缓存
                        processData: false, // 告诉jQuery不要去处理发送的数据
                        //contentType: false, // 告诉jQuery不要去设置Content-Type请求头
                        contentType: false,
                        //dataType: "text",//纯文本字符串。

                        //  data: { "File":"123"},
                        //data: {Id:"3"},
                        //data:JSON.stringify({File:"123321"}),
                        // data: "{param1:'" + txtparam1 + "',param2:'" + txtparam2 + "'}",
                        data: formData,
                        //  data: { "File": File1 },
                        //beforeSend: function () {
                        //    return $('#fm').form('validate');
                        //},
                        success: function (data1) {
                        var data1 = JSON.parse(data1);
                        // data1 = eval('(' + data1 + ')');      //将一个json字符串解析成js对象
                        //$.messager.
                            alert("操作提示：" + data1.Message); //显示后台信息
                        //if (data1.Success) {
                        //    alert("图片保存成功!");
                        //    //    // $('#dlg').dialog('close'); 	    //关闭弹出框
                        //    //  //  $("#picHTMLwrapper").html("").append();
                        //    //  //  $("#dg").datagrid('reload');
                        //    //    }
                        //};
                        //,
                        //error: function (XMLHttpRequest, textStatus, errorThrown) {
                        //    alert(XMLHttpRequest.status);
                        //    alert(XMLHttpRequest.readyState);
                        //    alert(textStatus);
                        }
                    });
                    return false;
                }
            </script>

        </div>
    </div>
    <%--    </form>--%>
</body>
</html>

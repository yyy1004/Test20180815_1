<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index-backup.aspx.cs" Inherits="Web.Index1" %>

<!DOCTYPE html>
<html>
<head>
    <title>巨鲸齿轮箱在线监测系统</title>
 <link rel="shortcut icon" href="/Images/slo.ico" type="image/x-icon" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="/Scripts/loading.js" type="text/javascript"></script>
    <link href="/EasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="/EasyUI/themes/gray/easyui.css" rel="stylesheet" type="text/css" />
    <link href="CSS/indexAspx.css" rel="stylesheet" type="text/css"/>
    <script src="/EasyUI/jquery.min.js" type="text/javascript"></script>
    <script src="/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/Scripts/distinguishBrowser.js" type="text/javascript"></script>
    <script src="EasyUI/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        //树形菜单的点击事件
        //$(function () {
        //    $("#treeMenu").tree({
        //        onClick: function (node) {
        //            //  alert(node);
        //            if (node.attributes !== undefined && node.attributes.url !== undefined) {
        //                if (node.attributes.url != '') {
        //                    //跳转到url指定的页面
        //                    //alert(node);
        //                    url = node.attributes.url + "?node=" + node.id;
        //                    //使iframe刷新
        //                    document.getElementById('myiframe').src = url;
        //                }
        //            }
        //        }
        //    });
        //    //周期性调用日期更新函数
        //    setInterval("GetTime()", 1000);
        //});
        $(document).ready(function () {
            $("#treeMenu").tree({
                onClick: function (node) {
                    //  alert(node);
                    if (node.attributes !== undefined && node.attributes.url !== undefined) {
                        if (node.attributes.url != '') {
                            //跳转到url指定的页面
                            //alert(node);
                            url = node.attributes.url + "?node=" + node.id;
                            //使iframe刷新
                            document.getElementById('myiframe').src = url;
                        }
                    }
                }
            });
        });

        $(function () {

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
    <style>
        html,body
        {
            width:100%;
            height:100%;
            margin:0;
            padding:0;
        }
        imgContent {
            width: 100%;
            height: 125px;
            margin: 0px auto;
            overflow: hidden;
        }

        /*所有图片盒子*/
        .con {
            width: 100%;
            text-align: center;
        }

        .con1 {
            z-index: 3;
            margin: 0px auto;
        }

        .con2 {
            z-index: 2;
            margin: 0px auto;
        }

        .con3 {
            z-index: 1;
            margin: 0px auto;
        }


        /*所有图片*/
        #img3 {
            margin: 0 auto;
            width: 100%;
            z-index: 3;
            opacity: 0.6;
        }

        #img2 {
            margin: 0 auto;
            width: 100%;
            z-index: 2;
            opacity: 0.7;
        }

        .img {
            transition-duration: 1s;
            -webkit-transition-duration: 1s;
            -moz-transition-duration: 1s;
        }

        #img1 {
            margin: 0 auto;
            width: 100%;
            z-index: 1;
            opacity: 0.95;
        }

        img:hover {
            cursor: pointer;
        }
        /*#ddot {
            position: absolute;
            top: 168px;
            width:100%;
        }
         .dot {
             z-index:9;
            cursor: pointer;
            height: 13px;
            width: 13px;
            background-color: white;
            border-radius: 50%;
            display: inline-block;
            transition: background-color .6s ease;
            padding:1px;
        }*/
        .active,
        .dot:hover {
            background-color: #717171;
        }
    </style>

    <script type="text/javascript">
        var target = $('.tree-icon.tree-file');
      //  var length = target.length;
          for (var i = 0; i < target.length; i++) {
       // $('.tree-icon.tree-file').parent()[i].parentElement.childElementCount  //这个是正确的。或者$('.tree-icon.tree-file')[i].parentElement.parentElement.childElementCount
            if ($('.tree-icon.tree-file')[i].parent()[0].parentElement.childElementCount = 1) { //这个有问题，即使是正确的也不能起作用。
                target[i].className = "tree-icon tree-folder"
            };
        }
    </script>
</head>
<%--    <body style="overflow:hidden;">--%>
<body>

      <%--  <div class="easyui-layout" style="width: 99.9%; height: 100%;">--%>
    <div class="easyui-layout" style="width: 99.9%;height:100%;">
        <%--  头部区域--%>
        <div region="north" split="true" style="height: 125px; width:100%; position: relative;display:block;overflow:visible;">
            <div class="logo" style="margin-top:0 auto; height: 120px; width: 100%; background-image: url(/images/title3.png); background-repeat: no-repeat; background-size: cover; position: absolute; top: 0; left: 0; z-index: 12;"></div>
            <%--<div id="imgContent">
                <!--图片1-->
                <div class="con con1">
                    <img src="images/1.jpg" alt="img3" id="img3" class="img" style="width: 100%; height: 125px;">
                </div>

                <!--图片2-->
                <div class="con con2">
                    <img src="images/2.jpg" alt="img2" id="img2" class="img" style="width: 100%; height: 125px;">
                </div>
                <!--tup3-->
                <div class="con con3">
                    <img src="images/3.jpg" alt="img1" id="img1" class="img" style="width: 100%; height: 125px;">
                </div>
            </div>--%>
            <%-- <div id="ddot" style="text-align:center">
                <span class="dot" id="dot1"></span>
                <span class="dot" id="dot2"></span>
                <span class="dot" id="dot3"></span>
            </div>--%>
            <div class="head-log" <%--style="color: #0070b8; font-family: Georgia,Serif; font-weight: bolder; position: absolute; right: 10px; bottom: 10px; z-index: 10;"--%>>
                <div class="hd-tip">
                <span id="Timer1"></span><span class="role"><%=Session["角色"]%>：<%=Session["用户名"]%></span> 
<%--                <a class="logoff"  runat="server" OnClick="logoff1" >
                    <span>注销</span>
                </a>--%>

                </div>
                <div class="hd-logf">
                <form runat="server" id="form112">
                <asp:LinkButton ID="Logoff" runat="server" OnClick="btnLogoff">退出</asp:LinkButton>
                </form>

                </div>
            </div>
        </div>
        <%--左边菜单区域--%>
        <div region="west" split="true" id="west-tree" title="菜单" style="width: 270px;/*height:1800px;*/ padding: 10px 20px;">
            <ul  id="treeMenu" data-options="url:'/Menu/GetMenu.ashx',method:'get',animate:true,lines:true"><%--//删除 class="easyui-tree"避免加载两次--%>
            </ul>
<%--             <ul class="easyui-tree" id="treeMenu" data-options="url:'/Menu/GetMenu.ashx',method:'get',animate:true,lines:true">
            </ul>--%>
        </div>
        <%--右边内容区域--%>
        <div id="content" region="center" title="巨鲸在线监测" style="padding: 5px;/*height:1800px;*/ width: 100%">
            <iframe id="myiframe" name="myiframe" iframe src="Index.html" width="100%" height="99%" style="overflow:hidden;" frameborder="0"></iframe>
        </div>

    </div>
    <%-- 轮播图JS--%>
    <%--    <script type="text/javascript">
        var imgs = document.getElementsByClassName("img");
        var dots = document.getElementsByClassName("dot");
        var temp = 0;
        var p = 0;
        var src;
        window.onload = function slideshow() {
            if (temp == 3) temp = 0;
            src = imgs[2].src;
            imgs[2].src = imgs[1].src;
            imgs[1].src = imgs[0].src;
            imgs[0].src = src;
            //for (var j = 0; j < 3; j++) {
            //    if (j == temp) {
            //        dots[j].style.backgroundColor = "#717171";
            //    } else {
            //        dots[j].style.backgroundColor="white";
            //    } 
            //}
            //temp++;
            setTimeout(slideshow, 4000);
        }

        //function clearshow(){
        //    imgshows.style.display="none";
        //}
    </script>--%>

</body>
</html>

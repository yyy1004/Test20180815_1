
//加载菜单
$(document).ready(function () {
    $("#treeMenu").tree({
        onClick: function  GetLoop(node) {
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

    //周期性调用日期更新函数
$(function () {
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

//$(document).ready(function () {
//    $('.layout-panel-center div.panel-header').append('<div class="exit-wrap"><div class="exit"><a href="#" onclick="myexit()">退出</a></div><div class="hd-tip"><span id="Timer1"></span><span class="role"><%=Session["角色"]%>：<%=Session["用户名"]%></span></div></div>');
//});
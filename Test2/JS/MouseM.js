/// <reference path="/EasyUI/jquery.min.js" />

//----------------鼠标拖动-------------------
var selediv;//原div
var odmain = document.getElementById('main');//连接div
//首次加载画直线
    //$(function () {
    //    var ht = drawLine(parseInt(odmain.offsetLeft), parseInt(odmain.offsetTop), parseInt(selediv.offsetLeft), parseInt(selediv.offsetTop), "green");
    //    document.getElementById("main").innerHTML = ht;
    //});

    var mouseX, mouseY;//鼠标坐标
  //  var seledivX, seledivY;
var ismouseDown = false;//鼠标是否被按下
//var seledivflag;
var isIE = document.all ? true : false;//判断是否是IE

function mouseDown(obj, e) {
   // seledivflag = obj;
    selediv = obj;
    obj.style.cursor = 'move';
    ismouseDown = true;
    var e = e ? e : event;
    if (e.button == (document.all ? 1 : 0)) {
        mouseX = e.clientX;//鼠标按下时鼠标坐标
        mouseY = e.clientY;
        selediv.style.left= selediv.offsetLeft+ "px";  //离document左边的距离 鼠标按下时给其加样式
        selediv.style.top= selediv.offsetTop + "px";
        if (isIE) {
            selediv.setCapture();
            selediv.filters.alpha.opacity = 50;
        }
        else {
            window.captureEvents(Event.mousemove);
            selediv.style.opacity = 0.5;
        }
    }
}
//鼠标移动
function mouseMove(e) {
    var e = e ? e : event;

    if (ismouseDown == true)//鼠标被按下，且按下的时目标div
    {
       // var div = document.getElementById('div2');
        var moveX = e.clientX - mouseX;//移动的水平距离
        var moveY = e.clientY - mouseY;
        //var result=parseInt(selediv.style.left) +mrx;
        selediv.style.left = parseInt(selediv.style.left) + moveX + "px";//水平位置
        selediv.style.top = parseInt(selediv.style.top) + moveY + "px"; //垂直位置

        mouseX = e.clientX;//设定新的坐标位置，不可少
        mouseY = e.clientY;
        document.getElementById('span1').innerHTML = "div2-x:" + selediv.style.left + " div2-y:" + selediv.style.top;
        //document.getElementById("span1").innerHTML = "x:" + div.style.top + "y:" + div.style.left;
        document.getElementById('span2').innerHTML = "moveX:" + moveX + " moveY:" + moveY;
       // document.getElementById('span3').innerHTML = "seledivX:" + seledivX + " seledivY:" + seledivY;

        var x = selediv.style.left;
        var y = selediv.style.top;

        var lineHtml = drawLine(parseInt(odmain.offsetLeft), parseInt(odmain.offsetTop), parseInt(x), parseInt(y), "green");
        document.getElementById("main").innerHTML = lineHtml;
    }
}

//鼠标按下
//$(selediv).mousedown(function (e) {
//    //alert('selediv 鼠标被按下')
//    seledivflag = this;
//    mouseD = true;
//    var e = e ? e : event;
//    if (e.button == (document.all ? 1 : 0)) {
//        mouseX = e.clientX;//鼠标按下时鼠标坐标
//        mouseY = e.clientY;
//        selediv.style.left = selediv.offsetLeft + "px";  //离document左边的距离
//        selediv.style.top = selediv.offsetTop + "px";
//        if (isIE) {
//            selediv.setCapture();
//            selediv.filters.alpha.opacity = 50;
//        }
//        else {
//            window.captureEvents(Event.MOUSEMOVE);
//            selediv.style.opacity = 0.5;
//        }
//    }
//})
//document.onmousedown = function (e) {
//  //alert('selediv 鼠标被按下')
//    seledivflag = this;
//    mouseD = true;
//    var e = e ? e : event;
//    if (e.button == (document.all ? 1 : 0)) {
//        mx = e.clientX;//水平坐标
//        my = e.clientY;
//        selediv.style.left = selediv.offsetLeft + "px";  //离document左边的距离
//        selediv.style.top = selediv.offsetTop + "px";
//        if (isIE) {
//            selediv.setCapture();
//            selediv.filters.alpha.opacity = 50;
//        }
//        else {
//            window.captureEvents(Event.MOUSEMOVE);
//            selediv.style.opacity = 0.5;
//        }
//    }
//}
//鼠标移动
//document.onmousedown = function (e) {
//    var e = e ? e : event;// 获取有效的事件对象
//    if (e.button == (document.all ? 1 : 0)) {
//        mouseD = true;//鼠标被按下
//        //alert('鼠标被按下');
//    }
//   // alert('鼠标被按下 外面');
//}
//鼠标弹起

function mouseUp(e) {
    ismouseDown = false;
    //seledivflag = "";//失去div
    if (isIE) {
        selediv.releaseCapture();
        selediv.filters.alpha.opacity = 100;
    }
    else {
        window.releaseEvents(selediv.mousemove);
        selediv.style.opacity = 1;
    }
}
//document.onmouseup = function () {
//    mouseD = false;
//    seledivflag = "";//失去div
//    if (isIE) {
//        selediv.releaseCapture();
//        selediv.filters.alpha.opacity = 100;
//    }
//    else {
//        window.releaseEvents(selediv.mousemove);
//        selediv.style.opacity = 1;
//    }
//}
// 鼠标按下div



function drawLine(x0, y0, x1, y1, color) {
    var rs = "";
    if (y0 == y1)  //画横线
    {
        if (x0 > x1) { var t = x0; x0 = x1; x1 = t }
        rs = "<span style='top:" + y0 + "px;left:" + x0 + "px;position:absolute;font-size:1px;background-color:" + color + ";height:1px; width:" + Math.abs(x1 - x0) + "px'></span>";
    }
    else if (x0 == x1)  //画竖线
    {
        if (y0 > y1) { var t = y0; y0 = y1; y1 = t }
        rs = "<span style='top:" + y0 + "px;left:" + x0 + "px;position:absolute;font-size:1px;background-color:" + color + ";width:1px;height:" + Math.abs(y1 - y0) + "px'></span>";
    }
    else {
        var lx = x1 - x0
        var ly = y1 - y0
        var l = Math.sqrt(lx * lx + ly * ly)
        rs = new Array();
        for (var i = 0; i < l; i += 1) {
            var p = i / l;
            var px = parseInt(x0 + lx * p);
            var py = parseInt(y0 + ly * p);
            rs[rs.length] = "<span style='top:" + py + "px;left:" + px + "px;height:1px;width:1px;position:absolute;font-size:1px;background-color:" + color + "'></span>";
        }
        rs = rs.join("");
    }
    return rs
}
/// <reference path="/EasyUI/jquery.min.js" />

// #region -------------------自定义标签----------------------

$(function () {
    $("#tabs").tabs({
        plain: false,
        boder: false,
        width: $(window).width(),
        height: $(window).height(),
        onSelect: function (title, index) {
            tabtitle = title;//把当前tab的title赋值给tabtitle。
        }
    });
});

//监测浏览器
$(window).resize(function () {
    $('#tabs').tabs({
        plain: false,
        boder: false,
        width: $(window).width(),
        height: $(window).height(),
    }).tabs('resize');
    //alert("窗口变化")
})
//状态格式化  正常 报警  故障
function stateformater(val, row, index) {
    var status = row.status;
    var res = '';
    if (status == 1) {
        return "<font style='color:white;'>正常</font>";
    } else if (status == 2) {
        return "<font style='color:black;text-shadow: 0px 0px 0px #000;'>报警</font>";
    } else if (status == 3) {
        return "<font style='color:white;'>故障</font>"
    }
}
//机组状态列 样式 -背景
function statestyler(val, row, index) {
    var status = row.status;
    if (status == 1) {
        return 'background-color:blue';
    } else if (status == 2) {
        return 'background-color:yellow';
    } else {
        return 'background-color:red'
    }
}

// #endregion



//var oldtime;//保存更新时间。//不能放在定时器里
// var isFirstRun = true;//不能放在定时器里
////获取数据更新时间。
//function GetUpdateTime1() {
//    $.post("/JZ/JZHandler.ashx", { "action": "Qstatus" }, function (data) {
//        var data = JSON.parse(data);
//        return data.newtime;
//    })
//}



//var temptime = GetUpdateTime1();//获取当前更新时间。不需要保存，//放在定时器里
//if (oldtime!= temptime ) {  //不相等表示：时间更新了
//    oldtime = temptime;
//    myajax(isFirstRun);
//    isFirstRun = false;//后面不在动态显示柱状图
//}












// #region ---------------------机组监测  柱状图监测new-----------------------

function myajax(isFirstRun) {

    // #region     //查询data-递归

    //var currentIndex = 1;
    //GetQs();
    //function GetQs() {
    //    if (currentIndex > 8) {
    //        return;
    //    }

    //    $.ajax({
    //        url: "/JZ/JZHandler.ashx",
    //        type: "POST",
    //        data: { "action": ("Q" + currentIndex) },
    //        //  contentType:"application/json;charset=utf-8",//如果提交的是json数据类型，则必须有此参数,表示提交的数据类型
    //        //dataType: "json", //表示返回值类型，不必须  
    //        success: function (data) {
    //            var data = JSON.parse(data);
    //            //柱状图监测
    //            if (data.fd != "" && data.fd != null) {
    //                CreateChart(("canvas" + currentIndex), data);
    //            }
    //            //机组监测
    //            if (data.fd != "" && data.fd != null) {
    //                $("#ss" + currentIndex + "-name").html(data.fd);
    //                $("#ss" + currentIndex + "-value").html(data.fv + ' g');
    //                //正常
    //                if (data.fv < data.yjz * 1) {
    //                    $("#ss" + currentIndex + "-value").css("color", "green");
    //                    if (maxstatus < 1) {
    //                        maxstatus = 1;
    //                    }
    //                }
    //                    //报警
    //                else if (data.fv >= data.yjz * 1 && data.fv < data.bjz * 1) {
    //                    $("#ss" + currentIndex + "-value").css("color", "yellow");
    //                    if (maxstatus < 2) {
    //                        maxstatus = 2
    //                    }
    //                    //故障
    //                } else {
    //                    $("#ss" + currentIndex + "-value").css("color", "red");
    //                    if (maxstatus < 3) {
    //                        maxstatus = 3
    //                    }
    //                }
    //            }
    //            currentIndex++;
    //            GetQs();
    //        }
    //    })
    //}

    // #endregion

    // #region     //更新时间

    $.post("/JZ/JZHandler.ashx", { "action": "Qstatus" }, function (data) {
        var data = JSON.parse(data);
        if (data.newtime != "" && data.newtime != null) {
            // $("#recordingtime-name").html('记录时间');
            updatetime = new Date(data.newtime).getTime();//获取数据更新时间的毫秒数。
            $("#recordingdingtime-value").html(data.newtime);
        }
    })

    // #endregion

    // #region  查询data- 异步

    $.post("/JZ/JZHandler.ashx", { "action": "Q1" }, function (data) {
        var data = JSON.parse(data);
        var fv = data.fv * 1;
        fv = fv.toFixed(2); // 输出结果为 2.45
        //柱状图监测
        if (data.fd != "" && data.fd != null) {
            CreateChart('canvas1', data);
        }
        //机组监测
        if (data.fd != "" && data.fd != null) {
            $("#ss1-name").html(data.fd);
            $("#ss1-value").html(fv + ' g');
            //正常
            if (fv < data.yjz * 1) {
                $('#ss1-value').css("color", "green");
                if (maxstatus < 1) {
                    maxstatus = 1;
                }
            }
            else if (fv >= data.yjz * 1 && fv < data.bjz * 1) {
                //  $('#value-father1').css("background-color", "yellow");
                $('#ss1-value').css("color", "yellow");
                if (maxstatus < 2) {
                    maxstatus = 2
                }
            } else {
                //    $('#value-father1').each(function () {
                //      $(this).css("background-color", "red")
                //     });
                $('#ss1-value').css("color", "red");
                if (maxstatus < 3) {
                    maxstatus = 3
                }
            }
        }
    });
    $.post("/JZ/JZHandler.ashx", { "action": "Q2" }, function (data) {
        //alert(data);
        var data = JSON.parse(data);
        var fv = data.fv * 1;
        fv = fv.toFixed(2); // 输出结果为 2.45
        //柱状图监测
        if (data.fd != "" && data.fd != null) {
            CreateChart('canvas2', data);
        }
        if (data.fd != "" && data.fd != null) {
            $("#ss2-name").html(data.fd);
            $("#ss2-value").html(fv + ' g');
            if (fv < data.yjz * 1) {
                $('#ss2-value').css("color", "green");
                if (maxstatus < 1) {
                    maxstatus = 1;
                }
            }
            else if (fv >= data.yjz * 1 && fv < data.bjz * 1) {
                $('#ss2-value').css("color", "yellow");
                if (maxstatus < 2) {
                    maxstatus = 2
                }
            } else {
                $('#ss2-value').css("color", "red");
                if (maxstatus < 3) {
                    maxstatus = 3
                }
            }
        }
    });
    $.post("/JZ/JZHandler.ashx", { "action": "Q3" }, function (data) {
        // alert(data);
        var data = JSON.parse(data);
        var fv = data.fv * 1;
        fv = fv.toFixed(2); // 输出结果为 2.45
        if (data.fd != "" && data.fd != null) {
            CreateChart('canvas3', data);
            $("#ss3-name").html(data.fd);
            $("#ss3-value").html(fv + ' g');
            if (fv < data.yjz * 1) {
                $('#ss3-value').css("color", "green");
                if (maxstatus < 1) {
                    maxstatus = 1;
                }
            }
            else if (fv >= data.yjz * 1 && fv < data.bjz * 1) {
                $('#ss3-value').css("color", "yellow");
                if (maxstatus < 2) {
                    maxstatus = 2
                }
            } else {
                $('#ss3-value').css("color", "red");
                if (maxstatus < 3) {
                    maxstatus = 3
                }
            }
        }
    });
    $.post("/JZ/JZHandler.ashx", { "action": "Q4" }, function (data) {
        // alert(data);
        var data = JSON.parse(data);
        var fv = data.fv * 1;
        fv = fv.toFixed(2); // 输出结果为 2.45
        if (data.fd != "" && data.fd != null) {
            CreateChart('canvas4', data);
            $("#ss4-name").html(data.fd);
            $("#ss4-value").html(fv + ' g');
            if (fv < data.yjz * 1) {
                $('#ss4-value').css("color", "green");
                if (maxstatus < 1) {
                    maxstatus = 1;
                }
            }
            else if (fv >= data.yjz * 1 && fv < data.bjz * 1) {
                $('#ss4-value').css("color", "yellow");
                if (maxstatus < 2) {
                    maxstatus = 2
                }
            } else {
                $('#ss4-value').css("color", "red");
                if (maxstatus < 3) {
                    maxstatus = 3
                }
            }
        }
    });
    $.post("/JZ/JZHandler.ashx", { "action": "Q5" }, function (data) {
        // alert(data);
        var data = JSON.parse(data);
        var fv = data.fv * 1;
        fv = fv.toFixed(2); // 输出结果为 2.45
        if (data.fd != "" && data.fd != null) {
            CreateChart('canvas5', data);
            $("#ss5-name").html(data.fd);
            $("#ss5-value").html(fv + ' g');
            if (fv < data.yjz * 1) {
                $('#ss5-value').css("color", "green");
                if (maxstatus < 1) {
                    maxstatus = 1;
                }
            }
            else if (fv >= data.yjz * 1 && fv < data.bjz * 1) {
                $('#ss5-value').css("color", "yellow");
                if (maxstatus < 2) {
                    maxstatus = 2
                }
            } else {
                $('#ss5-value').css("color", "red");
                if (maxstatus < 3) {
                    maxstatus = 3
                }
            }
        }
    });
    $.post("/JZ/JZHandler.ashx", { "action": "Q6" }, function (data) {
        var data = JSON.parse(data);
        var fv = data.fv * 1;
        fv = fv.toFixed(2); // 输出结果为 2.45
        if (data.fd != "" && data.fd != null) {
            CreateChart('canvas6', data);
            $("#ss6-name").html(data.fd);
            $("#ss6-value").html(fv + ' g');
            if (fv < data.yjz * 1) {
                $('#ss6-value').css("color", "green");
                if (maxstatus < 1) {
                    maxstatus = 1;
                }
            }
            else if (fv >= data.yjz * 1 && fv < data.bjz * 1) {
                $('#ss6-value').css("color", "yellow");
                if (maxstatus < 2) {
                    maxstatus = 2
                }
            } else {
                $('#ss6-value').css("color", "red");
                if (maxstatus < 3) {
                    maxstatus = 3
                }
            }
        }
    });
    $.post("/JZ/JZHandler.ashx", { "action": "Q7" }, function (data) {
        // alert(data);
        var data = JSON.parse(data);
        var fv = data.fv * 1;
        fv = fv.toFixed(2); // 输出结果为 2.45
        if (data.fd != "" && data.fd != null) {
            CreateChart('canvas7', data);
            $("#ss7-name").html(data.fd);
            $("#ss7-value").html(fv + ' g');
            if (fv < data.yjz * 1) {
                $('#ss7-value').css("color", "green");
                if (maxstatus < 1) {
                    maxstatus = 1;
                }
            }
            else if (fv >= data.yjz * 1 && fv < data.bjz * 1) {
                $('#ss7-value').css("color", "yellow");
                if (maxstatus < 2) {
                    maxstatus = 2
                }
            } else {
                $('#ss7-value').css("color", "red");
                if (maxstatus < 3) {
                    maxstatus = 3
                }
            }
        }
    });
    $.post("/JZ/JZHandler.ashx", { "action": "Q8" }, function (data) {
        // alert(data);
        var data = JSON.parse(data);
        var fv = data.fv * 1;
        fv = fv.toFixed(2); // 输出结果为 2.45
        if (data.fd != "" && data.fd != null) {
            CreateChart('canvas8', data);
            $("#ss8-name").html(data.fd);
            $("#ss8-value").html(fv + ' g');
            if (fv < data.yjz * 1) {
                $('#ss8-value').css("color", "green");
                if (maxstatus < 1) {
                    maxstatus = 1;
                }
            }
            else if (fv >= data.yjz * 1 && fv < data.bjz * 1) {
                $('#ss8-value').css("color", "yellow");
                if (maxstatus < 2) {
                    maxstatus = 2
                }
            } else {
                $('#ss8-value').css("color", "red");
                if (maxstatus < 3) {
                    maxstatus = 3
                }
            }
        }
    });

    // #endregion

    // #region  启停

    if (jzstatus != maxstatus) {
        jzstatus = maxstatus;
    }
    maxstatus = 0;

    if (jzstatus != "" && jzstatus != null) {
        //如果服务器时间与当前是按对比大于5分钟，就判定断网。
      //  if (updatetime != undefined && (new Date().getTime() - updatetime) <5 * 60 * 1000) {
            if (jzstatus == 1) {
                $("#ssisrun-value").html('正常');
                $('#ssisrun-value').css("color", "green");
                $('#round-marking').css('background-color', 'green');
                //     $('#value-father1').each(function() {
                //  $(this).css("background-color", "green")
                //      })
            }
            else if (jzstatus == 2) {
                //  $('#value-father1').css("background-color", "yellow");
                $("#ssisrun-value").html('报警');
                $('#ssisrun-value').css("color", "yellow");
                $('#round-marking').css("background-color", "yellow");
            } else if (jzstatus == 3) {
                //    $('#value-father1').each(function () {
                //      $(this).css("background-color", "red")
                //     });
                $("#ssisrun-value").html('故障');
                $('#ssisrun-value').css("color", "red")
                $('#round-marking').css("background-color", "red");
            }
        //} else {// 断网-五分钟内没有更新
        //    $("#ssisrun-value").html('断网');
        //    $('#ssisrun-value').css("color", "#fff");
        //    $('#round-marking').css("background-color", "gray");
        //}
    }

    // #endregion

    // #region     //绘制柱状图

    function CreateChart(chartId, data) {
        var data1 = {
            "fd": data.fd,//名称
            "fv": (data.fv * 1).toFixed(2),//值
            "yjz": data.yjz,//预警值
            "bjz": data.bjz,//报警值
            "limitHigh": data.bjz * 1 + 10,//最高值
            "limitLow": "0",
        }
        var chart = new sBarChart(chartId, data1, {
            title: '',
            bgColor: '#fff',     //背景颜色
            titleColor: 'red',      // 标题颜色
            titlePosition: 'top',       // 标题位置
            axisColor: '#000',       // 坐标轴颜色
            contentColor: '#cecece',     // 内容横线颜色
            padding: 50,
            paddingTop: 35,
            isloop: isFirstRun,//是否动画显示
        })
    }

    // #endregion
}
var tabtitle;//标签名
var jzstatus = 0;//实时机组状态
var maxstatus = 0;
var updatetime;//获取服务器时间。
var timer;//定时器
//var isFirstRun = true;
myajax(true);//先执行一次，动画显示柱状图，然后再设置3秒执行一次
clearInterval(timer);//清除定时器
//timer = setInterval(myajax, 1000);
timer = setInterval(function () {
    if (tabtitle == "机组监测" || tabtitle == "柱状图监测") {
        //if (isFirstRun) {
        //    myajax(isFirstRun);
        //    isFirstRun = false;
        //}
        myajax(false);//不显示动画柱状图
    }

}, 2000);

// #endregion


//#region---------------------柱状图刷新old--------------------
//var timer2;
//var innerhtml = '<object type="text/html" data="/JZ/jzzzt.aspx" width="100%" height="600px;"></object>';
//clearInterval(timer2);
//GetJZZZTHtml();//先执行一次
////    setInterval("GetJZZZTHtml()", 3000);
//timer2 = setInterval(function () {
//    if (tabtitle == "柱状图监测") {
//        GetJZZZTHtml();
//    }
//}, 5000);

//function GetJZZZTHtml() {
//    document.getElementById("jzzzt").innerHTML = innerhtml;
//}
//// #endregion




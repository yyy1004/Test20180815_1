/// <reference path="/EasyUI/jquery.min.js" />

var tabtitle;//标签名
var tabindex;//标签索引。
var cdselected = "全部测点";//监测列表里所选择的测点

var cdselectedforfenxi = "选择测点";//信号分析里 所选择的测点 当选择测点的时候赋值。
var cdselectedvalue;//测点值。
var jzStatusType;//状态类型 1 正常 2 报警 3 停机
var oldtime;//保存更新时间。//放在定时器外
var isFirstRun = true;// 是否为第一次执行，放在定时器外
var timer;//定时器
var timerForNewtime;//定时器。查看服务器数据是否刷新。
var timerForFenxi;//数据分析刷新
var JTname;//集团名称
var GCname;//工厂名称
var jzname;//机组名称

// #region -------------------获取当前tabtitle----------------------

$(function () {
    $("#tabs").tabs({
        plain: false,
        boder: false,
        width: $(window).width(),
        height: $(window).height(),
        onSelect: function (title, index) {
            tabtitle = title;//把当前tab的title赋值给tabtitle。
            if (tabtitle == '机组监测') {
                GetTimeAndType();//先获取更新时间，状态。并显示更新时间和状态以及数据。//myajax(true);//先执行一次，动画显示柱状图，然后再设置3秒执行一次
                clearInterval(timer);//清除定时器
                clearInterval(timerForNewtime);
                clearInterval(timerForFenxi);
                //timer = setInterval(myajax, 1000);
                timer = setInterval(function () {

                    GetTimeAndType();

                }, 2000);
            }
            else if (tabtitle == "柱状图监测1") {
                isFirstRun = true;
                oldtime = undefined;
                GetTimeAndType();//先获取更新时间，状态。并显示更新时间和状态以及数据。//myajax(true);//先执行一次，动画显示柱状图，然后再设置3秒执行一次
                clearInterval(timer);//清除定时器
                clearInterval(timerForNewtime);
                clearInterval(timerForFenxi);
                //timer = setInterval(myajax, 1000);
                timer = setInterval(function () {
                    //   if (tabtitle == "机组监测" || tabtitle == "柱状图监测") {
                    GetTimeAndType();
                    //    }
                }, 2000);

            }
            else if (tabtitle == '监测记录') {
                GetMonitorTable();//初始化列表。
                isFirstRun = true;
                clearInterval(timer);//清除定时器
                clearInterval(timerForNewtime);
                clearInterval(timerForFenxi);
                GetCDComboboxData(cdselected, '#cd', true);//获取测点下拉框列表。
            }
            else if (tabtitle == '信号分析') {
                isFirstRun = true;
                jzname = $('.tree-node-selected .tree-title', parent.document.body)[0].innerHTML;//取父级frame的html
                GCname = $('.tree-node-selected', parent.document.body).parent().parent().prev().children('.tree-title')[0].innerHTML;
                //  console.log(jzname);
                clearInterval(timer);//清除定时器
                clearInterval(timerForNewtime);
                clearInterval(timerForFenxi);
                GetCDComboboxDataForfenxi(cdselectedforfenxi, '#cd-fenxi', true);//获取测点下拉框列表内容。
                //获取图标类型
                $("#cd-tableType").combobox({
                    valueField: 'label',
                    textField: 'value',
                    panelHeight: 80,
                    data: [{
                        label: '1',
                        value: '波形频率分析'
                    }, {
                        label: '2',
                        value: '历史趋势分析'
                    }],
                });
                //获取数据量
                $("#cd-dataCount").combobox({
                    valueField: 'label',
                    textField: 'value',
                    panelHeight:103,
                    data: [{
                        label: '1',
                        value: '2000'
                    }, {
                        label: '5',
                        value: '5000'
                    }, {
                        label: '10',
                        value: '10000'
                    }, 
                    ],
                });
                //如果cdselected为""时就不执行ajax-DataAnly()
                DataAnaly();//定义在largedata.js里
                DataAnalyForFrequency();//定义在largedata.js里

       
                var targetoldtime = oldtime;
                var targetcdselected = cdselectedforfenxi;
                timerForNewtime = setInterval(function myfunction() {
                    GetTimeForFenxi();
                }, 3000);
                timerForFenxi = setInterval(function myfunction() {
                    if (targetoldtime != oldtime || targetcdselected != cdselectedforfenxi) {
                        targetoldtime = oldtime;
                        targetcdselected = cdselectedforfenxi;
                        GetNewCdData(cdselectedforfenxi);//update测点值
                        DataAnaly();
                        DataAnalyForFrequency();
                    }
                }, 400);
            }
            // tabindex = index;
            //  var activeTab = $('#tabs').tabs('getTab', index);
            //var SelectedTab = $('#tabs').tabs('getSelected');
            //var tab = SelectedTab.panel('options').tab
            //var tabtitleText = tab[0].innerText;
            //    console.log(activeTab);
        }
    });



    //获取下拉框内容列表
    function GetCDComboboxData(text, inputid, isaddtext) {
        var datajson;//测点列表

        $.ajaxSettings.async = false;
        $.post("/JZ/JZHandler.ashx", { "action": "GetJZCDInfo" }, function (data) {
            datajson = (JSON.parse(data)).rows;
            if (isaddtext) {
                datajson.unshift({ id: "0", text: text });//将‘全部测点‘加到数据的头部，
            }
            //console.log(datajson);
        });
        $.ajaxSettings.async = true;

        var combobox = $(inputid).combobox({
            data: datajson,
            valueField: 'id',
            textField: 'text',
            onChange: function () {
                cdselected = combobox.combobox('getText');
                if (cdselected == 'text') {
                    cdselected = "";
                }
                getCdData();
            },
        });
    }
    function GetCDComboboxDataForfenxi(cdtext, inputid, isaddtext) {
        var datajson;//测点列表
        $.ajaxSettings.async = false;
        $.post("/JZ/JZHandler.ashx", { "action": "GetJZCDInfo" }, function (data) {
            datajson = (JSON.parse(data)).rows;
            for (var i = 0; i < datajson.length; i++) {
                if (datajson[i]["text"] == cdtext) {
                    datajson[i].selected = "true";
                }
            }
            if (isaddtext) {
                datajson.unshift({ id: "0", text: "选择测点" });//将‘全部测点‘加到数据的头部，
            }
            //console.log(datajson);
        });
        $.ajaxSettings.async = true;
        var combobox = $(inputid).combobox({
            data: datajson,
            valueField: 'id',
            textField: 'text',
            // select: cdselectedforfenxi,
            //onLoadSuccess: function () {
            //    $(this).combobox("select", cdtext);
            //},//这种方法也可以设置默认选择，但点击下三角不是选中状态。
            onChange: function () {
                cdselectedforfenxi = $(this).combobox('getText');//赋值。
                if (cdselectedforfenxi == '选择测点') {
                    cdselectedforfenxi = "";
                }
                // DataAnaly();//定义在largedata.js里
                //getCdData();
            },
        });

    }
});
// #endregion
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

// #region ---------------------机组监测  柱状图监测new-----------------------


//获取数据更新时间和类型，
function GetTimeAndType() {
    $.post("/JZ/JZHandler.ashx", { "action": "Qstatus" }, function (data) {
        var data = JSON.parse(data);
        var temptime = data.newtime;
        jzStatusType = data.status;
        if (oldtime != temptime) {  //true表示时间更新，第一次oldtime != temptime值为true，//如果时间没有更新，则什么也不做。
            oldtime = temptime;                                                      //保存当前时间
            $("#recordingdingtime-value").html(temptime);//更新时间
            ShowJZstatusType();                                                   //更新启停状态
            myajax(isFirstRun);                                                       //更新数据
            isFirstRun = false;//后面不在动态显示柱状图
        }
        function ShowJZstatusType() {
            if (jzStatusType != "" && jzStatusType != null) {
                //如果服务器时间与当前是按对比大于5分钟，就判定断网。
                //  if (updatetime != undefined && (new Date().getTime() - updatetime) <5 * 60 * 1000) {
                if (jzStatusType == 1) {
                    $("#ssisrun-value").html('正常');
                    $('#ssisrun-value').css("color", "green");
                    $('#round-marking').css('background-color', 'green');
                    //     $('#value-father1').each(function() {
                    //  $(this).css("background-color", "green")
                    //      })
                }
                else if (jzStatusType == 2) {
                    //  $('#value-father1').css("background-color", "yellow");
                    $("#ssisrun-value").html('报警');
                    $('#ssisrun-value').css("color", "yellow");
                    $('#round-marking').css("background-color", "yellow");
                } else if (jzStatusType == 3) {
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
        }
    })
}
//获取更新时间。
function GetTimeForFenxi() {
    $.post("/JZ/JZHandler.ashx", { "action": "newTime" }, function (data) {
        var temptime = JSON.parse(data);
        //  console.log(data);
        // var temptime = datanewtime;
        if (oldtime != temptime) {
            oldtime = temptime;
        }
    });
}

function GetNewCdData(cdname) {
    $.post("/JZ/JZHandler.ashx", { "action": "newCdData", "cdname": cdname }, function (data) {
        var data = JSON.parse(data);
        cdselectedvalue = (data * 1).toFixed(2);
    });
}

function myajax(isFirstRun) {

    // #region  查询data- 异步
    for (var i = 1; i < 9; i++) {
        (function Q(n) {//通过函数传参，避免异步导致的传参错误。
            $.post("/JZ/JZHandler.ashx", { "action": "Q" + n }, function (data) {
                mycallback(data, n);
            });
        })(i);
    }

    //回调函数- //机组监测
    function mycallback(data, index) {
        var data = JSON.parse(data);
        var fv = data.fv * 1;
        fv = fv.toFixed(2); // 输出结果为 2.45

        if (data.fd != "" && data.fd != null) {
            //机组监测
            $("#ss" + index + "-name").html(data.fd);
            $("#ss" + index + "-value").html(fv + ' g');
            //正常
            if (fv < data.yjz * 1) {
                $("#ss" + index + "-value").css("color", "green");
            }
            else if (fv >= data.yjz * 1 && fv < data.bjz * 1) {
                $("#ss" + index + "-value").css("color", "yellow");
            } else {
                $("#ss" + index + "-value").css("color", "red");
            }
            //柱状图监测
            // if (tabtitle=='机组柱状图') {
            CreateChart('canvas' + index, data);
            //  }

        }
        //绘制柱状图
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
    }
    // #endregion
}
// #endregion

//初始化机组监测表
function GetMonitorTable() {
    var datagrid = $('#dg1').datagrid({
        url: '/JZ/testHandler.ashx',
        method: 'post',
        queryParams: {
            action: 'GetCdData',
            Start: $('#dd1').datebox('getValue'),
            End: $('#dd3').datebox('getValue'),
            cdname: "",
        },
        pagination: "true",
        rownumbers: 'true',
        fitColumns: true, //自动使列适应表格宽度以防止出现水平滚动。
        singleselect: "true",
        align: 'center',
        //idField:'id',
        columns: [[
            { field: 'id', title: '编号', width: 100, align: 'center' },
            { field: 'jzname', title: '机组名称', width: 150, align: 'center' },
            { field: 'jzid', title: '机组编号', width: 130, align: 'center' },
            { field: "stime", title: '监测时间', width: 100, align: 'center' },
            { field: "status", title: '状态', width: 100, align: 'center', formatter: stateformater, styler: statestyler },
            { field: "cdname", title: '测点名称', width: 100, align: 'center' },
            { field: "cdvalue", title: '测点值', width: 100, align: 'center' }
         //    { field: "mid", title: '菜单号', width: "100", hidden: "true", align: 'center',display:'none' },
        ]]
    });
}

function Clear() {
    $('#cd').combobox('reset');
    $('#dd1').datebox('reset');
    $('#dd2').datebox('reset');
}

//搜索测点记录 根据测点名称
function getCdData() {
    $.ajax({
        url: "/JZ/testHandler.ashx?action=GetCdData",
        type: 'POST',
        data: {
            page: $("#dg1").datagrid('options').pageNumber,//第几页
            rows: $("#dg1").datagrid('options').pageSize,//每页数据量
            Start: $('#dd1').datebox('getValue'),//开始日期
            End: $('#dd3').datebox('getValue'),//结束日期
            cdname: cdselected,//测点名称
        },
        success: function (data) {
            $.messager.progress('close');
            data = eval('(' + data + ')');      //将一个json字符串解析成js对象
            $("#dg1").datagrid('loadData', data);
        }
    });
}

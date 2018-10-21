<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jz.aspx.cs" Inherits="Web.JZ.jz1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../CSS/jzstatus.css" rel="stylesheet" />
    <meta http-equiv="pragma" content="no-cache" />
    <script src="/Scripts/loading.js" type="text/javascript"></script>
    <link href="/EasyUI/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="/EasyUI/themes/gray/easyui.css" rel="stylesheet" type="text/css" />
    <link href="/CSS/jz.css" rel="stylesheet" type="text/css" />
    <script src="/EasyUI/jquery.min.js" type="text/javascript"></script>
    <script src="/EasyUI/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="/Scripts/distinguishBrowser.js" type="text/javascript"></script>
    <script src="../EasyUI/locale/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script src="../Echarts/echarts.min.js" type="text/javascript"></script>
    <script src="../JS/eChart.js" type="text/javascript"></script>
    <%--   <script src="../JS/largerdata.js" type="text/javascript"></script>--%>
    <%--<script src="../Echarts/echarts-kf.js" type="text/javascript"></script>--%>
</head>
<body>
    <form id="form1">
        <div class="jz-wrap clearfix">
            <div class="jz-con">
                <%-----切换标签-----%>
                <div class="easyui-tabs123" id="tabs" style="width: 100%; height: auto;">

                    <%--机组监测--%>
                    <div title="机组监测" class="jzjc" style="padding: 30px;">
                        <div id="jzjc-data1" style="width: 100%; margin: auto;">
                            <div id="jzzzt2" style="width: 800px; margin: auto;">
                                <div class="text-wrap1" style="background-image: url(/Images/jz-3D-zhouchen-nocirle-v2.png); background-repeat: no-repeat; background-size: 85% 69%; background-position: center; height: 480px; position: relative;">
                                    <div class="top">
                                        <div class="statusisrun tops status-all easyui-draggable" data-options="handle:'#titleisrun',onStartDrag:onDrag,onStopDrag:onStop">
                                            <%------第一行-------%>
                                            <div class="wrap-name" id="titleisrun">
                                                <div class="name" id="ssisrun-name">启停</div>
                                            </div>
                                            <%--------第二行-------%>
                                            <div class="wrap-value clearfix">
                                                <div class="number-father">
                                                    <div class="number">
                                                        <span id="round-marking"></span>
                                                    </div>
                                                </div>
                                                <div class="value-father " id="value-fatherisrun">
                                                    <div class="value-con">
                                                        <span class="value" id="ssisrun-value">无数据</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="statusT1 tops status-all easyui-draggable" data-options="handle:'#titleT1',onStartDrag:onDrag,onStopDrag:onStop">
                                            <%--第一行--%>
                                            <div class="wrap-name" id="titleT1">
                                                <div class="name" id="ssT1-name">供油温度</div>
                                            </div>
                                            <%--第二行--%>
                                            <div class="wrap-value clearfix">
                                                <div class="number-father">
                                                    <div class="number">
                                                        <span>T1</span>
                                                    </div>
                                                </div>
                                                <div class="value-father " id="value-fatherT1">
                                                    <div class="value-con">
                                                        <span class="value" id="ssT1-value">Null</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="status1 tops status-all easyui-draggable" data-options="handle:'#title1',onStartDrag:onDrag,onStopDrag:onStop">
                                            <%--第一行--%>
                                            <div class="wrap-name" id="title1">
                                                <div class="name" id="ss1-name">Null</div>
                                            </div>
                                            <%--第二行--%>
                                            <div class="wrap-value clearfix">
                                                <div class="number-father">
                                                    <div class="number">
                                                        <span>1</span>
                                                    </div>
                                                </div>
                                                <%--<div class="pipe-wrap">
                                                    <div class="pipe">|</div>
                                                </div>--%>
                                                <div class="value-father " id="value-father1">
                                                    <div class="value-con">
                                                        <span class="value" id="ss1-value">Null</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="status2  tops  status-all easyui-draggable" data-options="handle:'#title2',onStartDrag:onDrag,onStopDrag:onStop">
                                            <div class="wrap-name" id="title2">
                                                <div class="name" id="ss2-name">Null</div>
                                            </div>
                                            <div class="wrap-value clearfix">
                                                <div class="number-father">
                                                    <div class="number">
                                                        <span>2</span>
                                                    </div>
                                                </div>
                                                <div class="value-father " id="value-father2">
                                                    <div class="value-con">
                                                        <span class="value" id="ss2-value">Null</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="status3  tops  status-all easyui-draggable" data-options="handle:'#title3',onStartDrag:onDrag,onStopDrag:onStop">
                                            <div class="wrap-name" id="title3">
                                                <div class="name" id="ss3-name">Null</div>
                                            </div>
                                            <div class="wrap-value clearfix">
                                                <div class="number-father">
                                                    <div class="number">
                                                        <span>3</span>
                                                    </div>
                                                </div>
                                                <div class="value-father " id="value-father3">
                                                    <div class="value-con">
                                                        <span class="value" id="ss3-value">Null</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="status4  tops  status-all easyui-draggable" data-options="handle:'#title4',onStartDrag:onDrag,onStopDrag:onStop">
                                            <div class="wrap-name" id="title4">
                                                <div class="name" id="ss4-name">Null</div>
                                            </div>
                                            <div class="wrap-value clearfix">
                                                <div class="number-father">
                                                    <div class="number">
                                                        <span>4</span>
                                                    </div>
                                                </div>
                                                <div class="value-father " id="value-father4">
                                                    <div class="value-con">
                                                        <span class="value" id="ss4-value">Null</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="bottom">
                                        <div class="recordingtime tops status-all easyui-draggable" data-options="handle:'#recordingtime',onStartDrag:onDrag,onStopDrag:onStop">
                                            <%--第一行--%>
                                            <div class="wrap-name" id="recordingtime">
                                                <div class="name" id="recordingtime-name">更新时间</div>
                                            </div>
                                            <%--第二行--%>
                                            <div class="wrap-value clearfix">
                                                <div class="value-father " id="value-fatherrecordingtime">
                                                    <div class="value-con">
                                                        <span class="value" id="recordingdingtime-value"></span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="status5 bottoms status-all easyui-draggable" data-options="handle:'#title5',onStartDrag:onDrag,onStopDrag:onStop">
                                            <div class="wrap-name" id="title5">
                                                <div class="name" id="ss5-name">Null</div>
                                            </div>
                                            <div class="wrap-value clearfix">
                                                <div class="number-father">
                                                    <div class="number">
                                                        <span>5</span>
                                                    </div>
                                                </div>
                                                <div class="value-father " id="value-father5">
                                                    <div class="value-con">
                                                        <span class="value" id="ss5-value">Null</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="statusT2 bottoms status-all easyui-draggable" data-options="handle:'#titleT2',onStartDrag:onDrag,onStopDrag:onStop">
                                            <div class="wrap-name" id="titleT2">
                                                <div class="name" id="ssT2-name">供油温度</div>
                                            </div>
                                            <div class="wrap-value clearfix">
                                                <div class="number-father">
                                                    <div class="number">
                                                        <span>T2</span>
                                                    </div>
                                                </div>
                                                <div class="value-father " id="value-fatherT2">
                                                    <div class="value-con">
                                                        <span class="value" id="ssT2-value">Null</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="status6 bottoms status-all easyui-draggable" data-options="handle:'#title6',onStartDrag:onDrag,onStopDrag:onStop">
                                            <div class="wrap-name" id="title6">
                                                <div class="name" id="ss6-name">Null</div>
                                            </div>
                                            <div class="wrap-value clearfix">
                                                <div class="number-father">
                                                    <div class="number">
                                                        <span>6</span>
                                                    </div>
                                                </div>
                                                <div class="value-father " id="value-father6">
                                                    <div class="value-con">
                                                        <span class="value" id="ss6-value">Null</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="status7 bottoms status-all easyui-draggable" data-options="handle:'#title7',onStartDrag:onDrag,onStopDrag:onStop">
                                            <div class="wrap-name" id="title7">
                                                <div class="name" id="ss7-name">Null</div>
                                            </div>
                                            <div class="wrap-value clearfix">
                                                <div class="number-father">
                                                    <div class="number">
                                                        <span>7</span>
                                                    </div>
                                                </div>
                                                <div class="value-father " id="value-father7">
                                                    <div class="value-con">
                                                        <span class="value" id="ss7-value">Null</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="status8 bottoms status-all easyui-draggable" data-options="handle:'#title8',onStartDrag:onDrag,onStopDrag:onStop">
                                            <div class="wrap-name" id="title8">
                                                <div class="name" id="ss8-name">Null</div>
                                            </div>
                                            <div class="wrap-value clearfix">
                                                <div class="number-father">
                                                    <div class="number">
                                                        <span>8</span>
                                                    </div>
                                                </div>
                                                <div class="value-father " id="value-father8">
                                                    <div class="value-con">
                                                        <span class="value" id="ss8-value">Null</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="right">
                                        <%-------电机电流 (right)-------%>
                                        <div class="status9 rights status-all easyui-draggable" data-options="handle:'#title9',onStartDrag:onDrag,onStopDrag:onStop">
                                            <div class="wrap-name" id="title9">
                                                <div class="name" id="ss9-name">Null</div>
                                            </div>
                                            <div class="wrap-value clearfix">
                                                <div class="number-father">
                                                    <div class="number">
                                                        <span>I1</span>
                                                    </div>
                                                </div>
                                                <div class="value-father " id="value-father9">
                                                    <div class="value-con">
                                                        <span class="value" id="ss9-value">Null</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="status10 rights status-all easyui-draggable" data-options="handle:'#title10',onStartDrag:onDrag,onStopDrag:onStop">
                                            <div class="wrap-name" id="title10">
                                                <div class="name" id="ss10-name">Null</div>
                                            </div>
                                            <div class="wrap-value clearfix">
                                                <div class="number-father">
                                                    <div class="number">
                                                        <span>I2</span>
                                                    </div>
                                                </div>
                                                <div class="value-father " id="value-father10">
                                                    <div class="value-con">
                                                        <span class="value" id="ss10-value">Null</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="left">
                                        <div class="status11 lefts status-all easyui-draggable" data-options="handle:'#title11',onStartDrag:onDrag,onStopDrag:onStop">
                                            <div class="wrap-name" id="title11">
                                                <div class="name" id="ss11-name">Null</div>
                                            </div>
                                            <div class="wrap-value clearfix">
                                                <div class="number-father">
                                                    <div class="number">
                                                        <span>11</span>
                                                    </div>
                                                </div>
                                                <div class="value-father " id="value-father11">
                                                    <div class="value-con">
                                                        <span class="value" id="ss11-value">Null</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="status12 lefts status-all easyui-draggable" data-options="handle:'#title12',onStartDrag:onDrag,onStopDrag:onStop">
                                            <div class="wrap-name" id="title12">
                                                <div class="name" id="ss12-name">Null</div>
                                            </div>
                                            <div class="wrap-value clearfix">
                                                <div class="number-father">
                                                    <div class="number">
                                                        <span>12</span>
                                                    </div>
                                                </div>
                                                <div class="value-father " id="value-father12">
                                                    <div class="value-con">
                                                        <span class="value" id="ss12-value">Null</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="status13 lefts status-all easyui-draggable" data-options="handle:'#title13',onStartDrag:onDrag,onStopDrag:onStop">
                                            <div class="wrap-name" id="title13">
                                                <div class="name" id="ss13-name">Null</div>
                                            </div>
                                            <div class="wrap-value clearfix">
                                                <div class="number-father">
                                                    <div class="number">
                                                        <span>13</span>
                                                    </div>
                                                </div>
                                                <div class="value-father " id="value-father13">
                                                    <div class="value-con">
                                                        <span class="value" id="ss13-value">Null</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="status14 lefts status-all easyui-draggable" data-options="handle:'#title14',onStartDrag:onDrag,onStopDrag:onStop">
                                            <div class="wrap-name" id="title14">
                                                <div class="name" id="ss14-name">Null</div>
                                            </div>
                                            <div class="wrap-value clearfix">
                                                <div class="number-father">
                                                    <div class="number">
                                                        <span>14</span>
                                                    </div>
                                                </div>
                                                <div class="value-father " id="value-father14">
                                                    <div class="value-con">
                                                        <span class="value" id="ss14-value">Null</span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="order-wrap">
                                        <div class="outside order1 easyui-draggable">
                                            <span>1</span>
                                        </div>
                                        <div class="outside order2 easyui-draggable">
                                            <span>2</span>
                                        </div>
                                        <div class="outside order3 easyui-draggable">
                                            <span>3</span>
                                        </div>
                                        <div class="outside order4 easyui-draggable">
                                            <span>4</span>
                                        </div>
                                        <div class="outside order5 easyui-draggable">
                                            <span>5</span>
                                        </div>
                                        <div class="outside order6 easyui-draggable">
                                            <span>6</span>
                                        </div>
                                        <div class="outside order7 easyui-draggable">
                                            <span>7</span>
                                        </div>
                                        <div class="outside order8 easyui-draggable">
                                            <span>8</span>
                                        </div>
                                        <div class="outside orderI1 easyui-draggable">
                                            <span>I1</span>
                                        </div>
                                        <div class="outside orderI2 easyui-draggable">
                                            <span>I2</span>
                                        </div>
                                        <div class="outside order11 easyui-draggable">
                                            <span>11</span>
                                        </div>
                                        <div class="outside order12 easyui-draggable">
                                            <span>12</span>
                                        </div>
                                        <div class="outside order13 easyui-draggable">
                                            <span>13</span>
                                        </div>
                                        <div class="outside order14 easyui-draggable">
                                            <span>14</span>
                                        </div>
                                        <div class="outside orderT1 easyui-draggable">
                                            <span>T1</span>
                                        </div>
                                        <div class="outside orderT2 easyui-draggable">
                                            <span>T2</span>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>

                    <%--柱状图监测new--%>
                    <div title="柱状图监测" class="instruc" style="padding: 10px;">
                        <div style="width: 920px; height: 560px; border: 1px solid #ccc; margin: 0 auto;">
                            <div style="border: 0px solid #bbb; width: 920px; height: 280px; margin-top: 90px; margin: auto">
                                <div class="canvas1" style="border: 0px solid red; display: inline-block;">
                                    <canvas id="canvas1" width="220" height="270"></canvas>
                                </div>
                                <div class="canvas1" style="border: 0px solid red; display: inline-block;">
                                    <canvas id="canvas2" width="220" height="270"></canvas>
                                </div>
                                <div class="canvas1" style="border: 0px solid red; display: inline-block;">
                                    <canvas id="canvas3" width="220" height="270"></canvas>
                                </div>
                                <div class="canvas1" style="border: 0px solid red; display: inline-block;">
                                    <canvas id="canvas4" width="220" height="270"></canvas>
                                </div>
                            </div>
                            <div style="border: 0px solid green; width: 920px; height: 280px; margin-top: 90px; margin: auto">
                                <div class="canvas1" style="border: 0px solid red; display: inline-block;">
                                    <canvas id="canvas5" width="220" height="270"></canvas>
                                </div>
                                <div class="canvas1" style="border: 0px solid red; display: inline-block;">
                                    <canvas id="canvas6" width="220" height="270"></canvas>
                                </div>
                                <div class="canvas1" style="border: 0px solid red; display: inline-block;">
                                    <canvas id="canvas7" width="220" height="270"></canvas>
                                </div>
                                <div class="canvas1" style="border: 0px solid red; display: inline-block;">
                                    <canvas id="canvas8" width="220" height="270"></canvas>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%-- 监测记录--%>
                    <div title="监测记录" style="padding: 10px; width: auto;">

                        <div style="height: 35px;">
                            <div style="float: left">
                                <span>开始日期：</span>
                                <input id="dd1" type="text" class="easyui-datebox startdate" value="2011-11-11" style="width: 120px;" />
                                <span style="margin-left: 10px;">结束日期：</span>
                                <input id="dd2" type="text" class="easyui-datebox enddate" value="2011-11-11" style="width: 120px;" />
                                <div style="display: none">
                                    <input id="dd3" type="text" value="" class="easyui-datebox enddateafteroneday" />
                                </div>
                            </div>
                            <%--测点选择下拉框--%>
                            <div style="float: left; margin-left: 15px;">
                                <input id="cd" name="dept" value="全部测点" style="width: auto;" />
                            </div>

                            <%--搜索按钮--%>
                            <div style="float: left;">
                                <a id="bt1" href="#" class="easyui-linkbutton" plain="true" onclick="getCdData()" data-options="iconCls:'icon-search'">搜索</a>
                            </div>
                            <div class="datagrid-btn-separator" style="display: inline-block;"></div>

                            <%-- 下载测点数据--%>
                            <div style="float: left;">
                                <a href="#" class="easyui-linkbutton" plain="true" onclick="toExcel()" data-options="iconCls:'icon-save'">下载测点数据</a>
                            </div>

                            <div class="datagrid-btn-separator" style="display: inline-block;"></div>
                            <%--重置--%>
                            <div style="float: left;">
                                <a id='clear' href="#" class="easyui-linkbutton" plain="true" onclick="Clear()">重置</a>
                            </div>
                            <div class="datagrid-btn-separator" style="display: inline-block;"></div>
                        </div>
                        <div style="display: block;">
                            <table id="dg1"></table>
                        </div>
                    </div>
                    <%--信号分析--%>
                    <div title="信号分析" class="xhfx1" style="padding: 10px;">
                        <%--选择--%>
                        <div>
                            <div style="margin-bottom: 5px; float: left; border: 0px solid red;margin-right:8px;">
                                <input id="cd-fenxi" value="选择测点" />
                            </div>
                            <div style="float: left; border: 0px solid green;margin-right:8px;">
                                <input id="cd-tableType" value="选择分析类型" />
                            </div>
                            <div style="float: left; border: 0px solid green;">
                                <input id="cd-dataCount" value="选择数据量" />
                            </div>
                        </div>
                        <%--分析图表--%>
                        <div>
                            <div style="width: 100%; height: 500px; margin-bottom: 10px;">
                                <div style="width: 100%; height: 500px; float: left;">
                                    <div style="width: auto; height: 500px; padding: 8px;">
                                        <div id="chartpanel" style="width: auto; height: 500px; border: 1px solid #888;"></div>
                                    </div>

                                </div>
                                <div style="width: 100%; height: 500px; float: left;">
                                    <div style="width: auto; height: 500px; padding: 8px;">
                                        <div id="chartpanel2" style="width: auto; height: 500px; border: 1px solid #888;"></div>
                                    </div>

                                </div>
                            </div>
                            <div style="width: 100%; height: 500px; float: left;">
                                <div style="width: auto; height: 500px; padding: 8px;">
                                    <div id="chartpanel3" style="width: auto; height: 500px; border: 0px solid #888;"></div>
                                </div>

                            </div>
                            <div style="width: 100%; height: 500px; float: left;">
                                <div style="width: auto; height: 500px; padding: 8px;">
                                    <div id="chartpanel4" style="width: auto; height: 500px; border: 0px solid #888;"></div>
                                </div>

                            </div>

                            <div style="width: 100%; height: 500px; float: left;">
                                <div style="width: auto; height: 500px; padding: 8px;">
                                    <div id="chartpanel10" style="width: auto; height: 500px; border: 0px solid #888;"></div>
                                </div>

                            </div>

                        </div>

                    </div>

                    <%-- 机组文档--%>
                    <div title="机组文档" style="padding: 0px; width: auto">
                        <div id="newDiv"></div>
                        <script type="text/javascript">
                            document.getElementById("newDiv").innerHTML = '<object type="text/html" data="/File/Show2.html" width="100%" height="500px"></object>';
                        </script>
                    </div>
                </div>

            </div>
        </div>
    </form>
    <script type="text/javascript">
        function onDrag(e) {
            // console.log('wo');
            $(this).css("opacity", "0.3");
        }
        function onStop(e) {
            // console.log('wo');
            $(this).css("opacity", "1");
        }
    </script>
    <script type="text/javascript">
        //数据导出
        // var JZBH='<%=JZBH%>';
        //var cdname = cdselected;
        var cdname = '';
        function toExcel() {
            $.ajax({
                url: '/JZ/JZHandler.ashx',
                type: 'POST',
                data: {
                    action: 'ToExcel',
                    // JZId:JZBH,
                    cdname: cdselected,//测点名称
                    Start: $('#dd1').datebox('getValue'),//开始日期
                    End: $('#dd3').datebox('getValue')//结束日期
                },
                beforeSend: function () {
                    $.messager.progress({
                        text: '正在处理中......'
                    })
                },
                success: function (data) {
                    $.messager.progress('close');
                    //                    if (getBrowserType() == "Chrome") {
                    //                        location.href = data; //谷歌文件下载方式
                    //                    } else {
                    //                        window.open(data); //ie文件下载方式
                    //                    }
                    download(data);
                }
            });
        }
        //下载文件
        function download(url) {
            var iframe = document.createElement("iframe")
            iframe.style.display = "none";
            iframe.src = url;
            document.body.appendChild(iframe);
        }

    </script>
    <script src="../JS/date.js" type="text/javascript"></script>
    <script src="../JS/MouseM.js" type="text/javascript"></script>
    <script src="../JS/jz.js" type="text/javascript"></script>

    <script src="../JS/largerdata.js" type="text/javascript"></script>
</body>
</html>

/// <reference path="../Echarts/echarts-kf.js" />
/// <reference path="../EasyUI/jquery.min.js" />

var myChart = echarts.init(document.getElementById("chartpanel"));
var myChart2 = echarts.init(document.getElementById("chartpanel2"));
var myChart3 = echarts.init(document.getElementById("chartpanel3"));
var myChart4 = echarts.init(document.getElementById("chartpanel4"));

var dataCount;//数据量
var categoryData1;//横坐标
var data1;//纵坐标
//DataAnaly();

function DataAnalyForFrequency() {
    $.ajax({
        url: '/JZ/JZHandler.ashx',
        type: 'post',
        async: true,
        data: {
            action: 'GetJZCDfft',
            cdname: cdselectedforfenxi,
        },
        //beforeSend: function () {
        //    $.messager.progress({
        //        text: '正在处理中...'
        //    });
        // },
        success: function (data) {
            //  $.messager.progress('close');
            data1 = JSON.parse(data);
            //  console.log(data1);
            dataCount = data1.valueData.length/2;
            categoryData1 = new Array(dataCount);
            //   categoryData2 = new Array();
            GetcategoryData(dataCount);//获取横坐标的数据。

            var subtext = cdselectedforfenxi != '选择测点' ? GCname + ' ' + jzname + '\n' + '测点：' + cdselectedforfenxi + ' 测点值：' + cdselectedvalue + 'm/s² 采样频率：13183.5Hz ' + '\n' + oldtime : '无数据';

            var myoption = {
                color: ['#0070b8'],
                title: {
                    text: '频率图',
                    textStyle: {
                        color: '#cf2319',
                        fontWeight: 500,
                        fontSize: 16
                    },
                    itemGap: 7,
                    subtext: subtext,
                    subtextStyle: {
                        color: '#333', //'#cf2319',
                        fontSize: 14,
                        align: 'left',
                        lineHeight: 18,
                        width: 300,
                        height: 123,

                        rich: {
                            color: 'yellow',
                            fontSize: 14,
                            align: 'right',
                            lineHeight: 14,
                            width: 100,
                            // display:'block',
                            height: 23,
                            height: 28,
                        },

                    },
                    left: 60,
                    x: 'center',
                },
                toolbox: {
                    show: true,
                    itemSize: 14,//图形大小
                    itemGap: 5,//间隔
                    right: 40,
                    feature: {
                        dataZoom: {
                            yAxisIndex: false// 缩放 不控制y轴[1,2];
                        },
                        magicType: {
                            type: ['line', 'bar']//各种切换。, 'stack', 'tiled'
                        },
                        restore: {},//还原
                        //dataView: {
                        //    show: true,
                        //},//数据项目
                        saveAsImage: {
                            pixelRatio: 2
                        },
                    }
                },
                brush: {
                    //    show:false,
                    toolbox: ['lineX', 'clear'],//矩形 线性 切换多选单选 取消'rect','lineY', 'keep', 'clear'
                    xAxisIndex: 0,
                },
                tooltip: {
                    show: true,
                    trigger: 'axis',
                    //formatter:function (e) {
                    //    console.log(e);
                    //},
                    //triggerOn:'click',
                    axisPointer: {
                        type: 'cross',
                        crossStyle: {
                            color: '#db251a',//"#555",
                            width: 2,

                        },
                    },
                    //position: function (pos, params, el, elRect, size) {

                    //    $("#chartpanel").click(function (event) {
                    //       // console.log(event.type); // "click"事件
                    //        console.log(params[0].dataIndex);
                    //    });                   

                    //   },
                },
                grid: {
                    bottom: 60,
                    right: 25,
                    left: 65,
                    top: 90,
                    // width: '90%'
                },
                 dataZoom: [
                     {
                         type: 'inside'//平移 缩放
                     },
                {
                    type: 'slider'//滑动条型数据区域缩放
                }
                 ],
                xAxis: {
                    // data: data1.categoryData,

                    data: categoryData1,
                    name: '时间(ms)',
                    nameLocation: 'middle',//'center',//'end',//'start',
                    nameTextStyle: {
                        fontSize: 14,
                        fontWeight: 700,
                    },
                    nameGap: 30,
                    silent: false,//坐标轴是否是静态无法交互。
                    splitLine: {
                        show: false,
                    },
                    splitArea: {
                        show: false,
                    },
                    axisTick: {
                        show: false,
                    },
                    axisLabel: {
                        show: true,
                        interval: 99,
                    },
                    // type: 'value' ,
                    // interval:499,
                    //splitNumber:10,
                    //  animation: true,
                },
                yAxis: {
                    type: 'value',
                    splitArea: {
                        show: false
                    },
                    name: '幅值 (m/s²)',
                    nameGap: 38,
                    nameLocation: 'middle',
                    nameTextStyle: {
                        fontSize: 14,
                    },
                    axisPointer: { show: 'none', },
                },
                series: [{
                    name: 'bar',
                    type: 'line',
                    data: data1.valueData,
                    symbol: 'emptyCircle',// 'diamond',
                    symbolSize: 5,
                    sampling: 'max',//折线图在数据量远大于像素点时候的降采样策略，开启后可以有效的优化图表的绘制效率，默认关闭，也就是全部绘制不过滤数据点。
                    //data: data1.categoryData,
                    // Set `large` for large data amount
                    large: true,//是否开启大数据量优化
                    largeThreshold: 40000,//开启绘制优化的阈值。
                    progressive: 5000,//渐进式渲染时每一帧绘制图形数量，设为 0 时不启用渐进式渲染，支持每个系列单独配置。
                    progressiveThreshold: 4000,//启用渐进式渲染的图形数量阈值
                    //   progressiveChunkMode: 'sequential',//'mod',//'sequential': 
                }],
            };
            myChart2.setOption(myoption);
            //  myChart.on('mouseover','myChart', mouseDown);
         //   myChart.on('mouseover', myClick);
        //    myChart.on('brushselected', brushSelected);


            function myClick(param) {
                //  console.log(param);
                if (typeof param.seriesIndex == 'undefined') {
                    return;
                }
                if (param.type == 'mouseover') {
                    //    console.log(param.dataIndex);
                    var selectedClick = param.dataIndex;//鼠标点击的点横坐标；
                    var selectedItems = [];//鼠标点击点正负200区域横坐标，
                    var dataItem2 = [];//鼠标点击点正负200区域纵坐标，
                    var range = 100;
                    for (var i = 0; i < range * 2 + 1; i++) {
                        if (selectedClick - range + i >= 0) {
                            selectedItems.push(selectedClick - range + i);
                            dataItem2.push(data1.valueData[selectedClick - range + i])
                        }
                    }
                    //   console.log(selectedItems);
                    //    console.log(dataItem2);
                }
                var option = GetOption(selectedItems, dataItem2, 'line');
                //myChart3.setOption(option);


            }

            function brushSelected(params) {
                if (params.batch[0].areas.length == 0) {
                    return;
                }
                //console.log(params);
                //  console.log(params.batch[0].areas[0].range)
                var mainSeries = params.batch[0].areas;//选中区域
                var selectedItems = [];//选中区域的横坐标；
                var dataItem2 = [];//选中区域的纵坐标；
                for (var i = 0; i < mainSeries.length; i++) {
                    var length = mainSeries[i].coordRange[1] - mainSeries[i].coordRange[0] + 1;
                    if (mainSeries[i].coordRange && mainSeries[i].coordRange.length > 0) {
                        for (var j = 0; j < length; j++) {
                            selectedItems.push(mainSeries[i].coordRange[0] + j);

                            dataItem2.push(data1.valueData[mainSeries[i].coordRange[0] + j]);
                        }
                    }
                }
                var option = GetOption(selectedItems, dataItem2, 'line', '细化谱');
              //  myChart2.setOption(option);
            }

            function GetOption(selectedItems, dataItem2, seriesType, title) {
                var option = {
                    color: ['#0070b8'],
                    title: {
                        text: '' || title,
                        subtext: ' DataCount:' + echarts.format.addCommas(selectedItems.length),
                        x: 'center',

                    },
                    toolbox: {
                        show: true,
                        right: 40,
                        itemSize: 14,//图形大小
                        itemGap: 5,//间隔
                        feature: {
                            dataZoom: {
                                yAxisIndex: false// 缩放 不控制y轴[1,2];
                            },
                            saveAsImage: {
                                pixelRatio: 2
                            },
                            restore: {},//还原
                            //dataView: {
                            //    show: true,
                            //},//数据项目
                            magicType: {
                                type: ['line', 'bar']//各种切换。, 'stack', 'tiled'
                            },
                        }
                    },
                    tooltip: {
                        trigger: 'axis',
                        axisPointer: {
                            type: 'cross'
                        },
                        //    position: function (pos, params, el, elRect, size) {
                        // console.log(params[0].name);

                        //   },
                    },
                    brush: {
                        //    show:false,
                        //   toolbox: ['rect', 'lineX', 'lineY', 'keep', 'clear'],//矩形 线性 切换多选单选 取消
                        xAxisIndex: 0
                    },
                    grid: {
                        bottom: 60,
                        right: 25,
                        left: 65,
                        top: 90,
                        // width: '90%'
                    },
                    dataZoom: [
                        {
                            type: 'inside'//平移 缩放
                        },
                   //{
                   //    type: 'slider'//滑动条型数据区域缩放
                   //}
                    ],
                    xAxis: {
                        // data: data1.categoryData,
                        data: selectedItems,
                        name: '时间(ms)',
                        nameLocation: 'middle',
                        silent: false,//坐标轴是否是静态无法交互。
                        nameGap: 30,
                        nameTextStyle: {
                            fontSize: 14,
                            fontWeight: 700,
                        },
                        splitLine: {
                            show: false
                        },
                        splitArea: {
                            show: false
                        },
                        axisLabel: {
                            show: true,
                            interval: 199,
                        },
                        //  type: 'value' ,
                        // interval:100,
                        //  animation: true,
                    },
                    yAxis: {
                        splitArea: {
                            show: false
                        },
                        name: '幅值 (m/s²)',
                        nameGap: 38,
                        nameLocation: 'middle',
                        nameTextStyle: {
                            fontSize: 14,
                        },
                    },
                    series: [{
                        type: seriesType || 'bar',
                        data: dataItem2,
                        //data: data1.categoryData,
                        // Set `large` for large data amount
                        large: true,//是否开启大数据量优化
                        largeThreshold: 40000,//开启绘制优化的阈值。
                        progressive: 5000,//渐进式渲染时每一帧绘制图形数量，设为 0 时不启用渐进式渲染，支持每个系列单独配置。
                        progressiveThreshold: 4000,//启用渐进式渲染的图形数量阈值
                        //   progressiveChunkMode: 'sequential',//'mod',//'sequential': 
                    }],
                };
                return option;
            }

        },
    });
}

function DataAnaly() {
    $.ajax({
        url: '/JZ/JZHandler.ashx',
        type: 'post',
        async: true,
        data: {
            action: 'GetJZCDData',
            cdname: cdselectedforfenxi,
        },
        //beforeSend: function () {
        //    $.messager.progress({
        //        text: '正在处理中...'
        //    });
        // },
        success: function (data) {
            //  $.messager.progress('close');
            data1 = JSON.parse(data);
            dataCount = data1.valueData.length;
            categoryData1 = new Array(dataCount);
            //   categoryData2 = new Array();
            GetcategoryData(dataCount);//获取横坐标的数据。
            var subtext = cdselectedforfenxi != '选择测点' ? GCname + ' ' + jzname + '\n' + '测点：' + cdselectedforfenxi + ' 测点值：' + cdselectedvalue + 'm/s² 采样频率：13183.5Hz ' + '\n' + oldtime : '无数据';

            var myoption = {
                color: ['#0070b8'],
                title: {
                    text: '波形图',
                    textStyle: {
                        color: '#cf2319',
                        fontWeight: 500,
                        fontSize: 16
                    },
                    itemGap: 7,
                    subtext: subtext,
                    subtextStyle: {
                        color: '#333', //'#cf2319',
                        fontSize: 14,
                        align: 'left',
                        lineHeight: 18,
                        width: 300,
                        height: 123,

                        rich: {
                            color: 'yellow',
                            fontSize: 14,
                            align: 'right',
                            lineHeight: 14,
                            width: 100,
                            // display:'block',
                            height: 23,
                            height: 28,
                        },

                    },
                    left: 60,
                    x: 'center',
                },
                toolbox: {
                    show: true,
                    itemSize: 14,//图形大小
                    itemGap: 5,//间隔
                    right: 40,
                    feature: {
                        //dataZoom: {
                        //    yAxisIndex: false// 缩放 不控制y轴[1,2];
                        //},
                        magicType: {
                            type: ['line', 'bar']//各种切换。, 'stack', 'tiled'
                        },
                        restore: {},//还原
                        //dataView: {
                        //    show: true,
                        //},//数据项目
                        saveAsImage: {
                            pixelRatio: 2
                        },
                    }
                },
                brush: {
                    //    show:false,
                    toolbox: ['lineX'],//矩形 线性 切换多选单选 取消'rect','lineY', 'keep', 'clear'
                    xAxisIndex: 0,
                },
                tooltip: {
                    show: true,
                    trigger: 'axis',
                    //formatter:function (e) {
                    //    console.log(e);
                    //},
                    //triggerOn:'click',
                    axisPointer: {
                        type: 'cross',
                        crossStyle: {
                            color: '#db251a',//"#555",
                            width: 2,

                        },
                    },
                    //position: function (pos, params, el, elRect, size) {

                    //    $("#chartpanel").click(function (event) {
                    //       // console.log(event.type); // "click"事件
                    //        console.log(params[0].dataIndex);
                    //    });                   

                    //   },
                },
                grid: {
                    bottom: 60,
                    right: 25,
                    left: 65,
                    top: 90,
                    // width: '90%'
                },
                // dataZoom: [
                //     {
                //      //   type: 'inside'//平移 缩放
                //     },
                //{
                //   // type: 'slider'//滑动条型数据区域缩放
                //}
                // ],
                xAxis: {
                    // data: data1.categoryData,

                    data: categoryData1,
                    name: '时间(ms)',
                    nameLocation: 'middle',//'center',//'end',//'start',
                    nameTextStyle: {
                        fontSize: 14,
                        fontWeight: 700,
                    },
                    nameGap: 30,
                    silent: false,//坐标轴是否是静态无法交互。
                    splitLine: {
                        show: false,
                    },
                    splitArea: {
                        show: false,
                    },
                    axisTick: {
                        show: false,
                    },
                    axisLabel: {
                        show: true,
                        interval: 499,
                    },
                    // type: 'value' ,
                    // interval:499,
                    //splitNumber:10,
                    //  animation: true,
                },
                yAxis: {
                    type: 'value',
                    splitArea: {
                        show: false
                    },
                    name: '幅值 (m/s²)',
                    nameGap: 38,
                    nameLocation: 'middle',
                    nameTextStyle: {
                        fontSize: 14,
                    },
                    axisPointer: { show: 'none', },
                },
                series: [{
                    name: 'bar',
                    type: 'line',
                    data: data1.valueData,
                    symbol: 'emptyCircle',// 'diamond',
                    symbolSize: 5,
                    // sampling: 'max',//折线图在数据量远大于像素点时候的降采样策略，开启后可以有效的优化图表的绘制效率，默认关闭，也就是全部绘制不过滤数据点。
                    //data: data1.categoryData,
                    // Set `large` for large data amount
                    large: true,//是否开启大数据量优化
                    largeThreshold: 40000,//开启绘制优化的阈值。
                    progressive: 5000,//渐进式渲染时每一帧绘制图形数量，设为 0 时不启用渐进式渲染，支持每个系列单独配置。
                    progressiveThreshold: 4000,//启用渐进式渲染的图形数量阈值
                    //   progressiveChunkMode: 'sequential',//'mod',//'sequential': 
                }],
            };
            myChart.setOption(myoption);
            //  myChart.on('mouseover','myChart', mouseDown);
            //myChart.on('mouseover', myClick);
            myChart.on('brushselected', brushSelected);


            function myClick(param) {
                //  console.log(param);
                if (typeof param.seriesIndex == 'undefined') {
                    return;
                }
                if (param.type == 'mouseover') {
                    //    console.log(param.dataIndex);
                    var selectedClick = param.dataIndex;//鼠标点击的点横坐标；
                    var selectedItems = [];//鼠标点击点正负200区域横坐标，
                    var dataItem2 = [];//鼠标点击点正负200区域纵坐标，
                    var range = 100;
                    for (var i = 0; i < range * 2 + 1; i++) {
                        if (selectedClick - range + i >= 0) {
                            selectedItems.push(selectedClick - range + i);
                            dataItem2.push(data1.valueData[selectedClick - range + i])
                        }
                    }
                    //   console.log(selectedItems);
                    //    console.log(dataItem2);
                }
                var option = GetOption(selectedItems, dataItem2, 'line');
                myChart3.setOption(option);


            }

            function brushSelected(params) {
                if (params.batch[0].areas.length == 0) {
                    return;
                }
                //console.log(params);
                //  console.log(params.batch[0].areas[0].range)
                var mainSeries = params.batch[0].areas;//选中区域
                var selectedItems = [];//选中区域的横坐标；
                var dataItem2 = [];//选中区域的纵坐标；
                for (var i = 0; i < mainSeries.length; i++) {
                    var length = mainSeries[i].coordRange[1] - mainSeries[i].coordRange[0] + 1;
                    if (mainSeries[i].coordRange && mainSeries[i].coordRange.length > 0) {
                        for (var j = 0; j < length; j++) {
                            selectedItems.push(mainSeries[i].coordRange[0] + j);

                            dataItem2.push(data1.valueData[mainSeries[i].coordRange[0] + j]);
                        }
                    }
                }
                var option = GetOption(selectedItems, dataItem2, 'line', '细化谱');
               // myChart2.setOption(option);
            }

            function GetOption(selectedItems, dataItem2, seriesType, title) {
                var option = {
                    color: ['#0070b8'],
                    title: {
                        text: '' || title,
                        subtext: ' DataCount:' + echarts.format.addCommas(selectedItems.length),
                        x: 'center',

                    },
                    toolbox: {
                        show: true,
                        right: 40,
                        itemSize: 14,//图形大小
                        itemGap: 5,//间隔
                        feature: {
                            dataZoom: {
                                yAxisIndex: false// 缩放 不控制y轴[1,2];
                            },
                            saveAsImage: {
                                pixelRatio: 2
                            },
                            restore: {},//还原
                            //dataView: {
                            //    show: true,
                            //},//数据项目
                            magicType: {
                                type: ['line', 'bar']//各种切换。, 'stack', 'tiled'
                            },
                        }
                    },
                    tooltip: {
                        trigger: 'axis',
                        axisPointer: {
                            type: 'cross'
                        },
                        //    position: function (pos, params, el, elRect, size) {
                        // console.log(params[0].name);

                        //   },
                    },
                    brush: {
                        //    show:false,
                        //   toolbox: ['rect', 'lineX', 'lineY', 'keep', 'clear'],//矩形 线性 切换多选单选 取消
                        xAxisIndex: 0
                    },
                    grid: {
                        bottom: 60,
                        right: 25,
                        left: 65,
                        top: 90,
                        // width: '90%'
                    },
                    dataZoom: [
                        {
                            type: 'inside'//平移 缩放
                        },
                   //{
                   //    type: 'slider'//滑动条型数据区域缩放
                   //}
                    ],
                    xAxis: {
                        // data: data1.categoryData,
                        data: selectedItems,
                        name: '时间(ms)',
                        nameLocation: 'middle',
                        silent: false,//坐标轴是否是静态无法交互。
                        nameGap: 30,
                        nameTextStyle: {
                            fontSize: 14,
                            fontWeight: 700,
                        },
                        splitLine: {
                            show: false
                        },
                        splitArea: {
                            show: false
                        },
                        axisLabel: {
                            show: true,
                            interval: 199,
                        },
                        //  type: 'value' ,
                        // interval:100,
                        //  animation: true,
                    },
                    yAxis: {
                        splitArea: {
                            show: false
                        },
                        name: '幅值 (m/s²)',
                        nameGap: 38,
                        nameLocation: 'middle',
                        nameTextStyle: {
                            fontSize: 14,
                        },
                    },
                    series: [{
                        type: seriesType || 'bar',
                        data: dataItem2,
                        //data: data1.categoryData,
                        // Set `large` for large data amount
                        large: true,//是否开启大数据量优化
                        largeThreshold: 40000,//开启绘制优化的阈值。
                        progressive: 5000,//渐进式渲染时每一帧绘制图形数量，设为 0 时不启用渐进式渲染，支持每个系列单独配置。
                        progressiveThreshold: 4000,//启用渐进式渲染的图形数量阈值
                        //   progressiveChunkMode: 'sequential',//'mod',//'sequential': 
                    }],
                };
                return option;
            }
        },


    });

}

function GetcategoryData(dataCount) {
    for (var i = 0; i <= dataCount; i++) {
        categoryData1[i] = i;
    }
}




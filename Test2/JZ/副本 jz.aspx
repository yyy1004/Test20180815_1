<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jz.aspx.cs" Inherits="Web.JZ.jz1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
     <body style="height: 200pt; margin: 0">
       <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
       <div id="container" style="height:100pt;width:100pt " >
         
         </div>
       <div id="Div1" style="height:100pt;width:100pt; float:left" ></div>
       <div id="Div2" style="height:100pt;width:100pt; float:left " ></div>
       <div id="Div3" style="height:150pt;width:150pt; float:left " ></div>
       <div id="Div4" style="height:150pt;width:150pt; float:left " ></div>
       <div id="Div5" style="height:150pt;width:150pt; float:left " ></div>
       <div id="Div6" style="height:150pt;width:150pt; float:left " ></div>
       <div id="Div7" style="height:150pt;width:150pt ; float:left" ></div>

       <script type="text/javascript" src="http://echarts.baidu.com/gallery/vendors/echarts/echarts.min.js"></script>
       <script type="text/javascript" src="http://echarts.baidu.com/gallery/vendors/echarts-gl/echarts-gl.min.js"></script>
       <script type="text/javascript" src="http://echarts.baidu.com/gallery/vendors/echarts-stat/ecStat.min.js"></script>
       <script type="text/javascript" src="http://echarts.baidu.com/gallery/vendors/echarts/extension/dataTool.min.js"></script>
       <script type="text/javascript" src="http://echarts.baidu.com/gallery/vendors/echarts/map/js/china.js"></script>
       <script type="text/javascript" src="http://echarts.baidu.com/gallery/vendors/echarts/map/js/world.js"></script>
       <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=ZUONbpqGBsYGXNIYHicvbAbM"></script>
       <script type="text/javascript" src="http://echarts.baidu.com/gallery/vendors/echarts/extension/bmap.min.js"></script>
       <script type="text/javascript" src="http://echarts.baidu.com/gallery/vendors/simplex.js"></script>
       <script type="text/javascript">
           var dom = document.getElementById("container");
           var myChart = echarts.init(dom);
           var app = {};
           option = null;
           option = {
               //               tooltip: {
               //                   formatter: "{a} <br/>{b} : {c}%"
               //               },
               //               toolbox: {
               //                   feature: {
               //                       restore: {},
               //                       saveAsImage: {}
               //                   }
               //               },
               series: [
        {
            name: '温度',
            type: 'gauge',
            detail: { formatter: '{value}' },
            data: [{ value: 50, name: '温度'}],
            min: '0',
            max: '45',
            splitLine: { //分割线样式（及10、20等长线样式）
                length: 10,
                lineStyle: { // 属性lineStyle控制线条样式
                    width: 0.5
                }
            },
            axisTick: { //刻度线样式（及短线样式）
                length: 1
            },
            axisLine: {
                show: true,
                lineStyle: { // 属性lineStyle控制线条样式
                    color: [ //表盘颜色

                             [0.7, "#0000FF"], //51%-70%处的颜色
                              [0.9, "#FFFF00"], //70%-90%处的颜色
                              [1, "#FF0000"]//90%-100%处的颜色
                          ],
                    width: 10//表盘宽度
                }
            }
        }

    ]
           };

           setInterval(function () {
               option.series[0].data[0].value = (Math.random() * 50).toFixed(2) - 0;
               myChart.setOption(option, true);
           }, 20000);
           ;
           if (option && typeof option === "object") {
               myChart.setOption(option, true);
           }
       </script>
    <script type="text/javascript">
        //画图
        //  function draw(dv, Dname, Dmin, Dmid1, Dmid2, Dmax, Dval) {
        var Dv = "Div2";
        var Dname = '速度';
        var Dmin = 10;
        var Dmid1 = 20;
        var Dmid2 = 30;
        var Dmax = 50;
        var Dval = 40;
        var dom = document.getElementById(Dv);
        var myChart3 = echarts.init(dom);
        var app = {};
        option3 = null;
        option3 = {
            //               tooltip: {
            //                   formatter: "{a} <br/>{b} : {c}%"
            //               },
            //               toolbox: {
            //                   feature: {
            //                       restore: {},
            //                       saveAsImage: {}
            //                   }
            //               },
            series: [
        {
            name: Dname,
            type: 'gauge',
            detail: { formatter: '{value}', fontSize: 15 },
            data: [{ value: 50, name: Dname}],
            min: '0',
            max: '45',
            splitLine: { //分割线样式（及10、20等长线样式）
                length: 10,
                lineStyle: { // 属性lineStyle控制线条样式
                    width: 0.5
                }
            },
            axisTick: { //刻度线样式（及短线样式）
                length: 1
            },
            axisLabel : { //文字样式（及“10”、“20”等文字样式）
                     color : "black",
                     distance: -25 ,//文字离表盘的距离
                      fontSize : 4
                  },
              pointer : { //指针样式
                     length: '60%',
                     width:3
                 },
            axisLine: {
                show: true,
                lineStyle: { // 属性lineStyle控制线条样式
                    color: [ //表盘颜色

                             [Dmid1 / Dmax, "#0000FF"], //51%-70%处的颜色
                              [Dmid2 / Dmax, "#FFFF00"], //70%-90%处的颜色
                              [1, "#FF0000"]//90%-100%处的颜色
                          ],
                    width: 5//表盘宽度
                }
            }
        }

    ]
        };

        setInterval(function () {
            option3.series[0].data[0].value = (Math.random() * 50).toFixed(2) - 0;
            myChart3.setOption(option3, true);
        }, 2000);
        ;
        if (option3 && typeof option3 === "object") {
            myChart3.setOption(option3, true);
        }
        //             draw("Div1", "温度", 10, 25, 35, 50, 30);
        //             draw("Div2", "速度", 100, 250, 350, 500, 300);
       
       </script>       

       <script type="text/javascript">
           //画图
           //  function draw(dv, Dname, Dmin, Dmid1, Dmid2, Dmax, Dval) {
           var Dv = "Div1";
           var Dname = '速度';
           var Dmin = 10;
           var Dmid1 = 20;
           var Dmid2 = 30;
           var Dmax = 50;
           var Dval = 40;
           var dom = document.getElementById(Dv);
           var myChart2 = echarts.init(dom);
           var app = {};
           option2 = null;
           option2 = {
               //               tooltip: {
               //                   formatter: "{a} <br/>{b} : {c}%"
               //               },
               //               toolbox: {
               //                   feature: {
               //                       restore: {},
               //                       saveAsImage: {}
               //                   }
               //               },
               series: [
        {
            name: Dname,
            type: 'gauge',
            detail: { formatter: '{value}' },
            data: [{ value: 50, name: Dname}],
            min: '0',
            max: '45',
            splitLine: { //分割线样式（及10、20等长线样式）
                length: 10,
                lineStyle: { // 属性lineStyle控制线条样式
                    width: 0.5
                }
            },
            axisTick: { //刻度线样式（及短线样式）
                length: 1
            },
            axisLine: {
                show: true,
                lineStyle: { // 属性lineStyle控制线条样式
                    color: [ //表盘颜色

                             [Dmid1 / Dmax, "#0000FF"], //51%-70%处的颜色
                              [Dmid2 / Dmax, "#FFFF00"], //70%-90%处的颜色
                              [1, "#FF0000"]//90%-100%处的颜色
                          ],
                    width: 10//表盘宽度
                }
            }
        }

    ]
           };

    setInterval(function () {
        var x = document.getElementById("TextBox1").value;
        option2.series[0].data[0].value = x - 0;
               myChart2.setOption(option2, true);
           }, 2000);
           ;
           if (option2 && typeof option2 === "object") {
               myChart2.setOption(option2, true);
           }
           //             draw("Div1", "温度", 10, 25, 35, 50, 30);
           //             draw("Div2", "速度", 100, 250, 350, 500, 300);
       
       </script>
   </body>
    </form>
</body>
</html>

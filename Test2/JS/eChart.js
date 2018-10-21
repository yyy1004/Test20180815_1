function sBarChart(canvas, data1, options) {
    this.canvas = document.getElementById(canvas);
    this.context = this.canvas.getContext('2d');
    //   this.data = data;             // 存放图表数据
    this.data1 = data1;
    // this.dataLength = this.data.length; // 图表数据的长度
    this.dataLength = 1;
    this.width = this.canvas.width;  // canvas 宽度
    this.height = this.canvas.height;   // canvas 高度
    this.padding = 50;        // canvas 内边距
    this.paddingTop = 50;        // canvas 内上边距
    this.yEqual = 5;                            // y轴分成5等分
    this.yLength = 0;                           // y轴坐标点之间的真实长度
    this.xLength = 0;                           // x轴坐标点之间的真实长度
    this.yFictitious = 0;                       // y轴坐标点之间显示的间距
    this.yRatio = 0;                            // y轴坐标真实长度和坐标间距的比
    this.bgColor = '#ffffff';                   // 默认背景颜色
    this.fillColor = '#1E9FFF';                 // 默认填充颜色
    this.axisColor = '#666666';                 // 坐标轴颜色
    this.contentColor = '#eeeeee';              // 内容横线颜色
    this.titleColor = '#000000';                // 图表标题颜色
    this.title = '';                            // 图表标题
    this.titlePosition = 'top';                 // 图表标题位置: top / bottom
    this.looped = null;                         // 是否循环
    this.current = 0;                           // 当前加载柱状图高度的百分数
    this.currentIndex = -1;
    this.onceMove = -1;
    this.isloop = false;//是否动画显示
    this.init(options);
}

sBarChart.prototype = {
    init: function (options) {
        if (options) {
            this.padding = options.padding || 50;
            this.paddingTop = options.paddingTop || 50;
            this.yEqual = options.yEqual || 5;
            this.bgColor = options.bgColor || '#ffffff';
            this.fillColor = options.fillColor || '#1E9FFF';
            this.axisColor = options.axisColor || '#666666';
            this.contentColor = options.contentColor || '#eeeeee';
            this.titleColor = options.titleColor || '#000000';
            this.title = options.title;
            this.titlePosition = options.titlePosition || 'top';
            this.isloop = options.isloop || false;
        }
        //柱状填充颜色
        if (this.data1.fv * 1 >= 0 && this.data1.fv * 1 < this.data1.yjz) {
            this.fillColor = 'green';
        } else if (this.data1.fv * 1 >= this.data1.yjz && this.data1.fv * 1 < this.data1.bjz) {
            this.fillColor = 'yellow';
        } else if (this.data1.fv * 1 >= this.data1.bjz) {
            this.fillColor = 'red';
        } else {
            this.fillColor = 'gray';
        }

        //   this.yLength = Math.floor((this.height - this.padding * 2 - 10) / this.yEqual);//y轴像素长度
        this.yLength = Math.floor(this.height - this.padding - this.paddingTop * 1 - 5);//y轴像素长度
        this.xLength = Math.floor((this.width - this.padding * 1.5 - 10) / this.dataLength);//x轴像素长度
        //  this.xLength = 20;//x轴像素长度
        //  this.yFictitious = this.getYFictitious(this.data);
        this.yFictitious = this.data1.limitHigh;
        this.yRatio = this.yLength / this.data1.limitHigh;//像素长度与数据长度比率。
        if (this.isloop) {
            this.looping();//柱状动画
        } else {
            this.noloogping();
        }


    },
    looping: function () {
        this.looped = requestAnimationFrame(this.looping.bind(this));
        if (this.current < 100) {
            //current 用来计算当前柱状的高度占最终高度的百分之几，通过不断循环实现柱状上升的动画
            this.current = (this.current + 3) > 100 ? 100 : (this.current + 3);
            //    console.log('looping');
            this.drawAnimation();
        } else {
            window.cancelAnimationFrame(this.looped);
            this.looped = null;
            this.watchHover();//监听鼠标事件
        }
    },
    noloogping: function () {
        this.current = 100;
        this.drawAnimation();
        this.watchHover();//监听鼠标事件
    },
    drawAnimation: function () {
        for (var i = 0; i < this.dataLength; i++) {
            var x = Math.ceil(this.data1.fv * this.current / 100 * this.yRatio);//数据当前值
            // var x = Math.ceil(this.data1.fv  / 100 * this.yRatio);//数据当前值
            var y = this.height - this.padding - x;

            this.data1.left = this.padding + this.xLength * (0 + 0.4);
            this.data1.top = y;
            this.data1.right = this.padding + this.xLength * (0 + 0.6);
            this.data1.bottom = this.height - this.padding;
            this.drawUpdate();
        }
    },


    drawUpdate: function () {
        this.context.fillStyle = this.bgColor;//设置背景颜色。
        this.context.fillRect(0, 0, this.width, this.height);//绘制矩形
        this.drawAxis();//绘制坐标轴
        this.drawPoint();//绘制刻度
        this.drawTitle();//绘制标题
        this.drawChart();//绘制表
    },
    drawChart: function () {
        this.context.fillStyle = this.fillColor;
        for (var i = 0; i < this.dataLength; i++) {
            this.context.fillRect(
                this.data1.left,
                this.data1.top,
                this.data1.right - this.data1.left,
                this.data1.bottom - this.data1.top
            );
            this.context.font = '14px Arial'
            //添加当前值
            this.context.fillText(
                this.data1.fv * this.current / 100,//设置文字
                this.data1.left + this.xLength / 5,//横坐标
                this.data1.top - 5 //纵坐标
            );
        }
    },
    //绘制坐标轴
    drawAxis: function () {
        this.context.beginPath();
        this.context.strokeStyle = this.axisColor;//坐标轴的颜色
        // y轴线, +0.5是为了解决canvas画1像素会显示成2像素的问题
        this.context.moveTo(this.padding + 0.5, this.height - this.padding + 0.5);
        this.context.lineTo(this.padding + 0.5, this.paddingTop * 1 + 0.5);
        // this.context.stroke();
        // x轴线
        this.context.moveTo(this.padding + 0.5, this.height - this.padding + 0.5);
        this.context.lineTo(this.width - this.padding / 2 + 0.5, this.height - this.padding + 0.5);
        this.context.stroke();
    },

    drawPoint: function () {
        //x轴刻度
        this.context.beginPath();
        this.context.font = "15px Arial";//刻度字体 大小
        this.context.textAlign = 'center';//字体居中
        this.context.fillStyle = this.axisColor;//设置或返回用于填充绘画的颜色、渐变或模式
        this.context.strokeStyle = '#000';//笔头颜色
        for (var i = 0; i < this.dataLength; i++) {
            var xAxis = this.data1.fd;//x轴
            var xlen = this.xLength * (i + 1);//y轴
            // this.context.moveTo(this.padding + xlen, this.height - this.padding);//绘制刻度
            //  this.context.lineTo(this.padding + xlen, this.height - this.padding + 5);
            this.context.fillText(xAxis, this.padding + xlen - this.xLength / 2, this.height - this.padding + 24);//x轴-填充文字
            //  this.context.stroke();//开始绘制x刻度
        }
        this.context.stroke();//一起绘制
        //y轴坐标
        this.context.beginPath();
        this.context.font = '12px Microsoft YaHei';//字体大小 颜色
        this.context.textAlign = 'right';//对齐方式
        this.context.fillStyle = this.axisColor;//坐标轴颜色
        //设置0位置的刻度。
        this.context.moveTo(this.padding + 0.5, this.height - this.padding + 0.5);//设置起点
        this.context.lineTo(this.padding - 4.5, this.height - this.padding + 0.5);//设置终点  y值不变，x有变化，说明是横向的。
        this.context.stroke();
        this.context.fillText(0, this.padding - 10, this.height - this.padding + 5);//填充文字  --->0，y轴。文字的坐标起点，都是左下角
        if (this.data1.yjz) {
            ///var y = this.yFictitious * (i + 1);//第一个刻度值
            var y = this.data1.yjz;//第一个刻度值
            //var ylen = this.yLength * (i + 1);//第一个y轴刻度的长度。
            var ylen = y * this.yRatio;//第一个y轴刻度的长度。
            this.context.beginPath();
            this.context.strokeStyle = this.axisColor;//刻度颜色
            this.context.moveTo(this.padding + 0.5, this.height - this.padding - ylen + 0.5);//设置起点
            this.context.lineTo(this.padding - 4.5, this.height - this.padding - ylen + 0.5);//设置终点
            this.context.stroke();//绘制y轴刻度
            this.context.fillText(y, this.padding - 10, this.height - this.padding - ylen + 5);//填充刻度值 
            this.context.beginPath();
            this.context.strokeStyle = this.contentColor;//设置y轴横线颜色
            this.context.moveTo(this.padding + 0.5, this.height - this.padding - ylen + 0.5)//起点
            this.context.lineTo(this.width - this.padding / 2 + 0.5, this.height - this.padding - ylen + 0.5);//终点
            this.context.stroke();
        }
        if (this.data1.bjz) {
            ///var y = this.yFictitious * (i + 1);//第一个刻度值
            var y = this.data1.bjz;//第一个刻度值
            //var ylen = this.yLength * (i + 1);//第一个y轴刻度的长度。
            var ylen = y * this.yRatio;//第一个y轴刻度的长度。
            this.context.beginPath();
            this.context.strokeStyle = this.axisColor;//刻度颜色
            this.context.moveTo(this.padding + 0.5, this.height - this.padding - ylen + 0.5);//设置起点
            this.context.lineTo(this.padding - 4.5, this.height - this.padding - ylen + 0.5);//设置终点
            this.context.stroke();//绘制y轴刻度
            this.context.fillText(y, this.padding - 10, this.height - this.padding - ylen + 5);//填充刻度值 
            this.context.beginPath();
            this.context.strokeStyle = this.contentColor;//设置y轴横线颜色
            this.context.moveTo(this.padding + 0.5, this.height - this.padding - ylen + 0.5)//起点
            this.context.lineTo(this.width - this.padding / 2 + 0.5, this.height - this.padding - ylen + 0.5);//终点
            this.context.stroke();
        }
        if (this.data1.limitHigh) {
            ///var y = this.yFictitious * (i + 1);//第一个刻度值
            var y = this.data1.limitHigh;//第一个刻度值
            //var ylen = this.yLength * (i + 1);//第一个y轴刻度的长度。
            var ylen = y * this.yRatio;//第一个y轴刻度的长度。
            this.context.beginPath();
            this.context.strokeStyle = this.axisColor;//刻度颜色
            this.context.moveTo(this.padding + 0.5, this.height - this.padding - ylen + 0.5);//设置起点
            this.context.lineTo(this.padding - 4.5, this.height - this.padding - ylen + 0.5);//设置终点
            this.context.stroke();//绘制y轴刻度
            this.context.fillText(y, this.padding - 10, this.height - this.padding - ylen + 5);//填充刻度值 
            this.context.beginPath();
            this.context.strokeStyle = this.contentColor;//设置y轴横线颜色
            this.context.moveTo(this.padding + 0.5, this.height - this.padding - ylen + 0.5)//起点
            this.context.lineTo(this.width - this.padding / 2 + 0.5, this.height - this.padding - ylen + 0.5);//终点
            this.context.stroke();
        }


    },
    drawTitle: function () {
        if (this.title) {
            this.context.beginPath();
            this.context.textAlign = 'center';
            this.context.fillStyle = this.titleColor;
            this.context.font = '16px Microsoft YaHei';
            if (this.titlePosition === 'bottom' && this.padding >= 40) {
                this.context.fillText(this.title, this.width / 2, this.height - 5)
            } else {
                this.context.fillText(this.title, this.width / 2, this.padding / 2)
            }
        }
    },
    /**
    * 监听鼠标移动事件
    */
    watchHover: function () {
        var self = this;
        self.canvas.addEventListener('mousemove', function (ev) {
            ev = ev || window.event;
            self.currentIndex = -1;
            for (var i = 0; i < self.dataLength; i++) {
                if (ev.offsetX > self.data1.left &&
                    ev.offsetX < self.data1.right &&
                    ev.offsetY > self.data1.top &&
                    ev.offsetY < self.data1.bottom) {
                    self.currentIndex = i;
                }
            }
            self.drawHover();
        })
    },
    drawHover: function () {
        if (this.currentIndex !== -1) {
            if (this.onceMove === -1) {
                this.onceMove = this.currentIndex;
                this.canvas.style.cursor = 'pointer';
            }
        } else {
            if (this.onceMove !== -1) {
                this.onceMove = -1;
                this.canvas.style.cursor = 'inherit';
            }
        }
    },
    /**
    * y轴坐标点之间显示的间距
    * @param data 
    * @return y轴坐标间距
    */
    //getYFictitious: function (data) {
    //    var arr = data.slice(0);//从0个位置开始选取。
    //    arr.sort(function (a, b) {
    //        return -(a.value - b.value);
    //    });//从大到小排序
    //    var len = Math.ceil(arr[0].value / this.yEqual);//最大值/等份==每段长度(data)。
    //    var pow = len.toString().length - 1;
    //    pow = pow > 2 ? 2 : pow;
    //    return Math.ceil(len / Math.pow(10, pow)) * Math.pow(10, pow);
    //}

}
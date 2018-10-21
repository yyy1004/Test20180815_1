function Zz(a,b)
{
	if (session("ifEng")== 0) 
		Zz= a
	else
		Zz= b	
}
function PadeLeft(str,chr,iLen)
{
	var len = str.length;
	if(len<iLen)
	{
		for(var i=len;i<iLen;i++)
		{
			str = chr + str;
		}
	}
	return str;
}
function getLastLocation(str,chr)
{
    var len = str.length;   

    for(var i=len-1;i>=0;i--)
    {
        var s = str.substring(i,i+1);
        if(s == chr)
        {
            return i;
        }
    }
    return -1;
}
//去掉字串左边的字符
function TrimStart(str,chr)
{
	if (str.charAt(0) == chr)
	{
		str = str.slice(1);
		str = TrimStart(str,chr);
	}
	return str;
}
//去掉字串右边的字符
function TrimEnd(str,chr)
{
	var iLength;

	iLength = str.length;
	if (str.charAt(iLength - 1) == chr)
	{
	//如果字串右边第一个字符为空格
	str = str.slice(0, iLength - 1);
	str = TrimEnd(str,chr);
	}
	return str;
}
//-------------
//去掉字串左边的空格
function lTrim(str)
{
	if (str.charAt(0) == " ")
	{
	//如果字串左边第一个字符为空格
	str = str.slice(1);//将空格从字串中去掉
	//这一句也可改成 str = str.substring(1, str.length);
	str = lTrim(str); //递归调用
	}
	return str;
}

//去掉字串右边的空格
function rTrim(str)
{
	var iLength;

	iLength = str.length;
	if (str.charAt(iLength - 1) == " ")
	{
	//如果字串右边第一个字符为空格
	str = str.slice(0, iLength - 1);//将空格从字串中去掉
	//这一句也可改成 str = str.substring(0, iLength - 1);
	str = rTrim(str); //递归调用
	}
	return str;
}

//去掉字串两边的空格
function trim(str)
{
	return lTrim(rTrim(str));
}


 //---------------------------
 //功能：获得字符串的长度
 //返回值：字符串长度
 //---------------------------
 function GetStringLen(str)
 {
    return str.replace(/[^\x00-\xff]/g,"xx").length;
//	var j=0 ;
//	for(var i=0 ;i<str.length ; i++)
//	{
//		var temp = escape(str.charAt(i)).length ;
//		if(temp>3)
//			j = j+2 ;
//		else
//			j = j+1 ;
//	}
//	
//	return j ;
 }
 
 function checkStrLen(str,len)
 {
	var j= GetStringLen(str) ;
	if(j<=parseInt(len))
		return true ;
	else
		return false ;
		
 }
 
  //----------------------------
  //功能：用户名输入 数字与字符混合
  //参数：str：输入值
  //返回值：true:正确 false:错误
  //作者：胡世永 2004-6-3
  //-----------------------------
function checkPassword(str)
{
	if(str=="") return true ;
	//匹配字符[0-9]
	/*var re	= new RegExp("([0-9]{1,})") ;
	var r	= str.search(re) ;
	if(r==-1) return false;

	//匹配字符[A-Z][a-z]
	re 	= new RegExp("([A-z]{1,})") ;
	r	= str.search(re) ;
	if(r==-1) return false ;*/
	
	return true ;
}	 
  //----------------------------
  //功能：金额输入
  //参数：str：输入值
  //返回值：true:正确 false:错误
  //作者：胡世永 2003-8-19
  //-----------------------------
function checkMoney(str)
{
	if(str=="") return true ;
	var re	= new RegExp("^(-?[0-9]{1,})(.[0-9]{1,2})?$") ;
	var r	= str.match(re) ;
	return r ;
}	

  //----------------------------
  //功能：浮点数输入
  //参数：str：输入值
  //返回值：true:正确 false:错误
  //作者：胡世永 2003-8-26
  //-----------------------------
function checkFloat(str){
	if(str=="") return true ;
	var Reg=new RegExp("^(-?[0-9]{1,}).?[0-9]{0,}$", "g");
	return str.match(Reg);
}
  //----------------------------
  //功能：考试成绩输入
  //参数：str：输入值 num:最长整数位数
  //返回值：true:正确 false:错误
  //作者：胡世永 2003-8-19
  //-----------------------------
function checkScore(str,num)
{
	if(str=="") return true ;
	if(checkFloat(str)==false) return false ;
	var IntLen = str.indexOf(".") ;
	
	var strLen = str.length ;
	if(IntLen>=parseInt(num)) return false ;
	if(IntLen<=0) return true ;
	if(strLen-IntLen>2) return false ;
	return true ;
}


function checkUrlEnd(v)
{
 if(v=="") return true ;
 
 if (right(v,4)!=".com" && right(v,4)!=".net" && right(v,4)!=".org" && right(v,3)!=".cn")
 {
   return false;
 }
 else
 {
   return true;
 };

}

function checkUrlBegin(v)
{
 if(v=="") return true ;
 
 if (left(v,7)!="http://" || left(v,4)!="www.")
 {
   return true;
 }
 else
 {
   return false;
 };

}
	
	
//----------------------------
//功能：数字输入---按键检查是否符合
//参数：str:控件,index:控件的TabIndex:调用可以用onKeypress="checkNumber(this,this.TabIndex)"
//返回值：控制事件，按键非法事件操作无效
//作者：叶勇、张立彬、王晓亮 
//-----------------------------	
function checkNumber(str,index)
{
	if(str=="") 
	{ 
		event.returnValue=true ;
		return true ;
	}	
	if(event.keyCode>57||event.keyCode<48)
		event.returnValue=false;		
}


//----------------------------
//功能：判断是否为整数
//参数：num:字符串
//返回值：false/true
//作者：叶勇、张立彬、王晓亮 
//-----------------------------	

function CheckInteger(num) {
	
	if(num=="") return true ;

    if (num.charAt(0)=='-') 
    num=num.substring(1,num.length-1); 
    temp=isNaN(num);
    if (temp==true) {
       return false;
    }
    else {
       if (num.indexOf('.')!=-1) {
          return false;
       }
       else {
          return true;
       }}
}

//判断日期是否为2007-12-05 13:24:2这种格式,guowj
function CheckDateTime(datestr)
{
	if(datestr=="") return true;
	var timestr = "";
	if(datestr.indexOf(' ')!=-1)
	{
		timestr = datestr.substring(datestr.indexOf(' ')+1,datestr.length);
		datestr = datestr.substring(0,datestr.indexOf(' '));
	}
	if(datestr.indexOf('-')!=-1)
	{
		for(i=0;i<3;i++)
		{
			if (i==0){year=datestr.substring(0,datestr.indexOf('-'));if(year.length>4){return false;}}
			if (i==1){month=datestr.substring(0,datestr.indexOf('-'));if((month.length>2) || (month>12)){return false;}}
			if (i==2){day=datestr.substring(0,datestr.length);if((day.length>2) || (day>31)){return false;}}
			datestr = datestr.substring(datestr.indexOf('-') + 1,datestr.length);
		}
		if(CheckMultiDate(year,month,day))
		{
			if(timestr=="") return true;
			if(timestr.indexOf(':')!=-1)
			{
				for(i=0;i<3;i++)
				{
					if (i==0){hours=timestr.substring(0,timestr.indexOf(':'));if(hours.length>2){return false;}}
					if (i==1){minutes=timestr.substring(0,timestr.indexOf(':'));if((minutes.length>2) || (minutes>59)){return false;}}
					if (i==2){seconds=timestr.substring(0,timestr.length);if((seconds.length>2) || (seconds>59)){return false;}}
					timestr = timestr.substring(timestr.indexOf(':') + 1,timestr.length);
				}
				return CheckMultiTime(hours,minutes,seconds);
			}
		}
	}
	return false;
}

function CheckMultiTime(hours,minutes,seconds)
{
	if(!CheckInteger(hours)){
		return false
	}
	if(!CheckInteger(minutes)){
		return false
	}
	if(!CheckInteger(seconds)){
		return false
	}
	hours=parseInt(hours,10)
	minutes=parseInt(minutes,10)
	seconds=parseInt(seconds,10)

	if (isNaN(hours)){
        return false
    }else
	if (isNaN(minutes)){
        return false
    }else
	if (isNaN(seconds)){
        return false
    }
    if((hours < 0) || (hours > 23))
    {
		return false;
    }
    else if((minutes<0) || (minutes>59) || (seconds<0) || (seconds>59))
    {
		return false;
	}
    return true;
}

//功能：返回日期是否合法（指定分割符）
//参数：datestr:日期字符串;type:分割符
//返回值：false:错;日期:正确
//作者：张立彬、叶勇、王晓亮 
//-----------------------------	
function CheckDate(datestr,type) {

	var changer = "yes";
	if(datestr=="") return true ;
	if(type==1)
	{
		chars = "/";
		changer = "no";
	}
	else if(type==2)
	{
		chars = "-";
		changer = "no";
	}
	else if(type==3)
	{
		chars = ".";
		changer = "no";
	}
	else if(type==4)
	{
			chars = "/";
			changer = "yes";
	}
	else if(type==5)
	{
		chars = "-";
		changer = "no";
	}
	else if(type==6)
	{
		chars = ".";
		changer = "yes";
	}		
	else
	{
		return false;
	}
	if(datestr.indexOf(chars)!=-1)
	{
		for(i=0;i<3;i++)
		{
			if (i==0){year=datestr.substring(0,datestr.indexOf(chars));if(year.length>4){return false;}}
			if (i==1){month=datestr.substring(0,datestr.indexOf(chars));if((month.length>2) || (month>12)){return false;}}
			if (i==2){day=datestr.substring(0,datestr.length);if((day.length>2) || (day>31)){return false;}}
			datestr = datestr.substring(datestr.indexOf(chars) + 1,datestr.length);
		}
		if(changer=="no")
		{
			return CheckMultiDate(year,month,day);
		}
		else
		{
			return CheckMultiDate(day,year,month);
		}
	}
	return false;
}	




function left(v,len)
{
 if (v.length<len) return "";
  return v.substring(0,len);

}

function right(v,len)
{
  if (v.length<len) return "";
  return v.substring(v.length-len,v.length);
}

//----------------------------
//功能：返回年月日是否合法
//参数：year:年字符串;month:月字符串;day:日字符串
//返回值：false:错;true:正确
//作者：张立彬、叶勇、王晓亮 
//-----------------------------	
function CheckMultiDate(year,month,day) {
	if(!CheckInteger(year)){
		return false
	}
	if(!CheckInteger(month)){
		return false
	}
	if(!CheckInteger(day)){
		return false
	}
	year=parseInt(year,10)
	month=parseInt(month,10)
	day=parseInt(day,10)

	if (isNaN(year)){
        return false
    }else
	if (isNaN(month)){
        return false
    }else
	if (isNaN(day)){
        return false
    }

   if ((year < 1800) || (year > 9999)){
                return false
        }
   else {
	if (month < 1 || month > 12){
                return false
        }
        else {
                if ((month==4)||(month==6)||(month==9)||(month==11)){
                        if (day <1 || day > 30){
                                return false
                        }
                }
                else if (month==2){
                        if ((year%400==0) || ((year%4==0) && (year%100!=0))){
                                if (day <1 || day > 29){
                                        return false
                                }
                        }
                        else if (day < 1 || day > 28){
                                return false

                        }
                }
                else if (day <1 || day >31){
                        return false
                }
        }
  }

            return true;
}

//----------------------------
//功能：检查页面是否有数据
//参数：控件
//作者：郭文娟
//-----------------------------	
function checkData()
{
	var Ctrls=document.forms[0].elements;
	var hasdata=0;
	for(var i=0;i<Ctrls.length;i++)
	{
		if(Ctrls[i].type=="checkbox")
		{
			hasdata=1;
			break;
		}
	}
	if(hasdata==0)
	{
		alert("没有数据");
		return false;
	}
	else
		return true;
}

//----------------------------
//功能：检查调查表页面调查问题中英文选项数量是否相等
//参数：Form名称
//返回值：控制事件，onSubmit
//作者：郭文娟
//-----------------------------	
function check_ans(thisform)
{
	var i=0;
	var j=0;
	var result = true;
	for (i=0;i<thisform.elements.length;i++)
	{
		if (thisform.elements[i].style.NeedCheck == "True")
		{
			if(thisform.elements[i].style.NotNull == "True")
			{
				if(trim(thisform.elements[i].value) == "")
				{
					thisform.elements[i].focus();
					alert(thisform.elements[i].style.des + "不能为空");
					result= false;
					break;
				}
			}
			else
			{
				if(thisform.elements[i].Orders != "")
				{
					if(thisform.elements[i].sign == "1")
					{
						if(trim(thisform.elements[i].value) == "" && trim(thisform.elements[i+1].value) == "")
						{
							thisform.elements[i].focus();
							alert("第" + thisform.elements[i].Orders + "题"+thisform.elements[i].style.des + "不能为空");
							result=false;
							break;
						}
						if(trim(thisform.elements[i].value) == "" && trim(thisform.elements[i+1].value) != "")
						{
							if(!confirm("第"+thisform.elements[i].Orders+"题" + thisform.elements[i].style.des + "为空,确定继续吗？"))
							{
								thisform.elements[i].focus();
								result = false;
								break;
							}
						}
						if(trim(thisform.elements[i].value) != "" && trim(thisform.elements[i+1].value) != "")
						{
							if(trim(thisform.elements[i].value).replace("；",";").split(";").length != trim(thisform.elements[i+1].value).replace("；",";").split(";").length)
							{
								if(!confirm("第"+thisform.elements[i].Orders+"题中英文数量不等,确定继续吗？"))
								{
									thisform.elements[i].focus();
									result = false;
									break;
								}
							}
						}
					}
					if(thisform.elements[i].sign == "3" && trim(thisform.elements[i].value) != "")
					{
						if(trim(thisform.elements[i-1].value)!="" && trim(thisform.elements[i].value).replace("；",";").split(";").length != trim(thisform.elements[i-1].value).replace("；",";").split(";").length)
						{
							thisform.elements[i].focus();
							alert("第" + thisform.elements[i].Orders + "题中文"+thisform.elements[i].style.des + "与可选答案数目不等");
							result=false;
							break;
						}
						if(trim(thisform.elements[i+2].value)!="" && trim(thisform.elements[i].value).replace("；",";").split(";").length != trim(thisform.elements[i+2].value).replace("；",";").split(";").length)
						{
							thisform.elements[i].focus();
							alert("第" + thisform.elements[i].Orders + "题英文"+thisform.elements[i].style.des + "与可选答案数目不等");
							result=false;
							break;
						}
					}
				}
			}
		}
	}
	return result;
}


//验证是否能输入汉字
function isUnicodeTrue(Unicode)
{
	if(Unicode.match(/[^ -~]/))
	{
		return true;
	}
	else
	{
		return false;
	}
	
}

//----------------------------
//功能：检查观众增加Form信息完整性
//-----------------------------	
function check_VisName()
{
	var result = true;
	var obj1 = document.getElementById('VisitorName');
	var obj2 = document.getElementById('FirstEName');
	var obj3 = document.getElementById('LastEName');
	if(obj1 != null && obj2 != null && obj3 != null)
	{
		if(trim(obj1.value).length <2 && (trim(obj2.value).length + trim(obj3.value).length)<2)
		{
			result = false;
			alert('请写全姓名');
		}
	}
	return result;
}

function check_Unit()
{
	var result = true;
	var obj1 = document.getElementById('UnitName');
	var obj2 = document.getElementById('UnitEName');
	if(obj1 != null && obj2 != null)
	{
		if(trim(obj1.value).length == 0 && trim(obj2.value).length ==0)
		{
			result = false;
			alert('请填写所在公司名称');
		}
	}
	return result;
}

//验证时间格式
function isTime(Time)
{
	var Reg = new RegExp("^((0?|1)\\d|2[0-3]):(0?|[1-5])\\d(:(0?|[1-5])\\d(\\.\\d{3})?)?$$");
	return Reg.test(Time);
}

function check_Contact()
{
	var result = true;
	var objArray = new Array('Address','EAddress','Tel1','Tel2','Tel3','Email','Mobile');
	var obj;
	for(var i=0;i<objArray.length;i++)
	{
		obj = document.getElementById(objArray[i]);
		if(obj != null)
		{
			result = false;
			if(trim(obj.value).replace('*','') != "")
			{
				result = true;
				break;
			}
		}
	}
	if(!result) alert('请填写联系信息');
	return result;
}



//----------------------------
//功能：检查Form那的控件的合法性
//参数：Form名称
//返回值：控制事件，onSubmit
//作者：张立彬 郭文娟
//-----------------------------	
function check_form(thisform)
{
	var i=0;
	var result = true;
	for (i=0;i<thisform.elements.length;i++)
	{
		if (thisform.elements[i].style.NeedCheck == "True")
		{
			result = check(thisform.elements[i]);
			if (!result)
			{        
				break;
			}  
		}
	}
	return result;
}

//----------------------------
//功能：检查某控件的输入是否合法
//参数：控件
//返回值：控制事件，onSubmit
//作者：张立彬
//-----------------------------	
function check(v)
{
	var eValue=trim(v.value);
	if(eValue == "*")
	{
		eValue = "";
	}
	if (v.style.NotNull=="True")
	{
		if (eValue == "")
		{	
			v.focus();
			alert(v.style.des + "不能为空");
			return false;
		};          
	}
	   
	//如果为空，返回True 
	if(eValue == "") return true ;
   
	if(v.style.MaxLength>0)
	{
		if(!checkStrLen(v.value,v.style.MaxLength))
		{
			v.focus();
			alert(v.style.des + "长度不能大于【" + v.style.MaxLength+ "】"); 
			return false;
		}
	} 
   
	if (v.style.FixedLength>0)
	{
		if (v.value.length!=v.style.FixedLength)
		{	
			v.focus();
			alert(v.style.des + "长度非法【" + v.style.FixedLength+ "】"); 
			return false;
		}        
	}
   
	if (v.style.isEmail=="True")
	{
		if ((v.value.indexOf("@")<1) || (v.value.indexOf("@")>v.value.length-6) || (v.value.indexOf(".")<1) )
		{
      		v.focus();
			alert(v.style.des + "输入的内容不是合法电子邮件"); 
			return false;      
		}   
	}
   
	if(v.style.isFix == "True")
	{
		if(v.value.indexOf(".")<1)
		{
			v.focus();
			alert(v.style.des + "Mail后缀不正确");
			return false;
		}
	}
	var reg = /\D/g;
	if(v.style.isTel=="True")
	{
		if(v.value.indexOf("-")<1)
		{
			v.focus();
			alert(v.style.des + "格式不正确");
			return false;
		}
		//电话只能含数字和-
		var Arr = v.value.split('-');
		for(var i=0;i<Arr.length;i++)
		{
			if(reg.test(Arr[i]))
			{
				alert(v.style.des+"格式不正确");
				return false;
			}
		}
	}
	if(v.style.isTel2=="True")   //Tel2或传真2，允许里面有“\”或“/”,不限制一定要有“-”
	{
	    var vTel=v.value;
		vTel=vTel.replace(/\\/g,'-');
	    vTel=vTel.replace(/\//g,'-');
		//电话只能含数字和-
		var Arr = vTel.split('-');
		for(var i=0;i<Arr.length;i++)
		{
			if(reg.test(Arr[i]))
			{
				alert(v.style.des+"格式不正确");
				return false;
			}
		}
	}
	if (v.style.isAge=="True")
	{
		    
		if (v.value<1 || v.value>100 )
		{
      		v.focus();
			alert(v.style.des + "输入非法"); 
			return false;      
		}
		
	}
	if (v.style.isDate=="True")
	{
		if (!CheckDate(v.value,2))
		{
      		v.focus();
			alert(v.style.des + "日期非法"); 
			return false;      
		}

	}
	if (v.style.isZip=="True")
	{
		if (!CheckInteger(v.value) || v.value.length!=6)
		{
      		v.focus();
			alert(v.style.des + "非法"); 
			return false;      
		}
	}
	if (v.style.isNum == "True")
	{
		if(reg.test(v.value))
		{
			v.focus();
			alert(v.style.des + "不是数字");
			return false;
		}
	}
	
    if (v.style.isUnicode=="True")
    {
        if (isUnicodeTrue(v.value))
        {
            alert(v.style.des + "非法，不能包含汉字！");
            v.focus();
            return false;
        }
    }
	
    if (v.style.isInt=="True")
	{
		if (!CheckInteger(v.value))
		{
      		v.focus();
			alert(v.style.des + "不是整数"); 
			return false;      
		}
	   
	}
    if (v.style.isFloat=="True")
	{
		if (!checkFloat(v.value))
		{
      		v.focus();
			alert(v.style.des + "不是浮点数"); 
			return false;      
		}
	   
	}
	if (v.style.isWWW=="True")
	{
		if (!checkUrlEnd(v.value) || !checkUrlBegin(v.value))       
		{
	         
	      
      		v.focus();
			alert(v.style.des + "输入的内容不是网址"); 
			return false;      
		}
	   
	} 
   
	if(v.style.isMoney=="True")
	{
		if (!checkMoney(v.value))       
		{
      		v.focus();
			alert(v.style.des + "输入的内容不是货币类型"); 
			return false;      
		}
		v.value = parseFloat(v.value) ;
	}
	if(v.style.isScore!=""&&v.style.isScore!=null)
	{
		if (!checkScore(v.value,v.style.isScore))       
		{
      		v.focus();
			alert(v.style.des + "输入的内容不是合法的考试成绩"); 
			return false;      
		}
		v.value = parseFloat(v.value) ;
	}
   
	if(v.style.isPassword!=""&&v.style.isPassword!=null)
	{
		if (!checkPassword(v.value))   
		{
      		v.focus();
			alert(v.style.des + "输入的内容必须是字符或、数字或者数字和字母组合"); 
			return false;      
		}
	}
	if(v.style.isTime =="True")
		{
			if(!isTime(v.value))
			{
				alert(v.style.des + "非法。例如：15:20");
				v.focus();
				return false;
			}
		}
	return true;
}
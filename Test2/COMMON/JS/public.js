//----------------------------------------------------------------------------------------
//函数及功能说明（按照函数名称排序）
//	CheckAll(Ctrls,obj)--------全选按钮
//	CheckKeyCode(obj,keyCodes)------检验键盘输入是否是有效数字，如果不是则不准输入
//	GetDateFromStrInput(strInput)---------把普通格式的年月日变换成Date类型数据
//	getFrameFeature(FrameName,FeatureName,FrameClass)---读取FRAME的属性（比如FRAME的宽、高等值）
//	getScreenFeature(FeatureName)--------读取当前屏幕的属性（比如分辨率的宽、高等值）
//	GetStandardFormatStringFromDate(strInput)--------把Date类型数据变换成普通格式的年月日
//	fPopUpDlg(endtarget,object)--------弹出窗口对话窗口，返回值存放在object里面
//	fPopUpDlgDefine(endtarget,Width,Height)-------弹出窗口对话窗口，可自定义窗口大小，但不返回值
//  fPopUpDlgReturn(endtarget,Width,Height)--------弹出窗口对话窗口，可自定义窗口大小，返回值
//	fWindowOpenDefine(endtarget,Width,Height,Type)-----弹出窗口
//	SetFeature(Width,Height)--------设置弹出窗口参数（对话框）
//	SetFeatureNormal(Width,Height)-----设置弹出窗口参数（对话框）
//  setFrameFeature(FrameName,FeatureName,FrameClass,FeatureValue)------------设置Frame的属性
//	SetWinFeature(Width,Height)-----设置弹出窗口参数（非对话框）	
//	SetWinFeatureNormal(Width,Height)---设置弹出窗口参数（非对话框）	
//	ShowAreaInfoFinder(strFormObject,strFormObject1,strFormObject2)-------展会地域选择器
//	ShowBoothFinder(strFormObject,strFormObject1,strFormObject2,strFormObject3,strFormObject4,strFormObject5)----展台多选器(返回六个参数）
//	ShowCommonFinder(strFormObject,strFormObject1,strFinderName,strCommonID)------通用选择器
//	ShowCommonFinder(strFormObject,strFormObject1,strFinderName,strCommonID,PostBack)-----通用选择器
//	ShowCommonFinderByLevel(strFormObject,strFormObject1,strFinderName,strEntry)-----通用分层选择器
//	ShowCommonFinderThree(strFormObject,strFormObject1,strFormObject2,strFinderName)----通用选择器(返回三个参数）
//	ShowCommonMultiFinder(strFormObject,strFormObject1,strFinderName,strCommonID)-------通用选择器（多选器）
//	ShowCommonMultiFinderThree(strFormObject,strFormObject1,strFormObject2,strFinderName)-----通用多选器(返回三个参数）
//	ShowExhiAlertTime(strFormObject,strFormObject1,strFormObject2,strExhiCode)-----展会初始化时获得提醒时间(返回三个参数）
//	ShowServiceFinder(strFormObject,strFormObject1,strFormObject2,strFormObject3,strFormObject4,strFormObject5,strFormObject6,strFormObject7)----服务多选器(返回七个参数）
//----------------------------------------------------------------------------------------

//系统根目录
//var vRoot = "/Visitor";
var vRoot = "/EfServiceCenter"  ;


function GetPath(v)
{
	alert(vRoot+"/"+v);
	return vRoot+"/"+v;
}

  //----------------------------
  //功能：弹出窗口
  //参数：endtarget:目标页面 Object:传入参数
  //返回值：Object
  //作者：胡世永 
  //-----------------------------
function fPopUpDlg(endtarget,object){
    var vReturn = window.showModalDialog(endtarget,'',SetFeature(400,310));
    if(typeof(vReturn)!="undefined") object.value=vReturn ;	
    
}

  //----------------------------
  //功能：设置弹出窗口参数
  //参数：Width：页面宽度 Height：页面高度
  //返回值：string
  //作者：胡世永 
  //-----------------------------
function SetFeature(Width,Height){
	var iHeight=Height;
	var iWidth =Width;
	
	var obH=0;
	
	var iLeft=148 ;
	var iTop =24;
	
	//var iLeft=event.screenX - event.offsetX - 148 ;
	//var iTop =event.screenY - event.offsetY + 24;
	
	var Att="dialogHeight: " + iHeight + "px;"+"dialogwidth:" + iWidth + "px;dialogleft:" + iLeft + "px;dialogtop:" + iTop +"px;status:0;help:0;";
	 
	return Att;
}

  //----------------------------
  //功能：设置弹出窗口参数
  //参数：Width：页面宽度 Height：页面高度
  //返回值：string
  //作者：胡世永 
  //-----------------------------
function SetFeatureNormal(Width,Height){
	var iHeight=Height;
	var iWidth =Width;
	
	var obH=0;
	
	var iLeft=148 ;
	var iTop =24;
	
	//var iLeft=event.screenX - event.offsetX - 148 ;
	//var iTop =event.screenY - event.offsetY + 24;
	
	var Att="dialogHeight: " + iHeight + "px;"+"dialogwidth:" + iWidth + "px;dialogleft:" + iLeft + "px;dialogtop:" + iTop +"px;";
	 
	return Att;
}

  //----------------------------
  //功能：弹出窗口
  //参数：endtarget:目标页面 Width：页面宽度 Height：页面高度
  //返回值：No
  //作者：胡世永 
  //-----------------------------
function fPopUpDlgDefine(endtarget,Width,Height)
{
	var vReturn = window.showModalDialog(endtarget,'',SetFeature(Width,Height));
}

  //----------------------------
  //功能：弹出窗口
  //参数：endtarget:目标页面 Width：页面宽度 Height：页面高度
  //返回值：返回值
  //作者：胡世永 
  //-----------------------------
function fPopUpDlgReturn(endtarget,Width,Height)
{
	var vReturn = window.showModalDialog(endtarget,'',SetFeature(Width,Height));
	return vReturn;
}

  //----------------------------
  //功能：设置弹出窗口参数
  //参数：Width：页面宽度 Height：页面高度
  //返回值：string
  //作者：胡世永 
  //-----------------------------
function SetWinFeature(Width,Height){
	var iHeight=Height;
	var iWidth =Width;
	
	var obH=0;
	
	var iLeft=148 ;
	var iTop =24;
	
	var Att="Height= " + iHeight + "px,"+"width=" + iWidth + "px,left=" + iLeft + "px,top=" + iTop +"px,toolbar=no, menubar=no, scrollbars=auto, resizable=no,location=no, status=no";
	
	return Att;
}

  //----------------------------
  //功能：设置弹出窗口参数
  //参数：Width：页面宽度 Height：页面高度
  //返回值：string
  //作者：胡世永 
  //-----------------------------
function SetWinFeatureNormal(Width,Height){
	var iHeight=Height;
	var iWidth =Width;
	
	var obH=0;
	
	var iLeft=148 ;
	var iTop =24;
	
	var Att="Height= " + iHeight + "px,"+"width=" + iWidth + "px,left=" + iLeft + "px,top=" + iTop +"px,scrollbars=yes, resizable=yes";
	
	return Att;
}

  //----------------------------
  //功能：弹出窗口
  //参数：endtarget:目标页面 Width：页面宽度 Height：页面高度
  //返回值：No
  //作者：胡世永 
  //-----------------------------
function fWindowOpenDefine(endtarget,Width,Height,Type)
{

	if(Type=="1") 
	{
		var Att = SetWinFeature(Width,Height) ;
		window.open(endtarget,"",Att);
	}
	else if(Type=="2") 
	{
	
		var Att = SetWinFeatureNormal(Width,Height) ;
		window.open(endtarget,"",Att);
	}
	else
	{
		var Att = SetWinFeature(Width,Height) ;
		window.open(endtarget,"",Att);
	}
		

}

  //----------------------------
  //功能：把普通格式的年月日变换成Date类型数据
  //参数：strInput 标准格式的2003-10-20
  //返回值：Date
  //作者：hushiyong 
  //-----------------------------
function GetDateFromStrInput(strInput)
{
	var MyArray ;
	MyArray = strInput.split("-") ;
	alert(MyArray[1]);
	if(MyArray.length!=3)
		return new Date("1-1-1900") ;
	else
		return new Date(MyArray[1] + "-" + MyArray[2] + "-" + MyArray[0]) ;
}
  //----------------------------
  //功能：把Date类型数据变换成普通格式的年月日
  //参数：strInput Date
  //返回值：标准格式的2003-10-20
  //作者：hushiyong 
  //-----------------------------
function GetStandardFormatStringFromDate(strInput)
{
	return strInput.getYear() + "-" + (strInput.getMonth()+1) + "-" + strInput.getDate() ;
}


//----------------------------
  //功能：通用选择器
  //参数：strFormObject,strFormObject1:控件,strFinderName:选择器名
  //返回值：传递参数
  //作者：彭近兵 
  //-----------------------------
function ShowCommonFinder(strFormObject,strFormObject1,strFinderName,strCommonID)
{
	var strGotoPath = '/COMMON/Finder/CommonFinder/CommonFinderbrowse.aspx' ;
	if(window.screen.width == "800")
		window.open( vRoot  +  strGotoPath + '?FormObject=' + strFormObject + '&FormObject1=' + strFormObject1 + '&FinderName=' + strFinderName+ '&CommonID=' + strCommonID  ,strFinderName,"width=500,height=350,status=no,resizable=yes,scrollbars=yes,top=0,left=0") ;
	else
		window.open( vRoot  +  strGotoPath + '?FormObject=' + strFormObject + '&FormObject1=' + strFormObject1 + '&FinderName=' + strFinderName+ '&CommonID=' + strCommonID  ,strFinderName,"width=550,height=450,status=no,resizable=yes,scrollbars=yes,top=0,left=0") ;
	window.opener	= self ;	
}

function ShowCommonFinderleft(strFormObject,strFormObject1,strFinderName,strCommonID)
{
	var strGotoPath = '/COMMON/Finder/CommonFinder/CommonFinderbrowse.aspx' ;
	if(window.screen.width == "800")
		window.open( vRoot  +  strGotoPath + '?FormObject=' + strFormObject + '&FormObject1=' + strFormObject1 + '&FinderName=' + strFinderName+ '&CommonID=' + strCommonID  ,strFinderName,"width=500,height=350,status=no,resizable=yes,scrollbars=yes,top=0,left="+(window.screen.width-550)) ;
	else
		window.open( vRoot  +  strGotoPath + '?FormObject=' + strFormObject + '&FormObject1=' + strFormObject1 + '&FinderName=' + strFinderName+ '&CommonID=' + strCommonID  ,strFinderName,"width=550,height=450,status=no,resizable=yes,scrollbars=yes,top=0,left="+(window.screen.width-600)) ;
	window.opener	= self ;	
}
//----------------------------
  //功能：通用选择器
  //参数：strFormObject,strFormObject1:控件,strFinderName:选择器名
  //返回值：传递参数
  //作者：彭近兵 
  //-----------------------------
function ShowCommonFinder(strFormObject,strFormObject1,strFinderName,strCommonID,PostBack)
{
	var strGotoPath = '/COMMON/Finder/CommonFinder/CommonFinderbrowse.aspx';
	if(window.screen.width == "800")
		window.open( vRoot  +  strGotoPath + '?FormObject=' + strFormObject + '&FormObject1=' + strFormObject1 + '&FinderName=' + strFinderName+ '&CommonID=' + strCommonID + '&PostBack=' + PostBack   ,strFinderName,"width=500,height=350,status=no,resizable=yes,scrollbars=yes,top=0,left=0") ;
	else	
		window.open( vRoot  +  strGotoPath + '?FormObject=' + strFormObject + '&FormObject1=' + strFormObject1 + '&FinderName=' + strFinderName+ '&CommonID=' + strCommonID + '&PostBack=' + PostBack   ,strFinderName,"width=550,height=450,status=no,resizable=yes,scrollbars=yes,top=0,left=0") ;
	window.opener	= self ;
}

function ShowCommonFinderleft(strFormObject,strFormObject1,strFinderName,strCommonID,PostBack)
{
	var strGotoPath = '/COMMON/Finder/CommonFinder/CommonFinderbrowse.aspx';
	if(window.screen.width == "800")
		window.open( vRoot  +  strGotoPath + '?FormObject=' + strFormObject + '&FormObject1=' + strFormObject1 + '&FinderName=' + strFinderName+ '&CommonID=' + strCommonID + '&PostBack=' + PostBack   ,strFinderName,"width=500,height=350,status=no,resizable=yes,scrollbars=yes,top=0,left="+(window.screen.width-550)) ;
	else	
		window.open( vRoot  +  strGotoPath + '?FormObject=' + strFormObject + '&FormObject1=' + strFormObject1 + '&FinderName=' + strFinderName+ '&CommonID=' + strCommonID + '&PostBack=' + PostBack   ,strFinderName,"width=550,height=450,status=no,resizable=yes,scrollbars=yes,top=0,left="+(window.screen.width-600)) ;
	window.opener	= self ;
}
//----------------------------
  //功能：通用分层选择器
  //参数：strFormObject,strFormObject1:控件,strFinderName:选择器名,strEntry:是否允许进入下一级,参数值为“Yes”或“No”，大小写敏感。
  //返回值：传递参数
  //作者：彭近兵 
  //-----------------------------
function ShowCommonFinderByLevel(strFormObject,strFormObject1,strFinderName,strEntry)
{
	var strGotoPath = '/COMMON/Finder/CommonFinder/CommonFinderByLevel.aspx' ;
	if(window.screen.width == "800")
		window.open( vRoot  +  strGotoPath + '?FormObject=' + strFormObject + '&FormObject1=' + strFormObject1 + '&FinderName=' + strFinderName+ '&Entry=' + strEntry  ,"","width=500,height=350,status=no,resizable=yes,scrollbars=yes,top=50,left=100") ;
	else	
		window.open( vRoot  +  strGotoPath + '?FormObject=' + strFormObject + '&FormObject1=' + strFormObject1 + '&FinderName=' + strFinderName+ '&Entry=' + strEntry  ,"","width=550,height=450,status=no,resizable=yes,scrollbars=yes,top=200,left=200") ;
	window.opener	= self ;	
}

//----------------------------
  //功能：通用选择器（多选器）
  //参数：strFormObject,strFormObject1:控件,strFinderName:选择器名
  //返回值：传递参数
  //作者：彭近兵 
  //-----------------------------
function ShowCommonMultiFinder(strFormObject,strFormObject1,strFinderName,strCommonID)
{
	var strGotoPath = '/COMMON/Finder/CommonMultiFinder/CommonMultiFinder.aspx' ;
	if(window.screen.width == "800")
		window.open( vRoot  +  strGotoPath + '?FormObject=' + strFormObject + '&FormObject1=' + strFormObject1 + '&FinderName=' + strFinderName+ '&CommonID=' + strCommonID  ,"","width=500,height=350,status=no,resizable=yes,scrollbars=yes,top=50,left=100") ;
	else	
		window.open( vRoot  +  strGotoPath + '?FormObject=' + strFormObject + '&FormObject1=' + strFormObject1 + '&FinderName=' + strFinderName+ '&CommonID=' + strCommonID  ,"","width=550,height=450,status=no,resizable=yes,scrollbars=yes,top=200,left=200") ;
	window.opener	= self ;	
}
//----------------------------
  //功能：通用选择器(返回三个参数）
  //参数：strFormObject,strFormObject1,strFormObject2:控件,strFinderName:选择器名
  //返回值：传递参数
  //作者：彭近兵 
  //-----------------------------
function ShowCommonFinderThree(strFormObject,strFormObject1,strFormObject2,strFinderName)
{
	if(window.screen.width == "800")
		window.open(vRoot + '/COMMON/Finder/CommonFinderThree/CommonFinderThreebrowse.aspx?FormObject=' + strFormObject + '&FormObject1=' + strFormObject1+ '&FormObject2=' + strFormObject2 + '&FinderName=' + strFinderName  ,"","width=500,height=350,status=no,resizable=yes,scrollbars=yes,top=50,left=100") ;
	else	
		window.open(vRoot + '/COMMON/Finder/CommonFinderThree/CommonFinderThreebrowse.aspx?FormObject=' + strFormObject + '&FormObject1=' + strFormObject1+ '&FormObject2=' + strFormObject2 + '&FinderName=' + strFinderName  ,"","width=550,height=450,status=no,resizable=yes,scrollbars=yes,top=200,left=200") ;
		
	window.opener	= self ;	
}
//----------------------------
  //功能：通用多选器(返回三个参数）
  //参数：strFormObject,strFormObject1,strFormObject2:控件,strFinderName:选择器名
  //返回值：传递参数
  //作者：彭近兵 
  //-----------------------------
function ShowCommonMultiFinderThree(strFormObject,strFormObject1,strFormObject2,strFinderName)
{
	if(window.screen.width == "800")
		window.open(vRoot + '/COMMON/Finder/CommonFinderThree/CommonMultiFinderThree.aspx?FormObject=' + strFormObject + '&FormObject1=' + strFormObject1+ '&FormObject2=' + strFormObject2 + '&FinderName=' + strFinderName  ,"","width=500,height=350,status=no,resizable=yes,scrollbars=yes,top=50,left=100") ;
	else	
		window.open(vRoot + '/COMMON/Finder/CommonFinderThree/CommonMultiFinderThree.aspx?FormObject=' + strFormObject + '&FormObject1=' + strFormObject1+ '&FormObject2=' + strFormObject2 + '&FinderName=' + strFinderName  ,"","width=550,height=450,status=no,resizable=yes,scrollbars=yes,top=200,left=200") ;
	window.opener	= self ;	
}
//----------------------------
  //功能：展台多选器(返回六个参数）
  //参数：strFormObject(HallCode),strFormObject1(BoothCode),strFormObject2(BoothType),strFormObject3(Area)
  //返回值：传递参数
  //作者：陆世军 
  //-----------------------------
//function ShowBoothFinder(strFormObject,strFormObject1,strFormObject2,strFormObject3,strFormObject4,strFormObject5,strFormObject6,strHallCode)
//{	
//	if(window.screen.width == "800")
//		window.open(vRoot + '/COMMON/Finder/BoothFinder/BoothFinderBrowse.aspx?FormObject=' + strFormObject + '&FormObject1=' + strFormObject1+ '&FormObject2=' + strFormObject2 + '&FormObject3=' + strFormObject3+ '&FormObject4=' + strFormObject4+ '&FormObject5=' + strFormObject5 + '&FormObject6=' + strFormObject6  +  '&strHallCode=' + strHallCode  ,"","width=500,height=350,status=yes,resizable=yes,scrollbars=yes,top=50,left=100") ;
//	else
//		window.open(vRoot + '/COMMON/Finder/BoothFinder/BoothFinderBrowse.aspx?FormObject=' + strFormObject + '&FormObject1=' + strFormObject1+ '&FormObject2=' + strFormObject2 + '&FormObject3=' + strFormObject3+ '&FormObject4=' + strFormObject4+ '&FormObject5=' + strFormObject5 + '&FormObject6=' + strFormObject6  +  '&strHallCode=' + strHallCode  ,"","width=550,height=450,status=yes,resizable=yes,scrollbars=yes,top=200,left=200") ;
//	window.opener	= self ;	
//}

function ShowBoothFinder(strFormObject,strFormObject1,strFormObject2,strFormObject3,strFormObject4,strFormObject5,strFormObject6,strFormObject7,strHallCode)
{	
	if(window.screen.width == "800")
		window.open(vRoot + '/COMMON/Finder/BoothFinder/BoothFinderBrowse.aspx?FormObject=' + strFormObject + '&FormObject1=' + strFormObject1+ '&FormObject2=' + strFormObject2 + '&FormObject3=' + strFormObject3+ '&FormObject4=' + strFormObject4+ '&FormObject5=' + strFormObject5 + '&FormObject6=' + strFormObject6  + '&FormObject7=' + strFormObject7 + '&strHallCode=' + strHallCode  ,"","width=500,height=350,status=yes,resizable=yes,scrollbars=yes,top=50,left=100") ;
	else
		window.open(vRoot + '/COMMON/Finder/BoothFinder/BoothFinderBrowse.aspx?FormObject=' + strFormObject + '&FormObject1=' + strFormObject1+ '&FormObject2=' + strFormObject2 + '&FormObject3=' + strFormObject3+ '&FormObject4=' + strFormObject4+ '&FormObject5=' + strFormObject5 + '&FormObject6=' + strFormObject6  + '&FormObject7=' + strFormObject7 + '&strHallCode=' + strHallCode  ,"","width=550,height=450,status=yes,resizable=yes,scrollbars=yes,top=200,left=200") ;
	window.opener	= self ;	
}



//----------------------------
  //功能：服务多选器(返回七个参数）
  //参数：strFormObject(ServiceCode),strFormObject1(ServiceName),strFormObject2(ServiceType),strFormObject3(CompanyName),strFormObject3(Price),strFormObject3(ServiceUnit),strFormObject3(Actor)
  //返回值：传递参数
  //作者：陆世军 
  //-----------------------------
function ShowServiceFinder(strFormObject,strFormObject1,strFormObject2,strFormObject3,strFormObject4,strFormObject5,strFormObject6,strFormObject7,strFormObject8)
{
	if(window.screen.width == "800")
		window.open(vRoot + '/COMMON/Finder/ServiceFinder/ServiceFinderBrowse.aspx?FormObject=' + strFormObject + '&FormObject1=' + strFormObject1+ '&FormObject2=' + strFormObject2 + '&FormObject3=' + strFormObject3+ '&FormObject4=' + strFormObject4+ '&FormObject5=' + strFormObject5 + '&FormObject6=' + strFormObject6+ '&FormObject7=' + strFormObject7+ '&FormObject8=' + strFormObject8 ,"","width=500,height=350,status=no,resizable=yes,scrollbars=yes,top=50,left=100") ;	
	else	
		window.open(vRoot + '/COMMON/Finder/ServiceFinder/ServiceFinderBrowse.aspx?FormObject=' + strFormObject + '&FormObject1=' + strFormObject1+ '&FormObject2=' + strFormObject2 + '&FormObject3=' + strFormObject3+ '&FormObject4=' + strFormObject4+ '&FormObject5=' + strFormObject5 + '&FormObject6=' + strFormObject6+ '&FormObject7=' + strFormObject7+ '&FormObject8=' + strFormObject8 ,"","width=550,height=450,status=no,resizable=yes,scrollbars=yes,top=200,left=200") ;
	window.opener	= self ;	
}
//--------------------------------
//功能：检验键盘输入是否是有效数字，如果不是则不准输入
//参数：obj:要检验的文本框；keyCodes:event.keyCode
//作者：陆世军
//--------------------------------
function CheckKeyCode(obj,keyCodes)
{
	if(event.keyCode==110 || event.keyCode==190)
	{
		if(obj.value.indexOf('.')!=-1)
		{
			return false;
		}
		else
		{
			return true;
		}
	}
	else
	{
		var keyCodes=new Array(8,9,13,16,17,35,36,37,38,39,40,46,48,49,50,51,52,53,54,55,56,57,96,97,98,99,100,101,102,103,104,105);
		for(var i=0;i<keyCodes.length;i++)
		{
			if(event.keyCode==keyCodes[i])
			{
				return true;
			}
		}
		return false;
	}
}
//----------------------------
  //功能：展会初始化时获得提醒时间(返回三个参数）
  //参数：strFormObject,strFormObject1,strFormObject2:控件,strExhiCode:展会代码
  //返回值：传递参数
  //作者：张权 
  //-----------------------------
function ShowExhiAlertTime(strFormObject,strFormObject1,strFormObject2,strExhiCode)
{
	if(window.screen.width == "800")
		window.open(vRoot + '/Exhibition/Exhibit/ExhiAlertTimeChoose.aspx?FormObject=' + strFormObject + '&FormObject1=' + strFormObject1+ '&FormObject2=' + strFormObject2 + '&ExhiCode=' + strExhiCode  ,"","width=500,height=350,status=no,resizable=yes,scrollbars=yes,top=50,left=100") ;
	else	
		window.open(vRoot + '/Exhibition/Exhibit/ExhiAlertTimeChoose.aspx?FormObject=' + strFormObject + '&FormObject1=' + strFormObject1+ '&FormObject2=' + strFormObject2 + '&ExhiCode=' + strExhiCode  ,"","width=550,height=450,status=no,resizable=yes,scrollbars=yes,top=200,left=200") ;
	window.opener	= self ;	
}
//----------------------------
  //功能：展会地域选择器
  //参数：strFormObject,strFormObject1:控件
  //返回值：传递参数
  //作者：井浩 
  //-----------------------------
function ShowAreaInfoFinder(strFormObject,strFormObject1,strFormObject2)
{
	var strGotoPath = '/COMMON/Finder/CommonFinder/SecAreaInfoList.aspx' ;
	if(window.screen.width == "800")
		window.open( vRoot  +  strGotoPath + '?FormObject=' + strFormObject + '&FormObject1=' + strFormObject1,"","width=500,height=350,status=no,resizable=yes,scrollbars=yes,top=50,left=100") ;
	else
		window.open( vRoot  +  strGotoPath + '?FormObject=' + strFormObject + '&FormObject1=' + strFormObject1,"","width=550,height=450,status=no,resizable=yes,scrollbars=yes,top=200,left=200") ;
	window.opener	= self ;	
}

//----------------------------
  //功能：全选按钮
  //参数：Ctrls:要全选的CheckBox  Array ,obj:全选的CheckBox
  //返回值：无
  //作者：陆世军
  //-----------------------------
function CheckAll(Ctrls,obj)
{
	for(var i=0;i<Ctrls.length;i++)
	{
		if(document.all[Ctrls[i]].disabled==false)
		{
			try
			{
				if(document.all[obj].checked)
				{
					document.all[Ctrls[i]].checked=true;
				}
				else
				{
					document.all[Ctrls[i]].checked=false;
				}
			}
			catch(e){}
		}
	}
}

//-----------------
function setObjDisable(obj)
{
	obj.setAttribute("disabled","disabled");
}

//-----------------
function removeDisable(obj)
{
	obj.removeAttribute("disabled")
}

//---------------
//检查页面的checkbox是否选中数据
//参数:Ctrls:页面全部控件元素
//返回值:true或false
//---------------
function getFromCheckbox(Ctrls)
{
	var HaveData = false;
	for(var i=0;i<Ctrls.length;i++)
	{
		if(Ctrls[i].type=='checkbox')
		{
			if(Ctrls[i].checked)
			{
				HaveData = true;
				break;
			}
		}
	}
	return HaveData;
}

//-----------------
//全选按钮选中,触发button控件组可用,否则不可用
//参数:obj:全选的checkbox,objArray:button控件组
//返回值:无
//作者:guowenjuan
//-----------------
function DisabledArray(objValue,objArray)
{
	for(var i=0;i<objArray.length;i++)
	{
		if(objValue)
			removeDisable(document.getElementById(objArray[i]));
		else
			setObjDisable(document.getElementById(objArray[i]));
	}
}

//-----------------
//checkbox按钮有选中,触发button控件组可用
//参数:Ctrls:页面全部控件元素 ,objArray:受控的控件id数组
//返回值:无
//作者:guowenjuan
//-----------------
function ArrayDisabled(Ctrls,objArray)
{
	var f = getFromCheckbox(Ctrls);
	DisabledArray(f,objArray);
}

//-----------------
//全选按钮
//参数:Ctrls:页面全部控件元素 ,obj:全选的CheckBox
//返回值:无
//作者:guowenjuan
//-----------------
function CheckAlls(Ctrls,obj)
{
	for(var i=0;i<Ctrls.length;i++)
	{
		if(Ctrls[i].type == 'checkbox')
		{
			if(obj.checked)
			{
				obj.value = '1';
				Ctrls[i].checked = true;
			}
			else
			{
				obj.value = '0';
				Ctrls[i].checked = false;
			}
		}
	}
}

//用于替换页面在提交的调查问题内容中用于其它分隔作用的特殊字符guowenjuan
function FormatString(Answer)
{
	var Str = Answer;
	Str = Str.replace(':','：');
	Str = Str.replace(';','；');
	Str = Str.replace('|','｜');
	Str = Str.replace('(','（');
	Str = Str.replace(')','）');
	Str = Str.replace('%','');
	return Str;
}

//----------------------------
  //功能：读取当前屏幕的属性（比如分辨率的宽、高等值）
  //参数：FeatureName------属性名称字符串，比如width（宽度）,height（高度）
  //返回值：属性值
  //作者：郭宇春
  //-----------------------------
function getScreenFeature(FeatureName)
{
	var strFun="screen."+FeatureName;
	return eval(strFun);
}

//----------------------------
  //功能：读取FRAME的属性（比如FRAME的宽、高等值）
  //参数：
  //	FrameName------Frame对象的名称。
  //	FeatureName------属性名称字符串，比如width（宽度）,height（高度）
  //	FrameClass-------框架的级别。0：表示父调子；1：表示子调父(指在框架内的页面里使用本框架或其它框架的属性)
  //	举例（在一个Frame内部读取自己的信息window.parent.document.getElementById("FrameName")）
  //返回值：属性值
  //作者：郭宇春
  //-----------------------------
function getFrameFeature(FrameName,FeatureName,FrameClass)
{
	var strFun="window";	
	if(FrameClass>0) //子调父的情况，需要添加parent标识
	{
		for(i=0;i<FrameClass;i++)
		{
			strFun=strFun+".parent";
		}
	}	
	strFun=strFun+".document.getElementById(\""+FrameName+"\")."+FeatureName;	
	return eval(strFun);
}

//----------------------------
  //功能：设置FRAME的属性（比如FRAME的宽、高等值）
  //参数：
  //	FrameName------Frame对象的名称。
  //	FeatureName------属性名称字符串，比如width（宽度）,height（高度）
  //	FrameClass-------框架的级别。0：表示父调子；1：表示子调父(指在框架内的页面里使用本框架或其它框架的属性)
  //    FeatureValue-----设置框架的某个属性
  //	举例（在一个Frame内部读取自己的信息window.parent.document.getElementById("FrameName")）
  //返回值：属性值
  //作者：郭宇春
  //-----------------------------
function setFrameFeature(FrameName,FeatureName,FrameClass,FeatureValue)
{
	var strFun="window";	
	if(FrameClass>0) //子调父的情况，需要添加parent标识
	{
		for(i=0;i<FrameClass;i++)
		{
			strFun=strFun+".parent";
		}
	}	
	strFun=strFun+".document.getElementById(\""+FrameName+"\")."+FeatureName+"='"+FeatureValue+"'";	
	eval(strFun);
}


//----------------------------
  //功能：读取图片对象的原始高度和宽度
  //参数：
  //	picUrl------图片的Url。
  //	featureName------width或height
  //返回值：
  //作者：郭宇春
  //-----------------------------
function getImageActualSize(picUrl,featureName)
{
	//这是个很笨的办法，最好不用
	var divObj;
	var imgObj;
	//判断需要使用的div元素是否存在
	if(typeof(divHidden)!='object')
	{
		document.write("<div id='divHidden' ></div>");
	}
	divObj=divHidden;
	//判断隐藏的图形对象是否存在
	if(typeof(divObj.imgHidden)!='object')
	{
		imgObj=divObj.appendChild(document.createElement("<img id='imgHidden' src='"+picUrl+"'>"));		
	}
	else
	{
		imgObj=divObj.imgHidden;
	}
	imgObj.visibility="hidden";
	return eval("imgObj."+featureName);
}

//----------------------------
  //功能：判断一个页面元素是否存在
  //参数：
  //	elementName------元素名称
  //返回值：true/false
  //作者：郭宇春
  //-----------------------------
function isElementExist(elementName)
{
	var bResult=false;
	var cObj=eval("document.all(\""+elementName+"\")");

	if(cObj!=null)
	{
		bResult=true;
	}
	
	return bResult;
}

//读取一个Cookie的值
function getcookie(obj)
{
    var aa = new RegExp("；","ig");
    var bb = new RegExp("＝","ig");
    var cookieString = new String(document.cookie);
	var cookieHeader = "(" + obj.id + ")";
	var cookiefooter = "(/" + obj.id + ")";
	var beginPosition = cookieString.indexOf(cookieHeader);
	var endPosition = cookieString.indexOf(cookiefooter);
	if (beginPosition != -1)
	{
		return cookieString.substring(beginPosition + cookieHeader.length,endPosition).replace(aa,";").replace(bb,"=");
	}
	else
	{
		return "";
	}
}

//取下一个控件cookies
function getEnterControl(obj)
{
	var cookieString = new String(document.cookie);
	var cookieHeader = "(/" + obj.id + ")";
	var beginPosition = cookieString.indexOf(cookieHeader)+cookieHeader.length+1;
	var endPosition = cookieString.indexOf(")",beginPosition);
	if(endPosition != -1)
		return document.getElementById(cookieString.substring(beginPosition,endPosition));
	else//否则返回顺序的下一个
		return null;
}

function  getNextControl(Ctrls,obj)
{
	for(var i=0;i<Ctrls.length;i++)
	{
		if(Ctrls[i].Orders != null && parseInt(Ctrls[i].Orders) > parseInt(obj.Orders))
		{
			return Ctrls[i];
		}
	}
	return obj;
}

function getPreControl(Ctrls,obj)
{
	for(var i = Ctrls.length-1;i >= 0;i--)
	{
		if(Ctrls[i].Orders != null && parseInt(Ctrls[i].Orders) < parseInt(obj.Orders))
		{
			return Ctrls[i];
		}
	}
	return obj;
}

//从Cookie读取与对象id相同的cookie值，付给对象，并选中对象。
function DoFocus(obj)
{
    try
	{
		if(obj.value == null || obj.value == "")
		{
			var  objValue = getcookie(obj);
			if(obj.id == 'VisitorCard')
				obj.value = parseInt(objValue)+1;
			else
				obj.value = objValue;
			obj.select();
		}
	}
	catch(e){}
}

//按键后的页面响应。
//	1.按上箭头、左箭头（能够移至页面顺序的上一个控件）
//	2.按回车、下箭头、右箭头（能够移至页面顺序的下一个控件）
//	3.如果当前焦点对象为下拉框，按数字键可以选择选项
//	4.按shift+alt，能够获得与焦点对象同名的cookie值（当然cookie需要存在）
//	5.按下"回车"+Ctrl键、"回车"+alt键、空格+Ctrl键、"空格"+alt键中的一种，直接提交本页面
//	   条件：本页面的保存按钮为"BtnSave"，且存在__doPostBack函数
//	6.按ctrl+alt，页面所有控件能够获得同名的cookie值（当然cookie需要存在）
//==============================================================================================
//	Ctrls：页面控件对象集合
//	obj：当前焦点对象
function DoKeyDown(Ctrls,obj)
{
    //用数字键选择下拉框的值。obj为下拉框对象。
	if(obj.tagName.toLowerCase() == "select")
	{
		var index="";
		if((event.keyCode>=48 && event.keyCode<=57))
		{
			index = String.fromCharCode(event.keyCode)-1;
		}
		else if(event.keyCode>=97 && event.keyCode<=105)
		{
			index = String.fromCharCode(event.keyCode-48)-1;
		}
		try
		{
		    obj.options[index].selected = true;
		}
		catch(e){}
	}
	//同时按下shift+alt键，从cookie中读出obj的值
	if(event.shiftKey && event.altKey )
	{
		obj.value = getcookie(obj.id);
	}
	//按下"回车"+Ctrl键、"回车"+alt键、空格+Ctrl键、"空格"+alt键中的一种，直接提交本页面
	//条件：本页面的保存按钮为"BtnSave"，且存在__doPostBack函数
    if((event.keyCode==13 || event.keyCode==83)  &&  (event.ctrlKey || event.altKey))
    {
		try
      	{
         	if (check_form1(Form1))
         	{
         		__doPostBack('BtnSave','');
         	}
      	}
      	catch(e){}
      	return false;
    }
    //对页面控件集合进行的处理
	//同时按下ctrl+alt键，从cookie中给页面对象赋值
	if(event.ctrlKey && event.altKey)
	{
		for(var i=0;i<Ctrls.length;i++)
		{
			try
			{	
				var CtrlValue = getcookie(Ctrls[i].id);
				if(Ctrls[i].Orders != null && CtrlValue != "")
				{
					Ctrls[i].value = CtrlValue;
				}
			}
			catch(e){}
		}
	}
	//textarea的处理情况
	if(obj.tagName.toLowerCase() == "textarea" && obj.value != "")
	{
		//值不为空，不处理
		return;
	}
	else
	{
		var NextObj;
		if(event.keyCode == 13)
		{
			//跳到cookie的下一个控件
			NextObj = getEnterControl(obj);
			if(NextObj == null)
			{
				NextObj = getNextControl(Ctrls,obj);
			}
			NextObj.focus();
		}
		else if(event.keyCode == 38)//UP按顺序跳至上一个控件
		{
			getPreControl(Ctrls,obj).focus();
		}
		else if(event.keyCode == 40)//DOWN按顺序跳至下一个控件
		{
			getNextControl(Ctrls,obj).focus();
		}
		else if(event.keyCode == 37)//PRE
		{
			if(obj.value !="" && (obj.type == "text" || obj.tagName.toLowerCase() == "textarea"))
			{}
			else
				getPreControl(Ctrls,obj).focus();
		}
		else if(event.keyCode==39 )//NEXT
		{
			if(obj.value !="" && (obj.type == "text" || obj.tagName.toLowerCase() == "textarea"))
			{}
			else
				getNextControl(Ctrls,obj).focus();
		}
	}
}

//设置一个Cookie的值
function setcookie(name,value)
{
	var aa = new RegExp(";","ig");
    var bb = new RegExp("=","ig");
　　var Then = new Date();

	Then.setTime(Then.getTime() + 30*24*60*60*1000 ); //30天
	document.cookie = name + "=" + value.replace(aa,"；").replace(bb,"＝") + ";expires="+ Then.toGMTString()
}

//=============================================================================================
//这是一段肯定会执行的程序，而不是一个函数
  try
  {
	var TimeMinute=0;
	var TimeSecond=0;
	var OutTimeText='；您的编辑时间超过6分钟，请注意保存。';
	var OTT='';
	var TS=0;
	var TS=0;
	//在状态栏显示页面停留时间的函数
	function showtime()
	{
		if(TimeSecond>59){
			TimeMinute++;
			TimeSecond=0;
		}
		if(TimeMinute>59){
			TimeMinute=0;
		}
		if(TimeSecond<10){
			TS="0"+TimeSecond;
		}else{
			TS=TimeSecond;
		}
		if(TimeMinute<10){
			TM="0"+TimeMinute;
		}else{
			TM=TimeMinute;
		}
		if(TimeMinute>=6){
			if(TimeSecond%2==0){
				OTT=OutTimeText;
			}else{
				OTT=OutTimeText;
			}
		}
		if(TimeMinute>=10 && TimeSecond==0){
			OutTimeText='；您的编辑时间超过10分钟，请刷新或重新登录';
		}
		window.status="页面停留时间["+TM+":"+TS+"]"+OTT;
		TimeSecond++;
		setTimeout('showtime()',1000);
	}
	showtime(); //执行显示停留时间函数
}
catch(e){}

//document.body.onload = function onload()
function onLoad()
{
	var Orders = 0;
	for(var i=0;i<document.forms[0].elements.length;i++)
	{
		var obj = document.forms[0].elements[i];
		if(obj.type == "text" || obj.tagName.toLowerCase() == "textarea")
		{
			if(obj.disabled == false)
			{
				obj.Orders = Orders.toString();
				obj.onfocus = function onfocus(){DoFocus(this);}
				obj.onkeydown = function onkeydown(){DoKeyDown(document.forms[0].elements,this);}
				Orders++;
			}
		}
		else if(obj.tagName.toLowerCase() == "select")
		{
			if(obj.disabled == false)
			{
				obj.Orders = Orders.toString();
				obj.onfocus=function onfocus(){DoFocus(this);}
				obj.onkeydown = function onkeydown(){DoKeyDown(document.forms[0].elements,this);}
				Orders++;
			}
		}
		else if(obj.type == "button")
		{
			if(obj.disabled == false)
			{
				obj.Orders = Orders.toString();
				obj.onkeydown = function onkeydown(){DoKeyDown(document.forms[0].elements,this);}
				Orders++;
			}
		}
		//alert(obj.type+"="+obj.id+"="+obj.Orders);
	}
}

//在这个函数里面，将页面值保存到cookie，并记录了录入顺序
//document.body.onbeforeunload = function onbeforeunload()
function unLoad()
{
  	var cookieString = new String(document.cookie);
	var cookieHeader = 'WebCookies=';
	var cookiefooter = ";";
	var beginPosition = cookieString.indexOf(cookieHeader);
	var endPosition =cookieString.indexOf(cookiefooter);
	var WebCookies = '';
	if(beginPosition != -1)
	{
		WebCookies = cookieString.substring(beginPosition+ cookieHeader.length,endPosition);
	}
	//alert(WebCookies);
	for(var i=0;i<document.forms[0].elements.length;i++)
	{
		var obj = document.forms[0].elements[i];
		if(obj.Orders != null)
		{
			var objCookie = obj.value;
			if(WebCookies.indexOf('(' + obj.id + ')') != -1 && WebCookies.indexOf('(/' + obj.id + ')') != -1)
			{
				var aaaa = WebCookies.substring(WebCookies.indexOf('(' + obj.id + ')'),WebCookies.indexOf('(/' + obj.id + ')') + obj.id.length + 3);
				if(objCookie != '')
				{
					//var aaaa = new RegExp('^.*\('+obj.id+'\)'+'.*\(\/'+obj.id+'\).*$','g');
					//aaaa.Multiline = true;
					//WebCookies = WebCookies.replace(aaaa,'(' + obj.id + ')' + objCookie + '(/' + obj.id + ')');
					WebCookies = WebCookies.replace(aaaa,'(' + obj.id + ')' + objCookie + '(/' + obj.id + ')');
				}
				else
				{
					WebCookies = WebCookies.replace(aaaa,'');
				}
			}
			else if(objCookie != '')
			{
				WebCookies += '(' + obj.id + ')' + objCookie + '(/' + obj.id + ')';
			}
		}
	}
	setcookie('WebCookies',WebCookies);
	//alert(WebCookies);
}

//=============================================================================================
//获取某个下拉框的选中文本
function getSelectText(selectName)
{
	var objSelect = document.all(selectName);
	var selectedText = "";
	for(var i = 0; i < objSelect.options.length; i++)
	{
		if (objSelect.options(i).selected)
		{
			if (objSelect.options(i).value != "")
				selectedText += objSelect.options(i).text + ",";
		}
	}
	return selectedText.substring(0, selectedText.length - 1);
}

//设置某个下拉框的选中值
function setSelectText(selectName, strText)
{
	if ( strText == "" ) return;

	var objSelect	= document.all(selectName);

	var strArray = strText.split(",");
	for ( var i = 0; i < objSelect.options.length; i++)
	{
		objSelect.options(i).selected = false;
		for ( var k = 0; k < strArray.length; k++ )
		{
			if ( objSelect.options(i).text == strArray[k] )
			{
				objSelect.options(i).selected = true;
				break;
			}
		}
	}
}

//获取CheckBox或Radio数组的选定值。如果选中多个，以逗号分隔
function GetCheckedValue(elementName) {
	var obj = GetObject(elementName);
	var nLength = GetLength(elementName);
	var strValue = "";
	switch (nLength) {
		case 0:
			break;
		case 1:
			if (obj.checked)
				strValue = obj.value;
			break;
		default:
			for (var i = 0; i < nLength; i++) {
				if (obj.item(i).checked) {
					if (strValue == "")
						strValue = obj.item(i).value;
					else
						strValue += "," + obj.item(i).value;
			    }
			}
	}
	return strValue;
}

// 取元素数组的长度。
function GetLength(element)
{
    var obj = GetObject(element);
    if (obj == null)
        return 0;
    var nLength = obj.length;
    if (isNaN(nLength) || obj.tagName=="SELECT")
        nLength = 1;
    return nLength;
}

/**
 * 若参数为一对象，则直接返回此对象；若参数为一字符串，则返回以此字符串为ID的对象(或对象集合)。
 */
function GetObject(elementName) {
	var obj;
	if (typeof elementName == "object") {
		obj = elementName;
	}
	else {
		obj = document.all(elementName);
	}
	return obj;
}

//字符串是否为数字
function IsNumber(str){
    if (str.length == 0)
        return true;
    for (var i=0;i < str.length;i++)
    {
        if ((str.substring(i,i+1) < '0') || (str.substring(i,i+1) > '9'))
            return false;
    }
    return true;
}


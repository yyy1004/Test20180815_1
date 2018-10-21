function process_button()
{
	try
	{
	var oEl = event.srcElement;
	event.cancelBubble = true;
	while( -1 == oEl.className.indexOf( "Btn" ) )	//check if it is button
	{
		oEl = oEl.parentElement;
		if( !oEl ) return;
	}
	//the className is named of XXXBtnOff or XXXBtnUp......
	var baseClass = oEl.className.substring( 0 , oEl.className.indexOf( "Btn" ) + 3 );	//get the className
	var btnImage = null;
	if( oEl.IMG ) btnImage = oEl.all.tags( "IMG" )[0];	//check if there is a img to change
	switch( event.type )
	{
		case "mouseout" :
			if( oEl.contains( event.toElement ) ) return;
			if( btnImage )
			{
				btnImage.src =  btnImage.src.substring( 0 , btnImage.src.length - 5 ) + "1.gif";
			}
			if( oEl.STATE )
			{
				oEl.className = baseClass + oEl.STATE;
			}
			else
			{
				oEl.className = baseClass + "Off";
			}
			break;
		case "mouseover":
			if( oEl.contains( event.fromElement ) ) return;	
			if( btnImage )
			{
				btnImage.src =  btnImage.src.substring( 0 , btnImage.src.length - 5 ) + "2.gif";
			}
			if( oEl.STATE )
			{
				var tmpState = oEl.STATE == "Off" ? "Up" : "Down";
				oEl.className = baseClass + tmpState;
			}
			else
			{
				oEl.className = baseClass + "Up";
			}
			break;
		case "mousedown" :
			if (event.button == 1)
			oEl.className = baseClass + "Down";
			break;
	
		case "mouseup" :
			if( oEl.STATE )
			{
				oEl.STATE = oEl.STATE == "Off" ? "On" : "Off";
				var tmpState = oEl.STATE == "On" ? "Down" : "Up";
				oEl.className = baseClass + tmpState;
			}
			else
			{
				oEl.className = baseClass + "Up";
			}
			break;
	
		case "click" :
		    event.returnValue = doButtonClick( oEl );
			break;
	
		case "dblclick" :
			event.returnValue = doButtonClick( oEl );
			break;
	
		case "keyup" :
			if( 13 == event.keyCode )
			{
				if( oEl.STATE )
				{
					oEl.STATE = oEl.STATE == "Off" ? "On" : "Off";
					var tmpState = oEl.STATE == "On" ? "Down" : "Up";
					oEl.className = baseClass + tmpState;
				}
				else
				{
					oEl.className = baseClass + "Up";
				}
				event.returnValue = doButtonClick( oEl );
			}
			break;
			
		case "selectstart" :
			if( oEl )
			{
				event.returnValue = false;
				return false;
			}
			break;
		default :
			break;
	
	}
	}
	catch(e)
	{
	}
}

function doButtonClick( oEl )
{
/*	var oLink = oEl.all.tags( "A" );

	if( oLink.length  )
	{
		if( "_blank" == oLink[0].target )
		{
			winComments = window.open(  oLink[0].href ,"winComments" , "width=640,height=480,location=no,status=yes,resizable=yes,scrollbars=yes,menubar=yes,toolbar=no" );
			winComments.focus();
			event.returnValue = false;
			return false;
		}
		else if( !oEl.ACTION )
		{
			oLink[0].click();
		}
	}
*/
	if( "function" == typeof( fnCustomAction ) )
	{	
		return fnCustomAction( oEl );
	}
}
var show_help	=  false;
var w_help;
function get_help()
{
	screenWidth		=  window.screen.availWidth;
	screenHeight	=  window.screen.availHeight;
	try
	{
		urlHelp = getFrameUrl();
		astrLocation = window.location.href.split("/");
		strLoc = "";
		for ( i = 5; i < astrLocation.length; i++ )
		{
			strLoc = strLoc + "../";
		}
		if ( false == show_help || "object" != typeof( w_help.clientInformation ))
		{
			if ( screenWidth > 800 )
			{
				winHelpWidth = 230;
			}
			else
			{
				winHelpWidth = 180;
			}
			top.window.resizeTo( screenWidth - winHelpWidth + 4, screenHeight + 8 );
			top.window.moveTo( -4, -4 );
			w_help = window.open( strLoc + "help/help.jsp?id=" + urlHelp , "_blank" , "top=0,left=" + (screenWidth - winHelpWidth) + ",width=" + (winHelpWidth-10) + ",height=" + ( screenHeight - 60 + 4) + ",location=no,scrollbars=1,toolbar=no,resizable=1" );
			w_help.resizeTo( winHelpWidth, screenHeight + 8 );
			w_help.moveTo( screenWidth - winHelpWidth, 0 );
			show_help = true;
		}
		else
		{
			if ( "object" == typeof( w_help ) )
			{
				strWinHelpUrl = w_help.frames[1].location.href;
				strOldUrl	  = strWinHelpUrl.substring( strWinHelpUrl.indexOf("id=")+3, strWinHelpUrl.length );
				if ( strOldUrl != urlHelp )
				{
					w_help.frames[1].location.href = strLoc + "help/helpmain.jsp?id=" + urlHelp;
				}
				w_help.focus();
//				w_help.close();
			}
//			show_help = false;
		}
	}
	catch(e)
	{
	}
}
window.onunload = shutoffall;
function shutoffall()
{
	if ( "object" == typeof( w_help ) )
	{
		top.window.resizeTo( screenWidth , screenHeight + 8 );
		w_help.close();
	}
}
function getFrameUrl()
{
	var strTemp = new Array();
	var strTemp2= new Array();
	var strTemp2=window.location.href.split('/');
	if ( ("index.jsp" == strTemp2[strTemp2.length-1]) || ( 0 == strTemp2[strTemp2.length-1].length) )
	{
		return "index.jsp";
	}
	else
	{
		num 		= top.frames.length;
		curFrame 	= top.frames( num-1 );
		url 		= curFrame.location.href;
		if ( (-1 != url.indexOf("noteframe.jsp")) || (-1 != url.indexOf("jwmaster.jsp")) || (-1 != url.indexOf("teacher.jsp")) || (-1 != url.indexOf("masterfun.jsp")) || (-1 != url.indexOf("bzr.jsp")) || (-1 != url.indexOf("lecturewrite.jsp")) )
		{
			strTemp = url.split('?');
		}
		else
		{
			strTemp[0] = url;
		}
		strTemp		= strTemp[0].split('/');
		urlTemp		= strTemp.slice( 4, strTemp.length );
		url = urlTemp.join("/");
		url = url.replace( '&',';' );
		//alert( url );
		return url;
	}
}

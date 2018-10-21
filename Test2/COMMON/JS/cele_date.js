var date_start,date_end,g_object,pop
var a1=new Image;
var a2=new Image;
a1.src="../images/a1.gif";
a2.src="../images/a2.gif";

 function change_date(temp,mode)
 {
 var t_month,t_year
	if (mode){
	    if(mode==1)
		t_month=parseInt(cele_date_month.value,10)+parseInt(temp,10);
		else
		t_month=parseInt(temp)
		if (t_month<cele_date_month.options(0).text) {
			cele_date_month.value=cele_date_month.options(cele_date_month.length-1).text;
			change_date(parseInt(cele_date_year.value,10)-1,0);
			}
		else{
			if (t_month>cele_date_month.options(cele_date_month.length-1).text){
				cele_date_month.value=cele_date_month.options(0).text;
				change_date(parseInt(cele_date_year.value,10)+1,0);
				}			
			else
				{
				cele_date_month.value=t_month;
				 set_cele_date(cele_date_year.value,cele_date_month.value);				
				}
		}
	}  
	else{
		t_year=parseInt(temp,10);
		if (t_year<cele_date_year.options(0).text) {
			cele_date_year.value=cele_date_year.options(0).text;
			set_cele_date(cele_date_year.value,1);				
			}
		else{
			if (parseInt(t_year,10)>parseInt(cele_date_year.options(cele_date_year.length-1).text,10)){
				cele_date_year.value=cele_date_year.options(cele_date_year.length-1).text;
				set_cele_date(cele_date_year.value,12);				
				}			
			else
				{cele_date_year.value=t_year;
				 set_cele_date(cele_date_year.value,cele_date_month.value);				
				}
		}
	}
 }
 

 function init(d_start,d_end)
 {
 	 var temp_str;
	 var i=0
	 var j=0

     
     
	 document.writeln("<div name=\"cele_date\" id=\"cele_date\"  style=\"display:none;z-index:200;overflow:visible\" style=\"LEFT: 69px; POSITION: absolute; TOP: 159px\" onClick=\"event.cancelBubble=true;\" >&nbsp; </div>");
	 
	 //window.cele_date.innerHTML="";//为了兼容netscape注释掉此处--2001.3.29
	 temp_str="<table background=\"/namis/images/cele_title_bg.gif\" width=160 height=19 border=0 cellspacing=0 cellpadding=0><tr><td>&nbsp;</td></tr></table>"
	 temp_str+="<table width=160 border=1 cellspacing=2 cellpadding=0  bgcolor=\"#FFFFFF\" bordercolor=\"#C6D6E3\"><tr><td> <table bordercolor=\"#C6D6E3\" bgcolor=\"#C6D6E3\" cellPadding=0 cellSpacing=0 border=1>";
	 temp_str+="<tr><td colspan=7><center><b><u>日历</u></b></center></td></tr>"
	 temp_str+="<tr><td colspan=7>";
	 temp_str+="<center><img id=s_a1 name=s_a1 src=\"a1.gif\" language=\"javascript\" onclick=\"change_date(-1,1)\" >";
	 temp_str+=""
	 temp_str+="<select name=\"cele_date_year\" id=\"cele_date_year\" language=\"javascript\" onchange=\"change_date(this.value,0)\">"
	 for (i=1900;i<=2006;i++)
	 {
	 temp_str+="<OPTION value=\""+i.toString()+"\">"+i.toString()+"</OPTION>";
	 }
	 temp_str+="</select>&nbsp;";
	 temp_str+=""
	 temp_str+="<select name=\"cele_date_month\" id=\"cele_date_month\" language=\"javascript\" onchange=\"change_date(this.value,2)\" >"
	 for (i=1;i<=12;i++)
	 {
	 temp_str+="<OPTION value=\""+i.toString()+"\">"+i.toString()+"</OPTION>";
	 }
	 temp_str+="</select>&nbsp;";
	 temp_str+=""
	 temp_str+="<img id=s_a2 name=s_a2 src=\"a2.gif\" language=\"javascript\" onclick=\"change_date(1,1)\" >";
	 temp_str+="</center></td></tr><tr><td style=\"width:20px\">"
	 temp_str+="日</td><td style=\"width:20px\">";temp_str+="一</td><td style=\"width:20px\">"; temp_str+="二</td><td style=\"width:20px\">"; temp_str+="三</td><td style=\"width:20px\">"
	 temp_str+="四</td><td style=\"width:20px\">";temp_str+="五</td><td style=\"width:20px\">"; temp_str+="六</td></tr>";
	 temp_str+="<tr bgcolor=black><td style=\"height=4px;bgcolor:black\" colspan=7></td></tr>"
	 for (i=1 ;i<=6 ;i++)
	 {
	 temp_str+="<tr>";
		for(j=1;j<=7;j++){
			temp_str+="<td name=\"c"+i+"_"+j+"\"id=\"c"+i+"_"+j+"\" style=\"CURSOR: hand\"  language=\"javascript\" onclick=\"td_click(this)\">&nbsp;</td>"
			}
     temp_str+="</tr>"	    
	 }
	 temp_str+="</td></tr></table></td></tr></table>";
	 window.cele_date.innerHTML=temp_str;
	 document.all("s_a1").src=a1.src
	 document.all("s_a2").src=a2.src
 }
 
 function set_cele_date(year,month)
 {
   var i,j,p,k
   var nd=new Date(year,month-1,1);
   event.cancelBubble=true;
   cele_date_year.value=year;
   cele_date_month.value=month;   
   k=nd.getDay()-1
   for (i=1;i<=6;i++)
      for(j=1;j<=7;j++)
      {
      eval("c"+i+"_"+j+".innerHTML=\"&nbsp;\"");
      eval("c"+i+"_"+j+".bgColor=\"#C6D6E3\"");
      eval("c"+i+"_"+j+".style.cursor=\"hand\"");
     // eval("c"+i+"_"+j+".style.fontWeight=\"bold\"");
      
      }
   while(month-1==nd.getMonth())
    { 
	  j=(nd.getDay() +1);
      p=parseInt((nd.getDate()+k) / 7)+1;
      eval("c"+p+"_"+j+".innerHTML="+"\""+nd.getDate()+"\"");
      
      if (nd>date_end || nd<date_start)
      {
      //eval("c"+p+"_"+j+".style.backgroundImage.src=disble_ground.src");
      eval("c"+p+"_"+j+".style.color=\"Gainsboro\"");
      eval("c"+p+"_"+j+".style.cursor=\"text\"");
      }else
      eval("c"+p+"_"+j+".style.color=\"black\"");
      nd=new Date(nd.valueOf() + 86400000)
    }
 }


function show_cele_date(eP,d_start,d_end,t_object,str,offser_x,offser_y)
{
pop=str;			
var cur_x=offser_x
var cur_y=offser_y
var s,cur_d
var eT = eP.offsetTop;  
var eH = eP.offsetHeight;  
var dH = window.cele_date.style.pixelHeight;  
var sT = document.body.scrollTop;  
event.cancelBubble=true;

if (cur_x!=null )
{window.cele_date.style.left = eP.offsetLeft+(cur_x);  }
else
window.cele_date.style.left = eP.offsetLeft;  

if (cur_y!=null)
{
if(eT-dH >= sT && eT+eH+dH > document.body.clientHeight+sT)
window.cele_date.style.top = eT-dH+(cur_y);  
else
window.cele_date.style.top = eT+eH+(cur_y);  
}else
{
if(eT-dH >= sT && eT+eH+dH > document.body.clientHeight+sT)
window.cele_date.style.top = eT-dH;  
else
window.cele_date.style.top = eT+eH;  


}

s=d_start.split("-")
date_start=new Date(s[0],s[1]-1,s[2])
s=d_end.split("-")
date_end=new Date(s[0],s[1]-1,s[2])
g_object=t_object
cur_d=new Date()
set_cele_date(cur_d.getYear(),cur_d.getMonth()+1);
window.cele_date.style.display="block";



}
function td_click(t_object)
{
var t_d
if (parseInt(t_object.innerHTML,10)>=1 && parseInt(t_object.innerHTML,10)<=31 ) 
{
 t_d=new Date(cele_date_year.value,cele_date_month.value-1,t_object.innerHTML)
if (t_d<=date_end && t_d>=date_start)
 {g_object.value=cele_date_year.value+"-"+cele_date_month.value+"-"+t_object.innerHTML
 window.cele_date.style.display="none";}
	 if (pop!=(null||''))			
	{
	eval(pop);	
	}

 }
 g_object.focus();
}

function h_cele_date()
{
window.cele_date.style.display="none";
}
 
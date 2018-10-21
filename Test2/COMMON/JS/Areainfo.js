
function Dsy() 
{ 
 this.Items = {}; 
} 
Dsy.prototype.add = function(id,iArray) 
{ 
 this.Items[id] = iArray; 
} 
Dsy.prototype.Exists = function(id) 
{ 
 if(typeof(this.Items[id]) == "undefined") return false; 
 return true; 
} 

function change(v){ 
 var str="0"; 
 for(i=0;i<v;i++){ str+=("_"+(document.getElementById(s[i]).selectedIndex-1));}; 
 var ss=document.getElementById(s[v]); 
 with(ss){ 
  length = 0; 
  options[0]=new Option(opt0[v],opt0[v]); 
  if(v && document.getElementById(s[v-1]).selectedIndex>0 || !v) 
  { 
   if(dsy.Exists(str))
   { 
    ar = dsy.Items[str]; 
    for(i=0;i<ar.length;i++)options[length]=new Option(ar[i],ar[i]); 
    if(v)options[1].selected = true; 
   } 
  } 
  if(++v<s.length){change(v);} 
 } 
} 



var s=["sl_country","sl_province"]; 
var opt0 = ["-请选择-","-请选择-"]; 
function SetupAreaInfo() 
{ 
 for(i=0;i<s.length-1;i++) 
  document.getElementById(s[i]).onchange=new Function("change("+(i+1)+")"); 
  change(0);   
  if(document.all('hidencountry').value!='')
  {
	SetSelectAuto('sl_Country',document.all('hidencountry').value);
	change(1); 	
	SetSelectAuto('sl_Province',document.all('hidenprovince').value);
	
	//change(2);
	//SetSelectAuto('sl_City',document.all('hidencity').value);      
  }
  else
  {
	SetSelectAuto('sl_Country',"中国");
	change(1); 
	SetSelectAuto('sl_Province',"-请选择-");
	//change(2);
	//SetSelectAuto('sl_City',"-请选择-");      
  
  }
}

//下拉框自动选择
		function SetSelectAuto(controller,selecttext)
		{
			for(var i=0;i<document.all(controller).options.length;i++)
			{
				if(document.all(controller).options[i].text==selecttext)
				{
					document.all(controller).options[i].selected = true;
					break;
				}
			}
		}

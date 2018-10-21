function el(id){
    return document.getElementById(id);
}

function getReq()
{
    var oHttpReq = null;
    
    if(window.ActiveXObject)
        oHttpReq = new ActiveXObject("MSXML2.XMLHTTP");
    else if(window.createRequest)
        oHttpReq = window.createRequest();
    else
        oHttpReq = new XMLHttpRequest();
        
    return oHttpReq;
}

var shield;


function CommonAction(a,b,c,d)
{
    var req = getReq();
    req.open("POST", "http://localhost/efservicecenter/opt/CommonAction.aspx?Btn="+a+"&ID="+b+"&IDs="+c+"&Pages="+d, true);
    req.onreadystatechange = function(){
        if(req.readyState==4){
            //alert(req.responseText);
            if(a=="del")
            {
                //removeRow(el("tr_"+b));
                location.reload();
                alert("操作成功！");
            }
        }
    };
    req.send(null);
}

function ReturnPic(u,v)
{
    var req = getReq();
    req.open("POST", "http://10.0.0.95/efservicecenter/EfCompany/Product/PdtPicTreatment.aspx?Type="+u+"&GetPic=1&ID="+v, true);
    req.onreadystatechange = function(){
        if(req.readyState==4){
            
            el("star").src = req.responseText;
            
        }
    };
    req.send(null);   
}

function Goto(v)
{
        var req = getReq();
    req.open("POST", "http://10.0.0.95/efservicecenter/EfCompany/Product/PdtPicTreatment.aspx?Type="+v+"&GetPic=2", true);
    req.onreadystatechange = function(){
        if(req.readyState==4){
            
            window.opener.document.all("HidFeedBack").value = req.responseText;
            window.close();
            
        }
    };
    req.send(null); 
}



function removeRow(e)
{
    e.style.backgroundColor = "#ff0000";
    setTimeout(function(){e.style.backgroundColor = "#ffcccc";},100);
    setTimeout(function(){e.style.backgroundColor = "MistyRose";},300);
    setTimeout(function(){e.style.display = "none";},450);
}


function GetTagByInput(TagName)
{
	var i=0;
	var resultstr="";
    el("SearchField").value = "";
	var input = document.getElementsByTagName(TagName) ;
	//alert(input.length);
	
	for (i=0;i<input.length;i++)
	{
		if (input[i].id.substring(0,6)=="Search")
		{
			if(input[i].value.length>0)
			{
			    resultstr+=" and "+input[i].id.substring(7,input[i].id.length)+" like '%"+input[i].value+"%'";
			    //alert(resultstr);
			}
		}
	}
	el("SearchField").value = resultstr;
}
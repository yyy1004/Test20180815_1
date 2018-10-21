function el(id){
    return document.getElementById(id);
}

function showInfo(v){
    shideBody();
    
    el("tit").innerHTML = "展品类别选择";
    el("bod").innerHTML = "<img src='../../images/wait.gif' alt='loading...' />";
    el("bot").innerHTML = "<input type='button' value='提交' onclick='formPost();' /><input type='button' value='关闭' onclick='cancelShide()' />";
    
    var req = getReq();
    req.open("POST", v+".aspx?TypType=产品", true);
    req.onreadystatechange = function(){
        if(req.readyState==4){
            el("bod").innerHTML = req.responseText;
        }
    };
    
    req.send(null);
}

function showPerson(v){
    shideBody();
    
    el("tit").innerHTML = "联系人选择";
    el("bod").innerHTML = "<img src='../../images/wait.gif' alt='loading...' />";
    el("bot").innerHTML = "<input type='button' value='提交' onclick='formPost();' /><input type='button' value='关闭' onclick='cancelShide()' />";
    var req = getReq();
    req.open("POST", v+".aspx?Show=1", true);
    req.onreadystatechange = function(){
        if(req.readyState==4){
            el("bod").innerHTML = req.responseText;
        }
    };
    req.send(null);
}

function showGroup(v){
    shideBody();
    
    el("tit").innerHTML = "用户组选择";
    el("bod").innerHTML = "<img src='../../images/wait.gif' alt='loading...' />";
    el("bot").innerHTML = "<input type='button' value='提交' onclick='formPost();' /><input type='button' value='关闭' onclick='cancelShide()' />";
    var req = getReq();
    req.open("POST", v+".aspx?Show=1", true);
    req.onreadystatechange = function(){
        if(req.readyState==4){
            el("bod").innerHTML = req.responseText;
        }
    };
    req.send(null);
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
function shideBody()
{
    shield = document.createElement("DIV");
    shield.id = "shield";
    shield.style.position = "absolute";
    shield.style.left = "0px";
    shield.style.top = "0px";
    shield.style.width = "100%";
    shield.style.height = document.documentElement.scrollHeight+"px";
    shield.style.background = "#333333";
    shield.style.textAlign = "center";
    shield.style.zIndex = "10";
    shield.style.filter = "alpha(opacity=0)";
    shield.style.opacity = 0;
    document.body.appendChild(shield);
    
    this.setOpacity = function(obj,opacity){
    if(opacity>=1)opacity=opacity/100;
    try{ obj.style.opacity=opacity; }catch(e){}
    try{
        if(obj.filters.length>0 && obj.filters("alpha")){
            obj.filters("alpha").opacity=opacity*150;
        }else{
            obj.style.filter="alpha(opacity=\""+(opacity*150)+"\")";
        }
    }catch(e){}
    }
    var c = 0;
    this.doAlpha = function(){
    if (++c > 20){clearInterval(ad);return 0;}
    setOpacity(shield,c);
    }
    var ad = setInterval("doAlpha()",1);
    
    
    el("divh").style.display = "";
    el("divh").style.marginTop = -75+document.documentElement.scrollTop + "px";

        
}
function cancelShide()
{
    if(el("divh"))
    {
        el("divh").style.display = "none";
    }
    if(shield)
    {
        document.body.removeChild(shield);
        shield = null;
    }
    el("bod").innerHTML = "";
}
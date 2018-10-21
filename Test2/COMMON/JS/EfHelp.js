    function DisableHelpMenu()
    {
        var vMenuFrame=window.parent.frames['menuBar'];
        if(vMenuFrame==null) return;
        var vTarget=vMenuFrame.document.getElementById("LbHelp");
        vTarget.onclick = function() {return false;}
        vTarget.disabled=true;
    }
    function EnableHelpMenu()
    {
        var vMenuFrame=window.parent.frames['menuBar'];
        if(vMenuFrame==null) return;
        var vTarget=vMenuFrame.document.getElementById("LbHelp");
        vTarget.disabled=false;
        vTarget.onclick = function() {ShowMenu();}
    }
    
	function ShowMenu()
	{
	    var vMainFile= window.parent.frames['mainFrame'].document.location.href;
	    var vPos=vMainFile.lastIndexOf(".");
	    vMainFile = vMainFile.substring(0,vPos+1)+"htm";
	    window.open(vMainFile,"系统帮助",'left=80,top=80,width=800,height=600,menubar=no,status=no,location=no,toolbar=no,resizable=no');
	}

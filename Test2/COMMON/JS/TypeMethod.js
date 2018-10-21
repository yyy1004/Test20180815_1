	function HttpRequestSender(method, url, params, contentType, onsuccess, onerror){
		this.method = method;
		this.url = url;
		this.params = params;
		this.onsuccess = onsuccess;
		if(onerror){
		    this.onerror = onerror;
		}
		else{
			this.onerror = function(){};
		}
	}
		
	HttpRequestSender.prototype.sendRequest = function(){
		this.request = null;
		if (window.XMLHttpRequest) {
			this.request = new XMLHttpRequest();
		}else if (typeof ActiveXObject != "undefined"){
			try{
				this.request = new ActiveXObject("Microsoft.XMLHTTP");
			}
			catch(err){
				this.request = null;
			}
		}
		if (this.request){
        	try{
          		var loader=this;
            	this.request.onreadystatechange=function(){
            		HttpRequestSender.onReadyState.call(loader);
          		}
          		this.request.open(this.method,this.url,false);
          		if (this.contentType){
            		this.request.setRequestHeader('Content-Type', this.contentType);
          		}
          		this.request.send(this.params);
        	}catch (err){
          		this.onerror.call(this);
        	}
  		}
	}
	
	HttpRequestSender.onReadyState = function(){
		var req=this.request;
  		if (req.readyState == 4){
    		if (req.status == 200 || req.status==0){
      			this.onsuccess.call(this, this);
    		}else{
      			this.onerror.call(this);
    		}
  		}
	}
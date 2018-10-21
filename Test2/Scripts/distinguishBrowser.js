function getBrowserType() {
    var explorer = navigator.userAgent;
    var result;
    //ie 
    if (explorer.indexOf("MSIE") >= 0) {
        result = "ie";
    }
    //firefox 
    else if (explorer.indexOf("Firefox") >= 0) {
        result = "Firefox";
    }
    //Chrome
    else if (explorer.indexOf("Chrome") >= 0) {
        result = "Chrome";
    }
    //Opera
    else if (explorer.indexOf("Opera") >= 0) {
        result = "Opera";
    }
    //Safari
    else if (explorer.indexOf("Safari") >= 0) {
        result = "Safari";
    }
    //Netscape
    else if (explorer.indexOf("Netscape") >= 0) {
        result = 'Netscape';
    }
    return result;
}
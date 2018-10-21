var oDiv;
function DisLayer(obj,objForm,GetPager)
{
	if(oDiv == undefined)
	{
		obj.style.backgroundColor='#ffff66';
		oDiv = document.createElement('div');
		oDiv.style.position = 'absolute';
		oDiv.style.backgroundColor = '#ccccff';
		oDiv.style.top = document.body.scrollTop + window.event.clientY;
		oDiv.style.left = document.body.scrollLeft + window.event.clientX;
		var oXHR = new XHR();
		oXHR.open('GET', GetPager, false);
		oXHR.send(null);
		if (oXHR.readyState == 4)
		{
			if (oXHR.status == 200)
			{
				var responseStr = oXHR.responseText;
				if(responseStr != "")
				{
					oDiv.innerHTML = responseStr;
				}
			}
		}
		objForm.appendChild(oDiv);
	}
	else
	{
		obj.style.backgroundColor='#ffffff';
		oDiv.innerHTML = '';
		//objForm.removeChild(oDiv);
		oDiv = undefined;
	}
}
function XHR() 
{
	var oXHR = null;
	if (typeof XMLHttpRequest == 'function') { /* Firefox */
		oXHR = new XMLHttpRequest();
	}
	else { /* IE */
		var arrSignatures = [
			'Microsoft.XMLHTTP',
			'MSXML2.XMLHTTP'];
		for (var i = 0; i < arrSignatures.length; i++) {
			try {
				oXHR = new ActiveXObject(arrSignatures[i]);
				break;
			}
			catch (ex) {}
		}
	}
	if (oXHR == null) {
		throw new Error(0, "XMLHttpRequest not found.");
	}
	return oXHR;
};
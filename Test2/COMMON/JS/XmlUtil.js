//-----------------------------------------------------------------------------
//本文件包含函数列表
//	1.function createNode(NodeName,AttrName,AttrValue,TextValue)
//		--函数功能：根据输入的参数，创建一个节点
//	2.function insertNode(pNode,cNode,iPosition)
//		--函数功能：将指定节点插入目标节点的指定位置之前
//	3.function appendTableRowAt(tableIdName,trClassName,tdClassName,arrHtml,rowIndex)
//		--函数功能：在Table的一行上方添加一行
//	4.function hasElementChild(pNode)
//		--函数功能：判断一个节点是否包含NODE_ELEMENT类型的子节点
//	5.function isElementNode(oNode)
//		--函数功能：判断一个节点是否是NODE_ELEMENT类型的子节点
//	6.function isLastRow(tableID,rowIndex)
//		--函数功能：判断其中一行是否为表的最后一行
//	7.function getElementChildNumber(oNode)
//		--函数功能：读取节点中有效子节点的数量function findObjIndexById(obj)
//	8.function findObjIndexById(obj)
//		--函数功能：确定当前对象在数组对象中的位置
//	9.function convertXmlStringToNode(sXml)
//		--函数功能：将Xml字符串转化为一个节点
//	10.function insertNodesFromXmlString(oNode,sXml,iLevel)
//		--函数功能：将Xml字符串转化为节点列表，添加到指定节点里
//-----------------------------------------------------------------------------
//--函数功能：根据输入的参数，创建一个节点
//-----输入参数：
//----------NodeName：节点名称
//----------AttrName:属性字符串数组
//----------AttrValue:属性值字符串数组
//----------TextValue:属性字符串数组
//-----输出参数
//----------一个节点
	function createNode(NodeName,AttrName,AttrValue,TextValue)
	{

		var oXml=new ActiveXObject("Microsoft.XMLDOM");
		var oNode=oXml.createNode(1,NodeName,"");
		var aAttrList=oNode.attributes;//属性数组


		var oAttr;//临时变量，保存一个属性节点

		for (var i=0;i<AttrName.length;i++)
		{
			//依次添加各个属性
			oAttr=oXml.createAttribute(AttrName[i]); //创建属性节点
			oAttr.value=AttrValue[i]; //设置属性值
			aAttrList.setNamedItem(oAttr); //添加到节点
		}
		oNode.text=TextValue;

		oXml=null;

		return oNode;
	}

//-----------------------------------------------------------------------------
//-----------------------------------------------------------------------------
//--函数功能：将指定节点插入目标节点的指定位置之前
//-----输入参数：
//----------pNode：目标节点
//----------cNode: 指定节点
//----------iPosition:位置（-1:表示在尾部）
//-----输出参数
//----------无，但pNode的内容会发生变化
	function insertNode(pNode,cNode,iPosition)
	{
		var aChildList;

		if(iPosition<0)
		{
			//添加在节点的末尾
			pNode.appendChild(cNode);
		}
		else
		{
			//添加在某个节点之前
			aChildList=pNode.childNodes;
			for (var i=0;i<aChildList.length;i++)
			{
				if (i==iPosition)
				{
					pNode.insertBefore(cNode,pNode.childNodes.item(i));
					break;
				}
			}

		}
	}

//-----------------------------------------------------------------------------
//-----------------------------------------------------------------------------
//--函数功能：在Table的一行上方添加一行
//-----输入参数：
//----------tableIdName：表Table元素的id
//----------trClassName: TR元素的样式，没有为空
//----------tdClassName: TD元素的样式，没有为空
//----------arrHtml: TR元素的样式，没有为空
//----------rowIndex:位置（-1:表示在尾部）
//-----输出参数
//----------无，但Table的内容会发生变化
function appendTableRowAt(tableIdName,trClassName,tdClassName,arrHtml,rowIndex)
{
	var tb=document.all(tableIdName);
	var i = rowIndex;
	if(tb && tb.tagName=="TABLE")
	{
	   var rowId=tb.rows.length - 1;
	   //如果rowIndex超过最后一行，或者rowIndex<0,则将i设置为-1
	   if(rowIndex>rowId||rowIndex<0) i=-1;
	   var row=tb.insertRow(i);
	   var cols=arrHtml.length;

	   if(row)
	   {
	      row.className=trClassName ;
	      for(var j=0;j<cols;j++)
	      {
		 var cell=row.insertCell(j);
		 if(cell)
		 {
			if(j>0)
			{
				//第一列不受tdClassName影响
				cell.className=tdClassName;
		    }
		    cell.innerHTML=arrHtml[j];
		    cell.noWrap=true;
		 }
	       }
	    }
	}
}
//-----------------------------------------------------------------------------
//-----------------------------------------------------------------------------
//--函数功能：判断一个节点是否包含NODE_ELEMENT类型的子节点
//-----输入参数：
//----------pNode：目标节点
//-----输出参数
//----------true/false
function hasElementChild(pNode)
{
	var bResult=false;

	if(pNode.hasChildNodes())
	{
		for (var i=0;i<pNode.childNodes.length;i++)
		{
			if(isElementNode(pNode.childNodes.item(i)))
			{
				bResult=true;
				break;
			}
		}
	}

	return bResult;
}
//-----------------------------------------------------------------------------
//-----------------------------------------------------------------------------
//--函数功能：判断一个节点是否是NODE_ELEMENT类型的子节点
//-----输入参数：
//----------oNode：目标节点
//-----输出参数
//----------true/false
function isElementNode(oNode)
{
	var bResult=false;

	if(oNode.nodeType==1)
	{
		bResult=true;
	}

	return bResult;
}
//-----------------------------------------------------------------------------
//-----------------------------------------------------------------------------
//--函数功能：删除表中的一行
//-----输入参数：
//----------tableID：表元素的ID
//----------rowIndex：行数，从0开始计算
//-----输出参数
//----------无，对页面产生影响
function deleteTableRow(tableID,rowIndex)
{
	var oTable=document.all(tableID);
	//注意：oTable.rows.length中包含表头的一行，所以rowIndex的极限比它小
	if(rowIndex<oTable.rows.length) oTable.deleteRow(rowIndex);

}
//-----------------------------------------------------------------------------
//-----------------------------------------------------------------------------
//--函数功能：判断其中一行是否为表的最后一行
//-----输入参数：
//----------tableID：表元素的ID
//----------rowIndex：行数，从0开始计算
//-----输出参数
//----------true/false
//-----注意：这里涉及的表存在表头的一行，否则不对。
function isLastRow(tableID,rowIndex)
{
	if(rowIndex+1<document.all(tableID).rows.length-1)
		return false;
	else
		return true;
}
//-----------------------------------------------------------------------------
//-----------------------------------------------------------------------------
//--函数功能：读取节点中有效子节点的数量
//-----输入参数：
//----------oNode：节点
//-----输出参数
//----------有效节点数量
function getElementChildNumber(oNode)
{
	var oNodeList=oNode.childNodes;
	var len=0;

	for(var i=0;i<oNodeList.length;i++)
	{
		if(isElementNode(oNodeList.item(i)))
		{
			len=len+1;
		}
	}

	return len;

}
//-----------------------------------------------------------------------------
//-----------------------------------------------------------------------------
//--函数功能：确定当前对象在数组对象中的位置
//-----输入参数：
//----------obj:页面对象。该对象的id属性必须设置，是对象数组的成员。
//-----输出参数
//----------对象在对象数组中的位置，从0开始。不是数组时也返回0
function findObjIndexById(obj)
{
	var objCollection = document.all(obj.id);
	var objLen = objCollection.length;
	if ( isNaN(objLen) ) return 0;

	for ( var i = 0; i < objLen; i++ )
	{
		if ( obj == objCollection[i] )
			return i;
	}
	return 0;
}
//-----------------------------------------------------------------------------
//-----------------------------------------------------------------------------
//--函数功能：将一个节点的Xml字符串转化为一个节点
//-----输入参数：
//----------sXml:Xml字符串。
//-----输出参数
//----------节点
function convertXmlStringToNode(sXml)
{
	var oXml=new ActiveXObject("Microsoft.XMLDOM");
	var oRoot;

	oXml.loadXML(sXml);
	oRoot=oXml.documentElement;

	oXml=null;

	return oRoot;
}
//-----------------------------------------------------------------------------
//-----------------------------------------------------------------------------
//--函数功能：将Xml字符串转化为节点列表，添加到指定节点里
//-----输入参数：
//----------oNode:指定节点。
//----------sXml:Xml字符串。
//-----输出参数
//----------无，指定节点内容变化
function insertNodesFromXmlString(oNode,sXml,iLevel)
{
        var oXml=new ActiveXObject("Microsoft.XMLDOM");
        var oRoot;
        var oTempNode;
        var attrValue;

        oXml.loadXML("<root>"+sXml+"</root>");
        oRoot=oXml.documentElement;


        for (var i=0;i<oRoot.childNodes.length;i++)
        {
                if(isElementNode(oRoot.childNodes.item(i)))
                {
                        if(getNodeLevel(oRoot.childNodes.item(i).getAttribute('DetailCode'))==iLevel)
                        {
                                //找到指定级别的节点，从该级节点开始构造
                                oTempNode=oRoot.childNodes.item(i).cloneNode(true);
                                insertNodesFromXmlString(oTempNode,sXml,iLevel+1);
                                if(iLevel!=0) //如果不是顶级节点，则需要判断父节点
                                {
                                        attrValue=oTempNode.getAttribute('DetailCode');
                                        if(attrValue.substr(0,attrValue.lastIndexOf("-"))==oNode.getAttribute('DetailCode'))
                                        {
                                                //只添加自己的儿子
                                                oNode.appendChild(oTempNode);
                                        }
                                }
                                else
                                {   //顶级节点全部添加
                                        oNode.appendChild(oTempNode);
                                }
                        }
                }
        }

        oXml=null;
}
//-----------------------------------------------------------------------------
//-----------------------------------------------------------------------------
//-----------------------------------------------------------------------------
//--函数功能：根据序号，从Xml对象里查出相应的节点
//-----输入参数：
//----------idValue:属性值
//----------attrName:属性名称
//----------oNode:节点
//-----输出参数
//----------符合条件的节点。找不到，返回null

	function findNode(idValue,attrName,oNode)
	{
		var oNodeList=oNode.childNodes;
		var attrValue;
		var ifFind=false; //临时变量，确定是否找到
		var oNodeTemp=null;
		var i;


		for (i=0;i<oNodeList.length;i++)
		{
			if(isElementNode(oNodeList.item(i)))//只有是Element的节点才找
			{
				//先判断本节点是否是要查找的节点
				attrValue=oNodeList.item(i).getAttribute(attrName);
				if(attrValue==idValue)
				{
					ifFind=true;
					oNodeTemp=oNodeList.item(i);
					break;
				}
				//再判断本节点的子节点是否是要查找的节点
				oNodeTemp=findNode(idValue,attrName,oNodeList.item(i));
				if(oNodeTemp!=null)
				{
					ifFind=true;
					break;
				}
			}
		}

		if(ifFind)
			return oNodeTemp;
		else
			return null;

	}

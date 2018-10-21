var CCAjaxService=function() {
CCAjaxService.initializeBase(this);
this._timeout = 0;
this._userContext = null;
this._succeeded = null;
this._failed = null;
}
CCAjaxService.prototype={
_get_path:function() {
 var p = this.get_path();
 if (p) return p;
 else return CCAjaxService._staticInstance.get_path();},
UpdateCompanyInfo:function(ComID,FieldName,FieldValue,succeededCallback, failedCallback, userContext) {
return this._invoke(this._get_path(), 'UpdateCompanyInfo',false,{ComID:ComID,FieldName:FieldName,FieldValue:FieldValue},succeededCallback,failedCallback,userContext); },
UpdatePersonInfo:function(PersonIDList,FieldName,FieldValue,succeededCallback, failedCallback, userContext) {
return this._invoke(this._get_path(), 'UpdatePersonInfo',false,{PersonIDList:PersonIDList,FieldName:FieldName,FieldValue:FieldValue},succeededCallback,failedCallback,userContext); },
UpdatePersonJoinInfo:function(PsjIDList,FieldName,FieldValue,succeededCallback, failedCallback, userContext) {
return this._invoke(this._get_path(), 'UpdatePersonJoinInfo',false,{PsjIDList:PsjIDList,FieldName:FieldName,FieldValue:FieldValue},succeededCallback,failedCallback,userContext); },
SetComMainPerson:function(ComID,PsnID,succeededCallback, failedCallback, userContext) {
return this._invoke(this._get_path(), 'SetComMainPerson',false,{ComID:ComID,PsnID:PsnID},succeededCallback,failedCallback,userContext); },
GetVisitorListByName:function(VisitorName,succeededCallback, failedCallback, userContext) {
return this._invoke(this._get_path(), 'GetVisitorListByName',false,{VisitorName:VisitorName},succeededCallback,failedCallback,userContext); },
GetVisitorList:function(VisitorName,VisitorEmail,VisitorMobile,VisitorCom,succeededCallback, failedCallback, userContext) {
return this._invoke(this._get_path(), 'GetVisitorList',false,{VisitorName:VisitorName,VisitorEmail:VisitorEmail,VisitorMobile:VisitorMobile,VisitorCom:VisitorCom},succeededCallback,failedCallback,userContext); },
GetQuestionFormList:function(TaskIDList,succeededCallback, failedCallback, userContext) {
return this._invoke(this._get_path(), 'GetQuestionFormList',false,{TaskIDList:TaskIDList},succeededCallback,failedCallback,userContext); },
GetQuestionList:function(AnswerID,succeededCallback, failedCallback, userContext) {
return this._invoke(this._get_path(), 'GetQuestionList',false,{AnswerID:AnswerID},succeededCallback,failedCallback,userContext); }}
CCAjaxService.registerClass('CCAjaxService',Sys.Net.WebServiceProxy);
CCAjaxService._staticInstance = new CCAjaxService();
CCAjaxService.set_path = function(value) { CCAjaxService._staticInstance.set_path(value); }
CCAjaxService.get_path = function() { return CCAjaxService._staticInstance.get_path(); }
CCAjaxService.set_timeout = function(value) { CCAjaxService._staticInstance.set_timeout(value); }
CCAjaxService.get_timeout = function() { return CCAjaxService._staticInstance.get_timeout(); }
CCAjaxService.set_defaultUserContext = function(value) { CCAjaxService._staticInstance.set_defaultUserContext(value); }
CCAjaxService.get_defaultUserContext = function() { return CCAjaxService._staticInstance.get_defaultUserContext(); }
CCAjaxService.set_defaultSucceededCallback = function(value) { CCAjaxService._staticInstance.set_defaultSucceededCallback(value); }
CCAjaxService.get_defaultSucceededCallback = function() { return CCAjaxService._staticInstance.get_defaultSucceededCallback(); }
CCAjaxService.set_defaultFailedCallback = function(value) { CCAjaxService._staticInstance.set_defaultFailedCallback(value); }
CCAjaxService.get_defaultFailedCallback = function() { return CCAjaxService._staticInstance.get_defaultFailedCallback(); }
CCAjaxService.set_path("/EfServiceCenter/wcfservice/CCAjaxService.svc");
CCAjaxService.UpdateCompanyInfo= function(ComID,FieldName,FieldValue,onSuccess,onFailed,userContext) {CCAjaxService._staticInstance.UpdateCompanyInfo(ComID,FieldName,FieldValue,onSuccess,onFailed,userContext); }
CCAjaxService.UpdatePersonInfo= function(PersonIDList,FieldName,FieldValue,onSuccess,onFailed,userContext) {CCAjaxService._staticInstance.UpdatePersonInfo(PersonIDList,FieldName,FieldValue,onSuccess,onFailed,userContext); }
CCAjaxService.UpdatePersonJoinInfo= function(PsjIDList,FieldName,FieldValue,onSuccess,onFailed,userContext) {CCAjaxService._staticInstance.UpdatePersonJoinInfo(PsjIDList,FieldName,FieldValue,onSuccess,onFailed,userContext); }
CCAjaxService.SetComMainPerson= function(ComID,PsnID,onSuccess,onFailed,userContext) {CCAjaxService._staticInstance.SetComMainPerson(ComID,PsnID,onSuccess,onFailed,userContext); }
CCAjaxService.GetVisitorListByName= function(VisitorName,onSuccess,onFailed,userContext) {CCAjaxService._staticInstance.GetVisitorListByName(VisitorName,onSuccess,onFailed,userContext); }
CCAjaxService.GetVisitorList= function(VisitorName,VisitorEmail,VisitorMobile,VisitorCom,onSuccess,onFailed,userContext) {CCAjaxService._staticInstance.GetVisitorList(VisitorName,VisitorEmail,VisitorMobile,VisitorCom,onSuccess,onFailed,userContext); }
CCAjaxService.GetQuestionFormList= function(TaskIDList,onSuccess,onFailed,userContext) {CCAjaxService._staticInstance.GetQuestionFormList(TaskIDList,onSuccess,onFailed,userContext); }
CCAjaxService.GetQuestionList= function(AnswerID,onSuccess,onFailed,userContext) {CCAjaxService._staticInstance.GetQuestionList(AnswerID,onSuccess,onFailed,userContext); }

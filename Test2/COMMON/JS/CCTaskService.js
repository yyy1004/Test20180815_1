var CCTaskService=function()
{
CCTaskService.initializeBase(this);
this._timeout = 0;
this._userContext = null;
this._succeeded = null;
this._failed = null;
}
CCTaskService.prototype={
_get_path:function() {
 var p = this.get_path();
 if (p) return p;
 else return CCTaskService._staticInstance.get_path();},
DoWork:function(succeededCallback, failedCallback, userContext) {
return this._invoke(this._get_path(), 'DoWork',true,{},succeededCallback,failedCallback,userContext); }}
CCTaskService.registerClass('CCTaskService',Sys.Net.WebServiceProxy);
CCTaskService._staticInstance = new CCTaskService();
CCTaskService.set_path = function(value) { CCTaskService._staticInstance.set_path(value); }
CCTaskService.get_path = function() { return CCTaskService._staticInstance.get_path(); }
CCTaskService.set_timeout = function(value) { CCTaskService._staticInstance.set_timeout(value); }
CCTaskService.get_timeout = function() { return CCTaskService._staticInstance.get_timeout(); }
CCTaskService.set_defaultUserContext = function(value) { CCTaskService._staticInstance.set_defaultUserContext(value); }
CCTaskService.get_defaultUserContext = function() { return CCTaskService._staticInstance.get_defaultUserContext(); }
CCTaskService.set_defaultSucceededCallback = function(value) { CCTaskService._staticInstance.set_defaultSucceededCallback(value); }
CCTaskService.get_defaultSucceededCallback = function() { return CCTaskService._staticInstance.get_defaultSucceededCallback(); }
CCTaskService.set_defaultFailedCallback = function(value) { CCTaskService._staticInstance.set_defaultFailedCallback(value); }
CCTaskService.get_defaultFailedCallback = function() { return CCTaskService._staticInstance.get_defaultFailedCallback(); }
CCTaskService.set_path("/CallCenter/wcf/CCTaskService.svc");
CCTaskService.DoWork= function(onSuccess,onFailed,userContext) {CCTaskService._staticInstance.DoWork(onSuccess,onFailed,userContext); }

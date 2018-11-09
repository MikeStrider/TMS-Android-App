﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace TMSNextLevel.webrefAmazonTMSprogramUSeast {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="WebServiceSoap", Namespace="http://mstrong.tk")]
    public partial class WebService : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback SetDriverAvailOperationCompleted;
        
        private System.Threading.SendOrPostCallback SetDriverNotAvailOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetStatusOperationCompleted;
        
        private System.Threading.SendOrPostCallback updateVehicleOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public WebService() {
            this.Url = "http://tmsprogram.us-east-1.elasticbeanstalk.com/webservice.asmx";
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event SetDriverAvailCompletedEventHandler SetDriverAvailCompleted;
        
        /// <remarks/>
        public event SetDriverNotAvailCompletedEventHandler SetDriverNotAvailCompleted;
        
        /// <remarks/>
        public event GetStatusCompletedEventHandler GetStatusCompleted;
        
        /// <remarks/>
        public event updateVehicleCompletedEventHandler updateVehicleCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://mstrong.tk/SetDriverAvail", RequestNamespace="http://mstrong.tk", ResponseNamespace="http://mstrong.tk", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int SetDriverAvail(string drivername, string password, string location, string datetime) {
            object[] results = this.Invoke("SetDriverAvail", new object[] {
                        drivername,
                        password,
                        location,
                        datetime});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void SetDriverAvailAsync(string drivername, string password, string location, string datetime) {
            this.SetDriverAvailAsync(drivername, password, location, datetime, null);
        }
        
        /// <remarks/>
        public void SetDriverAvailAsync(string drivername, string password, string location, string datetime, object userState) {
            if ((this.SetDriverAvailOperationCompleted == null)) {
                this.SetDriverAvailOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSetDriverAvailOperationCompleted);
            }
            this.InvokeAsync("SetDriverAvail", new object[] {
                        drivername,
                        password,
                        location,
                        datetime}, this.SetDriverAvailOperationCompleted, userState);
        }
        
        private void OnSetDriverAvailOperationCompleted(object arg) {
            if ((this.SetDriverAvailCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SetDriverAvailCompleted(this, new SetDriverAvailCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://mstrong.tk/SetDriverNotAvail", RequestNamespace="http://mstrong.tk", ResponseNamespace="http://mstrong.tk", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public int SetDriverNotAvail(string drivername, string password) {
            object[] results = this.Invoke("SetDriverNotAvail", new object[] {
                        drivername,
                        password});
            return ((int)(results[0]));
        }
        
        /// <remarks/>
        public void SetDriverNotAvailAsync(string drivername, string password) {
            this.SetDriverNotAvailAsync(drivername, password, null);
        }
        
        /// <remarks/>
        public void SetDriverNotAvailAsync(string drivername, string password, object userState) {
            if ((this.SetDriverNotAvailOperationCompleted == null)) {
                this.SetDriverNotAvailOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSetDriverNotAvailOperationCompleted);
            }
            this.InvokeAsync("SetDriverNotAvail", new object[] {
                        drivername,
                        password}, this.SetDriverNotAvailOperationCompleted, userState);
        }
        
        private void OnSetDriverNotAvailOperationCompleted(object arg) {
            if ((this.SetDriverNotAvailCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SetDriverNotAvailCompleted(this, new SetDriverNotAvailCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://mstrong.tk/GetStatus", RequestNamespace="http://mstrong.tk", ResponseNamespace="http://mstrong.tk", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetStatus(string drivername, string password) {
            object[] results = this.Invoke("GetStatus", new object[] {
                        drivername,
                        password});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetStatusAsync(string drivername, string password) {
            this.GetStatusAsync(drivername, password, null);
        }
        
        /// <remarks/>
        public void GetStatusAsync(string drivername, string password, object userState) {
            if ((this.GetStatusOperationCompleted == null)) {
                this.GetStatusOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetStatusOperationCompleted);
            }
            this.InvokeAsync("GetStatus", new object[] {
                        drivername,
                        password}, this.GetStatusOperationCompleted, userState);
        }
        
        private void OnGetStatusOperationCompleted(object arg) {
            if ((this.GetStatusCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetStatusCompleted(this, new GetStatusCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://mstrong.tk/updateVehicle", RequestNamespace="http://mstrong.tk", ResponseNamespace="http://mstrong.tk", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string updateVehicle(int compid, string truck, string location, string datetime) {
            object[] results = this.Invoke("updateVehicle", new object[] {
                        compid,
                        truck,
                        location,
                        datetime});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void updateVehicleAsync(int compid, string truck, string location, string datetime) {
            this.updateVehicleAsync(compid, truck, location, datetime, null);
        }
        
        /// <remarks/>
        public void updateVehicleAsync(int compid, string truck, string location, string datetime, object userState) {
            if ((this.updateVehicleOperationCompleted == null)) {
                this.updateVehicleOperationCompleted = new System.Threading.SendOrPostCallback(this.OnupdateVehicleOperationCompleted);
            }
            this.InvokeAsync("updateVehicle", new object[] {
                        compid,
                        truck,
                        location,
                        datetime}, this.updateVehicleOperationCompleted, userState);
        }
        
        private void OnupdateVehicleOperationCompleted(object arg) {
            if ((this.updateVehicleCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.updateVehicleCompleted(this, new updateVehicleCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    public delegate void SetDriverAvailCompletedEventHandler(object sender, SetDriverAvailCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SetDriverAvailCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SetDriverAvailCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    public delegate void SetDriverNotAvailCompletedEventHandler(object sender, SetDriverNotAvailCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SetDriverNotAvailCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SetDriverNotAvailCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public int Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((int)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    public delegate void GetStatusCompletedEventHandler(object sender, GetStatusCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetStatusCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetStatusCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    public delegate void updateVehicleCompletedEventHandler(object sender, updateVehicleCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class updateVehicleCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal updateVehicleCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591
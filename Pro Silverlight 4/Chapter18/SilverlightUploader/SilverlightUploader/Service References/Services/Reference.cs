﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This code was auto-generated by Microsoft.Silverlight.ServiceReference, version 4.0.50401.0
// 
namespace SilverlightUploader.Services {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="", ConfigurationName="Services.FileService")]
    public interface FileService {
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="urn:FileService/GetFileList", ReplyAction="urn:FileService/GetFileListResponse")]
        System.IAsyncResult BeginGetFileList(System.AsyncCallback callback, object asyncState);
        
        System.Collections.ObjectModel.ObservableCollection<string> EndGetFileList(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="urn:FileService/UploadFile", ReplyAction="urn:FileService/UploadFileResponse")]
        System.IAsyncResult BeginUploadFile(string fileName, byte[] data, System.AsyncCallback callback, object asyncState);
        
        void EndUploadFile(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="urn:FileService/DownloadFile", ReplyAction="urn:FileService/DownloadFileResponse")]
        System.IAsyncResult BeginDownloadFile(string fileName, System.AsyncCallback callback, object asyncState);
        
        byte[] EndDownloadFile(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface FileServiceChannel : SilverlightUploader.Services.FileService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetFileListCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetFileListCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public System.Collections.ObjectModel.ObservableCollection<string> Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((System.Collections.ObjectModel.ObservableCollection<string>)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DownloadFileCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public DownloadFileCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public byte[] Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((byte[])(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class FileServiceClient : System.ServiceModel.ClientBase<SilverlightUploader.Services.FileService>, SilverlightUploader.Services.FileService {
        
        private BeginOperationDelegate onBeginGetFileListDelegate;
        
        private EndOperationDelegate onEndGetFileListDelegate;
        
        private System.Threading.SendOrPostCallback onGetFileListCompletedDelegate;
        
        private BeginOperationDelegate onBeginUploadFileDelegate;
        
        private EndOperationDelegate onEndUploadFileDelegate;
        
        private System.Threading.SendOrPostCallback onUploadFileCompletedDelegate;
        
        private BeginOperationDelegate onBeginDownloadFileDelegate;
        
        private EndOperationDelegate onEndDownloadFileDelegate;
        
        private System.Threading.SendOrPostCallback onDownloadFileCompletedDelegate;
        
        private BeginOperationDelegate onBeginOpenDelegate;
        
        private EndOperationDelegate onEndOpenDelegate;
        
        private System.Threading.SendOrPostCallback onOpenCompletedDelegate;
        
        private BeginOperationDelegate onBeginCloseDelegate;
        
        private EndOperationDelegate onEndCloseDelegate;
        
        private System.Threading.SendOrPostCallback onCloseCompletedDelegate;
        
        public FileServiceClient() {
        }
        
        public FileServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public FileServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FileServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FileServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Net.CookieContainer CookieContainer {
            get {
                System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
                if ((httpCookieContainerManager != null)) {
                    return httpCookieContainerManager.CookieContainer;
                }
                else {
                    return null;
                }
            }
            set {
                System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
                if ((httpCookieContainerManager != null)) {
                    httpCookieContainerManager.CookieContainer = value;
                }
                else {
                    throw new System.InvalidOperationException("Unable to set the CookieContainer. Please make sure the binding contains an HttpC" +
                            "ookieContainerBindingElement.");
                }
            }
        }
        
        public event System.EventHandler<GetFileListCompletedEventArgs> GetFileListCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> UploadFileCompleted;
        
        public event System.EventHandler<DownloadFileCompletedEventArgs> DownloadFileCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> OpenCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> CloseCompleted;
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult SilverlightUploader.Services.FileService.BeginGetFileList(System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetFileList(callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Collections.ObjectModel.ObservableCollection<string> SilverlightUploader.Services.FileService.EndGetFileList(System.IAsyncResult result) {
            return base.Channel.EndGetFileList(result);
        }
        
        private System.IAsyncResult OnBeginGetFileList(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((SilverlightUploader.Services.FileService)(this)).BeginGetFileList(callback, asyncState);
        }
        
        private object[] OnEndGetFileList(System.IAsyncResult result) {
            System.Collections.ObjectModel.ObservableCollection<string> retVal = ((SilverlightUploader.Services.FileService)(this)).EndGetFileList(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGetFileListCompleted(object state) {
            if ((this.GetFileListCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetFileListCompleted(this, new GetFileListCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetFileListAsync() {
            this.GetFileListAsync(null);
        }
        
        public void GetFileListAsync(object userState) {
            if ((this.onBeginGetFileListDelegate == null)) {
                this.onBeginGetFileListDelegate = new BeginOperationDelegate(this.OnBeginGetFileList);
            }
            if ((this.onEndGetFileListDelegate == null)) {
                this.onEndGetFileListDelegate = new EndOperationDelegate(this.OnEndGetFileList);
            }
            if ((this.onGetFileListCompletedDelegate == null)) {
                this.onGetFileListCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetFileListCompleted);
            }
            base.InvokeAsync(this.onBeginGetFileListDelegate, null, this.onEndGetFileListDelegate, this.onGetFileListCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult SilverlightUploader.Services.FileService.BeginUploadFile(string fileName, byte[] data, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginUploadFile(fileName, data, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        void SilverlightUploader.Services.FileService.EndUploadFile(System.IAsyncResult result) {
            base.Channel.EndUploadFile(result);
        }
        
        private System.IAsyncResult OnBeginUploadFile(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string fileName = ((string)(inValues[0]));
            byte[] data = ((byte[])(inValues[1]));
            return ((SilverlightUploader.Services.FileService)(this)).BeginUploadFile(fileName, data, callback, asyncState);
        }
        
        private object[] OnEndUploadFile(System.IAsyncResult result) {
            ((SilverlightUploader.Services.FileService)(this)).EndUploadFile(result);
            return null;
        }
        
        private void OnUploadFileCompleted(object state) {
            if ((this.UploadFileCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.UploadFileCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void UploadFileAsync(string fileName, byte[] data) {
            this.UploadFileAsync(fileName, data, null);
        }
        
        public void UploadFileAsync(string fileName, byte[] data, object userState) {
            if ((this.onBeginUploadFileDelegate == null)) {
                this.onBeginUploadFileDelegate = new BeginOperationDelegate(this.OnBeginUploadFile);
            }
            if ((this.onEndUploadFileDelegate == null)) {
                this.onEndUploadFileDelegate = new EndOperationDelegate(this.OnEndUploadFile);
            }
            if ((this.onUploadFileCompletedDelegate == null)) {
                this.onUploadFileCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnUploadFileCompleted);
            }
            base.InvokeAsync(this.onBeginUploadFileDelegate, new object[] {
                        fileName,
                        data}, this.onEndUploadFileDelegate, this.onUploadFileCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult SilverlightUploader.Services.FileService.BeginDownloadFile(string fileName, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginDownloadFile(fileName, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        byte[] SilverlightUploader.Services.FileService.EndDownloadFile(System.IAsyncResult result) {
            return base.Channel.EndDownloadFile(result);
        }
        
        private System.IAsyncResult OnBeginDownloadFile(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string fileName = ((string)(inValues[0]));
            return ((SilverlightUploader.Services.FileService)(this)).BeginDownloadFile(fileName, callback, asyncState);
        }
        
        private object[] OnEndDownloadFile(System.IAsyncResult result) {
            byte[] retVal = ((SilverlightUploader.Services.FileService)(this)).EndDownloadFile(result);
            return new object[] {
                    retVal};
        }
        
        private void OnDownloadFileCompleted(object state) {
            if ((this.DownloadFileCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.DownloadFileCompleted(this, new DownloadFileCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void DownloadFileAsync(string fileName) {
            this.DownloadFileAsync(fileName, null);
        }
        
        public void DownloadFileAsync(string fileName, object userState) {
            if ((this.onBeginDownloadFileDelegate == null)) {
                this.onBeginDownloadFileDelegate = new BeginOperationDelegate(this.OnBeginDownloadFile);
            }
            if ((this.onEndDownloadFileDelegate == null)) {
                this.onEndDownloadFileDelegate = new EndOperationDelegate(this.OnEndDownloadFile);
            }
            if ((this.onDownloadFileCompletedDelegate == null)) {
                this.onDownloadFileCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnDownloadFileCompleted);
            }
            base.InvokeAsync(this.onBeginDownloadFileDelegate, new object[] {
                        fileName}, this.onEndDownloadFileDelegate, this.onDownloadFileCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginOpen(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(callback, asyncState);
        }
        
        private object[] OnEndOpen(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndOpen(result);
            return null;
        }
        
        private void OnOpenCompleted(object state) {
            if ((this.OpenCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.OpenCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void OpenAsync() {
            this.OpenAsync(null);
        }
        
        public void OpenAsync(object userState) {
            if ((this.onBeginOpenDelegate == null)) {
                this.onBeginOpenDelegate = new BeginOperationDelegate(this.OnBeginOpen);
            }
            if ((this.onEndOpenDelegate == null)) {
                this.onEndOpenDelegate = new EndOperationDelegate(this.OnEndOpen);
            }
            if ((this.onOpenCompletedDelegate == null)) {
                this.onOpenCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnOpenCompleted);
            }
            base.InvokeAsync(this.onBeginOpenDelegate, null, this.onEndOpenDelegate, this.onOpenCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginClose(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginClose(callback, asyncState);
        }
        
        private object[] OnEndClose(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndClose(result);
            return null;
        }
        
        private void OnCloseCompleted(object state) {
            if ((this.CloseCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.CloseCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void CloseAsync() {
            this.CloseAsync(null);
        }
        
        public void CloseAsync(object userState) {
            if ((this.onBeginCloseDelegate == null)) {
                this.onBeginCloseDelegate = new BeginOperationDelegate(this.OnBeginClose);
            }
            if ((this.onEndCloseDelegate == null)) {
                this.onEndCloseDelegate = new EndOperationDelegate(this.OnEndClose);
            }
            if ((this.onCloseCompletedDelegate == null)) {
                this.onCloseCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnCloseCompleted);
            }
            base.InvokeAsync(this.onBeginCloseDelegate, null, this.onEndCloseDelegate, this.onCloseCompletedDelegate, userState);
        }
        
        protected override SilverlightUploader.Services.FileService CreateChannel() {
            return new FileServiceClientChannel(this);
        }
        
        private class FileServiceClientChannel : ChannelBase<SilverlightUploader.Services.FileService>, SilverlightUploader.Services.FileService {
            
            public FileServiceClientChannel(System.ServiceModel.ClientBase<SilverlightUploader.Services.FileService> client) : 
                    base(client) {
            }
            
            public System.IAsyncResult BeginGetFileList(System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[0];
                System.IAsyncResult _result = base.BeginInvoke("GetFileList", _args, callback, asyncState);
                return _result;
            }
            
            public System.Collections.ObjectModel.ObservableCollection<string> EndGetFileList(System.IAsyncResult result) {
                object[] _args = new object[0];
                System.Collections.ObjectModel.ObservableCollection<string> _result = ((System.Collections.ObjectModel.ObservableCollection<string>)(base.EndInvoke("GetFileList", _args, result)));
                return _result;
            }
            
            public System.IAsyncResult BeginUploadFile(string fileName, byte[] data, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[2];
                _args[0] = fileName;
                _args[1] = data;
                System.IAsyncResult _result = base.BeginInvoke("UploadFile", _args, callback, asyncState);
                return _result;
            }
            
            public void EndUploadFile(System.IAsyncResult result) {
                object[] _args = new object[0];
                base.EndInvoke("UploadFile", _args, result);
            }
            
            public System.IAsyncResult BeginDownloadFile(string fileName, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[1];
                _args[0] = fileName;
                System.IAsyncResult _result = base.BeginInvoke("DownloadFile", _args, callback, asyncState);
                return _result;
            }
            
            public byte[] EndDownloadFile(System.IAsyncResult result) {
                object[] _args = new object[0];
                byte[] _result = ((byte[])(base.EndInvoke("DownloadFile", _args, result)));
                return _result;
            }
        }
    }
}

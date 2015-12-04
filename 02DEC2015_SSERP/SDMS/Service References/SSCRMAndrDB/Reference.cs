﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4927
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SDMS.SSCRMAndrDB {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://202.63.115.34/", ConfigurationName="SSCRMAndrDB.SSCRMAndrDBSoap")]
    public interface SSCRMAndrDBSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://202.63.115.34/GetUserLogin_Proc", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        System.Data.DataSet GetUserLogin_Proc(string UserId, string Password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://202.63.115.34/BranchMaster_Proc", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        System.Data.DataSet BranchMaster_Proc(string CompanyCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://202.63.115.34/CompanyMaster_Proc", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        System.Data.DataSet CompanyMaster_Proc();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://202.63.115.34/InvProductSearchCursor_Get", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        System.Data.DataSet InvProductSearchCursor_Get(string CompanyCode, string BranchCode, string StateCode, string FinalYear);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://202.63.115.34/SSCRM_SRV2MOB_MAPPING", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        System.Data.DataSet SSCRM_SRV2MOB_MAPPING(string Number);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface SSCRMAndrDBSoapChannel : SDMS.SSCRMAndrDB.SSCRMAndrDBSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class SSCRMAndrDBSoapClient : System.ServiceModel.ClientBase<SDMS.SSCRMAndrDB.SSCRMAndrDBSoap>, SDMS.SSCRMAndrDB.SSCRMAndrDBSoap {
        
        public SSCRMAndrDBSoapClient() {
        }
        
        public SSCRMAndrDBSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SSCRMAndrDBSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SSCRMAndrDBSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SSCRMAndrDBSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Data.DataSet GetUserLogin_Proc(string UserId, string Password) {
            return base.Channel.GetUserLogin_Proc(UserId, Password);
        }
        
        public System.Data.DataSet BranchMaster_Proc(string CompanyCode) {
            return base.Channel.BranchMaster_Proc(CompanyCode);
        }
        
        public System.Data.DataSet CompanyMaster_Proc() {
            return base.Channel.CompanyMaster_Proc();
        }
        
        public System.Data.DataSet InvProductSearchCursor_Get(string CompanyCode, string BranchCode, string StateCode, string FinalYear) {
            return base.Channel.InvProductSearchCursor_Get(CompanyCode, BranchCode, StateCode, FinalYear);
        }
        
        public System.Data.DataSet SSCRM_SRV2MOB_MAPPING(string Number) {
            return base.Channel.SSCRM_SRV2MOB_MAPPING(Number);
        }
    }
}

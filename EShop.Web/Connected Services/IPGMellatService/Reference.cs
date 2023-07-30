//------------------------------------------------------------------------------

namespace IPGMellatService
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://interfaces.core.sw.bps.com/", ConfigurationName="IPGMellatService.IPaymentGateway")]
    public interface IPaymentGateway
    {
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        IPGMellatService.bpRefundRequestResponse bpRefundRequest(IPGMellatService.bpRefundRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        System.Threading.Tasks.Task<IPGMellatService.bpRefundRequestResponse> bpRefundRequestAsync(IPGMellatService.bpRefundRequest request);
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        IPGMellatService.bpSaleReferenceIdRequestResponse bpSaleReferenceIdRequest(IPGMellatService.bpSaleReferenceIdRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        System.Threading.Tasks.Task<IPGMellatService.bpSaleReferenceIdRequestResponse> bpSaleReferenceIdRequestAsync(IPGMellatService.bpSaleReferenceIdRequest request);
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        IPGMellatService.bpDynamicPayRequestResponse bpDynamicPayRequest(IPGMellatService.bpDynamicPayRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        System.Threading.Tasks.Task<IPGMellatService.bpDynamicPayRequestResponse> bpDynamicPayRequestAsync(IPGMellatService.bpDynamicPayRequest request);
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        IPGMellatService.bpInquiryRequestResponse bpInquiryRequest(IPGMellatService.bpInquiryRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        System.Threading.Tasks.Task<IPGMellatService.bpInquiryRequestResponse> bpInquiryRequestAsync(IPGMellatService.bpInquiryRequest request);
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        IPGMellatService.bpSettleRequestResponse bpSettleRequest(IPGMellatService.bpSettleRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        System.Threading.Tasks.Task<IPGMellatService.bpSettleRequestResponse> bpSettleRequestAsync(IPGMellatService.bpSettleRequest request);
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        IPGMellatService.bpChargePayRequestResponse bpChargePayRequest(IPGMellatService.bpChargePayRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        System.Threading.Tasks.Task<IPGMellatService.bpChargePayRequestResponse> bpChargePayRequestAsync(IPGMellatService.bpChargePayRequest request);
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        IPGMellatService.bpCumulativeDynamicPayRequestResponse bpCumulativeDynamicPayRequest(IPGMellatService.bpCumulativeDynamicPayRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        System.Threading.Tasks.Task<IPGMellatService.bpCumulativeDynamicPayRequestResponse> bpCumulativeDynamicPayRequestAsync(IPGMellatService.bpCumulativeDynamicPayRequest request);
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        IPGMellatService.bpPayRequestResponse bpPayRequest(IPGMellatService.bpPayRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        System.Threading.Tasks.Task<IPGMellatService.bpPayRequestResponse> bpPayRequestAsync(IPGMellatService.bpPayRequest request);
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        IPGMellatService.bpRefundInquiryRequestResponse bpRefundInquiryRequest(IPGMellatService.bpRefundInquiryRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        System.Threading.Tasks.Task<IPGMellatService.bpRefundInquiryRequestResponse> bpRefundInquiryRequestAsync(IPGMellatService.bpRefundInquiryRequest request);
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        IPGMellatService.bpReversalRequestResponse bpReversalRequest(IPGMellatService.bpReversalRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        System.Threading.Tasks.Task<IPGMellatService.bpReversalRequestResponse> bpReversalRequestAsync(IPGMellatService.bpReversalRequest request);
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        IPGMellatService.bpPosRefundRequestResponse bpPosRefundRequest(IPGMellatService.bpPosRefundRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        System.Threading.Tasks.Task<IPGMellatService.bpPosRefundRequestResponse> bpPosRefundRequestAsync(IPGMellatService.bpPosRefundRequest request);
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        IPGMellatService.bpRefundToPANRequestResponse bpRefundToPANRequest(IPGMellatService.bpRefundToPANRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        System.Threading.Tasks.Task<IPGMellatService.bpRefundToPANRequestResponse> bpRefundToPANRequestAsync(IPGMellatService.bpRefundToPANRequest request);
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        IPGMellatService.bpVerifyRequestResponse bpVerifyRequest(IPGMellatService.bpVerifyRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        System.Threading.Tasks.Task<IPGMellatService.bpVerifyRequestResponse> bpVerifyRequestAsync(IPGMellatService.bpVerifyRequest request);
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        IPGMellatService.bpRefundVerifyRequestResponse bpRefundVerifyRequest(IPGMellatService.bpRefundVerifyRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        System.Threading.Tasks.Task<IPGMellatService.bpRefundVerifyRequestResponse> bpRefundVerifyRequestAsync(IPGMellatService.bpRefundVerifyRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class bpRefundRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="bpRefundRequest", Namespace="http://interfaces.core.sw.bps.com/", Order=0)]
        public IPGMellatService.bpRefundRequestBody Body;
        
        public bpRefundRequest()
        {
        }
        
        public bpRefundRequest(IPGMellatService.bpRefundRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class bpRefundRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public long terminalId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string userName;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string userPassword;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public long orderId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public long saleOrderId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public long saleReferenceId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=6)]
        public long refundAmount;
        
        public bpRefundRequestBody()
        {
        }
        
        public bpRefundRequestBody(long terminalId, string userName, string userPassword, long orderId, long saleOrderId, long saleReferenceId, long refundAmount)
        {
            this.terminalId = terminalId;
            this.userName = userName;
            this.userPassword = userPassword;
            this.orderId = orderId;
            this.saleOrderId = saleOrderId;
            this.saleReferenceId = saleReferenceId;
            this.refundAmount = refundAmount;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class bpRefundRequestResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="bpRefundRequestResponse", Namespace="http://interfaces.core.sw.bps.com/", Order=0)]
        public IPGMellatService.bpRefundRequestResponseBody Body;
        
        public bpRefundRequestResponse()
        {
        }
        
        public bpRefundRequestResponse(IPGMellatService.bpRefundRequestResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class bpRefundRequestResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string @return;
        
        public bpRefundRequestResponseBody()
        {
        }
        
        public bpRefundRequestResponseBody(string @return)
        {
            this.@return = @return;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class bpSaleReferenceIdRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="bpSaleReferenceIdRequest", Namespace="http://interfaces.core.sw.bps.com/", Order=0)]
        public IPGMellatService.bpSaleReferenceIdRequestBody Body;
        
        public bpSaleReferenceIdRequest()
        {
        }
        
        public bpSaleReferenceIdRequest(IPGMellatService.bpSaleReferenceIdRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class bpSaleReferenceIdRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public long terminalId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string userName;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string userPassword;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public long orderId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public long saleOrderId;
        
        public bpSaleReferenceIdRequestBody()
        {
        }
        
        public bpSaleReferenceIdRequestBody(long terminalId, string userName, string userPassword, long orderId, long saleOrderId)
        {
            this.terminalId = terminalId;
            this.userName = userName;
            this.userPassword = userPassword;
            this.orderId = orderId;
            this.saleOrderId = saleOrderId;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class bpSaleReferenceIdRequestResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="bpSaleReferenceIdRequestResponse", Namespace="http://interfaces.core.sw.bps.com/", Order=0)]
        public IPGMellatService.bpSaleReferenceIdRequestResponseBody Body;
        
        public bpSaleReferenceIdRequestResponse()
        {
        }
        
        public bpSaleReferenceIdRequestResponse(IPGMellatService.bpSaleReferenceIdRequestResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class bpSaleReferenceIdRequestResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string @return;
        
        public bpSaleReferenceIdRequestResponseBody()
        {
        }
        
        public bpSaleReferenceIdRequestResponseBody(string @return)
        {
            this.@return = @return;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class bpDynamicPayRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="bpDynamicPayRequest", Namespace="http://interfaces.core.sw.bps.com/", Order=0)]
        public IPGMellatService.bpDynamicPayRequestBody Body;
        
        public bpDynamicPayRequest()
        {
        }
        
        public bpDynamicPayRequest(IPGMellatService.bpDynamicPayRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class bpDynamicPayRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public long terminalId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string userName;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string userPassword;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public long orderId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public long amount;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string localDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public string localTime;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=7)]
        public string additionalData;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=8)]
        public string callBackUrl;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=9)]
        public string payerId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=10)]
        public long subServiceId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=11)]
        public string mobileNo;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=12)]
        public string encPan;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=13)]
        public string panHiddenMode;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=14)]
        public string cartItem;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=15)]
        public string enc;
        
        public bpDynamicPayRequestBody()
        {
        }
        
        public bpDynamicPayRequestBody(
                    long terminalId, 
                    string userName, 
                    string userPassword, 
                    long orderId, 
                    long amount, 
                    string localDate, 
                    string localTime, 
                    string additionalData, 
                    string callBackUrl, 
                    string payerId, 
                    long subServiceId, 
                    string mobileNo, 
                    string encPan, 
                    string panHiddenMode, 
                    string cartItem, 
                    string enc)
        {
            this.terminalId = terminalId;
            this.userName = userName;
            this.userPassword = userPassword;
            this.orderId = orderId;
            this.amount = amount;
            this.localDate = localDate;
            this.localTime = localTime;
            this.additionalData = additionalData;
            this.callBackUrl = callBackUrl;
            this.payerId = payerId;
            this.subServiceId = subServiceId;
            this.mobileNo = mobileNo;
            this.encPan = encPan;
            this.panHiddenMode = panHiddenMode;
            this.cartItem = cartItem;
            this.enc = enc;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class bpDynamicPayRequestResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="bpDynamicPayRequestResponse", Namespace="http://interfaces.core.sw.bps.com/", Order=0)]
        public IPGMellatService.bpDynamicPayRequestResponseBody Body;
        
        public bpDynamicPayRequestResponse()
        {
        }
        
        public bpDynamicPayRequestResponse(IPGMellatService.bpDynamicPayRequestResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class bpDynamicPayRequestResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string @return;
        
        public bpDynamicPayRequestResponseBody()
        {
        }
        
        public bpDynamicPayRequestResponseBody(string @return)
        {
            this.@return = @return;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class bpInquiryRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="bpInquiryRequest", Namespace="http://interfaces.core.sw.bps.com/", Order=0)]
        public IPGMellatService.bpInquiryRequestBody Body;
        
        public bpInquiryRequest()
        {
        }
        
        public bpInquiryRequest(IPGMellatService.bpInquiryRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class bpInquiryRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public long terminalId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string userName;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string userPassword;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public long orderId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public long saleOrderId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public long saleReferenceId;
        
        public bpInquiryRequestBody()
        {
        }
        
        public bpInquiryRequestBody(long terminalId, string userName, string userPassword, long orderId, long saleOrderId, long saleReferenceId)
        {
            this.terminalId = terminalId;
            this.userName = userName;
            this.userPassword = userPassword;
            this.orderId = orderId;
            this.saleOrderId = saleOrderId;
            this.saleReferenceId = saleReferenceId;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class bpInquiryRequestResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="bpInquiryRequestResponse", Namespace="http://interfaces.core.sw.bps.com/", Order=0)]
        public IPGMellatService.bpInquiryRequestResponseBody Body;
        
        public bpInquiryRequestResponse()
        {
        }
        
        public bpInquiryRequestResponse(IPGMellatService.bpInquiryRequestResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class bpInquiryRequestResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string @return;
        
        public bpInquiryRequestResponseBody()
        {
        }
        
        public bpInquiryRequestResponseBody(string @return)
        {
            this.@return = @return;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class bpSettleRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="bpSettleRequest", Namespace="http://interfaces.core.sw.bps.com/", Order=0)]
        public IPGMellatService.bpSettleRequestBody Body;
        
        public bpSettleRequest()
        {
        }
        
        public bpSettleRequest(IPGMellatService.bpSettleRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class bpSettleRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public long terminalId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string userName;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string userPassword;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public long orderId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public long saleOrderId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public long saleReferenceId;
        
        public bpSettleRequestBody()
        {
        }
        
        public bpSettleRequestBody(long terminalId, string userName, string userPassword, long orderId, long saleOrderId, long saleReferenceId)
        {
            this.terminalId = terminalId;
            this.userName = userName;
            this.userPassword = userPassword;
            this.orderId = orderId;
            this.saleOrderId = saleOrderId;
            this.saleReferenceId = saleReferenceId;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class bpSettleRequestResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="bpSettleRequestResponse", Namespace="http://interfaces.core.sw.bps.com/", Order=0)]
        public IPGMellatService.bpSettleRequestResponseBody Body;
        
        public bpSettleRequestResponse()
        {
        }
        
        public bpSettleRequestResponse(IPGMellatService.bpSettleRequestResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class bpSettleRequestResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string @return;
        
        public bpSettleRequestResponseBody()
        {
        }
        
        public bpSettleRequestResponseBody(string @return)
        {
            this.@return = @return;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class bpChargePayRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="bpChargePayRequest", Namespace="http://interfaces.core.sw.bps.com/", Order=0)]
        public IPGMellatService.bpChargePayRequestBody Body;
        
        public bpChargePayRequest()
        {
        }
        
        public bpChargePayRequest(IPGMellatService.bpChargePayRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class bpChargePayRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public long terminalId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string userName;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string userPassword;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public long orderId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public long amount;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string localDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public string localTime;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=7)]
        public string additionalData;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=8)]
        public string callBackUrl;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=9)]
        public string payerId;
        
        public bpChargePayRequestBody()
        {
        }
        
        public bpChargePayRequestBody(long terminalId, string userName, string userPassword, long orderId, long amount, string localDate, string localTime, string additionalData, string callBackUrl, string payerId)
        {
            this.terminalId = terminalId;
            this.userName = userName;
            this.userPassword = userPassword;
            this.orderId = orderId;
            this.amount = amount;
            this.localDate = localDate;
            this.localTime = localTime;
            this.additionalData = additionalData;
            this.callBackUrl = callBackUrl;
            this.payerId = payerId;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class bpChargePayRequestResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="bpChargePayRequestResponse", Namespace="http://interfaces.core.sw.bps.com/", Order=0)]
        public IPGMellatService.bpChargePayRequestResponseBody Body;
        
        public bpChargePayRequestResponse()
        {
        }
        
        public bpChargePayRequestResponse(IPGMellatService.bpChargePayRequestResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class bpChargePayRequestResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string @return;
        
        public bpChargePayRequestResponseBody()
        {
        }
        
        public bpChargePayRequestResponseBody(string @return)
        {
            this.@return = @return;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class bpCumulativeDynamicPayRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="bpCumulativeDynamicPayRequest", Namespace="http://interfaces.core.sw.bps.com/", Order=0)]
        public IPGMellatService.bpCumulativeDynamicPayRequestBody Body;
        
        public bpCumulativeDynamicPayRequest()
        {
        }
        
        public bpCumulativeDynamicPayRequest(IPGMellatService.bpCumulativeDynamicPayRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class bpCumulativeDynamicPayRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public long terminalId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string userName;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string userPassword;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public long orderId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public long amount;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string localDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public string localTime;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=7)]
        public string additionalData;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=8)]
        public string callBackUrl;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=9)]
        public string mobileNo;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=10)]
        public string encPan;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=11)]
        public string panHiddenMode;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=12)]
        public string cartItem;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=13)]
        public string enc;
        
        public bpCumulativeDynamicPayRequestBody()
        {
        }
        
        public bpCumulativeDynamicPayRequestBody(long terminalId, string userName, string userPassword, long orderId, long amount, string localDate, string localTime, string additionalData, string callBackUrl, string mobileNo, string encPan, string panHiddenMode, string cartItem, string enc)
        {
            this.terminalId = terminalId;
            this.userName = userName;
            this.userPassword = userPassword;
            this.orderId = orderId;
            this.amount = amount;
            this.localDate = localDate;
            this.localTime = localTime;
            this.additionalData = additionalData;
            this.callBackUrl = callBackUrl;
            this.mobileNo = mobileNo;
            this.encPan = encPan;
            this.panHiddenMode = panHiddenMode;
            this.cartItem = cartItem;
            this.enc = enc;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class bpCumulativeDynamicPayRequestResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="bpCumulativeDynamicPayRequestResponse", Namespace="http://interfaces.core.sw.bps.com/", Order=0)]
        public IPGMellatService.bpCumulativeDynamicPayRequestResponseBody Body;
        
        public bpCumulativeDynamicPayRequestResponse()
        {
        }
        
        public bpCumulativeDynamicPayRequestResponse(IPGMellatService.bpCumulativeDynamicPayRequestResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class bpCumulativeDynamicPayRequestResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string @return;
        
        public bpCumulativeDynamicPayRequestResponseBody()
        {
        }
        
        public bpCumulativeDynamicPayRequestResponseBody(string @return)
        {
            this.@return = @return;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class bpPayRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="bpPayRequest", Namespace="http://interfaces.core.sw.bps.com/", Order=0)]
        public IPGMellatService.bpPayRequestBody Body;
        
        public bpPayRequest()
        {
        }
        
        public bpPayRequest(IPGMellatService.bpPayRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class bpPayRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public long terminalId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string userName;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string userPassword;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public long orderId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public long amount;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string localDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public string localTime;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=7)]
        public string additionalData;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=8)]
        public string callBackUrl;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=9)]
        public string payerId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=10)]
        public string mobileNo;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=11)]
        public string encPan;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=12)]
        public string panHiddenMode;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=13)]
        public string cartItem;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=14)]
        public string enc;
        
        public bpPayRequestBody()
        {
        }
        
        public bpPayRequestBody(long terminalId, string userName, string userPassword, long orderId, long amount, string localDate, string localTime, string additionalData, string callBackUrl, string payerId, string mobileNo, string encPan, string panHiddenMode, string cartItem, string enc)
        {
            this.terminalId = terminalId;
            this.userName = userName;
            this.userPassword = userPassword;
            this.orderId = orderId;
            this.amount = amount;
            this.localDate = localDate;
            this.localTime = localTime;
            this.additionalData = additionalData;
            this.callBackUrl = callBackUrl;
            this.payerId = payerId;
            this.mobileNo = mobileNo;
            this.encPan = encPan;
            this.panHiddenMode = panHiddenMode;
            this.cartItem = cartItem;
            this.enc = enc;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class bpPayRequestResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="bpPayRequestResponse", Namespace="http://interfaces.core.sw.bps.com/", Order=0)]
        public IPGMellatService.bpPayRequestResponseBody Body;
        
        public bpPayRequestResponse()
        {
        }
        
        public bpPayRequestResponse(IPGMellatService.bpPayRequestResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class bpPayRequestResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string @return;
        
        public bpPayRequestResponseBody()
        {
        }
        
        public bpPayRequestResponseBody(string @return)
        {
            this.@return = @return;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class bpRefundInquiryRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="bpRefundInquiryRequest", Namespace="http://interfaces.core.sw.bps.com/", Order=0)]
        public IPGMellatService.bpRefundInquiryRequestBody Body;
        
        public bpRefundInquiryRequest()
        {
        }
        
        public bpRefundInquiryRequest(IPGMellatService.bpRefundInquiryRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class bpRefundInquiryRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public long terminalId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string userName;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string userPassword;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public long orderId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public long refundOrderId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public long refundReferenceId;
        
        public bpRefundInquiryRequestBody()
        {
        }
        
        public bpRefundInquiryRequestBody(long terminalId, string userName, string userPassword, long orderId, long refundOrderId, long refundReferenceId)
        {
            this.terminalId = terminalId;
            this.userName = userName;
            this.userPassword = userPassword;
            this.orderId = orderId;
            this.refundOrderId = refundOrderId;
            this.refundReferenceId = refundReferenceId;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class bpRefundInquiryRequestResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="bpRefundInquiryRequestResponse", Namespace="http://interfaces.core.sw.bps.com/", Order=0)]
        public IPGMellatService.bpRefundInquiryRequestResponseBody Body;
        
        public bpRefundInquiryRequestResponse()
        {
        }
        
        public bpRefundInquiryRequestResponse(IPGMellatService.bpRefundInquiryRequestResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class bpRefundInquiryRequestResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string @return;
        
        public bpRefundInquiryRequestResponseBody()
        {
        }
        
        public bpRefundInquiryRequestResponseBody(string @return)
        {
            this.@return = @return;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class bpReversalRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="bpReversalRequest", Namespace="http://interfaces.core.sw.bps.com/", Order=0)]
        public IPGMellatService.bpReversalRequestBody Body;
        
        public bpReversalRequest()
        {
        }
        
        public bpReversalRequest(IPGMellatService.bpReversalRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class bpReversalRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public long terminalId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string userName;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string userPassword;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public long orderId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public long saleOrderId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public long saleReferenceId;
        
        public bpReversalRequestBody()
        {
        }
        
        public bpReversalRequestBody(long terminalId, string userName, string userPassword, long orderId, long saleOrderId, long saleReferenceId)
        {
            this.terminalId = terminalId;
            this.userName = userName;
            this.userPassword = userPassword;
            this.orderId = orderId;
            this.saleOrderId = saleOrderId;
            this.saleReferenceId = saleReferenceId;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class bpReversalRequestResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="bpReversalRequestResponse", Namespace="http://interfaces.core.sw.bps.com/", Order=0)]
        public IPGMellatService.bpReversalRequestResponseBody Body;
        
        public bpReversalRequestResponse()
        {
        }
        
        public bpReversalRequestResponse(IPGMellatService.bpReversalRequestResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class bpReversalRequestResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string @return;
        
        public bpReversalRequestResponseBody()
        {
        }
        
        public bpReversalRequestResponseBody(string @return)
        {
            this.@return = @return;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class bpPosRefundRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="bpPosRefundRequest", Namespace="http://interfaces.core.sw.bps.com/", Order=0)]
        public IPGMellatService.bpPosRefundRequestBody Body;
        
        public bpPosRefundRequest()
        {
        }
        
        public bpPosRefundRequest(IPGMellatService.bpPosRefundRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class bpPosRefundRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string user;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string password;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public long saleReferenceId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public long refundAmount;
        
        public bpPosRefundRequestBody()
        {
        }
        
        public bpPosRefundRequestBody(string user, string password, long saleReferenceId, long refundAmount)
        {
            this.user = user;
            this.password = password;
            this.saleReferenceId = saleReferenceId;
            this.refundAmount = refundAmount;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class bpPosRefundRequestResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="bpPosRefundRequestResponse", Namespace="http://interfaces.core.sw.bps.com/", Order=0)]
        public IPGMellatService.bpPosRefundRequestResponseBody Body;
        
        public bpPosRefundRequestResponse()
        {
        }
        
        public bpPosRefundRequestResponse(IPGMellatService.bpPosRefundRequestResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class bpPosRefundRequestResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string @return;
        
        public bpPosRefundRequestResponseBody()
        {
        }
        
        public bpPosRefundRequestResponseBody(string @return)
        {
            this.@return = @return;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class bpRefundToPANRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="bpRefundToPANRequest", Namespace="http://interfaces.core.sw.bps.com/", Order=0)]
        public IPGMellatService.bpRefundToPANRequestBody Body;
        
        public bpRefundToPANRequest()
        {
        }
        
        public bpRefundToPANRequest(IPGMellatService.bpRefundToPANRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class bpRefundToPANRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string user;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string password;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public long pan;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public long amount;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public long saleReferenceId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public long terminalId;
        
        public bpRefundToPANRequestBody()
        {
        }
        
        public bpRefundToPANRequestBody(string user, string password, long pan, long amount, long saleReferenceId, long terminalId)
        {
            this.user = user;
            this.password = password;
            this.pan = pan;
            this.amount = amount;
            this.saleReferenceId = saleReferenceId;
            this.terminalId = terminalId;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class bpRefundToPANRequestResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="bpRefundToPANRequestResponse", Namespace="http://interfaces.core.sw.bps.com/", Order=0)]
        public IPGMellatService.bpRefundToPANRequestResponseBody Body;
        
        public bpRefundToPANRequestResponse()
        {
        }
        
        public bpRefundToPANRequestResponse(IPGMellatService.bpRefundToPANRequestResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class bpRefundToPANRequestResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string @return;
        
        public bpRefundToPANRequestResponseBody()
        {
        }
        
        public bpRefundToPANRequestResponseBody(string @return)
        {
            this.@return = @return;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class bpVerifyRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="bpVerifyRequest", Namespace="http://interfaces.core.sw.bps.com/", Order=0)]
        public IPGMellatService.bpVerifyRequestBody Body;
        
        public bpVerifyRequest()
        {
        }
        
        public bpVerifyRequest(IPGMellatService.bpVerifyRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class bpVerifyRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public long terminalId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string userName;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string userPassword;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public long orderId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public long saleOrderId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public long saleReferenceId;
        
        public bpVerifyRequestBody()
        {
        }
        
        public bpVerifyRequestBody(long terminalId, string userName, string userPassword, long orderId, long saleOrderId, long saleReferenceId)
        {
            this.terminalId = terminalId;
            this.userName = userName;
            this.userPassword = userPassword;
            this.orderId = orderId;
            this.saleOrderId = saleOrderId;
            this.saleReferenceId = saleReferenceId;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class bpVerifyRequestResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="bpVerifyRequestResponse", Namespace="http://interfaces.core.sw.bps.com/", Order=0)]
        public IPGMellatService.bpVerifyRequestResponseBody Body;
        
        public bpVerifyRequestResponse()
        {
        }
        
        public bpVerifyRequestResponse(IPGMellatService.bpVerifyRequestResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class bpVerifyRequestResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string @return;
        
        public bpVerifyRequestResponseBody()
        {
        }
        
        public bpVerifyRequestResponseBody(string @return)
        {
            this.@return = @return;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class bpRefundVerifyRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="bpRefundVerifyRequest", Namespace="http://interfaces.core.sw.bps.com/", Order=0)]
        public IPGMellatService.bpRefundVerifyRequestBody Body;
        
        public bpRefundVerifyRequest()
        {
        }
        
        public bpRefundVerifyRequest(IPGMellatService.bpRefundVerifyRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class bpRefundVerifyRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public long terminalId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string userName;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string userPassword;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public long orderId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public long refundOrderId;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public long refundReferenceId;
        
        public bpRefundVerifyRequestBody()
        {
        }
        
        public bpRefundVerifyRequestBody(long terminalId, string userName, string userPassword, long orderId, long refundOrderId, long refundReferenceId)
        {
            this.terminalId = terminalId;
            this.userName = userName;
            this.userPassword = userPassword;
            this.orderId = orderId;
            this.refundOrderId = refundOrderId;
            this.refundReferenceId = refundReferenceId;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class bpRefundVerifyRequestResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="bpRefundVerifyRequestResponse", Namespace="http://interfaces.core.sw.bps.com/", Order=0)]
        public IPGMellatService.bpRefundVerifyRequestResponseBody Body;
        
        public bpRefundVerifyRequestResponse()
        {
        }
        
        public bpRefundVerifyRequestResponse(IPGMellatService.bpRefundVerifyRequestResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class bpRefundVerifyRequestResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string @return;
        
        public bpRefundVerifyRequestResponseBody()
        {
        }
        
        public bpRefundVerifyRequestResponseBody(string @return)
        {
            this.@return = @return;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public interface IPaymentGatewayChannel : IPGMellatService.IPaymentGateway, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public partial class PaymentGatewayClient : System.ServiceModel.ClientBase<IPGMellatService.IPaymentGateway>, IPGMellatService.IPaymentGateway
    {
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public PaymentGatewayClient() : 
                base(PaymentGatewayClient.GetDefaultBinding(), PaymentGatewayClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.PaymentGatewayImplPort.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public PaymentGatewayClient(EndpointConfiguration endpointConfiguration) : 
                base(PaymentGatewayClient.GetBindingForEndpoint(endpointConfiguration), PaymentGatewayClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public PaymentGatewayClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(PaymentGatewayClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public PaymentGatewayClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(PaymentGatewayClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public PaymentGatewayClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        IPGMellatService.bpRefundRequestResponse IPGMellatService.IPaymentGateway.bpRefundRequest(IPGMellatService.bpRefundRequest request)
        {
            return base.Channel.bpRefundRequest(request);
        }
        
        public string bpRefundRequest(long terminalId, string userName, string userPassword, long orderId, long saleOrderId, long saleReferenceId, long refundAmount)
        {
            IPGMellatService.bpRefundRequest inValue = new IPGMellatService.bpRefundRequest();
            inValue.Body = new IPGMellatService.bpRefundRequestBody();
            inValue.Body.terminalId = terminalId;
            inValue.Body.userName = userName;
            inValue.Body.userPassword = userPassword;
            inValue.Body.orderId = orderId;
            inValue.Body.saleOrderId = saleOrderId;
            inValue.Body.saleReferenceId = saleReferenceId;
            inValue.Body.refundAmount = refundAmount;
            IPGMellatService.bpRefundRequestResponse retVal = ((IPGMellatService.IPaymentGateway)(this)).bpRefundRequest(inValue);
            return retVal.Body.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<IPGMellatService.bpRefundRequestResponse> IPGMellatService.IPaymentGateway.bpRefundRequestAsync(IPGMellatService.bpRefundRequest request)
        {
            return base.Channel.bpRefundRequestAsync(request);
        }
        
        public System.Threading.Tasks.Task<IPGMellatService.bpRefundRequestResponse> bpRefundRequestAsync(long terminalId, string userName, string userPassword, long orderId, long saleOrderId, long saleReferenceId, long refundAmount)
        {
            IPGMellatService.bpRefundRequest inValue = new IPGMellatService.bpRefundRequest();
            inValue.Body = new IPGMellatService.bpRefundRequestBody();
            inValue.Body.terminalId = terminalId;
            inValue.Body.userName = userName;
            inValue.Body.userPassword = userPassword;
            inValue.Body.orderId = orderId;
            inValue.Body.saleOrderId = saleOrderId;
            inValue.Body.saleReferenceId = saleReferenceId;
            inValue.Body.refundAmount = refundAmount;
            return ((IPGMellatService.IPaymentGateway)(this)).bpRefundRequestAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        IPGMellatService.bpSaleReferenceIdRequestResponse IPGMellatService.IPaymentGateway.bpSaleReferenceIdRequest(IPGMellatService.bpSaleReferenceIdRequest request)
        {
            return base.Channel.bpSaleReferenceIdRequest(request);
        }
        
        public string bpSaleReferenceIdRequest(long terminalId, string userName, string userPassword, long orderId, long saleOrderId)
        {
            IPGMellatService.bpSaleReferenceIdRequest inValue = new IPGMellatService.bpSaleReferenceIdRequest();
            inValue.Body = new IPGMellatService.bpSaleReferenceIdRequestBody();
            inValue.Body.terminalId = terminalId;
            inValue.Body.userName = userName;
            inValue.Body.userPassword = userPassword;
            inValue.Body.orderId = orderId;
            inValue.Body.saleOrderId = saleOrderId;
            IPGMellatService.bpSaleReferenceIdRequestResponse retVal = ((IPGMellatService.IPaymentGateway)(this)).bpSaleReferenceIdRequest(inValue);
            return retVal.Body.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<IPGMellatService.bpSaleReferenceIdRequestResponse> IPGMellatService.IPaymentGateway.bpSaleReferenceIdRequestAsync(IPGMellatService.bpSaleReferenceIdRequest request)
        {
            return base.Channel.bpSaleReferenceIdRequestAsync(request);
        }
        
        public System.Threading.Tasks.Task<IPGMellatService.bpSaleReferenceIdRequestResponse> bpSaleReferenceIdRequestAsync(long terminalId, string userName, string userPassword, long orderId, long saleOrderId)
        {
            IPGMellatService.bpSaleReferenceIdRequest inValue = new IPGMellatService.bpSaleReferenceIdRequest();
            inValue.Body = new IPGMellatService.bpSaleReferenceIdRequestBody();
            inValue.Body.terminalId = terminalId;
            inValue.Body.userName = userName;
            inValue.Body.userPassword = userPassword;
            inValue.Body.orderId = orderId;
            inValue.Body.saleOrderId = saleOrderId;
            return ((IPGMellatService.IPaymentGateway)(this)).bpSaleReferenceIdRequestAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        IPGMellatService.bpDynamicPayRequestResponse IPGMellatService.IPaymentGateway.bpDynamicPayRequest(IPGMellatService.bpDynamicPayRequest request)
        {
            return base.Channel.bpDynamicPayRequest(request);
        }
        
        public string bpDynamicPayRequest(
                    long terminalId, 
                    string userName, 
                    string userPassword, 
                    long orderId, 
                    long amount, 
                    string localDate, 
                    string localTime, 
                    string additionalData, 
                    string callBackUrl, 
                    string payerId, 
                    long subServiceId, 
                    string mobileNo, 
                    string encPan, 
                    string panHiddenMode, 
                    string cartItem, 
                    string enc)
        {
            IPGMellatService.bpDynamicPayRequest inValue = new IPGMellatService.bpDynamicPayRequest();
            inValue.Body = new IPGMellatService.bpDynamicPayRequestBody();
            inValue.Body.terminalId = terminalId;
            inValue.Body.userName = userName;
            inValue.Body.userPassword = userPassword;
            inValue.Body.orderId = orderId;
            inValue.Body.amount = amount;
            inValue.Body.localDate = localDate;
            inValue.Body.localTime = localTime;
            inValue.Body.additionalData = additionalData;
            inValue.Body.callBackUrl = callBackUrl;
            inValue.Body.payerId = payerId;
            inValue.Body.subServiceId = subServiceId;
            inValue.Body.mobileNo = mobileNo;
            inValue.Body.encPan = encPan;
            inValue.Body.panHiddenMode = panHiddenMode;
            inValue.Body.cartItem = cartItem;
            inValue.Body.enc = enc;
            IPGMellatService.bpDynamicPayRequestResponse retVal = ((IPGMellatService.IPaymentGateway)(this)).bpDynamicPayRequest(inValue);
            return retVal.Body.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<IPGMellatService.bpDynamicPayRequestResponse> IPGMellatService.IPaymentGateway.bpDynamicPayRequestAsync(IPGMellatService.bpDynamicPayRequest request)
        {
            return base.Channel.bpDynamicPayRequestAsync(request);
        }
        
        public System.Threading.Tasks.Task<IPGMellatService.bpDynamicPayRequestResponse> bpDynamicPayRequestAsync(
                    long terminalId, 
                    string userName, 
                    string userPassword, 
                    long orderId, 
                    long amount, 
                    string localDate, 
                    string localTime, 
                    string additionalData, 
                    string callBackUrl, 
                    string payerId, 
                    long subServiceId, 
                    string mobileNo, 
                    string encPan, 
                    string panHiddenMode, 
                    string cartItem, 
                    string enc)
        {
            IPGMellatService.bpDynamicPayRequest inValue = new IPGMellatService.bpDynamicPayRequest();
            inValue.Body = new IPGMellatService.bpDynamicPayRequestBody();
            inValue.Body.terminalId = terminalId;
            inValue.Body.userName = userName;
            inValue.Body.userPassword = userPassword;
            inValue.Body.orderId = orderId;
            inValue.Body.amount = amount;
            inValue.Body.localDate = localDate;
            inValue.Body.localTime = localTime;
            inValue.Body.additionalData = additionalData;
            inValue.Body.callBackUrl = callBackUrl;
            inValue.Body.payerId = payerId;
            inValue.Body.subServiceId = subServiceId;
            inValue.Body.mobileNo = mobileNo;
            inValue.Body.encPan = encPan;
            inValue.Body.panHiddenMode = panHiddenMode;
            inValue.Body.cartItem = cartItem;
            inValue.Body.enc = enc;
            return ((IPGMellatService.IPaymentGateway)(this)).bpDynamicPayRequestAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        IPGMellatService.bpInquiryRequestResponse IPGMellatService.IPaymentGateway.bpInquiryRequest(IPGMellatService.bpInquiryRequest request)
        {
            return base.Channel.bpInquiryRequest(request);
        }
        
        public string bpInquiryRequest(long terminalId, string userName, string userPassword, long orderId, long saleOrderId, long saleReferenceId)
        {
            IPGMellatService.bpInquiryRequest inValue = new IPGMellatService.bpInquiryRequest();
            inValue.Body = new IPGMellatService.bpInquiryRequestBody();
            inValue.Body.terminalId = terminalId;
            inValue.Body.userName = userName;
            inValue.Body.userPassword = userPassword;
            inValue.Body.orderId = orderId;
            inValue.Body.saleOrderId = saleOrderId;
            inValue.Body.saleReferenceId = saleReferenceId;
            IPGMellatService.bpInquiryRequestResponse retVal = ((IPGMellatService.IPaymentGateway)(this)).bpInquiryRequest(inValue);
            return retVal.Body.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<IPGMellatService.bpInquiryRequestResponse> IPGMellatService.IPaymentGateway.bpInquiryRequestAsync(IPGMellatService.bpInquiryRequest request)
        {
            return base.Channel.bpInquiryRequestAsync(request);
        }
        
        public System.Threading.Tasks.Task<IPGMellatService.bpInquiryRequestResponse> bpInquiryRequestAsync(long terminalId, string userName, string userPassword, long orderId, long saleOrderId, long saleReferenceId)
        {
            IPGMellatService.bpInquiryRequest inValue = new IPGMellatService.bpInquiryRequest();
            inValue.Body = new IPGMellatService.bpInquiryRequestBody();
            inValue.Body.terminalId = terminalId;
            inValue.Body.userName = userName;
            inValue.Body.userPassword = userPassword;
            inValue.Body.orderId = orderId;
            inValue.Body.saleOrderId = saleOrderId;
            inValue.Body.saleReferenceId = saleReferenceId;
            return ((IPGMellatService.IPaymentGateway)(this)).bpInquiryRequestAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        IPGMellatService.bpSettleRequestResponse IPGMellatService.IPaymentGateway.bpSettleRequest(IPGMellatService.bpSettleRequest request)
        {
            return base.Channel.bpSettleRequest(request);
        }
        
        public string bpSettleRequest(long terminalId, string userName, string userPassword, long orderId, long saleOrderId, long saleReferenceId)
        {
            IPGMellatService.bpSettleRequest inValue = new IPGMellatService.bpSettleRequest();
            inValue.Body = new IPGMellatService.bpSettleRequestBody();
            inValue.Body.terminalId = terminalId;
            inValue.Body.userName = userName;
            inValue.Body.userPassword = userPassword;
            inValue.Body.orderId = orderId;
            inValue.Body.saleOrderId = saleOrderId;
            inValue.Body.saleReferenceId = saleReferenceId;
            IPGMellatService.bpSettleRequestResponse retVal = ((IPGMellatService.IPaymentGateway)(this)).bpSettleRequest(inValue);
            return retVal.Body.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<IPGMellatService.bpSettleRequestResponse> IPGMellatService.IPaymentGateway.bpSettleRequestAsync(IPGMellatService.bpSettleRequest request)
        {
            return base.Channel.bpSettleRequestAsync(request);
        }
        
        public System.Threading.Tasks.Task<IPGMellatService.bpSettleRequestResponse> bpSettleRequestAsync(long terminalId, string userName, string userPassword, long orderId, long saleOrderId, long saleReferenceId)
        {
            IPGMellatService.bpSettleRequest inValue = new IPGMellatService.bpSettleRequest();
            inValue.Body = new IPGMellatService.bpSettleRequestBody();
            inValue.Body.terminalId = terminalId;
            inValue.Body.userName = userName;
            inValue.Body.userPassword = userPassword;
            inValue.Body.orderId = orderId;
            inValue.Body.saleOrderId = saleOrderId;
            inValue.Body.saleReferenceId = saleReferenceId;
            return ((IPGMellatService.IPaymentGateway)(this)).bpSettleRequestAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        IPGMellatService.bpChargePayRequestResponse IPGMellatService.IPaymentGateway.bpChargePayRequest(IPGMellatService.bpChargePayRequest request)
        {
            return base.Channel.bpChargePayRequest(request);
        }
        
        public string bpChargePayRequest(long terminalId, string userName, string userPassword, long orderId, long amount, string localDate, string localTime, string additionalData, string callBackUrl, string payerId)
        {
            IPGMellatService.bpChargePayRequest inValue = new IPGMellatService.bpChargePayRequest();
            inValue.Body = new IPGMellatService.bpChargePayRequestBody();
            inValue.Body.terminalId = terminalId;
            inValue.Body.userName = userName;
            inValue.Body.userPassword = userPassword;
            inValue.Body.orderId = orderId;
            inValue.Body.amount = amount;
            inValue.Body.localDate = localDate;
            inValue.Body.localTime = localTime;
            inValue.Body.additionalData = additionalData;
            inValue.Body.callBackUrl = callBackUrl;
            inValue.Body.payerId = payerId;
            IPGMellatService.bpChargePayRequestResponse retVal = ((IPGMellatService.IPaymentGateway)(this)).bpChargePayRequest(inValue);
            return retVal.Body.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<IPGMellatService.bpChargePayRequestResponse> IPGMellatService.IPaymentGateway.bpChargePayRequestAsync(IPGMellatService.bpChargePayRequest request)
        {
            return base.Channel.bpChargePayRequestAsync(request);
        }
        
        public System.Threading.Tasks.Task<IPGMellatService.bpChargePayRequestResponse> bpChargePayRequestAsync(long terminalId, string userName, string userPassword, long orderId, long amount, string localDate, string localTime, string additionalData, string callBackUrl, string payerId)
        {
            IPGMellatService.bpChargePayRequest inValue = new IPGMellatService.bpChargePayRequest();
            inValue.Body = new IPGMellatService.bpChargePayRequestBody();
            inValue.Body.terminalId = terminalId;
            inValue.Body.userName = userName;
            inValue.Body.userPassword = userPassword;
            inValue.Body.orderId = orderId;
            inValue.Body.amount = amount;
            inValue.Body.localDate = localDate;
            inValue.Body.localTime = localTime;
            inValue.Body.additionalData = additionalData;
            inValue.Body.callBackUrl = callBackUrl;
            inValue.Body.payerId = payerId;
            return ((IPGMellatService.IPaymentGateway)(this)).bpChargePayRequestAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        IPGMellatService.bpCumulativeDynamicPayRequestResponse IPGMellatService.IPaymentGateway.bpCumulativeDynamicPayRequest(IPGMellatService.bpCumulativeDynamicPayRequest request)
        {
            return base.Channel.bpCumulativeDynamicPayRequest(request);
        }
        
        public string bpCumulativeDynamicPayRequest(long terminalId, string userName, string userPassword, long orderId, long amount, string localDate, string localTime, string additionalData, string callBackUrl, string mobileNo, string encPan, string panHiddenMode, string cartItem, string enc)
        {
            IPGMellatService.bpCumulativeDynamicPayRequest inValue = new IPGMellatService.bpCumulativeDynamicPayRequest();
            inValue.Body = new IPGMellatService.bpCumulativeDynamicPayRequestBody();
            inValue.Body.terminalId = terminalId;
            inValue.Body.userName = userName;
            inValue.Body.userPassword = userPassword;
            inValue.Body.orderId = orderId;
            inValue.Body.amount = amount;
            inValue.Body.localDate = localDate;
            inValue.Body.localTime = localTime;
            inValue.Body.additionalData = additionalData;
            inValue.Body.callBackUrl = callBackUrl;
            inValue.Body.mobileNo = mobileNo;
            inValue.Body.encPan = encPan;
            inValue.Body.panHiddenMode = panHiddenMode;
            inValue.Body.cartItem = cartItem;
            inValue.Body.enc = enc;
            IPGMellatService.bpCumulativeDynamicPayRequestResponse retVal = ((IPGMellatService.IPaymentGateway)(this)).bpCumulativeDynamicPayRequest(inValue);
            return retVal.Body.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<IPGMellatService.bpCumulativeDynamicPayRequestResponse> IPGMellatService.IPaymentGateway.bpCumulativeDynamicPayRequestAsync(IPGMellatService.bpCumulativeDynamicPayRequest request)
        {
            return base.Channel.bpCumulativeDynamicPayRequestAsync(request);
        }
        
        public System.Threading.Tasks.Task<IPGMellatService.bpCumulativeDynamicPayRequestResponse> bpCumulativeDynamicPayRequestAsync(long terminalId, string userName, string userPassword, long orderId, long amount, string localDate, string localTime, string additionalData, string callBackUrl, string mobileNo, string encPan, string panHiddenMode, string cartItem, string enc)
        {
            IPGMellatService.bpCumulativeDynamicPayRequest inValue = new IPGMellatService.bpCumulativeDynamicPayRequest();
            inValue.Body = new IPGMellatService.bpCumulativeDynamicPayRequestBody();
            inValue.Body.terminalId = terminalId;
            inValue.Body.userName = userName;
            inValue.Body.userPassword = userPassword;
            inValue.Body.orderId = orderId;
            inValue.Body.amount = amount;
            inValue.Body.localDate = localDate;
            inValue.Body.localTime = localTime;
            inValue.Body.additionalData = additionalData;
            inValue.Body.callBackUrl = callBackUrl;
            inValue.Body.mobileNo = mobileNo;
            inValue.Body.encPan = encPan;
            inValue.Body.panHiddenMode = panHiddenMode;
            inValue.Body.cartItem = cartItem;
            inValue.Body.enc = enc;
            return ((IPGMellatService.IPaymentGateway)(this)).bpCumulativeDynamicPayRequestAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        IPGMellatService.bpPayRequestResponse IPGMellatService.IPaymentGateway.bpPayRequest(IPGMellatService.bpPayRequest request)
        {
            return base.Channel.bpPayRequest(request);
        }
        
        public string bpPayRequest(long terminalId, string userName, string userPassword, long orderId, long amount, string localDate, string localTime, string additionalData, string callBackUrl, string payerId, string mobileNo, string encPan, string panHiddenMode, string cartItem, string enc)
        {
            IPGMellatService.bpPayRequest inValue = new IPGMellatService.bpPayRequest();
            inValue.Body = new IPGMellatService.bpPayRequestBody();
            inValue.Body.terminalId = terminalId;
            inValue.Body.userName = userName;
            inValue.Body.userPassword = userPassword;
            inValue.Body.orderId = orderId;
            inValue.Body.amount = amount;
            inValue.Body.localDate = localDate;
            inValue.Body.localTime = localTime;
            inValue.Body.additionalData = additionalData;
            inValue.Body.callBackUrl = callBackUrl;
            inValue.Body.payerId = payerId;
            inValue.Body.mobileNo = mobileNo;
            inValue.Body.encPan = encPan;
            inValue.Body.panHiddenMode = panHiddenMode;
            inValue.Body.cartItem = cartItem;
            inValue.Body.enc = enc;
            IPGMellatService.bpPayRequestResponse retVal = ((IPGMellatService.IPaymentGateway)(this)).bpPayRequest(inValue);
            return retVal.Body.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<IPGMellatService.bpPayRequestResponse> IPGMellatService.IPaymentGateway.bpPayRequestAsync(IPGMellatService.bpPayRequest request)
        {
            return base.Channel.bpPayRequestAsync(request);
        }
        
        public System.Threading.Tasks.Task<IPGMellatService.bpPayRequestResponse> bpPayRequestAsync(long terminalId, string userName, string userPassword, long orderId, long amount, string localDate, string localTime, string additionalData, string callBackUrl, string payerId, string mobileNo, string encPan, string panHiddenMode, string cartItem, string enc)
        {
            IPGMellatService.bpPayRequest inValue = new IPGMellatService.bpPayRequest();
            inValue.Body = new IPGMellatService.bpPayRequestBody();
            inValue.Body.terminalId = terminalId;
            inValue.Body.userName = userName;
            inValue.Body.userPassword = userPassword;
            inValue.Body.orderId = orderId;
            inValue.Body.amount = amount;
            inValue.Body.localDate = localDate;
            inValue.Body.localTime = localTime;
            inValue.Body.additionalData = additionalData;
            inValue.Body.callBackUrl = callBackUrl;
            inValue.Body.payerId = payerId;
            inValue.Body.mobileNo = mobileNo;
            inValue.Body.encPan = encPan;
            inValue.Body.panHiddenMode = panHiddenMode;
            inValue.Body.cartItem = cartItem;
            inValue.Body.enc = enc;
            return ((IPGMellatService.IPaymentGateway)(this)).bpPayRequestAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        IPGMellatService.bpRefundInquiryRequestResponse IPGMellatService.IPaymentGateway.bpRefundInquiryRequest(IPGMellatService.bpRefundInquiryRequest request)
        {
            return base.Channel.bpRefundInquiryRequest(request);
        }
        
        public string bpRefundInquiryRequest(long terminalId, string userName, string userPassword, long orderId, long refundOrderId, long refundReferenceId)
        {
            IPGMellatService.bpRefundInquiryRequest inValue = new IPGMellatService.bpRefundInquiryRequest();
            inValue.Body = new IPGMellatService.bpRefundInquiryRequestBody();
            inValue.Body.terminalId = terminalId;
            inValue.Body.userName = userName;
            inValue.Body.userPassword = userPassword;
            inValue.Body.orderId = orderId;
            inValue.Body.refundOrderId = refundOrderId;
            inValue.Body.refundReferenceId = refundReferenceId;
            IPGMellatService.bpRefundInquiryRequestResponse retVal = ((IPGMellatService.IPaymentGateway)(this)).bpRefundInquiryRequest(inValue);
            return retVal.Body.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<IPGMellatService.bpRefundInquiryRequestResponse> IPGMellatService.IPaymentGateway.bpRefundInquiryRequestAsync(IPGMellatService.bpRefundInquiryRequest request)
        {
            return base.Channel.bpRefundInquiryRequestAsync(request);
        }
        
        public System.Threading.Tasks.Task<IPGMellatService.bpRefundInquiryRequestResponse> bpRefundInquiryRequestAsync(long terminalId, string userName, string userPassword, long orderId, long refundOrderId, long refundReferenceId)
        {
            IPGMellatService.bpRefundInquiryRequest inValue = new IPGMellatService.bpRefundInquiryRequest();
            inValue.Body = new IPGMellatService.bpRefundInquiryRequestBody();
            inValue.Body.terminalId = terminalId;
            inValue.Body.userName = userName;
            inValue.Body.userPassword = userPassword;
            inValue.Body.orderId = orderId;
            inValue.Body.refundOrderId = refundOrderId;
            inValue.Body.refundReferenceId = refundReferenceId;
            return ((IPGMellatService.IPaymentGateway)(this)).bpRefundInquiryRequestAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        IPGMellatService.bpReversalRequestResponse IPGMellatService.IPaymentGateway.bpReversalRequest(IPGMellatService.bpReversalRequest request)
        {
            return base.Channel.bpReversalRequest(request);
        }
        
        public string bpReversalRequest(long terminalId, string userName, string userPassword, long orderId, long saleOrderId, long saleReferenceId)
        {
            IPGMellatService.bpReversalRequest inValue = new IPGMellatService.bpReversalRequest();
            inValue.Body = new IPGMellatService.bpReversalRequestBody();
            inValue.Body.terminalId = terminalId;
            inValue.Body.userName = userName;
            inValue.Body.userPassword = userPassword;
            inValue.Body.orderId = orderId;
            inValue.Body.saleOrderId = saleOrderId;
            inValue.Body.saleReferenceId = saleReferenceId;
            IPGMellatService.bpReversalRequestResponse retVal = ((IPGMellatService.IPaymentGateway)(this)).bpReversalRequest(inValue);
            return retVal.Body.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<IPGMellatService.bpReversalRequestResponse> IPGMellatService.IPaymentGateway.bpReversalRequestAsync(IPGMellatService.bpReversalRequest request)
        {
            return base.Channel.bpReversalRequestAsync(request);
        }
        
        public System.Threading.Tasks.Task<IPGMellatService.bpReversalRequestResponse> bpReversalRequestAsync(long terminalId, string userName, string userPassword, long orderId, long saleOrderId, long saleReferenceId)
        {
            IPGMellatService.bpReversalRequest inValue = new IPGMellatService.bpReversalRequest();
            inValue.Body = new IPGMellatService.bpReversalRequestBody();
            inValue.Body.terminalId = terminalId;
            inValue.Body.userName = userName;
            inValue.Body.userPassword = userPassword;
            inValue.Body.orderId = orderId;
            inValue.Body.saleOrderId = saleOrderId;
            inValue.Body.saleReferenceId = saleReferenceId;
            return ((IPGMellatService.IPaymentGateway)(this)).bpReversalRequestAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        IPGMellatService.bpPosRefundRequestResponse IPGMellatService.IPaymentGateway.bpPosRefundRequest(IPGMellatService.bpPosRefundRequest request)
        {
            return base.Channel.bpPosRefundRequest(request);
        }
        
        public string bpPosRefundRequest(string user, string password, long saleReferenceId, long refundAmount)
        {
            IPGMellatService.bpPosRefundRequest inValue = new IPGMellatService.bpPosRefundRequest();
            inValue.Body = new IPGMellatService.bpPosRefundRequestBody();
            inValue.Body.user = user;
            inValue.Body.password = password;
            inValue.Body.saleReferenceId = saleReferenceId;
            inValue.Body.refundAmount = refundAmount;
            IPGMellatService.bpPosRefundRequestResponse retVal = ((IPGMellatService.IPaymentGateway)(this)).bpPosRefundRequest(inValue);
            return retVal.Body.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<IPGMellatService.bpPosRefundRequestResponse> IPGMellatService.IPaymentGateway.bpPosRefundRequestAsync(IPGMellatService.bpPosRefundRequest request)
        {
            return base.Channel.bpPosRefundRequestAsync(request);
        }
        
        public System.Threading.Tasks.Task<IPGMellatService.bpPosRefundRequestResponse> bpPosRefundRequestAsync(string user, string password, long saleReferenceId, long refundAmount)
        {
            IPGMellatService.bpPosRefundRequest inValue = new IPGMellatService.bpPosRefundRequest();
            inValue.Body = new IPGMellatService.bpPosRefundRequestBody();
            inValue.Body.user = user;
            inValue.Body.password = password;
            inValue.Body.saleReferenceId = saleReferenceId;
            inValue.Body.refundAmount = refundAmount;
            return ((IPGMellatService.IPaymentGateway)(this)).bpPosRefundRequestAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        IPGMellatService.bpRefundToPANRequestResponse IPGMellatService.IPaymentGateway.bpRefundToPANRequest(IPGMellatService.bpRefundToPANRequest request)
        {
            return base.Channel.bpRefundToPANRequest(request);
        }
        
        public string bpRefundToPANRequest(string user, string password, long pan, long amount, long saleReferenceId, long terminalId)
        {
            IPGMellatService.bpRefundToPANRequest inValue = new IPGMellatService.bpRefundToPANRequest();
            inValue.Body = new IPGMellatService.bpRefundToPANRequestBody();
            inValue.Body.user = user;
            inValue.Body.password = password;
            inValue.Body.pan = pan;
            inValue.Body.amount = amount;
            inValue.Body.saleReferenceId = saleReferenceId;
            inValue.Body.terminalId = terminalId;
            IPGMellatService.bpRefundToPANRequestResponse retVal = ((IPGMellatService.IPaymentGateway)(this)).bpRefundToPANRequest(inValue);
            return retVal.Body.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<IPGMellatService.bpRefundToPANRequestResponse> IPGMellatService.IPaymentGateway.bpRefundToPANRequestAsync(IPGMellatService.bpRefundToPANRequest request)
        {
            return base.Channel.bpRefundToPANRequestAsync(request);
        }
        
        public System.Threading.Tasks.Task<IPGMellatService.bpRefundToPANRequestResponse> bpRefundToPANRequestAsync(string user, string password, long pan, long amount, long saleReferenceId, long terminalId)
        {
            IPGMellatService.bpRefundToPANRequest inValue = new IPGMellatService.bpRefundToPANRequest();
            inValue.Body = new IPGMellatService.bpRefundToPANRequestBody();
            inValue.Body.user = user;
            inValue.Body.password = password;
            inValue.Body.pan = pan;
            inValue.Body.amount = amount;
            inValue.Body.saleReferenceId = saleReferenceId;
            inValue.Body.terminalId = terminalId;
            return ((IPGMellatService.IPaymentGateway)(this)).bpRefundToPANRequestAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        IPGMellatService.bpVerifyRequestResponse IPGMellatService.IPaymentGateway.bpVerifyRequest(IPGMellatService.bpVerifyRequest request)
        {
            return base.Channel.bpVerifyRequest(request);
        }
        
        public string bpVerifyRequest(long terminalId, string userName, string userPassword, long orderId, long saleOrderId, long saleReferenceId)
        {
            IPGMellatService.bpVerifyRequest inValue = new IPGMellatService.bpVerifyRequest();
            inValue.Body = new IPGMellatService.bpVerifyRequestBody();
            inValue.Body.terminalId = terminalId;
            inValue.Body.userName = userName;
            inValue.Body.userPassword = userPassword;
            inValue.Body.orderId = orderId;
            inValue.Body.saleOrderId = saleOrderId;
            inValue.Body.saleReferenceId = saleReferenceId;
            IPGMellatService.bpVerifyRequestResponse retVal = ((IPGMellatService.IPaymentGateway)(this)).bpVerifyRequest(inValue);
            return retVal.Body.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<IPGMellatService.bpVerifyRequestResponse> IPGMellatService.IPaymentGateway.bpVerifyRequestAsync(IPGMellatService.bpVerifyRequest request)
        {
            return base.Channel.bpVerifyRequestAsync(request);
        }
        
        public System.Threading.Tasks.Task<IPGMellatService.bpVerifyRequestResponse> bpVerifyRequestAsync(long terminalId, string userName, string userPassword, long orderId, long saleOrderId, long saleReferenceId)
        {
            IPGMellatService.bpVerifyRequest inValue = new IPGMellatService.bpVerifyRequest();
            inValue.Body = new IPGMellatService.bpVerifyRequestBody();
            inValue.Body.terminalId = terminalId;
            inValue.Body.userName = userName;
            inValue.Body.userPassword = userPassword;
            inValue.Body.orderId = orderId;
            inValue.Body.saleOrderId = saleOrderId;
            inValue.Body.saleReferenceId = saleReferenceId;
            return ((IPGMellatService.IPaymentGateway)(this)).bpVerifyRequestAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        IPGMellatService.bpRefundVerifyRequestResponse IPGMellatService.IPaymentGateway.bpRefundVerifyRequest(IPGMellatService.bpRefundVerifyRequest request)
        {
            return base.Channel.bpRefundVerifyRequest(request);
        }
        
        public string bpRefundVerifyRequest(long terminalId, string userName, string userPassword, long orderId, long refundOrderId, long refundReferenceId)
        {
            IPGMellatService.bpRefundVerifyRequest inValue = new IPGMellatService.bpRefundVerifyRequest();
            inValue.Body = new IPGMellatService.bpRefundVerifyRequestBody();
            inValue.Body.terminalId = terminalId;
            inValue.Body.userName = userName;
            inValue.Body.userPassword = userPassword;
            inValue.Body.orderId = orderId;
            inValue.Body.refundOrderId = refundOrderId;
            inValue.Body.refundReferenceId = refundReferenceId;
            IPGMellatService.bpRefundVerifyRequestResponse retVal = ((IPGMellatService.IPaymentGateway)(this)).bpRefundVerifyRequest(inValue);
            return retVal.Body.@return;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<IPGMellatService.bpRefundVerifyRequestResponse> IPGMellatService.IPaymentGateway.bpRefundVerifyRequestAsync(IPGMellatService.bpRefundVerifyRequest request)
        {
            return base.Channel.bpRefundVerifyRequestAsync(request);
        }
        
        public System.Threading.Tasks.Task<IPGMellatService.bpRefundVerifyRequestResponse> bpRefundVerifyRequestAsync(long terminalId, string userName, string userPassword, long orderId, long refundOrderId, long refundReferenceId)
        {
            IPGMellatService.bpRefundVerifyRequest inValue = new IPGMellatService.bpRefundVerifyRequest();
            inValue.Body = new IPGMellatService.bpRefundVerifyRequestBody();
            inValue.Body.terminalId = terminalId;
            inValue.Body.userName = userName;
            inValue.Body.userPassword = userPassword;
            inValue.Body.orderId = orderId;
            inValue.Body.refundOrderId = refundOrderId;
            inValue.Body.refundReferenceId = refundReferenceId;
            return ((IPGMellatService.IPaymentGateway)(this)).bpRefundVerifyRequestAsync(inValue);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.PaymentGatewayImplPort))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                result.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.Transport;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.PaymentGatewayImplPort))
            {
                return new System.ServiceModel.EndpointAddress("https://bpm.shaparak.ir/pgwchannel/services/pgw");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return PaymentGatewayClient.GetBindingForEndpoint(EndpointConfiguration.PaymentGatewayImplPort);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return PaymentGatewayClient.GetEndpointAddress(EndpointConfiguration.PaymentGatewayImplPort);
        }
        
        public enum EndpointConfiguration
        {
            
            PaymentGatewayImplPort,
        }
    }
}

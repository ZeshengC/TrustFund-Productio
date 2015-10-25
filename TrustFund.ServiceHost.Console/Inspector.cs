using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace TrustFund.ServiceHost
{
    public class Inspector: IDispatchMessageInspector
    {
        public string Request { get; set; }
        public string Response { get; set; }

        public event EventHandler<InspectorEventArgs> RaiseRequestReveived;
        public event EventHandler<InspectorEventArgs> RaiseSendingReply;




        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel, System.ServiceModel.InstanceContext instanceContext)
        {
            Request = request.ToString();
            OnRaiseRequestReceived(Request);
            return null;
        }

        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            Response = reply.ToString();
            OnRaiseSendingReply(Response);
        }

        protected void OnRaiseRequestReceived(string message)
        {
            EventHandler<InspectorEventArgs> handler = RaiseRequestReveived;
            if(handler != null)
            {
                handler(this, new InspectorEventArgs(message));
            }

        }

        protected void OnRaiseSendingReply(string message)
        {
            EventHandler<InspectorEventArgs> handler = RaiseSendingReply;
            if(handler != null)
            {
                handler(this, new InspectorEventArgs(message));
            }
        }
    }

    public class InspectorEventArgs:EventArgs
    {
        public InspectorEventArgs(string message)
        {
            this.Message = message;
        }
        public string Message{get;set;}
    }
}

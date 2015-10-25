using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace Core.Common.ServiceModel
{
    public class ClientMessageInspector: IClientMessageInspector
    {
        public ClientMessageInspector(string username)
        {
            _username = username;
        }
        private string _username;
        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            
        }

        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
        {
            MessageHeader<string> header = new MessageHeader<string>(_username);
            request.Headers.Add(header.GetUntypedHeader("String", "System"));
            return null;
        }
    }
}

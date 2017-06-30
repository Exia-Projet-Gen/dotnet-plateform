using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using WCFInterfaces;

namespace Stoper.Controllers
{
    public class ComWCF
    {
        static private ChannelFactory<IServices> channelFactory = null;
        static private IServices services = null;
        static private string tokenApp = "l{8W9Fs1p5hz;K6m.gx(vAr)BbkYHIgkH!$1rgtiUtA$BAcdXhUMOY:!5<0L62W";

        public ComWCF()
        {
            channelFactory = new ChannelFactory<IServices>("tcpConfig");

            services = channelFactory.CreateChannel();
        }

        public STG send(STG message)
        {
            try
            {
                message.tokenApp = tokenApp;
                STG result = services.m_service(message);
                result.Print();
                return result;
            }
            catch
            {
                channelFactory.Abort();
                throw;
            }
        }
    }
}
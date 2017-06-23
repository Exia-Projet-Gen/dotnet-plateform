using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;

namespace HeavyClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            MessageBox.Show("Prêt ?");

            channelFactory = new ChannelFactory<WCFInterfaces.IMesServices>(
                new NetTcpBinding(),
                "net.tcp://localhost:2605/MesServicesWCF");

            services = channelFactory.CreateChannel();
        }

        private ChannelFactory<WCFInterfaces.IMesServices> channelFactory = null;

        private WCFInterfaces.IMesServices services = null;

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                labelLogin.Text = services.Authenticate(inputLogin.Text, inputPassword.Text);
            }
            catch
            {
                channelFactory.Abort();
                throw;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (channelFactory != null && channelFactory.State == CommunicationState.Opened)
            {
                channelFactory.Close();
            }
        }
    }
}

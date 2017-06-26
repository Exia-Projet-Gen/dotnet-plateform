using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ServiceModel;

namespace TestClient
{
    /// <summary>
    /// Logique d'interaction pour Connexion.xaml
    /// </summary>
    public partial class Connexion : Page
    {
        public Connexion()
        {
            InitializeComponent();

            channelFactory = new ChannelFactory<WCFInterfaces.IServices>(
                new NetTcpBinding(),
                "net.tcp://localhost:2605/ServicesWCF");

            services = channelFactory.CreateChannel();
        }

        private ChannelFactory<WCFInterfaces.IServices> channelFactory = null;

        private WCFInterfaces.IServices services = null;

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                String correctLogin = "Rem";
                String correctPassword = "Pass";

                if (loginBox.Text == correctLogin && passwordBox.Password.ToString() == correctPassword)
                {
                    result.Content = "Ok";

                    WCFInterfaces.STG message = new WCFInterfaces.STG()
                    {
                        statut_op = true,
                        info = "connection",
                        data = new object[2] { loginBox.Text, passwordBox.Password.ToString() },
                        operationname = "connection",
                        tokenApp = "tokenApp",
                        tokenUser = "tokenUser"
                    };

                    labelLogin.Content = services.m_service(message);
                    SendFiles sendFilesPage = new SendFiles();
                    this.NavigationService.Navigate(sendFilesPage);
                }

            }
            catch
            {
                channelFactory.Abort();
                throw;
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            //Window.Close();
        }
    }
}

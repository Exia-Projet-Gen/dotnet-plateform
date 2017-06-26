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
using System.Windows.Forms;

namespace TestClient
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            System.Windows.MessageBox.Show("Prêt ?");

            channelFactory = new ChannelFactory<WCFInterfaces.IServices>(
                new NetTcpBinding(),
                "net.tcp://localhost:2605/ServicesWCF");

            Connexion connexionPage = new Connexion();
            Main.NavigationService.Navigate(connexionPage);

           
        }

        private ChannelFactory<WCFInterfaces.IServices> channelFactory = null;

        private WCFInterfaces.IServices services = null;
    }
}

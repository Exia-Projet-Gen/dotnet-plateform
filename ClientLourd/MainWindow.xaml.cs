using System.Windows;
using System.ServiceModel;
using WCFInterfaces;

namespace TestClient
{
    public partial class MainWindow : Window
    {

        public static ChannelFactory<IServices> channelFactory = null;
        public static IServices services = null;

        public static string tokenApp = "l{8W9Fs1p5hz;K6m.gx(vAr)BbkYHIgkH!$1rgtiUtA$BAcdXhUMOY:!5<0L62W";
        public static string tokenUser;

        public static Connexion connexionPage;
        public static Inscription inscriptionPage;
        public static SendFiles sendFilesPage;

        public MainWindow()
        {
            InitializeComponent();

            channelFactory = new ChannelFactory<IServices>("tcpConfig");

            connexionPage = new Connexion();
            inscriptionPage = new Inscription();
            sendFilesPage = new SendFiles();

            Main.NavigationService.Navigate(inscriptionPage);
        }
    }
}

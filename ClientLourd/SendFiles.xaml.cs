using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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
using System.IO;

namespace TestClient
{
    /// <summary>
    /// Logique d'interaction pour SendFiles.xaml
    /// </summary>
    public partial class SendFiles : Page
    {
        String FileContent = null;

        public SendFiles()
        {
            InitializeComponent();

            channelFactory = new ChannelFactory<WCFInterfaces.IServices>(
                new NetTcpBinding(),
                "net.tcp://localhost:2605/ServicesWCF");

            services = channelFactory.CreateChannel();
        }

        private ChannelFactory<WCFInterfaces.IServices> channelFactory = null;

        private WCFInterfaces.IServices services = null;

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".txt";
            //ofd.Filter = "Document texte (.txt)/*.txt";
            if (ofd.ShowDialog() == true)
            {
                string filename = ofd.FileName;
                boxBrowse.Text = filename;
                FileContent = File.ReadAllText(filename);
            }
        }

        private void btnSendFiles_Click(object sender, RoutedEventArgs e)
        {
            fileBox.Text = FileContent;

            WCFInterfaces.STG message = new WCFInterfaces.STG()
            {
                statut_op = true,
                info = "file",
                data = new object[1] { FileContent },
                operationname = "sendFile",
                tokenApp = "tokenApp",
                tokenUser = "tokenUser"
            };

            labelFiles.Content = services.m_service(message);
        }
    }
}

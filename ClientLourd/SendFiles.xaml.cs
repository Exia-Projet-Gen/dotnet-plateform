using Microsoft.Win32;
using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using WCFInterfaces;

namespace TestClient
{
    /// <summary>
    /// Logique d'interaction pour SendFiles.xaml
    /// </summary>
    public partial class SendFiles : Page
    {
        String FileContent = null;
        String filename = null;

        public SendFiles()
        {
            InitializeComponent();

            MainWindow.channelFactory = new ChannelFactory<IServices>("tcpConfig");
            MainWindow.services = MainWindow.channelFactory.CreateChannel();
        }

        /// <summary>
        /// Search a file in files explorer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".txt";
            if (ofd.ShowDialog() == true)
            {
                filename = ofd.FileName;
                boxBrowse.Text = Path.GetFileName(filename);
                FileContent = File.ReadAllText(filename);
            }
        }

        /// <summary>
        /// Send files to decrypt platform
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendFiles_Click(object sender, RoutedEventArgs e)
        {
            STG message = new STG()
            {
                statut_op = true,
                info = "file",
                data = new object[] { Path.GetFileName(filename), FileContent },
                operationname = "bruteForce",
                tokenApp = MainWindow.tokenApp,
                tokenUser = MainWindow.tokenUser
            };

            //Verif that there is a file 
            if(boxBrowse.Text == "")
            {
                noFileFounded.Visibility = Visibility.Visible;
            }
            else
            {
                enCoursLabel.Visibility = Visibility.Visible;
                STG result = MainWindow.services.m_service(message);
                this.NavigationService.Navigate(MainWindow.displayResultsPage);
            }
        }

        /// <summary>
        /// Hide error message when there is a file 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void boxBrowse_TextChanged(object sender, TextChangedEventArgs e)
        {
            noFileFounded.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Log out function and redirect to log in page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void signOutBtn_Click(object sender, RoutedEventArgs e)
        {
            STG message = new STG()
            {
                statut_op = true,
                info = "signOut",
                data = new object[] { },
                operationname = "logout",
                tokenApp = MainWindow.tokenApp,
                tokenUser = MainWindow.tokenUser
            };

            MessageBoxResult resultat;

            resultat = MessageBox.Show("Do you really want to close the App ?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resultat == MessageBoxResult.Yes)
            {
                STG result = MainWindow.services.m_service(message);
                this.NavigationService.Navigate(MainWindow.connexionPage);
            }

        }

        /// <summary>
        /// Exit application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            STG message = new STG()
            {
                statut_op = true,
                info = "signOut",
                data = new object[] {},
                operationname = "logout",
                tokenApp = MainWindow.tokenApp,
                tokenUser = MainWindow.tokenUser
            };

            MessageBoxResult resultat;

            resultat = MessageBox.Show("Do you really want to close the App ?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resultat == MessageBoxResult.Yes)
            {
                STG result = MainWindow.services.m_service(message);
                Application.Current.Shutdown();
            }
        }
    }
}

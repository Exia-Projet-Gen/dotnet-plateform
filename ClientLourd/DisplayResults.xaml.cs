using Microsoft.Win32;
using System;
using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using WCFInterfaces;
using TestClient;

namespace ClientLourd
{
    /// <summary>
    /// Logique d'interaction pour DisplayResults.xaml
    /// </summary>
    public partial class DisplayResults : Page
    {
        public DisplayResults()
        {
            InitializeComponent();
            MainWindow.channelFactory = new ChannelFactory<IServices>("tcpConfig");
            MainWindow.services = MainWindow.channelFactory.CreateChannel();
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(MainWindow.sendFilesPage);
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
                tokenUser = ""
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
                data = new object[] { },
                operationname = "logout",
                tokenApp = MainWindow.tokenApp
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

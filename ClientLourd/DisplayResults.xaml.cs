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
                tokenUser = MainWindow.tokenUser
            };

            MessageBoxResult resultat;

            resultat = MessageBox.Show("Do you really want to sign out ?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

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

        private void refreshBtn_Click(object sender, RoutedEventArgs e)
        {
            STG message = new STG()
            {
                statut_op = true,
                info = "",
                data = new object[] { },
                operationname = "list",
                tokenApp = MainWindow.tokenApp,
                tokenUser = MainWindow.tokenUser
            };

            STG result = MainWindow.services.m_service(message);

            for (int i = 0; i< result.data.Length; i +=2)
            {
                comboBox.Items.Add(result.data[i]);
            }
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String filenameSelected = comboBox.SelectedItem.ToString();

            STG message = new STG()
            {
                statut_op = true,
                info = "",
                data = new object[] { filenameSelected },
                operationname = "result",
                tokenApp = MainWindow.tokenApp,
                tokenUser = MainWindow.tokenUser
            };

            STG result = MainWindow.services.m_service(message);


            statusLabel.Visibility = Visibility.Visible;

            keyBox.Text = result.data[1].ToString();
            statusLabel.Content = result.info;
            contentBox.Text = result.data[2].ToString();
        }
    }
}

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

        /// <summary>
        /// Go back button to return to the send files page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.sendFilesPage.boxBrowse.Clear();
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

        /// <summary>
        /// Refresh the page to see if there is changes on the progress of the files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void refreshBtn_Click(object sender, RoutedEventArgs e)
        {
            comboBox.Items.Clear();

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

        /// <summary>
        /// When another file is selected see the results and the progress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(comboBox.SelectedIndex == -1)
            {
                return;
            }

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

            statusLabel.Content = result.info;

            if(result.info == "finish")
            {
                keyBox.Text = result.data[1].ToString();
                contentBox.Text = result.data[2].ToString();
                pourcentageBox.Text = result.data[3].ToString();
                EmailBox.Text = result.data[4].ToString();
            }
            else if(result.info == "progress")
            {
                keyBox.Clear();
                contentBox.Clear();
                pourcentageBox.Clear();
                EmailBox.Clear();
            }
        }
    }
}

using System.Windows;
using System.Windows.Controls;
using System.ServiceModel;
using WCFInterfaces;

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

            MainWindow.channelFactory = new ChannelFactory<IServices>("tcpConfig");
            MainWindow.services = MainWindow.channelFactory.CreateChannel();
        }

        /// <summary>
        /// Send and verify login informations --> redirection to sendFilesPage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                STG message = new STG()
                {
                    statut_op = true,
                    info = "connection",
                    data = new object[] { loginBox.Text, passwordBox.Password.ToString() },
                    operationname = "login",
                    tokenApp = MainWindow.tokenApp
                };

                //Send login informations
                STG result = MainWindow.services.m_service(message);

                //Verif of login informations
                if (result.statut_op == false && result.info == "Can't find user")
                {
                    wrongLogin.Visibility = Visibility.Visible;
                }
                else if (result.statut_op == false && result.info == "Wrong password")
                {
                    wrongPassword.Visibility = Visibility.Visible;
                }
                else
                {
                    MainWindow.sendFilesPage.connectedUser.Content = message.data[0];
                    MainWindow.tokenUser = result.tokenUser;
                    this.NavigationService.Navigate(MainWindow.sendFilesPage);
                }
            } 
            catch
            {
                MainWindow.channelFactory.Abort();
                throw;
            }
        }

        /// <summary>
        /// Exit method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, RoutedEventArgs e)
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

        /// <summary>
        /// Redirect to the inscription page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(MainWindow.inscriptionPage);
        }

        private void passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            wrongPassword.Visibility = Visibility.Collapsed;
        }

        private void loginBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            wrongLogin.Visibility = Visibility.Collapsed;
        }
    }
}

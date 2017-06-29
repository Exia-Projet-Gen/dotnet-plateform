using System.ServiceModel;
using System.Windows;
using System.Windows.Controls;
using WCFInterfaces;

namespace TestClient
{
    /// <summary>
    /// Logique d'interaction pour Inscription.xaml
    /// </summary>
    public partial class Inscription : Page
    {
        public Inscription()
        {
            InitializeComponent();

            MainWindow.channelFactory = new ChannelFactory<IServices>("tcpConfig");
            MainWindow.services = MainWindow.channelFactory.CreateChannel();
        }

        /// <summary>
        /// Validate inscription.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInscription_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                STG message = new STG()
                {
                    statut_op = true,
                    info = "inscription",
                    data = new object[] { inscrLoginBox.Text, inscrMailBox.Text, inscrPasswordBox.Password.ToString() },
                    operationname = "signup",
                    tokenApp = MainWindow.tokenApp
                };

                STG result = MainWindow.services.m_service(message);

                //if password are different show error message
                if (inscrPasswordBox.Password.ToString() != inscrConfPasswordBox.Password.ToString())
                {
                    passwordDifferent.Visibility = Visibility.Visible;
                    passwordDifferent1.Visibility = Visibility.Visible;
                }
                else if (result.statut_op == false && result.info == "Username already exist")
                {
                    wrongLogin.Visibility = Visibility.Visible;
                }
                else if (result.statut_op == false && result.info == "Email already exist")
                {
                    wrongEmail.Visibility = Visibility.Visible;
                }
                else
                {
                    this.NavigationService.Navigate(MainWindow.connexionPage);
                }
            }
            catch
            {
                MainWindow.channelFactory.Abort();
                throw;
            }
        }

        /// <summary>
        /// Display error message for password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inscrPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            passwordDifferent.Visibility = Visibility.Collapsed;
            passwordDifferent1.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Display error message for confirmation password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inscrConfPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            passwordDifferent.Visibility = Visibility.Collapsed;
            passwordDifferent1.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Navigation to login page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGoToLogin_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(MainWindow.connexionPage);
        }

        /// <summary>
        /// display error message for mail.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inscrMailBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            wrongEmail.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Display error message for login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inscrLoginBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            wrongLogin.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Exit button to shutdown the app
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult resultat;

            resultat = MessageBox.Show("Do you really want to close the App ?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (resultat == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
    }
}

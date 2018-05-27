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
using System.Windows.Shapes;

namespace AllegroWEBAPI
{

    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ApiHandler.UserLogin = userLogin.Text;
            ApiHandler.UserPassword = userPassword.Password.ToString();
            ApiHandler apiHandler = new ApiHandler();
            try
            {
                apiHandler.logIntoAllegro();
            }
            catch (Exception exc) {
                MessageBoxResult result = MessageBox.Show(
                                          "Twój login i/lub hasło są niepoprawne. Spróbuj zalogować się jeszcze raz.", "Wystąpił błąd",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Warning);
                return;
            }
            
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}

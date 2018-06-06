using System;
using System.ComponentModel;
using System.Windows;

namespace AllegroWEBAPI
{

    public partial class LoginWindow : Window

    {
        BackgroundWorker worker;
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ApiHandler.UserLogin = "";
            ApiHandler.UserPassword = "";
            ApiHandler apiHandler = new ApiHandler();           
            ApiHandler.logs.Append("\n Nastąpiło poprawne zalogowanie dla użytkownika o nazwie: " + userLogin.Text + "o godzinie: " + DateTime.Now);
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();

            //ZMIANY!
            //wrzucone bo juz nie trzeba sie logowac

            try
            {
                //jeszcze do 05.06 potrzebne
                //apiHandler.logIntoAllegro();
            }
            catch (Exception exc) {
                Console.Write(exc.StackTrace);
                MessageBoxResult result = MessageBox.Show(
                                          "Twój login i/lub hasło są niepoprawne. Spróbuj zalogować się jeszcze raz.", "Wystąpił błąd",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Warning);
                ApiHandler.logs.Append("\n Próba zalogowania się niepoprawnym hasłem bądź loginem do allegro WEBAPI. " + DateTime.Now);
                return;
     
            }
        }
    }
}

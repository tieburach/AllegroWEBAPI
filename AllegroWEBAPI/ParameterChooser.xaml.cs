using AllegroWEBAPI.pl.allegro.webapi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows;

namespace AllegroWEBAPI
{

    public partial class ParameterChooser : Window
    {
        List<String> list = new List<String>();
        ItemsListType[] itemsList;
        int parameterIndex;

        public ParameterChooser(List<String> list, ItemsListType[] itemsList)
        {
            this.list = list;
            this.itemsList = itemsList;
            InitializeComponent();
            foreach (String single in list) {
                this.parameters.Items.Add(single);
                    }
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            parameterIndex =  this.parameters.SelectedIndex;
            insertIntoDatabase();
        }

        private void insertIntoDatabase ()
        {
            bool flag = true;
            SqlCommand cmd = ApiHandler.ThisConnection.CreateCommand();
            SqlDataAdapter dr = new SqlDataAdapter();
            foreach (var item in itemsList){
                string parameter = item.parametersInfo[parameterIndex].parameterValue[0];
                float cenafloat = item.priceInfo[0].priceValue;
                string cenaa = cenafloat + "";
                string waluta = item.priceInfo[0].priceType;
                string tytul = item.itemTitle;
                string time = item.timeToEnd;
                cmd.CommandText = "INSERT INTO AllegroDatabase.dbo.ProductsPrices (price, title, description, parametervalues) VALUES ('" + cenaa.Replace(",", ".") + "', '" + tytul + "', '" + waluta + "'," + parameter.Replace(",", ".") + ")";
                ApiHandler.logs.Append("\n" + cmd.CommandText + DateTime.Now);
                cmd.CommandType = CommandType.Text;
                dr.InsertCommand = cmd;
                try
                {
                    dr.InsertCommand.ExecuteNonQuery();
                    flag = false;
                }
                catch (SqlException exc)
                {
                    if (flag)
                    {
                        ApiHandler.logs.Append("\nWybrany aytrybut przez użytkownika nie jest ilościowy.\n");
                        MessageBoxResult result = MessageBox.Show("Wybrany przez Ciebie atrybut nie jest atrybutem ilościowym. Wybierz inny",
                                              "Błąd",
                                              MessageBoxButton.OK,
                                              MessageBoxImage.Error);
                        return;
                    }
                    ApiHandler.logs.Append("\nW powyższej próbie wstawienie wartości do bazy wystąpił błąd. Wynika to ze niepoprawnego formatu aukcji, czyli błędnego uzupełnienia przez użytkownika sprzedającego parametrów. \n");
                    
                }
                cmd.Dispose();
    
            }

            System.Diagnostics.Process.Start("CMD.exe", "/C Rscript Script.R " + MainWindow.categoryName + " " + MainWindow.categoryTree.Replace(" ",""));

            while (!Directory.Exists("C:/Users/Tieburach/Desktop/" + MainWindow.categoryName)) {
               System.Threading.Thread.Sleep(200);
            }

            Window1 window1 = new Window1();
            window1.Show();
        }
    }
}

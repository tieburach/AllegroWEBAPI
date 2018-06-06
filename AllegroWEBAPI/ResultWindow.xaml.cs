using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace AllegroWEBAPI
{

    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            string query = "SELECT price FROM AllegroDatabase.dbo.Results";
            ApiHandler.logs.Append("\n" + query + DateTime.Now);
            SqlCommand cmd = ApiHandler.ThisConnection.CreateCommand();
            cmd.CommandText = query;
            SqlDataReader dr;
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteReader();
            List<double> list = new List<double>();
            while (dr.Read())
            {
                list.Add(dr.GetDouble(0));
            }
            dr.Close();
            System.Threading.Thread.Sleep(300); 
            this.minimalPrice.Text = list[0].ToString("0.00") + " zł";
            this.averagePrice.Text = list[1].ToString("0.00") + " zł";
            this.maximumPrice.Text = list[2].ToString("0.00") + " zł";
            ApiHandler.logs.Append("\nKoniec o: " + DateTime.Now);
            System.IO.File.WriteAllText("C:/Users/Tieburach/Desktop/" + MainWindow.categoryName + "/logs", ApiHandler.logs.ToString());

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //widok word
            System.Diagnostics.Process.Start("CMD.exe", "/C start winword C:/Users/Tieburach/Desktop/" + MainWindow.categoryName + "/Raport.docx");
            

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //widok html
            System.Diagnostics.Process.Start("CMD.exe", "/C start chrome C:/Users/Tieburach/Desktop/" + MainWindow.categoryName + "/Raport.html");
        }
    }
}


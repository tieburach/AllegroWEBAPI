﻿using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AllegroWEBAPI.pl.allegro.webapi;  // będziemy używać biblioteki webapi
using AllegroWEBAPI.Properties;
using System.Threading;

namespace AllegroWEBAPI
{



    public partial class MainWindow : Window
    {
        private int currentParentId = 0;
        private String currentName;
        private Boolean finished = false;
        public MainWindow()
        {
            ApiHandler.ThisConnection.Open();
            InitializeComponent();

            string Get_Kategoria = "SELECT Name FROM AllegroDatabase.dbo.Categories WHERE Parent_Id = 0";
            SqlCommand cmd = ApiHandler.ThisConnection.CreateCommand();
            cmd.CommandText = Get_Kategoria;

            SqlDataReader dr;
            cmd.CommandType = CommandType.Text;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
               this.Kategoria.Items.Add(dr.GetValue(0)); 
            }
            dr.Close();
    

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ApiHandler apiHandler = new ApiHandler();
            apiHandler.findByCategory(currentParentId);
            Window1 window1 = new Window1();
            window1.Show();
            this.Close();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!finished)
            {
                currentName = this.Kategoria.Text;
                this.actualChoice.Text += "->" + currentName;
                string query = "SELECT sub.Id FROM( SELECT * FROM AllegroDatabase.dbo.Categories cat WHERE cat.Parent_Id = " + currentParentId + " ) sub WHERE sub.Name Like '" + currentName + "'";
                SqlCommand cmd = ApiHandler.ThisConnection.CreateCommand();
                cmd.CommandText = query;
                SqlDataReader dr;
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    currentParentId = (int)(dr.GetValue(0));
                }
                dr.Close();
                this.Kategoria.Items.Clear();
                query = "SELECT Name FROM AllegroDatabase.dbo.Categories WHERE Parent_Id = " + currentParentId;
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    this.Kategoria.Items.Add(dr.GetValue(0));
                }
                dr.Close();

                if (Kategoria.Items.IsEmpty) {
                    this.Kategoria.Visibility = Visibility.Hidden;
                    this.continueButton.Visibility = Visibility.Hidden;
                }

            }
            else {
                ;
            }
        }
    }
    }


using AllegroWEBAPI.pl.allegro.webapi;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AllegroWEBAPI
{

    class ApiHandler
    {
        public static void SetCertificatePolicy()
        {
            ServicePointManager.ServerCertificateValidationCallback += RemoteCertificateValidate;
        }
        private static bool RemoteCertificateValidate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {

            return true;
        }
        private static SqlConnection thisConnection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;" +
            "Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;" +
            "MultiSubnetFailover=False");

       

        public static StringBuilder logs = new StringBuilder("Początek:\n" + DateTime.Now);

        public static SqlConnection ThisConnection { get => thisConnection; set => thisConnection = value; }
        private static String userLogin;
        private static String userPassword;
        private serviceService service;
        private string sessionHandle;
        const string webapiKey = "22092f5b";
        Int64 versionKey;
        const int countryid = 1;

        public ApiHandler()
        {
            SetCertificatePolicy();
            Service = new serviceService();
            Service.doQuerySysStatus(1, Countryid, WebapiKey, out versionKey);
        }


        public static string UserLogin { get => userLogin; set => userLogin = value; }
        public static string UserPassword { get => userPassword; set => userPassword = value; }
        public serviceService Service { get => service; set => service = value; }

        
        public static int Countryid => countryid;

        public static string WebapiKey => webapiKey;



        public void logIntoAllegro()
        {
            Int64 hashoffset = 0; Int64 serverTime = 0;    //deklaracje zmiennych zwracanych przez funkcję doLogin
            sessionHandle =
                   Service.doLogin(
                   userLogin,
                   userPassword,
                   Countryid,
                   WebapiKey,
                   versionKey,
                   out hashoffset,
                   out serverTime);
        }

        public void findByCategory(int CategoryId)
        {
            FilterOptionsType[] filter = new FilterOptionsType[1];
            filter[0] = new FilterOptionsType();
            filter[0].filterId = "category";
            string[] filterValue_Category = new string[1];
            filterValue_Category[0] = "" + CategoryId; // numer kategorii
            filter[0].filterValueId = filterValue_Category;
            int itemsFeaturedCount; // Liczba promowanych ofert
            bool itemsFeaturedCountSpecified;
            ItemsListType[] itemsList; // Oferty
            CategoriesListType categoriesList; // Informacje o kategoriach
            FiltersListType[] filtersList; // Filtry możliwe do ustawienia
            string[] filtersRejected; // Niepoprawne filtry

            //sessionHandle - przechowuję liczbę wszystkich zwróconych ofert (pasujacych do zapytania)
            int sessionHandle = Service.doGetItemsList(
                            WebapiKey, // WebApi klucz
                            1, // Id kraju => 1 dla PL
                            filter, // Kryteria filtrowania
                            null, // Kryteria sortowania
                            1000, // Rozmiar porcji wynikowej, domyślnie (przy false pobiera 100)
                            true, // 
                            0, // Sterowanie pobieraniem kolejnych porcji
                            false, //
                            0, // Sterowanie zakresem zwracanych danych
                            false, //
                            out itemsFeaturedCount, out itemsFeaturedCountSpecified, out itemsList, out categoriesList, out filtersList, out filtersRejected);


            List<String> list = new List<String>();
            for (int i = 0; i< itemsList[0].parametersInfo.Length;i++) { 
                list.Add(itemsList[0].parametersInfo[i].parameterName);
            }
            ParameterChooser parameterChooser = new ParameterChooser(list, itemsList);
            parameterChooser.Show();
            
        }


    }

}

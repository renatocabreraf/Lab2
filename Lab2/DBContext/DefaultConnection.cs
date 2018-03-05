using Lab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TDA;
using TDALibrary;

namespace Lab2.DBContext
{
    public class DefaultConnection
    {
        private static volatile DefaultConnection Instance;
        private static object syncRoot = new Object();
       
        public ABinBusqueda<Pais, int> pais = new ABinBusqueda<Pais, int>();
        public int IDActual { get; set; }

        private DefaultConnection()
        {
            IDActual = 0;
        }

        public static DefaultConnection getInstance
        {
            get
            {
                if (Instance == null)
                {
                    lock (syncRoot)
                    {
                        if (Instance == null)
                        {
                            Instance = new DefaultConnection();
                        }
                    }
                }
                return Instance;
            }
        }

    }
}
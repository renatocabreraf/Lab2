using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lab2.Models
{
    public class Pais
    {
        public int PaisID { get; set; }

        public string NombrePais { get; set; }

        public string Grupo { get; set; } 
    }
}
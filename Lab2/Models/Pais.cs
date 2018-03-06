using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace Lab2.Models
{
    public class Pais
    {
        public int PaisID { get; set; }

        public string NombrePais { get; set; }

        public string Grupo { get; set; } 
    }
    public class Valor
    {
        public string nombre { get; set; }
        public string Grupo { get; set; }
    }

    public class Valor2
    {
        public string nombre { get; set; }
        public string Grupo { get; set; }
    }

    public class Izquierdo
    {
        public Valor2 valor { get; set; }
        public object izquierdo { get; set; }
        public object derecho { get; set; }
    }

    public class Valor3
    {
        public string nombre { get; set; }
        public string Grupo { get; set; }
    }

    public class Valor4
    {
        public string nombre { get; set; }
        public string Grupo { get; set; }
    }

    public class Valor5
    {
        public string nombre { get; set; }
        public string Grupo { get; set; }
    }

    public class Izquierdo3
    {
        public Valor5 valor { get; set; }
        public object izquierdo { get; set; }
        public object derecho { get; set; }
    }

    public class Valor6
    {
        public string nombre { get; set; }
        public string Grupo { get; set; }
    }

    public class Derecho2
    {
        public Valor6 valor { get; set; }
        public object izquierdo { get; set; }
        public object derecho { get; set; }
    }

    public class Izquierdo2
    {
        public Valor4 valor { get; set; }
        public Izquierdo3 izquierdo { get; set; }
        public Derecho2 derecho { get; set; }
    }

    public class Valor7
    {
        public string nombre { get; set; }
        public string Grupo { get; set; }
    }

    public class Valor8
    {
        public string nombre { get; set; }
        public string Grupo { get; set; }
    }

    public class Valor9
    {
        public string nombre { get; set; }
        public string Grupo { get; set; }
    }

    public class Izquierdo5
    {
        public Valor9 valor { get; set; }
        public object izquierdo { get; set; }
        public object derecho { get; set; }
    }

    public class Valor10
    {
        public string nombre { get; set; }
        public string Grupo { get; set; }
    }

    public class Derecho4
    {
        public Valor10 valor { get; set; }
        public object izquierdo { get; set; }
        public object derecho { get; set; }
    }

    public class Izquierdo4
    {
        public Valor8 valor { get; set; }
        public Izquierdo5 izquierdo { get; set; }
        public Derecho4 derecho { get; set; }
    }

    public class Derecho3
    {
        public Valor7 valor { get; set; }
        public Izquierdo4 izquierdo { get; set; }
        public object derecho { get; set; }
    }

    public class Derecho
    {
        public Valor3 valor { get; set; }
        public Izquierdo2 izquierdo { get; set; }
        public Derecho3 derecho { get; set; }
    }

    public class PaisNuevo
    {
        public Valor valor { get; set; }
        public Izquierdo izquierdo { get; set; }
        public Derecho derecho { get; set; }
       
        }

    }
    

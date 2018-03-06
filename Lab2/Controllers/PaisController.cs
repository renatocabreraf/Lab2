using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using Lab2.Models;
using System.Net;
using System.IO;
using System.Data;
using Lab2.DBContext;
using LumenWorks.Framework.IO.Csv;
using System.Collections.Generic;
using Newtonsoft.Json;
using TDA;
using TDALibrary;

namespace Lab2.Controllers
{
    
    public class PaisController : Controller
    {
        //no logramos terminar la cargad e archuivos
        DefaultConnection db = DefaultConnection.getInstance;
        string Lista;

        // GET: /Pais/
        public ActionResult Index(string searchString)
        {

            
            return View(db.pais.ToList());
        }

        //
        // GET: /Pais/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Pais PaisBuscada = db.pais.Buscar(id);

            if (PaisBuscada == null)
            {
                return HttpNotFound();
            }

            return View(PaisBuscada);
        }

        //
        // GET: /Pais/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Pais/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "NombrePais,Grupo")] Pais Pais)
        {
            try
            {
                // TODO: Add insert logic here
                db.pais.FuncionCompararLlave = Comparar;
                db.pais.FuncionObtenerLlave = ObtenerClave;
                Pais.PaisID = ++db.IDActual;
                db.pais.Insertar(Pais);
                db.pais.RecorrerPostOrder(ObtenerListado);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Pais/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Pais PaisBuscada = db.pais.Buscar(id);

            if (PaisBuscada == null)
            {
                return HttpNotFound();
            }

            return View(PaisBuscada);
        }

        //
        // POST: /Pais/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NombrePais,Grupo")]Pais Pais)
        {
            try
            {
                Pais PaisBuscada = db.pais.Buscar(Pais.PaisID);
                if (PaisBuscada == null)
                {
                    return HttpNotFound();
                }
                PaisBuscada.nombre = Pais.nombre;
                PaisBuscada.Grupo = Pais.Grupo;
               

                return RedirectToAction("Index");
            }
            catch
            {
                return View("Index");
            }
        }

        //
        // GET: /Pais/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Pais PaisBuscada = db.pais.Buscar(id);

            if (PaisBuscada == null)
            {
                return HttpNotFound();
            }

            return View(PaisBuscada);
        }

        //
        // POST: /Pais/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Pais PaisBuscada = db.pais.Buscar( id);

                if (PaisBuscada == null)
                {
                    return HttpNotFound();
                }

                db.pais.Eliminar(id);
                db.IDActual--;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        /// <summary>
        /// GET UPLOAD
        /// </summary>
        /// <returns></returns>
        public ActionResult Upload()
        {
            return View();
        }
        /// <summary>
        /// Se sube un archivo
        /// </summary>
        /// <param name="upload"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string filePath = string.Empty;
                if (upload != null && upload.ContentLength > 0)
                {

                    if (upload.FileName.EndsWith(".json"))
                    {
                        Stream stream = upload.InputStream;
                        JsonReader<ABinBusqueda<Numero, int>> reader = new JsonReader<TDA.ABinBusqueda<Numero, int>>();
                        db.pais = reader.Data(stream);
                    }
                    else
                    {
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
            }
            return View();
 
    }
        int Comparar(string Clave1, string Clave2)
        {
            return string.CompareOrdinal(Clave1, Clave2);
        }
        int Comparar(int Clave1, int Clave2)
        {
            if (Clave1 > Clave2)
                return 1;
            else if (Clave1 < Clave2)
                return -1;
            else
                return 0;
        }
        int ObtenerClave(Pais dato)
        {
            return dato.PaisID;
        }
        
        private void ObtenerListado(Pais miPais)
        {
            Lista = Lista + " " + miPais.PaisID + " : " + miPais.PaisID + " |";
        }
        public class JsonReader<T>
        {
            /// <summary>
            /// Lector de Archivos tipo Json
            /// </summary>
            /// <param name="rutaOrigen">Ruta de archivos</param>
            /// <returns></returns>
            public ABinBusqueda<Pais, int> Data(Stream rutaOrigen)
            {
                try
                {
                    ABinBusqueda<Pais, int> data;
                    StreamReader reader = new StreamReader(rutaOrigen);
                    string temp = reader.ReadToEnd();
                    data = JsonConvert.DeserializeObject<ABinBusqueda<Pais, int>>(temp);
                    reader.Close();
                    return data;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
    }

}

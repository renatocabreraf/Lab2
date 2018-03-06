using Lab2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TDA;

namespace Lab2.Controllers
{
    public class CadenaController : Controller
    {
        public ABinBusqueda<Numero, int> Arbolito = new ABinBusqueda<Numero, int>();
        string Lista;
        // GET: Int
        public ActionResult Index()
        {
            return View(Arbolito.ToList());
        }

        // GET: Int/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Numero NumeroBuscado = Arbolito.Buscar(id);

            if (NumeroBuscado == null)
            {
                return HttpNotFound();
            }

            return View(NumeroBuscado);
        }

        // GET: Int/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Int/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "numero")] Numero numero)
        {
            try
            {
                // TODO: Add insert logic here
                Arbolito.FuncionCompararLlave = Comparar;
                Arbolito.FuncionObtenerLlave = ObtenerClave;

                Arbolito.Insertar(numero);
                Arbolito.RecorrerPostOrder(ObtenerListado);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        // GET: Int/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Numero NumeroBuscado = Arbolito.Buscar(id);

            if (NumeroBuscado == null)
            {
                return HttpNotFound();
            }

            return View(NumeroBuscado);
        }

        // POST: Int/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "numero")]Numero numero)
        {
            try
            {
                Numero NumeroBuscado = Arbolito.Buscar(numero.numero);
                if (NumeroBuscado == null)
                {
                    return HttpNotFound();
                }
                NumeroBuscado.numero = numero.numero;
                


                return RedirectToAction("Index");
            }
            catch
            {
                return View("Index");
            }
        }


        // GET: Int/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Numero NumeroBuscado = Arbolito.Buscar(id);

            if (NumeroBuscado == null)
            {
                return HttpNotFound();
            }

            return View(NumeroBuscado);
        }

        // POST: Int/Delete/5
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

                Numero NumeroBuscado = Arbolito.Buscar(id);

                if (NumeroBuscado == null)
                {
                    return HttpNotFound();
                }

                Arbolito.Eliminar(id);
                
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
                        List<ABinBusqueda<Pais, int>> jsonlist = new List<ABinBusqueda<Pais, int>>();

                        string path = Server.MapPath("~/Uploads/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        filePath = path + Path.GetFileName(upload.FileName);
                        string extension = Path.GetExtension(upload.FileName);
                        upload.SaveAs(filePath);

                        string json = System.IO.File.ReadAllText(filePath);
                        ABinBusqueda<Pais, int> nuevoPais = JsonConvert.DeserializeObject<ABinBusqueda<Pais, int>>(json);
                        jsonlist.Add(nuevoPais);
                        return View(jsonlist);
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
        int ObtenerClave(Numero dato)
        {
            return dato.numero;
        }

        private void ObtenerListado(Numero miPais)
        {
            Lista = Lista + " " + miPais.numero + " : " + miPais.numero + " |";
        }
    }
}

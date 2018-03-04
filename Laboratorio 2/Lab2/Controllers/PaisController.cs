using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Lab2.Models;
using System.Net;
using System.IO;
using System.Data;
namespace Lab2.Controllers
{

    public class PaisController : Controller
    {

        DefaultConnection db = DefaultConnection.getInstance;


        // GET: /Pais/
        public ActionResult Index(string searchString)
        {

            var pais = from s in db.pais
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                pais = pais.Where(s => s.Apellido.Contains(searchString)
                                       || s.Nombre.Contains(searchString));
            }
            return View(pais.ToList());
        }

        //
        // GET: /Pais/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Pais PaisBuscada = db.pais.FirstOrDefault(x => x.PaisID == id);

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
        public ActionResult Create([Bind(Include = "PaisID,Nombre,Apellido,Edad,Salario,Club,Posicion")] Pais Pais)
        {
            try
            {
                // TODO: Add insert logic here
                Pais.PaisID = ++db.IDActual;
                db.pais.AddLast(Pais);
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

            Pais PaisBuscada = db.pais.FirstOrDefault(x => x.PaisID == id);

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
        public ActionResult Edit([Bind(Include = "PaisID,Nombre,Apellido,Edad,Salario,Club,Posicion")]Pais Pais)
        {
            try
            {
                Pais PaisBuscada = db.pais.FirstOrDefault(x => x.PaisID == Pais.PaisID);
                if (PaisBuscada == null)
                {
                    return HttpNotFound();
                }
                PaisBuscada.NombrePais = Pais.NombrePais;
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

            Pais PaisBuscada = db.pais.FirstOrDefault(x => x.PaisID == id);

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

                Pais PaisBuscada = db.pais.FirstOrDefault(x => x.PaisID == id);

                if (PaisBuscada == null)
                {
                    return HttpNotFound();
                }

                db.pais.Remove(PaisBuscada);
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

                    if (upload.FileName.EndsWith(".csv"))
                    {
                        Stream stream = upload.InputStream;
                        DataTable csvTable = new DataTable();
                        using (CsvReader csvReader =
                            new CsvReader(new StreamReader(stream), true))
                        {
                            csvTable.Load(csvReader);
                        }
                        string path = Server.MapPath("~/Uploads/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        filePath = path + Path.GetFileName(upload.FileName);
                        string extension = Path.GetExtension(upload.FileName);
                        upload.SaveAs(filePath);

                        string csvData = System.IO.File.ReadAllText(filePath);
                        foreach (string row in csvData.Split('\n'))
                        {

                            if (!string.IsNullOrEmpty(row))
                            {
                                db.pais.AddLast(new Pais
                                {
                                    PaisID = ++db.IDActual,
                                    Club = row.Split(',')[0],
                                    Apellido = row.Split(',')[1],
                                    Nombre = row.Split(',')[2],
                                    Posicion = row.Split(',')[3],
                                    Salario = row.Split(',')[4],

                                });

                            }

                        }
                        return View(csvTable);
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

        public ActionResult Buscador(String Nombre)
        {
            var busqueda = from s in db.pais select s;
            if (!String.IsNullOrEmpty(Nombre))
            {
                busqueda = busqueda.Where(j => j.Nombre.Contains(Nombre));
            }
            return View(busqueda);
        }
    }
}

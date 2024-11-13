using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WebApplication1.Models;
using WebApplication1.Dao;

namespace WebApplication1.Controllers
{
    public class ConsultasController : Controller
    {
        ConsultasDao consultasDao = new ConsultasDao();
        // GET: Consultas
        public ActionResult ListadoCategorias()
        {
            List<Categorias> listado = consultasDao.ListarCategorias();
            return View(listado);
        }

        public ActionResult ListadoProductos()
        {
            List<Productos> listado = consultasDao.ListarProductos(1);
            return View(listado);
        }

        public ActionResult ListadoProductosSP()
        {
            List<Productos> listado = consultasDao.ListarProductosSP(6);
            return View(listado);
        }
    }
}
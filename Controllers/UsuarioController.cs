using ConcursosContratos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Data.Entity.Core.Metadata.Edm;

namespace ConcursosContratos.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ViewResult Index(int? page)
        {
            listaResponsabilidad();
       
            //List<UsuarioCLS> listaUsuario = new List<UsuarioCLS>();
            using(var bd= new CCDevEntities())
            {
        var      listaUsuario = (from usuario in bd.Usuarios
                                join responsabilidad in bd.Responsabilidads
                                on usuario.IDRESPONSABILIDAD equals
                                responsabilidad.IDRESPONSABILIDAD
                                where usuario.ACTIVO == 1
                                select new UsuarioCLS
                                {
                                   idusuario=usuario.IDUSUARIO,
                                    usuario = usuario.USUARIO1,
                                    contra = usuario.CONTRA,
                                    descripcion = usuario.DESCRIPCION,
                                    correo = usuario.CORREO,
                                    responsabilidad=responsabilidad.RESPONSABILIDAD1
                                }).ToList();
                int pageSize = 8;
                int pageNumber = (page ?? 1);
                return View(listaUsuario.ToPagedList(pageNumber, pageSize));
            }
           
        }
        public void listaResponsabilidad()
        {

            List<SelectListItem> listItems;
            using(var bd = new CCDevEntities())
            {
                listItems=(from item in bd.Responsabilidads
                           select new SelectListItem
                           {
                               Text=item.RESPONSABILIDAD1,
                               Value=item.IDRESPONSABILIDAD.ToString()
                           }).ToList();
                listItems.Insert(0, new SelectListItem { Text = "<--Seleccionar-->" });
                ViewBag.listares = listItems;
            }
        
        }

        [HttpPost]
        public string Guardar(UsuarioCLS usuarioCLS, int? titulo)
        {

            string rpta = "";

            try
            {
                using (var bd = new CCDevEntities())
                {
                    using (var transaccion = new TransactionScope())
                    {
                        if (titulo.Equals(1))
                        {
                            int cant = 0;

                            cant = bd.Usuarios.Where(u => u.IDUSUARIO == usuarioCLS.idusuarioe).Count();
                            if (cant.Equals(1))
                            {
                                //editar                            
                                Usuario usuario = bd.Usuarios.Where(u => u.USUARIO1 == usuarioCLS.usuarioe).First();
                                usuario.DESCRIPCION = usuarioCLS.descripcione;
                                usuario.CORREO = usuarioCLS.correoe;
                                //usuario.IDRESPONSABILIDAD = usuarioCLS.idresponsabilidade;
                                rpta = bd.SaveChanges().ToString();
                                transaccion.Complete();
                            }
                            else
                            {
                                //usuarioCLS.idresponsabilidade = 1;
                                if (!ModelState.IsValid)
                                {
                                    var query = (from state in ModelState.Values
                                                 from error in state.Errors
                                                 select error.ErrorMessage).ToList();
                                    //forma el html para pasarlo a la vista
                                    rpta += "<ul class='list-group'>";
                                    //los resultados de query se agregaran con un for each
                                    foreach (var item in query)
                                    {
                                        rpta += "<li class='list-group-item text-danger'>" + item + "!!</li>";
                                    }
                                    rpta += "</ul>";
                                }
                                else
                                {
                                    int cantidad = 0;

                                    cantidad = bd.Usuarios.Where(u => u.USUARIO1 == usuarioCLS.usuario).Count();
                                    if (cantidad.Equals(0))
                                    {
                                        Usuario usuario = new Usuario();
                                        usuario.USUARIO1 = usuarioCLS.usuario;
                                        //conversion de la cadena a cifrado-----------//
                                        SHA256Managed sha = new SHA256Managed();
                                        byte[] byteContra = Encoding.Default.GetBytes(usuarioCLS.contra);
                                        byte[] byteContraCifrado = sha.ComputeHash(byteContra);
                                        string cadenaContraCifrada = BitConverter.ToString(byteContraCifrado).Replace("-", "");
                                        //--------------------------------------------//
                                        usuario.CONTRA = cadenaContraCifrada;
                                        usuario.DESCRIPCION = usuarioCLS.descripcion;
                                        usuario.CORREO = usuarioCLS.correo;
                                        usuario.IDRESPONSABILIDAD = usuarioCLS.idresponsabilidad;
                                        usuario.ACTIVO = 1;
                                        bd.Usuarios.Add(usuario);
                                        rpta = bd.SaveChanges().ToString();

                                        transaccion.Complete();

                                        return rpta;
                                    }
                                    else
                                    {
                                        rpta = "ya existe el usuario";
                                    }
                                }
                            }
                        }
                     
                    }
                }
            }
            catch (Exception ex)
            {
                rpta = "Ocurrio un error: " + ex;
            }
            return rpta;

        }

        public ActionResult Filtrar(string user,int? page)
        {
            
            listaResponsabilidad();
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            using (var bd= new CCDevEntities()) {
                if (user == null) { 
            var    listItems = (from usuario in bd.Usuarios
                                           join responsabilidad in bd.Responsabilidads
                                           on usuario.IDRESPONSABILIDAD equals
                                           responsabilidad.IDRESPONSABILIDAD
                                           where usuario.ACTIVO == 1
                                           select new UsuarioCLS
                                           {
                                               idusuario=usuario.IDUSUARIO,
                                               usuario = usuario.USUARIO1,
                                               contra = usuario.CONTRA,
                                               descripcion = usuario.DESCRIPCION,
                                               correo = usuario.CORREO,
                                               responsabilidad = responsabilidad.RESPONSABILIDAD1
                                           }).ToList();
                    listItems = listItems.OrderBy(u => u.idusuario).ToList();
              
                    
                    return PartialView("_Lista", listItems.ToPagedList(pageNumber, pageSize));
                }
                else
                {
                var   listItems = (from usuario in bd.Usuarios
                                 join responsabilidad in bd.Responsabilidads
                                 on usuario.IDRESPONSABILIDAD equals
                                 responsabilidad.IDRESPONSABILIDAD
                                 where usuario.ACTIVO == 1
                                 &&(usuario.USUARIO1.Contains(user)
                                   || usuario.DESCRIPCION.Contains(user)
                                   || usuario.CORREO.Contains(user))
                                   //||responsabilidad.RESPONSABILIDAD1.Contains(usuarioCLS.responsabilidad))
                                   select new UsuarioCLS
                                 {
                                       idusuario = usuario.IDUSUARIO,
                                       usuario = usuario.USUARIO1,
                                     contra = usuario.CONTRA,
                                     descripcion = usuario.DESCRIPCION,
                                     correo = usuario.CORREO,
                                     responsabilidad = responsabilidad.RESPONSABILIDAD1
                                 }).ToList();
                    listItems = listItems.OrderBy(u => u.idusuario).ToList();
                    return PartialView("_Lista", listItems.ToPagedList(pageNumber, pageSize));
                }
            }
           
        }
        public JsonResult recuperarDatos(int idusuario)
        {
            UsuarioCLS usuarioCLS = new UsuarioCLS();
            using(var bd= new CCDevEntities())
            {
                Usuario usuario = bd.Usuarios.Where(u => u.IDUSUARIO == idusuario).First();
                usuarioCLS.idusuario = usuario.IDUSUARIO;
                usuarioCLS.usuario = usuario.USUARIO1;      
                usuarioCLS.descripcion = usuario.DESCRIPCION;
                usuarioCLS.correo = usuario.CORREO;
                usuarioCLS.idresponsabilidad = usuario.IDRESPONSABILIDAD;
            }
            return Json(usuarioCLS, JsonRequestBehavior.AllowGet);
        }
        public string Eliminar(int idusuario)
        {

            string rpta = "";

            try
            {
                using (var bd = new CCDevEntities())
                {
                    Usuario usuario = bd.Usuarios.Where(u => u.IDUSUARIO == idusuario).First();
                    usuario.ACTIVO = 0;
                    rpta = bd.SaveChanges().ToString();
                }
            }catch(Exception ex)
            {
                rpta = "error "+ex;
            }
            return rpta;
        }
    }
}
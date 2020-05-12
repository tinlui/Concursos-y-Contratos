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

namespace ConcursosContratos.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            listaResponsabilidad();

            List<UsuarioCLS> listaUsuario = new List<UsuarioCLS>();
            using(var bd= new CCDevEntities())
            {
                listaUsuario = (from usuario in bd.Usuarios
                                join responsabilidad in bd.Responsabilidads
                                on usuario.IDRESPONSABILIDAD equals
                                responsabilidad.IDRESPONSABILIDAD
                                where usuario.ACTIVO == 1
                                select new UsuarioCLS
                                {
                                   
                                    usuario = usuario.USUARIO1,
                                    contra = usuario.CONTRA,
                                    descripcion = usuario.DESCRIPCION,
                                    correo = usuario.CORREO,
                                    responsabilidad=responsabilidad.RESPONSABILIDAD1
                                }).ToList();
            }
            return View(listaUsuario);
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
        public string Guardar(UsuarioCLS usuarioCLS)
        {

            string rpta = "";

            try
            {
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
                    using (var bd = new CCDevEntities())
                    {

                        int cantidad = 0;                       
                        using(var transaccion = new TransactionScope())
                        {
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
                                int cant = 0;
                                cant = bd.Usuarios.Where(u => u.USUARIO1 == usuarioCLS.usuario && u.CORREO == usuarioCLS.correo).Count();
                                if (cant.Equals(1))
                                {
                                    //editar                            
                                    Usuario usuario = bd.Usuarios.Where(u => u.USUARIO1 == usuarioCLS.usuario && u.CORREO == usuarioCLS.correo).First();
                                    usuario.DESCRIPCION = usuarioCLS.descripcion;
                                    usuario.CORREO = usuarioCLS.correo;
                                    usuario.IDRESPONSABILIDAD = usuarioCLS.idresponsabilidad;
                                    rpta = bd.SaveChanges().ToString();                                
                                    transaccion.Complete();                              
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
            catch (Exception ex)
            {
                rpta = "Ocurrio un error: "+ex;
            }
            return rpta;

        }
    }
}
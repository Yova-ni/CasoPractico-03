using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using UNACH.Videoclub.WebService.Models;

namespace UNACH.Videoclub.WebService
{
    /// <summary>
    /// Descripción breve de PeliculasService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class PeliculasService : System.Web.Services.WebService
    {

        [WebMethod(Description = "Lista de películas")]
        public List<Pelicula> Peliculas()
        {
            using (VideoclubEntities conexion = new VideoclubEntities())
            {
                var consulta = (from l in conexion.Peliculas select l).ToList();
                return consulta;
            }
        }

        [WebMethod (Description = "Insertar registro de películas")]
        public bool InsertarPelicula(string titulo, int anio, string genero, string presupuesto)
        {
            try
            {
                using (VideoclubEntities conexion = new VideoclubEntities())
                {
                    Pelicula nuevaPelicula = new Pelicula();
                    nuevaPelicula.Id = Guid.NewGuid();
                    nuevaPelicula.Titulo = titulo;
                    nuevaPelicula.Anio = anio;
                    nuevaPelicula.Genero = genero;
                    nuevaPelicula.Presupuesto = presupuesto;
                    conexion.Peliculas.Add(nuevaPelicula);
                    conexion.SaveChanges();
                    return true;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        [WebMethod (Description = "Modificar registro de película")]
        public bool ModificarPelicula(Guid id, string titulo, int anio, string genero, string presupuesto)
        {
            try
            {
                using (VideoclubEntities conexion = new VideoclubEntities())
                {
                    var consulta = (from l in conexion.Peliculas where l.Id == id select l).FirstOrDefault();
                    if (consulta != null)
                    {
                        consulta.Titulo = titulo;
                        consulta.Anio = anio;
                        consulta.Genero = genero;
                        consulta.Presupuesto = presupuesto;
                        conexion.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        [WebMethod (Description = "Elminar registro")]
        public bool EliminarPelicula(Guid id)
        {
            try
            {
                using (VideoclubEntities conexion = new VideoclubEntities())
                {
                    var consulta = (from l in conexion.Peliculas where l.Id == id select l).FirstOrDefault();
                    conexion.Peliculas.Remove(consulta);
                    conexion.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

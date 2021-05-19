using CapaEntidades;
using CapaUtilidades.IGenerica;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
//using VisioForge.Shared.MediaFoundation.OPM;

namespace CapaDatos
{
    public class csInventarioDatos : IGenerica<tbProductos>
    {
        public bool eliminar(tbProductos producto)
        {
            try
            {
                using (var context = new dbSuperJMEntities1())
                {
                    context.Entry<tbProductos>(producto).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool guardar(tbProductos producto)
        {
            try
            {
                using (dbSuperJMEntities1 context = new dbSuperJMEntities1())
                {
                    context.tbProductos.Add(producto);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool modificar(tbProductos producto)
        {
            try
            {
                using (var context = new dbSuperJMEntities1())
                {
                    context.Entry<tbProductos>(producto).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<tbProductos> obtenerlista(int producto)
        {
            try
            {
                using (dbSuperJMEntities1 contex = new dbSuperJMEntities1())
                {
                    int est = 1;

                    return (from c in contex.tbProductos
                            where c.Estado == est
                            select c).ToList();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

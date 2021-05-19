using CapaDatos;
using CapaEntidades;
using CapaUtilidades.IGenerica;
using System.Collections.Generic;

namespace CapaNegocio
{
    public class csInventarioNegocio : IGenerica<tbProductos>
    {

        //Instancia para llamar los metodos de CapaDatos
        csInventarioDatos inventario = new csInventarioDatos();

        public bool eliminar(tbProductos producto)
        {
            return inventario.eliminar(producto);
        }

        public bool guardar(tbProductos producto)
        {
            return inventario.guardar(producto);
        }

        public bool modificar(tbProductos producto)
        {
            return inventario.modificar(producto);
        }

        public List<tbProductos> obtenerlista(int producto)
        {
            return inventario.obtenerlista(producto);
        }
    }
    
}

using System.Collections.Generic;

namespace CapaUtilidades.IGenerica
{
    public interface IGenerica<E>
    {

        bool guardar(E producto);

        bool modificar(E producto);

        bool eliminar(E producto);

        List<E> obtenerlista(int producto);
    }
}
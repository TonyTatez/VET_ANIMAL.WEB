namespace TMS_MANTENIMIENTO.WEB.Models
{
    //public class CrearCiudad
    //{
    //    public string Descripcion { get; set; }

    //}

    //public class EditarCiudad
    //{
    //    public long Id { get; set; }
    //    public Crear
    //
    //    Anio { get; set; }
    //}

    public class ItemPais
    {
        public long idPais { get; set; }
        public string nombre { get; set; }
    }

    public class EliminarPais
    {
        public long Id { get; set; }
        public Boolean Activo { get; set; }
    }

    //public class ListaAnio
    //{
    //    public List<ItemAnio> ItemAnio { get; set; }
    //}

    public class PaisViewModel
    {
        public List<ItemPais> ItemPais { get; set; }
        public long idPais { get; set; }
        public string nombre { get; set; }
        //   public string tipoCiudad { get; set; }
    }
}
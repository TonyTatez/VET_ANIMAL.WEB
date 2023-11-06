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

    public class ItemCiudad
    {
        public long idCiudad { get; set; }
        public string descripcionCiudad { get; set; }
        public long? idProvincia { get; set; }
    }

    public class EliminarCiudad
    {
        public long Id { get; set; }
        public Boolean Activo { get; set; }
    }

    //public class ListaAnio
    //{
    //    public List<ItemAnio> ItemAnio { get; set; }
    //}

    public class ItemProvincias
    {
        public long? idProvincia { get; set; }
        public string descripcion { get; set; }
    }

    public class CiudadViewModel
    {
        public long? idProvincia { get; set; }
        public List<ItemCiudad> ItemCiudad { get; set; }
        public List<ItemProvincias> ListaProvincias { get; set; }
        public long idCiudad { get; set; }
        public string descripcionCiudad { get; set; }
        //   public string tipoCiudad { get; set; }
    }
}
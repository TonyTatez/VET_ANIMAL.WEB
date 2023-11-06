namespace TMS_MANTENIMIENTO.WEB.Models
{
    //public class CrearAnio
    //{
    //    public string Descripcion { get; set; }

    //}

    //public class EditarAnio
    //{
    //    public long Id { get; set; }
    //    public CrearAnio Anio { get; set; }
    //}

    public class ItemAnio
    {
        public long idAnio { get; set; }
        public long descripcion { get; set; }
    }

    public class EliminarAnio
    {
        public long Id { get; set; }
        public Boolean Activo { get; set; }
    }

    //public class ListaAnio
    //{
    //    public List<ItemAnio> ItemAnio { get; set; }
    //}

    public class AnioViewModel
    {
        public List<ItemAnio> ItemAnio { get; set; }
        public long IdAnio { get; set; }
        public long Descripcion { get; set; }
        //   public string tipoAnio { get; set; }
    }
}
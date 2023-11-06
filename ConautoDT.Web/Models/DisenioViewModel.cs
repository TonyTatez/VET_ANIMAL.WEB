namespace TMS_MANTENIMIENTO.WEB.Models
{
    public class CrearDisenio
    {
        public string Descripcion { get; set; }
    }

    public class EditarDisenio
    {
        public long Id { get; set; }
        public CrearDisenio Disenio { get; set; }
    }

    public class ItemDisenio
    {
        public long idDisenio { get; set; }
        public string descripcion { get; set; }
        public long? idCatalogo { get; set; }
        public string tipoDisenio { get; set; }
    }

    public class EliminarDisenio
    {
        public long Id { get; set; }
        public Boolean Activo { get; set; }
    }

    public class DisenioViewModel
    {
        public long idDisenio { get; set; }
        public string descripcion { get; set; }
    }
}
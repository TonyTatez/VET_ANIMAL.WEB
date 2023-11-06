namespace TMS_MANTENIMIENTO.WEB.Models
{
    public class CrearEstado
    {
        public string Descripcion { get; set; }
    }

    public class EditarEstado
    {
        public long Id { get; set; }
        public CrearEstado Estado { get; set; }
    }

    public class ItemEstado
    {
        public long idEstado { get; set; }
        public string descripcion { get; set; }
        public long? idCatalogo { get; set; }
        public string tipoEstado { get; set; }
    }

    public class EliminarEstado
    {
        public long Id { get; set; }
        public Boolean Activo { get; set; }
    }

    public class EstadoViewModel
    {
        public long idEstado { get; set; }
        public string descripcion { get; set; }
    }
}
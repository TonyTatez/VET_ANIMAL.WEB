namespace TMS_MANTENIMIENTO.WEB.Models
{
    public class CrearTipo
    {
        public string Descripcion { get; set; }
    }

    public class EditarTipo
    {
        public long Id { get; set; }
        public CrearTipo Tipo { get; set; }
    }

    public class ItemTipo
    {
        public long idTipo { get; set; }
        public string descripcion { get; set; }
        public long? idCatalogo { get; set; }
        public string tipoTipo { get; set; }
    }

    public class EliminarTipo
    {
        public long Id { get; set; }
        public Boolean Activo { get; set; }
    }

    public class TipoViewModel
    {
        public long idTipo { get; set; }
        public string descripcion { get; set; }
    }
}
namespace TMS_MANTENIMIENTO.WEB.Models
{
    public class CrearMedida
    {
        public string Descripcion { get; set; }
    }

    public class EditarMedida
    {
        public long Id { get; set; }
        public CrearMedida Medida { get; set; }
    }

    public class ItemMedida
    {
        public long idMedida { get; set; }
        public string descripcion { get; set; }
        public long? idCatalogo { get; set; }
        public string tipoMedida { get; set; }
    }

    public class EliminarMedida
    {
        public long Id { get; set; }
        public Boolean Activo { get; set; }
    }

    public class MedidaViewModel
    {
        public long idMedida { get; set; }
        public string descripcion { get; set; }
    }
}
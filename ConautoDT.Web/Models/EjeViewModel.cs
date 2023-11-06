namespace TMS_MANTENIMIENTO.WEB.Models
{
    public class CrearEje
    {
        public string Descripcion { get; set; }
    }

    public class EditarEje
    {
        public long Id { get; set; }
        public CrearEje Eje { get; set; }
    }

    public class ItemEje
    {
        public long idEje { get; set; }
        public string descripcion { get; set; }
        public long? idCatalogo { get; set; }
        public string tipoEje { get; set; }
    }

    public class EliminarEje
    {
        public long Id { get; set; }
        public Boolean Activo { get; set; }
    }

    public class EjeViewModel
    {
        public long idEje { get; set; }
        public string descripcion { get; set; }
    }
}
namespace TMS_MANTENIMIENTO.WEB.Models
{
    public class CrearModelo
    {
        public string Descripcion { get; set; }
    }

    public class EditarModelo
    {
        public long Id { get; set; }
        public CrearModelo Modelo { get; set; }
    }

    public class ItemModelo
    {
        public long idModelo { get; set; }
        public string descripcion { get; set; }
    }

    public class EliminarModelo
    {
        public long Id { get; set; }
        public Boolean Activo { get; set; }
    }

    public class ModeloViewModel
    {
        public long idModelo { get; set; }
        public string descripcion { get; set; }
    }
}
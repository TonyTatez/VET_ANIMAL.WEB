namespace TMS_MANTENIMIENTO.WEB.Models
{
    public class CrearMarca
    {
        public string Descripcion { get; set; }
    }

    public class EditarMarca
    {
        public long Id { get; set; }
        public CrearMarca Marca { get; set; }
    }

    public class ItemMarca
    {
        public long idMarca { get; set; }
        public string descripcion { get; set; }
    }

    public class EliminarMarca
    {
        public long Id { get; set; }
        public Boolean Activo { get; set; }
    }

    public class MarcaViewModel
    {
        public long idMarca { get; set; }
        public string descripcion { get; set; }
    }
}
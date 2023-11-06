namespace TMS_MANTENIMIENTO.WEB.Models
{
    public class CrearChofer
    {
        public string Identificacion { get; set; }
        public string Nombres { get; set; }
    }

    public class EditarChofer
    {
        public long Id { get; set; }
        public CrearChofer Chofer { get; set; }
    }

    public class ItemChofer
    {
        public long idChofer { get; set; }
        public string identificacion { get; set; }
        public string nombres { get; set; }
    }

    public class EliminarChofer
    {
        public long Id { get; set; }
        public Boolean Activo { get; set; }
    }

    public class ListaChofer
    {
        public List<ItemChofer> ItemChofer { get; set; }
    }

    public class ChoferViewModel
    {
        public List<ItemChofer> ListaChoferes { get; set; }
        public long idChofer { get; set; }
        public string identificacion { get; set; }
        public string nombres { get; set; }
    }
}
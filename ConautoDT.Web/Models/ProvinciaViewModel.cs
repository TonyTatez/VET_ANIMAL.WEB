namespace TMS_MANTENIMIENTO.WEB.Models
{
    //public class CrearColor
    //{
    //    public string Descripcion { get; set; }

    //}

    //public class EditarColor
    //{
    //    public long Id { get; set; }
    //    public CrearColor Color { get; set; }
    //}

    public class ItemProvincia
    {
        public long idProvincia { get; set; }
        public string descripcion { get; set; }
        public long? idPais { get; set; }
    }

    public class EliminarProvincia
    {
        public long Id { get; set; }
        public Boolean Activo { get; set; }
    }

    public class ItemPaises
    {
        public long idPais { get; set; }
        public string nombre { get; set; }
    }

    //public class ListaColor
    //{
    //    public List<ItemColor> ItemColor { get; set; }
    //}

    public class ProvinciaViewModel
    {
        public long? idPais { get; set; }
        public List<ItemPaises> ListaPaises { get; set; }
        public List<ItemProvincia> ItemProvincia { get; set; }
        public long idProvincia { get; set; }
        public string descripcion { get; set; }
        //   public string tipoColor { get; set; }
    }
}
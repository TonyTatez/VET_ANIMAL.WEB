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

    public class ItemColor
    {
        public long idColor { get; set; }
        public string descripcion { get; set; }
    }

    public class EliminarColor
    {
        public long Id { get; set; }
        public Boolean Activo { get; set; }
    }

    //public class ListaColor
    //{
    //    public List<ItemColor> ItemColor { get; set; }
    //}

    public class ColorViewModel
    {
        public List<ItemColor> ItemColor { get; set; }
        public long idColor { get; set; }
        public string descripcion { get; set; }
        //   public string tipoColor { get; set; }
    }
}
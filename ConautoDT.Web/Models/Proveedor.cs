namespace TMS_MANTENIMIENTO.WEB.Models
{
    public class CrearProveedor
    {
        public string Ruc { get; set; }
        public string RazonSocial { get; set; }
        public long TipoProveedorId { get; set; }
        public virtual TipoProveedor TipoProveedor { get; set; }
    }

    public class TipoProveedor
    {
        public long Id { get; set; }
        public string Descripcion { get; set; }
    }

    public class EditarProveedor
    {
        public long Id { get; set; }
        public CrearProveedor Proveedor { get; set; }
    }

    public class ItemProveedor
    {
        public long Id { get; set; }
        public string Ruc { get; set; }
        public string RazonSocial { get; set; }
        public int TipoProveedorId { get; set; }
        public virtual TipoProveedor TipoProveedor { get; set; }
    }

    public class EliminarProveedor
    {
        public long Id { get; set; }
        public Boolean Activo { get; set; }
    }

    public class ListaProveedor
    {
        public List<ItemProveedor> ItemProveedor { get; set; }
    }
}
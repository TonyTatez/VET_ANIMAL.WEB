namespace TMS_MANTENIMIENTO.WEB.Models
{
    public class ProductoStockInicialViewModel
    {
        public ProductoStockInicialViewModel()
        {
            //InventarioStockInicialSwiss1 = new ICollection<InventarioStockInicialSwiss>();
        }

        public decimal? CostoPromedio { get; set; }
        public string Empaque { get; set; }
        public string LineaNegocio { get; set; }
        public string Marca { get; set; }
        public string Producto { get; set; }
        public string SKU { get; set; }
        public string Subtipo { get; set; }
        public string Tipo { get; set; }
        public long? Id { get; set; }
        public int stockIncial { get; set; }
        public ICollection<InventarioStockInicialSwiss> InventarioStockInicialSwiss { get; set; }
    }

    public class InventarioStockInicialSwiss
    {
        public long IdInventarioStockInicialSwiss { get; set; }
        public long? IdEmpresa { get; set; }
        public long? IdBodega { get; set; }
        public long? IdOficina { get; set; }
        public long? IdProducto { get; set; }
        public int Stock { get; set; }
        public int Reserva { get; set; }
        public decimal? CostoPromedio { get; set; }
        public string Empresa { get; set; }
        public string Bodega { get; set; }
        public string Oficina { get; set; }
        public string Producto { get; set; }
    }
}
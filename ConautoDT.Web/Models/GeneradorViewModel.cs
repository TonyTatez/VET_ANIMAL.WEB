namespace TMS_MANTENIMIENTO.WEB.Models
{
    public class ItemGenerador
    {
        public long idGenerador { get; set; }
        public long idMarca { get; set; }
        public string vin { get; set; }
        public long horometro { get; set; }
        public string placa { get; set; }
        public ItemMarca marca { get; set; }
        public string descripcionMarca { get; set; }
    }

    public class EliminarGenerador
    {
        public long Id { get; set; }
        public Boolean Activo { get; set; }
    }

    public class GeneradorViewModel
    {
        public List<ItemGenerador> ItemGenerador { get; set; }
        public long idGenerador { get; set; }
        public long idMarca { get; set; }
        public string vin { get; set; }
        public long horometro { get; set; }
        public string placa { get; set; }
        public ItemMarca marca { get; set; }
        public string descripcionMarca { get; set; }
        public List<ItemMarca> ListaMarca { get; set; }
        //   public string tipoGenerador { get; set; }
    }
}
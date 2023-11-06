namespace TMS_MANTENIMIENTO.WEB.Models
{
    //public class CrearChasis
    //{
    //    public string Descripcion { get; set; }

    //}

    //public class EditarChasis
    //{
    //    public long Id { get; set; }
    //    public CrearChasis Chasis { get; set; }
    //}
    public class ItemChasisTipo
    {
        public long chasisId { get; set; }
        public string placa { get; set; }
        public string vin { get; set; }
        public string tamanio { get; set; }
        public string placaAntigua { get; set; }
        public long idMarcaChasis { get; set; }
        public string descipcionMarca { get; set; }
    }

    public class ItemChasis
    {
        public long idChasis { get; set; }
        public string placa { get; set; }
        public string vin { get; set; }
        public string tamanio { get; set; }
        public string placaAntigua { get; set; }
        public long idMarcaChasis { get; set; }
        public string descipcionMarca { get; set; }
    }

    public class EliminarChasis
    {
        public long Id { get; set; }
        public Boolean Activo { get; set; }
    }

    public class ChasisViewModel
    {
        public List<ItemChasis> ItemChasis { get; set; }
        public long idChasis { get; set; }
        public string placa { get; set; }
        public string vin { get; set; }
        public string tamanio { get; set; }
        public string placaAntigua { get; set; }
        public long idMarcaChasis { get; set; }

        public List<ItemMarca> ListaMarca { get; set; }
    }
}
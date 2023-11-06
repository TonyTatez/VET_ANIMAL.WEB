namespace TMS_MANTENIMIENTO.WEB.Models
{
    //public class CrearPlaca
    //{
    //    public string Descripcion { get; set; }

    //}

    //public class EditarPlaca
    //{
    //    public long Id { get; set; }
    //    public CrearPlaca Placa { get; set; }
    //}

    public class ItemCabezalTipoVehiculo
    {
        public long cabezalId { get; set; }
        public string placa { get; set; }
        public string descripcion { get; set; }

        public long? idModelo { get; set; }
        public long idMarca { get; set; }
        public string descripcionModelo { get; set; }
        public string descripcionMarca { get; set; }
    }

    public class ItemCabezal
    {
        public long idCabezal { get; set; }
        public string placa { get; set; }
        public string descripcion { get; set; }

        public long? idModelo { get; set; }
        public long idMarca { get; set; }
        public string descripcionModelo { get; set; }
        public string descripcionMarca { get; set; }
    }

    public class EliminarCabezal
    {
        public long idCabezal { get; set; }
        public Boolean Activo { get; set; }
    }

    //public class ListaPlaca
    //{
    //    public List<ItemPlaca> ItemPlaca { get; set; }
    //}

    public class CabezalViewModel
    {
        public List<ItemCabezal> ItemCabezal { get; set; }
        public long idCabezal { get; set; }
        public string placa { get; set; }
        public long idModelo { get; set; }
        public long idMarca { get; set; }

        public List<ItemModelo> ListaModelos { get; set; }

        public List<ItemMarca> ListaMarcas { get; set; }
    }
}
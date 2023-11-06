namespace TMS_MANTENIMIENTO.WEB.Models
{
    public class VehiculoViewModel
    {
        public long idVehiculo { get; set; }
        public long idTipoVehiculo { get; set; }
        public string tipoVehiculo { get; set; }
        public long? idCabezal { get; set; }
        public long? idChasis { get; set; }

        public int numeroLlantas { get; set; }
        public int numeroLlantasAsignadas { get; set; }
        public int numeroLlantasEstructura { get; set; }
        public long idTipoEstructura { get; set; }
        public string tipoEstructura { get; set; }
        public long? idColor { get; set; }
        public string color { get; set; }
        public long? idAnio { get; set; }
        public long? anio { get; set; }
        public long? kilometraje { get; set; }
        public long? kilometrajeAcumulado { get; set; }
        public long? idEstadoVehiculo { get; set; }
        public string estadoVehiculo { get; set; }
        public string placa { get; set; }
        public string vin { get; set; }
        public string tamanio { get; set; }
        public string placaAntigua { get; set; }
        public long? idMarcaChasis { get; set; }
        public string marcaChasis { get; set; }
        public long? idMarcaCabezal { get; set; }
        public string marcaCabezal { get; set; }
        public long? idModeloCabezal { get; set; }
        public string modeloCabezal { get; set; }

        public string image { get; set; }
    }

    //public class ItemVehiculo
    //{
    //    public string vin { get; set; }
    //    public string tamanio { get; set; }
    //    public string placaAntigua { get; set; }
    //    public long idVehiculo { get; set; }
    //    public long tipoVehiculoId { get; set; }
    //    public long? cabezalId { get; set; }
    //    public long? chasisId { get; set; }
    //    public long? descripcionDanio { get; set; }
    //    public int? numeroLlantas { get; set; }
    //    public string descripcionPlacaVehiculo { get; set; }
    //    public long? tipoEstructuraId { get; set; }
    //    public string image { get; set; }
    //    public string descripcionTipoVehiculo { get; set; }
    //    public string descripcionMarca { get; set; }
    //    public string descripcionModelo { get; set; }
    //    public string descripcionTipoEstructura { get; set; }
    //    public string descripcionColor { get; set; }
    //    public long? idMarcaCabezal { get; set; }
    //    public long? idModeloCabezal { get; set; }
    //    public long? danioId { get; set; }
    //    public long? colorId { get; set; }
    //    public long? idMarcaChasis { get; set; }
    //}

    public class ItemModeloCabezal
    {
        public long idModelo { get; set; }
        public string descripcion { get; set; }
    }

    public class ItemMarcaGenerador
    {
        public long idMarca { get; set; }
        public string descripcion { get; set; }
    }

    public class ItemMarcaChasis
    {
        public long idMarca { get; set; }
        public string descripcion { get; set; }
    }

    public class PosicionLlantaBusqueda
    {
        public long vehiculoId { get; set; }
        public long posicionLlanta { get; set; }
    }

    public class ListaVehiculo
    {
        public string VinChasis { get; set; }
        public List<ItemMarcaGenerador> ListaMarcaGenerador { get; set; }
        public List<ItemCabezal> ListaCabezales { get; set; }
        public List<ItemModeloCabezal> ListaModeloCabezales { get; set; }
        public long idModeloCabezales { get; set; }
        public long ColorId { get; set; }
        public long numerosTotalTipo { get; set; }
        public long Id { get; set; }
        public long TipoVehiculoId { get; set; }
        public long? cabezalId { get; set; }
        public long? chasisId { get; set; }
        public long? ChoferId { get; set; }
        public int NumeroLlantas { get; set; }
        public long idMarcaCabezal { get; set; }
        public long idAnio { get; set; }
        public string horometro { get; set; }
        public string VinCabezal { get; set; }
        public string tamanio { get; set; }
        public string placaAntigua { get; set; }
        public long Kilometraje { get; set; }
        public long KilometrajeAcumulado { get; set; }
        public long idMarcaChasis { get; set; }
        public long TipoEstructuraId { get; set; }
        public long TipoMarcaCabezal { get; set; }
        public string image { get; set; }
        public string DescripcionChasis { get; set; }
        public string DescripcionCabezal { get; set; }
        public string DescripcionGenerador { get; set; }
        public List<TipoViewModel> ListaTipoVehiculo { get; set; }
        public List<TipoEstructuraViewModel> ListaTipoEstructura { get; set; }
        public List<ItemMarca> ListaMarca { get; set; }
        public List<ItemChasis> ListaChasis { get; set; }
        public List<ItemCabezal> ListaPlaca { get; set; }
        public List<ItemModelo> ListaModelo { get; set; }
        public List<DisenioViewModel> ListaItemDiseno { get; set; }
        public List<ItemMedida> ListaMedidaLlanta { get; set; }
        public List<ItemAnio> ListaAnio { get; set; }
        public List<ItemMarcaChasis> ListaChasisMarca { get; set; }
        public List<VehiculoViewModel> ListaVehiculos { get; set; }
    }

    public class GetVehiculoAnterior
    {
        public long? CabezalId { get; set; }
        public long? ChasisId { get; set; }
    }

    public class GetVehiculo
    {
        public long IdVehiculo { get; set; }
        public long IdTipoVehiculo { get; set; }
    }
}
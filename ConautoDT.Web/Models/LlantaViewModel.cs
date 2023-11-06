using System.ComponentModel.DataAnnotations;

namespace TMS_MANTENIMIENTO.WEB.Models
{
    public class CrearLlanta
    {
        public long Termico { get; set; }
        public long DisenoId { get; set; }
        public long MedidaId { get; set; }
        public string Original { get; set; }
        public long MarcaId { get; set; }
        public long EjeId { get; set; }
        public string UltimaInspeccion { get; set; }
        public DisenioViewModel Diseno { get; set; }
        public virtual ItemMedida Medida { get; set; }
        public virtual ItemMarca Marca { get; set; }
        public virtual ItemEje Eje { get; set; }
        public virtual List<ItemInspeccionLlanta> ListaInspeccionLlanta { get; set; }
    }

    public class EditarLlanta
    {
        public long Id { get; set; }
        public CrearLlanta Llanta { get; set; }
    }

    public class ItemLlantaData
    {
        public long idLlantas { get; set; }
        public long termico { get; set; }
        public long disenoId { get; set; }
        public long medidaId { get; set; }
        public string original { get; set; }
        public long marcaId { get; set; }
        public long ejeId { get; set; }
        public long vehiculoId { get; set; }
    }

    public class ItemTermico
    {
        public long idLlantas { get; set; }
        public long termico { get; set; }
        public string valor { get; set; }
        public long disenoId { get; set; }
        public long medidaId { get; set; }
        public string original { get; set; }
        public long marcaId { get; set; }
        public long ejeId { get; set; }
        public long vehiculoId { get; set; }
        public long ultimaInspeccion { get; set; }
        public virtual string diseño { get; set; }
        public virtual string medidas { get; set; }
        public virtual string marcas { get; set; }
        public virtual string eje { get; set; }
        public virtual string vehiculo { get; set; }
        public virtual long listaInspeccionLlanta { get; set; }
    }

    public class ItemPosicionLlanta
    {
        public long posicionActual { get; set; }
    }

    public class ItemLlanta
    {
        public long disenioAnterior { get; set; }

        public long disenioSiguiente { get; set; }

        public long numeroPosicion { get; set; }
        public long marcaAnterior { get; set; }
        public long marcaSiguiente { get; set; }
        public long? disenoId { get; set; }
        public long? medidaId { get; set; }
        public long? idTipoLlanta { get; set; }
        public long? idTipoVehiculo { get; set; }
        public long? idLlantas { get; set; }
        public long? termico { get; set; }
        public long? idEje { get; set; }
        public long? idMarca { get; set; }
        public long? idVehiculo { get; set; }
        public long? idDisenio { get; set; }
        public long? idMedida { get; set; }
        public long? idCabezal { get; set; }
        public long? idChasis { get; set; }
        public long? idEstado { get; set; }
        public string original { get; set; }
        public string estado { get; set; }
        public string tipoLlanta { get; set; }
        public long? idMarcaCabezal { get; set; }
        public long? idMarcaChasis { get; set; }

        public string posicion { get; set; }

        public decimal mmOriginal { get; set; }
        public long? marcaId { get; set; }
        public long? ejeId { get; set; }
        public long? vehiculoId { get; set; }
        public DateTime? fechaUltimaInspeccion { get; set; }
        public virtual string diseño { get; set; }
        public virtual string medidas { get; set; }
        public virtual string marcas { get; set; }
        public virtual string eje { get; set; }
        public virtual string vehiculo { get; set; }
        public virtual long listaInspeccionLlanta { get; set; }
        public long? posicionActual { get; set; }
    }

    public class EliminarLlanta
    {
        public long Id { get; set; }
        public Boolean Activo { get; set; }
    }

    public class ListaLlantaData
    {
    }

    public class itemLLantaInspeccionId
    {
        public long vehiculoId { get; set; }
        public long llantaId { get; set; }
        public long termico { get; set; }
        public long idIdLlantasInspecciones { get; set; }
        public long idLlanta { get; set; }
        public decimal mM1 { get; set; }
        public decimal mM2 { get; set; }
        public decimal mM3 { get; set; }
        public decimal mM4 { get; set; }
        public string fechaInspeccion { get; set; }
        public decimal mmOriginal { get; set; }
        public decimal mmMinimo { get; set; }
        public decimal mmRetiro { get; set; }
        public decimal porcDesgaste { get; set; }

        public string observaciones { get; set; }
    }

    public class itemLLantaInspeccionViewModel
    {
        public string placa { get; set; }

        public long termico { get; set; }
        public decimal mM1 { get; set; }
        public decimal mM2 { get; set; }
        public decimal mM3 { get; set; }
        public decimal mM4 { get; set; }
        public string fechaInspeccion { get; set; }

        public string estadoInspeccion { get; set; }
        public string tipoVehiculo { get; set; }
        public string posicion { get; set; }
        public string marca { get; set; }

        public string medida { get; set; }

        public string disenio { get; set; }

        //   public string ciudad { get; set; }

        public decimal mmOriginal { get; set; }
        public decimal mmMinimo { get; set; }
        public decimal mmRetiro { get; set; }
        public decimal porcDesgaste { get; set; }

        public string observaciones { get; set; }
    }

    public class itemVehiculoSeleccionado
    {
        public long vehiculoId { get; set; }
        public long tipoId { get; set; }
    }

    public class itemBusquedaInspeccion
    {
        public long vehiculoId { get; set; }
        public long llantaId { get; set; }
        public long termico { get; set; }

        public string fechaDesde { get; set; }

        public string fechaHasta { get; set; }
    }

    public class TermicoDesechar
    {
        public long termico { get; set; }
    }

    public class RotacionLLantaTermico
    {
        public long Termico1 { get; set; }
        public long Termico2 { get; set; }
        public long IdCabeceraMontaje { get; set; }

        public string TipoEstado { get; set; }
        public long posicionLlanta { get; set; }

        public long? cabezalId { get; set; }

        public long? chasisId { get; set; }
        public long tipoVehiculoId { get; set; }

        public long MMoriginal { get; set; }

        public long tipoLlanta { get; set; }

        public long medidaId { get; set; }

        public long marcaId { get; set; }

        public long disenioId { get; set; }

        public long ejeId { get; set; }
    }

    public class itemMontajeLlantaBusquedaTermicos
    {
        public long tipoVehiculo { get; set; }
        public long? cabezalId { get; set; }
        public long? chasisId { get; set; }

        public long termico { get; set; }

        public long posicionActual { get; set; }

        public long idVehiculo { get; set; }
    }

    public class LlantaDisponibleTermico
    {
        public long Termico { get; set; }
    }


    public class DatosTermicosLlantas
    {
        public long idLlantas { get; set; }

        public long idDisenio { get; set; }

        public long idEjeLlanta { get; set; }

        public string placa { get; set; }

        public decimal mmOriginal { get; set; }

        public long idMarcaLlanta { get; set; }

        
    public long idMedidaLlanta { get; set; }

    }


    public class itemMontajeTermico
    {
        public long termico { get; set; }

        public long posicionActual { get; set; }

        public long idVehiculo { get; set; }

        public string estado { get; set; }

        public int numeroLLantasAsignadas { get; set; }

        public long idCabeceraMontaje { get; set; }
    }

    public class itemRotacionIdCabecera
    {
        public long idCabeceraMontaje { get; set; }
    }

    public class itemTipoLlanta
    {
        public long vehiculoId { get; set; }
        public long tipoId { get; set; }
    }

    public class itemCiudad
    {
        public long idCiudad { get; set; }
        public long? idProvincia { get; set; }

        [MaxLength(100)]
        public string descripcionCiudad { get; set; }

        [MaxLength(100)]
        public string descripcionProvincia { get; set; }
    }

    public class ListaLlanta
    {


        
        public List<LlantaDisponibleTermico> LlantaDisponibleTermicoLista { get; set; }
        public List<ItemCabezalTipoVehiculo> ListaCabezalTipo { get; set; }
        public List<DatosTermicosLlantas> DatosTermicosLlantasListaf { get; set; }
        public DatosTermicosLlantas DatosTermicosLlantasLista { get; set; }
        public List<itemLLantaInspeccionViewModel> ListaInspeccionView { get; set; }

        public List<itemMontajeLlantaBusquedaTermicos> ListaMontajeLlantaBusquedaTermicos { get; set; }

        public List<itemMontajeTermico> ListaTraerTermico { get; set; }
        public List<ItemChasisTipo> ListaChasisTipo { get; set; }
        public List<itemTipoLlanta> ListaTipoLlantaVersion2 { get; set; }
        public FiltroReporteInspeccion Filtro { get; set; }
        public long TipoLLantaId { get; set; }
        public long idMarcaChasis { get; set; }
        public long idCiudad { get; set; }
        public long Id { get; set; }
        public long Termico { get; set; }
        public long? Kilometraje { get; set; }
        public long TermicoReencauche { get; set; }
        public string Termicos { get; set; }
        public string DisenoNombre { get; set; }
        public string MedidaNombre { get; set; }
        public string MarcaNombre { get; set; }
        public string EjeNombre { get; set; }
        public string VehiculoNombre { get; set; }

        public string TipoLlantaNombre { get; set; }
        public long TipoVehiculoId { get; set; }
        public long CabezalId { get; set; }

        public long TipoLlantaId { get; set; }
        public long ChasisId { get; set; }
        public string MedidaNombreLlanta { get; set; }

        public long? idMarcaCabezal { get; set; }
        public String Observaciones { get; set; }

        public String PosicionTextoReencauche { get; set; }

        public long DisenoId { get; set; }
        public long MedidaId { get; set; }
        public string Original { get; set; }
        public long MarcaId { get; set; }
        public long EjeId { get; set; }
        public long EstadoId { get; set; }
        public long EstadoInspeccionId { get; set; }
        public long VehiculoId { get; set; }
        public long UltimaInspeccion { get; set; }

        public long DisenoLlantaId { get; set; }
        public long MarcaLlantaId { get; set; }
        public DateTime? FechaInspeccion { get; set; }
        public string FechaReencauche { get; set; }
        public long idLlantasInspecciones { get; set; }
        public long idLlanta { get; set; }
        public long mm1 { get; set; }
        public long mm2 { get; set; }
        public long mm3 { get; set; }
        public long mm4 { get; set; }
        public long numeroPosicion { get; set; }
        public long marcaAnterior { get; set; }
        public long marcaSiguiente { get; set; }

        public string PlacaDescrcion { get; set; }
        public long disenioAnterior { get; set; }

        public long disenioSiguiente { get; set; }

        public string MarcaLlanta { get; set; }
        public string MedidaLlanta { get; set; }
        public string DisenioLlanta { get; set; }

        public string TipoVehiculoDescripcion { get; set; }

        public string EstadoDescripcion { get; set; }
        public string DisenioNombreAnterior { get; set; }
        public string MarcaNombreAnterior { get; set; }

        public long mmRetiro { get; set; }
        public long mmMinimo { get; set; }

        public long porcentajeDesgaste { get; set; }

        public long secuencia { get; set; }
        public long PosicionAtual { get; set; }

        public long idLlantavehiculo { get; set; }

        public string operacion { get; set; }

        /// <summary>
        ///  public long VehiculoLlantaId { get; set; }
        /// </summary>

        public List<itemLLantaInspeccionId> ListaLlantaInspeccionUltimo { get; set; }

        public List<itemVehiculoSeleccionado> ListaVehiculoSeleccionado { get; set; }
        public List<ItemLlanta> ItemLlanta { get; set; }
        public List<ItemTermico> ItemTermico { get; set; }
        public List<DisenioViewModel> ListaItemDiseno { get; set; }
        public List<ItemMedida> ListaMedidaLlanta { get; set; }
        public List<ItemMarca> ListaMarca { get; set; }
        public List<TipoViewModel> ListaTipoLlanta { get; set; }
        public List<ItemEje> ListaEje { get; set; }
        public List<ItemChasis> ListaChasis { get; set; }
        public List<ItemPosicionLlanta> ListaPosicionLlanta { get; set; }

        public List<ItemTermico> ListaTermico { get; set; }
        public List<ItemCabezal> ListaPlaca { get; set; }
        public List<ItemModelo> ListaModelo { get; set; }
        public List<ItemEstado> ListaEstado { get; set; }
        public List<TipoViewModel> ListaTipoVehiculo { get; set; }
        public List<itemCiudad> ListaCiudad { get; set; }
        public List<HistoricoInspeccionLlantaViewModel> ListaInspeccion { get; set; }

        public List<ItemEstado> ListaInspeccionLlanta { get; set; }

        public List<ItemMarcaChasis> ListaChasisMarca { get; set; }

        //  public List<ItemVehiculo> ListaVehiculoLlanta { get; set; }
    }

    public class CrearInspeccionLlanta
    {
        public long LlantaId { get; set; }
        public long TipoId { get; set; }
        public long MM1 { get; set; }
        public long MM2 { get; set; }
        public long MM3 { get; set; }
        public long MM4 { get; set; }
        public long DisenoId { get; set; }
        public long MarcaId { get; set; }
        public DateTime FechaInspeccion { get; set; }
        public long VehiculoId { get; set; }
        public TipoViewModel Tipo { get; set; }
        public DisenioViewModel Diseno { get; set; }
        public virtual ItemMarca Marca { get; set; }
        public virtual ItemLlanta Llanta { get; set; }
    }

    public class EditarInspeccionLlanta
    {
        public long Id { get; set; }
        public long LlantaId { get; set; }
        public CrearInspeccionLlanta Llanta { get; set; }
    }

    public class ItemInspeccionLlanta
    {
        public long LlantaId { get; set; }
        public long Id { get; set; }
        public long TipoId { get; set; }
        public long MM1 { get; set; }
        public long MM2 { get; set; }
        public long MM3 { get; set; }
        public long MM4 { get; set; }
        public long DisenoId { get; set; }
        public long MarcaId { get; set; }
        public DateTime FechaInspeccion { get; set; }
        public long VehiculoId { get; set; }
        public TipoViewModel Tipo { get; set; }
        public DisenioViewModel Diseno { get; set; }
        public virtual ItemMarca Marca { get; set; }
        public virtual ItemLlanta Llanta { get; set; }
    }

    public class EliminarInspeccionLlanta
    {
        public long Id { get; set; }
        public long LlantaId { get; set; }
        public Boolean Activo { get; set; }
    }

    public class LlantaModeloApi
    {
        public long IdLlantas { get; set; }
        public string Termico { get; set; }
        public long DisenoId { get; set; }
        public long MedidaId { get; set; }
        public string Original { get; set; }
        public long MarcaId { get; set; }
        public long EjeId { get; set; }
        public long TipoVehiculoId { get; set; }
        public long TipoEstructuraId { get; set; }
        public long MarcaVehiculoId { get; set; }
        public string Placa { get; set; }
        public long VehiculoId { get; set; }
    }

    public class LlantaInformacionApi
    {
        public long idLlantas { get; set; }
        public long termico { get; set; }
        public string medida { get; set; }
        public string original { get; set; }
        public string marca { get; set; }
        public string eje { get; set; }
        public string ultimaInspeccion { get; set; }
        public string estadoLlanta { get; set; }
        public string disenioLlanta { get; set; }

        public string marcaVehiculo { get; set; }

    }

    public class HistoricoInspeccionLlantaViewModel
    {
        public long idHistoInspeLlanta { get; set; }
        public long idLlanta { get; set; }

        public string termico { get; set; }
        public long idVehiculo { get; set; }

        public string placa { get; set; }
        public long idEstadoLLanta { get; set; }

        public string estadoLlanta { get; set; }
        public long idTipoVehiculo { get; set; }

        public string tipoVehiculo { get; set; }
        public int numeroPosicion { get; set; }

        public string posicion { get; set; }
        public long idLLantaInspe { get; set; }
        public DateTime fechaInspeccion { get; set; }
        public long idMarcaLLanta { get; set; }

        public string marcaLlanta { get; set; }
        public long idMedidaLLanta { get; set; }

        public string medidaLlanta { get; set; }
        public long idDisenioLLanta { get; set; }

        public string disenioLlanta { get; set; }
    }

    public class ReporteExcelInspeccionLlanta
    {
        public long idHistoInspeLlanta { get; set; }
        public string termico { get; set; }
        public string placa { get; set; }
        public string estadoLlanta { get; set; }
        public string tipoVehiculo { get; set; }
        public int numeroPosicion { get; set; }
        public string posicion { get; set; }
        public DateTime fechaInspeccion { get; set; }
        public string marcaLlanta { get; set; }
        public string medidaLlanta { get; set; }
        public string disenioLlanta { get; set; }
    }

    public class FiltroReporteInspeccion
    {
        public DateTime? desde { get; set; }
        public DateTime? hasta { get; set; }
        public string Termico { get; set; }
        public long IdTipoLlanta { get; set; }
    }

    public class CabeceraMontajeLlantaTableLista
    {
        public long idCabeceraMontaje { get; set; }
        public string codigoMontaje { get; set; }
        public string tipoVehiculoNombre { get; set; }
        public string vehiculoNombre { get; set; }
        public string idVehiculo { get; set; }

        public string idTipoVehiculo { get; set; }
        public DateTime fechaMontaje { get; set; }
    }

    public class DetalleMontajeLlantaTableLista
    {
        public long IdCabeceraMontaje { get; set; }
        public long idCabeceraDetalleMontaje { get; set; }

        //     public string placa { get; set; }

        //   public string tipoVehiculo { get; set; }

        public long numeroPosicion { get; set; }

        public string posicion { get; set; }

        public string descripcion { get; set; }

        public string termicoActual { get; set; }

        public string termicoAnterior { get; set; }

        public string tipoMontaje { get; set; }

        public DateTime fechaMontajeTipo { get; set; }
    }


    public class ListaMontajeLlanta
    {
        
       public List<LlantaInformacionApi> ListaLlantaInformacionApi { get; set; }


        public List<ItemEstado> ListaEstado { get; set; }
        public List<TipoViewModel> ListaTipoLlanta { get; set; }

        public List<CabeceraMontajeLlantaTableLista> CabeceraMontajeLlantaTableListas { get; set; }

        public List<DetalleMontajeLlantaTableLista> DetalleMontajeLlantaTableListalistas { get; set; }
        public long? CabezalId { get; set; }
        public long EstadoId { get; set; }
        public long IdLlanta { get; set; }
        public long Termico { get; set; }
        public string vin { get; set; }

        public List<ItemEje> ListaEje { get; set; }
        public List<ItemChasisTipo> ListaChasisTipo { get; set; }
        public long? placaIdTipoVehiculo { get; set; }
        public long danioId { get; set; }
        public long idModeloCabezal { get; set; }
        public string VinChasis { get; set; }
        public string Tamanio { get; set; }
        public List<ItemCabezalTipoVehiculo> ListaCabezalTipo { get; set; }
        public List<ItemMarcaGenerador> ListaMarcaGenerador { get; set; }
        public List<VehiculoViewModel> ListaVehiculos { get; set; }
        public List<ItemCabezal> ListaCabezales { get; set; }
        public List<ItemModeloCabezal> ListaModeloCabezales { get; set; }
        public long idMarca { get; set; }
        public long idModeloCabezales { get; set; }
        public long descripcionDanio { get; set; }
        public long ColorId { get; set; }
        public long numerosTotalTipo { get; set; }
        public long Id { get; set; }
        public long TipoVehiculoId { get; set; }
        public long? cabezalId { get; set; }
        public long? chasisId { get; set; }
        public long MarcaVehiculoId { get; set; }
        public long ModeloVehiculoId { get; set; }
        public long? ChoferId { get; set; }
        public int NumeroLlantas { get; set; }
        public long idCabezal { get; set; }
        public long idMarcaCabezal { get; set; }
        public long idAnio { get; set; }
        public long DisenoId { get; set; }
        public long MedidaId { get; set; }
        public string Original { get; set; }
        public long MarcaId { get; set; }
        public long EjeId { get; set; }
        public long VehiculoId { get; set; }
        public long UltimaInspeccion { get; set; }
        public string vingenerador { get; set; }
        public string Vingeneradores { get; set; }
        public string horometro { get; set; }
        public string placaGenerador { get; set; }

        public string VinCabezal { get; set; }
        public string tamanio { get; set; }
        public string placaAntigua { get; set; }
        public string MarcaGenerador { get; set; }
        public long TipoLlantaId { get; set; }
        public long Kilometraje { get; set; }
        public long KilometrajeAcumulado { get; set; }
        public string MarcaChasis { get; set; }
        public string TipoEstado { get; set; }
        public long idMarcaChasis { get; set; }
        public long IdMarcaGenerador { get; set; }
        public long DisenoLlantaId { get; set; }
        public long MarcaLlantaId { get; set; }
        public DateTime FechaInspeccion { get; set; }
        public long idLlantasInspecciones { get; set; }
        public long idCabeceraMontaje { get; set; }

        public List<ListaLlanta> LlantaItem { get; set; }
        public long PosicionLlantaVehiculoId { get; set; }

        //public virtual ItemCatalogo TipoVehiculo { get; set; }
        //public virtual ItemChasis Chasis { get; set; }
        //public virtual ItemMarca MarcaVehiculo { get; set; }
        //public virtual ItemModelo ModeloVehiculo { get; set; }
        //public virtual ItemChofer Chofer { get; set; }
        public long TipoEstructuraId { get; set; }

        public long TipoMarcaCabezal { get; set; }
        public long TipoMarcaChasis { get; set; }

        //public virtual ItemCatalogo TipoEstructura { get; set; }
        public string image { get; set; }

        public string DescripcionTipoVehiculo { get; set; }
        public string DescripcionChasis { get; set; }
        public string DescripcionCabezal { get; set; }

        public string descripcionColor { get; set; }
        public string DescripcionGenerador { get; set; }
        public string DescripcionMarcaCabezal { get; set; }
        public string DescripcionMarcaChasis { get; set; }
        public string DescripcionMarcaGenerador { get; set; }

        public string DescripcionModeloVehiculo { get; set; }
        public string DescripcionChofer { get; set; }
        public string DescripcionTipoEstructura { get; set; }
        public string DescripcionPlaca { get; set; }

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
        public List<ItemColor> ListaColor { get; set; }
    }
}
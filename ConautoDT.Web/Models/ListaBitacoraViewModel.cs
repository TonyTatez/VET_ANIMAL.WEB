namespace TMS_MANTENIMIENTO.WEB.Models
{
    public class ListaBitacoraViewModel
    {
        public class ListadoBitacoraViewModel
        {
            public ListadoBitacoraViewModel()
            {
                Ordenes = new List<itemBitacoraViewModel>();
            }

            public List<itemBitacoraViewModel> Ordenes { get; set; }

            public DateTime FechaDesde { get; set; }
            public DateTime FechaHasta { get; set; }
            public string Codigo { get; set; }
        }

        public class itemBitacoraViewModel
        {
            public long IdOrden { get; set; }
            public string OT { get; set; }
            public string CONSECUTIVO { get; set; }
            public string FECHARECEPCIÓN { get; set; }
            public string FECHAPROGRAMADA { get; set; }
            public string FECHAEJECUCION { get; set; }
            public string TECNICOASIGNADO { get; set; }
            public string CLIENTE { get; set; }
            public string VENDEDOR { get; set; }
            public string EQUIPO { get; set; }
            public string MARCA { get; set; }
            public string CIUDAD { get; set; }
            public string PROVINCIA { get; set; }
            public string TIPOTRABAJO { get; set; }
            public string OBSERVACIONES { get; set; }
            public string VEHICULO { get; set; }
            public string SECUENCIA { get; set; }
            public string MAÑANA { get; set; }
            public string TARDE { get; set; }
        }
    }
}
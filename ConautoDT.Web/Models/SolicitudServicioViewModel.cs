using System.ComponentModel.DataAnnotations;

namespace TMS_MANTENIMIENTO.WEB.Models
{
    public class SolicitudServicioViewModel
    {
        public class ServicioViewModel
        {
            public ServicioViewModel()
            {
                DetallesProductos = new List<DetalleItemsViewModel>();
            }

            public long IdOrden { get; set; }

            [Display(Name = "Número Factura")]
            public string NumeroFactura { get; set; }

            [Display(Name = "Fecha Solicitud")]
            public string FechaSolicitud { get; set; }

            [Display(Name = "Departamento")]
            public string Departamento { get; set; }

            [Display(Name = "Solicitado Por")]
            public string SolicitadoPor { get; set; }

            [Display(Name = "Razón Social Cliente")]
            public string RazonSocialCliente { get; set; }

            [Display(Name = "Direccion Asistencia")]
            public string Direccionasistencia { get; set; }

            [Display(Name = "Referencia")]
            public string Referencia { get; set; }

            [Display(Name = "Sector Ciudad")]
            public string SectorCiudad { get; set; }

            [Display(Name = "Teléfono")]
            public string Telefono { get; set; }

            [Display(Name = "Celular")]
            public string Celular { get; set; }

            [Display(Name = "Contactar con")]
            public string Contactarcon { get; set; }

            [Display(Name = "Email")]
            public string Email { get; set; }

            public DateTime FechaFactura { get; set; }
            public bool LogisticaEquiposEntregado { get; set; }
            public string Descripciotrabajorealizar { get; set; }

            public List<DetalleItemsViewModel> DetallesProductos { get; set; }
        }

        public class DetalleItemsViewModel
        {
            public DetalleItemsViewModel()
            {
                Trabajos = new List<TrabajoRealizarItemsViewModel>();
            }

            public string CodigoSKU { get; set; }
            public string Descripcion { get; set; }
            public string Marca { get; set; }
            public string TipoEquipo { get; set; }
            public string Modelo { get; set; }

            public List<TrabajoRealizarItemsViewModel> Trabajos { get; set; }
        }

        public class TrabajoRealizarItemsViewModel
        {
            public bool MantenimientoCorrectivo { get; set; }
            public bool Arranque { get; set; }
            public bool Garantia { get; set; }
            public bool MantenimientoPreventivo { get; set; }
            public bool Diagnostico { get; set; }
            public bool InspeccionInstalacion { get; set; }
            public bool PreArranque { get; set; }
            public bool Consignación { get; set; }
        }
    }
}
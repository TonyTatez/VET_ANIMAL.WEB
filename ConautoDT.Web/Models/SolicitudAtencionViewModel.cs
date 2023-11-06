using System.ComponentModel.DataAnnotations;
using static TMS_MANTENIMIENTO.WEB.Models.SolicitudServicioViewModel;

namespace TMS_MANTENIMIENTO.WEB.Models
{
    public class SolicitudAtencionViewModel
    {
        public class ServicioAtencionViewModel
        {
            public ServicioAtencionViewModel()
            {
                DetallesProductos = new List<DetalleItemsViewModel>();
            }

            public long IdOrden { get; set; }

            [Display(Name = "Estado")]
            public string EstadoSolicitud { get; set; }

            [Display(Name = "Fecha Agendamiento")]
            public DateTime FechaAgendamiento { get; set; }

            [Display(Name = "Técnico")]
            public string Tecnico { get; set; }

            [Display(Name = "Código Solicitud")]
            public string CodigoSolicitud { get; set; }

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
    }
}
using System.ComponentModel.DataAnnotations;
using TMS_MANTENIMIENTO.WEB.Models;

namespace VET_ANIMAL.WEB.Models
{
    public class ItemClientes
    {
        public long idCliente { get; set; }
        public string codigo { get; set; }
        public string identificacion { get; set; }
        public string nombres { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        [MaxLength(350)]
        public string direccion { get; set; }
        public long idMascota { get; set; }
    }

    public class EliminarClientes
    {
        public long Id { get; set; }
        public Boolean Activo { get; set; }
    }
    public class ItemMascotas
    {
        public long idMascota { get; set; }
        public string codigo { get; set; }
        public string nombreMascota { get; set; }
    }

    public class ListaClientes
    {
        public List<ItemClientes> ItemClientes { get; set; }
    }

    public class ClientesViewModel
    {
        public long? idMascota { get; set; }
        public List<ItemClientes> ListaClientes { get; set; }
        public List<ItemMascotas> ListaMascotas { get; set; }
        public long idCliente { get; set; }
        public string codigo { get; set; }
        public string identificacion { get; set; }
        public string nombres { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        [MaxLength(350)]
        public string direccion { get; set; }
        //   public string tipoCiudad { get; set; }
    }
}

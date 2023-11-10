using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VET_ANIMAL.WEB.Models
{

    public class ItemMascota
    {
        public long IdMascota { get; set; }
        public string Codigo { get; set; }
        public string NombreMascota { get; set; }
        public string Raza { get; set; }
        public string Sexo { get; set; }
        public float? Peso { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public long IdCliente { get; set; }

    }

    public class EliminarMascotas
    {
        public long Id { get; set; }
        public Boolean Activo { get; set; }
    }
    public class ItemCliente
    {
        public long idClientes { get; set; }
        public string codigo { get; set; }
        public string nombres { get; set; }
    }

    public class ListaMascotas
    {
        public List<ItemMascotas> ItemMascotas { get; set; }
    }
    public class MascotasViewModel
    {
            public long? IdCliente { get; set; }
            public List<ItemMascotas> ListaMascotas { get; set; }
            public List<ItemCliente> ListaClientes { get; set; }
            public long IdMascota { get; set; }
            public string Codigo { get; set; }
            public string NombreMascota { get; set; }
            public string Raza { get; set; }
            public string Sexo { get; set; }
            public float? Peso { get; set; }
            public DateTime? FechaNacimiento { get; set; }
            
            
        
    }
}

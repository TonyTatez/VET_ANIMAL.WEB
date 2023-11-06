namespace TMS_MANTENIMIENTO.WEB.Models
{
    public class ItemTipoEstructuras
    {
        public long IdTipoEstructura { get; set; }

        public string Descripcion { get; set; }
        public int NumeroLlantas { get; set; }

        public string UrlImagen { get; set; }

        public string Delanteras { get; set; }
        public string Posteriores { get; set; }

        public long idTipoEstructura { get; set; }

        public string descripcion { get; set; }
        public int numeroLlantas { get; set; }

        public string urlImagen { get; set; }

        public string delanteras { get; set; }
        public string posteriores { get; set; }
    }

    public class TipoEstructuraViewModel
    {
        public List<ItemTipoEstructuras> ItemTipoEstructura { get; set; }
        public long idTipoEstructura { get; set; }
        public long IdTipoEstructura { get; set; }

        public string Descripcion { get; set; }
        public int NumeroLlantas { get; set; }

        public string UrlImagen { get; set; }

        public string Delanteras { get; set; }
        public string Posteriores { get; set; }
        public string descripcion { get; set; }
        public int numeroLlantas { get; set; }

        public string urlImagen { get; set; }

        public string delanteras { get; set; }
        public string posteriores { get; set; }
    }
}
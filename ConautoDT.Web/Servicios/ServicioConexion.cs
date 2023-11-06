using System.Net.Http.Headers;

namespace TMS_MANTENIMIENTO.WEB.Servicios
{
    public class ServicioConexion
    {
        private static HttpClient client = new HttpClient();

        public HttpClient Conexion()
        {
            RepositorioProyectos proyecto = new RepositorioProyectos();

            var proyectoApi = proyecto.ObtenerProyectos().Where(x => x.Descripcion == "ApiMantenimiento").FirstOrDefault();
            client.BaseAddress = new Uri(proyectoApi.Link);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }
    }
}
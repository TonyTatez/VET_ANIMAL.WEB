using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NLog;
using RestSharp;
using System.Text;
using TMS_MANTENIMIENTO.WEB.Engines;
using TMS_MANTENIMIENTO.WEB.Models;
using TMS_MANTENIMIENTO.WEB.Servicios;
using Utf8Json;

namespace TMS_MANTENIMIENTO.WEB.Controllers
{
    public class LlantaController : Controller
    {
        private readonly IConfiguration configuration;

        private RestClient _apiClient;
        private RestClient _appAutogoClient;
        private static Logger _log = LogManager.GetLogger("Account");
      
        private string responseContent { get; }
        private AccountService _AccountService;
        private readonly ExcelFormatsHandler excelHandler = new ExcelFormatsHandler();
        private readonly GemboxReportingEngine _reportingEngine = new GemboxReportingEngine();

        public LlantaController(IConfiguration configuration)
        {
            this.configuration = configuration;
            _apiClient = new RestClient(configuration["APIClient"]);//RestClient(baseURL);
            //_apiClient.ThrowOnAnyError = true;
            //_apiClient.Timeout = 120000;
            //_apiClient.UseUtf8Json();
            _AccountService = new AccountService(configuration);
        }

        // GET: LlantaController
        public ActionResult Index()
        {
            TempData["menu"] = "";

            ListaLlanta model = new ListaLlanta();

            //List<ItemInspeccionLlanta> inspeccionesllanta = new List<ItemInspeccionLlanta>();
            //inspeccionesllanta.Add(new ItemInspeccionLlanta { Id=1, TipoId=1, MM1=17, MM2=17, MM3=16, MM4=18, DisenoId= 1, Llanta = null,
            //                    MarcaId=1, VehiculoId=1,Diseno = disenos.FirstOrDefault(),LlantaId=1,Vehiculo= vehiculos.FirstOrDefault(),
            //                    FechaInspeccion=DateTime.Now,Marca= marcas.FirstOrDefault(),Tipo=tipos.FirstOrDefault() });

            //ListarLlantas
            string tokenValue = Request.Cookies["token"];
            var client = new RestClient(configuration["APIClient"]);
            var request = new RestRequest("/api/Llanta/ListarLlantas", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            var response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemLlanta> ListaLlantas = System.Text.Json.JsonSerializer.Deserialize<List<ItemLlanta>>(content);

                model.ItemLlanta = ListaLlantas;
            }
            else
            {
                model.ItemLlanta = null;
            }

            TempData["menu"] = "";

            request = new RestRequest("/api/Tipos/Vehiculos/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<TipoViewModel> ListaTipoVehiculos = System.Text.Json.JsonSerializer.Deserialize<List<TipoViewModel>>(content);

                model.ListaTipoVehiculo = ListaTipoVehiculos;
            }
            else
            {
                model.ListaTipoVehiculo = null;
            }
            //ciudad
            request = new RestRequest("/api/Ciudad/ListarCiudad", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<itemCiudad> ListaCiudad = System.Text.Json.JsonSerializer.Deserialize<List<itemCiudad>>(content);

                model.ListaCiudad = ListaCiudad;
            }
            else
            {
                model.ListaCiudad = null;
            }

            //TIPO LLANTA
            request = new RestRequest("/api/Tipos/Llantas/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<TipoViewModel> ListaTipoLlanta = System.Text.Json.JsonSerializer.Deserialize<List<TipoViewModel>>(content);

                model.ListaTipoLlanta = ListaTipoLlanta;
            }
            else
            {
                model.ListaTipoLlanta = null;
            }

            //diseño
            request = new RestRequest("/api/Disenios/Llantas/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<DisenioViewModel> listadisenio = System.Text.Json.JsonSerializer.Deserialize<List<DisenioViewModel>>(content);

                model.ListaItemDiseno = listadisenio;
            }
            else
            {
                model.ListaItemDiseno = null;
            }

            //medida
            request = new RestRequest("/api/Medidas/Llantas/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemMedida> listamedida = System.Text.Json.JsonSerializer.Deserialize<List<ItemMedida>>(content);

                model.ListaMedidaLlanta = listamedida;
            }
            else
            {
                model.ListaMedidaLlanta = null;
            }

            //estoy aqui para montaje

            //marca VEHICULO CHASIS
            request = new RestRequest("/api/Marcas/Chasis/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemMarcaChasis> listamarca = System.Text.Json.JsonSerializer.Deserialize<List<ItemMarcaChasis>>(content);

                model.ListaChasisMarca = listamarca;
            }
            else
            {
                model.ListaMarca = null;
            }
            //estado
            request = new RestRequest("/api/Estados/Inspecciones/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemEstado> listestadoins = System.Text.Json.JsonSerializer.Deserialize<List<ItemEstado>>(content);

                model.ListaInspeccionLlanta = listestadoins;
            }
            else
            {
                model.ListaInspeccionLlanta = null;
            }

            //marca VEHICULO cabezales
            request = new RestRequest("/api/Marcas/Cabezales/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemMarca> listamarca = System.Text.Json.JsonSerializer.Deserialize<List<ItemMarca>>(content);

                model.ListaMarca = listamarca;
            }
            else
            {
                model.ListaMarca = null;
            }

            //marca
            request = new RestRequest("/api/Marcas/Llantas/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemMarca> listamarca = System.Text.Json.JsonSerializer.Deserialize<List<ItemMarca>>(content);

                model.ListaMarca = listamarca;
            }
            else
            {
                model.ListaMarca = null;
            }

            //eje
            request = new RestRequest("/api/Ejes/Llantas/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemEje> listaeje = System.Text.Json.JsonSerializer.Deserialize<List<ItemEje>>(content);

                model.ListaEje = listaeje;
            }
            else
            {
                model.ListaEje = null;
            }

            //estado
            request = new RestRequest("/api/Estados/Llantas/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemEstado> listaeje = System.Text.Json.JsonSerializer.Deserialize<List<ItemEstado>>(content);

                model.ListaEstado = listaeje;
            }
            else
            {
                model.ListaEstado = null;
            }

            //cabezal
            request = new RestRequest("/api/Vehiculo/ListarTipoVehiculosListado", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            request.AddQueryParameter("TipoVehiculo", "CABEZAL");
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemCabezalTipoVehiculo> listaplaca = System.Text.Json.JsonSerializer.Deserialize<List<ItemCabezalTipoVehiculo>>(content);

                model.ListaCabezalTipo = listaplaca;
            }
            else
            {
                model.ListaCabezalTipo = null;
            }

            //chasis
            request = new RestRequest("/api/Vehiculo/ListarTipoVehiculosListado", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            request.AddQueryParameter("TipoVehiculo", "CHASIS");

            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemChasisTipo> listavehicu = System.Text.Json.JsonSerializer.Deserialize<List<ItemChasisTipo>>(content);

                model.ListaChasisTipo = listavehicu;
            }
            else
            {
                model.ListaChasisTipo = null;
            }

            return View(model);
        }

        // GET: LlantaController/Details/5

        [HttpPost]
        public ActionResult BuscarTermico()
        {
            ListaLlanta model = new ListaLlanta();

            string tokenValue = Request.Cookies["token"];
            var client = new RestClient(configuration["APIClient"]);
            var request = new RestRequest("/api/Llanta/ListarLlantasTermico", Method.Get);

            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            //request.AddQueryParameter("Tipo", "Llanta");

            var response = client.Execute(request);
            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemTermico> ListaTermicos = System.Text.Json.JsonSerializer.Deserialize<List<ItemTermico>>(content);

                model.Termico = Convert.ToInt64(ListaTermicos[0].termico);
            }
            else
            {
                request = new RestRequest("/api/configuracionParametros/ListarConfiguracionParametros", Method.Get);
                request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
                //request.AddQueryParameter("Tipo", "Llanta");

                response = client.Execute(request);
                if (response.Content.Length > 2 && response.IsSuccessful == true)
                {
                    var content = response.Content;

                    List<ItemTermico> ListaTermicos = System.Text.Json.JsonSerializer.Deserialize<List<ItemTermico>>(content);

                    model.Termico = Convert.ToInt64(ListaTermicos[0].valor);
                }
                else
                {
                    model.Termico = 0;
                }
            }

            return Json(new { data = model.Termico });
        }

        [HttpPost]
        public ActionResult BuscarUltimaInspeccion()
        {
            string json = Request.Form["json"];

            // Deserializa el JSON en un objeto utilizando Json.NET
            ListaLlanta model2 = new ListaLlanta();
            List<itemLLantaInspeccionId> listaObj = JsonConvert.DeserializeObject<List<itemLLantaInspeccionId>>(json);
            long idVehiculo = listaObj[0].vehiculoId;
            long idLlanta = listaObj[0].llantaId;
            long termico = listaObj[0].termico;

            //       ListaLlanta model2 = new ListaLlanta();
            string tokenValue = Request.Cookies["token"];

            var client = new RestClient(configuration["APIClient"]);

            var request = new RestRequest("/api/LlantaInspeccion/ListarUltimaLlantaInspeccion", Method.Get);

            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            request.AddJsonBody(new
            {
                IdVehiculo = idVehiculo,
                IdLlanta = idLlanta,
                termico = termico,
            });
            request.AddJsonBody(listaObj);

            var response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<itemLLantaInspeccionId> Listallantainpec = System.Text.Json.JsonSerializer.Deserialize<List<itemLLantaInspeccionId>>(content);

                model2.ListaLlantaInspeccionUltimo = Listallantainpec;
            }
            else
            {
                model2.ListaLlantaInspeccionUltimo = null;
            }

            return Json(new { data = model2.ListaLlantaInspeccionUltimo });
        }

        [HttpPost]
        public ActionResult BuscarTipoVehiculoId()
        {
            try
            {
                string json = Request.Form["json"];

                // Deserializa el JSON en un objeto utilizando Json.NET
                List<itemVehiculoSeleccionado> listaObj = JsonConvert.DeserializeObject<List<itemVehiculoSeleccionado>>(json);
                long idVehiculo = listaObj[0].vehiculoId;
                long tipoId = listaObj[0].tipoId;

                ListaVehiculo model = new ListaVehiculo();
                ListaLlanta model2 = new ListaLlanta();
                string tokenValue = Request.Cookies["token"];
                var client = new RestClient(configuration["APIClient"]);
                var request = new RestRequest("/api/Vehiculo/ListarNumeroLlantasVehiculosListado", Method.Get);
                request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
                request.AddQueryParameter("VehiculoId", idVehiculo);
                request.AddQueryParameter("TipoId", tipoId);
                var response = client.Execute(request);

                var request2 = new RestRequest("/api/Llanta/ListarPosicionesLlantas", Method.Get);
                request2.AddQueryParameter("TipoId", tipoId);
                request2.AddQueryParameter("VehiculoId", idVehiculo);
                request2.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
                var response2 = client.Execute(request2);
                if (response2.Content.Length > 2 && response2.IsSuccessful == true)
                {
                    var content2 = response2.Content;

                    List<ItemPosicionLlanta> ListaLlantas = System.Text.Json.JsonSerializer.Deserialize<List<ItemPosicionLlanta>>(content2);

                    model2.ListaPosicionLlanta = ListaLlantas;
                }
                else
                {
                    model2.ListaPosicionLlanta = null;
                }

                if (response.Content.Length > 2 && response.IsSuccessful == true)
                {
                    var content = response.Content;
                    List<VehiculoViewModel> ListaVehiculos = System.Text.Json.JsonSerializer.Deserialize<List<VehiculoViewModel>>(content);
                    //List<ItemVehiculo> ListaVehiculos = System.Text.Json.JsonSerializer.Deserialize<List<ItemVehiculo>>(content);

                    model.ListaVehiculos = ListaVehiculos;
                }
                else
                {
                    model.ListaVehiculos = null;
                }

                List<long> arreglo = new List<long>();
                if (model2.ListaPosicionLlanta != null)
                {
                    int contador = 0;

                    for (int i = 0; i < model.ListaVehiculos.FirstOrDefault().numeroLlantas; i++)
                    {
                        arreglo.Add(i + 1);
                    }

                    for (int j = 0; j < model2.ListaPosicionLlanta.Count; j++)
                    {
                        arreglo.Remove(model2.ListaPosicionLlanta[j].posicionActual);
                    }

                    return Json(new { data = arreglo });
                }
                else
                {
                    if (model.ListaVehiculos != null)
                    {
                        for (int i = 0; i < model.ListaVehiculos.FirstOrDefault().numeroLlantas; i++)
                        {
                            arreglo.Add(i + 1);
                        }

                        return Json(new { data = arreglo });
                    }
                    else
                    {
                        return Json(new { data = "" });
                    }
                }
            }
            catch (Exception e)
            {
                return Json(new { data = "" });
            }
        }

        // GET: LlantaController/Create

        [HttpPost]
        public ActionResult PosicionesLlantasParaInspeccion()
        {
            try
            {
                string json = Request.Form["json"];

                // Deserializa el JSON en un objeto utilizando Json.NET
                List<itemVehiculoSeleccionado> listaObj = JsonConvert.DeserializeObject<List<itemVehiculoSeleccionado>>(json);
                long idVehiculo = listaObj[0].vehiculoId;
                long tipoId = listaObj[0].tipoId;

                ListaVehiculo model = new ListaVehiculo();
                ListaLlanta model2 = new ListaLlanta();
                string tokenValue = Request.Cookies["token"];
                var client = new RestClient(configuration["APIClient"]);
                var request = new RestRequest("/api/Vehiculo/ListarNumeroLlantasVehiculosListado", Method.Get);

                request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
                request.AddQueryParameter("VehiculoId", idVehiculo);
                request.AddQueryParameter("TipoId", tipoId);
                var response = client.Execute(request);

                var request2 = new RestRequest("/api/Llanta/ListarPosicionesLlantas", Method.Get);
                request2.AddQueryParameter("TipoId", tipoId);
                request2.AddQueryParameter("VehiculoId", idVehiculo);
                request2.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
                var response2 = client.Execute(request2);
                if (response2.Content.Length > 2 && response2.IsSuccessful == true)
                {
                    var content2 = response2.Content;

                    List<ItemPosicionLlanta> ListaLlantas = System.Text.Json.JsonSerializer.Deserialize<List<ItemPosicionLlanta>>(content2);

                    model2.ListaPosicionLlanta = ListaLlantas;
                }
                else
                {
                    model2.ListaPosicionLlanta = null;
                }

                if (response.Content.Length > 2 && response.IsSuccessful == true)
                {
                    var content = response.Content;
                    List<VehiculoViewModel> ListaVehiculos = System.Text.Json.JsonSerializer.Deserialize<List<VehiculoViewModel>>(content);
                    //List<ItemVehiculo> ListaVehiculos = System.Text.Json.JsonSerializer.Deserialize<List<ItemVehiculo>>(content);

                    model.ListaVehiculos = ListaVehiculos;
                }
                else
                {
                    model.ListaVehiculos = null;
                }

                List<long> arreglo = new List<long>();
                if (model2.ListaPosicionLlanta != null)
                {
                    int contador = 0;

                    for (int i = 0; i < model.ListaVehiculos.FirstOrDefault().numeroLlantas; i++)
                    {
                        arreglo.Add(i + 1);
                    }

                    return Json(new { data = arreglo });
                }
                else
                {
                    if (model.ListaVehiculos != null)
                    {
                        for (int i = 0; i < model.ListaVehiculos.FirstOrDefault().numeroLlantas; i++)
                        {
                            arreglo.Add(i + 1);
                        }

                        return Json(new { data = arreglo });
                    }
                    else
                    {
                        return Json(new { data = "" });
                    }
                }
            }
            catch (Exception e)
            {
                return Json(new { data = "" });
            }
        }

        [HttpPost]
        public ActionResult BusquedaHistoricoInspecciones()
        {
            try
            {
                string json = Request.Form["json"];

                // Deserializa el JSON en un objeto utilizando Json.NET
                List<itemBusquedaInspeccion> listaObj = JsonConvert.DeserializeObject<List<itemBusquedaInspeccion>>(json);
                long idVehiculo = listaObj[0].vehiculoId;
                long llantaid = listaObj[0].llantaId;
                long termico = listaObj[0].termico;
                string fechadesde = listaObj[0].fechaDesde;
                string fechahasta = listaObj[0].fechaHasta;

                ListaLlanta model2 = new ListaLlanta();
                string tokenValue = Request.Cookies["token"];
                var client = new RestClient(configuration["APIClient"]);
                var request = new RestRequest("/api/LlantaInspeccion/ListarBusquedaHistoricoInspecciones", Method.Get);
                request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
                request.AddQueryParameter("VehiculoId", idVehiculo);
                request.AddQueryParameter("LlantaId", llantaid);
                request.AddQueryParameter("Termico", termico);
                request.AddQueryParameter("FechaDesde", fechadesde);
                request.AddQueryParameter("FechaHasta", fechahasta);
                var response = client.Execute(request);

                if (response.Content.Length > 2 && response.IsSuccessful == true)
                {
                    var content = response.Content;
                    List<itemLLantaInspeccionViewModel> ListaInspecc = System.Text.Json.JsonSerializer.Deserialize<List<itemLLantaInspeccionViewModel>>(content);
                    //List<ItemVehiculo> ListaVehiculos = System.Text.Json.JsonSerializer.Deserialize<List<ItemVehiculo>>(content);

                    model2.ListaInspeccionView = ListaInspecc;
                }
                else
                {
                    model2.ListaInspeccionView = null;
                }

                return Json(new { data = model2.ListaInspeccionView });
            }
            catch (Exception e)
            {
                return Json(new { data = "" });
            }
        }

        [HttpPost]
        public ActionResult BusquedaDetalleMontajeLLantas()
        {
            try
            {
                string json = Request.Form["json"];

                // Deserializa el JSON en un objeto utilizando Json.NET
                List<DetalleMontajeLlantaTableLista> listaObj = JsonConvert.DeserializeObject<List<DetalleMontajeLlantaTableLista>>(json);
                long idCabeceraMontaje = listaObj[0].IdCabeceraMontaje;

                ListaMontajeLlanta model2 = new ListaMontajeLlanta();
                string tokenValue = Request.Cookies["token"];
                var client = new RestClient(configuration["APIClient"]);
                var request = new RestRequest("/api/Llanta/ListarBusquedaDetalleCabeceraMontaje", Method.Get);
                request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
                request.AddQueryParameter("idCabeceraMontaje", idCabeceraMontaje);

                var response = client.Execute(request);

                if (response.Content.Length > 2 && response.IsSuccessful == true)
                {
                    var content = response.Content;
                    List<DetalleMontajeLlantaTableLista> ListaDetalleMontajeLLanta = System.Text.Json.JsonSerializer.Deserialize<List<DetalleMontajeLlantaTableLista>>(content);
                    //List<ItemVehiculo> ListaVehiculos = System.Text.Json.JsonSerializer.Deserialize<List<ItemVehiculo>>(content);

                    model2.DetalleMontajeLlantaTableListalistas = ListaDetalleMontajeLLanta;
                }
                else
                {
                    model2.DetalleMontajeLlantaTableListalistas = null;
                }

                return Json(new { data = model2.DetalleMontajeLlantaTableListalistas });
            }
            catch (Exception e)
            {
                return Json(new { data = "" });
            }
        }

        [HttpPost]
        public ActionResult ObtenerLLantasCabeceraMontajes()
        {
            try
            {
                string json = Request.Form["json"];

                // Deserializa el JSON en un objeto utilizando Json.NET
                //List<DetalleMontajeLlantaTableLista> listaObj = JsonConvert.DeserializeObject<List<DetalleMontajeLlantaTableLista>>(json);
                //long idCabeceraMontaje = listaObj[0].IdCabeceraMontaje;

                ListaMontajeLlanta model = new ListaMontajeLlanta();
                string tokenValue = Request.Cookies["token"];
                var client = new RestClient(configuration["APIClient"]);
                var request = new RestRequest("/api/MontajeLlanta/ListarDatosCabbeceraMontaje", Method.Get);
                request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
                var response = client.Execute(request);

                if (response.Content.Length > 2 && response.IsSuccessful == true)
                {
                    var content = response.Content;

                    List<CabeceraMontajeLlantaTableLista> ListaTipoCabeceraMontaje = System.Text.Json.JsonSerializer.Deserialize<List<CabeceraMontajeLlantaTableLista>>(content);

                    model.CabeceraMontajeLlantaTableListas = ListaTipoCabeceraMontaje;
                }
                else
                {
                    model.CabeceraMontajeLlantaTableListas = null;
                }

                return Json(new { data = model.CabeceraMontajeLlantaTableListas });
            }
            catch (Exception e)
            {
                return Json(new { data = "" });
            }
        }
        ///l--------------------------/
        [HttpPost]
        public ActionResult BuscarLlantaDisponibleTermico()
        {
            try
            {
                string json = Request.Form["json"];

                // Deserializa el JSON en un objeto utilizando Json.NET
                List<LlantaDisponibleTermico> listaObj = JsonConvert.DeserializeObject<List<LlantaDisponibleTermico>>(json);
                long termico = listaObj[0].Termico;

                

          

                ListaLlanta model2 = new ListaLlanta();
                string tokenValue = Request.Cookies["token"];
                var client = new RestClient(configuration["APIClient"]);
                var request = new RestRequest("/api/MontajeLlanta/ObtenerDetallesLlantaPorTermico", Method.Get);
                request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
                request.AddQueryParameter("termico", termico);
              

                var response = client.Execute(request);

                if (response.Content.Length > 2 && response.IsSuccessful == true)
                {
                    var content = response.Content;
                    DatosTermicosLlantas ListaInspecc = System.Text.Json.JsonSerializer.Deserialize<DatosTermicosLlantas>(content);
                   

                        model2.DatosTermicosLlantasLista = ListaInspecc;
                    }
                    else
                    {
                        model2.DatosTermicosLlantasLista = null;
                    }
                
          

                return Json(new { data = model2.DatosTermicosLlantasLista });
            }
            catch (Exception e)
            {
                return Json(new { data = "" });
            }
        }

        [HttpPost]
        public ActionResult BuscarTermicoMontajeLlanta()
        {
            try
            {
                string json = Request.Form["json"];

                // Deserializa el JSON en un objeto utilizando Json.NET
                List<itemMontajeLlantaBusquedaTermicos> listaObj = JsonConvert.DeserializeObject<List<itemMontajeLlantaBusquedaTermicos>>(json);
                long tipoVehiculo = listaObj[0].tipoVehiculo;

                long? cabezalId = listaObj[0].cabezalId;
                long? chasisId = listaObj[0].chasisId;

                if (cabezalId == null)
                {
                    cabezalId = 0;
                }
                if (chasisId == null)
                {
                    chasisId = 0;
                }

                ListaLlanta model2 = new ListaLlanta();
                string tokenValue = Request.Cookies["token"];
                var client = new RestClient(configuration["APIClient"]);
                var request = new RestRequest("/api/Llanta/ListarTermicoMontajeLlanta", Method.Get);
                request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
                request.AddQueryParameter("tipoVehiculo", tipoVehiculo);
                request.AddQueryParameter("cabezalId", (long)cabezalId);
                request.AddQueryParameter("chasisId", (long)chasisId);

                var response = client.Execute(request);

                if (response.Content.Length > 2 && response.IsSuccessful == true)
                {
                    var content = response.Content;
                    List<itemMontajeTermico> ListaInspecc = System.Text.Json.JsonSerializer.Deserialize<List<itemMontajeTermico>>(content);
                    //List<ItemVehiculo> ListaVehiculos = System.Text.Json.JsonSerializer.Deserialize<List<ItemVehiculo>>(content);
                    List<itemMontajeTermico> ListaInspeccActual = new List<itemMontajeTermico>();
                    if (ListaInspecc.Count() < ListaInspecc[0].numeroLLantasAsignadas)
                    {
                        itemMontajeTermico objtermico;
                        for (int i = 1; i < ListaInspecc[0].numeroLLantasAsignadas + 1; i++)
                        {
                            bool bandera = false;
                            objtermico = new itemMontajeTermico();
                            //var datPos =0;

                            //if (i== ListaInspecc[0].posicionActual)
                            //{
                            //     datPos = -1;
                            //}
                            //else
                            //{
                            //     datPos = i;
                            //}

                            foreach (var item in ListaInspecc)
                            {
                                if (i != item.posicionActual)
                                {
                                    bandera = true;
                                }
                                else
                                {
                                    bandera = false;
                                    break;
                                }
                            }

                            if (bandera == true)
                            {
                                objtermico.termico = 0;
                                objtermico.idVehiculo = 0;
                                objtermico.posicionActual = i;
                                ListaInspecc.Add(objtermico);
                            }
                        }

                        //if (ListaInspecc.Count < ListaInspecc[0].numeroLLantasAsignadas)
                        //{
                        //    itemMontajeTermico objtermicos;
                        //    for (int i = ListaInspecc.Count; i < ListaInspecc[0].numeroLLantasAsignadas; i++)
                        //    {
                        //        bool bandera = false;
                        //        objtermicos = new itemMontajeTermico();
                        //        //var datPos =0;

                        //        //if (i== ListaInspecc[0].posicionActual)
                        //        //{
                        //        //     datPos = -1;
                        //        //}
                        //        //else
                        //        //{
                        //        //     datPos = i;
                        //        //}

                        //        foreach (var item in ListaInspecc)
                        //        {
                        //            if (i != item.posicionActual)
                        //            {
                        //                bandera = true;

                        //            }
                        //            else
                        //            {
                        //                bandera = false;
                        //            }

                        //        }

                        //        if (bandera == true)
                        //        {
                        //            objtermicos.termico = 0;
                        //            objtermicos.idVehiculo = 0;
                        //            objtermicos.posicionActual = i;
                        //            ListaInspecc.Add(objtermicos);
                        //        }

                        //    }

                        //}

                        var listTer = ListaInspecc.OrderBy(x => x.posicionActual);
                        model2.ListaTraerTermico = listTer.ToList();
                    }

                    else
                    {
                        model2.ListaTraerTermico = ListaInspecc;
                    }
                }
                ////else
                ////{
                ////    model2.ListaTraerTermico = null;
                ////}

                return Json(new { data = model2.ListaTraerTermico });
            }
            catch (Exception e)
            {
                return Json(new { data = "" });
            }
        }

        [HttpPost]
        public ActionResult AsignacionNuevaLlanta()
        {
            try
            {
                string json = Request.Form["json"];

                // Deserializa el JSON en un objeto utilizando Json.NET
                List<RotacionLLantaTermico> listaObj = JsonConvert.DeserializeObject<List<RotacionLLantaTermico>>(json);

                long termico1 = listaObj[0].Termico1;
                long tipoVehiculo = listaObj[0].tipoVehiculoId;
                long posicion = listaObj[0].posicionLlanta;
                long IdCabeceraMontaje = listaObj[0].IdCabeceraMontaje;
                string tipoEstado = listaObj[0].TipoEstado;
                long chasisid = 0;
                long cabezalId = 0;

                if (listaObj[0].chasisId == null)
                {
                    chasisid = 0;
                }
                else
                {
                    chasisid = (long)listaObj[0].chasisId;
                }

                if (listaObj[0].cabezalId == null)
                {
                    cabezalId = 0;
                }
                else
                {
                    cabezalId = (long)listaObj[0].cabezalId;
                }

                ListaLlanta model2 = new ListaLlanta();
                string tokenValue = Request.Cookies["token"];
                var client = new RestClient(configuration["APIClient"]);
                var request = new RestRequest("/api/MontajeLlanta/GuardarAsignacionMontajeRotacion", Method.Post);
                request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

                request.AddQueryParameter("IdCabeceraMontaje", IdCabeceraMontaje);
                request.AddQueryParameter("Termico1", termico1);
                request.AddQueryParameter("Posicion", posicion);
                request.AddQueryParameter("tipoVehiculo", tipoVehiculo);
                request.AddQueryParameter("cabezalId", cabezalId);
                request.AddQueryParameter("chasisId", chasisid);
                request.AddQueryParameter("TipoEstado", tipoEstado);

                request.AddJsonBody(new
                {
                    MMoriginal = listaObj[0].MMoriginal,
                    tipoLlanta = listaObj[0].tipoLlanta,
                    medidaId = listaObj[0].medidaId,
                    marcaId = listaObj[0].marcaId,
                    disenioId = listaObj[0].disenioId,
                    ejeId = listaObj[0].ejeId,
                });

                var response = client.Execute(request);

                if (response.Content.Length > 2 && response.IsSuccessful == true)
                {
                    var content = response.Content;
                    List<itemMontajeTermico> ListaInspecc = System.Text.Json.JsonSerializer.Deserialize<List<itemMontajeTermico>>(content);
                    //List<ItemVehiculo> ListaVehiculos = System.Text.Json.JsonSerializer.Deserialize<List<ItemVehiculo>>(content);

                    model2.ListaTraerTermico = ListaInspecc;
                }
                else
                {
                    List<itemMontajeTermico> listMonta = new List<itemMontajeTermico>();
                    itemMontajeTermico objter = new itemMontajeTermico();
                    objter.idCabeceraMontaje = 0;
                    listMonta.Add(objter);

                    model2.ListaTraerTermico = listMonta;
                }

                return Json(new { data = model2.ListaTraerTermico });
            }
            catch (Exception e)
            {
                return Json(new { data = "" });
            }
        }

        [HttpPost]
        public ActionResult RotacionLLantasCambio()
        {
            try
            {
                string json = Request.Form["json"];

                // Deserializa el JSON en un objeto utilizando Json.NET
                List<RotacionLLantaTermico> listaObj = JsonConvert.DeserializeObject<List<RotacionLLantaTermico>>(json);

                long termico1 = listaObj[0].Termico1;
                long termico2 = listaObj[0].Termico2;
                long IdCabeceraMontaje = listaObj[0].IdCabeceraMontaje;
                string tipoEstado = listaObj[0].TipoEstado;

                ListaLlanta model2 = new ListaLlanta();
                string tokenValue = Request.Cookies["token"];
                var client = new RestClient(configuration["APIClient"]);
                var request = new RestRequest("/api/MontajeLlanta/GuardarMontajeRotacion", Method.Post);
                request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

                request.AddQueryParameter("IdCabeceraMontaje", IdCabeceraMontaje);
                request.AddQueryParameter("Termico1", termico1);
                request.AddQueryParameter("Termico2", termico2);
                request.AddQueryParameter("TipoEstado", tipoEstado);

                var response = client.Execute(request);

                if (response.Content.Length > 2 && response.IsSuccessful == true)
                {
                    var content = response.Content;
                    List<itemMontajeTermico> ListaInspecc = System.Text.Json.JsonSerializer.Deserialize<List<itemMontajeTermico>>(content);
                    //List<ItemVehiculo> ListaVehiculos = System.Text.Json.JsonSerializer.Deserialize<List<ItemVehiculo>>(content);

                    model2.ListaTraerTermico = ListaInspecc;
                }
                else
                {
                    List<itemMontajeTermico> listMonta = new List<itemMontajeTermico>();
                    itemMontajeTermico objter = new itemMontajeTermico();
                    objter.idCabeceraMontaje = 0;
                    listMonta.Add(objter);

                    model2.ListaTraerTermico = listMonta;
                }

                return Json(new { data = model2.ListaTraerTermico });
            }
            catch (Exception e)
            {
                return Json(new { data = "" });
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: LlantaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LlantaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LlantaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> GuardaryEditarInfo(ListaLlanta model)
        {
            string tokenValue = Request.Cookies["token"];
            // var client = new RestClient(configuration["APIClient"];
            var request = new RestRequest("/api/Llanta/NuevaLlanta", Method.Post);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            if (model.Id == 0)
            {
                request.AddJsonBody(new
                {
                    IdLlanta = 0,
                    Termico = Convert.ToInt64(model.Termico),
                    MMOriginal = Convert.ToDecimal(model.Original),
                    IdChasis = model.ChasisId,
                    IdCabezal = model.CabezalId,
                    IdDisenio = Convert.ToInt64(model.DisenoId),
                    IdMedida = Convert.ToInt64(model.MedidaId),
                    IdEstado = model.EstadoId,
                    PosicionActual = Convert.ToInt64(model.PosicionAtual),
                    IdMarca = Convert.ToInt64(model.MarcaId),
                    IdEje = Convert.ToInt64(model.EjeId),
                    FechaUltimaInspeccion = DateTime.Now,
                    VehiculoNombre = model.VehiculoNombre,
                    IdTipoVehiculo = model.TipoVehiculoId,
                    IdVehiculo = model.VehiculoId,
                    IdTipoLlanta = model.TipoLlantaId
                });
            }
            else
            {
                request.AddJsonBody(new
                {
                    IdLlanta = model.Id,
                    Termico = Convert.ToInt64(model.Termico),
                    MMOriginal = model.Original,
                    IdChasis = model.ChasisId,
                    IdCabezal = model.CabezalId,
                    IdDisenio = Convert.ToInt64(model.DisenoId),
                    IdMedida = Convert.ToInt64(model.MedidaId),
                    IdEstado = model.EstadoId,
                    PosicionActual = Convert.ToInt64(model.PosicionAtual),
                    IdMarca = Convert.ToInt64(model.MarcaId),
                    IdEje = Convert.ToInt64(model.EjeId),
                    FechaUltimaInspeccion = DateTime.Now,
                    VehiculoNombre = model.VehiculoNombre,
                    VehiculoId = model.VehiculoId,
                    IdTipoVehiculo = model.TipoVehiculoId,
                    IdTipoLlanta = model.TipoLlantaId,
                });
            }

            request.AddJsonBody(model);

            if (model.DisenoNombre == "0" || model.MedidaNombreLlanta == "0" || model.MarcaNombre == "0" || model.EjeNombre == "0" || model.VehiculoNombre == "" || model.Original == "")
            {
                TempData["MensajeError"] = "Rellene todos los campos";
                return Redirect("Index");
            }

            // TempData["menu"] = null;
            try
            {
                if (ModelState.IsValid)
                {
                    _log.Info("Accediendo al API");
                    var response = await _apiClient.ExecuteAsync(request, Method.Post);
                    //      _log.Info("Registrando Tipo de" + Tipo);
                    //responseContent = ;
                    if (response.IsSuccessful)
                    {
                        // LogedDataViewModel LogedData = JsonSerializer.Deserialize<LogedDataViewModel>(response.Content);

                        // Crear una cookie para almacenar el token
                        //Response.Cookies.Append("token", LogedData.token);
                        //Response.Cookies.Append("expiracion", LogedData.expiracion.ToString());
                        // Response.Cookies.Append("user", model.User);

                        if (model.Id == 0)
                        {
                            TempData["MensajeExito"] = "Registro Exitoso";
                        }
                        else
                        {
                            TempData["MensajeExito"] = "Se edito correctamente";
                        }

                        return RedirectToAction("Index", "Llanta");
                    }
                    TempData["MensajeError"] = response.Content;
                    return View(model);
                }
                TempData["MensajeError"] = "Rellene todos los campos";

                return View(model);
            }
            catch (JsonParsingException e)
            {
                _log.Error(e, "Error Obteniendo Token");
                _log.Error(e.GetUnderlyingStringUnsafe());
                TempData["MensajeError"] = e.Message.ToString();
                //return RedirectToAction("Index", "Home");
                return View(model);
            }
            catch (Exception e)
            {
                _log.Error(e, "Error al iniciar sesión");
                _log.Error(responseContent);
                TempData["MensajeError"] = e.Message;
                return Redirect("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> GuardaryEditarInfoInspeccionLlanta(ListaLlanta model)
        {
            string tokenValue = Request.Cookies["token"];
            // var client = new RestClient(configuration["APIClient"];
            var request = new RestRequest("/api/LlantaInspeccion/NuevaLlantaInspeccion", Method.Post);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            model.FechaInspeccion = DateTime.Now;
            long? idCabezal, idChasis;
            if (model.CabezalId == 0)
            {
                idCabezal = null;
            }
            else
            {
                idCabezal = model.CabezalId;
            }

            if (model.ChasisId == 0)
            {
                idChasis = null;
            }
            else
            {
                idChasis = model.ChasisId;
            }

            request.AddJsonBody(new
            {
                IdLlantasInspecciones = 0,
                IdLlanta = Convert.ToInt64(model.idLlanta),
                IdVehiculo = Convert.ToInt64(model.VehiculoId),
                IdCabezal = idCabezal,
                IdChasis = idChasis,
                IdTipoVehiculo = Convert.ToInt64(model.TipoVehiculoId),
                IdMarcaChasis = Convert.ToInt64(model.idMarcaChasis),
                IdMarcaCabezal = Convert.ToInt64(model.idMarcaCabezal),
                MMOriginal = Convert.ToDecimal(model.Original),
                MMMinimo = Convert.ToInt64(model.mmMinimo),
                MMRetiro = Convert.ToInt64(model.mmRetiro),
                PorcDesgaste = Convert.ToInt64(model.porcentajeDesgaste),
                Secuencia = Convert.ToInt64(model.secuencia),
                IdTipoLlanta = Convert.ToInt64(model.TipoLlantaId),
                IdEje = Convert.ToInt64(model.EjeId),
                IdMarcaLlanta = model.MarcaId,
                IdDisenio = Convert.ToInt64(model.DisenoId),
                IdMedida = Convert.ToInt64(model.MedidaId),
                Posicion = Convert.ToInt64(model.PosicionAtual),
                IdCiudad = Convert.ToInt64(model.idCiudad),
                IdEstadoLlanta = Convert.ToInt64(model.EstadoId),
                IdEstadoInspeccion = Convert.ToInt64(model.EstadoInspeccionId),
                MM1 = Convert.ToInt64(model.mm1),
                MM2 = Convert.ToInt64(model.mm2),
                MM3 = Convert.ToInt64(model.mm3),
                MM4 = Convert.ToInt64(model.mm4),
                FechaInspeccion = model.FechaInspeccion,
                Termico = model.Termico,
                Observaciones = model.Observaciones,
                Placa = model.PlacaDescrcion,
                Estado = model.EstadoDescripcion,
                TipoVehiculo = model.TipoVehiculoDescripcion,
                MarcaLlanta = model.MarcaLlanta,
                MedidaLlanta = model.MedidaLlanta,
                DisenioLlanta = model.DisenioLlanta
            });

            request.AddJsonBody(model);

            // TempData["menu"] = null;
            try
            {
                _log.Info("Accediendo al API");
                var response = await _apiClient.ExecuteAsync(request, Method.Post);
                //      _log.Info("Registrando Tipo de" + Tipo);
                //responseContent = ;
                if (response.IsSuccessful)
                {
                    // LogedDataViewModel LogedData = JsonSerializer.Deserialize<LogedDataViewModel>(response.Content);

                    // Crear una cookie para almacenar el token
                    //Response.Cookies.Append("token", LogedData.token);
                    //Response.Cookies.Append("expiracion", LogedData.expiracion.ToString());
                    // Response.Cookies.Append("user", model.User);

                    if (model.Id == 0)
                    {
                        TempData["MensajeExito"] = "Registro de Inspeccion  Exitoso";
                    }
                    else
                    {
                        TempData["MensajeExito"] = "Se edito correctamente";
                    }

                    return RedirectToAction("Index", "Llanta");
                }
                TempData["MensajeError"] = response.Content;
                return View(model);

                TempData["MensajeError"] = "Rellene todos los campos";

                return View(model);
            }
            catch (JsonParsingException e)
            {
                _log.Error(e, "Error Obteniendo Token");
                _log.Error(e.GetUnderlyingStringUnsafe());
                TempData["MensajeError"] = e.Message.ToString();
                //return RedirectToAction("Index", "Home");
                return View(model);
            }
            catch (Exception e)
            {
                _log.Error(e, "Error al iniciar sesión");
                _log.Error(responseContent);
                TempData["MensajeError"] = e.Message;
                return Redirect("Index");
            }
        }


        //[HttpPost]
        //public async Task<ActionResult> ListraTermicoDisponibles()
        //{

        //    string tokenValue = Request.Cookies["token"];
        //    // var client = new RestClient(configuration["APIClient"];
        //    var request = new RestRequest("/api/MontajeLlanta/ListarTermicosDisponibles", Method.Get);
        //    request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);


        //    // TempData["menu"] = null;



        //}


        [HttpPost]
        public ActionResult ListraTermicoDisponibles()
        {
            try
            {

                ListaMontajeLlanta model = new ListaMontajeLlanta();
                string tokenValue = Request.Cookies["token"];
                var client = new RestClient(configuration["APIClient"]);
             
              var   request = new RestRequest("/api/MontajeLlanta/ListarTermicoDisponibles", Method.Get);
                request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
             var    response = client.Execute(request);

                if (response.Content.Length > 2 && response.IsSuccessful == true)
                {
                    var content = response.Content;

                    List<LlantaInformacionApi> listaTermicosDisponibles = System.Text.Json.JsonSerializer.Deserialize<List<LlantaInformacionApi>>(content);

                    model.ListaLlantaInformacionApi = listaTermicosDisponibles;
                }
                else
                {
                    model.ListaLlantaInformacionApi = null;
                }

                return Json(new { data = model.ListaLlantaInformacionApi });
            }
            catch (Exception e)
            {
                return Json(new { data = "" });
            }
        }

        [HttpPost]
        public async Task<ActionResult> GuardaryEditarReencaucheLlantas(ListaLlanta model)
        {
            string tokenValue = Request.Cookies["token"];
            // var client = new RestClient(configuration["APIClient"];
            var request = new RestRequest("/api/Reencauche/RegistrarReencauche", Method.Post);

            //copiar y pegar en el resto de controladores
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            //------------------------------------------

            request.AddJsonBody(new
            {
                IdVehiculo = model.VehiculoId,
                IdLlantas = model.idLlanta,
                NumeroPosicion = model.numeroPosicion,
                TipoLLantaNombre = model.TipoLlantaNombre,
                FechaReencauche = model.FechaReencauche,
                Posicion = model.PosicionTextoReencauche,
                IdMarcaNueva = model.marcaSiguiente,
                IdMarcaAnterior = model.marcaAnterior,
                IdDisenioNueva = model.disenioSiguiente,
                IdDisenioAnterior = model.disenioAnterior,
                IdMedida = model.MedidaId,
                Kilometraje = model.Kilometraje,
                DisenioNombre = model.DisenioNombreAnterior,
                MarcaNombre = model.MarcaNombreAnterior,
                MedidaNombre = model.MedidaNombreLlanta,
                MmOriginal = model.Original,
                EjeIds = model.EjeId,
                FechaInspeccion = model.FechaInspeccion,
                TipoLlantaId = model.TipoLlantaId,
                EstadoLlantaId = model.EstadoId,
                Termico = model.TermicoReencauche,
                TipoVehiculoId = model.TipoVehiculoId,
            });

            request.AddJsonBody(model);

            // TempData["menu"] = null;
            try
            {
                _log.Info("Accediendo al API");
                var response = await _apiClient.ExecuteAsync(request, Method.Post);
                //      _log.Info("Registrando Tipo de" + Tipo);
                //responseContent = ;
                if (response.IsSuccessful)
                {
                    // LogedDataViewModel LogedData = JsonSerializer.Deserialize<LogedDataViewModel>(response.Content);

                    // Crear una cookie para almacenar el token
                    //Response.Cookies.Append("token", LogedData.token);
                    //Response.Cookies.Append("expiracion", LogedData.expiracion.ToString());
                    // Response.Cookies.Append("user", model.User);

                    //if (model.Id == 0)
                    //{
                    TempData["MensajeExito"] = "Registro de Reencauche  Exitoso";

                    //}
                    //else
                    //{
                    //    TempData["MensajeExito"] = "Se edito correctamente";

                    //}

                    return RedirectToAction("Index", "Llanta");
                }
                TempData["MensajeError"] = response.Content;
                return View(model);
            }
            catch (JsonParsingException e)
            {
                _log.Error(e, "Error Obteniendo Token");
                _log.Error(e.GetUnderlyingStringUnsafe());
                TempData["MensajeError"] = e.Message.ToString();
                //return RedirectToAction("Index", "Home");
                return View(model);
            }
            catch (Exception e)
            {
                _log.Error(e, "Error al iniciar sesión");
                _log.Error(responseContent);
                TempData["MensajeError"] = e.Message;
                return Redirect("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteInformacion(ListaLlanta model)
        {
            string tokenValue = Request.Cookies["token"];
            long id = model.Id;
            var request = new RestRequest("/api/Llanta/EliminaLlanta", Method.Post/*, DataFormat.Json*/);

            //copiar y pegar en el resto de controladores
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            //------------------------------------------

            request.AddQueryParameter("IdLlanta", id);
            //   request.AddJsonBody(model);

            try
            {
                if (model.Id != 0)
                {
                    if (ModelState.IsValid)
                    {
                        _log.Info("Accediendo al API");
                        var response = await _apiClient.ExecuteAsync(request, Method.Post);
                        // _log.Info("Registrando Tipo de" + Tipo);
                        //responseContent = ;
                        if (response.IsSuccessful)
                        {
                            // LogedDataViewModel LogedData = JsonSerializer.Deserialize<LogedDataViewModel>(response.Content);

                            // Crear una cookie para almacenar el token
                            //Response.Cookies.Append("token", LogedData.token);
                            //Response.Cookies.Append("expiracion", LogedData.expiracion.ToString());
                            // Response.Cookies.Append("user", model.User);

                            TempData["MensajeExito"] = "Eliminacion Exitosa";

                            return RedirectToAction("Index", "Llanta");
                        }
                        TempData["MensajeError"] = response.Content;
                        return View(model);
                    }
                    TempData["MensajeError"] = "No se pudo eliminar la Marca";
                }
                return View(model);
            }
            catch (Exception e)
            {
                _log.Error(e, "Error al iniciar sesión");
                _log.Error(responseContent);
                TempData["MensajeError"] = e.Message;
                return Redirect("Index");
            }

            // return View(model);
        }

        // GET: LlantaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LlantaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CargaMasiva()
        {
            TempData["menu"] = "";
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CargaMasiva(IFormFile fileUploader)
        {
            try
            {
                TempData["menu"] = "";
                string mensaje = string.Empty;
                List<ExcelHistoricoLlantas> archivo = new List<ExcelHistoricoLlantas>();

                if (fileUploader != null)
                {
                    if (fileUploader.Length > 0)
                    {
                        // Solo excel
                        if (fileUploader.ContentType == "application/vnd.ms-excel" || fileUploader.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                        {
                            string _FileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                            string _FileNameex = Path.GetExtension(fileUploader.FileName);
                            string nameExcel = _FileName + _FileNameex;

                            archivo = excelHandler.ConvertFormatExcelToClassExcelHistoricoLlantas(fileUploader.OpenReadStream());
                            if (archivo.Count() == 0)
                            {
                                TempData["MensajeError"] = "Error!! Al leer el archivo, asegurese de llenar la información correcta en base a la plantilla Excel";
                                TempData["MensajeInformacion"] = "Todos los campos son obligatorios";
                            }
                            else
                            {
                                string msj_error = string.Empty;
                                List<ListaRegistrosCargadosExcel> ListExcel = new List<ListaRegistrosCargadosExcel>();
                                var totalregistros = archivo.Count;
                                var contador = 0;
                                foreach (var item in archivo)
                                {
                                    try
                                    {
                                        //if (item.Termico == "0" || item.IdVehiculo == "" || item.Posicion == "0" || item.Medida == "0" || item.MarcaNueva == "" || item.Diseño == "")
                                        //{
                                        //    TempData["MensajeError"] = "Rellene todos los campos";

                                        //}
                                        //Primero creo la llanta
                                        ListaRegistrosCargadosExcel registro = new ListaRegistrosCargadosExcel();
                                        registro.Termico = item.Termico;
                                        registro.Placa = item.IdVehiculo;
                                        registro.FechaInspeccion = item.FechaInspeccion;
                                        string tokenValue = Request.Cookies["token"];
                                        var request = new RestRequest("/api/Llanta/CargaMasiva", Method.Post);
                                        request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
                                        if (item.Termico == "")
                                        {
                                            registro.Observaciones = "Llanta sin termico registrado";
                                            ListExcel.Add(registro);
                                            continue;
                                        }

                                        request.AddJsonBody(new
                                        {
                                            Termico = Convert.ToInt64(item.Termico),
                                            IdVehiculo = item.IdVehiculo,
                                            KmRecorridos = item.KmRecorridos,
                                            Posicion = item.Posicion,
                                            Medida = item.Medida,
                                            MarcaNueva = item.MarcaNueva,
                                            DisenoNuevo = item.Diseño,
                                            Tipo = item.Tipo,
                                            MarcaReenc = item.MarcaReencauche,
                                            DisenoReenc = item.DiseñoReencauche,
                                            Eje = item.Eje,
                                            MMOriginal = Convert.ToDecimal(item.MMOriginal),
                                            MM1 = Convert.ToDecimal(item.MM1),
                                            MM2 = Convert.ToDecimal(item.MM2),
                                            MM3 = Convert.ToDecimal(item.MM3),
                                            MM4 = Convert.ToDecimal(item.MM4),
                                            MMMin = Convert.ToDecimal(item.MMMin),
                                            MMRetiro = Convert.ToDecimal(item.MMRetiro),
                                            PorcentajeDesgaste = Convert.ToDecimal(item.PorcentajeDesgaste),
                                            FechaInspeccion = item.FechaInspeccion,
                                            Ciudad = item.Ciudad,
                                            MarcaVehiculo = item.MarcaVehiculo,
                                            ModeloVehiculo = item.ModeloVehiculo,
                                            TipoVehiculo = item.TipoVehiculo,
                                            TipoEstructura = item.TipoEstructura,
                                            EstadoInspeccion = item.Referencia,
                                            Observaciones = item.Observaciones
                                        });
                                        request.AddJsonBody(item);
                                        try
                                        {
                                            if (ModelState.IsValid)
                                            {
                                                _log.Info("Accediendo al API");
                                                var response = await _apiClient.ExecuteAsync(request, Method.Post);
                                                //      _log.Info("Registrando Tipo de" + Tipo);
                                                //responseContent = ;
                                                if (response.IsSuccessful)
                                                {
                                                    var content = response.Content;
                                                    // LogedDataViewModel LogedData = JsonSerializer.Deserialize<LogedDataViewModel>(response.Content);
                                                    var prueba = System.Text.Json.JsonSerializer.Deserialize<string>(content);
                                                    registro.Observaciones = prueba;
                                                    ListExcel.Add(registro);
                                                    contador++;
                                                    // Crear una cookie para almacenar el token
                                                    //Response.Cookies.Append("token", LogedData.token);
                                                    //Response.Cookies.Append("expiracion", LogedData.expiracion.ToString());
                                                    // Response.Cookies.Append("user", model.User);
                                                    TempData["MensajeExito"] = "Registro cargados correctamente " + contador + "/" + totalregistros;
                                                }
                                                else
                                                {
                                                    var content = response.Content;
                                                    registro.Observaciones = content;
                                                    ListExcel.Add(registro);
                                                    if (content == null)
                                                    {
                                                        var tabla = ObtenerTablaCargaMasiva(ListExcel);
                                                        ViewBag.Registros = tabla;
                                                        TempData["MensajeExito"] = null;
                                                        TempData["MensajeError"] = "Error al procesar todos los archivos - " + contador + "/" + totalregistros;
                                                        return View();
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                TempData["MensajeExito"] = "Error al cargar registros";
                                            }

                                            //return View(model);
                                        }
                                        catch (JsonParsingException e)
                                        {
                                            _log.Error(e, "Error Obteniendo Token");
                                            _log.Error(e.GetUnderlyingStringUnsafe());
                                            TempData["MensajeError"] = e.Message.ToString();
                                            return RedirectToAction("Index", "Home");
                                            //return View(model);
                                        }
                                        catch (Exception e)
                                        {
                                            _log.Error(e, "Error al iniciar sesión");
                                            _log.Error(responseContent);
                                            TempData["MensajeError"] = e.Message;
                                            return Redirect("Index");
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        TempData["MensajeError"] = ex.Message;
                                        return View();
                                    }
                                }
                                if (contador >= totalregistros / 2)
                                    TempData["MensajeExito"] = "Registro cargados correctamente " + contador + "/" + totalregistros;
                                else
                                    TempData["MensajeError"] = "Error al procesar todos los archivos - " + contador + "/" + totalregistros;
                                var Table = ObtenerTablaCargaMasiva(ListExcel);
                                ViewBag.Registros = Table;
                                return View();
                            }
                        }
                        else
                        {
                            TempData["MensajeError"] = "Advertencia!! Formato Archivo no es válido, asegurese que sea un archivo EXCEL";
                        }
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                TempData["MensajeError"] = "Archivo no encontrado";
                //Console.WriteLine($"The file was not found: '{e}'");
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine($"The directory was not found: '{e}'");
            }
            catch (IOException e)
            {
                Console.WriteLine($"The file could not be opened: '{e}'");
            }

            return View();
        }

        public async Task<ActionResult> ReporteInspeccionLlantas(FiltroReporteInspeccion filtro, string operacion)
        {
            TempData["menu"] = "";
            ListaLlanta model = new ListaLlanta();
            string tokenValue = Request.Cookies["token"];
            var client = new RestClient(configuration["APIClient"]);
            var request = new RestRequest("/api/HistoricoInspeccionLlanta/ListarHistoricoInspeccionesLlanta", Method.Get);
            if (filtro.desde == null)
                filtro.desde = DateTime.Today.AddDays(-7);
            if (filtro.hasta == null)
                filtro.hasta = DateTime.Today;
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            request.AddQueryParameter("FechaInicio", filtro.desde.ToString());
            request.AddQueryParameter("FechaFin", filtro.hasta.ToString());
            request.AddQueryParameter("Termico", filtro.Termico);
            request.AddQueryParameter("IdTipoLlanta", filtro.IdTipoLlanta);
            model.Filtro = filtro;
            var response = await client.ExecuteAsync(request);
            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<HistoricoInspeccionLlantaViewModel> ListaInspecciones = System.Text.Json.JsonSerializer.Deserialize<List<HistoricoInspeccionLlantaViewModel>>(content);

                model.ListaInspeccion = ListaInspecciones;
                if (operacion == "excel")
                {
                    var reporteExcel = ListaInspecciones.Select(x => new ReporteExcelInspeccionLlanta
                    {
                        idHistoInspeLlanta = x.idHistoInspeLlanta,
                        termico = x.termico,
                        placa = x.placa,
                        estadoLlanta = x.estadoLlanta,
                        tipoVehiculo = x.tipoVehiculo,
                        numeroPosicion = x.numeroPosicion,
                        posicion = x.posicion,
                        fechaInspeccion = x.fechaInspeccion,
                        marcaLlanta = x.marcaLlanta,
                        medidaLlanta = x.medidaLlanta,
                        disenioLlanta = x.disenioLlanta
                    }).ToList();
                    var reporte = _reportingEngine.GenerateReportToByteArray(reporteExcel);
                    return File(reporte, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Reporte Inspecciones Llantas.xlsx");
                }
            }
            else
            {
                model.ListaInspeccion = null;
            }

            //TIPO LLANTA
            request = new RestRequest("/api/Tipos/Llantas/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<TipoViewModel> ListaTipoLlanta = System.Text.Json.JsonSerializer.Deserialize<List<TipoViewModel>>(content);
                ListaTipoLlanta.Add(new TipoViewModel
                {
                    idTipo = 0,
                    descripcion = "TODOS"
                });
                model.ListaTipoLlanta = ListaTipoLlanta;
            }
            else
            {
                model.ListaTipoLlanta = null;
            }
            return View(model);
        }

        public ActionResult MontajeLlanta()
        {
            TempData["menu"] = "";
            ListaMontajeLlanta model = new ListaMontajeLlanta();

            //List<ItemVehiculo> ItemVehiculo = new List<ItemVehiculo>();
            string tokenValue = Request.Cookies["token"];
            var client = new RestClient(configuration["APIClient"]);
            var request = new RestRequest("/api/Vehiculo/ListarVehiculos", Method.Get);

            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            var response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;
                //List<ItemVehiculo> ListaVehiculos = System.Text.Json.JsonSerializer.Deserialize<List<ItemVehiculo>>(content);
                List<VehiculoViewModel> ListaVehiculos = System.Text.Json.JsonSerializer.Deserialize<List<VehiculoViewModel>>(content);
                //model.ItemVehiculo = ListaVehiculos;
                model.ListaVehiculos = ListaVehiculos;
            }
            else
            {
                model.ListaVehiculos = null;
            }

            //eje
            request = new RestRequest("/api/Ejes/Llantas/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemEje> listaeje = System.Text.Json.JsonSerializer.Deserialize<List<ItemEje>>(content);

                model.ListaEje = listaeje;
            }
            else
            {
                model.ListaEje = null;
            }


            //obtener termicos disponibles.
            request = new RestRequest("/api/MontajeLlanta/ListarTermicoDisponibles", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<LlantaInformacionApi> listaTermicosDisponibles = System.Text.Json.JsonSerializer.Deserialize<List<LlantaInformacionApi>>(content);

                model.ListaLlantaInformacionApi = listaTermicosDisponibles;
            }
            else
            {
                model.ListaLlantaInformacionApi = null;
            }

            //obtener index de montaje de llantas
            request = new RestRequest("/api/MontajeLlanta/ListarDatosCabbeceraMontaje", Method.Get);

            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<CabeceraMontajeLlantaTableLista> ListaTipoCabeceraMontaje = System.Text.Json.JsonSerializer.Deserialize<List<CabeceraMontajeLlantaTableLista>>(content);

                model.CabeceraMontajeLlantaTableListas = ListaTipoCabeceraMontaje;
            }
            else
            {
                model.CabeceraMontajeLlantaTableListas = null;
            }

            //TIPO LLANTA
            request = new RestRequest("/api/Tipos/Llantas/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<TipoViewModel> ListaTipoLlanta = System.Text.Json.JsonSerializer.Deserialize<List<TipoViewModel>>(content);

                model.ListaTipoLlanta = ListaTipoLlanta;
            }
            else
            {
                model.ListaTipoLlanta = null;
            }

            // var client = new RestClient(configuration["APIClient"];
            request = new RestRequest("/api/Catalogo/ListarTipos", Method.Get);

            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            request.AddQueryParameter("Tipo", "TipoVehiculo");
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<TipoViewModel> ListaTipoVehiculo = System.Text.Json.JsonSerializer.Deserialize<List<TipoViewModel>>(content);

                model.ListaTipoVehiculo = ListaTipoVehiculo;
            }
            else
            {
                model.ListaTipoVehiculo = null;
            }

            request = new RestRequest("/api/TipoEstructura/ListarTipoEstructura", Method.Get);

            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<TipoEstructuraViewModel> ListaTipoEstructura = System.Text.Json.JsonSerializer.Deserialize<List<TipoEstructuraViewModel>>(content);

                model.ListaTipoEstructura = ListaTipoEstructura;
            }
            else
            {
                model.ListaTipoEstructura = null;
            }

            request = new RestRequest("/api/Anio/ListarAnios", Method.Get);

            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemAnio> ListaAnio = System.Text.Json.JsonSerializer.Deserialize<List<ItemAnio>>(content);

                model.ListaAnio = ListaAnio;
            }
            else
            {
                model.ListaAnio = null;
            }

            //diseño
            request = new RestRequest("/api/Disenios/Llantas/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<DisenioViewModel> listadisenio = System.Text.Json.JsonSerializer.Deserialize<List<DisenioViewModel>>(content);

                model.ListaItemDiseno = listadisenio;
            }
            else
            {
                model.ListaItemDiseno = null;
            }

            //medida
            request = new RestRequest("/api/Medidas/Llantas/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemMedida> listamedida = System.Text.Json.JsonSerializer.Deserialize<List<ItemMedida>>(content);

                model.ListaMedidaLlanta = listamedida;
            }
            else
            {
                model.ListaMedidaLlanta = null;
            }

            //LISTAR MARCA CABEZAL

            request = new RestRequest("api/Marcas/Llantas/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            //request.AddQueryParameter("Tipo", "Llanta");

            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemCabezal> ListaMedida = System.Text.Json.JsonSerializer.Deserialize<List<ItemCabezal>>(content);

                model.ListaCabezales = ListaMedida;
            }
            else
            {
                model.ListaCabezales = null;
            }

            //listar modelo cabezal

            request = new RestRequest("api/Modelos/Cabezales/Listar", Method.Get);

            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            //request.AddQueryParameter("Tipo", "Llanta");

            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemModeloCabezal> ListaMedida = System.Text.Json.JsonSerializer.Deserialize<List<ItemModeloCabezal>>(content);

                model.ListaModeloCabezales = ListaMedida;
            }
            else
            {
                model.ListaModeloCabezales = null;
            }
            request = new RestRequest("/api/Marca/ListarMarcas", Method.Get);

            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            request.AddQueryParameter("Tipo", "Vehiculo");
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemMarca> ListaMarca = System.Text.Json.JsonSerializer.Deserialize<List<ItemMarca>>(content);

                model.ListaMarca = ListaMarca;
            }
            else
            {
                model.ListaMarca = null;
            }
            request = new RestRequest("/api/Marcas/Llantas/Listar", Method.Get);

            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemMarcaChasis> ListaMarca = System.Text.Json.JsonSerializer.Deserialize<List<ItemMarcaChasis>>(content);

                model.ListaChasisMarca = ListaMarca;
            }
            else
            {
                model.ListaChasisMarca = null;
            }
            request = new RestRequest("/api/Marcas/Generadores/Listar", Method.Get);

            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemMarcaGenerador> ListaMarca = System.Text.Json.JsonSerializer.Deserialize<List<ItemMarcaGenerador>>(content);

                model.ListaMarcaGenerador = ListaMarca;
            }
            else
            {
                model.ListaMarcaGenerador = null;
            }
            request = new RestRequest("/api/Chasis/ListarChasis", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            // request.AddQueryParameter("Tipo", "Vehiculo");
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemChasis> ListaChasis = System.Text.Json.JsonSerializer.Deserialize<List<ItemChasis>>(content);

                model.ListaChasis = ListaChasis;
            }
            else
            {
                model.ListaPlaca = null;
            }

            request = new RestRequest("/api/Cabezal/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            // request.AddQueryParameter("Tipo", "Vehiculo");
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemCabezal> ListaPlaca = System.Text.Json.JsonSerializer.Deserialize<List<ItemCabezal>>(content);

                model.ListaPlaca = ListaPlaca;
            }
            else
            {
                model.ListaPlaca = null;
            }

            request = new RestRequest("/api/Modelo/ListarModelos", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            request.AddQueryParameter("tipoModelo", "Vehiculo");
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;
                List<ItemModelo> ListaModelo = System.Text.Json.JsonSerializer.Deserialize<List<ItemModelo>>(content);
                model.ListaModelo = ListaModelo;
            }
            else
            {
                model.ListaModelo = null;
            }

            request = new RestRequest("/api/Tipos/Vehiculos/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<TipoViewModel> ListaTipo = System.Text.Json.JsonSerializer.Deserialize<List<TipoViewModel>>(content);

                model.ListaTipoVehiculo = ListaTipo;
            }
            else
            {
                model.ListaTipoVehiculo = null;
            }

            //marca VEHICULO CHASIS
            request = new RestRequest("/api/Marcas/Llantas/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemMarcaChasis> listamarca = System.Text.Json.JsonSerializer.Deserialize<List<ItemMarcaChasis>>(content);

                model.ListaChasisMarca = listamarca;
            }
            else
            {
                model.ListaMarca = null;
            }

            //marca VEHICULO cabezales
            request = new RestRequest("/api/Marcas/Llantas/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemMarca> listamarca = System.Text.Json.JsonSerializer.Deserialize<List<ItemMarca>>(content);

                model.ListaMarca = listamarca;
            }
            else
            {
                model.ListaMarca = null;
            }

            //var Lista = ServicioInspeccionData.ObtenerInspecciones();
            //var detalle = new List<InspeccionDetalleViewModel>();
            //Model.ListaInspecciones = Lista.Select(c => new RegistroInspeccionViewModel
            //{
            //    InspeccionId = c.InspeccionId,
            //    NumeroPlaca = c.NumeroPlaca,
            //    TipoPlacaStr = c.TipoPlaca.ToString(),
            //    Activo = c.Activo,
            //    InspeccionDetalle = detalle,
            //}).ToList();

            //cabezal
            request = new RestRequest("/api/Vehiculo/ListarTipoVehiculosListado", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            request.AddQueryParameter("TipoVehiculo", "CABEZAL");
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemCabezalTipoVehiculo> listaplaca = System.Text.Json.JsonSerializer.Deserialize<List<ItemCabezalTipoVehiculo>>(content);

                model.ListaCabezalTipo = listaplaca;
            }
            else
            {
                model.ListaCabezalTipo = null;
            }

            //chasis
            request = new RestRequest("/api/Vehiculo/ListarTipoVehiculosListado", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            request.AddQueryParameter("TipoVehiculo", "CHASIS");

            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemChasisTipo> listavehicu = System.Text.Json.JsonSerializer.Deserialize<List<ItemChasisTipo>>(content);

                model.ListaChasisTipo = listavehicu;
            }
            else
            {
                model.ListaChasisTipo = null;
            }

            TempData["menu"] = "";

            return View(model);
        }

        //public async Task<string> BuscarCrearActualizarLLanta(LlantaInformacionApi model)
        //{
        //    string tokenValue = Request.Cookies["token"];
        //    // var client = new RestClient(configuration["APIClient"];
        //    var request = new RestRequest("/api/Llanta/BuscarCrearLlanta", Method.Post);
        //    request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
        //    request.AddJsonBody(model);
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            _log.Info("Accediendo al API");
        //            var response = await _apiClient.ExecuteAsync(request, Method.Post);
        //            //      _log.Info("Registrando Tipo de" + Tipo);
        //            //responseContent = ;
        //            if (response.IsSuccessful)
        //            {
        //                return response.RootElement;
        //            };
        //            return "";
        //        }
        //        return "";
        //    }
        //    catch (JsonParsingException e)
        //    {
        //        _log.Error(e, "Error Obteniendo Token");
        //        _log.Error(e.GetUnderlyingStringUnsafe());
        //        TempData["MensajeError"] = e.Message.ToString();
        //        //return RedirectToAction("Index", "Home");
        //        return "";
        //    }
        //    catch (Exception e)
        //    {
        //        _log.Error(e, "Error al iniciar sesión");
        //        _log.Error(responseContent);
        //        TempData["MensajeError"] = e.Message;
        //        return "";
        //    }

        //}
        public string ObtenerTablaCargaMasiva(List<ListaRegistrosCargadosExcel> lista)
        {
            //cabecera tablaResult
            StringBuilder tablaResult = new StringBuilder();
            tablaResult.Append("<thead><tr><td>Termico</td><td>Placa</td><td>Fecha de Inspeccion</td><td>Resultado</td><td>Mensaje</td></tr></thead><tbody>");

            try
            {
                foreach (var item in lista)
                {
                    tablaResult.Append($"<tr><td>{item.Termico}</td><td>{item.Placa}</td><td>{item.FechaInspeccion}</td>");

                    try
                    {
                        #region Validaciones

                        if (item.Observaciones.Contains("Registro exitoso"))
                        {
                            tablaResult.Append($"<td style='background-color:#bde5b9;'>OK</td><td style='background-color:#bde5b9;'>{item.Observaciones}</td></tr>");
                        }
                        else
                        {
                            tablaResult.Append($"<td style='background-color:#ffb2b2;'>No</td><td style='background-color:#ffb2b2;'>{item.Observaciones}</td></tr>");
                        }

                        #endregion Validaciones
                    }
                    catch (Exception e)
                    {
                        tablaResult.Append($"<td style='background-color:#ffb2b2;'>NO</td><td style='background-color:#ffb2b2;'>No se proceso correctamente</td></tr>");
                    }
                }
            }
            catch (Exception e)
            {
                tablaResult.Append($"<tr><td style='background-color:#ffb2b2;'>NO</td><td style='background-color:#ffb2b2;'>" + e.Message + "</td></tr>");
            }

            tablaResult.Append("</tbody>");

            return tablaResult.ToString();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using NLog;
using RestSharp;
using TMS_MANTENIMIENTO.WEB.Models;
using TMS_MANTENIMIENTO.WEB.Servicios;
using Utf8Json;

namespace TMS_MANTENIMIENTO.WEB.Controllers
{
    public class ModelosController : Controller
    {
        private readonly IConfiguration configuration;

        private RestClient _apiClient;
        private RestClient _appAutogoClient;
        private static Logger _log = LogManager.GetLogger("Account");

        private string responseContent { get; }
        private AccountService _AccountService;

        public ModelosController(IConfiguration configuration)
        {
            this.configuration = configuration;
            _apiClient = new RestClient(configuration["APIClient"]);
            _AccountService = new AccountService(configuration);
        }

        //SECCION ModeloS LLANTAS

        public ActionResult IndexModelosCabezales()
        {
            List<ModeloViewModel> ListaModelos = new List<ModeloViewModel>();
            string tokenValue = Request.Cookies["token"];
            var client = new RestClient("https://localhost:7194");
            var request = new RestRequest("/api/Modelos/Cabezales/Listar", Method.Get);

            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            var response = client.Execute(request);

            if (response.Content.Length > 2)
            {
                var content = response.Content;

                ListaModelos = System.Text.Json.JsonSerializer.Deserialize<List<ModeloViewModel>>(content);
            }

            TempData["menu"] = "";

            return View(ListaModelos);
        }

        public async Task<JsonResult> CrearEditarModeloCabezal(ModeloViewModel data)
        {
            string tokenValue = Request.Cookies["token"];

            var request = new RestRequest("/api/Modelos/Cabezales/Registrar", Method.Post/*, DataFormat.Json*/);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            request.AddJsonBody(data);

            if (data.descripcion == null)
            {
                TempData["MensajeError"] = "COMPLETE TODA LA INFORMACIÓN SOLICITADA";
                return Json("ERROR");
            }

            try
            {
                if (data.descripcion != null)
                {
                    if (ModelState.IsValid)
                    {
                        _log.Info("Accediendo al API");
                        var response = await _apiClient.ExecuteAsync(request, Method.Post);
                        _log.Info("Registrando Modelo de LLanta " + data.descripcion);
                        if (response.IsSuccessful)
                        {
                            if (data.idModelo == 0)
                            {
                                TempData["MensajeExito"] = "REGISTRO EXITOSO";
                            }
                            else
                            {
                                TempData["MensajeExito"] = "SE ACTUALIZÓ CORRECTAMENTE";
                            }
                            return Json("OK");
                        }
                        TempData["MensajeError"] = response.Content;
                        return Json("ERROR");
                    }
                    TempData["MensajeError"] = "COMPLETE TODA LA INFORMACIÓN SOLICITADA";
                }
                return Json("ERROR");
            }
            catch (JsonParsingException e)
            {
                _log.Error(e, "Error Obteniendo Token");
                _log.Error(e.GetUnderlyingStringUnsafe());
                TempData["MensajeError"] = e.Message.ToString();
                return Json("ERROR");
            }
            catch (Exception e)
            {
                _log.Error(e, "Error al iniciar sesión");
                _log.Error(responseContent);
                TempData["MensajeError"] = e.Message;
                return Json("ERROR");
            }
        }

        public async Task<JsonResult> DesactivarModeloCabezal(long id)
        {
            string tokenValue = Request.Cookies["token"];
            if (id > 0)
            {
                var request = new RestRequest("/api/Modelos/Cabezales/Deshabilitar", Method.Post/*, DataFormat.Json*/);
                request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
                request.AddQueryParameter("IdModelo", id);

                try
                {
                    if (id != 0)
                    {
                        _log.Info("Accediendo al API");
                        var response = await _apiClient.ExecuteAsync(request, Method.Post);
                        if (response.IsSuccessful)
                        {
                            TempData["MensajeExito"] = "ELIMINADO CORRECTAMENTE";
                            return Json("OK");
                        }
                        else
                        {
                            TempData["MensajeError"] = response.Content;
                            return Json("ERROR");
                        }
                    }
                    else
                    {
                        return Json("ERROR");
                    }
                }
                catch (Exception e)
                {
                    _log.Error(e, "Error al iniciar sesión");
                    _log.Error(responseContent);
                    TempData["MensajeError"] = e.Message;
                    return Json("ERROR");
                }
            }
            return Json("ERROR");
        }

        //FIN SECCION ModeloS LLANTAS

        //SECCION ModeloS VEHICULOS

        public ActionResult IndexModelosVehiculos()
        {
            List<ModeloViewModel> ListaModelos = new List<ModeloViewModel>();
            string tokenValue = Request.Cookies["token"];
            var client = new RestClient("https://localhost:7194");
            var request = new RestRequest("/api/Modelos/Cabezales/Listar", Method.Get);

            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            var response = client.Execute(request);

            if (response.Content.Length > 2)
            {
                var content = response.Content;

                ListaModelos = System.Text.Json.JsonSerializer.Deserialize<List<ModeloViewModel>>(content);
            }

            TempData["menu"] = "";

            return View(ListaModelos);
        }

        //FIN SECCION ModeloS VEHICULOS

        //SECCION ModeloS GENERADORES

        public ActionResult IndexModelosGeneradores()
        {
            List<ModeloViewModel> ListaModelos = new List<ModeloViewModel>();
            string tokenValue = Request.Cookies["token"];
            var client = new RestClient("https://localhost:7194");
            var request = new RestRequest("/api/Modelos/Cabezales/Listar", Method.Get);

            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            var response = client.Execute(request);

            if (response.Content.Length > 2)
            {
                var content = response.Content;

                ListaModelos = System.Text.Json.JsonSerializer.Deserialize<List<ModeloViewModel>>(content);
            }

            TempData["menu"] = "";

            return View(ListaModelos);
        }

        //FIN SECCION ModeloS GENERADORES
    }
}
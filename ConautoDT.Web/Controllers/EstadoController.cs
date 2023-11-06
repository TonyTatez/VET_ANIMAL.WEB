using Microsoft.AspNetCore.Mvc;
using NLog;
using RestSharp;
using TMS_MANTENIMIENTO.WEB.Models;
using TMS_MANTENIMIENTO.WEB.Servicios;
using Utf8Json;

namespace TMS_MANTENIMIENTO.WEB.Controllers
{
    public class EstadosController : Controller
    {
        private readonly IConfiguration configuration;

        private RestClient _apiClient;
        private RestClient _appAutogoClient;
        private static Logger _log = LogManager.GetLogger("Account");
        private string responseContent { get; }
        private AccountService _AccountService;
      
        public EstadosController(IConfiguration configuration)
        {
            this.configuration = configuration;
            _apiClient = new RestClient(configuration["APIClient"]);
            _AccountService = new AccountService(configuration);
        }

        //SECCION Estados LLANTAS

        public ActionResult IndexEstadosLlantas()
        {
            List<EstadoViewModel> ListaEstados = new List<EstadoViewModel>();
            string tokenValue = Request.Cookies["token"];
            var client = new RestClient(configuration["APIClient"]);
            var request = new RestRequest("/api/Estados/Llantas/Listar", Method.Get);

            //copiar y pegar en el resto de controladores
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            //------------------------------------------

            var response = client.Execute(request);

            if (response.Content.Length > 2)
            {
                var content = response.Content;

                ListaEstados = System.Text.Json.JsonSerializer.Deserialize<List<EstadoViewModel>>(content);
            }

            TempData["menu"] = "";

            return View(ListaEstados);
        }

        public async Task<JsonResult> CrearEditarEstadoLlanta(EstadoViewModel data)
        {
            string tokenValue = Request.Cookies["token"];

            var request = new RestRequest("/api/Estados/Llantas/Registrar", Method.Post/*, DataFormat.Json*/);
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
                        _log.Info("Registrando Estado de LLanta " + data.descripcion);
                        if (response.IsSuccessful)
                        {
                            if (data.idEstado == 0)
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

        public async Task<JsonResult> DesactivarEstadoLlanta(long id)
        {
            string tokenValue = Request.Cookies["token"];
            if (id > 0)
            {
                var request = new RestRequest("/api/Estados/Llantas/Deshabilitar", Method.Post/*, DataFormat.Json*/);
                request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
                request.AddQueryParameter("IdEstado", id);

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

        //FIN SECCION Estados LLANTAS

        //SECCION Estados Vehiculos

        public ActionResult IndexEstadosVehiculos()
        {
            List<EstadoViewModel> ListaEstados = new List<EstadoViewModel>();
            string tokenValue = Request.Cookies["token"];
            var client = new RestClient();
            var request = new RestRequest("/api/Estados/Vehiculos/Listar", Method.Get);

            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            var response = client.Execute(request);

            if (response.Content.Length > 2)
            {
                var content = response.Content;

                ListaEstados = System.Text.Json.JsonSerializer.Deserialize<List<EstadoViewModel>>(content);
            }

            TempData["menu"] = "";

            return View(ListaEstados);
        }

        public async Task<JsonResult> CrearEditarEstadoVehiculo(EstadoViewModel data)
        {
            string tokenValue = Request.Cookies["token"];

            var request = new RestRequest("/api/Estados/Vehiculos/Registrar", Method.Post/*, DataFormat.Json*/);
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
                        _log.Info("Registrando Estado de Vehiculos " + data.descripcion);
                        if (response.IsSuccessful)
                        {
                            if (data.idEstado == 0)
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

        public async Task<JsonResult> DesactivarEstadoVehiculo(long id)
        {
            string tokenValue = Request.Cookies["token"];
            if (id > 0)
            {
                var request = new RestRequest("/api/Estados/Vehiculos/Deshabilitar", Method.Post/*, DataFormat.Json*/);
                request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
                request.AddQueryParameter("IdEstado", id);

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

        //FIN SECCION Estados Vehiculos

        //SECCION Estados INSPECCIONES

        public ActionResult IndexEstadosInspecciones()
        {
            List<EstadoViewModel> ListaEstados = new List<EstadoViewModel>();
            string tokenValue = Request.Cookies["token"];
            var client = new RestClient("https://localhost:7194");
            var request = new RestRequest("/api/Estados/Inspecciones/Listar", Method.Get);

            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            var response = client.Execute(request);

            if (response.Content.Length > 2)
            {
                var content = response.Content;

                ListaEstados = System.Text.Json.JsonSerializer.Deserialize<List<EstadoViewModel>>(content);
            }

            TempData["menu"] = "";

            return View(ListaEstados);
        }

        public async Task<JsonResult> CrearEditarEstadoInspeccion(EstadoViewModel data)
        {
            string tokenValue = Request.Cookies["token"];

            var request = new RestRequest("/api/Estados/Inspecciones/Registrar", Method.Post/*, DataFormat.Json*/);
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
                        _log.Info("Registrando Estado de Inspeccion " + data.descripcion);
                        if (response.IsSuccessful)
                        {
                            if (data.idEstado == 0)
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

        public async Task<JsonResult> DesactivarEstadoInspeccion(long id)
        {
            string tokenValue = Request.Cookies["token"];
            if (id > 0)
            {
                var request = new RestRequest("/api/Estados/Inspecciones/Deshabilitar", Method.Post/*, DataFormat.Json*/);
                request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
                request.AddQueryParameter("IdEstado", id);

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

        //FIN SECCION Estados INSPECCIONES
    }
}
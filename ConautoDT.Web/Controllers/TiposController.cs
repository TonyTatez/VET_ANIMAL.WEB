using Microsoft.AspNetCore.Mvc;
using NLog;
using RestSharp;
using TMS_MANTENIMIENTO.WEB.Models;
using TMS_MANTENIMIENTO.WEB.Servicios;
using Utf8Json;

namespace TMS_MANTENIMIENTO.WEB.Controllers
{
    public class TiposController : Controller
    {
        private readonly IConfiguration configuration;

        private RestClient _apiClient;
        private RestClient _appAutogoClient;
        private static Logger _log = LogManager.GetLogger("Account");
        private string responseContent { get; }
        private AccountService _AccountService;
      
        public TiposController(IConfiguration configuration)
        {
            this.configuration = configuration;
            _apiClient = new RestClient(configuration["APIClient"]);
            _AccountService = new AccountService(configuration);
        }

        #region "SECCION TIPOS LLANTAS"

        public ActionResult IndexTiposLlantas()
        {
            List<TipoViewModel> ListaTipos = new List<TipoViewModel>();
            string tokenValue = Request.Cookies["token"];
            var client = new RestClient(configuration["APIClient"]);
            var request = new RestRequest("/api/Tipos/Llantas/Listar", Method.Get);

            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            var response = client.Execute(request);

            if (response.Content.Length > 2)
            {
                var content = response.Content;

                ListaTipos = System.Text.Json.JsonSerializer.Deserialize<List<TipoViewModel>>(content);
            }

            TempData["menu"] = "";

            return View(ListaTipos);
        }

        public async Task<JsonResult> CrearEditarTipoLlanta(TipoViewModel data)
        {
            string tokenValue = Request.Cookies["token"];

            var request = new RestRequest("/api/Tipos/Llantas/Registrar", Method.Post/*, DataFormat.Json*/);
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
                        _log.Info("Registrando Tipo de LLanta " + data.descripcion);
                        if (response.IsSuccessful)
                        {
                            if (data.idTipo == 0)
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

        public async Task<JsonResult> DesactivarTipoLlanta(long id)
        {
            string tokenValue = Request.Cookies["token"];
            if (id > 0)
            {
                var request = new RestRequest("/api/Tipos/Llantas/Deshabilitar", Method.Post/*, DataFormat.Json*/);
                request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
                request.AddQueryParameter("IdTipo", id);

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

        #endregion "SECCION TIPOS LLANTAS"

        #region "SECCION TIPOS VEHICULOS"

        public ActionResult IndexTiposVehiculos()
        {
            List<TipoViewModel> ListaTipos = new List<TipoViewModel>();
            string tokenValue = Request.Cookies["token"];
            var client = new RestClient(configuration["APIClient"]);
            var request = new RestRequest("/api/Tipos/Vehiculos/Listar", Method.Get);

            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            var response = client.Execute(request);

            if (response.Content.Length > 2)
            {
                var content = response.Content;

                ListaTipos = System.Text.Json.JsonSerializer.Deserialize<List<TipoViewModel>>(content);
            }

            TempData["menu"] = "";

            return View(ListaTipos);
        }

        public async Task<JsonResult> CrearEditarTipoVehiculo(TipoViewModel data)
        {
            string tokenValue = Request.Cookies["token"];

            var request = new RestRequest("/api/Tipos/Vehiculos/Registrar", Method.Post/*, DataFormat.Json*/);
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
                        _log.Info("Registrando Tipo de LLanta " + data.descripcion);
                        if (response.IsSuccessful)
                        {
                            if (data.idTipo == 0)
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

        public async Task<JsonResult> DesactivarTipoVehiculo(long id)
        {
            string tokenValue = Request.Cookies["token"];
            if (id > 0)
            {
                var request = new RestRequest("/api/Tipos/Vehiculos/Deshabilitar", Method.Post/*, DataFormat.Json*/);
                request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
                request.AddQueryParameter("IdTipo", id);

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

        #endregion "SECCION TIPOS VEHICULOS"

        #region "SECCION TIPOS GENERADORES"

        public ActionResult IndexTiposGeneradores()
        {
            List<TipoViewModel> ListaTipos = new List<TipoViewModel>();
            string tokenValue = Request.Cookies["token"];
            var client = new RestClient(configuration["APIClient"]);
            var request = new RestRequest("/api/Tipos/Llantas/Listar", Method.Get);

            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            var response = client.Execute(request);

            if (response.Content.Length > 2)
            {
                var content = response.Content;

                ListaTipos = System.Text.Json.JsonSerializer.Deserialize<List<TipoViewModel>>(content);
            }

            TempData["menu"] = "";

            return View(ListaTipos);
        }

        #endregion "SECCION TIPOS GENERADORES"
    }
}
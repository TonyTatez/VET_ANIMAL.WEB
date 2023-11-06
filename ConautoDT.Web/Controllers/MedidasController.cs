using Microsoft.AspNetCore.Mvc;
using NLog;
using RestSharp;
using TMS_MANTENIMIENTO.WEB.Models;
using TMS_MANTENIMIENTO.WEB.Servicios;
using Utf8Json;

namespace TMS_MANTENIMIENTO.WEB.Controllers
{
    public class MedidasController : Controller
    {
        private readonly IConfiguration configuration;
      

        private RestClient _apiClient;
        private RestClient _appAutogoClient;
        private static Logger _log = LogManager.GetLogger("Account");
        private string responseContent { get; }
        private AccountService _AccountService;

        public MedidasController(IConfiguration configuration)
        {
            this.configuration = configuration;
            _apiClient = new RestClient(configuration["APIClient"]);
            _AccountService = new AccountService(configuration);
        }

        //SECCION MedidaS LLANTAS

        public ActionResult IndexMedidasLlantas()
        {
            List<MedidaViewModel> ListaMedidas = new List<MedidaViewModel>();
            string tokenValue = Request.Cookies["token"];
            var client = new RestClient(configuration["APIClient"]);
            var request = new RestRequest("/api/Medidas/Llantas/Listar", Method.Get);

            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            var response = client.Execute(request);

            if (response.Content.Length > 2)
            {
                var content = response.Content;

                ListaMedidas = System.Text.Json.JsonSerializer.Deserialize<List<MedidaViewModel>>(content);
            }

            TempData["menu"] = "";

            return View(ListaMedidas);
        }

        public async Task<JsonResult> CrearEditarMedidaLlanta(MedidaViewModel data)
        {
            string tokenValue = Request.Cookies["token"];

            var request = new RestRequest("/api/Medidas/Llantas/Registrar", Method.Post/*, DataFormat.Json*/);
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
                        _log.Info("Registrando Medida de LLanta " + data.descripcion);
                        if (response.IsSuccessful)
                        {
                            if (data.idMedida == 0)
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

        public async Task<JsonResult> DesactivarMedidaLlanta(long id)
        {
            string tokenValue = Request.Cookies["token"];
            if (id > 0)
            {
                var request = new RestRequest("/api/Medidas/Llantas/Deshabilitar", Method.Post/*, DataFormat.Json*/);
                request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
                request.AddQueryParameter("IdMedida", id);

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

        //FIN SECCION MedidaS LLANTAS

        //SECCION MedidaS VEHICULOS

        public ActionResult IndexMedidasVehiculos()
        {
            List<MedidaViewModel> ListaMedidas = new List<MedidaViewModel>();
            string tokenValue = Request.Cookies["token"];
            var client = new RestClient(configuration["APIClient"]);
            var request = new RestRequest("/api/Medidas/Llantas/Listar", Method.Get);

            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            var response = client.Execute(request);

            if (response.Content.Length > 2)
            {
                var content = response.Content;

                ListaMedidas = System.Text.Json.JsonSerializer.Deserialize<List<MedidaViewModel>>(content);
            }

            TempData["menu"] = "";

            return View(ListaMedidas);
        }

        //FIN SECCION MedidaS VEHICULOS

        //SECCION MedidaS GENERADORES

        public ActionResult IndexMedidasGeneradores()
        {
            List<MedidaViewModel> ListaMedidas = new List<MedidaViewModel>();
            string tokenValue = Request.Cookies["token"];
            var client = new RestClient(configuration["APIClient"]);
            var request = new RestRequest("/api/Medidas/Llantas/Listar", Method.Get);

            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            var response = client.Execute(request);

            if (response.Content.Length > 2)
            {
                var content = response.Content;

                ListaMedidas = System.Text.Json.JsonSerializer.Deserialize<List<MedidaViewModel>>(content);
            }

            TempData["menu"] = "";

            return View(ListaMedidas);
        }

        //FIN SECCION MedidaS GENERADORES
    }
}
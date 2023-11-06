using Microsoft.AspNetCore.Mvc;
using NLog;
using RestSharp;
using TMS_MANTENIMIENTO.WEB.Models;
using TMS_MANTENIMIENTO.WEB.Servicios;
using Utf8Json;

namespace TMS_MANTENIMIENTO.WEB.Controllers
{
    public class MarcasController : Controller
    {

        private readonly IConfiguration configuration;
      
        private RestClient _apiClient;
        private RestClient _appAutogoClient;
        private static Logger _log = LogManager.GetLogger("Account");
        private string responseContent { get; }
        private AccountService _AccountService;

        public MarcasController(IConfiguration configuration)
        {
            this.configuration = configuration;
            _apiClient = new RestClient(configuration["APIClient"]);
            _AccountService = new AccountService(configuration);
        }

        //SECCION MARCAS LLANTAS

        public ActionResult IndexMarcasLlantas()
        {
            List<MarcaViewModel> ListaMarcas = new List<MarcaViewModel>();
            string tokenValue = Request.Cookies["token"];
            var client = new RestClient(configuration["APIClient"]);
            var request = new RestRequest("/api/Marcas/Llantas/Listar", Method.Get);

            //copiar y pegar en el resto de controladores
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            //------------------------------------------

            var response = client.Execute(request);

            if (response.Content.Length > 2)
            {
                var content = response.Content;

                ListaMarcas = System.Text.Json.JsonSerializer.Deserialize<List<MarcaViewModel>>(content);
            }

            TempData["menu"] = "";

            return View(ListaMarcas);
        }

        public async Task<JsonResult> CrearEditarMarcaLlanta(MarcaViewModel data)
        {
            string tokenValue = Request.Cookies["token"];

            var request = new RestRequest("/api/Marcas/Llantas/Registrar", Method.Post/*, DataFormat.Json*/);
            //copiar y pegar en el resto de controladores
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            //------------------------------------------

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
                        _log.Info("Registrando Marca de LLanta " + data.descripcion);
                        if (response.IsSuccessful)
                        {
                            if (data.idMarca == 0)
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

        public async Task<JsonResult> DesactivarMarcaLlanta(long id)
        {
            string tokenValue = Request.Cookies["token"];
            if (id > 0)
            {
                var request = new RestRequest("/api/Marcas/Llantas/Deshabilitar", Method.Post/*, DataFormat.Json*/);
                //copiar y pegar en el resto de controladores
                request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
                //------------------------------------------
                request.AddQueryParameter("IdMarca", id);

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

        //FIN SECCION MARCAS LLANTAS

        //SECCION MARCAS CABEZALES

        public ActionResult IndexMarcasCabezales()
        {
            List<MarcaViewModel> ListaMarcas = new List<MarcaViewModel>();
            string tokenValue = Request.Cookies["token"];
            var client = new RestClient(configuration["APIClient"]);
            var request = new RestRequest("/api/Marcas/Cabezales/Listar", Method.Get);

            //copiar y pegar en el resto de controladores
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            //------------------------------------------

            var response = client.Execute(request);

            if (response.Content.Length > 2)
            {
                var content = response.Content;

                ListaMarcas = System.Text.Json.JsonSerializer.Deserialize<List<MarcaViewModel>>(content);
            }

            TempData["menu"] = "";

            return View(ListaMarcas);
        }

        public async Task<JsonResult> CrearEditarMarcaCabezal(MarcaViewModel data)
        {
            string tokenValue = Request.Cookies["token"];

            var request = new RestRequest("/api/Marcas/Cabezales/Registrar", Method.Post/*, DataFormat.Json*/);
            //copiar y pegar en el resto de controladores
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            //------------------------------------------

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
                        _log.Info("Registrando Marca de Cabezales " + data.descripcion);
                        if (response.IsSuccessful)
                        {
                            if (data.idMarca == 0)
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

        public async Task<JsonResult> DesactivarMarcaCabezal(long id)
        {
            string tokenValue = Request.Cookies["token"];
            if (id > 0)
            {
                var request = new RestRequest("/api/Marcas/Cabezales/Deshabilitar", Method.Post/*, DataFormat.Json*/);
                //copiar y pegar en el resto de controladores
                request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
                //------------------------------------------
                request.AddQueryParameter("IdMarca", id);

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

        //FIN SECCION MARCAS CABEZALES

        //SECCION MARCAS CHASIS

        public ActionResult IndexMarcasChasis()
        {
            List<MarcaViewModel> ListaMarcas = new List<MarcaViewModel>();
            string tokenValue = Request.Cookies["token"];
            var client = new RestClient(configuration["APIClient"]);
            var request = new RestRequest("/api/Marcas/Chasis/Listar", Method.Get);

            //copiar y pegar en el resto de controladores
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            //------------------------------------------

            var response = client.Execute(request);

            if (response.Content.Length > 2)
            {
                var content = response.Content;

                ListaMarcas = System.Text.Json.JsonSerializer.Deserialize<List<MarcaViewModel>>(content);
            }

            TempData["menu"] = "";

            return View(ListaMarcas);
        }

        public async Task<JsonResult> CrearEditarMarcaChasis(MarcaViewModel data)
        {
            string tokenValue = Request.Cookies["token"];

            var request = new RestRequest("/api/Marcas/Chasis/Registrar", Method.Post/*, DataFormat.Json*/);
            //copiar y pegar en el resto de controladores
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            //------------------------------------------

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
                        _log.Info("Registrando Marca de Chasis " + data.descripcion);
                        if (response.IsSuccessful)
                        {
                            if (data.idMarca == 0)
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

        public async Task<JsonResult> DesactivarMarcaChasis(long id)
        {
            string tokenValue = Request.Cookies["token"];
            if (id > 0)
            {
                var request = new RestRequest("/api/Marcas/Chasis/Deshabilitar", Method.Post/*, DataFormat.Json*/);
                //copiar y pegar en el resto de controladores
                request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
                //------------------------------------------
                request.AddQueryParameter("IdMarca", id);

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

        //FIN SECCION MARCAS CHASIS

        //SECCION MARCAS GENERADORES

        public ActionResult IndexMarcasGeneradores()
        {
            List<MarcaViewModel> ListaMarcas = new List<MarcaViewModel>();
            string tokenValue = Request.Cookies["token"];
            var client = new RestClient(configuration["APIClient"]);
            var request = new RestRequest("/api/Marcas/Generadores/Listar", Method.Get);

            //copiar y pegar en el resto de controladores
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            //------------------------------------------

            var response = client.Execute(request);

            if (response.Content.Length > 2)
            {
                var content = response.Content;

                ListaMarcas = System.Text.Json.JsonSerializer.Deserialize<List<MarcaViewModel>>(content);
            }

            TempData["menu"] = "";

            return View(ListaMarcas);
        }

        public async Task<JsonResult> CrearEditarMarcaGenerador(MarcaViewModel data)
        {
            string tokenValue = Request.Cookies["token"];

            var request = new RestRequest("/api/Marcas/Generadores/Registrar", Method.Post/*, DataFormat.Json*/);
            //copiar y pegar en el resto de controladores
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            //------------------------------------------

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
                        _log.Info("Registrando Marca de Generador " + data.descripcion);
                        if (response.IsSuccessful)
                        {
                            if (data.idMarca == 0)
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

        public async Task<JsonResult> DesactivarMarcaGenerador(long id)
        {
            string tokenValue = Request.Cookies["token"];
            if (id > 0)
            {
                var request = new RestRequest("/api/Marcas/Generadores/Deshabilitar", Method.Post/*, DataFormat.Json*/);
                //copiar y pegar en el resto de controladores
                request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
                //------------------------------------------
                request.AddQueryParameter("IdMarca", id);

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

        //FIN SECCION MARCAS GENERADORES
    }
}
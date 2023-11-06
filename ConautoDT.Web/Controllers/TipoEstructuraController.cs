using Microsoft.AspNetCore.Mvc;
using NLog;
using RestSharp;
using TMS_MANTENIMIENTO.WEB.Models;
using TMS_MANTENIMIENTO.WEB.Servicios;
using Utf8Json;

namespace TMS_MANTENIMIENTO.WEB.Controllers
{
    public class TipoEstructuraController : Controller
    {
        private readonly IConfiguration configuration;
       
        private RestClient _apiClient;
        private RestClient _appAutogoClient;
        private static Logger _log = LogManager.GetLogger("Account");
        private string responseContent { get; }
        private AccountService _AccountService;

        public TipoEstructuraController(IConfiguration configuration)
        {
            this.configuration = configuration;
            _apiClient = new RestClient(configuration["APIClient"]);//RestClient(baseURL);
            //_apiClient.ThrowOnAnyError = true;
            //_apiClient.Timeout = 120000;
            //_apiClient.UseUtf8Json();
            _AccountService = new AccountService(configuration);
        }

        public ActionResult Index()
        {
            string Tipo = "TipoEstructura";

            TipoEstructuraViewModel model = new TipoEstructuraViewModel();

            string tokenValue = Request.Cookies["token"];
            var client = new RestClient(configuration["APIClient"]);
            var request = new RestRequest("/api/TipoEstructura/ListarTipoEstructura", Method.Get);

            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            request.AddQueryParameter("Tipo", Tipo);

            var response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemTipoEstructuras> ListaCatalogo = System.Text.Json.JsonSerializer.Deserialize<List<ItemTipoEstructuras>>(content);
                model.ItemTipoEstructura = ListaCatalogo;
            }
            else
            {
                model.ItemTipoEstructura = null;
            }
            //      model.ItemTipoEstructura = Tipo;

            TempData["menu"] = "";

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> GuardaryEditarInfoTipoEstructura(ItemTipoEstructuras model)
        {
            //   string Tipo = "TipoLlanta";
            string tokenValue = Request.Cookies["token"];

            var request = new RestRequest("/api/TipoEstructura/NuevaTipoEstructura", Method.Post/*, DataFormat.Json*/);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            if (model.IdTipoEstructura == 0)
            {
                request.AddJsonBody(new
                {
                    IdTipoEstructura = 0,
                    Descripcion = model.Descripcion,
                    NumeroLlantas = model.NumeroLlantas,
                    Delanteras = model.Delanteras
                    ,
                    Posteriores = model.Posteriores,
                    UrlImagen = model.UrlImagen
                });
            }
            else
            {
                request.AddJsonBody(new
                {
                    IdTipoEstructura = model.IdTipoEstructura,
                    Descripcion = model.Descripcion,
                    NumeroLlantas = model.NumeroLlantas,
                    Delanteras = model.Delanteras
                   ,
                    Posteriores = model.Posteriores,
                    UrlImagen = model.UrlImagen
                });
            }

            request.AddJsonBody(model);

            if (model.Delanteras == null && model.NumeroLlantas != 0 && model.Delanteras == null && model.Posteriores == null)
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
                    _log.Info("Registrando Tipo de Estructura" + model.IdTipoEstructura);
                    //responseContent = ;
                    if (response.IsSuccessful)
                    {
                        // LogedDataViewModel LogedData = JsonSerializer.Deserialize<LogedDataViewModel>(response.Content);

                        // Crear una cookie para almacenar el token
                        //Response.Cookies.Append("token", LogedData.token);
                        //Response.Cookies.Append("expiracion", LogedData.expiracion.ToString());
                        // Response.Cookies.Append("user", model.User);

                        if (model.IdTipoEstructura == 0)
                        {
                            TempData["MensajeExito"] = "Registro Exitoso";
                        }
                        else
                        {
                            TempData["MensajeExito"] = "Se edito correctamente";
                        }

                        return Redirect("Index");
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

        public async Task<ActionResult> DeleteInformacionTipoEstructura(ItemTipoEstructuras model)
        {
            string Tipo = "TipoEstructura";

            string tokenValue = Request.Cookies["token"];
            long id = model.idTipoEstructura;
            if (id > 0)
            {
                var request = new RestRequest("/api/TipoEstructura/EliminaTipoEstructura", Method.Post/*, DataFormat.Json*/);
                request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
                request.AddQueryParameter("IdTipo", id);
                //   request.AddJsonBody(model);

                try
                {
                    if (model.idTipoEstructura != 0)
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

                                return Redirect("Index");
                            }
                            TempData["MensajeError"] = response.Content;
                            return View(model);
                        }
                        TempData["MensajeError"] = "No se pudo eliminar la Catalogo";
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
            }
            return Redirect("Index");
        }
    }
}
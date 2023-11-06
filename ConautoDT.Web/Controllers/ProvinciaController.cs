using Microsoft.AspNetCore.Mvc;
using NLog;
using RestSharp;
using TMS_MANTENIMIENTO.WEB.Models;
using TMS_MANTENIMIENTO.WEB.Servicios;
using Utf8Json;

namespace TMS_MANTENIMIENTO.WEB.Controllers
{
    public class ProvinciaController : Controller

    {
        private readonly IConfiguration configuration;
       
        private RestClient _apiClient;
        private RestClient _appAutogoClient;
        private static Logger _log = LogManager.GetLogger("Account");
        private string responseContent { get; }
        private AccountService _AccountService;

        public ProvinciaController(IConfiguration configuration)
        {
            this.configuration = configuration;
            _apiClient = new RestClient(configuration["APIClient"]);//RestClient(baseURL);
            //_apiClient.ThrowOnAnyError = true;
            //_apiClient.Timeout = 120000;
            //_apiClient.UseUtf8Json();
            _AccountService = new AccountService(configuration);
        }

        // GET: ProvinciaController
        public ActionResult Index()
        {
            ProvinciaViewModel model = new ProvinciaViewModel();

            string tokenValue = Request.Cookies["token"];
            var client = new RestClient(configuration["APIClient"]);
            var request = new RestRequest("/api/Provincia/ListarProvincia", Method.Get);

            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            // request.AddQueryParameter("Tipo", Tipo);

            var response = client.Execute(request);

            if (response.Content.Length > 2)
            {
                var content = response.Content;

                List<ItemProvincia> ListaProvincia = System.Text.Json.JsonSerializer.Deserialize<List<ItemProvincia>>(content);
                model.ItemProvincia = ListaProvincia;
            }
            else
            {
                model.ItemProvincia = null;
            }

            request = new RestRequest("/api/Pais/ListarPais", Method.Get);

            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemPaises> ItemPaises = System.Text.Json.JsonSerializer.Deserialize<List<ItemPaises>>(content);

                model.ListaPaises = ItemPaises;
            }
            else
            {
                model.ListaPaises = null;
            }

            // model.tipoColor = Tipo;

            TempData["menu"] = "";

            return View(model);
        }

        //// GET: ProvinciaController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: ProvinciaController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: ProvinciaController/Create
        [HttpPost]
        public async Task<ActionResult> GuardaryEditarInfo(ItemProvincia model)
        {
            string tokenValue = Request.Cookies["token"];

            var request = new RestRequest("/api/Provincia/NuevaProvincia", Method.Post/*, DataFormat.Json*/);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            if (model.idProvincia == 0)
            {
                request.AddJsonBody(new { idProvincia = 0, descripcion = model.descripcion, idPais = model.idPais/*, tipoProvincia= Tipo*/ });
            }
            else
            {
                request.AddJsonBody(new { idProvincia = model.idProvincia, descripcion = model.descripcion, idPais = model.idPais/*, tipoProvincia = Tipo */});
            }

            request.AddJsonBody(model);

            if (model.descripcion == null)
            {
                TempData["MensajeError"] = "Rellene todos los campos";
                return Redirect("Index");
            }

            // TempData["menu"] = null;
            try
            {
                if (model.descripcion != null)
                {
                    if (ModelState.IsValid)
                    {
                        _log.Info("Accediendo al API");
                        var response = await _apiClient.ExecuteAsync(request, Method.Post);
                        _log.Info("Registrando Provincia "/* + Tipo*/);
                        //responseContent = ;
                        if (response.IsSuccessful)
                        {
                            // LogedDataViewModel LogedData = JsonSerializer.Deserialize<LogedDataViewModel>(response.Content);

                            // Crear una cookie para almacenar el token
                            //Response.Cookies.Append("token", LogedData.token);
                            //Response.Cookies.Append("expiracion", LogedData.expiracion.ToString());
                            // Response.Cookies.Append("user", model.User);

                            if (model.idProvincia == 0)
                            {
                                TempData["MensajeExito"] = "Registro Exitoso";
                            }
                            else
                            {
                                TempData["MensajeExito"] = "Se edito correctamente";
                            }

                            return RedirectToAction("Index", "Provincia");
                        }
                        TempData["MensajeError"] = response.Content;
                        return View(model);
                    }
                    TempData["MensajeError"] = "Rellene todos los campos";
                }
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

        // POST: ProvinciaController/Edit/5
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

        // GET: ProvinciaController/Delete/5
        [HttpPost]
        public async Task<ActionResult> DeleteInformacion(ItemProvincia model)
        {
            string tokenValue = Request.Cookies["token"];
            long id = model.idProvincia;
            var request = new RestRequest("/api/Provincia/EliminarProvincia", Method.Post/*, DataFormat.Json*/);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            request.AddQueryParameter("IdProvincia", id);
            //   request.AddJsonBody(model);

            try
            {
                if (model.idProvincia != 0)
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

                            return RedirectToAction("Index", "Provincia");
                        }
                        TempData["MensajeError"] = response.Content;
                        return View(model);
                    }
                    TempData["MensajeError"] = "No se pudo eliminar la Provincia";
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

        // POST: ProvinciaController/Delete/5
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
    }
}
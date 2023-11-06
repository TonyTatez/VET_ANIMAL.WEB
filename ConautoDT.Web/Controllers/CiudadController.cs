using Microsoft.AspNetCore.Mvc;
using NLog;
using RestSharp;
using TMS_MANTENIMIENTO.WEB.Models;
using TMS_MANTENIMIENTO.WEB.Servicios;
using Utf8Json;

namespace TMS_MANTENIMIENTO.WEB.Controllers
{
    public class CiudadController : Controller

    {
        private readonly IConfiguration configuration;
        
        private RestClient _apiClient;
        private RestClient _appAutogoClient;
        private static Logger _log = LogManager.GetLogger("Account");
        private string responseContent { get; }
        private AccountService _AccountService;

        public CiudadController(IConfiguration configuration)
        {
            this.configuration = configuration;
            _apiClient = new RestClient(configuration["APIClient"]);//RestClient(baseURL);
            //_apiClient.ThrowOnAnyError = true;
            //_apiClient.Timeout = 120000;
            //_apiClient.UseUtf8Json();
            _AccountService = new AccountService(configuration);
        }

        // GET: CiudadController
        public ActionResult Index()
        {
            CiudadViewModel model = new CiudadViewModel();

            string tokenValue = Request.Cookies["token"];
            var client = new RestClient(configuration["APIClient"]);
            var request = new RestRequest("/api/Ciudad/ListarCiudad", Method.Get);

            //copiar y pegar en el resto de controladores
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            //------------------------------------------
            // request.AddQueryParameter("Tipo", Tipo);

            var response = client.Execute(request);

            if (response.Content.Length > 2)
            {
                var content = response.Content;

                List<ItemCiudad> ListaCiudades = System.Text.Json.JsonSerializer.Deserialize<List<ItemCiudad>>(content);
                model.ItemCiudad = ListaCiudades;
            }
            else
            {
                model.ItemCiudad = null;
            }

            request = new RestRequest("/api/Provincia/ListarProvincia", Method.Get);

            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemProvincias> ListaProvincias = System.Text.Json.JsonSerializer.Deserialize<List<ItemProvincias>>(content);

                model.ListaProvincias = ListaProvincias;
            }
            else
            {
                model.ListaProvincias = null;
            }

            // model.tipoColor = Tipo;

            TempData["menu"] = "";

            return View(model);
        }

        //// GET: CiudadController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: CiudadController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: CiudadController/Create
        [HttpPost]
        public async Task<ActionResult> GuardaryEditarInfo(ItemCiudad model)
        {
            string tokenValue = Request.Cookies["token"];

            var request = new RestRequest("/api/Ciudad/NuevaCiudad", Method.Post/*, DataFormat.Json*/);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            if (model.idCiudad == 0)
            {
                request.AddJsonBody(new { idCiudad = 0, descripcionCiudad = model.descripcionCiudad, idProvincia = model.idProvincia/*, tipoCiudad= Tipo*/ });
            }
            else
            {
                request.AddJsonBody(new { idCiudad = model.idCiudad, descripcionCiudad = model.descripcionCiudad, idProvincia = model.idProvincia/*, tipoCiudad = Tipo */});
            }

            request.AddJsonBody(model);

            if (model.descripcionCiudad == null)
            {
                TempData["MensajeError"] = "Rellene todos los campos";
                return Redirect("Index");
            }

            // TempData["menu"] = null;
            try
            {
                if (model.descripcionCiudad != null)
                {
                    if (ModelState.IsValid)
                    {
                        _log.Info("Accediendo al API");
                        var response = await _apiClient.ExecuteAsync(request, Method.Post);
                        _log.Info("Registrando Ciudad "/* + Tipo*/);
                        //responseContent = ;
                        if (response.IsSuccessful)
                        {
                            // LogedDataViewModel LogedData = JsonSerializer.Deserialize<LogedDataViewModel>(response.Content);

                            // Crear una cookie para almacenar el token
                            //Response.Cookies.Append("token", LogedData.token);
                            //Response.Cookies.Append("expiracion", LogedData.expiracion.ToString());
                            // Response.Cookies.Append("user", model.User);

                            if (model.idCiudad == 0)
                            {
                                TempData["MensajeExito"] = "Registro Exitoso";
                            }
                            else
                            {
                                TempData["MensajeExito"] = "Se edito correctamente";
                            }

                            return RedirectToAction("Index", "Ciudad");
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

        // POST: CiudadController/Edit/5
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

        // GET: CiudadController/Delete/5
        [HttpPost]
        public async Task<ActionResult> DeleteInformacion(ItemCiudad model)
        {
            string tokenValue = Request.Cookies["token"];
            long id = model.idCiudad;
            var request = new RestRequest("/api/Ciudad/EliminarCiudad", Method.Post/*, DataFormat.Json*/);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            request.AddQueryParameter("IdCiudad", id);
            //   request.AddJsonBody(model);

            try
            {
                if (model.idCiudad != 0)
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

                            return RedirectToAction("Index", "Ciudad");
                        }
                        TempData["MensajeError"] = response.Content;
                        return View(model);
                    }
                    TempData["MensajeError"] = "No se pudo eliminar la Ciudad";
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

        // POST: CiudadController/Delete/5
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
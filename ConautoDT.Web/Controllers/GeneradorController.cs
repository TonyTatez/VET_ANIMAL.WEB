using Microsoft.AspNetCore.Mvc;
using NLog;
using RestSharp;
using TMS_MANTENIMIENTO.WEB.Models;
using TMS_MANTENIMIENTO.WEB.Servicios;
using Utf8Json;

namespace TMS_MANTENIMIENTO.WEB.Controllers
{
    public class GeneradorController : Controller
    {
        // string Tipo = "Llanta";
        private readonly IConfiguration configuration;
       
        private RestClient _apiClient;
        private RestClient _appAutogoClient;
        private static Logger _log = LogManager.GetLogger("Account");
        private string responseContent { get; }
        private AccountService _AccountService;
        // GET: GeneradorController1

        public GeneradorController(IConfiguration configuration)
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
            GeneradorViewModel model = new GeneradorViewModel();

            string tokenValue = Request.Cookies["token"];
            var client = new RestClient(configuration["APIClient"]);
            var request = new RestRequest("/api/Generador/Listar", Method.Get);

            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            // request.AddQueryParameter("Tipo", Tipo);

            var response = client.Execute(request);
            if (response.Content.Length > 2)
            {
                var content = response.Content;
                List<ItemGenerador> ListaGeneradors = System.Text.Json.JsonSerializer.Deserialize<List<ItemGenerador>>(content);
                model.ItemGenerador = ListaGeneradors;

                if (ListaGeneradors[0].idGenerador != 0)
                {
                    model.ItemGenerador = ListaGeneradors;
                }
                else
                {
                    model.ItemGenerador = null;
                }
            }
            else
            {
                model.ItemGenerador = null;
            }
            // model.tipoGenerador = Tipo;

            TempData["menu"] = "";

            request = new RestRequest("/api/Marcas/Generadores/Listar", Method.Get);

            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            //   request.AddQueryParameter("Tipo", "Vehiculo");
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;
                List<ItemMarca> ListaMarcas = System.Text.Json.JsonSerializer.Deserialize<List<ItemMarca>>(content);
                model.ListaMarca = ListaMarcas;
            }
            else
            {
                model.ListaMarca = null;
            }
            return View(model);
        }

        // POST: GeneradorController1/Create
        [HttpPost]
        public async Task<ActionResult> GuardaryEditarInfo(ItemGenerador model)
        {
            string tokenValue = Request.Cookies["token"];

            var request = new RestRequest("/api/Generador/Registrar", Method.Post/*, DataFormat.Json*/);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            if (model.idGenerador == 0)
            {
                request.AddJsonBody(new { idGenerador = 0, placa = model.placa, vin = model.vin, idMarca = model.idMarca, horometro = model.horometro /*, tipoGenerador = Tipo*/ });
            }
            else
            {
                request.AddJsonBody(new { idGenerador = model.idGenerador, placa = model.placa, vin = model.vin, idMarca = model.idMarca, horometro = model.horometro/*, tipoGenerador = Tipo */});
            }

            request.AddJsonBody(model);

            if (model.placa == null)
            {
                TempData["MensajeError"] = "Rellene todos los campos";
                return Redirect("Index");
            }

            // TempData["menu"] = null;
            try
            {
                if (model.placa != null)
                {
                    if (ModelState.IsValid)
                    {
                        _log.Info("Accediendo al API");
                        var response = await _apiClient.ExecuteAsync(request, Method.Post);
                        _log.Info("Registrando Generador "/* + Tipo*/);
                        //responseContent = ;
                        if (response.IsSuccessful)
                        {
                            // LogedDataViewModel LogedData = JsonSerializer.Deserialize<LogedDataViewModel>(response.Content);

                            // Crear una cookie para almacenar el token
                            //Response.Cookies.Append("token", LogedData.token);
                            //Response.Cookies.Append("expiracion", LogedData.expiracion.ToString());
                            // Response.Cookies.Append("user", model.User);

                            if (model.idGenerador == 0)
                            {
                                TempData["MensajeExito"] = "Registro Exitoso";
                            }
                            else
                            {
                                TempData["MensajeExito"] = "Se edito correctamente";
                            }

                            return RedirectToAction("Index", "Generador");
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

        // POST: GeneradorController1/Edit/5
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

        // GET: GeneradorController1/Delete/5
        [HttpPost]
        public async Task<ActionResult> DeleteInformacion(ItemGenerador model)
        {
            string tokenValue = Request.Cookies["token"];
            long id = model.idGenerador;
            if (id > 0)
            {
                var request = new RestRequest("/api/Generador/Deshabilitar", Method.Post/*, DataFormat.Json*/);
                request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
                request.AddQueryParameter("IdGenerador", id);
                //   request.AddJsonBody(model);

                try
                {
                    if (model.idGenerador != 0)
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

                                return RedirectToAction("Index", "Generador");
                            }
                            TempData["MensajeError"] = response.Content;
                            return View(model);
                        }
                        TempData["MensajeError"] = "No se pudo eliminar la Generador";
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
            return RedirectToAction("Index", "Generador");
            // return View(model);
        }

        // POST: GeneradorController1/Delete/5
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
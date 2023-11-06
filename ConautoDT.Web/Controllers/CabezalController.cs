using Microsoft.AspNetCore.Mvc;
using NLog;
using RestSharp;
using TMS_MANTENIMIENTO.WEB.Models;
using TMS_MANTENIMIENTO.WEB.Servicios;
using Utf8Json;

namespace TMS_MANTENIMIENTO.WEB.Controllers
{
    public class CabezalController : Controller
    {
        // string Tipo = "Llanta";
        private readonly IConfiguration configuration;
       
        private RestClient _apiClient;
        private RestClient _appAutogoClient;
        private static Logger _log = LogManager.GetLogger("Account");
        private string responseContent { get; }
        private AccountService _AccountService;
        // GET: PlacaController1

        public CabezalController(IConfiguration configuration)
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
            CabezalViewModel model = new CabezalViewModel();

            string tokenValue = Request.Cookies["token"];
            var client = new RestClient(configuration["APIClient"]);
            var request = new RestRequest("/api/Cabezal/Listar", Method.Get);

            //copiar y pegar en el resto de controladores
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            //------------------------------------------

            // request.AddQueryParameter("Tipo", Tipo);

            var response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemCabezal> ListaCabezales = System.Text.Json.JsonSerializer.Deserialize<List<ItemCabezal>>(content);
                model.ItemCabezal = ListaCabezales;

                if (ListaCabezales[0].idCabezal != 0)
                {
                    model.ItemCabezal = ListaCabezales;
                }
                else
                {
                    model.ItemCabezal = null;
                }
            }
            else
            {
                model.ItemCabezal = null;
            }

            TempData["menu"] = "";

            request = new RestRequest("/api/Modelos/Cabezales/Listar", Method.Get);

            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            // request.AddQueryParameter("tipoModelo", "Vehiculo");
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemModelo> ListaModelos = System.Text.Json.JsonSerializer.Deserialize<List<ItemModelo>>(content);

                model.ListaModelos = ListaModelos;
            }
            else
            {
                model.ListaModelos = null;
            }

            request = new RestRequest("/api/Marcas/Cabezales/Listar", Method.Get);

            //copiar y pegar en el resto de controladores
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            //------------------------------------------

            // request.AddQueryParameter("tipoModelo", "Vehiculo");
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemMarca> ListaMarcas = System.Text.Json.JsonSerializer.Deserialize<List<ItemMarca>>(content);

                model.ListaMarcas = ListaMarcas;
            }
            else
            {
                model.ListaMarcas = null;
            }

            return View(model);
        }

        // POST: PlacaController1/Create
        [HttpPost]
        public async Task<ActionResult> GuardaryEditarInfo(ItemCabezal model)
        {
            string tokenValue = Request.Cookies["token"];

            var request = new RestRequest("/api/Cabezal/Registrar", Method.Post/*, DataFormat.Json*/);

            //copiar y pegar en el resto de controladores
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            //------------------------------------------

            if (model.idCabezal == 0)
            {
                request.AddJsonBody(new { idCabezal = 0, placa = model.placa, idModelo = model.idModelo, idMarca = model.idMarca });
            }
            else
            {
                request.AddJsonBody(new { idCabezal = model.idCabezal, placa = model.placa, idModelo = model.idModelo, idMarca = model.idMarca });
            }

            request.AddJsonBody(model);

            if (model.placa == null)
            {
                TempData["MensajeError"] = "Rellene todos los campos";
                return Redirect("Index");
            }

            try
            {
                if (model.placa != null)
                {
                    if (ModelState.IsValid)
                    {
                        _log.Info("Accediendo al API");
                        var response = await _apiClient.ExecuteAsync(request, Method.Post);
                        _log.Info("Registrando Cabezal "/* + Tipo*/);
                        if (response.IsSuccessful)
                        {
                            if (model.idCabezal == 0)
                            {
                                TempData["MensajeExito"] = "Registro Exitoso";
                            }
                            else
                            {
                                TempData["MensajeExito"] = "Se edito correctamente";
                            }
                            return RedirectToAction("Index", "Cabezal");
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

        // POST: PlacaController1/Edit/5
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

        // GET: PlacaController1/Delete/5
        [HttpPost]
        public async Task<ActionResult> DeleteInformacion(ItemCabezal model)
        {
            string tokenValue = Request.Cookies["token"];
            long id = model.idCabezal;
            if (id > 0)
            {
                var request = new RestRequest("/api/Cabezal/Deshabilitar", Method.Post/*, DataFormat.Json*/);

                //copiar y pegar en el resto de controladores
                request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
                //------------------------------------------

                request.AddQueryParameter("IdCabezal", model.idCabezal);
                //   request.AddJsonBody(model);

                try
                {
                    if (model.idCabezal != 0)
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

                                return RedirectToAction("Index", "Cabezal");
                            }
                            TempData["MensajeError"] = response.Content;
                            return View(model);
                        }
                        TempData["MensajeError"] = "No se pudo eliminar el Cabezal";
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
            return RedirectToAction("Index", "Cabezal");
        }

        // POST: CabezalController1/Delete/5
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
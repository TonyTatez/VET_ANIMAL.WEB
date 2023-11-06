using Microsoft.AspNetCore.Mvc;
using NLog;
using RestSharp;
using TMS_MANTENIMIENTO.WEB.Models;
using TMS_MANTENIMIENTO.WEB.Servicios;
using Utf8Json;

namespace TMS_MANTENIMIENTO.WEB.Controllers
{
    public class ChasisController : Controller
    {
        // string Tipo = "Llanta";
        private readonly IConfiguration configuration;
       
        private RestClient _apiClient;
        private RestClient _appAutogoClient;
        private static Logger _log = LogManager.GetLogger("Account");
        private string responseContent { get; }
        private AccountService _AccountService;
        // GET: ChasisController1

        public ChasisController(IConfiguration configuration)
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
            ChasisViewModel model = new ChasisViewModel();

            string tokenValue = Request.Cookies["token"];
            var client = new RestClient(configuration["APIClient"]);
            var request = new RestRequest("/api/Chasis/Listar", Method.Get);

            //copiar y pegar en el resto de controladores
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            //------------------------------------------

            // request.AddQueryParameter("Tipo", Tipo);

            var response = client.Execute(request);

            if (response.Content.Length > 2)
            {
                var content = response.Content;

                List<ItemChasis> ListaChasiss = System.Text.Json.JsonSerializer.Deserialize<List<ItemChasis>>(content);
                model.ItemChasis = ListaChasiss;

                if (ListaChasiss[0].idChasis != 0)
                {
                    model.ItemChasis = ListaChasiss;
                }
                else
                {
                    model.ItemChasis = null;
                }
            }
            else
            {
                model.ItemChasis = null;
            }

            request = new RestRequest("/api/Marcas/Chasis/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            response = client.Execute(request);

            if (response.Content.Length > 2)
            {
                var content = response.Content;

                List<ItemMarca> ListaMarcas = System.Text.Json.JsonSerializer.Deserialize<List<ItemMarca>>(content);
                model.ListaMarca = ListaMarcas;

                if (ListaMarcas[0].idMarca != 0)
                {
                    model.ListaMarca = ListaMarcas;
                }
                else
                {
                    model.ListaMarca = null;
                }
            }
            else
            {
                model.ListaMarca = null;
            }

            // model.tipoChasis = Tipo;

            TempData["menu"] = "";

            return View(model);
        }

        // POST: ChasisController1/Create
        [HttpPost]
        public async Task<ActionResult> GuardaryEditarInfo(ItemChasis model)
        {
            string tokenValue = Request.Cookies["token"];

            var request = new RestRequest("/api/Chasis/Registrar", Method.Post/*, DataFormat.Json*/);
            //copiar y pegar en el resto de controladores
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            //------------------------------------------

            if (model.idChasis == 0)
            {
                request.AddJsonBody(new { idChasis = 0, placa = model.placa, vin = model.vin, tamanio = model.tamanio, placaAntigua = model.placaAntigua, idMarcaChasis = model.idMarcaChasis });
            }
            else
            {
                request.AddJsonBody(new { idChasis = model.idChasis, placa = model.placa, vin = model.vin, tamanio = model.tamanio, placaAntigua = model.placaAntigua, idMarcaChasis = model.idMarcaChasis });
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
                        _log.Info("Registrando Chasis "/* + Tipo*/);
                        //responseContent = ;
                        if (response.IsSuccessful)
                        {
                            // LogedDataViewModel LogedData = JsonSerializer.Deserialize<LogedDataViewModel>(response.Content);

                            // Crear una cookie para almacenar el token
                            //Response.Cookies.Append("token", LogedData.token);
                            //Response.Cookies.Append("expiracion", LogedData.expiracion.ToString());
                            // Response.Cookies.Append("user", model.User);

                            if (model.idChasis == 0)
                            {
                                TempData["MensajeExito"] = "Registro Exitoso";
                            }
                            else
                            {
                                TempData["MensajeExito"] = "Se edito correctamente";
                            }

                            return RedirectToAction("Index", "Chasis");
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

        // POST: ChasisController1/Edit/5
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

        // GET: ChasisController1/Delete/5
        [HttpPost]
        public async Task<ActionResult> DeleteInformacion(ItemChasis model)
        {
            string tokenValue = Request.Cookies["token"];
            long id = model.idChasis;
            if (id > 0)
            {
                var request = new RestRequest("/api/Chasis/Deshabilitar", Method.Post/*, DataFormat.Json*/);

                //copiar y pegar en el resto de controladores
                request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
                //------------------------------------------

                request.AddQueryParameter("IdChasis", id);
                //   request.AddJsonBody(model);

                try
                {
                    if (model.idChasis != 0)
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

                                return RedirectToAction("Index", "Chasis");
                            }
                            TempData["MensajeError"] = response.Content;
                            return View(model);
                        }
                        TempData["MensajeError"] = "No se pudo eliminar la Chasis";
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
            return RedirectToAction("Index", "Chasis");
            // return View(model);
        }

        // POST: ChasisController1/Delete/5
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
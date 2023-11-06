using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using RestSharp;
using TMS_MANTENIMIENTO.WEB.Models;
using TMS_MANTENIMIENTO.WEB.Servicios;
using Utf8Json;

namespace TMS_MANTENIMIENTO.WEB.Controllers
{
    public class VehiculoController : Controller
    {
      
        private readonly IConfiguration configuration;
        private RestClient _apiClient;
        private RestClient _appAutogoClient;
        private static Logger _log = LogManager.GetLogger("Account");
        private string responseContent { get; }
        private AccountService _AccountService;

        public VehiculoController(IConfiguration configuration)
        {
            this.configuration = configuration;
            _apiClient = new RestClient(configuration["APIClient"]);//RestClient(baseURL);
            //_apiClient.ThrowOnAnyError = true;
            //_apiClient.Timeout = 120000;
            //_apiClient.UseUtf8Json();
            _AccountService = new AccountService(configuration);
        }

        // GET: VehiculoController
        public ActionResult Index()
        {
            TempData["menu"] = "";

            ListaVehiculo model = new ListaVehiculo();

            //List<ItemVehiculo> ItemVehiculo = new List<ItemVehiculo>();
            List<VehiculoViewModel> ItemVehiculo = new List<VehiculoViewModel>();

            //List<ItemCatalogo> tiposVehiculo = new List<ItemCatalogo>();
            //tiposVehiculo.Add(new ItemCatalogo { idCatalogo = 1, descripcion = "Cabezal" });
            //tiposVehiculo.Add(new ItemCatalogo { idCatalogo = 2, descripcion = "Chasis" });

            //List<ItemChasis> chasis = new List<ItemChasis>();
            //chasis.Add(new ItemChasis { idChasis = 1, descripcion = "ATC 08" });
            //chasis.Add(new ItemChasis { idChasis = 2, descripcion = "ATC 09" });

            //List<ItemMarca> marca = new List<ItemMarca>();
            //marca.Add(new ItemMarca { idMarca = 1, descripcion = "JAC" });

            //List<ItemModelo> modelo = new List<ItemModelo>();
            //modelo.Add(new ItemModelo { idModelo = 1, descripcion = "C7H-540 off road 48 TON" });

            //List<ItemChofer> chofer = new List<ItemChofer>();
            //chofer.Add(new ItemChofer { idChofer = 1, nombres = "Carlos Mejia", identificacion = "099999999"});

            //List<ItemPosicionLlantaVehiculo> itemPosicionLlantaVehiculos = new List<ItemPosicionLlantaVehiculo>();

            //ItemVehiculo item = new ItemVehiculo();
            //item.Id = 1;

            //item.Placa = "GTE-1111";
            //item.Chofer= chofer.FirstOrDefault();
            //item.MarcaVehiculo= marca.FirstOrDefault();
            //item.ModeloVehiculo=modelo.FirstOrDefault();
            //item.Chasis= chasis.FirstOrDefault();
            //item.TipoVehiculo= tiposVehiculo.FirstOrDefault();
            //item.NumeroLlantas= 1;
            //item.TipoEstructuraId = 2;
            //item.image = imagen(item.TipoEstructuraId);
            //item.ListaPosicionLlantaVehiculo = new List<ItemPosicionLlantaVehiculo>();
            //item.ListaPosicionLlantaVehiculo.Add(new ItemPosicionLlantaVehiculo { TermicoId = 1000, VehiculoId = 1, Posicion = 1 });
            //item.ListaPosicionLlantaVehiculo.Add(new ItemPosicionLlantaVehiculo { TermicoId = 1000, VehiculoId = 2, Posicion = 2 });

            //ItemVehiculo.Add(item);
            //item = new ItemVehiculo();
            //item.Id = 2;
            //item.Placa = "GTE-7878";
            //item.Chofer = chofer.FirstOrDefault();
            //item.MarcaVehiculo = marca.FirstOrDefault();
            //item.ModeloVehiculo = modelo.FirstOrDefault();
            //item.Chasis = chasis.FirstOrDefault();
            //item.TipoVehiculo = tiposVehiculo.FirstOrDefault();
            //item.NumeroLlantas = 1;
            //item.TipoEstructuraId = 1;
            //item.image = imagen(item.TipoEstructuraId);
            //item.ListaPosicionLlantaVehiculo = new List<ItemPosicionLlantaVehiculo>();
            //item.ListaPosicionLlantaVehiculo.Add(new ItemPosicionLlantaVehiculo { TermicoId = 1000, VehiculoId = 1, Posicion = 1 });
            //item.ListaPosicionLlantaVehiculo.Add(new ItemPosicionLlantaVehiculo { TermicoId = 1001, VehiculoId = 2, Posicion = 2 });

            //ItemVehiculo.Add(item);
            ////item.ListaPosicionLlantaVehiculo = listaPosicionLlantaVehiculo;
            //model.ItemVehiculo = ItemVehiculo;


            string tokenValue = Request.Cookies["token"];
            var client = new RestClient(configuration["APIClient"]);
            var request = new RestRequest("/api/Vehiculo/ListarVehiculos", Method.Get);

            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            var response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)  
            {
                var content = response.Content;
                List<VehiculoViewModel> ListaVehiculos = System.Text.Json.JsonSerializer.Deserialize<List<VehiculoViewModel>>(content);
                //List<ItemVehiculo> ListaVehiculos = System.Text.Json.JsonSerializer.Deserialize<List<ItemVehiculo>>(content);
                //model.ItemVehiculo = ListaVehiculos;
                model.ListaVehiculos = ListaVehiculos;
            }
            else
            {
                //model.ItemVehiculo = null;
                model.ListaVehiculos = null;
            }
            // var client = new RestClient(configuration["APIClient"]);
            request = new RestRequest("/api/Catalogo/ListarTipos", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            request.AddQueryParameter("Tipo", "TipoVehiculo");
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;
                List<TipoViewModel> ListaTipoVehiculo = System.Text.Json.JsonSerializer.Deserialize<List<TipoViewModel>>(content);
                model.ListaTipoVehiculo = ListaTipoVehiculo;
            }
            else
            {
                model.ListaTipoVehiculo = null;
            }

            request = new RestRequest("/api/TipoEstructura/ListarTipoEstructura", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;
                List<TipoEstructuraViewModel> ListaTipoEstructura = System.Text.Json.JsonSerializer.Deserialize<List<TipoEstructuraViewModel>>(content);
                model.ListaTipoEstructura = ListaTipoEstructura;
            }
            else
            {
                model.ListaTipoEstructura = null;
            }

            request = new RestRequest("/api/Anio/ListarAnios", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;
                List<ItemAnio> ListaAnio = System.Text.Json.JsonSerializer.Deserialize<List<ItemAnio>>(content);
                model.ListaAnio = ListaAnio;
            }
            else
            {
                model.ListaAnio = null;
            }

            request = new RestRequest("/api/Diseño/ListarDiseños", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            request.AddQueryParameter("Tipo", "Llanta");
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;
                var ListaDiseno = System.Text.Json.JsonSerializer.Deserialize<List<DisenioViewModel>>(content);
                model.ListaItemDiseno = ListaDiseno;
            }
            else
            {
                model.ListaItemDiseno = null;
            }

            request = new RestRequest("/api/Medida/ListarMedidas", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            //request.AddQueryParameter("Tipo", "Llanta");
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;
                List<ItemMedida> ListaMedida = System.Text.Json.JsonSerializer.Deserialize<List<ItemMedida>>(content);
                model.ListaMedidaLlanta = ListaMedida;
            }
            else
            {
                model.ListaMedidaLlanta = null;
            }

            //LISTAR MARCA CABEZAL
            request = new RestRequest("api/Marcas/Cabezales/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            //request.AddQueryParameter("Tipo", "Llanta");
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;
                List<ItemCabezal> ListaMedida = System.Text.Json.JsonSerializer.Deserialize<List<ItemCabezal>>(content);
                model.ListaCabezales = ListaMedida;
            }
            else
            {
                model.ListaCabezales = null;
            }

            //listar modelo cabezal
            request = new RestRequest("api/Modelos/Cabezales/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            //request.AddQueryParameter("Tipo", "Llanta");
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;
                List<ItemModeloCabezal> ListaMedida = System.Text.Json.JsonSerializer.Deserialize<List<ItemModeloCabezal>>(content);
                model.ListaModeloCabezales = ListaMedida;
            }
            else
            {
                model.ListaModeloCabezales = null;
            }

            request = new RestRequest("/api/Marca/ListarMarcas", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            request.AddQueryParameter("Tipo", "Vehiculo");
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;
                List<ItemMarca> ListaMarca = System.Text.Json.JsonSerializer.Deserialize<List<ItemMarca>>(content);
                model.ListaMarca = ListaMarca;
            }
            else
            {
                model.ListaMarca = null;
            }

            request = new RestRequest("/api/Marcas/Chasis/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;
                List<ItemMarcaChasis> ListaMarca = System.Text.Json.JsonSerializer.Deserialize<List<ItemMarcaChasis>>(content);
                model.ListaChasisMarca = ListaMarca;
            }
            else
            {
                model.ListaChasisMarca = null;
            }

            request = new RestRequest("/api/Marcas/Generadores/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;
                List<ItemMarcaGenerador> ListaMarca = System.Text.Json.JsonSerializer.Deserialize<List<ItemMarcaGenerador>>(content);
                model.ListaMarcaGenerador = ListaMarca;
            }
            else
            {
                model.ListaMarcaGenerador = null;
            }

            request = new RestRequest("/api/Chasis/ListarChasis", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            // request.AddQueryParameter("Tipo", "Vehiculo");
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;
                List<ItemChasis> ListaChasis = System.Text.Json.JsonSerializer.Deserialize<List<ItemChasis>>(content);
                model.ListaChasis = ListaChasis;
            }
            else
            {
                model.ListaPlaca = null;
            }

            request = new RestRequest("/api/Cabezal/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            // request.AddQueryParameter("Tipo", "Vehiculo");
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;
                List<ItemCabezal> ListaPlaca = System.Text.Json.JsonSerializer.Deserialize<List<ItemCabezal>>(content);
                model.ListaPlaca = ListaPlaca;
            }
            else
            {
                model.ListaPlaca = null;
            }

            request = new RestRequest("/api/Modelo/ListarModelos", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            request.AddQueryParameter("tipoModelo", "Vehiculo");
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;
                List<ItemModelo> ListaModelo = System.Text.Json.JsonSerializer.Deserialize<List<ItemModelo>>(content);
                model.ListaModelo = ListaModelo;
            }
            else
            {
                model.ListaModelo = null;
            }

            request = new RestRequest("/api/Tipos/Vehiculos/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;
                List<TipoViewModel> ListaTipo = System.Text.Json.JsonSerializer.Deserialize<List<TipoViewModel>>(content);
                model.ListaTipoVehiculo = ListaTipo;
            }
            else
            {
                model.ListaTipoVehiculo = null;
            }
            //var Lista = ServicioInspeccionData.ObtenerInspecciones();
            //var detalle = new List<InspeccionDetalleViewModel>();
            //Model.ListaInspecciones = Lista.Select(c => new RegistroInspeccionViewModel
            //{
            //    InspeccionId = c.InspeccionId,
            //    NumeroPlaca = c.NumeroPlaca,
            //    TipoPlacaStr = c.TipoPlaca.ToString(),
            //    Activo = c.Activo,
            //    InspeccionDetalle = detalle,
            //}).ToList();

            TempData["menu"] = "";
            return View(model);
        }

        public string imagen(long TipoEstructuraId)
        {
            if (TipoEstructuraId == 1)
                return "t2";
            else
                return "t3";
        }

        [HttpPost]
        public async Task<ActionResult> FindColor()
        {
            string tokenValue = Request.Cookies["token"];
            var request = new RestRequest("/api/Color/ListarColor", Method.Get/*, DataFormat.Json*/);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            try
            {
                _log.Info("Accediendo al API");
                var response = await _apiClient.ExecuteAsync(request, Method.Get);
                // _log.Info("Registrando Tipo de" + Tipo);
                //responseContent = ;
                var content = response.Content;
                List<ItemColor> Lista = System.Text.Json.JsonSerializer.Deserialize<List<ItemColor>>(content);
                var color = Lista;
                return Json(new { data = color });
            }
            catch (Exception e)
            {
                return Json(new { data = "" });
            }
        }

        [HttpPost]
        public async Task<ActionResult> GuardaryEditarInfo(ListaVehiculo model)
        {
            string tokenValue = Request.Cookies["token"];
            // var client = new RestClient(configuration["APIClient"]);
            var request = new RestRequest("/api/Vehiculo/NuevoVehiculo", Method.Post);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            if (model.Id == 0)
            {
                request.AddJsonBody(new
                {
                    //   IdLlantas = 0,
                    IdVehiculo = 0,
                    TipoVehiculoId = Convert.ToInt64(model.TipoVehiculoId),
                    MarcaVehiculoId = Convert.ToInt64(model.idMarcaCabezal),
                    ModeloVehiculoId = Convert.ToInt64(model.idModeloCabezales),
                    ChoferId = Convert.ToInt64(model.ChoferId),
                    NumeroLlantas = Convert.ToInt64(model.numerosTotalTipo),
                    TipoEstructuraId = Convert.ToInt64(model.TipoEstructuraId),
                    colorId = Convert.ToInt64(model.ColorId),
                    vinChasis = model.VinChasis,
                    Horometro = model.horometro,
                    Tamminio = model.tamanio,
                    VinCabezal = model.VinCabezal,
                    placaAntigua = model.placaAntigua,
                    IdMarcaChasis = model.idMarcaChasis,
                    DescripcionCabezal = model.DescripcionCabezal != null ? model.DescripcionCabezal.ToUpper() : model.DescripcionCabezal,
                    DescripcionChasis = model.DescripcionChasis != null ? model.DescripcionChasis.ToUpper() : model.DescripcionChasis,
                    DescripcionGenerador = model.DescripcionGenerador != null ? model.DescripcionGenerador.ToUpper() : model.DescripcionGenerador,
                    TipoMarcaCabezal = model.TipoMarcaCabezal,
                    anioId = model.idAnio,
                    kilometraje = model.Kilometraje,
                    kilometrajeAcumulado = model.KilometrajeAcumulado
                });
            }
            else
            {
                request.AddJsonBody(new
                {
                    IdVehiculo = model.Id,
                    CabezalId = model.cabezalId,
                    ChasisId = model.chasisId,
                    TipoVehiculoId = Convert.ToInt64(model.TipoVehiculoId),
                    MarcaVehiculoId = Convert.ToInt64(model.idMarcaCabezal),
                    ModeloVehiculoId = Convert.ToInt64(model.idModeloCabezales),
                    ChoferId = Convert.ToInt64(model.ChoferId),
                    NumeroLlantas = Convert.ToInt64(model.numerosTotalTipo),
                    TipoEstructuraId = Convert.ToInt64(model.TipoEstructuraId),
                    colorId = Convert.ToInt64(model.ColorId),
                    vinChasis = model.VinChasis,
                    Horometro = model.horometro,
                    Tamminio = model.tamanio,
                    VinCabezal = model.VinCabezal,
                    placaAntigua = model.placaAntigua,
                    IdMarcaChasis = model.idMarcaChasis,
                    DescripcionCabezal = model.DescripcionCabezal != null ? model.DescripcionCabezal.ToUpper() : model.DescripcionCabezal,
                    DescripcionChasis = model.DescripcionChasis != null ? model.DescripcionChasis.ToUpper() : model.DescripcionChasis,
                    DescripcionGenerador = model.DescripcionGenerador != null ? model.DescripcionGenerador.ToUpper() : model.DescripcionGenerador,
                    TipoMarcaCabezal = model.TipoMarcaCabezal,
                    anioId = model.idAnio,
                    kilometraje = model.Kilometraje,
                    kilometrajeAcumulado = model.KilometrajeAcumulado
                });
            }

            request.AddJsonBody(model);
            if (model.DescripcionCabezal == null && model.DescripcionChasis == null && model.DescripcionGenerador == null)
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
                    //      _log.Info("Registrando Tipo de" + Tipo);
                    //responseContent = ;
                    if (response.IsSuccessful && response.Content != "false")
                    {
                        // LogedDataViewModel LogedData = JsonSerializer.Deserialize<LogedDataViewModel>(response.Content);

                        // Crear una cookie para almacenar el token
                        //Response.Cookies.Append("token", LogedData.token);
                        //Response.Cookies.Append("expiracion", LogedData.expiracion.ToString());
                        // Response.Cookies.Append("user", model.User);
                        if (model.Id == 0)
                        {
                            TempData["MensajeExito"] = "Registro Exitoso";
                        }
                        else
                        {
                            TempData["MensajeExito"] = "Se edito correctamente";
                        }
                        return RedirectToAction("Index", "Vehiculo");
                    }
                    TempData["MensajeError"] = "No se pudo registrar";
                    return RedirectToAction("Index", "Vehiculo");
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

        [HttpPost]
        public async Task<ActionResult> FindLlantaPosicion()
        {
            ListaLlanta model = new ListaLlanta();
            string json = Request.Form["json"];
            // Deserializa el JSON en un objeto utilizando Json.NET
            List<PosicionLlantaBusqueda> listaObj = JsonConvert.DeserializeObject<List<PosicionLlantaBusqueda>>(json);
            long id = listaObj[0].vehiculoId;
            long posicionLlanta = listaObj[0].posicionLlanta;
            string tokenValue = Request.Cookies["token"];

            var request = new RestRequest("/api/Llanta/ListarPosicionesLlantasUbicacion", Method.Post/*, DataFormat.Json*/);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            request.AddQueryParameter("Id", id);
            request.AddQueryParameter("Posicion", posicionLlanta);

            try
            {
                _log.Info("Accediendo al API");
                var response = await _apiClient.ExecuteAsync(request, Method.Post);
                // _log.Info("Registrando Tipo de" + Tipo);
                //responseContent = ;
                var content = response.Content;
                List<ItemLlanta> ListaLlantas = System.Text.Json.JsonSerializer.Deserialize<List<ItemLlanta>>(content);
                model.ItemLlanta = ListaLlantas;
                return Json(new { data = model.ItemLlanta });
            }
            catch (Exception e)
            {
                return Json(new { data = "" });
            }
        }

        [HttpPost]
        public async Task<ActionResult> FindTipoEstructura()
        {
            string json = Request.Form["json"];
            // Deserializa el JSON en un objeto utilizando Json.NET
            List<ListaVehiculo> listaObj = JsonConvert.DeserializeObject<List<ListaVehiculo>>(json);
            long id = listaObj[0].TipoEstructuraId;
            string tokenValue = Request.Cookies["token"];

            var request = new RestRequest("/api/TipoEstructura/ListarNumeroLlanta", Method.Post/*, DataFormat.Json*/);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            request.AddQueryParameter("Id", id);

            try
            {
                _log.Info("Accediendo al API");
                var response = await _apiClient.ExecuteAsync(request, Method.Post);
                // _log.Info("Registrando Tipo de" + Tipo);
                //responseContent = ;
                var content = response.Content;
                List<ItemTipoEstructuras> Lista = System.Text.Json.JsonSerializer.Deserialize<List<ItemTipoEstructuras>>(content);
                var numeroLLanta = Lista[0].numeroLlantas;
                return Json(new { data = numeroLLanta });
            }
            catch (Exception e)
            {
                return Json(new { data = "" });
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteInformacion(ListaVehiculo model)
        {
            string tokenValue = Request.Cookies["token"];
            long id = model.Id;
            var request = new RestRequest("/api/Vehiculo/EliminaVehiculo", Method.Post/*, DataFormat.Json*/);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            request.AddQueryParameter("IdVehiculo", id);
            //   request.AddJsonBody(model);
            try
            {
                if (model.Id != 0)
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
                            return RedirectToAction("Index", "Vehiculo");
                        }
                        TempData["MensajeError"] = response.Content;
                        return View(model);
                    }
                    TempData["MensajeError"] = "No se pudo eliminar la Marca";
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



        [HttpPost]
        public async Task<ActionResult> ObtenerVehiculoId()
        {
            string json = Request.Form["json"];
            // Deserializa el JSON en un objeto utilizando Json.NET
            GetVehiculoAnterior vehiculo = JsonConvert.DeserializeObject<GetVehiculoAnterior>(json);
            string tokenValue = Request.Cookies["token"];
            var request = new RestRequest("/api/Vehiculo/ObtenerVehiculoAnterior", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            //IdVehiculo (Cabezal o Chasis)
            long cabezalID = 0;
            long chasisID = 0;

            if (vehiculo.CabezalId == null)
            {
                cabezalID = 0;

            }else
            {
                cabezalID = (long) vehiculo.CabezalId;

            }
            if (vehiculo.ChasisId == null)
            {
                chasisID = 0;

            }
            else
            {
                chasisID = (long)vehiculo.ChasisId;

            }

            request.AddQueryParameter("CabezalId", cabezalID);
            request.AddQueryParameter("ChasisId",chasisID );
            try
            {
                _log.Info("Accediendo al API");
                var response = await _apiClient.ExecuteAsync(request, Method.Get);
                // _log.Info("Registrando Tipo de" + Tipo);
                //responseContent = ;
                var content = response.Content;
                VehiculoViewModel Vehiculo = System.Text.Json.JsonSerializer.Deserialize<VehiculoViewModel>(content);
                return Json(new { data = Vehiculo });
            }
            catch (Exception e)
            {
                return Json(new { data = "" });
            }
        }

        [HttpPost]
        public async Task<ActionResult> ObtenerVehiculo()
        {
            string json = Request.Form["json"];
            // Deserializa el JSON en un objeto utilizando Json.NET
            GetVehiculo vehiculo = JsonConvert.DeserializeObject<GetVehiculo>(json);
            string tokenValue = Request.Cookies["token"];
            var request = new RestRequest("/api/Vehiculo/ObtenerVehiculo", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            //IdVehiculo (Cabezal o Chasis)
            request.AddQueryParameter("IdVehiculo", vehiculo.IdVehiculo);
            request.AddQueryParameter("IdTipoVehiculo", vehiculo.IdTipoVehiculo);
            try
            {
                _log.Info("Accediendo al API");
                var response = await _apiClient.ExecuteAsync(request, Method.Get);
                // _log.Info("Registrando Tipo de" + Tipo);
                //responseContent = ;
                var content = response.Content;
                VehiculoViewModel Vehiculo = System.Text.Json.JsonSerializer.Deserialize<VehiculoViewModel>(content);
                return Json(new { data = Vehiculo });
            }
            catch (Exception e)
            {
                return Json(new { data = "" });
            }
        }

        // GET: VehiculoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VehiculoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VehiculoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: VehiculoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VehiculoController/Edit/5
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

        // GET: VehiculoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VehiculoController/Delete/5
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
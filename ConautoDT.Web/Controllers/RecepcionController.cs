using Microsoft.AspNetCore.Mvc;
using TMS_MANTENIMIENTO.WEB.Models;
//using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NLog;
using RestSharp;
using System.Text;
using TMS_MANTENIMIENTO.WEB.Engines;
//using TMS_MANTENIMIENTO.WEB.Models;
using TMS_MANTENIMIENTO.WEB.Servicios;
using Utf8Json;

namespace TMS_MANTENIMIENTO.WEB.Controllers
{
    public class RecepcionController : Controller
    {
        private readonly IConfiguration configuration;
        public ActionResult Index()
        {
            TempData["menu"] = "";
          
        ListaLlanta model = new ListaLlanta();
        
            //List<ItemInspeccionLlanta> inspeccionesllanta = new List<ItemInspeccionLlanta>();
            //inspeccionesllanta.Add(new ItemInspeccionLlanta { Id=1, TipoId=1, MM1=17, MM2=17, MM3=16, MM4=18, DisenoId= 1, Llanta = null,
            //                    MarcaId=1, VehiculoId=1,Diseno = disenos.FirstOrDefault(),LlantaId=1,Vehiculo= vehiculos.FirstOrDefault(),
            //                    FechaInspeccion=DateTime.Now,Marca= marcas.FirstOrDefault(),Tipo=tipos.FirstOrDefault() });

            //ListarLlantas
            string tokenValue = Request.Cookies["token"];
            var client = new RestClient(configuration["APIClient"]);
            var request = new RestRequest("/api/Llanta/ListarLlantas", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            var response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemLlanta> ListaLlantas = System.Text.Json.JsonSerializer.Deserialize<List<ItemLlanta>>(content);

                model.ItemLlanta = ListaLlantas;
            }
            else
            {
                model.ItemLlanta = null;
            }

            TempData["menu"] = "";

            request = new RestRequest("/api/Tipos/Vehiculos/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<TipoViewModel> ListaTipoVehiculos = System.Text.Json.JsonSerializer.Deserialize<List<TipoViewModel>>(content);

                model.ListaTipoVehiculo = ListaTipoVehiculos;
            }
            else
            {
                model.ListaTipoVehiculo = null;
            }
            //ciudad
            request = new RestRequest("/api/Ciudad/ListarCiudad", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<itemCiudad> ListaCiudad = System.Text.Json.JsonSerializer.Deserialize<List<itemCiudad>>(content);

                model.ListaCiudad = ListaCiudad;
            }
            else
            {
                model.ListaCiudad = null;
            }

            //TIPO LLANTA
            request = new RestRequest("/api/Tipos/Llantas/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<TipoViewModel> ListaTipoLlanta = System.Text.Json.JsonSerializer.Deserialize<List<TipoViewModel>>(content);

                model.ListaTipoLlanta = ListaTipoLlanta;
            }
            else
            {
                model.ListaTipoLlanta = null;
            }

            //diseño
            request = new RestRequest("/api/Disenios/Llantas/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<DisenioViewModel> listadisenio = System.Text.Json.JsonSerializer.Deserialize<List<DisenioViewModel>>(content);

                model.ListaItemDiseno = listadisenio;
            }
            else
            {
                model.ListaItemDiseno = null;
            }

            //medida
            request = new RestRequest("/api/Medidas/Llantas/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemMedida> listamedida = System.Text.Json.JsonSerializer.Deserialize<List<ItemMedida>>(content);

                model.ListaMedidaLlanta = listamedida;
            }
            else
            {
                model.ListaMedidaLlanta = null;
            }

            //estoy aqui para montaje

            //marca VEHICULO CHASIS
            request = new RestRequest("/api/Marcas/Chasis/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);

            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemMarcaChasis> listamarca = System.Text.Json.JsonSerializer.Deserialize<List<ItemMarcaChasis>>(content);

                model.ListaChasisMarca = listamarca;
            }
            else
            {
                model.ListaMarca = null;
            }
            //estado
            request = new RestRequest("/api/Estados/Inspecciones/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemEstado> listestadoins = System.Text.Json.JsonSerializer.Deserialize<List<ItemEstado>>(content);

                model.ListaInspeccionLlanta = listestadoins;
            }
            else
            {
                model.ListaInspeccionLlanta = null;
            }

            //marca VEHICULO cabezales
            request = new RestRequest("/api/Marcas/Cabezales/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemMarca> listamarca = System.Text.Json.JsonSerializer.Deserialize<List<ItemMarca>>(content);

                model.ListaMarca = listamarca;
            }
            else
            {
                model.ListaMarca = null;
            }

            //marca
            request = new RestRequest("/api/Marcas/Llantas/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemMarca> listamarca = System.Text.Json.JsonSerializer.Deserialize<List<ItemMarca>>(content);

                model.ListaMarca = listamarca;
            }
            else
            {
                model.ListaMarca = null;
            }

            //eje
            request = new RestRequest("/api/Ejes/Llantas/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemEje> listaeje = System.Text.Json.JsonSerializer.Deserialize<List<ItemEje>>(content);

                model.ListaEje = listaeje;
            }
            else
            {
                model.ListaEje = null;
            }

            //estado
            request = new RestRequest("/api/Estados/Llantas/Listar", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemEstado> listaeje = System.Text.Json.JsonSerializer.Deserialize<List<ItemEstado>>(content);

                model.ListaEstado = listaeje;
            }
            else
            {
                model.ListaEstado = null;
            }

            //cabezal
            request = new RestRequest("/api/Vehiculo/ListarTipoVehiculosListado", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            request.AddQueryParameter("TipoVehiculo", "CABEZAL");
            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemCabezalTipoVehiculo> listaplaca = System.Text.Json.JsonSerializer.Deserialize<List<ItemCabezalTipoVehiculo>>(content);

                model.ListaCabezalTipo = listaplaca;
            }
            else
            {
                model.ListaCabezalTipo = null;
            }

            //chasis
            request = new RestRequest("/api/Vehiculo/ListarTipoVehiculosListado", Method.Get);
            request.AddParameter("Authorization", string.Format("Bearer " + tokenValue), ParameterType.HttpHeader);
            request.AddQueryParameter("TipoVehiculo", "CHASIS");

            response = client.Execute(request);

            if (response.Content.Length > 2 && response.IsSuccessful == true)
            {
                var content = response.Content;

                List<ItemChasisTipo> listavehicu = System.Text.Json.JsonSerializer.Deserialize<List<ItemChasisTipo>>(content);

                model.ListaChasisTipo = listavehicu;
            }
            else
            {
                model.ListaChasisTipo = null;
            }

            return View(model);
        }

    }
}

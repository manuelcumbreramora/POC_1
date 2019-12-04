using log4net;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace POC_API_v2_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        static HttpClient clientCRUD = new HttpClient();
        static HttpClient clientDUMMY = new HttpClient();

        static async Task<Uri> CreateMensajeAsync(TrazaProceso traza)
        {
            var logger = LogManager.GetLogger(typeof(APIController));
            HttpResponseMessage response = null;
            try
            {
                response = await clientCRUD.PostAsJsonAsync("api/TrazaProceso", traza);
                response.EnsureSuccessStatusCode();
                clientCRUD.CancelPendingRequests();
                logger.Info("Ejecutado Método CreateMensajeAsync");
            }
            catch (Exception ex)
            {
                logger.Error(String.Concat("Error en método CreateMensajeAsync: ", ex.Message));
            }
            return response.Headers.Location;
        }
        static async Task<Uri> EnviaMensajeAsync(TrazaProceso traza)
        {
            var logger = LogManager.GetLogger(typeof(APIController));
            HttpResponseMessage response = null;
            try
            {
                response = await clientDUMMY.PostAsJsonAsync("api/DUMMY/{traza.id}", traza);
                response.EnsureSuccessStatusCode();
                clientDUMMY.CancelPendingRequests();
                logger.Info("Ejecutado Método EnviaMensajeAsync");
            }
            catch (Exception ex)
            {
                logger.Error(String.Concat("Error en método EnviaMensajeAsync: ", ex.Message));
            }
            return response.Headers.Location;
        }

        [HttpGet("ProcesoAPI")]
        public async Task<int> IniciaLlamada()
        {
            var logger = LogManager.GetLogger(typeof(APIController));
            try
            {
                clientCRUD.BaseAddress = new Uri("http://localhost:56454/");
                clientCRUD.DefaultRequestHeaders.Accept.Clear();
                clientCRUD.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                clientDUMMY.BaseAddress = new Uri("http://localhost:57188/");
                clientDUMMY.DefaultRequestHeaders.Accept.Clear();
                clientDUMMY.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                System.Guid guid = System.Guid.NewGuid();

                TrazaProceso tr = new TrazaProceso();
                tr.Id = guid;
                tr.MensajeInicial = "Se realiza solicitud al servicio DUMMY";
                tr.FechaMensajeInicial = DateTime.Now;

                await CreateMensajeAsync(tr);
                await EnviaMensajeAsync(tr);

                logger.Info("Ejecutado proceso API");
                return 1;
            }
            catch (Exception ex)
            {
                logger.Error(String.Concat("Error al procesar PROC_API: ", ex.Message));
                return 0;
            }


            
        }

        [HttpPost]
        public async Task<Uri> PutTrazaProceso(TrazaProceso trazaProceso)
        {
            var logger = LogManager.GetLogger(typeof(APIController));
            logger.Info("Ejecutado Método PutTrazaProceso para recepción de DUMMY");

            return null;
        }

    }
}
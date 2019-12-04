using log4net;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace DUMMY_v_2_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DUMMYController : ControllerBase
    {
        static HttpClient clientCRUD = new HttpClient();
        static HttpClient clientAPI = new HttpClient();

        static async Task<Uri> CreateMensajeAsync(TrazaProceso traza)
        {
            var logger = LogManager.GetLogger(typeof(DUMMYController));
            HttpResponseMessage response = null;

            try
            {
                response = await clientCRUD.PostAsJsonAsync("api/TrazaProceso/ModificarTraza", traza);
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                logger.Error(String.Concat("Error al procesar PROC_DUMMY: ", ex.Message));
                return null;
            }
        }
        static async Task<Uri> EjecutarAccion(TrazaProceso traza)
        {
            var logger = LogManager.GetLogger(typeof(DUMMYController));
            HttpResponseMessage response = null;

            try
            {
                //Thread.Sleep(new Random().Next(60000, 180000));
                Thread.Sleep(10000);
                var cadenaResultado = "";

                switch (new Random().Next(1, 4))
                {
                    case 1:
                        cadenaResultado = "Error";
                        break;
                    case 2:
                        cadenaResultado = "Warning";
                        break;
                    case 3:
                        cadenaResultado = "Success";
                        break;

                }
                traza.MensajeResultado = cadenaResultado;
                traza.FechaMensajeResultado = DateTime.Now;

                response = await clientCRUD.PostAsJsonAsync("api/TrazaProceso/ModificarTraza", traza);
                await ComunicaMensajeAPIAsync(traza);

                logger.Info("Ejecutado Método EjecutarAccion");
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                logger.Error(String.Concat("Error al procesar PROC_DUMMY: ", ex.Message));
                return null;
            }
        }
        static async Task<Uri> ComunicaMensajeAPIAsync(TrazaProceso traza)
        {
            var logger = LogManager.GetLogger(typeof(DUMMYController));
            HttpResponseMessage response = null;

            try
            {
                response = await clientAPI.PostAsJsonAsync("api/TrazaProceso", traza);
                logger.Info("Ejecutado Método ComunicaMensajeAPIAsync");
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                logger.Error(String.Concat("Error al procesar PROC_DUMMY: ", ex.Message));
                return null;
            }            
        }

        [HttpPost("{id}")]
        public async Task<TrazaProceso> IniciaLlamada(TrazaProceso traza)
        {
            var logger = LogManager.GetLogger(typeof(DUMMYController));
            try
            {
                clientCRUD.BaseAddress = new Uri("http://localhost:56454/");
                clientCRUD.DefaultRequestHeaders.Accept.Clear();
                clientCRUD.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                clientAPI.BaseAddress = new Uri("http://localhost:56099/");
                clientAPI.DefaultRequestHeaders.Accept.Clear();
                clientAPI.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                traza.MensajeRecepcion = "OK";
                traza.FechaMensajeRecepcion = DateTime.Now;

                await CreateMensajeAsync(traza);
                await EjecutarAccion(traza);

                logger.Info("Ejecutado proceso DUMMY");

                return traza;

            }catch(Exception ex)
            {
                logger.Error(String.Concat("Error al procesar PROC_DUMMY: ", ex.Message));
                return null;
            }
        }

    }
}
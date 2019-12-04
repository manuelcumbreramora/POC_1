using log4net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Poc_CRUD_v_2_1.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poc_CRUD_v_2_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrazaProcesoController : ControllerBase
    {
        private readonly CRUDContext _context;

        public TrazaProcesoController(CRUDContext context)
        {
            _context = context;
        }

        // GET: api/TrazaProceso
        [HttpGet]
        public IEnumerable<TrazaProceso> GetTrazaProceso()
        {
            return _context.TrazaProceso;
        }

        [HttpPost("ModificarTraza")]
        public async Task<IActionResult> PutTrazaProceso(TrazaProceso trazaProceso)
        {
            var logger = LogManager.GetLogger(typeof(TrazaProcesoController));
            try
            {
                _context.Entry(trazaProceso).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                logger.Info("Ejecutado Método ModificarTraza");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                logger.Error(String.Concat("Error en método PutTrazaProceso: ", ex.Message));
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TrazaProceso>> PostTrazaProceso(TrazaProceso trazaProceso)
        {
            var logger = LogManager.GetLogger(typeof(TrazaProcesoController));
            try
            {
                _context.TrazaProceso.Add(trazaProceso);
                await _context.SaveChangesAsync();
                logger.Info("Ejecutado Método PostTrazaProceso");
                return CreatedAtAction("GetTrazaProceso", new { id = trazaProceso.Id }, trazaProceso);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                logger.Error(String.Concat("Error en método PostTrazaProceso:", ex.Message));
                return NoContent();
            }

        }
    }
}
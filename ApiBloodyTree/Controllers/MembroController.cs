using ApiBloodyTree.Context;
using ApiBloodyTree.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ApiBloodyTree.Business;

namespace ApiBloodyTree.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembroController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly MembroBusiness _membroBusiness;

        public MembroController(AppDbContext context, MembroBusiness membroBusiness)
        {
            _context = context;
            _membroBusiness = membroBusiness;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Membro>>> GetMembro()
        {
            return await _context.Membros.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Membro>> GetMembro(int id)
        {
            var membro = await _context.Membros.FindAsync(id);

            if (membro == null)
            {
                return NotFound();
            }
            return membro;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMembro(int id, Membro membro)
        {
            if(id != membro.IdMembro)
            {
                return BadRequest();
            }

            _context.Entry(membro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(_context.Membros.Any(a => a.IdMembro == id))
                {
                    return BadRequest();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Membro>> PostMembro(Membro membro)
        {
            _context.Membros.Add(membro);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMembro", new { id = membro.IdMembro }, membro);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMembro(int id)
        {
            var membro = await _context.Membros.FindAsync(id);
            if(membro == null)
            {
                return NotFound();
            }

            _context.Membros.Remove(membro);
            await _context.SaveChangesAsync();

            return NoContent();

        }

        [HttpPut("atualizarProbabilidades/{id}")]
        public async Task<IActionResult> AtualizarProbabilidades(int id, [FromQuery] string tipoSanguineoPai, 
            [FromQuery] string tipoSanguineoMae, [FromQuery] bool fatorRhPai, [FromQuery] bool fatorRhMae)
        {
            var membro = await _context.Membros.FindAsync(id);

            if (membro == null)
            {
                return NotFound();
            }

            Dictionary<string, double> probabilidades = _membroBusiness.CalcularTiposSanguineos(tipoSanguineoPai.ToUpper(), tipoSanguineoMae.ToUpper());

            var FatorRh = _membroBusiness.CalcularFatorRh(fatorRhPai, fatorRhMae);

            membro.GrupoA = probabilidades.GetValueOrDefault("A", 0.0);
            membro.GrupoB = probabilidades.GetValueOrDefault("B", 0.0);
            membro.GrupoO = probabilidades.GetValueOrDefault("O", 0.0);
            membro.GrupoAB = probabilidades.GetValueOrDefault("AB", 0.0);
            membro.FatorRh = FatorRh.Item1;
            membro.PortadorNegativo = FatorRh.Item2;

            _context.Entry(membro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Membros.Any(a => a.IdMembro == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
    }
}
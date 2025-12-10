using Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hexagonal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerHandler _clientHandler;

        public CustomersController(ICustomerHandler clientHandler)
        {
            _clientHandler = clientHandler;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] CustomerDto clientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                int newId = _clientHandler.Create(clientDto);
                return CreatedAtAction(nameof(GetById), new { id = newId }, clientDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var client = _clientHandler.GetById(id);
            if (client == null)
            {
                return NotFound(new { message = $"Customer avec ID {id} non trouvé." });
            }
            return Ok(client);
        }

        // -------------------
        // R : READ (Lire tous les clients)
        // -------------------
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var clients = _clientHandler.GetAll();
            return Ok(clients);
        }

        // -------------------
        // U : UPDATE (Mettre à jour un client existant)
        // -------------------
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, [FromBody] CustomerDto clientDto)
        {
            if (id != clientDto.Id)
            {
                return BadRequest(new { message = "L'ID de l'URL ne correspond pas à l'ID de l'objet." });
            }

            try
            {
                _clientHandler.Update(id, clientDto);
                return NoContent();
            }
            catch (InvalidOperationException)
            {
                return NotFound(new { message = $"Customer avec ID {id} non trouvé pour la mise à jour." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // -------------------
        // D : DELETE (Supprimer un client)
        // -------------------
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            try
            {
                _clientHandler.Delete(id);
                return NoContent();
            }
            catch (InvalidOperationException)
            {
                return NotFound(new { message = $"Customer avec ID {id} non trouvé pour la suppression." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

using Application;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hexagonal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustmersController : ControllerBase
    {
        private readonly IClientHandler _clientHandler;

        // Injection du handler de la couche Application
        public CustmersController(IClientHandler clientHandler)
        {
            _clientHandler = clientHandler;
        }

        // -------------------
        // C : CREATE (Créer un nouveau client)
        // -------------------
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] ClientDto clientDto)
        {
            // Validation basique (la validation métier est dans la couche Domain)
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                int newId = _clientHandler.Create(clientDto);
                // Retourne un statut 201 avec l'URI du nouvel objet
                return CreatedAtAction(nameof(GetById), new { id = newId }, clientDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // -------------------
        // R : READ (Lire un client spécifique)
        // -------------------
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var client = _clientHandler.GetById(id);

            if (client == null)
            {
                return NotFound(new { message = $"Client avec ID {id} non trouvé." });
            }

            return Ok(client);
        }

        // -------------------
        // R : READ (Lire tous les clients)
        // -------------------
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
        public IActionResult Put(int id, [FromBody] ClientDto clientDto)
        {
            if (id != clientDto.Id)
            {
                return BadRequest(new { message = "L'ID de l'URL ne correspond pas à l'ID de l'objet." });
            }

            try
            {
                _clientHandler.Update(id, clientDto);
                // Statut 204 indique un succès sans corps de réponse
                return NoContent();
            }
            catch (InvalidOperationException)
            {
                return NotFound(new { message = $"Client avec ID {id} non trouvé pour la mise à jour." });
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
                return NotFound(new { message = $"Client avec ID {id} non trouvé pour la suppression." });
            }
            catch (Exception ex)
            {
                // Peut-être une erreur de clé étrangère (client a des factures)
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

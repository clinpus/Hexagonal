using Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hexagonal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {


        private readonly IUserHandler _userHandler;

        // Injectez votre UserRepository ou votre DbContext ici

        public UserController(IUserHandler userHandler)
        {
            _userHandler = userHandler;
        }


        // -------------------
        // C : CREATE (Créer un nouveau user)
        // -------------------
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([FromBody] UserDto userDto)
        {
            // Validation basique (la validation métier est dans la couche Domain)
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                int newId = _userHandler.Create(userDto);
                // Retourne un statut 201 avec l'URI du nouvel objet
                return CreatedAtAction(nameof(GetById), new { id = newId }, userDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // -------------------
        // R : READ (Lire un user spécifique)
        // -------------------
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var user = _userHandler.GetById(id);

            if (user == null)
            {
                return NotFound(new { message = $"User avec ID {id} non trouvé." });
            }

            return Ok(user);
        }

        // -------------------
        // R : READ (Lire tous les users)
        // -------------------
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var users = _userHandler.GetAll();
            return Ok(users);
        }

        // -------------------
        // U : UPDATE (Mettre à jour un user existant)
        // -------------------
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, [FromBody] UserDto userDto)
        {
            if (id != userDto.Id)
            {
                return BadRequest(new { message = "L'ID de l'URL ne correspond pas à l'ID de l'objet." });
            }

            try
            {
                _userHandler.Update(id, userDto);
                // Statut 204 indique un succès sans corps de réponse
                return NoContent();
            }
            catch (InvalidOperationException)
            {
                return NotFound(new { message = $"User avec ID {id} non trouvé pour la mise à jour." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // -------------------
        // D : DELETE (Supprimer un user)
        // -------------------
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            try
            {
                _userHandler.Delete(id);
                return NoContent();
            }
            catch (InvalidOperationException)
            {
                return NotFound(new { message = $"User avec ID {id} non trouvé pour la suppression." });
            }
            catch (Exception ex)
            {
                // Peut-être une erreur de clé étrangère (user a des factures)
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}

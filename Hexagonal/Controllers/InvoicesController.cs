using Application;
using Application.Interfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace Hexagonal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoicesController : Controller
    {

        private readonly IInvoiceHandler _invoiceHandler;

        public InvoicesController(IInvoiceHandler invoiceHandler)
        {
            // Le Handler fait partie de la couche Application
            _invoiceHandler = invoiceHandler;
        }


        // --------------------------------------
        // C : CREATE (Créer un nouveau invoice)
        // --------------------------------------
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Post([FromBody] InvoiceDto invoiceDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                int newId = _invoiceHandler.Create(invoiceDto);
                // Retourne un statut 201 avec l'URI du nouvel objet
                return CreatedAtAction(nameof(GetById), new { id = newId }, invoiceDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Invoice> GetById(int id)
        {
            var invoice = _invoiceHandler.GetById(id);
            if (invoice == null)
            {
                return NotFound();
            }
            return Ok(invoice);
        }

        // ----------------------------------
        // R : READ (Lire tous les invoices)
        // ----------------------------------
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var invoice = _invoiceHandler.GetAll();
            return Ok(invoice);
        }

        [HttpGet("calculTotal")]
        public ActionResult<Invoice> GetTotalInvoice(int id)
        {
            int[] prices = { 1, 2, 3 };
             int total = _invoiceHandler.CalculTotal(prices);
            if (total == null)
            {
                return NotFound();
            }
            return Ok(total);
        }


        // --------------------------------------
        // C : CREATE (Créer un nouveau invoice)
        // -------------------------------------
        [HttpPost("{invoiceId}/Lines")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddInvoiceLine(int invoiceId, [FromBody] InvoiceLineDto invoiceLineDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //invoiceLineDto.InvoiceId = invoiceId;
                int newId = _invoiceHandler.AddInvoiceLine(invoiceId, invoiceLineDto);
                // Retourne un statut 201 avec l'URI du nouvel objet
                return CreatedAtAction(nameof(GetById), new { id = newId }, invoiceLineDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}

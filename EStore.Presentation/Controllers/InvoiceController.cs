using EStore.Application.Services.Abstracts;
using EStore.Domain.Entities.Concretes;
using EStore.Domain.ViewModels.Invoice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EStore.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpPost("AddInvoice")]
        [Authorize(Roles = "SuperAdmin,Admin,Cashier")]

        public async Task<IActionResult> AddInvoice(AddInvoiceVM addInvoiceVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _invoiceService.AddInvoiceAsync(addInvoiceVM);
            return Ok();
        }
        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Admin,Cashier")]
        public async Task<IActionResult> GetAllInvoices()
        {
            var invoices = await _invoiceService.GetAllInvoicesAsync();
            return Ok(invoices);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "SuperAdmin,Admin,Cashier")]
        public async Task<IActionResult>GetInvoiceById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var invoice = await _invoiceService.GetInvoiceByIdAsync(id);
            return Ok(invoice);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "SuperAdmin,Admin,Cashier")]
        public async Task<IActionResult>DeleteInvoice(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _invoiceService.DeleteInvoiceAsync(id);
            return Ok();
        }
        [HttpPost("refund")]
        public async Task<IActionResult> CreateRefundInvoice(int invoiceId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _invoiceService.CreateRefundInvoiceAsync(invoiceId);
            return Ok();
        }
    }
}

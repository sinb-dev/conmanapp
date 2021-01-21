using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Conman.Models;
namespace Conman.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase 
    {
        private readonly ConmanContext _context;
        public ReservationController(ConmanContext context) {
            _context = context;
        }

        [HttpPost("{requestToken}")]
        public async Task<IActionResult> ReservePort(string requestToken, [FromForm] Reservation requestReservation)
        {
            if (!_context.ValidateAdminToken(requestToken)) return Unauthorized();

            var existing = _context.GetReservation(requestReservation.Port);
            if (existing != null)
            {
                return Problem("Port is already reserved");
            }

            //Validate the requested reservation
            if (requestReservation.ReservedFor == null) 
            {
                requestReservation.ReservedFor = _context.GetToken(requestReservation.Token);
            }
            if (requestReservation.ReservedFor == null)
            {
                return Problem("The user this reservation is for does not exist");
            }

            //Save reservation
            requestReservation.ApprovedBy = _context.GetToken(requestToken);
            requestReservation.Reserved = DateTime.Now;

            _context.Add(requestReservation);
            await _context.SaveChangesAsync();
            requestReservation.ApprovedBy = null;
            
            //Return reservation as confirmation
            return Ok(requestReservation);
        }
        [HttpPost("{requestToken}/clear")]
        public async Task<IActionResult> ClearReservation(string requestToken, [FromForm] Reservation requestReservation)
        {
            if (!_context.ValidateAdminToken(requestToken)) return Unauthorized();

            var existing = _context.GetReservation(requestReservation.Port);
            if (existing == null)
            {
                return Ok("Port is not reserved");
            }

            //Validate the requested reservation
            if (requestReservation.ReservedFor == null) 
            {
                requestReservation.ReservedFor = _context.GetToken(requestReservation.Token);
            }
            if (requestReservation.ReservedFor == null)
            {
                return Problem("The user this reservation is for does not exist");
            }
            requestReservation.ApprovedBy = _context.GetToken(requestToken);

            await _context.SaveChangesAsync();
            _context.Add(requestReservation);
            requestReservation.ApprovedBy = null;
            requestReservation.ReservedFor = null;
            return Ok(requestReservation);
        }
    }
}
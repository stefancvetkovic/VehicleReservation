using MediatR;
using Microsoft.AspNetCore.Mvc;
using VehicleReservation.Application.CQRS.Reservations.Commands.AddReservationForVehicle;
using VehicleReservation.Application.CQRS.Reservations.Queries;
using VehicleReservation.Dto;
using VehicleReservation.WebApi.Controllers.Base;

namespace VehicleReservation.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReservationController : BaseApiController
    {
        /// <summary>
        /// Returns List of all upcoming reservations
        /// </summary>
        /// <returns>List<Reservation></returns>
        // GET: api/<ReservationController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            if (Mediator != null)
            {
                return Ok(await Mediator.Send(new GetAllReservationsQuery()));
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Add new Reservation for vehicle
        /// </summary>
        /// <param name="value"></param>
        /// <returns><c>Reservation</c></returns>
        // POST api/<ReservationController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] ReservationDto reservation)
        {
            return Ok(await Mediator!.Send(new AddReservationForVehicleCommand { ReservationDto = reservation}));
        }
    }
}
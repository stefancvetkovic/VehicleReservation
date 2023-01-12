using Microsoft.AspNetCore.Mvc;
using VehicleReservation.Application.CQRS.Vehicles.Commands.AddVehicle;
using VehicleReservation.Application.CQRS.Vehicles.Commands.DeleteVehicle;
using VehicleReservation.Application.CQRS.Vehicles.Commands.UpdateVehicle;
using VehicleReservation.Application.CQRS.Vehicles.Queries;
using VehicleReservation.Dto;
using VehicleReservation.WebApi.Controllers.Base;

namespace VehicleReservation.WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VehicleController : BaseApiController
    {
        // GET: api/<VehicleReservationController>
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            if (Mediator != null)
            {
                return Ok(await Mediator.Send(new GetAllVehiclesQuery()));
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST api/<VehicleReservationController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] VehicleDto vehicle)
        {
            if (Mediator != null)
            {
                return Ok(await Mediator.Send(new AddVehicleCommand { Vehicle = vehicle}));
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        // PUT api/<VehicleReservationController>/5
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] VehicleDto vehicle)
        {
            if (Mediator != null)
            {
                return Ok(await Mediator.Send(new UpdateVehicleCommand {Vehicle = vehicle }));
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<VehicleReservationController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string id)
        {
            if (Mediator != null)
            {
                return Ok(await Mediator.Send(new DeleteVehicleCommand {VehicleId = id }));
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

﻿using System.ComponentModel.DataAnnotations;
using System.Net;
using GefestVision.API.Management.Repositories;
using GefestVision.Core.Contracts;
using GuardNet;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace GefestVision.API.Management.Controllers;

[ApiController]
[Route("api/v1/devices")]
public class MessageController : ControllerBase
{
    private readonly DeviceRepository _deviceRepository;

    public MessageController(DeviceRepository deviceRepository)
    {
        Guard.NotNull(deviceRepository, nameof(deviceRepository));

        _deviceRepository = deviceRepository;
    }

    /// <summary>
    ///     Receive Message
    /// </summary>
    /// <remarks>Provides capability to update information for a given device.</remarks>
    /// <param name="deviceId">Unique id for a given device</param>
    /// <param name="messageType">Type of message sent to send to device</param>
    /// <param name="message">Payload to send to device</param>
    /// <response code="202">Message sent to device</response>
    /// <response code="400">Message sent to device</response>
    /// <response code="503">We are undergoing some issues</response>
    [HttpPost("{deviceId}/messages/{messageType}", Name = "Message_ReceiveMessage")]
    [ProducesResponseType(typeof(DeviceInfo), StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    [SwaggerResponseHeader(
        new[] { (int)HttpStatusCode.Accepted, (int)HttpStatusCode.BadRequest, (int)HttpStatusCode.InternalServerError },
        "RequestId", "string", "The header that has a request ID that uniquely identifies this operation call")]
    [SwaggerResponseHeader(
        new[] { (int)HttpStatusCode.Accepted, (int)HttpStatusCode.BadRequest, (int)HttpStatusCode.InternalServerError },
        "X-Transaction-Id", "string",
        "The header that has the transaction ID is used to correlate multiple operation calls.")]
    public async Task<IActionResult> SendMessage([FromRoute] string deviceId,
        [FromRoute] [Required] MessageTypes messageType, [FromBody] [Required] Message message)
    {
        if (string.IsNullOrWhiteSpace(message.Content))
        {
            return BadRequest("No message content is provided");
        }

        await _deviceRepository.ReceiveMessageAsync(deviceId, messageType, message);
        return Accepted();
    }
}

﻿using System.Net;
using GefestVision.API.Management.Exceptions;
using GefestVision.API.Management.Repositories;
using GefestVision.Core.Contracts;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace GefestVision.API.Management.Controllers;

[ApiController]
[Route("api/v1/discovery")]
public class DiscoveryController : ControllerBase
{
    private readonly DeviceRegistryRepository _deviceRegistryRepository;
    private readonly ILogger<DeviceController> _logger;

    public DiscoveryController(DeviceRegistryRepository deviceRegistryRepository, ILogger<DeviceController> logger)
    {
        _logger = logger;
        _deviceRegistryRepository = deviceRegistryRepository;
    }

    /// <summary>
    ///     Discover Device
    /// </summary>
    /// <remarks>Provides ID of the device for a given IMEI.</remarks>
    /// <param name="imei">IMEI of the device</param>
    /// <response code="200">Device information is provided</response>
    /// <response code="404">Device was not discovered</response>
    /// <response code="503">We are undergoing some issues</response>
    [HttpGet("{imei}/device", Name = "Discovery_GetDevice")]
    [ProducesResponseType(typeof(DeviceInfo), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    [SwaggerResponseHeader(new[] { (int)HttpStatusCode.OK, (int)HttpStatusCode.NotFound }, "RequestId", "string",
        "The header that has a request ID that uniquely identifies this operation call")]
    [SwaggerResponseHeader(
        new[] { (int)HttpStatusCode.OK, (int)HttpStatusCode.NotFound, (int)HttpStatusCode.InternalServerError },
        "X-Transaction-Id", "string",
        "The header that has the transaction ID is used to correlate multiple operation calls.")]
    public async Task<IActionResult> GetDevice([FromRoute] string imei)
    {
        try
        {
            var data = await _deviceRegistryRepository.GetDeviceIdAsync(imei);
            return Ok(data);
        }
        catch (UnknownDeviceException)
        {
            return NotFound();
        }
    }
}

using Application.DTO;
using Application.IService;
using Microsoft.AspNetCore.Mvc;

namespace InterfaceAdapters.Controllers;

[Route("api/location")]
[ApiController]
public class LocationController : ControllerBase
{
    private readonly ILocationService _service;

    public LocationController(ILocationService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<LocationDTO>> Create(CreateLocationDTO dto)
    {
        var locationInput = new CreateLocationInput { Description = dto.Description };
        var locationCreated = await _service.Create(locationInput);

        if (locationCreated != null)
            return Created("Location Created", locationCreated);

        return BadRequest();
    }
}

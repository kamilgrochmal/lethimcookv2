using Microsoft.AspNetCore.Mvc;

namespace LetHimCookV2.API.API;

[ApiController]
[Route("[controller]")]
public abstract class BaseController : ControllerBase
{
    protected ActionResult<T> OkOrNotFound<T>(T model)
    {
        if (model is not null)
        {
            return Ok(model);
        }

        return NotFound();
    }
}
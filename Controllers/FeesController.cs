using Microsoft.AspNetCore.Mvc;

// Specifies that the class is a controller that uses API patterns
[ApiController]
// Sets the route prefix for the API as "api/fees"
[Route("api/fees")]
public class FeesController : ControllerBase
{
    // Field for the FeesService which will be responsible for the fee-related logic
    private readonly FeesService _feesService;

    // Dependency injection of the FeesService
    public FeesController(FeesService feesService)
    {
        _feesService = feesService;
    }

    // HTTP GET endpoint for obtaining the current payment fee
    // The route will be "api/fees/current"
    [HttpGet("current")]
    public ActionResult<decimal> GetCurrentFee()
    {
        // Retrieve the current fee from the FeesService and return it as an HTTP 200 OK response
        return Ok(_feesService.GetPaymentFee());
    }
}

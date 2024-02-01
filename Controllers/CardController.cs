using Microsoft.AspNetCore.Mvc;

// Define a RESTful API controller for card management
[ApiController]
[Route("api/cards")]
public class CardController : ControllerBase
{
    // Inject the CardService to handle card logic
    private readonly CardService _cardService;

    public CardController(CardService cardService)
    {
        _cardService = cardService;
    }

    // API to create a new card
    [HttpPost]
    public ActionResult<Card> CreateCard()
    {
        // Ok() is an HTTP 200 OK response with the created Card object as the payload
        return Ok(_cardService.CreateCard());
    }

    // API to get the balance of a card by its ID
    [HttpGet("{id}/balance")]
    public ActionResult<decimal> GetBalance(string id)
    {
        return Ok(_cardService.GetBalance(id));
    }

    // API to make a payment
    [HttpPost("{id}/pay")]
    public ActionResult Pay(string id, [FromBody] decimal amount)
    {
        _cardService.Pay(id, amount);
        // Ok() is an HTTP 200 OK response
        return Ok();
    }
}


using isracard.Models.BankDB;
using isracard.Services;
using Microsoft.AspNetCore.Mvc;



namespace isracard.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreditCardController : ControllerBase
    {
        private readonly CreditCardService _creditCardService;

        public CreditCardController(CreditCardService creditCardService)
        {
            _creditCardService = creditCardService;
        }

        [HttpGet("GetBanks")]
        //IActionResult: IActionResult is an interface that defines a generic HTTP response.It allows developers to flexibly return different types of HTTP responses from web API methods or controllers
        public async Task<IActionResult> GetBanks()
        {
            var banks = await _creditCardService.GetBanks();
            return Ok(banks);
            
        }

        [HttpGet("GetCards")]
        public IActionResult GetCards()
        {
            
            var cards = _creditCardService.GetCards();
            Console.WriteLine( cards);
            return Ok(cards);
        }

        [HttpGet("GetSpasificCard")]
        public IEnumerable<CreditCard> GetSpasificCard([FromQuery] string codeNumber, [FromQuery] string bankCode, [FromQuery] bool? isBlocked)
        {
            var cards = _creditCardService.GetCards();
            return cards.Where(c =>
                (string.IsNullOrEmpty(c.CodeNumber) || c.CodeNumber ==  codeNumber) &&
                (string.IsNullOrEmpty(c.BankCode) || c.BankCode == bankCode) &&
                (c.IsBlocked == isBlocked)).ToList();
        }

        [HttpPost("IncreaseCreditLimit")]
        public IActionResult IncreaseCreditLimit([FromBody] IncreaseLimitRequest request)
        {
            var result = _creditCardService.IncreaseLimit(request);
            //Console.WriteLine("l-----------------------------------------------");
            if (result.Success)
            {
                Console.WriteLine(result.Message);
                return Ok(result.Message);
            }
            Console.WriteLine("result faild");
            return BadRequest(result.Message);

        }
        
}


    public class IncreaseLimitRequest
    {
        public string CodeNumber { get; set; }
        public decimal RequestedIncrease { get; set; }
        public string Occupation { get; set; }
        public decimal AverageIncome { get; set; }
        public IncreaseLimitRequest(string CodeNumber, decimal RequestedIncrease, string Occupation, decimal AverageIncome) {
            //Console.WriteLine("in request");
            this.CodeNumber = CodeNumber;
            this.RequestedIncrease = RequestedIncrease;
            this.Occupation = Occupation;
            this.AverageIncome = AverageIncome;
        }
    }
}


using isracard.Models.BankDB;
using isracard.Reposetories;
using System.Collections.Generic;


namespace isracard.Services
{
    public class CreditCardService
    {

        private readonly List<Bank> banks;
        private readonly List<CreditCard> cards;

        private readonly BankRepo _bankRepo;
        //private readonly CreditCardsRepo creditCardsRepo;


        public CreditCardService(BankRepo _bankRepo)
        {
            this._bankRepo = _bankRepo;
           
            cards = new List<CreditCard> {
                new CreditCard { CodeNumber = "1234567890123456", CreatedAt = DateTime.Now.AddYears(-1), ImagePath = "C:\\Users\\LiorPC\\Desktop\\creditcard pic.png", IsBlocked = false, IsDigital = true, AverageIncome = 15000, BankCode = "001" ,Occupation = "employee",Limit = 8000},
                new CreditCard { CodeNumber = "2345678901234567", CreatedAt = DateTime.Now.AddYears(0), ImagePath = "path/to/image2.jpg", IsBlocked = true, IsDigital = false, AverageIncome = 3000, BankCode = "002" ,Occupation = "other",Limit = 6000},
                new CreditCard { CodeNumber = "5467678901234567", CreatedAt = DateTime.Now.AddYears(-6), ImagePath = "path/to/image3.jpg", IsBlocked = false, IsDigital = false, AverageIncome = 4000, BankCode = "003",Occupation = "indepndence",Limit = 7000 }
            };
        }

        //return the list of banks
        public async Task<IEnumerable<Bank>> GetBanks()
        {
            
            return await _bankRepo.GetBanks();
        }

        //return the list of cards
        public IEnumerable<CreditCard> GetCards()
        {
            return cards;
        }

        //public IEnumerable<CreditCard>
    

    public CreditCardResult IncreaseLimit(Controllers.IncreaseLimitRequest request)
        {
            var card = cards.FirstOrDefault(c => c.CodeNumber == request.CodeNumber);
            if (card == null || card.IsBlocked || card.CreatedAt > DateTime.Now.AddMonths(-3) || request.AverageIncome < 12000)
            {
                return new CreditCardResult(false, "Cannot increase Limit due to policy restrictions.");
            }

            decimal increaseAmount = request.Occupation == "employee" ? request.AverageIncome / 2 : request.AverageIncome / 3;
            if (request.Occupation == "employee")
            {
                increaseAmount = request.AverageIncome / 2;
            }
            else if (request.Occupation == "indepndence")
            {
                increaseAmount = request.AverageIncome / 3;
            }
            else
            {
                Console.WriteLine("IncreaseLimit request is denial");
            }


            if (request.RequestedIncrease > increaseAmount)
            {
                return new CreditCardResult(false, "Requested increase amount.");
            }

            if (card.AverageIncome + request.RequestedIncrease > 100000)
            {
                return new CreditCardResult(false, "Cannot exceed the total card Limit of 100,000.");
            }

            card.Limit += request.RequestedIncrease;
            return new CreditCardResult(true, "Credit Limit was increased successfully.");
        }
    }

    public class CreditCardResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        //ctor
        public CreditCardResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }

}


namespace isracard.Models.BankDB
{
    public class CreditCard
    {
        
            public string CodeNumber { get; set; }
            public DateTime CreatedAt { get; set; }
            public string ImagePath { get; set; }
            public bool IsBlocked { get; set; }
            public bool IsDigital { get; set; }
            public decimal AverageIncome { get; set; }
            public decimal Limit { get; set; }
            public string BankCode { get; set; }
            public string Occupation { get; set;}
        
    }
}

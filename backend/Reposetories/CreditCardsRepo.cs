using isracard.Models.BankDB;
using System.Text.Json;

namespace isracard.Reposetories
{
    public class CreditCardRepo { 
        public async Task<List<Cards>> GetCards()
        {
            var s = AppDomain.CurrentDomain.BaseDirectory;
            Console.WriteLine(s);
        
        
            string filePath = s + "\\\\cards.json";
            using FileStream openStream = File.OpenRead(filePath);
            string cards = await JsonSerializer.DeserializeAsync<string>(openStream);
        
            return null;

        }
    
    }
    public class Cards
    {
        public List<Cards> cards;
    }
}

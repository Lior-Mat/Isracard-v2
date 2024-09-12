using isracard.Models.BankDB;
using System.Text.Json;

namespace isracard.Reposetories
{
    public class BankRepo
    {

        //get banks
        public async Task<List<Bank>> GetBanks()
        {
                var s = AppDomain.CurrentDomain.BaseDirectory;
                Console.WriteLine(s);
                //string keyWord = "isracard";
                //string result = String.Join("\\", s.Split('\\').TakeWhile(part => !part.Equals(keyWord)).Concat(new[] { keyWord })) + "\\";
                //Console.WriteLine(result);
                string filePath = s + "\\banks.json";
                using FileStream openStream = File.OpenRead(filePath);
            //string t = await JsonSerializer.DeserializeAsync<string>(openStream);
            Banks banks2 = await JsonSerializer.DeserializeAsync<Banks>(openStream);
            using StreamReader reader = new(filePath);
            var json = reader.ReadToEnd();
            Banks banks = JsonSerializer.Deserialize<Banks>(json);
            //banks.banks ?? throw new ArgumentNullException("Banks")
            return banks.banks.ToList();
            
            
        }
    }
    public class Banks
    {
        public List<Bank> banks { get; set; }
    }
}

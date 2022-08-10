// See https://aka.ms/new-console-template for more information
using ConsoleAppDb;
using Microsoft.EntityFrameworkCore;
using SampleWebAPI.Data;
using SampleWebAPI.Domain;


//Entity Framework
SamuraiContext _context = new SamuraiContext();
_context.Database.EnsureCreated();

Console.WriteLine("Sebelum tambah data samurai");
//GetSamurai();
/*Console.WriteLine("Tambah data samurai");
AddSamurai();*/
//AddMultipleSamurai("Samurai 4", "Samurai 5", "Samurai 6","Samurai 7","Samurai 8","Samurai 9");
//GetSamuraiByName("samurai");
//var data = GetById(2);
//Console.WriteLine($"GetById - {data.Id} - {data.Name}");

/*var samurai = _context.Samurais.Find(2);
Console.WriteLine($"{samurai.Id} - {samurai.Name}");*/
//AddMoreThanOneType();

//UpdateSamurai(6,"Rengoku Kyojiro");
//DeleteSamurai(15);
//DeleteMultipleSamurai("Samurai");
//AddSamuraiWithQuote();
//AddQuoteToExistingSamurai();
//AddSamuraiToExistingBattle();
//AddSamuraiWithHorse();
//AddHorseToExistingSamurai();
//GetSamurai();
//GetQuotes();
//GetBattle();
//RemoveSamuraiFromBattle();
//GetBattlesWithSamurais();
//GetQuotesWithSamurai();
//GetSamuraiWithQuotes();
//ProjectionSample();
//GetSamuraiWithHorse();
//QueryWithRawSQLInterpolated();
QueryUsingSP();

Console.ReadKey();

void AddSamurai()
{
    var samurai = new Samurai { Name = "Tanjiro" };
    _context.Samurais.Add(samurai);
    _context.SaveChanges();
}
void AddMultipleSamurai(params string[] names)
{
    foreach(string name in names)
    {
        _context.Samurais.Add(new Samurai { Name= name });
    }
    _context.SaveChanges();
}
void AddMoreThanOneType()
{
    _context.AddRange(new Samurai { Name = "Muzan Kibursuji" },
        new Samurai { Name = "Haomaru" },
        new Battle { Name = "Battle of Anegawa"},
        new Battle { Name = "Battle of Kyoto"});
    _context.SaveChanges();
}
Samurai GetById(int id)
{
    //var result = _context.Samurais.Where(s => s.Id == id).FirstOrDefault();
    var result = (from s in _context.Samurais
                  where s.Id == id
                  select s).FirstOrDefault();
    if (result != null)
        return result;
    else
        throw new Exception($"Data dengan id {id} tidak ditemukan");
}
void GetSamurai()
{
    var samurais = _context.Samurais.OrderByDescending(s => s.Name).ToList();
    /*var samurais = (from s in _context.Samurais
                   orderby s.Name descending
                   select s).ToList();*/
    Console.WriteLine($"Jumlah samurai: {samurais.Count}");
    foreach(var samurai in samurais)
    {
        Console.WriteLine($"{samurai.Id} - {samurai.Name}");
    }
}
void GetBattle()
{
    var battles = _context.Battles.OrderBy(b => b.Name).ToList();
    foreach(var battle in battles)
    {
        Console.WriteLine($"{battle.BattleId} - {battle.Name}");
    }
}
void GetSamuraiByName(string name)
{
    var samurais = _context.Samurais.Where(s => s.Name.Contains(name.ToLower())).OrderBy(s => s.Name).ToList();
    foreach(var samurai in samurais)
    {
        Console.WriteLine($"{samurai.Id} - {samurai.Name}");
    }
}
void GetQuotes()
{
    var quotes = _context.Quotes.OrderBy(q => q.Text).ToList();
    foreach( var quote in quotes)
    {
        Console.WriteLine($"{quote.Text} - {quote.SamuraiId}");
    }
}
void GetQuotesWithSamurai()
{
    var quotes = _context.Quotes.Include(q=>q.Samurai).OrderBy(q => q.Text).ToList();
    foreach(var quote in quotes)
    {
        Console.WriteLine($"{quote.Text} by {quote.Samurai.Name}");
    }
}
void GetSamuraiWithQuotes()
{
    var samurais = _context.Samurais.Include(s => s.Quotes).ToList();
    foreach(var samurai in samurais)
    {
        Console.WriteLine($"{samurai.Name}");
        foreach(var quote in samurai.Quotes)
        {
            Console.WriteLine($"-----> {quote.Text}");
        }
    }
}
void UpdateSamurai(int id,string nama)
{
    var samurai = _context.Samurais.FirstOrDefault(s=>s.Id==id);
    if(samurai!=null)
    {
        samurai.Name = nama;
        _context.SaveChanges();
    }
    else
    {
        Console.WriteLine("Data tidak ditemukan");
    }
}
void DeleteSamurai(int id)
{
    var samurai = _context.Samurais.Find(id);
    if(samurai!=null)
    {
        _context.Samurais.Remove(samurai);
        _context.SaveChanges();
    }
    else
    {
        Console.WriteLine("Data tidak ditemukan");
    }
}
void DeleteMultipleSamurai(string name)
{
    var results = _context.Samurais.Where(s => s.Name.Contains(name.ToLower()))
            .OrderBy(s => s.Name).ToList();
    _context.Samurais.RemoveRange(results);
    _context.SaveChanges();
}
void AddQuote()
{
    var newQuote = new Quote
    {
        Text = "Dont fear of death",
        SamuraiId = 1
    };
    _context.Quotes.Add(newQuote);
    _context.SaveChanges();
}
void AddSamuraiWithQuote()
{
    var samurai = new Samurai
    {
        Name = "Miyamoto Musashi",
        Quotes = new List<Quote>
        {
            new Quote { Text = "Think lightly of yourself and deeply word" },
            new Quote { Text = "Do nothing that is no use" }
        }
    };
    _context.Samurais.Add(samurai);
    _context.SaveChanges();
}
void AddQuoteToExistingSamurai()
{
    var samurai = _context.Samurais.Find(2);
    if(samurai!=null)
    {
        samurai.Quotes.Add(new Quote { Text = "Do not fear death"});
        _context.SaveChanges();
    }
}
void ProjectionSample()
{
    var results = _context.Samurais.Include(s=>s.Quotes).Select(s => new { 
        s.Name,
        JumlahQuotes = s.Quotes.Count
    }).ToList();
    foreach(var item in results)
    {
        Console.WriteLine($"{item.Name} - {item.JumlahQuotes}");
    }
}
void AddSamuraiToExistingBattle()
{
    //var battle = _context.Battles.FirstOrDefault(b => b.BattleId == 1);
    //var samurai = _context.Samurais.FirstOrDefault(s => s.Id == 2);

    //var samurai1 = _context.Samurais.Find(1);
    var samurai2 = _context.Samurais.Find(2);
    var samurai3 = _context.Samurais.Find(3);
    var samurai4 = _context.Samurais.Find(4);

    var battle2 = _context.Battles.Find(2);

    //battle.Samurais.Add(samurai);
    samurai2.Battles.Add(battle2);
    samurai3.Battles.Add(battle2);
    samurai4.Battles.Add(battle2);

    _context.SaveChanges();
}
void GetBattlesWithSamurais()
{
    var battles = _context.Battles.Include(b => b.Samurais).ToList();
    foreach(var battle in battles)
    {
        Console.WriteLine($"{battle.BattleId} - {battle.Name} :");
        foreach(var samurai in battle.Samurais)
        {
            Console.WriteLine($"-----> {samurai.Id} - {samurai.Name}");
        }
    }
}
void RemoveSamuraiFromBattle()
{
    var battles = _context.Battles.Include(b => b.Samurais.Where(s => s.Id == 2))
        .FirstOrDefault(b => b.BattleId == 2);
    var samurai = battles.Samurais[0];
    battles.Samurais.Remove(samurai);
    _context.SaveChanges();
}
void AddSamuraiWithHorse()
{
    var samurai = new Samurai { Name = "Kenshin Himura", Horse = new Horse { Name = "White Tornado" } };
    _context.Samurais.Add(samurai);
    _context.SaveChanges();
}
void AddHorseToExistingSamurai()
{
    var samurai = _context.Samurais.FirstOrDefault(s => s.Id == 1);
    samurai.Horse = new Horse { Name = "Red Tornado" };
    _context.SaveChanges();
}
void GetSamuraiWithHorse()
{
    var samurais = _context.Samurais.Include(s => s.Horse).ToList();
    foreach(var samurai in samurais)
    {
        if(samurai.Horse!=null)
            Console.WriteLine($"{samurai.Name} - {samurai.Horse.Name}");
    }
}

void QueryWithRawSQL()
{
    //jangan digunakan karena rawan SQL Injection
    string name = "Zenitsu";
    var samurais = _context.Samurais.FromSqlRaw($"select * from Samurais where Name='{name}' ").ToList();
    foreach(var samurai in samurais)
    {
        Console.WriteLine($"{samurai.Name}");
    }
}

void QueryWithRawSQLInterpolated()
{
    string name = "Zenitsu";
    var samurais = _context.Samurais.FromSqlInterpolated($"select * from Samurais where Name={name}").ToList();
    foreach (var samurai in samurais)
    {
        Console.WriteLine($"{samurai.Name}");
    }
}

void GetSamuraiBattleStats()
{
    var stats = _context.SamuraiBattleStats.OrderBy(s => s.Name).ToList();
    foreach(var stat in stats)
    {
        Console.WriteLine($"{stat.Name} - {stat.NumberOfBattles} - {stat.EarliestBattle}");
    }
}

void QueryUsingSP()
{
    var text = "light";
    var samurais = _context.Samurais.FromSqlInterpolated($"exec dbo.SamuraisWhoSaidAWord {text}").ToList();
    foreach(var samurai in samurais)
    {
        Console.WriteLine($"{samurai.Id} - {samurai.Name}");
    }
}





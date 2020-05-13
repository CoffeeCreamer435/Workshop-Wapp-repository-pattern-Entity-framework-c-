# Optimalisatie

Om een applicatie snel te houden moeten quries en databases geoptimaliseerd worden worden. Het optimaliseren van databases wordt over het algemeen gedaan met keys. Hier gaan wij niet dieper op in omdat dit buiten de scope valt.
Een onderdeel die wel binnen de scope valt is het optimaliseren van quries.

Een querie optimaliseren is niet alleen code aanpassen maar ook weten hoe de architectuur van de applicatie in elkaar zit. Hierdoor kunnen query’s gerichter geschreven worden.

## Know your Deferred Calls

Bij defferd calls moet jij je even voorstellen dat er een qurie geschreven moet worden vanuit een tabel die meer dan 100.000 record bevat. Het ophalen van 100.000 records duurt lang en is niet efficent zoals te zien is in code voorbeeld 1.

```C#
var airBNBContext = _context.Reviews.Include(r => r.Listing).ToList().FirstOrDefault();
```

Deze code doet eerst alle 100.000 records ophalen en vervolgens pakt hij de eerste uit deze lijst. Dit betekent dus dat de database lang bezig is en de server veel recourses nodig heeft. Door de qurie te optimaliseren hoeft de database nog maar 1 record op te halen en de applicatie hoeft er nog maar 1 te verwerken.

```c#
var airBNBContext =  _context.Reviews.Include(r => r.Listing).FirstOrDefault();
```

Deze operatie maakt een query die Top(1) doet.

```SQL
SELECT Top(1) [listing_id]
      ,[id]
      ,[date]
      ,[reviewer_id]
      ,[reviewer_name]
      ,[comments]
  FROM [AirBNB].[dbo].[reviews]
```

i.p.v.

```sql
SELECT [listing_id]
      ,[id]
      ,[date]
      ,[reviewer_id]
      ,[reviewer_name]
      ,[comments]
  FROM [AirBNB].[dbo].[reviews]
```

## Should I drain the data

![Meme](https://i.chzbgr.com/full/2260120320/hD79AB6DE/liquid-cat-takes-shapes-of-many-objects)

Als de vorige query al preformance verbeteringen geeft dan gaat de volgende dat zeker nog meer doen. Als er een UI gevuld moet worden met **data** is het belangrijk dat deze niet te zwaar wordt voor jouw browser. Toch heb je altijd gebruikers te tussen zitten die altijd alles willen zien. Hoe kun je deze UI's optimaliseren?

Dit is eingelijk heel erg simpel, als wij de data kunnen aanpassen zodat alleen de nodige data wordt doorgegeven aan de UI levert dit al een preformance boost op. Codevoorbeeld 1 laat zien dat alle data wordt opgehaald.

```c#
var airBNBContext =  _context.Reviews.Include(r => r.Listing).Take(500);
```

wat resulteerd in de volgende qurie

```sql
SELECT Top(1) [listing_id]
      ,[id]
      ,[date]
      ,[reviewer_id]
      ,[reviewer_name]
      ,[comments]
  FROM [AirBNB].[dbo].[reviews]
```

Deze qurie heeft de volgende DTO nodig

```c#
 public partial class Reviews
    {
        public int ListingId { get; set; }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ReviewerId { get; set; }
        public string ReviewerName { get; set; }
        public string Comments { get; set; }

        public virtual Listings Listing { get; set; }
    }
```

Door de code op de volgende manier aan te passen wordt hij sneller.

```c#
 var airBNBContext =  _context.Reviews.Select(r => new ReviewsTest()
            {
                ListingId = r.ListingId,
                Id = r.Id,
                ReviewerId = r.ReviewerId,
                ReviewerName = r.ReviewerName,
                Comments = r.Comments
            }).Take(500).ToList();
```

wat resulteerd in de volgende querie

```sql
SELECT TOP(500) [r].[listing_id] AS [ListingId], [r].[id] AS [Id], [r].[reviewer_id] AS [ReviewerId], [r].[reviewer_name] AS [ReviewerName], [r].[comments] AS [Comments]
FROM [reviews] AS [r]
```

maak een nieuwe `ReviewsTest` model.

```c#
    public partial class ReviewsTest
    {
        public int ListingId { get; set; }
        public int Id { get; set; }
        public int ReviewerId { get; set; }
        public string ReviewerName { get; set; }
        public string Comments { get; set; }

        public virtual Listings Listing { get; set; }
    }
```

Vervang nu in de view waar deze model nodig is de volgende regel:

```c#
@model IEnumerable<BNB.Model.Reviews>
```

naar

```csharp
@model IEnumerable<BNB.Model.ReviewsTest>
```

Verschil in miliseconden met een volledige DTO `2102` met een aangepaste DTO `1491`.

In dit voorbeeld haal ik de Date weg. Deze vind ik niet meer belangrijk voor mijn UI.

## Async await

Het aanpassen van SQl query levert niet altijd een tijdwinst op vooral als jouw applicatie moet wachten tot een operatie klaar is. Soms kan het niet anders dan moet de applicatie wachten.
Een veel gebruikte methode hiervoor is Async await. In de twee code voorbeelden hieronder staat in de eerste een functie die niet async is en in de tweede een functie die async is.

```csharp
var airBNBContext1 =  _context.Reviews.Include(r => r.Listing).Take(500).ToList();
```

```csharp
var airBNBContext1 = await _context.Reviews.Include(r => r.Listing).Take(500).ToListAsync();
```

Zoals hier boven te zien is, is de functie `ToList()` vervangen voor `ToListAsync`. Deze manier van schrijven kan nog verder geoptimaliseerd worden. Hiervoor moeten wij kijken naar het volgende code voorbeeld.

```csharp
var airBNBContext = _context.Reviews.Include(r => r.Listing).Take(500);
var airBNBContextList = airBNBContext.AsParallel().ToList();
```

Bij het uitvoeren van Plinq ligt er een groot gevaar op de loer als de datasets niet even groot zijn. Het .net framework maakt onderwater nieuwe threads aan die een bepaalde taak aangewezen krijgen. Deze taak is het maken van een list in dit geval. Als de dataset te groot is of ongelijk verdeeld maakt .net meer threads aan en kan het voorkomen dat de applicatie niets meer doet door de hoeveelheid threads. Dit kan leiding tot een langzamere applicatie.

## Minimalizeer de aantal database queries

Veel data ophalen van een database kost weinig rekenkracht en weinig tijd alleen moeten de queries wel goed geschreven worden. Een veel voorkomende fout die beginnende ontwikkelaars maken is dat zij te veel database queries uitvoeren. Hieronder staan twee voorbeelden.

```csharp
int previousNumber = 0;
for (int i = 1; i < 11; i++)
{
    await _context.Reviews.Include(r => r.Listing).Skip(0).Take(i * 500).AsNoTracking().AsQueryable().ToListAsync();
    previousNumber = i * 500;
}
```

```csharp
 var airBNBContext = await _context.Reviews.Include(r => r.Listing).Take(5000).ToListAsync();
```

Bij voorbeeld 1 doet de query er `9072` ms over en bij het tweede voorbeeld `7652` ms.

## As No Tracking

De laatste en een van de eenvoudigste manieren om jouw get querie te versnellen is `AsNoTracking()` functie. Deze functie zorgt er voor dat LINQ weet dat jouw query alleen een get query is. Deze functie zorgt er voor dat LINQ niet bijhoudt welke wijzingen er zijn. In het code voorbeeld hieronder staat een toegepast voorbeeld.

```csharp
var airBNBContext = _context.Reviews.Include(r => r.Listing).Take(500).AsNoTracking();
```

Bron: https://www.danylkoweb.com/Blog/5-entity-framework-performance-tips-KX

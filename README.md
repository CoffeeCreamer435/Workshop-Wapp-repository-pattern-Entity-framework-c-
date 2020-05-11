## Entity framework recap

Met het entity framework kan dus op een hoger abstract niveau data opgehaald worden met verschillende toepassingen zoals:
 - Cross platform support voor Windows, Linux en Mac
 - Model gebaseerde data 
 - Het schrijven van LINQ queries voor onderliggende database maar ook rouwe SQL queries
 - Monitoren van veranderingen in property values
 - INSERT, UPDATE en DELETE commando’s 
 - Optimistic Concurrency (OCC) is een methode die vaak gebruikt word bij transactie en beschermt overschrijvingen op hetzelfde moment.
 - First level cache voor data return querys
 - Een set aan conventies 
 - Configuratie mogelijkheden voor attributen
 - Migratie commando’s voor derde

## Voor en nadelen van het entity framework

Zoals ik al eerder zei is het entity framework er ter behoefte van het abstract programmeren. Abstract programmeren is een begrip dat breedt word geinterpereerd en ervaren. Extra dynamiek kan vaak op traditionele wijze behaald worden en een entity framework kun je daarom als extra beschouwen. Anderzijds zorgt extra dynamiek er wel voor dat je de applicatie beter kan uitbouwen of testen. Los van deze maatschappelijke discussie kunnen we wel een aantal pros en cons uitsluiten;

#### Pros
 - Sneller ontwikkelen
   - Omdat het EF op een hoger abstract niveau zit zijn er minder low-details nodig  
   - Data toepassingen in de vorm van domain-objects zonder database engine kunnen ontwikkeld worden in minder tijd
   - CRUD-procedures
   - Meerdere data toegangsmogelijkheden
 - Generalisatie
   - LINQ queries voor alle object types, zowel in als buiten een database
 - Kostenbesparend
   - Dit punt gaat gepaard met sneller ontwikkelen

#### Cons
 - Snelheid
   - Veel gebruikers klagen over performance issues bij het gebruik van grote domein modellen
 - Abstractie
    - Een ASP Core app heeft al een abstractie laag voor data toepassingen. 
    - De learning-curve neemt toe
 - Formaat
   - EF laad extra data in de class waardoor het formaat toeneemt 
   - Fout gevoelige data migratie
   - De data migratie functionaliteit van het EF is onbetrouwbaar en breekt regelmatig.


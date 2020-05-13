# Repository pattern

Met het repository pattern kun je een bepaald aantal records uit een data store halen en behandelen als een in memory-dataset, zoals een database, API, CSV of in-memory database.<br />
Het repository pattern zorgt voor een nette scheiding en eenrichtingsafhankelijkheid tussen het domein en de data mapping lagen. 

Een repository heeft minsten 2 instrumenten, een repository interface en een repository class die het interface implementeert. In de interface wordt vermeld welke methode uitgevoerd worden zoals add, update of delete maar zonder uitwerking, die staan in de class beschreven.<br />
Elke implementatie van de verschillende data stores wordt geschreven in een andere class, die de de interface implementeert. Hierdoor kun je de implementatie injecten bij het starten van het programma.

Doordat de implementatie van verschillende data stores op dezelfde object-geörienteerde manier werkt, kun je erg makkelijk wisselen tussen de verschillende implementaties, zonder dat je de code die het gebruikt hoeft aan te passen. Daarnaast kan je erg makkelijk wisselen van een data store, bijvoorbeeld van een database naar REST API of in-memory mock. Dit vergroot ook erg de testbaarheid van de applicatie.

Anderzijds zijn programmeurs over het algemeen minder gesteld op het Repository pattern en wordt het vaak gezien als anti-pattern, omdat:

1. Het (nog) een laag van abstractie toevoegt aan de applicatie, waardoor de applicatie weer wat complexer wordt;
1. Het ervoor zorgt dat niet alle ORM-opties van het Entity framework werken. 



![Repository pattern Entity Framework](https://raw.githubusercontent.com/CoffeeCreamer435/Workshop-Wapp-repository-pattern-Entity-framework-c-/repository-pattern/Repository%20pattern%20Entity%20Framework%20structure%20with-without.png)

![Repository pattern C#](https://raw.githubusercontent.com/CoffeeCreamer435/Workshop-Wapp-repository-pattern-Entity-framework-c-/repository-pattern/Repository%20pattern%20Csharp%20structure.png)

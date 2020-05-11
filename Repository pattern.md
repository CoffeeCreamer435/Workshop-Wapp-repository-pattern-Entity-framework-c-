# Repository pattern

Met het repository pattern kun je een bepaald aantal records uit een data store halen en behandelen als een in memory-dataset, zoals een database, API, CSV of in-memory database.<br />
Het repository pattern zorgt voor een nette scheiding en eenrichtingsafhankelijkheid tussen het domein en de data mapping lagen. 

Een repository heeft minsten 2 instrumenten, een repository interface en een repository class die het interface implementeert. In de interface wordt vermeld welke methode uitgevoerd worden zoals add, update of delete maar zonder uitwerking, die staan in de class beschreven.<br />
Elke implementatie van de verschillende data stores wordt geschreven in een andere class, die de de interface implementeert. Hierdoor kun je de implementatie injecten bij het starten van het programma.

Doordat de implementatie van verschillende data stores op dezelfde object-geörienteerde manier werkt, kun je erg makkelijk wisselen tussen de verschillende implementaties, zonder dat je de code die het gebruikt hoeft aan te passen. Daarnaast kan je erg makkelijk wisselen van een data store, bijvoorbeeld van een database naar REST API. Dit vergroot ook erg de testbaarheid van de applicatie.

_Anderzijds zijn programmeurs over het algemeen minder gesteld op het RP. Het enitity framework is namelijk al een extra laag aan abstractie en door het RP zorg je er voor dat niet alle object-relational mapping (ORM) opties werken._<br />
Anti-pattern

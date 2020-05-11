## Hoe word het repository pattern (RP) toegepast?

Met het RP kun je data ophalen van verschillende data toepassingen zoals mysql databases en opgeslagen bestanden. Het idee achter RP is dat je met repositorys in lagen werkt. Een repository heeft minsten 2 instrumenten, een repository interface en een repository class die het interface implementeert. In het interface word vermeld welke welke methode uitgevoerd worden zoals add, update of delete maar zonder uitwerking, die staan in de class beschreven. 

Het RP is er ter behoefte van het abstract programmeren. Wil je bijvoorbeeld gaan unit testen dan kun je makkelijk naar een mock switchen i.p.v. een SQL database. Ook gebruik je models die met aggregate root objecten werken. Aggregate root objecten worden geladen vanuit de startup in de repository en zijn beschikbaar voor child objecten.

Anderzijds zijn programmeurs over het algemeen minder gesteld op het RP. Het enitity framework is namelijk al een extra laag aan abstractie en door het RP zorg je er voor dat niet alle abject-relational mapping (ORM) opties werken.

## Sensitive data exposure

Op #3 van de OWASP lijst staat sensitive data exposure en alleen daarom al moet dit onderwerp zeker niet light opgevat worden.

SDE heeft met name betrekking tot het veilig opslaan van data. Vanaf het moment dat we opleiding ICT zijn begonnen zijn we hier ook mee in aanraking gekomen. Zo weten we allemaal dat je gevoelige data zoals wachtwoorden moet encrypten maar daar blijft het niet bij. Als je data veilig wilt opslaan dan moet je deze stappen overwegen;

**Classificeer data**
In deze stap classificeer je data zoals gevoelig of normaal

**Controles toepassen**
Op basis van de geclassificeerde data kan je rechten ontlenen waardoor alleen bevoegde toegang hebben tot specifieke data

**Data encrypten**
Gebruik up-to-date encryptie methode om gevoelige data af te schermen

**Sterke ciphers**
Een cipher is een methode die gebruikt wordt om een versleutelde verbinding op te zetten volgens het TLS protocol. 

**Perfect forward secretsy (PFS) en/of HTTP Strict Transport Security (HSTS)**
PFS is een protocol dat voorkomt dat met het verkrijgen van de private key, informatie die in eerste instantie versleuteld is verzonden alsnog ontcijferd kan worden. Dit omdat er gebruik wordt gemaakt van unieke session keys.

HSTS is een serverinstelling die het gebruik van een veilige HTTPS verbinding afdwingt.


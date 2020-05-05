# Hoe kun jij je zelf beschermen tegen een Sensitive Data Exposure

... Wat uitleg van wat het is en waarom zou je dit doen

## Encrypted verbinding

Tegenwoordig zien wij steeds meer websites die gebruik maken van een `https` verbinding i.p.v. een `http` verbinding. `Https` is een uitbreiding van het bestaande `http` protocol. Het `https` protocol zorgt voor end to end encryptie. Dit voorkomt dat een man in the middle attack kan plaats vinden. Een man in the middle attack is een persoon die zich voordoet als een server of modem. Via deze weg probeert hij internet gegevens te stelen zoals een gebruikersnaam en wachtwoord.

De verbinding naar de database kan ook encrypt worden. Dit kan gedaan worden met een TLS verbinding. TLS staat voor Transport Layer Security volgens Microsoft.
Om een cetificaat te krijgen moet de ontwikkelaar aan de volgende eisen voldoen:

1. The certificate must be in either the local computer certificate store or the current user certificate store.

2. The SQL Server Service Account must have the necessary permission to access the TLS certificate.

3. The current system time must be after the Valid from property of the certificate and before the Valid to property of the certificate.

4. The certificate must be meant for server authentication. This requires the Enhanced Key Usage property of the certificate to specify Server Authentication (1.3.6.1.5.5.7.3.1).

5. The certificate must be created by using the KeySpec option of AT_KEYEXCHANGE. Usually, the certificate's key usage property (KEY_USAGE) will also include key encipherment (CERT_KEY_ENCIPHERMENT_KEY_USAGE).

6. The Subject property of the certificate must indicate that the common name (CN) is the same as the host name or fully qualified domain name (FQDN) of the server computer. When using the host name, the DNS suffix must be specified in the certificate. If SQL Server is running on a failover cluster, the common name must match the host name or FQDN of the virtual server and the certificates must be provisioned on all nodes in the failover cluster.

7. SQL Server 2008 R2 and the SQL Server 2008 R2 Native Client (SNAC) support wildcard certificates. SNAC has since been deprecated and replaced with the Microsoft OLE DB Driver for SQL Server and Microsoft ODBC Driver for SQL Server. Other clients might not support wildcard certificates. For more information, see the client documentation and KB 258858.
   Wildcard certificate cannot be selected by using the SQL Server Configuration Manager. To use a wildcard certificate, you must edit the HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQLServer\SuperSocketNetLib registry key, and enter the thumbprint of the certificate, without spaces, to the Certificate value.

Door aan deze eisen te voldoen kun je een cetificaat krijgen en kun je een encrypted verbinding opzetten met jouw database.

## Encryptie

Natuurlijk is het niet voldoende om alleen maar om jouw verbindingen te encrypten. Data die is opgeslagen is natuurlijk een geweldig doelwit voor een crimineel. Data kan namelijk veel geld opleveren in het criminele circuit. Om te voorkomen dat gevoelige data uitlekt zoals een wachtwoord of betaalgegevens moeten deze encrypted worden. Deze encryptie moet gedaan worden met een sterk algoritme.
Volgens Owasp zijn deze algoritmes aangeraden:

- Argon2
- Scrypt
- Bcrypt
- PBKDF2

Toch zijn er nog steeds mensen die encrypten met slechte encryptie methodes zoals:

- md5
- AES-ECB lager dan 128 bits
- SHA lager dan 512 bits

Een ander veel gebruikte fout is het opslaan van de encryption key. Volgens Crypteron slaan veel mensen hun key op de volgende plaatsen op:

- In de database
- Op de HDD
- In de applicatie configuratie
- Dezelfde key gebruiken voor alle data.

Om te voorkomen dat criminelen jouw key te pakken krijgen zijn er verschillende systemen mogelijk. De volgende methodes worden veel gebruikt:

- Key Encryption Key (KEK)
- Opslaan op een andere server en met een secure connection ophalen
- Master encryption key met een Master signing key

Bronnen

1. https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine?view=sql-server-ver15
2. https://owasp.org/www-project-top-ten/OWASP_Top_Ten_2017/Top_10-2017_A3-Sensitive_Data_Exposure.html
3. https://www.crypteron.com/blog/the-real-problem-with-encryption/

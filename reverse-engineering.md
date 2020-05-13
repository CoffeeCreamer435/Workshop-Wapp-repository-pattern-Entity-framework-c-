Installeer dotnet ef tools:
`dotnet tool install --global dotnet-ef --version 5.0.0-preview.3.20181.2`

Voeg benodige packages toe:
`dotnet add package Microsoft.EntityFrameworkCore.SqlServer -v 5.0.0-preview.3.20181.2`

`dotnet add package Microsoft.EntityFrameworkCore.Design -v 5.0.0-preview.3.20181.2`

Reverse engineer database:
`dotnet ef dbcontext scaffold "Server=<server>;Database=<database>;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer`

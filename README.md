# TooliRent API

## Beskrivning
TooliRent är ett API för uthyrning av verktyg. Systemet låter medlemmar registrera sig, boka verktyg, följa sina bokningar och se status. Admins kan hantera verktyg, kategorier, användare och se statistik. JWT används för autentisering och rollbaserad åtkomst med rollerna Member och Admin.

## Kom igång
1. Klona repot: git clone <repo-url> och cd TooliRent
2. Lägg till anslutningssträng och JWT-inställningar i appsettings.json: "ConnectionStrings": { "DefaultConnection": "Server=<server>;Database=TooliRentDb;Trusted_Connection=True;" }, "JwtSettings": { "Key": "<hemlig-nyckel>", "Issuer": "TooliRent", "Audience": "TooliRentUsers", "ExpiryMinutes": 60 }
3. Skapa databasen med dotnet ef database update --startup-project ../TooliRent.Api
(OBS! Se till att du är i TooliRent.Infrastructure directoryn när kommandot utförs)
4. Starta API:et med cd ../TooliRent.Api och dotnet run. Swagger UI finns på https://localhost:<port>/swagger

## API Endpoints
Autentisering: POST /api/auth/register – Skapa konto, POST /api/auth/login – Logga in och få JWT-token, POST /api/auth/refresh – Förnya token

Verktyg: GET /api/tools – Lista alla verktyg, GET /api/tools/{id} – Verktygsdetaljer, POST /api/tools – Admin: skapa verktyg, PUT /api/tools/{id} – Admin: uppdatera verktyg, DELETE /api/tools/{id} – Admin: ta bort verktyg

Bokningar: GET /api/bookings – Admin: lista alla bokningar, GET /api/bookings/my – Member: lista egna bokningar, POST /api/bookings – Skapa bokning, PUT /api/bookings/{id} – Uppdatera bokning, PUT /api/bookings/{id}/cancel – Avboka bokning, PUT /api/bookings/{id}/collect – Markera som hämtad, PUT /api/bookings/{id}/return – Markera som återlämnad, GET /api/bookings/overdue – Lista försenade bokningar

Statistik (Admin): GET /api/statistics – Verktygs-, boknings-, användar- och intäktsstatistik

## Datamodeller
User: Id, Username, Email, PasswordHash, Role, IsActive, CreatedAt, LastLoginAt
Tool: Id, Name, Description, PricePerDay, IsAvailable, CategoryId, Bookings
Booking: Id, UserId, ToolId, StartDate, EndDate, CollectedAt, ReturnedAt, IsCollected, IsReturned, IsCancelled, TotalPrice
Category: Id, Name, Description, Tools

## Noteringar
Datum och tid sparas som UTC. Bokningspris räknas automatiskt baserat på pris per dag och antal dagar. Förseningsavgifter hanteras i bokningslogiken. Bokningar är aktiva om de inte är avbokade eller återlämnade.

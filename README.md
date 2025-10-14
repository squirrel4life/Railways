# Railways

## 🇵🇱 Opis projektu
**Railways** to aplikacja webowa stworzona w technologii **ASP.NET MVC**, służąca do **wyszukiwania biletów kolejowych**.  
Pozwala użytkownikom wyszukiwać połączenia i rezerwować miejsca.  
Projekt demonstruje praktyczne zastosowanie wzorca **Model–View–Controller** i integrację z bazą danych SQL.

---

## 🇵🇱 Funkcjonalności
- Wyszukiwanie połączeń kolejowych na podstawie stacji początkowej i końcowej  
- Rezerwacja biletów online  
- Panel administracyjny do zarządzania połączeniami i cenami  
- Autoryzacja i rejestracja użytkowników (ASP.NET Identity)  

---

## 🇵🇱 Technologie
- **ASP.NET MVC (C#)**  
- **Entity Framework Core**  
- **MS SQL Server**  
- **Bootstrap / Razor Views**  
- **Dependency Injection / Repository Pattern**  

---

## 🇵🇱 Uruchomienie
1. Sklonuj repozytorium:  
   ```bash
   git clone https://github.com/squirrel4life/Railways.git
   cd Railways
   ```
2. Utwórz bazę danych.
3. Zaktualizuj `Web.config`:
   - connection string
     ```xml
     <add name="RailwayTicketOfficeDBEntities" connectionString="metadata=res://*/Model1.csdl|res://*/Model1.ssdl|res://*/Model1.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=SERVER_NAME;initial catalog=DB_NAME;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
     ```
   - zewnętrzna autoryzacja
     ```xml
      <add key="EnableFacebookAuth" value="true" />
      <add key="EnableGoogleAuth" value="true" />
      <add key="FacebookAppId" value="FB_APP_ID" />
      <add key="FacebookAppSecret" value="FB_APP_SECRET" />
      <add key="GoogleClientId" value="GOOGLE_CLIENT_ID" />
      <add key="GoogleClientSecret" value="GOOGLE_CLIENT_SECRET" />
     ```
4. Wykonaj migracje i uruchom projekt:  
   ```bash
   dotnet ef database update
   dotnet run
   ```
5. Otwórz w przeglądarce: [https://localhost:5001](https://localhost:5001)

---

## 🇵🇱 Przykład działania
Użytkownik wyszukuje trasę *Warszawa → Kraków*, wybiera pociąg i miejsce.

---

# Railways

## 🇬🇧 Project description
**Railways** is a web application built with **ASP.NET MVC** for **train ticket bookings**.  
It allows users to search for routes and book seats.  
The project demonstrates practical use of the **Model–View–Controller** pattern and SQL database integration.

---

## 🇬🇧 Features
- Search train connections by departure and destination stations  
- Book tickets online  
- Admin panel for managing routes and fares  
- User registration and login (ASP.NET Identity)  

---

## 🇬🇧 Tech stack
- **ASP.NET MVC (C#)**  
- **Entity Framework Core**  
- **MS SQL Server**  
- **Bootstrap / Razor Views**  
- **Dependency Injection / Repository Pattern**  

---

## 🇬🇧 How to run
1. Clone the repository:  
   ```bash
   git clone https://github.com/squirrel4life/Railways.git
   cd Railways
   ```
2. Create a database.
3. Update `Web.config`:
   - connection string
     ```xml
     <add name="RailwayTicketOfficeDBEntities" connectionString="metadata=res://*/Model1.csdl|res://*/Model1.ssdl|res://*/Model1.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=SERVER_NAME;initial catalog=DB_NAME;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
     ```
   - OAuth
     ```xml
      <add key="EnableFacebookAuth" value="true" />
      <add key="EnableGoogleAuth" value="true" />
      <add key="FacebookAppId" value="FB_APP_ID" />
      <add key="FacebookAppSecret" value="FB_APP_SECRET" />
      <add key="GoogleClientId" value="GOOGLE_CLIENT_ID" />
      <add key="GoogleClientSecret" value="GOOGLE_CLIENT_SECRET" />
     ```
4. Apply migrations and run:  
   ```bash
   dotnet ef database update
   dotnet run
   ```
5. Open in your browser: [https://localhost:5001](https://localhost:5001)

---

## 🇬🇧 Example
A user searches for *Warsaw → Krakow*, selects a train, seat, and books a ticket.

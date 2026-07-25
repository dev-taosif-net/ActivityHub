# ActivityHub

An ASP.NET Core (.NET 10) Web API for organising activities, built on a Clean Architecture layout with EF Core and SQLite.

## Project structure

```
Domain/          Entities. No dependencies.
Application/     Business logic. Depends on Domain.
Infrastructure/  EF Core DbContext, configurations, migrations, seeding.
API/             Minimal API host and composition root.
```

Dependencies point inward: `API → Infrastructure → Application → Domain`. The API references Infrastructure only to wire up DI at startup — the SQLite provider is never referenced from the API's own code.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- EF Core CLI tools, for creating migrations:
  ```
  dotnet tool install --global dotnet-ef
  ```

## Running

```
dotnet run --project API
```

On startup the app applies any pending migrations (creating `API/activityhub.db` if it does not exist) and seeds 5 sample activities when the table is empty. Both steps are no-ops once the database is current and populated.

The API listens on `https://localhost:7183`, with the Scalar API reference at:

```
https://localhost:7183/scalar/v1
```

## Database

SQLite, configured via `ConnectionStrings:DefaultConnection` in [API/appsettings.json](API/appsettings.json). The `.db` file is gitignored — it is always reproducible from the migrations.

Add a migration:

```
dotnet ef migrations add <Name> -p Infrastructure -s API -o Persistence/Migrations
```

It is applied automatically on the next run. To apply it manually instead:

```
dotnet ef database update -p Infrastructure -s API
```

To reset: delete `API/activityhub.db` and run the project again — the schema is rebuilt and the sample data re-seeded.

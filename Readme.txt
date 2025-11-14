this is for appsettings.json:

{
  "ConnectionStrings": {
    "DefaultConnection": "server=[Your Server];user=[your DB username];password=[your DB password];Database=[DB name];TrustServerCertificate=True;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
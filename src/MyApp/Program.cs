using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


// Pobranie konfiguracji Redisa ze zmiennych środowiskowych
// (jeśli nie jest ustawione to uzywa domyślnych wartości)

var redisHost = Environment.GetEnvironmentVariable("REDIS_HOST") ?? "localhost";
var redisPort = Environment.GetEnvironmentVariable("REDIS_PORT") ?? "6379";

// Utworzenie Connection String dla Redisa
var redisConnectionString = $"{redisHost}:{redisPort}";

Console.WriteLine($"Connecting to {redisConnectionString}...");


// Nawiązanie połączenia z bazą danych Redis
var redis = ConnectionMultiplexer.Connect(redisConnectionString);
var db = redis.GetDatabase();

Console.WriteLine("Connected.");

// Zapisanie klucza
db.StringSet("message", "Hello, Redis!");

app.MapGet("/", () => "Hello World!");

// Odczytanie klucza
app.MapGet("/message", () => (string?) db.StringGet("message"));

app.Run();

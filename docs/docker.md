## **Cele ćwiczenia**

Po tych ćwiczeniach uczestnicy będą wiedzieć:

- Czym jest Docker i jakie problemy rozwiązuje
- Jakie są różnice między konteneryzacją a wirtualizacją
- Jakie są główne zalety korzystania z Dockera
## **1. Wprowadzenie**

**Pytanie otwierające:** _Jakie macie doświadczenia z wdrażaniem aplikacji na różnych środowiskach?_

**Problem:**

- Aplikacje działają inaczej na różnych komputerach („u mnie działa!”)
- Różnice w konfiguracji systemu, bibliotek, wersjach zależności
- Długi czas wdrażania nowych aplikacji

**Rozwiązanie:**  
Docker pozwala zapakować aplikację wraz z jej zależnościami do kontenera, który działa tak samo na każdym środowisku.
## **2. Czym jest Docker?**

Docker to platforma do zarządzania aplikacjami w kontenerach. Kontenery umożliwiają uruchamianie aplikacji wraz ze wszystkimi zależnościami w odizolowanym środowisku, niezależnie od systemu operacyjnego i konfiguracji sprzętowej.

## **3. Konteneryzacja vs. Wirtualizacja**

### **Główne różnice:**

| Cecha             | Konteneryzacja (Docker) | Wirtualizacja (VM) |
| ----------------- | ----------------------- | ------------------ |
| Zużycie zasobów   | Mniejsze                | Większe            |
| Czas uruchomienia | Sekundy                 |  Minuty            |
| Izolacja          | Procesowa               | Pełna VM           |
| Wymagana OS       | Wspólny kernel          | Osobny OS          |

➡ **Wniosek:** Docker jest lżejszy i szybszy, ale nie zastępuje VM w każdej sytuacji.

### **Zalety Dockera**

- **Przenośność** – ten sam kontener działa na różnych maszynach
- **Szybkie uruchamianie** – kontenery startują w sekundach
- **Łatwa skalowalność** – możliwość uruchamiania wielu instancji
- **Efektywność zasobów** – dzielenie jądra systemu zamiast pełnej wirtualizacji

## **4. Podstawowe pojęcia**

- **Obraz (Image)** – gotowy pakiet zawierający system plików i aplikację
- **Kontener (Container)** – uruchomiona instancja obrazu
- **Dockerfile** – plik opisujący sposób budowy obrazu
- **Rejestr (Registry)** – miejsce przechowywania obrazów (np. Docker Hub)
- **Docker Compose** – narzędzie do zarządzania wieloma kontenerami za pomocą pliku `docker-compose.yml`.

## **5. Podstawowe komendy Dockera**

- `docker pull <image>` – pobranie obrazu z rejestru.
- `docker run <image>` – uruchomienie kontenera z danego obrazu.
- `docker ps` – wyświetlenie listy uruchomionych kontenerów.
- `docker stop <container_id>` – zatrzymanie kontenera.
- `docker start <container_id>` – ponowne uruchomienie zatrzymanego kontenera.
- `docker rm <container_id>` – usunięcie kontenera.
- `docker images` – wyświetlenie listy pobranych obrazów.
- `docker rmi <image_id>` – usunięcie obrazu Dockera.
- `docker build -t <image_name> .` – budowanie obrazu z Dockerfile.
- `docker run -d -p 8080:80 <image>` – uruchomienie kontenera w tle i mapowanie portów.
- `docker exec -it <container_id> bash` – uruchomienie powłoki `bash` wewnątrz działającego kontenera.
- `docker compose up -d` – uruchomienie usług z pliku `docker-compose.yml` w trybie demona.
- `docker compose down` – zatrzymanie i usunięcie wszystkich kontenerów oraz sieci utworzonych przez `docker-compose`.


## **6. Pierwsze kroki z Dockerem**

### 👨‍💻 **Ćwiczenie 1: Sprawdzenie instalacji Dockera**  
🔹 Otwórz terminal i wpisz:
```sh
docker --version
```

✅ Jeśli wyświetli się wersja Dockera, wszystko działa.

### 👨‍💻 **Ćwiczenie 2: Uruchomienie pierwszego kontenera**  
🔹 Uruchom polecenie:
```sh
docker run hello-world
```

✅ Co się stało?
- Docker pobrał obraz `hello-world` z Docker Hub
- Utworzył kontener
- Kontener wypisał komunikat i zakończył działanie

### 👨‍💻 **Ćwiczenie 3: Sprawdzenie listy kontenerów**  
🔹 Wyświetl uruchomione kontenery:
```sh
docker ps
```

🔹 Wyświetl wszystkie kontenery (także zakończone):
```sh
docker ps -a
```

### 👨‍💻 **Ćwiczenie 4: Uruchomienie redis w konterze**  
🔹 Utwórz kontener z obrazem `redis`
```sh
docker run --name my-redis  -d -p 6379:6379 redis
```

🔹 Przejdź do basha
```sh
docker exec -it my-redis bash
```


🔹 Uruchom `redis-cli`

```sh
docker exec -it my-redis redis-cli
```

## 7. Tworzenie własnego obrazu

1. Tworzymy aplikację .NET 9 
```sh
dotnet new web -n MyApp
cd MyApp
```

2. Tworzymy `Dockerfile` w katalogu aplikacji:
```dockerfile
# Pobranie obrazu SDK do budowania aplikacji
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Kopiowanie plików projektu i przywmcenie zależności
COPY *.csproj .
RUN dotnet restore

# Kopiowanie reszty plików i budowanie aplikacji
COPY . .
RUN dotnet publish -c Release -o /out

# Pobranie lekkiego obrazu runtime .NET 9
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Kopiowanie aplikacji z poprzedniego etapu
COPY --from=build /out .

# Ustawienie domyślnej komendy uruchamiającej aplikację
ENTRYPOINT ["dotnet", "MyNet.dll"]

# Eksponowanie portu
EXPOSE 5000
```

3. Budowanie obrazu
```sh
docker build -t my-app .
```

4. Uruchomienie kontenera
```sh
docker run -p 5000:5000 my-app
```

## 8. Tworzenie kompozycji Docker Compose

1. **Tworzymy aplikację .NET 9**
```sh
dotnet new web -n MyApp cd MyApp
```


2. **Dodajemy obsługę Redisa w kodzie**  
    W pliku `Program.cs` dodajemy kod umożliwiający połączenie z Redisem:
    
```csharp
using StackExchange.Redis; // Import biblioteki do obsługi Redisa

var builder = WebApplication.CreateBuilder(args); // Tworzenie obiektu aplikacji
var app = builder.Build(); // Budowanie aplikacji

// Pobranie konfiguracji Redisa z zmiennych środowiskowych (jeśli nie ustawione, używa domyślnych wartości)
var redisHost = Environment.GetEnvironmentVariable("REDIS_HOST") ?? "localhost";
var redisPort = Environment.GetEnvironmentVariable("REDIS_PORT") ?? "6379";
var redisConnectionString = $"{redisHost}:{redisPort}"; // Tworzenie connection stringa do Redisa

// Nawiązanie połączenia z serwerem Redis
var redis = ConnectionMultiplexer.Connect(redisConnectionString);
var db = redis.GetDatabase(); // Pobranie dostępu do bazy danych Redisa

// Zapisanie przykładowej wartości do Redisa
db.StringSet("message", "Hello from Redis!");

// Endpoint zwracający wartość zapisaną w Redisie
app.MapGet("/message", () => (string?) db.StringGet("message"));

// Endpoint zwracający domyślny komunikat
app.MapGet("/", () => "Hello, World!");

// Uruchomienie aplikacji
app.Run();

```

    
4. **Tworzymy `docker-compose.yml`**
    
```yaml
services:
  app:
    build: .
    ports:
      - "8080:8080"
    environment:
      - REDIS_HOST=redis
      - REDIS_PORT=6379
    depends_on:
      - redis

  redis:
    image: "redis:7.2"
    ports:
      - "6379:6379"

```

    
5. **Uruchamiamy całą kompozycję**
     
```sh
docker-compose up -d
```
    
6. **Sprawdzamy działanie aplikacji**  
    Otwieramy w przeglądarce:
    
    `http://localhost:8080/message`
    

Teraz aplikacja działa w kontenerze i łączy się z Redisem! 🚀


## **9. Podsumowanie**

✅ Stworzyliśmy aplikację .NET + REDIS
✅ Uruchomiliśmy ją w kontenerze Docker
✅ Stworzyliśmy kompozycję

# 🗳️ Zadanie: Aplikacja do głosowania w czasie rzeczywistym z SignalR

## 🎯 Cel:
Stwórz aplikację webową, która prezentuje wyniki głosowania na żywo w postaci wykresu słupkowego. Klienci (np. aplikacje konsolowe lub webowe) mogą głosować na jednego z kandydatów. Lista kandydatów powinna być pobierana dynamicznie z serwera.

## 🛠️ Wymagania funkcjonalne:
1. Serwer (ASP.NET Core + SignalR):
- Utrzymuje listę opcji do głosowania (np. kandydatów na prezydenta).
- Udostępnia endpoint do pobierania aktualnych opcji do głosowania.
- Obsługuje komunikację SignalR: otrzymuje głosy od klientów, na bieżąco aktualizuje i wysyła wyniki głosowania do wszystkich połączonych klientów.

2. Klient Web (np. JS / Blazor / HTMX / Razor Pages):
- Automatycznie pobiera listę kandydatów przy starcie
- Wyświetla słupkowy wykres z aktualnymi wynikami.
- Automatycznie aktualizuje wykres w czasie rzeczywistym po każdej zmianie danych.

3. Klient Konsolowy (C#):
- Pobiera listę kandydatów z API.
- Pozwala użytkownikowi oddać głos przez wpisanie identyfikatora opcji.
- Wysyła głos do serwera przez SignalR lub zwykły HTTP.


## 📊 Przykład:
Lista kandydatów:

- 1. Alicja
- 2. Bartek
- 3. Celina

Głosowanie z konsoli:
```bash
Oddaj swój głos (wpisz numer kandydata): 2
Twój głos został oddany na Bartka.
```

W tym czasie na stronie web:

- Słupki dynamicznie przesuwają się na podstawie liczby głosów.


## 🔁 Wskazówki:

**Wspólne:**
- Komunikację oddawania głosów można obsłużyć przez SignalR lub HTTP (fallback).
- Wykorzystaj DI i singleton do przechowywania wyników głosowania.


**Web:**
- Użyj bibliotek JS lub komponentów Blazora do wizualizacji słupków (np. Chart.js, JSInterop, MudBlazor, HTMX + SVG).
- Możesz użyć IHubContext do broadcastowania aktualizacji.

## 📌 Rozszerzenia (dla chętnych):
- Wprowadzenie timeoutu na głosowanie (np. 30 sekund).
- Obsługa resetowania wyników przez admina.
- Obsługa grup SignalR (np. oddzielne głosowania dla różnych tematów).
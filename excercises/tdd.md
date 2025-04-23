# TDD - Kalkulator zużycie energii elektrycznej maszyn przemysłowych

## Wprowadzenie

W fabryce produkcyjnej różne typy maszyn przemysłowych (frezarki, tokarki, prasy) mają określone zużycie energii elektrycznej (kWh), które zależy od typu maszyny oraz ustawienia trybu oszczędzania energii. System sterowania musi obliczać zużycie energii elektrycznej na podstawie typu maszyny, czasu pracy oraz wybranego trybu oszczędzania energii.

## Cel
Celem jest stworzenie kalkulatora zużycia energii elektrycznej, który będzie elastyczny i umożliwi rozszerzanie o nowe maszyny oraz reguły obliczeniowe.

## Zadanie

Utwórz klasę `MachinePowerCalculator` z metodą `GetPowerConsumption (string machineType, int duration, bool isEnergySaving)`, która zwraca zużycie energii elektrycznej w kWh (_double_) na podstawie typu maszyny i czasu pracy.

Wymagania realizuj zgodnie z techniką **TDD** (_Test-driven-development_):

- **Red** - kod nieprzechodzący test
- **Green** - kod przechodzący test
- **Refactor** - refaktoryzacja kodu i testów

## Wymagania funkcjonalne

- 1. Jeśli typ maszyny jest pusty lub null, metoda rzuca wyjątek `ArgumentException` z komunikatem `Machine type cannot be empty.`

- 2. Czas pracy maszyny musi być większy niż 0 godzin, w przeciwnym razie rzucany jest wyjątek `Duration must be greater than zero.`

- 3. Typ maszyny **MillingMachine** (frezarka): bazowy pobór mocy = `5.0 kWh`. Charakterystyka pracy jest liniowa.

- 4. Typ maszyny **Press** (prasa): bazowy pobór mocy = `7.2 kWh`. Charakterystyka pracy jest liniowa.

- 5. Typ maszyny **Lathe** (tokarka): bazowy pobór mocy = `3.5 kWh`. Charakterystyka pracy jest nieliniowa i opisana wzorem: $$ Power = 3.5 \times \log(duration + 1) $$. Na początku, gdy maszyna pracuje przez krótki czas pobór mocy rośnie dość szybko a później wzrost poboru mocy wyhamowuje:

- 1 godzina pracy: $$ \text{Power} \approx 1.05 \, \text{kWh} $$
- 2 godziny pracy: $$ \text{Power} \approx 1.67 \, \text{kWh} $$
- 10 godzin pracy: $$ \text{Power} \approx 3.64 \, \text{kWh} $$
- 100 godzin pracy: $$ \text{Power} \approx 7.02 \, \text{kWh} $$
- 6. Nieznany typ maszyny rzuca wyjątek ArgumentException z komunikatem `Invalid machine type`.

- **7. Dodatkowe wymaganie:** Modyfikator oszczędzania energii: Jeśli jest włączony `(isEnergySaving = true)`, pobór mocy jest redukowany o `20%` (np. dla frezarki: `5.0 kWh * 0.8 = 4.0 kWh`).


Pamiętaj o robieniu na bieżąco commitów do Gita.

## Punktacja

| Nazwa zadania       | Punkty |
| ------------------- | ------ |
| Nazewnictwo         | 1 pkt  |
| Pokrycie testami    | 2 pkt  |
| Refaktoring         | 1 pkt  |
| Dodatkowe wymaganie | 1 pkt  |
| Wzorce Projektowe   | 2 pkt  |
    
## Czas: 45 min.
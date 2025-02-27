# Podstawowe operacje Git


## 1. Konfiguracja
- Ustawienie nazwy użytkownika:
```
git config --global user.name "Twoje Imię"
```

- Ustawienie adresu e-mail:
```
git config --global user.email "twoj@email.com"
```


## 2. Inicjalizacja repozytorium
- Utwórz repozytorium
```
git init
```

- Utwórz pustą rewizję
```
git commit --allow-empty -m "Początek prac"
```

## 3. Status repozytorium

- Sprawdzenie statusu plików
```
git status
```


## 4. Dodanie plików do obszaru staging

- Dodanie pojedynczego pliku
```
git add nazwa_pliku
```

- Dodanie wszystkich plików
```
git add .
```

5. Utworzenie rewizji (commit)
- Zapisanie zmian z opisem
```
git commit -m "Opis zmian"
```

- Zmień ostatni commit, zachowując jego wiadomość
```
git commit --amend --no-edit
```

6. Historia zmian (commitów)
- Wyświetlenie historii
```
git log
```

- Wyświetlenie historii w formie skróconej
```
git log --oneline
```

7. Przeglądanie zmian
- Różnice pomiędzy plikami
```
git diff
```

8. Gałęzie (Branches)

- Utworzenie nowej gałęzi
```
git branch nazwa_gałęzi
```
lub
```
git switch -c nazwa_gałęzi
```

- Przełączenie na inną gałąź
```
- git checkout nazwa_gałęzi
```

lub 
```
- git switch nazwa_gałęzi
```

- Utworzenie nowej gałęzi i przełączenie na nią
```
git checkout -b nazwa_gałęzi
```

9. Scalanie gałęzi (Merge)

- Scalanie gałęzi

```
git merge nazwa_gałęzi
```

10. Cofanie zmian
- Cofnięcie zmian w plikach przed commitem
```
git restore nazwa_pliku
```

- Cofnięcie ostatniego commita
```
git reset --soft HEAD~1
```

11. Zdalne repozytorium
- Dodanie zdalnego repozytorium
```
git remote add origin url_repozytorium
```

- Wysłanie zmian na zdalne repozytorium
```
git push origin main
```

- Pobranie zmian z zdalnego repozytorium
```
git pull origin main
```

12. Tagi
- Utworzenie nowego tagu
```
git tag v1.0
```

- Wysłanie tagu do zdalnego repozytorium
```
git push origin v1.0
```


13. Klonowanie repozytorium
- Pobranie repozytorium
```
git clone url_repozytorium
```
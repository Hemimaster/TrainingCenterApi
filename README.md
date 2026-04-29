# 🎓 TrainingCenterApi

## 📘 Opis projektu

REST API do zarządzania salami szkoleniowymi i rezerwacjami.

Aplikacja umożliwia:
- ➕ dodawanie sal i rezerwacji
- 📥 pobieranie danych
- ✏️ aktualizację
- ❌ usuwanie
- 🔍 filtrowanie danych
- ✅ walidację danych wejściowych
- ⚙️ obsługę reguł biznesowych

---

## 🛠️ Technologie

- .NET 10
- ASP.NET Core Web API
- 🧠 Dane w pamięci (in-memory)
- 🧪 Postman (testowanie API)

---

## 🚀 Uruchomienie

1. Sklonuj repozytorium
2. Otwórz projekt w Rider / Visual Studio
3. Uruchom aplikację
4. API będzie dostępne pod adresem:


http://localhost
:<port>


📌 (port jest wyświetlany w konsoli po uruchomieniu aplikacji)

---

## 🔗 Endpointy

### 🏢 Rooms


GET /api/rooms
GET /api/rooms/{id}
GET /api/rooms/building/{buildingCode}
POST /api/rooms
PUT /api/rooms/{id}
DELETE /api/rooms/{id}


### 📅 Reservations


GET /api/reservations
GET /api/reservations/{id}
POST /api/reservations
PUT /api/reservations/{id}
DELETE /api/reservations/{id}


---

## ⚠️ Walidacja i reguły biznesowe

- ❗ Name, BuildingCode, OrganizerName i Topic są wymagane
- 📏 Capacity musi być większe od zera
- ⏰ EndTime musi być późniejsze niż StartTime
- 🚫 Nie można dodać rezerwacji dla sali, która nie istnieje
- 🔒 Nie można dodać rezerwacji dla sali nieaktywnej
- ⛔ Rezerwacje tej samej sali nie mogą się nakładać czasowo
- ❌ Nie można usunąć sali z istniejącymi rezerwacjami (409 Conflict)

---

## 🧪 Testowanie

API zostało przetestowane przy użyciu Postman.

Sprawdzone scenariusze:
- ✔ GET (poprawne i błędne)
- ✔ POST (poprawne i błędne)
- ✔ walidacja 400 Bad Request
- ✔ konflikty 409 Conflict
- ✔ usuwanie zasobów (204 No Content)
- ✔ 404 dla nieistniejących danych

---

## 📥 Przykładowe requesty

### ➕ POST /api/rooms

```json
{
  "name": "Lab 204",
  "buildingCode": "B",
  "floor": 2,
  "capacity": 24,
  "hasProjector": true,
  "isActive": true
}
```
### ➕ POST /api/reservations

```json
{
  "roomId": 2,
  "organizerName": "Anna Kowalska",
  "topic": "Warsztaty z HTTP i REST",
  "date": "2026-05-10",
  "startTime": "10:00:00",
  "endTime": "12:30:00",
  "status": "confirmed"
}
```
## 👨‍💻 Autor

Projekt wykonany przez:

Grzegorz Wojewódzki
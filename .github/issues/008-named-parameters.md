# Issue #8: Remove Redundant Named Parameters

**Labels:** `code-quality`, `refactoring`
**Priority:** P3 - Low
**Category:** 🟢 CODE-QUALITÄT & BEST PRACTICES

---

## Beschreibung

Im gesamten Codebase werden Named Parameters auch bei eindeutigen Parametern verwendet, was die Lesbarkeit nicht verbessert.

## Beispiele

**Datei:** `DateTimeRangeParser.cs:47`
```csharp
_calculators.AddRange(collection: calculators); // ❌ Unnötig
```

**Datei:** `DateTimeRange.cs:138`
```csharp
current = current.AddDays(value: 1); // ❌ Unnötig
```

**Datei:** `DateTimeRangeCalculatorBase.cs:27`
```csharp
=> input?.ToLower() == match?.ToLower(); // ✅ Gut
```

## Empfehlung

Named Parameters nur verwenden bei:
- Optionalen Parametern
- Bool-Parametern (für Klarheit)
- Mehrdeutigen Parametern

## Betroffene Dateien

- Alle `.cs` Dateien (codebase-wide refactoring)

## Geschätzter Aufwand

2-3 Stunden

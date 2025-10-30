# Issue #3: Fix Inverted Logic in DateTimeRange.TimeSpan Property

**Labels:** `bug`, `critical`
**Priority:** P0 - Critical
**Category:** 🔴 KRITISCHE PROBLEME

---

## Beschreibung

Die `TimeSpan`-Property hat invertierte Logik und liefert immer `TimeSpan.MinValue` für valide Ranges.

## Problematischer Code

**Datei:** `src/DateTimeRangeParser/DateTimeRange.cs:97-100`

```csharp
public TimeSpan TimeSpan =>
    Start >= End     // ❌ FALSCH: Sollte <= sein!
    ? End - Start
    : TimeSpan.MinValue;
```

## Aktuelles Verhalten

```csharp
var range = new DateTimeRange(
    start: new DateTime(2024, 1, 1),
    end: new DateTime(2024, 1, 10)
);

range.TimeSpan // Gibt TimeSpan.MinValue zurück ❌
// Erwartet: 9 Tage
```

## Lösung - Option A (Conservative)

```csharp
public TimeSpan TimeSpan =>
    Start <= End
    ? End - Start
    : TimeSpan.Zero;
```

## Lösung - Option B (Strict)

```csharp
public TimeSpan TimeSpan =>
    Start <= End
    ? End - Start
    : throw new InvalidOperationException("Start must be before or equal to End");
```

## Betroffene Dateien

- `src/DateTimeRangeParser/DateTimeRange.cs`

## Testing

- Bestehende Tests für TimeSpan prüfen/hinzufügen
- Edge Cases: gleiche Start/End-Daten

## Geschätzter Aufwand

1 Stunde

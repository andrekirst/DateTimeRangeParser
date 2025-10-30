# Issue #12: Improve DateTimeRange.Empty Semantics

**Labels:** `enhancement`, `api-design`
**Priority:** P3 - Low
**Category:** 🟢 CODE-QUALITÄT & BEST PRACTICES

---

## Beschreibung

`DateTimeRange.Empty` verwendet `DateTime.MinValue` bis `DateTime.MaxValue`, was nicht wirklich "empty" ist.

## Aktueller Code

**Datei:** `DateTimeRange.cs:83-89`

```csharp
public static DateTimeRange Empty =>
    new DateTimeRange(
        start: DateTime.MinValue,   // ❌ Nicht intuitiv
        end: DateTime.MaxValue)
    {
        IsValid = false
    };
```

## Problem

```csharp
var empty = DateTimeRange.Empty;
empty.Start // DateTime.MinValue (01.01.0001)
empty.End   // DateTime.MaxValue (31.12.9999)
// Iteration würde Millionen Jahre dauern!
```

## Lösung - Option A (Konsistente Werte)

```csharp
public static DateTimeRange Empty => new()
{
    Start = DateTime.MinValue,
    End = DateTime.MinValue,  // Konsistent
    IsValid = false
};
```

## Lösung - Option B (Default Values)

```csharp
public static DateTimeRange Empty => new()
{
    Start = default,
    End = default,
    IsValid = false
};
```

## Betroffene Dateien

- `src/DateTimeRangeParser/DateTimeRange.cs`

## Geschätzter Aufwand

1 Stunde

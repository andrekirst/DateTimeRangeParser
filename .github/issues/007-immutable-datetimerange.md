# Issue #7: Make DateTimeRange Immutable (readonly properties)

**Labels:** `enhancement`, `breaking-change`, `api-design`
**Priority:** P2 - Medium
**Category:** 🟢 CODE-QUALITÄT & BEST PRACTICES

---

## Beschreibung

`Start` und `End` haben public setter, was DateTimeRange nach der Konstruktion mutierbar macht. Dies widerspricht dem Design eines Value Objects.

## Problematischer Code

**Datei:** `src/DateTimeRangeParser/DateTimeRange.cs:93-95`

```csharp
public DateTime Start { get; set; } // ❌ Kann nachträglich verändert werden
public DateTime End { get; set; }
```

## Problem

```csharp
var range = new DateTimeRange(new DateTime(2024, 1, 1), new DateTime(2024, 1, 10));
range.Start = new DateTime(2025, 1, 1); // ❌ Sollte nicht möglich sein
```

## Lösung - Option A (Init-only, C# 9+)

```csharp
public DateTime Start { get; init; }
public DateTime End { get; init; }
```

## Lösung - Option B (Readonly)

```csharp
public DateTime Start { get; }
public DateTime End { get; }

public DateTimeRange(DateTime start, DateTime end)
{
    Start = start;
    End = end;
}
```

## Breaking Change

⚠️ Dies ist eine Breaking Change für Consumer, die Properties nach der Konstruktion setzen.

## Migration Path

1. Als Major Version Release (2.0.0)
2. Deprecation Warning in 1.x
3. Dokumentieren in Release Notes

## Betroffene Dateien

- `src/DateTimeRangeParser/DateTimeRange.cs`

## Geschätzter Aufwand

2-3 Stunden

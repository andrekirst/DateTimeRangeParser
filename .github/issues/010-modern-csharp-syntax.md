# Issue #10: Modernize C# Syntax (Pattern Matching, File-scoped Namespaces)

**Labels:** `refactoring`, `code-quality`
**Priority:** P3 - Low
**Category:** 🟢 CODE-QUALITÄT & BEST PRACTICES

---

## Beschreibung

Der Code verwendet veraltete Syntax-Patterns, die mit modernem C# verbessert werden können.

## Verbesserung 1: Pattern Matching

**Datei:** `DateTimeRange.cs:107-113`

```csharp
// Aktuell
public override bool Equals(object obj)
{
    DateTimeRange other = obj as DateTimeRange;
    return Start == other?.Start && End == other.End;
}

// Modern
public override bool Equals(object obj)
{
    return obj is DateTimeRange other
        && Start == other.Start
        && End == other.End;
}
```

## Verbesserung 2: File-scoped Namespaces (C# 10)

```csharp
// Aktuell
namespace DateTimeRangeParser
{
    public class DateTimeRange { }
}

// Modern
namespace DateTimeRangeParser;

public class DateTimeRange { }
```

## Verbesserung 3: Target-typed new (C# 9)

```csharp
// Aktuell
new Dictionary<string, DateTimeRange>()

// Modern
new()
```

## Betroffene Dateien

- Alle `.cs` Dateien

## Geschätzter Aufwand

3-4 Stunden

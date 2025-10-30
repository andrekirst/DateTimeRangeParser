# Issue #9: Enable Nullable Reference Types

**Labels:** `enhancement`, `type-safety`
**Priority:** P2 - Medium
**Category:** 🟢 CODE-QUALITÄT & BEST PRACTICES

---

## Beschreibung

Nullable Reference Types (C# 8+) sind nicht aktiviert, was zu möglichen Null-Reference-Exceptions führen kann.

## Lösung

**DateTimeRangeParser.csproj:**
```xml
<PropertyGroup>
  <Nullable>enable</Nullable>
</PropertyGroup>
```

## Code-Änderungen

```csharp
// Vorher
public DateTimeRange Parse(string input)

// Nachher
public DateTimeRange Parse(string? input)

// Vorher
public override bool Equals(object obj)

// Nachher
public override bool Equals(object? obj)
```

## Benefits

- Compile-time Null-Safety
- Bessere IDE-Unterstützung
- Weniger Runtime-Exceptions

## Effort

- ~2-4 Stunden für alle Annotationen
- Keine Breaking Changes für Consumer

## Betroffene Dateien

- Alle `.csproj` Dateien
- Alle `.cs` Dateien

## Geschätzter Aufwand

3-4 Stunden

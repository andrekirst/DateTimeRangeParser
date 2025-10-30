# Issue #11: Add Cache Management Methods

**Labels:** `enhancement`, `api`
**Priority:** P2 - Medium
**Category:** 🟢 CODE-QUALITÄT & BEST PRACTICES

---

## Beschreibung

Es gibt keine Möglichkeit, den Cache zu leeren oder seine Größe abzufragen.

## Fehlende API

```csharp
parser.ClearCache();           // Nicht vorhanden
var size = parser.CacheSize;   // Nicht vorhanden
```

## Lösung

**Datei:** `DateTimeRangeParser.cs`

```csharp
/// <summary>
/// Clears all cached parse results.
/// </summary>
public void ClearCache()
{
    _cachedValues.Clear();
}

/// <summary>
/// Gets the current number of cached entries.
/// </summary>
public int CacheCount => _cachedValues.Count;

/// <summary>
/// Checks if a specific input is cached.
/// </summary>
public bool IsCached(string input) =>
    CachingEnabled && _cachedValues.ContainsKey(input);
```

## Use Cases

- Memory Management bei vielen verschiedenen Inputs
- Testing
- Diagnostics

## Betroffene Dateien

- `src/DateTimeRangeParser/DateTimeRangeParser.cs`
- `src/DateTimeRangeParser/IDateTimeRangerParser.cs` (Interface erweitern?)

## Geschätzter Aufwand

1-2 Stunden

# Issue #2: Fix Thread-Safety Issue in Cache (ConcurrentDictionary)

**Labels:** `bug`, `critical`, `thread-safety`
**Priority:** P0 - Critical
**Category:** 🔴 KRITISCHE PROBLEME

---

## Beschreibung

Der Cache in `DateTimeRangeParser` ist nicht thread-safe. Bei parallelen Parse-Aufrufen kann es zu `ArgumentException` (doppelter Key) oder Race Conditions kommen.

## Problematischer Code

**Datei:** `src/DateTimeRangeParser/DateTimeRangeParser.cs:11`

```csharp
private readonly Dictionary<string, DateTimeRange> _cachedValues = new Dictionary<string, DateTimeRange>();
```

**Fehler tritt auf bei:**
```csharp
private void AddCalculatedValueToCache(string input, DateTimeRange calculatedValue)
{
    if (CachingEnabled)
    {
        _cachedValues.Add(key: input, value: calculatedValue); // ❌ Nicht thread-safe
    }
}
```

## Reproduktion

```csharp
var parser = new DateTimeRangeParser();
Parallel.For(0, 100, i =>
{
    parser.Parse("today"); // Race condition bei Cache-Add
});
```

## Lösung

Verwende `ConcurrentDictionary`:

```csharp
using System.Collections.Concurrent;

private readonly ConcurrentDictionary<string, DateTimeRange> _cachedValues = new();

private bool ExistsCachedValue(string cacheValue)
{
    return CachingEnabled && _cachedValues.ContainsKey(cacheValue);
}

private void AddCalculatedValueToCache(string input, DateTimeRange calculatedValue)
{
    if (CachingEnabled)
    {
        _cachedValues.TryAdd(input, calculatedValue);
    }
}
```

## Betroffene Dateien

- `src/DateTimeRangeParser/DateTimeRangeParser.cs`

## Testing

- Unit-Test für parallele Parse-Aufrufe hinzufügen
- Stress-Test mit 1000+ parallelen Requests

## Geschätzter Aufwand

1-2 Stunden

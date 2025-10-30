# Issue #5: Add Input Validation for Parse Method

**Labels:** `enhancement`, `robustness`
**Priority:** P1 - High
**Category:** 🟡 WICHTIGE VERBESSERUNGEN

---

## Beschreibung

Die `Parse`-Methode validiert nicht auf `null` oder leere Strings, was zu unerwarteten Exceptions führen kann.

## Problematischer Code

**Datei:** `src/DateTimeRangeParser/DateTimeRangeParser.cs:50-75`

```csharp
public DateTimeRange Parse(string input)
{
    if (ExistsCachedValue(cacheValue: input)) // ❌ Was wenn input == null?
    {
        return _cachedValues[key: input];
    }
    // ...
}
```

## Lösung

```csharp
public DateTimeRange Parse(string input)
{
    if (string.IsNullOrWhiteSpace(input))
    {
        return DateTimeRange.Empty;
    }

    if (ExistsCachedValue(input))
    {
        return _cachedValues[input];
    }
    // ... Rest
}
```

## Testing

```csharp
[Theory]
[InlineData(null)]
[InlineData("")]
[InlineData("   ")]
public void Parse_InvalidInput_ReturnsEmpty(string input)
{
    var parser = new DateTimeRangeParser();
    var result = parser.Parse(input);
    result.ShouldBe(DateTimeRange.Empty);
}
```

## Betroffene Dateien

- `src/DateTimeRangeParser/DateTimeRangeParser.cs`
- `src/DateTimeRangeParser.Tests/` (neue Tests)

## Geschätzter Aufwand

2-3 Stunden

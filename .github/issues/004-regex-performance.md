# Issue #4: Improve Regex Performance with Static Compilation

**Labels:** `enhancement`, `performance`
**Priority:** P1 - High
**Category:** 🟡 WICHTIGE VERBESSERUNGEN

---

## Beschreibung

Regex wird bei jedem Aufruf neu kompiliert, was die Performance beeinträchtigt.

## Problematischer Code

**Datei:** `src/DateTimeRangeParser/Calculations/NumberRangeTypeCalculator.cs:52-55`

```csharp
public override bool DoesMatchInput(string input)
{
    return Regex.IsMatch(
        input: input,
        pattern: Pattern,
        options: RegexOptions.Compiled); // ❌ Wird NICHT einmal kompiliert
}
```

## Lösung - Option A (C# 7+)

```csharp
private static readonly Regex PatternRegex = new(Pattern, RegexOptions.Compiled);

public override bool DoesMatchInput(string input)
{
    return PatternRegex.IsMatch(input);
}
```

## Lösung - Option B (.NET 7+ Source Generators)

```csharp
[GeneratedRegex(@"^([\+\-]{0,1})([1-9]\d*)(d|w|m|y)$", RegexOptions.Compiled)]
private static partial Regex PatternRegex();

public override bool DoesMatchInput(string input)
{
    return PatternRegex().IsMatch(input);
}
```

## Performance-Gewinn

- Geschätzt: 2-5x schneller bei wiederholten Aufrufen
- Weniger GC-Druck

## Betroffene Dateien

- `src/DateTimeRangeParser/Calculations/NumberRangeTypeCalculator.cs`

## Geschätzter Aufwand

1-2 Stunden

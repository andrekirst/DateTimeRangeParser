# Issue #6: Fix Null-Check Logic in DynamicRangeCalculator

**Labels:** `bug`, `code-quality`
**Priority:** P1 - High
**Category:** 🟡 WICHTIGE VERBESSERUNGEN

---

## Beschreibung

Null-Check für String-Split ist nutzlos, da `Split()` nie `null` zurückgibt.

## Problematischer Code

**Datei:** `src/DateTimeRangeParser/Calculations/DynamicRangeCalculator.cs:40-44`

```csharp
public override bool DoesMatchInput(string input)
{
    string[] splitBySeperator = input.Split(separator: Separator);
    if (splitBySeperator != null && splitBySeperator.Length != 2) // null-Check ist nutzlos
    {
        return false;
    }
    // ...
}
```

## Lösung

```csharp
public override bool DoesMatchInput(string input)
{
    if (string.IsNullOrEmpty(input))
        return false;

    string[] splitBySeperator = input.Split(Separator);
    if (splitBySeperator.Length != 2)
        return false;

    return
        OtherCalculations.Any(m => m.DoesMatchInput(splitBySeperator[0])) &&
        OtherCalculations.Any(m => m.DoesMatchInput(splitBySeperator[1]));
}
```

## Betroffene Dateien

- `src/DateTimeRangeParser/Calculations/DynamicRangeCalculator.cs`

## Geschätzter Aufwand

1 Stunde

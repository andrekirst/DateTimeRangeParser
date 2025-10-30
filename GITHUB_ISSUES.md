# GitHub Issues - DateTimeRangeParser Code-Analyse

Diese Datei enthält alle identifizierten Verbesserungsvorschläge als strukturierte GitHub Issues.

---

## 🔴 KRITISCHE PROBLEME

### Issue #1: Migrate to .NET 8.0 (End-of-Life .NET Core 2.1)

**Labels:** `critical`, `enhancement`, `security`
**Priority:** P0 - Critical
**Assignee:** -

#### Beschreibung
Das Projekt verwendet .NET Core 2.1, das seit August 2021 End-of-Life ist und keine Sicherheitsupdates mehr erhält.

#### Probleme
- ❌ Keine Sicherheitsupdates
- ❌ Fehlende Performance-Verbesserungen
- ❌ Kompatibilitätsprobleme mit modernen Tools
- ❌ NuGet-Pakete werden möglicherweise nicht mehr unterstützt

#### Lösung
Migration auf .NET 8.0 (LTS bis November 2026) oder .NET 6.0 (LTS bis November 2024)

**DateTimeRangeParser.csproj:**
```xml
<PropertyGroup>
  <TargetFramework>net8.0</TargetFramework>
  <!-- Für Multi-Targeting -->
  <!-- <TargetFrameworks>net6.0;net8.0</TargetFrameworks> -->
</PropertyGroup>
```

#### Betroffene Dateien
- `src/DateTimeRangeParser/DateTimeRangeParser.csproj`
- `src/DateTimeRangeParser.Tests/DateTimeRangeParser.Tests.csproj`
- `src/DateTimeRangeParser.TestConsole/DateTimeRangeParser.TestConsole.csproj`
- `appveyor.yml`

#### Testing
- Alle Unit-Tests müssen nach Migration erfolgreich sein
- Kompatibilität mit bestehenden Consumers prüfen

---

### Issue #2: Fix Thread-Safety Issue in Cache (ConcurrentDictionary)

**Labels:** `bug`, `critical`, `thread-safety`
**Priority:** P0 - Critical
**Assignee:** -

#### Beschreibung
Der Cache in `DateTimeRangeParser` ist nicht thread-safe. Bei parallelen Parse-Aufrufen kann es zu `ArgumentException` (doppelter Key) oder Race Conditions kommen.

#### Problematischer Code
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

#### Reproduktion
```csharp
var parser = new DateTimeRangeParser();
Parallel.For(0, 100, i =>
{
    parser.Parse("today"); // Race condition bei Cache-Add
});
```

#### Lösung
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

#### Betroffene Dateien
- `src/DateTimeRangeParser/DateTimeRangeParser.cs`

#### Testing
- Unit-Test für parallele Parse-Aufrufe hinzufügen
- Stress-Test mit 1000+ parallelen Requests

---

### Issue #3: Fix Inverted Logic in DateTimeRange.TimeSpan Property

**Labels:** `bug`, `critical`
**Priority:** P0 - Critical
**Assignee:** -

#### Beschreibung
Die `TimeSpan`-Property hat invertierte Logik und liefert immer `TimeSpan.MinValue` für valide Ranges.

#### Problematischer Code
**Datei:** `src/DateTimeRangeParser/DateTimeRange.cs:97-100`

```csharp
public TimeSpan TimeSpan =>
    Start >= End     // ❌ FALSCH: Sollte <= sein!
    ? End - Start
    : TimeSpan.MinValue;
```

#### Aktuelles Verhalten
```csharp
var range = new DateTimeRange(
    start: new DateTime(2024, 1, 1),
    end: new DateTime(2024, 1, 10)
);

range.TimeSpan // Gibt TimeSpan.MinValue zurück ❌
// Erwartet: 9 Tage
```

#### Lösung - Option A (Conservative)
```csharp
public TimeSpan TimeSpan =>
    Start <= End
    ? End - Start
    : TimeSpan.Zero;
```

#### Lösung - Option B (Strict)
```csharp
public TimeSpan TimeSpan =>
    Start <= End
    ? End - Start
    : throw new InvalidOperationException("Start must be before or equal to End");
```

#### Betroffene Dateien
- `src/DateTimeRangeParser/DateTimeRange.cs`

#### Testing
- Bestehende Tests für TimeSpan prüfen/hinzufügen
- Edge Cases: gleiche Start/End-Daten

---

## 🟡 WICHTIGE VERBESSERUNGEN

### Issue #4: Improve Regex Performance with Static Compilation

**Labels:** `enhancement`, `performance`
**Priority:** P1 - High
**Assignee:** -

#### Beschreibung
Regex wird bei jedem Aufruf neu kompiliert, was die Performance beeinträchtigt.

#### Problematischer Code
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

#### Lösung - Option A (C# 7+)
```csharp
private static readonly Regex PatternRegex = new(Pattern, RegexOptions.Compiled);

public override bool DoesMatchInput(string input)
{
    return PatternRegex.IsMatch(input);
}
```

#### Lösung - Option B (.NET 7+ Source Generators)
```csharp
[GeneratedRegex(@"^([\+\-]{0,1})([1-9]\d*)(d|w|m|y)$", RegexOptions.Compiled)]
private static partial Regex PatternRegex();

public override bool DoesMatchInput(string input)
{
    return PatternRegex().IsMatch(input);
}
```

#### Performance-Gewinn
- Geschätzt: 2-5x schneller bei wiederholten Aufrufen
- Weniger GC-Druck

#### Betroffene Dateien
- `src/DateTimeRangeParser/Calculations/NumberRangeTypeCalculator.cs`

---

### Issue #5: Add Input Validation for Parse Method

**Labels:** `enhancement`, `robustness`
**Priority:** P1 - High
**Assignee:** -

#### Beschreibung
Die `Parse`-Methode validiert nicht auf `null` oder leere Strings, was zu unerwarteten Exceptions führen kann.

#### Problematischer Code
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

#### Lösung
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

#### Testing
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

#### Betroffene Dateien
- `src/DateTimeRangeParser/DateTimeRangeParser.cs`
- `src/DateTimeRangeParser.Tests/` (neue Tests)

---

### Issue #6: Fix Null-Check Logic in DynamicRangeCalculator

**Labels:** `bug`, `code-quality`
**Priority:** P1 - High
**Assignee:** -

#### Beschreibung
Null-Check für String-Split ist nutzlos, da `Split()` nie `null` zurückgibt.

#### Problematischer Code
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

#### Lösung
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

#### Betroffene Dateien
- `src/DateTimeRangeParser/Calculations/DynamicRangeCalculator.cs`

---

### Issue #7: Make DateTimeRange Immutable (readonly properties)

**Labels:** `enhancement`, `breaking-change`, `api-design`
**Priority:** P2 - Medium
**Assignee:** -

#### Beschreibung
`Start` und `End` haben public setter, was DateTimeRange nach der Konstruktion mutierbar macht. Dies widerspricht dem Design eines Value Objects.

#### Problematischer Code
**Datei:** `src/DateTimeRangeParser/DateTimeRange.cs:93-95`

```csharp
public DateTime Start { get; set; } // ❌ Kann nachträglich verändert werden
public DateTime End { get; set; }
```

#### Problem
```csharp
var range = new DateTimeRange(new DateTime(2024, 1, 1), new DateTime(2024, 1, 10));
range.Start = new DateTime(2025, 1, 1); // ❌ Sollte nicht möglich sein
```

#### Lösung - Option A (Init-only, C# 9+)
```csharp
public DateTime Start { get; init; }
public DateTime End { get; init; }
```

#### Lösung - Option B (Readonly)
```csharp
public DateTime Start { get; }
public DateTime End { get; }

public DateTimeRange(DateTime start, DateTime end)
{
    Start = start;
    End = end;
}
```

#### Breaking Change
⚠️ Dies ist eine Breaking Change für Consumer, die Properties nach der Konstruktion setzen.

#### Migration Path
1. Als Major Version Release (2.0.0)
2. Deprecation Warning in 1.x
3. Dokumentieren in Release Notes

#### Betroffene Dateien
- `src/DateTimeRangeParser/DateTimeRange.cs`

---

## 🟢 CODE-QUALITÄT & BEST PRACTICES

### Issue #8: Remove Redundant Named Parameters

**Labels:** `code-quality`, `refactoring`
**Priority:** P3 - Low
**Assignee:** -

#### Beschreibung
Im gesamten Codebase werden Named Parameters auch bei eindeutigen Parametern verwendet, was die Lesbarkeit nicht verbessert.

#### Beispiele

**Datei:** `DateTimeRangeParser.cs:47`
```csharp
_calculators.AddRange(collection: calculators); // ❌ Unnötig
```

**Datei:** `DateTimeRange.cs:138`
```csharp
current = current.AddDays(value: 1); // ❌ Unnötig
```

**Datei:** `DateTimeRangeCalculatorBase.cs:27`
```csharp
=> input?.ToLower() == match?.ToLower(); // ✅ Gut
```

#### Empfehlung
Named Parameters nur verwenden bei:
- Optionalen Parametern
- Bool-Parametern (für Klarheit)
- Mehrdeutigen Parametern

#### Betroffene Dateien
- Alle `.cs` Dateien (codebase-wide refactoring)

---

### Issue #9: Enable Nullable Reference Types

**Labels:** `enhancement`, `type-safety`
**Priority:** P2 - Medium
**Assignee:** -

#### Beschreibung
Nullable Reference Types (C# 8+) sind nicht aktiviert, was zu möglichen Null-Reference-Exceptions führen kann.

#### Lösung
**DateTimeRangeParser.csproj:**
```xml
<PropertyGroup>
  <Nullable>enable</Nullable>
</PropertyGroup>
```

#### Code-Änderungen
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

#### Benefits
- Compile-time Null-Safety
- Bessere IDE-Unterstützung
- Weniger Runtime-Exceptions

#### Effort
- ~2-4 Stunden für alle Annotationen
- Keine Breaking Changes für Consumer

#### Betroffene Dateien
- Alle `.csproj` Dateien
- Alle `.cs` Dateien

---

### Issue #10: Modernize C# Syntax (Pattern Matching, File-scoped Namespaces)

**Labels:** `refactoring`, `code-quality`
**Priority:** P3 - Low
**Assignee:** -

#### Beschreibung
Der Code verwendet veraltete Syntax-Patterns, die mit modernem C# verbessert werden können.

#### Verbesserung 1: Pattern Matching
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

#### Verbesserung 2: File-scoped Namespaces (C# 10)
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

#### Verbesserung 3: Target-typed new (C# 9)
```csharp
// Aktuell
new Dictionary<string, DateTimeRange>()

// Modern
new()
```

#### Betroffene Dateien
- Alle `.cs` Dateien

---

### Issue #11: Add Cache Management Methods

**Labels:** `enhancement`, `api`
**Priority:** P2 - Medium
**Assignee:** -

#### Beschreibung
Es gibt keine Möglichkeit, den Cache zu leeren oder seine Größe abzufragen.

#### Fehlende API
```csharp
parser.ClearCache();           // Nicht vorhanden
var size = parser.CacheSize;   // Nicht vorhanden
```

#### Lösung
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

#### Use Cases
- Memory Management bei vielen verschiedenen Inputs
- Testing
- Diagnostics

#### Betroffene Dateien
- `src/DateTimeRangeParser/DateTimeRangeParser.cs`
- `src/DateTimeRangeParser/IDateTimeRangerParser.cs` (Interface erweitern?)

---

### Issue #12: Improve DateTimeRange.Empty Semantics

**Labels:** `enhancement`, `api-design`
**Priority:** P3 - Low
**Assignee:** -

#### Beschreibung
`DateTimeRange.Empty` verwendet `DateTime.MinValue` bis `DateTime.MaxValue`, was nicht wirklich "empty" ist.

#### Aktueller Code
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

#### Problem
```csharp
var empty = DateTimeRange.Empty;
empty.Start // DateTime.MinValue (01.01.0001)
empty.End   // DateTime.MaxValue (31.12.9999)
// Iteration würde Millionen Jahre dauern!
```

#### Lösung - Option A (Konsistente Werte)
```csharp
public static DateTimeRange Empty => new()
{
    Start = DateTime.MinValue,
    End = DateTime.MinValue,  // Konsistent
    IsValid = false
};
```

#### Lösung - Option B (Default Values)
```csharp
public static DateTimeRange Empty => new()
{
    Start = default,
    End = default,
    IsValid = false
};
```

#### Betroffene Dateien
- `src/DateTimeRangeParser/DateTimeRange.cs`

---

## 📝 DOKUMENTATION

### Issue #13: Add XML Documentation for Public API

**Labels:** `documentation`, `enhancement`
**Priority:** P2 - Medium
**Assignee:** -

#### Beschreibung
Die öffentliche API hat keine XML-Dokumentation, was IntelliSense-Support und API-Dokumentation behindert.

#### Aktueller Zustand
```csharp
public DateTimeRange Parse(string input) // ❌ Keine Dokumentation
```

#### Lösung
```csharp
/// <summary>
/// Parses a string input into a <see cref="DateTimeRange"/>.
/// </summary>
/// <param name="input">
/// Input string representing a date range. Supported formats:
/// <list type="bullet">
///   <item>"today", "yesterday" - Single day ranges</item>
///   <item>"1d", "-7d" - Relative day offsets</item>
///   <item>"currentweek", "lastweek" - Week ranges</item>
///   <item>"thismonth", "thisyear" - Month/year ranges</item>
///   <item>"CW13.2024" - Specific calendar week</item>
///   <item>"yesterday->today" - Dynamic ranges</item>
/// </list>
/// </param>
/// <returns>
/// The calculated <see cref="DateTimeRange"/> or <see cref="DateTimeRange.Empty"/>
/// if the input format is not recognized.
/// </returns>
/// <example>
/// <code>
/// var parser = new DateTimeRangeParser();
/// var today = parser.Parse("today");
/// var lastWeek = parser.Parse("-7d->today");
/// </code>
/// </example>
public DateTimeRange Parse(string input)
```

#### Aktivierung in .csproj
```xml
<PropertyGroup>
  <GenerateDocumentationFile>true</GenerateDocumentationFile>
  <NoWarn>$(NoWarn);CS1591</NoWarn> <!-- Optional: Warnings für fehlende Docs -->
</PropertyGroup>
```

#### Zu dokumentieren
- `DateTimeRangeParser` (alle public members)
- `DateTimeRange` (alle public members)
- `DateTimeRangeCalculatorBase`
- `IDateTimeRangerParser`
- `IDateTimeProvider`

#### Benefits
- IntelliSense in IDEs
- Automatische API-Dokumentation (DocFX, etc.)
- Bessere Developer Experience

#### Betroffene Dateien
- Alle öffentlichen Klassen und Members

---

### Issue #14: Expand README with Usage Examples and Documentation

**Labels:** `documentation`, `enhancement`
**Priority:** P1 - High
**Assignee:** -

#### Beschreibung
Das README enthält nur eine Zeile und ist nicht hilfreich für neue Benutzer.

#### Aktuelles README
```markdown
# DateTimeRangeParser

An API for parsing DateTime-Ranges from an input string
```

#### Empfohlene Struktur

```markdown
# DateTimeRangeParser

[![NuGet](https://img.shields.io/nuget/v/DateTimeRangeParser.svg)](https://www.nuget.org/packages/DateTimeRangeParser/)
[![Build Status](https://ci.appveyor.com/api/projects/status/...)](https://ci.appveyor.com/project/...)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

A flexible .NET library for parsing human-readable date range expressions into structured `DateTimeRange` objects.

## Features

- 🎯 Parse natural language date expressions ("today", "yesterday", "currentweek")
- 📅 Support for relative dates ("1d", "-7d", "2w", "-1m")
- 🔢 Calendar week support ("CW13.2024")
- 🔗 Dynamic range composition ("yesterday->today")
- 🌍 Culture-aware calculators (English, German)
- ⚡ Built-in caching for performance
- 🧪 Fully tested with comprehensive unit tests

## Installation

### NuGet Package Manager
```bash
Install-Package DateTimeRangeParser
```

### .NET CLI
```bash
dotnet add package DateTimeRangeParser
```

## Quick Start

```csharp
using DateTimeRangeParser;

var parser = new DateTimeRangeParser();

// Simple date expressions
var today = parser.Parse("today");
// Result: Start=2024-10-30, End=2024-10-30

var yesterday = parser.Parse("yesterday");
// Result: Start=2024-10-29, End=2024-10-29

// Relative dates
var lastWeek = parser.Parse("-7d");
// Result: Start=2024-10-23, End=2024-10-23

// Date ranges
var currentWeek = parser.Parse("currentweek");
// Result: Start=2024-10-28 (Monday), End=2024-11-03 (Sunday)

// Dynamic ranges
var lastSevenDays = parser.Parse("-6d->today");
// Result: Start=2024-10-24, End=2024-10-30

// Calendar weeks
var calendarWeek = parser.Parse("CW13.2024");
// Result: Start=2024-03-25, End=2024-03-31
```

## Supported Formats

### English Calculators
| Input | Description | Example Result (if today = 2024-10-30) |
|-------|-------------|---------------------------------------|
| `today` | Current day | 2024-10-30 to 2024-10-30 |
| `yesterday` | Previous day | 2024-10-29 to 2024-10-29 |
| `currentweek` | Monday to Sunday of current week | 2024-10-28 to 2024-11-03 |
| `lastweek` | Previous week | 2024-10-21 to 2024-10-27 |
| `thismonth` | First to last day of current month | 2024-10-01 to 2024-10-31 |
| `thisyear` | January 1 to December 31 of current year | 2024-01-01 to 2024-12-31 |
| `1d` | Tomorrow (+1 day) | 2024-10-31 to 2024-10-31 |
| `-7d` | 7 days ago | 2024-10-23 to 2024-10-23 |
| `2w` | 2 weeks from now | 2024-11-13 to 2024-11-13 |
| `-1m` | 1 month ago | 2024-09-30 to 2024-09-30 |
| `1y` | 1 year from now | 2025-10-30 to 2025-10-30 |
| `CW13.2024` | Calendar week 13 of 2024 | 2024-03-25 to 2024-03-31 |
| `yesterday->today` | Range from yesterday to today | 2024-10-29 to 2024-10-30 |

### German Calculators
| Input | Description |
|-------|-------------|
| `heute` | Heute (today) |
| `aktuellewoche` | Aktuelle Woche (current week) |

## Advanced Usage

### Caching Control
```csharp
var parser = new DateTimeRangeParser();

// Disable caching
parser.CachingEnabled = false;

// Re-enable caching
parser.CachingEnabled = true;
```

### Custom DateTime Provider (for Testing)
```csharp
public class CustomDateTimeProvider : IDateTimeProvider
{
    public DateTime Today => new DateTime(2024, 1, 1);
}

var parser = new DateTimeRangeParser(new CustomDateTimeProvider());
var result = parser.Parse("today");
// Result is based on 2024-01-01
```

### DateTimeRange Operations

```csharp
var range = parser.Parse("today");

// Spread operations
var expanded = range.SpreadByDays(3);        // ±3 days
var expandedWeeks = range.SpreadByWeeks(1);  // ±1 week
var expandedMonths = range.SpreadByMonths(2); // ±2 months

// Asymmetric spread
var asymmetric = range.SpreadByDays(1, 3);   // -1 day start, +3 days end

// Check if date is in range
bool contains = range.IsDateTimeBetween(DateTime.Now);

// Iterate through dates
foreach (var date in range)
{
    Console.WriteLine(date);
}

// Get duration
TimeSpan duration = range.TimeSpan;
```

### Event Handling
```csharp
var parser = new DateTimeRangeParser();

parser.RaisedCalculation += (sender, args) =>
{
    Console.WriteLine($"Calculator used: {args.RaisedCalculator.Name}");
};

var result = parser.Parse("today");
// Output: Calculator used: TodayCalculator
```

## Requirements

- .NET 8.0 or later (or .NET Core 2.1+ for older versions)

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Authors

- André Kirst ([@andrekirst](https://github.com/andrekirst))

## Changelog

See [CHANGELOG.md](CHANGELOG.md) for version history.
```

#### Zusätzliche Dateien
- `CHANGELOG.md` - Version History
- `CONTRIBUTING.md` - Contribution Guidelines
- `LICENSE` - MIT License (falls nicht vorhanden)

#### Betroffene Dateien
- `README.md`
- `CHANGELOG.md` (neu)
- `CONTRIBUTING.md` (neu)

---

## 🧪 TESTS

### Issue #15: Improve Test Coverage and Add Edge Case Tests

**Labels:** `testing`, `enhancement`
**Priority:** P2 - Medium
**Assignee:** -

#### Beschreibung
Die Tests sind gut, aber es fehlen Tests für Edge Cases und Thread-Safety.

#### Fehlende Test-Kategorien

**1. Null/Empty Input Tests**
```csharp
[Theory]
[InlineData(null)]
[InlineData("")]
[InlineData("   ")]
[InlineData("\t\n")]
public void Parse_InvalidInput_ReturnsEmpty(string input)
{
    var parser = new DateTimeRangeParser();
    var result = parser.Parse(input);
    result.ShouldBe(DateTimeRange.Empty);
    result.IsValid.ShouldBeFalse();
}
```

**2. Thread-Safety Tests**
```csharp
[Fact]
public void Parse_ParallelCalls_NoExceptions()
{
    var parser = new DateTimeRangeParser();
    var exceptions = new ConcurrentBag<Exception>();

    Parallel.For(0, 1000, i =>
    {
        try
        {
            parser.Parse("today");
        }
        catch (Exception ex)
        {
            exceptions.Add(ex);
        }
    });

    exceptions.ShouldBeEmpty();
}

[Fact]
public void Parse_ParallelCallsSameInput_CachesCorrectly()
{
    var parser = new DateTimeRangeParser();
    var results = new ConcurrentBag<DateTimeRange>();

    Parallel.For(0, 100, i =>
    {
        results.Add(parser.Parse("today"));
    });

    results.Distinct().Count().ShouldBe(1);
}
```

**3. TimeSpan Edge Cases**
```csharp
[Fact]
public void TimeSpan_SameStartAndEnd_ReturnsZero()
{
    var range = new DateTimeRange(
        new DateTime(2024, 1, 1),
        new DateTime(2024, 1, 1)
    );

    range.TimeSpan.ShouldBe(TimeSpan.Zero);
}

[Fact]
public void TimeSpan_EndBeforeStart_ThrowsOrReturnsZero()
{
    var range = new DateTimeRange(
        new DateTime(2024, 1, 10),
        new DateTime(2024, 1, 1)
    );

    // Depending on fix: either throws or returns TimeSpan.Zero
}
```

**4. Cache Tests**
```csharp
[Fact]
public void Parse_WithCachingDisabled_DoesNotCache()
{
    var parser = new DateTimeRangeParser { CachingEnabled = false };

    parser.Parse("today");
    parser.CacheCount.ShouldBe(0);
}

[Fact]
public void ClearCache_RemovesAllEntries()
{
    var parser = new DateTimeRangeParser();
    parser.Parse("today");
    parser.Parse("yesterday");

    parser.ClearCache();
    parser.CacheCount.ShouldBe(0);
}
```

**5. Consistency: Assert vs Shouldly**

Problem: Tests mischen `Assert` und `Shouldly`.

Empfehlung: Einheitlich `Shouldly` verwenden:
```csharp
// Statt
Assert.Equal(expected, actual);

// Verwende
actual.ShouldBe(expected);
```

#### Betroffene Dateien
- `src/DateTimeRangeParser.Tests/` (neue Tests hinzufügen)

---

## 🔧 ARCHITEKTUR

### Issue #16: Optimize CalculationsLoader Reflection Performance

**Labels:** `performance`, `enhancement`, `architecture`
**Priority:** P2 - Medium
**Assignee:** -

#### Beschreibung
`CalculationsLoader` verwendet Reflection über alle geladenen Assemblies, was langsam und unsicher ist.

#### Problematischer Code
**Datei:** `CalculationsLoader.cs:14-22`

```csharp
public List<DateTimeRangeCalculatorBase> LoadCalculations(List<CultureInfo> loadCulturesOf = null)
{
    Type calculatorBaseType = typeof(DateTimeRangeCalculatorBase);
    return AppDomain.CurrentDomain.GetAssemblies() // ❌ ALLE Assemblies!
        .SelectMany(assembly => assembly.GetTypes())
        .Where(type => type.IsPublic)
        .Where(type => !type.IsAbstract)
        .Where(type => calculatorBaseType.IsAssignableFrom(type))
        .Select(Activator.CreateInstance)
        .Cast<DateTimeRangeCalculatorBase>()
        .FilterBySupportedCultures(loadCulturesOf)
        .ToList();
}
```

#### Probleme
- Scannt ALLE Assemblies im AppDomain (inkl. System, Microsoft, etc.)
- Langsam bei großen Anwendungen
- Findet möglicherweise unerwünschte Implementierungen in Drittanbieter-Assemblies

#### Lösung - Option A (Scope auf eigenes Assembly)
```csharp
public List<DateTimeRangeCalculatorBase> LoadCalculations(List<CultureInfo> loadCulturesOf = null)
{
    Type calculatorBaseType = typeof(DateTimeRangeCalculatorBase);

    return calculatorBaseType.Assembly  // ✅ Nur eigenes Assembly
        .GetTypes()
        .Where(type => type.IsPublic)
        .Where(type => !type.IsAbstract)
        .Where(type => calculatorBaseType.IsAssignableFrom(type))
        .Select(Activator.CreateInstance)
        .Cast<DateTimeRangeCalculatorBase>()
        .FilterBySupportedCultures(loadCulturesOf)
        .ToList();
}
```

#### Lösung - Option B (.NET 7+ Source Generators)
Verwende Source Generators zur Compile-Zeit-Discovery statt Runtime-Reflection.

#### Performance-Gewinn
- 10-100x schneller Startup
- Keine unerwünschten Treffer
- Deterministisch

#### Betroffene Dateien
- `src/DateTimeRangeParser/CalculationsLoader.cs`

---

### Issue #17: Review Operator Overloads (++ and --) for Clarity

**Labels:** `api-design`, `usability`, `discussion`
**Priority:** P3 - Low
**Assignee:** -

#### Beschreibung
Die `++` und `--` Operatoren für `DateTimeRange` sind unintuitiv und schlecht dokumentiert.

#### Aktueller Code
**Datei:** `DateTimeRange.cs:163-171`

```csharp
public static DateTimeRange operator ++(DateTimeRange dateTimeRange)
{
    return dateTimeRange.SpreadByDays(days: 1);
}

public static DateTimeRange operator --(DateTimeRange dateTimeRange)
{
    return dateTimeRange.SpreadByDays(days: -1);
}
```

#### Problem: Unklare Semantik
```csharp
var range = parser.Parse("today"); // 2024-10-30 to 2024-10-30
range++; // Was passiert?
         // Erwartung: 2024-10-31 to 2024-10-31 (nächster Tag)?
         // Tatsächlich: 2024-10-29 to 2024-10-31 (spread!)
```

Der Name `SpreadByDays` deutet auf Expansion hin, nicht auf Verschiebung!

#### Diskussionspunkte
1. **Option A:** Operatoren entfernen (Breaking Change)
2. **Option B:** Semantik ändern auf Shift statt Spread
3. **Option C:** Nur dokumentieren und beibehalten

#### Empfehlung
Option A: Entfernen, da:
- Semantik unklar
- Wenig Nutzen (explizite Methoden sind klarer)
- Potenzielle Quelle von Bugs

Wenn beibehalten: Ausführliche XML-Dokumentation hinzufügen!

#### Betroffene Dateien
- `src/DateTimeRangeParser/DateTimeRange.cs`

---

### Issue #18: Update NuGet Dependencies

**Labels:** `dependencies`, `security`, `maintenance`
**Priority:** P2 - Medium
**Assignee:** -

#### Beschreibung
Test-Dependencies sind veraltet (teilweise 5+ Jahre alt).

#### Aktuelle Versionen
**Datei:** `DateTimeRangeParser.Tests.csproj`

```xml
<PackageReference Include="Moq" Version="4.10.1" />      <!-- 2019 -->
<PackageReference Include="Shouldly" Version="3.0.2" />  <!-- 2019 -->
<PackageReference Include="xunit" Version="2.4.1" />     <!-- 2019 -->
```

#### Empfohlene Updates (Stand 2024)
```xml
<PackageReference Include="Moq" Version="4.20.70" />
<PackageReference Include="Shouldly" Version="4.2.1" />
<PackageReference Include="xunit" Version="2.6.6" />
<PackageReference Include="xunit.runner.visualstudio" Version="2.5.6">
  <PrivateAssets>all</PrivateAssets>
  <IncludeAssets>runtime; build; native; contentfiles; analyzers</IncludeAssets>
</PackageReference>
```

#### Veraltete Tool-Reference entfernen
```xml
<!-- ❌ Entfernen - nicht mehr benötigt -->
<DotNetCliToolReference Include="dotnet-xunit" Version="2.3.1" />
```

#### Benefits
- Sicherheitsupdates
- Bug-Fixes
- Performance-Verbesserungen
- Kompatibilität mit .NET 8

#### Testing
Alle Unit-Tests müssen nach Update erfolgreich sein.

#### Betroffene Dateien
- `src/DateTimeRangeParser.Tests/DateTimeRangeParser.Tests.csproj`

---

## 📊 ZUSAMMENFASSUNG

### Priorisierung
- **P0 - Critical (sofort):** Issues #1, #2, #3
- **P1 - High (kurzfristig):** Issues #4, #5, #6, #14
- **P2 - Medium (mittelfristig):** Issues #7, #9, #11, #13, #15, #16, #18
- **P3 - Low (langfristig):** Issues #8, #10, #12, #17

### Geschätzter Aufwand
| Kategorie | Issues | Aufwand |
|-----------|--------|---------|
| Kritisch | 3 | ~8-16 Stunden |
| Wichtig | 4 | ~8-12 Stunden |
| Medium | 7 | ~16-24 Stunden |
| Niedrig | 4 | ~8-12 Stunden |
| **Gesamt** | **18** | **40-64 Stunden** |

### Labels für GitHub
Empfohlene Labels:
- `critical`, `bug`, `enhancement`, `performance`
- `documentation`, `testing`, `refactoring`
- `breaking-change`, `security`, `dependencies`
- `P0`, `P1`, `P2`, `P3`

---

**Generiert am:** 2024-10-30
**Analysiertes Projekt:** DateTimeRangeParser v1.1
**Branch:** claude/code-analysis-improvements-011CUdwhvioRAsxXiMRnnsn4

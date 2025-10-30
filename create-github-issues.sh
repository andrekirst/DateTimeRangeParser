#!/bin/bash

# Script zur automatischen Erstellung von GitHub Issues aus der Analyse
# Voraussetzung: GitHub CLI (gh) muss installiert und authentifiziert sein
# Installation: https://cli.github.com/

set -e

echo "🚀 Erstelle GitHub Issues für DateTimeRangeParser Code-Analyse"
echo ""

# Prüfe ob gh CLI verfügbar ist
if ! command -v gh &> /dev/null; then
    echo "❌ GitHub CLI (gh) ist nicht installiert."
    echo "Installation: https://cli.github.com/"
    exit 1
fi

# Prüfe ob im richtigen Verzeichnis
if [ ! -f "GITHUB_ISSUES.md" ]; then
    echo "❌ GITHUB_ISSUES.md nicht gefunden. Bitte im Projekt-Root ausführen."
    exit 1
fi

echo "📋 Erstelle 18 Issues..."
echo ""

# Issue #1: .NET 8.0 Migration
gh issue create \
  --title "Migrate to .NET 8.0 (End-of-Life .NET Core 2.1)" \
  --label "critical,enhancement,security" \
  --body "## Problem
Das Projekt verwendet .NET Core 2.1, das seit August 2021 End-of-Life ist.

**Risiken:**
- ❌ Keine Sicherheitsupdates
- ❌ Fehlende Performance-Verbesserungen
- ❌ Kompatibilitätsprobleme

## Lösung
Migration auf .NET 8.0 (LTS)

\`\`\`xml
<TargetFramework>net8.0</TargetFramework>
\`\`\`

**Dateien:**
- src/DateTimeRangeParser/DateTimeRangeParser.csproj
- src/DateTimeRangeParser.Tests/DateTimeRangeParser.Tests.csproj
- src/DateTimeRangeParser.TestConsole/DateTimeRangeParser.TestConsole.csproj
- appveyor.yml

**Priorität:** P0 - Critical"

echo "✅ Issue #1 erstellt"

# Issue #2: Thread-Safety
gh issue create \
  --title "Fix Thread-Safety Issue in Cache (ConcurrentDictionary)" \
  --label "bug,critical,thread-safety" \
  --body "## Problem
Der Cache ist nicht thread-safe. Bei parallelen Parse-Aufrufen können ArgumentException oder Race Conditions auftreten.

**Datei:** \`DateTimeRangeParser.cs:11\`

## Reproduktion
\`\`\`csharp
var parser = new DateTimeRangeParser();
Parallel.For(0, 100, i => parser.Parse(\"today\")); // Race condition
\`\`\`

## Lösung
\`\`\`csharp
private readonly ConcurrentDictionary<string, DateTimeRange> _cachedValues = new();
\`\`\`

**Priorität:** P0 - Critical"

echo "✅ Issue #2 erstellt"

# Issue #3: TimeSpan Bug
gh issue create \
  --title "Fix Inverted Logic in DateTimeRange.TimeSpan Property" \
  --label "bug,critical" \
  --body "## Problem
Die TimeSpan-Property hat invertierte Logik und liefert immer \`TimeSpan.MinValue\` für valide Ranges.

**Datei:** \`DateTimeRange.cs:97-100\`

\`\`\`csharp
public TimeSpan TimeSpan =>
    Start >= End     // ❌ FALSCH: Sollte <= sein!
    ? End - Start
    : TimeSpan.MinValue;
\`\`\`

## Lösung
\`\`\`csharp
public TimeSpan TimeSpan =>
    Start <= End
    ? End - Start
    : TimeSpan.Zero;
\`\`\`

**Priorität:** P0 - Critical"

echo "✅ Issue #3 erstellt"

# Issue #4: Regex Performance
gh issue create \
  --title "Improve Regex Performance with Static Compilation" \
  --label "enhancement,performance" \
  --body "## Problem
Regex wird bei jedem Aufruf neu kompiliert.

**Datei:** \`NumberRangeTypeCalculator.cs:52-55\`

## Lösung
\`\`\`csharp
private static readonly Regex PatternRegex = new(Pattern, RegexOptions.Compiled);

public override bool DoesMatchInput(string input)
{
    return PatternRegex.IsMatch(input);
}
\`\`\`

**Performance-Gewinn:** 2-5x schneller

**Priorität:** P1 - High"

echo "✅ Issue #4 erstellt"

# Issue #5: Input Validation
gh issue create \
  --title "Add Input Validation for Parse Method" \
  --label "enhancement,robustness" \
  --body "## Problem
Parse-Methode validiert nicht auf null oder leere Strings.

**Datei:** \`DateTimeRangeParser.cs:50\`

## Lösung
\`\`\`csharp
public DateTimeRange Parse(string input)
{
    if (string.IsNullOrWhiteSpace(input))
        return DateTimeRange.Empty;
    // ...
}
\`\`\`

**Priorität:** P1 - High"

echo "✅ Issue #5 erstellt"

# Issue #6: DynamicRangeCalculator
gh issue create \
  --title "Fix Null-Check Logic in DynamicRangeCalculator" \
  --label "bug,code-quality" \
  --body "## Problem
Null-Check für String.Split ist nutzlos, da Split() nie null zurückgibt.

**Datei:** \`DynamicRangeCalculator.cs:40-44\`

\`\`\`csharp
string[] splitBySeperator = input.Split(separator: Separator);
if (splitBySeperator != null && splitBySeperator.Length != 2) // null-Check nutzlos
\`\`\`

## Lösung
\`\`\`csharp
if (string.IsNullOrEmpty(input))
    return false;

string[] splitBySeperator = input.Split(Separator);
if (splitBySeperator.Length != 2)
    return false;
\`\`\`

**Priorität:** P1 - High"

echo "✅ Issue #6 erstellt"

# Issue #7: Immutability
gh issue create \
  --title "Make DateTimeRange Immutable (readonly properties)" \
  --label "enhancement,breaking-change,api-design" \
  --body "## Problem
Start und End haben public setter, was DateTimeRange mutierbar macht.

**Datei:** \`DateTimeRange.cs:93-95\`

## Lösung
\`\`\`csharp
public DateTime Start { get; init; }
public DateTime End { get; init; }
\`\`\`

⚠️ **Breaking Change** für v2.0.0

**Priorität:** P2 - Medium"

echo "✅ Issue #7 erstellt"

# Issue #8: Named Parameters
gh issue create \
  --title "Remove Redundant Named Parameters" \
  --label "code-quality,refactoring" \
  --body "## Problem
Named Parameters werden auch bei eindeutigen Parametern verwendet.

## Beispiele
\`\`\`csharp
_calculators.AddRange(collection: calculators); // ❌ Unnötig
current = current.AddDays(value: 1); // ❌ Unnötig
\`\`\`

**Empfehlung:** Nur bei optionalen/bool/mehrdeutigen Parametern verwenden

**Priorität:** P3 - Low"

echo "✅ Issue #8 erstellt"

# Issue #9: Nullable Reference Types
gh issue create \
  --title "Enable Nullable Reference Types" \
  --label "enhancement,type-safety" \
  --body "## Problem
Nullable Reference Types sind nicht aktiviert.

## Lösung
\`\`\`xml
<PropertyGroup>
  <Nullable>enable</Nullable>
</PropertyGroup>
\`\`\`

**Benefits:**
- Compile-time Null-Safety
- Bessere IDE-Unterstützung
- Weniger Runtime-Exceptions

**Priorität:** P2 - Medium"

echo "✅ Issue #9 erstellt"

# Issue #10: Modern C# Syntax
gh issue create \
  --title "Modernize C# Syntax (Pattern Matching, File-scoped Namespaces)" \
  --label "refactoring,code-quality" \
  --body "## Verbesserungen

**1. Pattern Matching:**
\`\`\`csharp
// Aktuell
DateTimeRange other = obj as DateTimeRange;
return Start == other?.Start && End == other.End;

// Modern
return obj is DateTimeRange other && Start == other.Start && End == other.End;
\`\`\`

**2. File-scoped Namespaces (C# 10):**
\`\`\`csharp
namespace DateTimeRangeParser;
public class DateTimeRange { }
\`\`\`

**Priorität:** P3 - Low"

echo "✅ Issue #10 erstellt"

# Issue #11: Cache Management
gh issue create \
  --title "Add Cache Management Methods" \
  --label "enhancement,api" \
  --body "## Problem
Keine Möglichkeit, Cache zu leeren oder abzufragen.

## Lösung
\`\`\`csharp
public void ClearCache() => _cachedValues.Clear();
public int CacheCount => _cachedValues.Count;
public bool IsCached(string input) => _cachedValues.ContainsKey(input);
\`\`\`

**Use Cases:**
- Memory Management
- Testing
- Diagnostics

**Priorität:** P2 - Medium"

echo "✅ Issue #11 erstellt"

# Issue #12: Empty Semantics
gh issue create \
  --title "Improve DateTimeRange.Empty Semantics" \
  --label "enhancement,api-design" \
  --body "## Problem
DateTimeRange.Empty verwendet DateTime.Min/MaxValue, was nicht intuitiv ist.

**Datei:** \`DateTimeRange.cs:83-89\`

\`\`\`csharp
public static DateTimeRange Empty =>
    new DateTimeRange(
        start: DateTime.MinValue,
        end: DateTime.MaxValue) // ❌ Nicht wirklich \"empty\"
    { IsValid = false };
\`\`\`

## Lösung
\`\`\`csharp
public static DateTimeRange Empty => new()
{
    Start = DateTime.MinValue,
    End = DateTime.MinValue,  // Konsistent
    IsValid = false
};
\`\`\`

**Priorität:** P3 - Low"

echo "✅ Issue #12 erstellt"

# Issue #13: XML Documentation
gh issue create \
  --title "Add XML Documentation for Public API" \
  --label "documentation,enhancement" \
  --body "## Problem
Die öffentliche API hat keine XML-Dokumentation.

## Lösung
\`\`\`csharp
/// <summary>
/// Parses a string input into a <see cref=\"DateTimeRange\"/>.
/// </summary>
/// <param name=\"input\">Input string (e.g., \"today\", \"1d\", \"thisweek\").</param>
/// <returns>The calculated date range or <see cref=\"DateTimeRange.Empty\"/>.</returns>
public DateTimeRange Parse(string input)
\`\`\`

**Aktivierung:**
\`\`\`xml
<GenerateDocumentationFile>true</GenerateDocumentationFile>
\`\`\`

**Benefits:**
- IntelliSense
- API-Dokumentation
- Bessere Developer Experience

**Priorität:** P2 - Medium"

echo "✅ Issue #13 erstellt"

# Issue #14: README
gh issue create \
  --title "Expand README with Usage Examples and Documentation" \
  --label "documentation,enhancement" \
  --body "## Problem
Das README enthält nur eine Zeile.

## Benötigte Inhalte
- 📦 Installation (NuGet)
- 🚀 Quick Start Beispiele
- 📋 Unterstützte Formate (Tabelle)
- 🔧 Advanced Usage
- 📚 API-Übersicht
- 📄 Lizenz

Siehe \`GITHUB_ISSUES.md\` für vollständiges Beispiel-README.

**Priorität:** P1 - High"

echo "✅ Issue #14 erstellt"

# Issue #15: Test Coverage
gh issue create \
  --title "Improve Test Coverage and Add Edge Case Tests" \
  --label "testing,enhancement" \
  --body "## Fehlende Tests

**1. Null/Empty Input:**
\`\`\`csharp
[Theory]
[InlineData(null)]
[InlineData(\"\")]
[InlineData(\"   \")]
public void Parse_InvalidInput_ReturnsEmpty(string input)
\`\`\`

**2. Thread-Safety:**
\`\`\`csharp
[Fact]
public void Parse_ParallelCalls_NoExceptions()
{
    var parser = new DateTimeRangeParser();
    Parallel.For(0, 1000, i => parser.Parse(\"today\"));
}
\`\`\`

**3. Cache Tests**
**4. TimeSpan Edge Cases**
**5. Konsistenz:** Nur Shouldly statt Assert mischen

**Priorität:** P2 - Medium"

echo "✅ Issue #15 erstellt"

# Issue #16: Reflection Performance
gh issue create \
  --title "Optimize CalculationsLoader Reflection Performance" \
  --label "performance,enhancement,architecture" \
  --body "## Problem
CalculationsLoader scannt ALLE Assemblies im AppDomain.

**Datei:** \`CalculationsLoader.cs:14-22\`

\`\`\`csharp
return AppDomain.CurrentDomain.GetAssemblies() // ❌ ALLE Assemblies!
\`\`\`

## Lösung
\`\`\`csharp
return calculatorBaseType.Assembly  // ✅ Nur eigenes Assembly
    .GetTypes()
    // ...
\`\`\`

**Performance-Gewinn:** 10-100x schneller Startup

**Priorität:** P2 - Medium"

echo "✅ Issue #16 erstellt"

# Issue #17: Operator Overloads
gh issue create \
  --title "Review Operator Overloads (++ and --) for Clarity" \
  --label "api-design,usability,discussion" \
  --body "## Problem
Die ++ und -- Operatoren sind unintuitiv.

**Datei:** \`DateTimeRange.cs:163-171\`

\`\`\`csharp
public static DateTimeRange operator ++(DateTimeRange dateTimeRange)
{
    return dateTimeRange.SpreadByDays(days: 1); // ❓ Unklar
}
\`\`\`

## Diskussion
- **Option A:** Entfernen (Breaking Change)
- **Option B:** Semantik ändern (Shift statt Spread)
- **Option C:** Dokumentieren und beibehalten

**Empfehlung:** Option A

**Priorität:** P3 - Low"

echo "✅ Issue #17 erstellt"

# Issue #18: NuGet Updates
gh issue create \
  --title "Update NuGet Dependencies" \
  --label "dependencies,security,maintenance" \
  --body "## Veraltete Pakete

\`\`\`xml
<PackageReference Include=\"Moq\" Version=\"4.10.1\" />      <!-- 2019 -->
<PackageReference Include=\"Shouldly\" Version=\"3.0.2\" />  <!-- 2019 -->
<PackageReference Include=\"xunit\" Version=\"2.4.1\" />     <!-- 2019 -->
\`\`\`

## Updates (2024)
\`\`\`xml
<PackageReference Include=\"Moq\" Version=\"4.20.70\" />
<PackageReference Include=\"Shouldly\" Version=\"4.2.1\" />
<PackageReference Include=\"xunit\" Version=\"2.6.6\" />
\`\`\`

**Entfernen:**
\`\`\`xml
<DotNetCliToolReference Include=\"dotnet-xunit\" Version=\"2.3.1\" /> <!-- obsolet -->
\`\`\`

**Benefits:**
- Sicherheitsupdates
- Bug-Fixes
- .NET 8 Kompatibilität

**Priorität:** P2 - Medium"

echo "✅ Issue #18 erstellt"

echo ""
echo "🎉 Alle 18 Issues erfolgreich erstellt!"
echo ""
echo "📊 Zusammenfassung:"
echo "   - P0 Critical: 3 Issues (#1, #2, #3)"
echo "   - P1 High: 4 Issues (#4, #5, #6, #14)"
echo "   - P2 Medium: 7 Issues (#7, #9, #11, #13, #15, #16, #18)"
echo "   - P3 Low: 4 Issues (#8, #10, #12, #17)"
echo ""
echo "🔗 Issues anzeigen: gh issue list"

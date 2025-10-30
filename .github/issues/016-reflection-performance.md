# Issue #16: Optimize CalculationsLoader Reflection Performance

**Labels:** `performance`, `enhancement`, `architecture`
**Priority:** P2 - Medium
**Category:** 🔧 ARCHITEKTUR

---

## Beschreibung

`CalculationsLoader` verwendet Reflection über alle geladenen Assemblies, was langsam und unsicher ist.

## Problematischer Code

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

## Probleme

- Scannt ALLE Assemblies im AppDomain (inkl. System, Microsoft, etc.)
- Langsam bei großen Anwendungen
- Findet möglicherweise unerwünschte Implementierungen in Drittanbieter-Assemblies

## Lösung - Option A (Scope auf eigenes Assembly)

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

## Lösung - Option B (.NET 7+ Source Generators)

Verwende Source Generators zur Compile-Zeit-Discovery statt Runtime-Reflection.

## Performance-Gewinn

- 10-100x schneller Startup
- Keine unerwünschten Treffer
- Deterministisch

## Betroffene Dateien

- `src/DateTimeRangeParser/CalculationsLoader.cs`

## Geschätzter Aufwand

2-3 Stunden

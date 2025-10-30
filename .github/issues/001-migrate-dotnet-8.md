# Issue #1: Migrate to .NET 8.0 (End-of-Life .NET Core 2.1)

**Labels:** `critical`, `enhancement`, `security`
**Priority:** P0 - Critical
**Category:** 🔴 KRITISCHE PROBLEME

---

## Beschreibung

Das Projekt verwendet .NET Core 2.1, das seit **August 2021 End-of-Life** ist und keine Sicherheitsupdates mehr erhält.

## Probleme

- ❌ Keine Sicherheitsupdates
- ❌ Fehlende Performance-Verbesserungen
- ❌ Kompatibilitätsprobleme mit modernen Tools
- ❌ NuGet-Pakete werden möglicherweise nicht mehr unterstützt

## Lösung

Migration auf .NET 8.0 (LTS bis November 2026) oder .NET 6.0 (LTS bis November 2024)

**DateTimeRangeParser.csproj:**
```xml
<PropertyGroup>
  <TargetFramework>net8.0</TargetFramework>
  <!-- Für Multi-Targeting -->
  <!-- <TargetFrameworks>net6.0;net8.0</TargetFrameworks> -->
</PropertyGroup>
```

## Betroffene Dateien

- `src/DateTimeRangeParser/DateTimeRangeParser.csproj`
- `src/DateTimeRangeParser.Tests/DateTimeRangeParser.Tests.csproj`
- `src/DateTimeRangeParser.TestConsole/DateTimeRangeParser.TestConsole.csproj`
- `appveyor.yml`

## Testing

- Alle Unit-Tests müssen nach Migration erfolgreich sein
- Kompatibilität mit bestehenden Consumers prüfen

## Geschätzter Aufwand

2-4 Stunden

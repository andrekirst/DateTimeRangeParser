# Issue #18: Update NuGet Dependencies

**Labels:** `dependencies`, `security`, `maintenance`
**Priority:** P2 - Medium
**Category:** 📦 DEPENDENCIES

---

## Beschreibung

Test-Dependencies sind veraltet (teilweise 5+ Jahre alt).

## Aktuelle Versionen

**Datei:** `DateTimeRangeParser.Tests.csproj`

```xml
<PackageReference Include="Moq" Version="4.10.1" />      <!-- 2019 -->
<PackageReference Include="Shouldly" Version="3.0.2" />  <!-- 2019 -->
<PackageReference Include="xunit" Version="2.4.1" />     <!-- 2019 -->
```

## Empfohlene Updates (Stand 2024)

```xml
<PackageReference Include="Moq" Version="4.20.70" />
<PackageReference Include="Shouldly" Version="4.2.1" />
<PackageReference Include="xunit" Version="2.6.6" />
<PackageReference Include="xunit.runner.visualstudio" Version="2.5.6">
  <PrivateAssets>all</PrivateAssets>
  <IncludeAssets>runtime; build; native; contentfiles; analyzers</IncludeAssets>
</PackageReference>
```

## Veraltete Tool-Reference entfernen

```xml
<!-- ❌ Entfernen - nicht mehr benötigt -->
<DotNetCliToolReference Include="dotnet-xunit" Version="2.3.1" />
```

## Benefits

- Sicherheitsupdates
- Bug-Fixes
- Performance-Verbesserungen
- Kompatibilität mit .NET 8

## Testing

Alle Unit-Tests müssen nach Update erfolgreich sein.

## Betroffene Dateien

- `src/DateTimeRangeParser.Tests/DateTimeRangeParser.Tests.csproj`

## Geschätzter Aufwand

1-2 Stunden

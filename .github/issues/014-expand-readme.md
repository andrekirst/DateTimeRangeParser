# Issue #14: Expand README with Usage Examples and Documentation

**Labels:** `documentation`, `enhancement`
**Priority:** P1 - High
**Category:** 🟡 WICHTIGE VERBESSERUNGEN

---

## Beschreibung

Das README enthält nur eine Zeile und ist nicht hilfreich für neue Benutzer.

## Aktuelles README

```markdown
# DateTimeRangeParser

An API for parsing DateTime-Ranges from an input string
```

## Benötigte Inhalte

### 1. Badges und Metadaten
- NuGet Version Badge
- Build Status
- License Badge

### 2. Features-Übersicht
- Natürliche Sprache ("today", "yesterday")
- Relative Daten ("1d", "-7d")
- Kalenderwochen ("CW13.2024")
- Dynamische Ranges ("yesterday->today")

### 3. Installation
```bash
dotnet add package DateTimeRangeParser
```

### 4. Quick Start
```csharp
var parser = new DateTimeRangeParser();
var today = parser.Parse("today");
```

### 5. Unterstützte Formate (Tabelle)

| Input | Description | Example Result |
|-------|-------------|----------------|
| `today` | Current day | 2024-10-30 to 2024-10-30 |
| `yesterday` | Previous day | 2024-10-29 to 2024-10-29 |
| `1d` | Tomorrow | 2024-10-31 to 2024-10-31 |
| `-7d` | 7 days ago | 2024-10-23 to 2024-10-23 |
| `currentweek` | Monday-Sunday | 2024-10-28 to 2024-11-03 |
| `thismonth` | Current month | 2024-10-01 to 2024-10-31 |
| `CW13.2024` | Calendar week | 2024-03-25 to 2024-03-31 |

### 6. Advanced Usage
- Caching Control
- Custom DateTime Provider
- DateTimeRange Operations
- Event Handling

### 7. Requirements
- .NET Version

### 8. Contributing, License, Authors

## Referenz

Siehe `GITHUB_ISSUES.md` für vollständiges Beispiel-README (Zeilen 1000-1200).

## Betroffene Dateien

- `README.md`
- `CHANGELOG.md` (neu erstellen)
- `CONTRIBUTING.md` (optional)

## Geschätzter Aufwand

3-4 Stunden

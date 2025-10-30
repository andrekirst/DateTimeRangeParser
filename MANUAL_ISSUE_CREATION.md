# Manuelle Erstellung der GitHub Issues

Falls Sie die Issues lieber manuell erstellen möchten, folgen Sie dieser Anleitung.

## Methode 1: Automatisches Script (Empfohlen)

Wenn Sie GitHub CLI (`gh`) installiert haben:

```bash
# GitHub CLI installieren (falls noch nicht vorhanden)
# macOS
brew install gh

# Windows
winget install GitHub.cli

# Linux
# Siehe: https://github.com/cli/cli/blob/trunk/docs/install_linux.md

# Authentifizieren
gh auth login

# Issues erstellen
./create-github-issues.sh
```

## Methode 2: Manuelle Erstellung über GitHub Web UI

### Schritt-für-Schritt Anleitung

1. Gehen Sie zu: https://github.com/andrekirst/DateTimeRangeParser/issues/new
2. Kopieren Sie die Issue-Daten aus `GITHUB_ISSUES.md`
3. Erstellen Sie jedes Issue einzeln mit den entsprechenden Labels

### Quick Links für Labels

Erstellen Sie zunächst diese Labels (falls nicht vorhanden):

**Settings → Labels → New label**

| Label | Farbe | Beschreibung |
|-------|-------|--------------|
| `critical` | #d73a4a (rot) | Kritische Probleme, die sofort behoben werden müssen |
| `bug` | #d73a4a (rot) | Etwas funktioniert nicht wie erwartet |
| `enhancement` | #a2eeef (blau) | Neue Features oder Verbesserungen |
| `performance` | #fbca04 (gelb) | Performance-bezogene Verbesserungen |
| `security` | #d73a4a (rot) | Sicherheitsrelevante Themen |
| `documentation` | #0075ca (blau) | Dokumentations-Verbesserungen |
| `testing` | #1d76db (blau) | Test-bezogene Themen |
| `refactoring` | #cfd3d7 (grau) | Code-Umstrukturierung ohne Funktionsänderung |
| `breaking-change` | #e99695 (rosa) | Änderungen, die bestehenden Code brechen |
| `thread-safety` | #d93f0b (orange) | Thread-Safety-Probleme |
| `code-quality` | #0e8a16 (grün) | Code-Qualitätsverbesserungen |
| `api-design` | #bfd4f2 (hellblau) | API-Design-Entscheidungen |
| `robustness` | #0e8a16 (grün) | Erhöhung der Robustheit |
| `type-safety` | #1d76db (blau) | Type-Safety-Verbesserungen |
| `usability` | #c5def5 (hellblau) | Usability-Verbesserungen |
| `discussion` | #cc317c (lila) | Diskussionsbedarf |
| `dependencies` | #0366d6 (blau) | Dependency-Updates |
| `maintenance` | #fbca04 (gelb) | Wartungsarbeiten |
| `architecture` | #1d76db (blau) | Architektur-Entscheidungen |

### Issue-Template (Beispiel für Issue #1)

```markdown
**Titel:** Migrate to .NET 8.0 (End-of-Life .NET Core 2.1)

**Labels:** critical, enhancement, security

**Beschreibung:**

## Problem
Das Projekt verwendet .NET Core 2.1, das seit August 2021 End-of-Life ist und keine Sicherheitsupdates mehr erhält.

**Risiken:**
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
- src/DateTimeRangeParser/DateTimeRangeParser.csproj
- src/DateTimeRangeParser.Tests/DateTimeRangeParser.Tests.csproj
- src/DateTimeRangeParser.TestConsole/DateTimeRangeParser.TestConsole.csproj
- appveyor.yml

## Testing
- Alle Unit-Tests müssen nach Migration erfolgreich sein
- Kompatibilität mit bestehenden Consumers prüfen

**Priorität:** P0 - Critical
```

Wiederholen Sie dies für alle 18 Issues (siehe `GITHUB_ISSUES.md` für vollständige Beschreibungen).

## Methode 3: GitHub Issues Import API

Für fortgeschrittene Benutzer mit API-Zugriff:

```bash
# Beispiel mit curl (benötigt GitHub Personal Access Token)
TOKEN="your_github_token"
REPO="andrekirst/DateTimeRangeParser"

curl -X POST \
  -H "Accept: application/vnd.github.v3+json" \
  -H "Authorization: token $TOKEN" \
  https://api.github.com/repos/$REPO/issues \
  -d '{
    "title": "Migrate to .NET 8.0 (End-of-Life .NET Core 2.1)",
    "body": "...",
    "labels": ["critical", "enhancement", "security"]
  }'
```

## Methode 4: GitHub Project Import

Sie können die Issues auch in ein GitHub Project Board importieren:

1. Erstellen Sie ein neues Project: https://github.com/andrekirst/DateTimeRangeParser/projects/new
2. Wählen Sie "Board" Template
3. Erstellen Sie Columns: "To Do", "In Progress", "Done"
4. Fügen Sie die Issues nach Priorität hinzu:
   - **P0 Critical** → Oben in "To Do"
   - **P1 High** → Mittlere Priorität
   - **P2 Medium** → Normale Priorität
   - **P3 Low** → Niedrige Priorität

## Zusammenfassung der 18 Issues

| # | Titel | Priorität | Labels |
|---|-------|-----------|--------|
| 1 | Migrate to .NET 8.0 | P0 | critical, enhancement, security |
| 2 | Fix Thread-Safety Issue | P0 | bug, critical, thread-safety |
| 3 | Fix TimeSpan Bug | P0 | bug, critical |
| 4 | Improve Regex Performance | P1 | enhancement, performance |
| 5 | Add Input Validation | P1 | enhancement, robustness |
| 6 | Fix DynamicRangeCalculator | P1 | bug, code-quality |
| 7 | Make DateTimeRange Immutable | P2 | enhancement, breaking-change, api-design |
| 8 | Remove Redundant Named Parameters | P3 | code-quality, refactoring |
| 9 | Enable Nullable Reference Types | P2 | enhancement, type-safety |
| 10 | Modernize C# Syntax | P3 | refactoring, code-quality |
| 11 | Add Cache Management | P2 | enhancement, api |
| 12 | Improve Empty Semantics | P3 | enhancement, api-design |
| 13 | Add XML Documentation | P2 | documentation, enhancement |
| 14 | Expand README | P1 | documentation, enhancement |
| 15 | Improve Test Coverage | P2 | testing, enhancement |
| 16 | Optimize Reflection | P2 | performance, enhancement, architecture |
| 17 | Review Operator Overloads | P3 | api-design, usability, discussion |
| 18 | Update NuGet Dependencies | P2 | dependencies, security, maintenance |

## Empfohlene Reihenfolge

**Sofort (P0 - Critical):**
1. Issue #1: .NET 8.0 Migration
2. Issue #2: Thread-Safety
3. Issue #3: TimeSpan Bug

**Kurzfristig (P1 - High):**
4. Issue #14: README erweitern
5. Issue #4: Regex Performance
6. Issue #5: Input Validation
7. Issue #6: DynamicRangeCalculator Fix

**Mittelfristig (P2 - Medium):**
8. Issue #9: Nullable Reference Types
9. Issue #13: XML Documentation
10. Issue #11: Cache Management
11. Issue #18: NuGet Updates
12. Issue #15: Test Coverage
13. Issue #16: Reflection Performance
14. Issue #7: Immutability (Breaking Change für v2.0)

**Langfristig (P3 - Low):**
15. Issue #10: Modern C# Syntax
16. Issue #8: Named Parameters
17. Issue #12: Empty Semantics
18. Issue #17: Operator Overloads

---

**Tipp:** Erstellen Sie einen Milestone "Code Quality Improvements" und fügen Sie alle Issues hinzu:
https://github.com/andrekirst/DateTimeRangeParser/milestones/new

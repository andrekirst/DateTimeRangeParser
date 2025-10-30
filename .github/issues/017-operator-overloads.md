# Issue #17: Review Operator Overloads (++ and --) for Clarity

**Labels:** `api-design`, `usability`, `discussion`
**Priority:** P3 - Low
**Category:** 🔧 ARCHITEKTUR

---

## Beschreibung

Die `++` und `--` Operatoren für `DateTimeRange` sind unintuitiv und schlecht dokumentiert.

## Aktueller Code

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

## Problem: Unklare Semantik

```csharp
var range = parser.Parse("today"); // 2024-10-30 to 2024-10-30
range++; // Was passiert?
         // Erwartung: 2024-10-31 to 2024-10-31 (nächster Tag)?
         // Tatsächlich: 2024-10-29 to 2024-10-31 (spread!)
```

Der Name `SpreadByDays` deutet auf Expansion hin, nicht auf Verschiebung!

## Diskussionspunkte

1. **Option A:** Operatoren entfernen (Breaking Change)
2. **Option B:** Semantik ändern auf Shift statt Spread
3. **Option C:** Nur dokumentieren und beibehalten

## Empfehlung

Option A: Entfernen, da:
- Semantik unklar
- Wenig Nutzen (explizite Methoden sind klarer)
- Potenzielle Quelle von Bugs

Wenn beibehalten: Ausführliche XML-Dokumentation hinzufügen!

## Betroffene Dateien

- `src/DateTimeRangeParser/DateTimeRange.cs`

## Geschätzter Aufwand

2-3 Stunden (inkl. Diskussion)

# Issue #15: Improve Test Coverage and Add Edge Case Tests

**Labels:** `testing`, `enhancement`
**Priority:** P2 - Medium
**Category:** 🧪 TESTS

---

## Beschreibung

Die Tests sind gut, aber es fehlen Tests für Edge Cases und Thread-Safety.

## Fehlende Test-Kategorien

### 1. Null/Empty Input Tests

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

### 2. Thread-Safety Tests

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

### 3. TimeSpan Edge Cases

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

### 4. Cache Tests

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

### 5. Consistency: Assert vs Shouldly

Problem: Tests mischen `Assert` und `Shouldly`.

Empfehlung: Einheitlich `Shouldly` verwenden:
```csharp
// Statt
Assert.Equal(expected, actual);

// Verwende
actual.ShouldBe(expected);
```

## Betroffene Dateien

- `src/DateTimeRangeParser.Tests/` (neue Tests hinzufügen)

## Geschätzter Aufwand

4-6 Stunden

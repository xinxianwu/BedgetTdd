using FluentAssertions;

namespace BudgetTdd;

[TestFixture]
public class DateTimeTests
{
    [Test]
    public void overlay_days_same_month()
    {
        var start = new DateTime(2023, 11, 01);
        var end = new DateTime(2023, 11, 15);
        var overlayDays = new DateRange(2023, 11).CalculateOverlayDays(start, end);

        overlayDays.Should().Be(15);
    }

    [Test]
    public void overlay_days_cross_month1()
    {
        var start = new DateTime(2023, 11, 29);
        var end = new DateTime(2023, 12, 02);
        var overlayDays = new DateRange(2023, 11).CalculateOverlayDays(start, end);

        overlayDays.Should().Be(2);
    }

    [Test]
    public void overlay_days_cross_month2()
    {
        var start = new DateTime(2023, 11, 29);
        var end = new DateTime(2023, 12, 03);
        var overlayDays = new DateRange(2023, 12).CalculateOverlayDays(start, end);

        overlayDays.Should().Be(3);
    }
}

public class DateRange
{
    private readonly int _month;
    private readonly int _year;

    public DateRange(int year, int month)
    {
        _year = year;
        _month = month;
    }

    public DateRange(string yearMonthString)
    {
        _year = int.Parse(yearMonthString[..4]);
        _month= int.Parse(yearMonthString[4..]);
    }

    public int CalculateOverlayDays(DateTime start2, DateTime end2)
    {
        var rangeStart = new DateTime(_year, _month, 1);
        var rangeEnd = new DateTime(_year, _month, DateTime.DaysInMonth(_year, _month));

        // 如果其中一個範圍在另一個範圍之前或之後，則沒有重疊
        if (rangeEnd < start2 || end2 < rangeStart)
        {
            return 0;
        }

        // 計算重疊的開始日期和結束日期
        var overlapStart = rangeStart > start2 ? rangeStart : start2;
        var overlapEnd = rangeEnd < end2 ? rangeEnd : end2;

        // 計算重疊的天數

        return (overlapEnd - overlapStart).Days + 1;
    }
}
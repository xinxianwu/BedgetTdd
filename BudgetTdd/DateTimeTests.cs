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
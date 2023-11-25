using BudgetTdd.Services;

namespace BudgetTdd.DataModels;

public class BudgetDateModel
{
    public string YearMonth { get; set; }
    public decimal Amount { get; set; }

    public YearMonth BudgetYearMonth => new(YearMonth);

    public decimal CalculateBudget(DateTime start, DateTime end)
    {
        var intervalDays = new DateRange(YearMonth).CalculateOverlayDays(start, end);
        var currentMonthDays = DateTime.DaysInMonth(int.Parse(YearMonth[..4]), int.Parse(YearMonth[4..]));

        return Amount / currentMonthDays * intervalDays;
    }
}
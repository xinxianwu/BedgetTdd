using BudgetTdd.Services;

namespace BudgetTdd.Extensions;

public static class YearMonthExtensions
{
    public static YearMonth AsYearMonth(this DateTime start)
    {
        var yearMonth = new YearMonth(start.ToString("yyyyMM"));
        return yearMonth;
    }
}
namespace BudgetTdd.Services;

public class YearMonth
{
    private readonly string _yearMonth;

    public YearMonth(string yearMonth)
    {
        _yearMonth = yearMonth;
    }

    public static bool operator >=(YearMonth left, YearMonth right)
    {
        return int.Parse(left._yearMonth) - int.Parse(right._yearMonth) >= 0;
    }

    public static bool operator <=(YearMonth left, YearMonth right)
    {
        return int.Parse(left._yearMonth) - int.Parse(right._yearMonth) <= 0;
    }
}
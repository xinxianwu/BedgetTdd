namespace BudgetTdd;

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
        _month = int.Parse(yearMonthString[4..]);
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
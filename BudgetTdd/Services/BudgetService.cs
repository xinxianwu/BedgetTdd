using BudgetTdd.Repo;

namespace BudgetTdd.Services;

public class BudgetService
{
    private readonly IBudgetRepo _budgetRepo;

    public BudgetService(IBudgetRepo budgetRepo)
    {
        _budgetRepo = budgetRepo;
    }

    public decimal Query(DateTime start, DateTime end)
    {
        return _budgetRepo.GetAll()
            .Where(x =>
            {
                var budgetYearMonth = new YearMonth(x.YearMonth);
                var startYearMonth = new YearMonth(start.ToString("yyyyMM"));
                var endYearMonth = new YearMonth(end.ToString("yyyyMM"));

                return budgetYearMonth >= startYearMonth && budgetYearMonth <= endYearMonth;
            })
            .Sum(x =>
            {
                var intervalDays = new DateRange(x.YearMonth).CalculateOverlayDays(start, end);
                var currentMonthDays = DateTime.DaysInMonth(int.Parse(x.YearMonth[..4]), int.Parse(x.YearMonth[4..]));

                return x.Amount / currentMonthDays * intervalDays;
            });
    }
}
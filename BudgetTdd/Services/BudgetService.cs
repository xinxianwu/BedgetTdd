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
        return _budgetRepo.GetAll().Sum(x => x.Amount);
    }
}
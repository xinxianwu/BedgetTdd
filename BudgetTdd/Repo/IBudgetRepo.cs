using BudgetTdd.DataModels;

namespace BudgetTdd.Repo;

public interface IBudgetRepo
{
    List<BudgetDateModel> GetAll();
}
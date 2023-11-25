using BudgetTdd.DataModels;
using BudgetTdd.Repo;
using BudgetTdd.Services;
using FluentAssertions;
using NSubstitute;

namespace BudgetTdd;

public class Tests
{
    private BudgetService _budgetService;
    private IBudgetRepo _budgetRepo;

    [SetUp]
    public void SetUp()
    {
        _budgetRepo = Substitute.For<IBudgetRepo>();
        _budgetService = new BudgetService(_budgetRepo);
    }

    [Test]
    public void QueryMonthly()
    {
        GivenBudgets(new List<BudgetDateModel>
        {
            new() { YearMonth = "202310", Amount = 31 },
            new() { YearMonth = "202311", Amount = 90 }
        });
        var budget = _budgetService.Query(new DateTime(2023, 11, 01), new DateTime(2023, 11, 30));

        BudgetShouldBe(budget, 90);
    }

    [Test]
    public void QueryDaily()
    {
        GivenBudgets(new List<BudgetDateModel>
        {
            new() { YearMonth = "202310", Amount = 31 },
            new() { YearMonth = "202311", Amount = 60 }
        });
        var budget = _budgetService.Query(new DateTime(2023, 10, 29), new DateTime(2023, 11, 02));

        BudgetShouldBe(budget, 7);
    }

    private static void BudgetShouldBe(decimal budget, int expected)
    {
        budget.Should().Be(expected);
    }

    private void GivenBudgets(List<BudgetDateModel> budgetDateModels)
    {
        _budgetRepo.GetAll().Returns(budgetDateModels);
    }
}
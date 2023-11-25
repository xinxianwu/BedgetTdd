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
            new() { YearMonth = "202310", Amount = 30 },
            new() { YearMonth = "202311", Amount = 40 }
        });
        var budget = _budgetService.Query(new DateTime(2023, 11, 01), new DateTime(2023, 11, 30));

        BudgetShouldBe(budget, 40);
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
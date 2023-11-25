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
        var budget = _budgetService.Query(new DateTime(2023, 11, 05), new DateTime(2023, 11, 25));

        BudgetShouldBe(budget, 63);
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

    [Test]
    public void QueryBudgetAndOneMonthNotFound()
    {
        GivenBudgets(new List<BudgetDateModel>
        {
            new() { YearMonth = "202309", Amount = 30 },
            new() { YearMonth = "202311", Amount = 90 }
        });
        var budget = _budgetService.Query(new DateTime(2023, 09, 29), new DateTime(2023, 11, 5));

        BudgetShouldBe(budget, 17);
    }

    [Test]
    public void QueryCrossYears()
    {
        GivenBudgets(new List<BudgetDateModel>
        {
            new() { YearMonth = "202310", Amount = 31 },
            new() { YearMonth = "202311", Amount = 30 },
            new() { YearMonth = "202312", Amount = 31 },
            new() { YearMonth = "202401", Amount = 31 }
        });
        var budget = _budgetService.Query(new DateTime(2023, 10, 01), new DateTime(2024, 01, 31));

        BudgetShouldBe(budget, 123);
    }

    [Test]
    public void QueryLeapYear()
    {
        GivenBudgets(new List<BudgetDateModel>
        {
            new() { YearMonth = "202312", Amount = 31 },
            new() { YearMonth = "202401", Amount = 31 },
            new() { YearMonth = "202402", Amount = 29 }
        });
        var budget = _budgetService.Query(new DateTime(2023, 10, 01), new DateTime(2024, 03, 01));

        BudgetShouldBe(budget, 91);
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
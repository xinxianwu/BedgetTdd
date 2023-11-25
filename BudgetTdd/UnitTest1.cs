using BudgetTdd.DataModels;
using BudgetTdd.Repo;
using BudgetTdd.Services;
using FluentAssertions;
using NSubstitute;

namespace BudgetTdd;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void QueryMonthly()
    {
        var budgetRepo = Substitute.For<IBudgetRepo>();
        budgetRepo.GetAll().Returns(new List<BudgetDateModel>
        {
            new() { Amount = 30 }
        });
        var budgetService = new BudgetService(budgetRepo);
        var budget = budgetService.Query(new DateTime(2023, 11, 01), new DateTime(2023, 11, 30));

        budget.Should().Be(30);
    }
}
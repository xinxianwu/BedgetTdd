using BudgetTdd.Services;
using FluentAssertions;

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
        var budgetService = new BudgetService();
        var budget = budgetService.Query(new DateTime(2023, 11, 01), new DateTime(2023, 11, 30));

        budget.Should().Be(30);
    }
}
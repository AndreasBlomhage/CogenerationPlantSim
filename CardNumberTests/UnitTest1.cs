namespace CardNumberTests;

[TestFixture]
public class Tests {
    [SetUp]
    public void Setup () { }

    [Test]
    public void Given_NoPreConditions_When_CallingRandomiseNumber_Then_RandomisedStringIsReturned () {
        var sut = new CardNumberViewModel();

        var string = sut.RandomiseNumber()

        Assert.Pass();
    }
}

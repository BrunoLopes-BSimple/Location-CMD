using Domain.Entities;

namespace Domain.Tests.LocationTests;

public class LocationConstructorTests
{
    [Theory]
    [InlineData("Some Location")]
    [InlineData("https://example.com")]
    public void Location_WhenCreatingLocationWithValidData_ThenCreatesLocation(string description)
    {
        // arrange
        var id = Guid.NewGuid();

        // act

        // assert
        new Location(id, description);
    }

    [Theory]
    [InlineData("Some Location")]
    [InlineData("https://example.com")]
    public void Location_WhenCreatingLocationWithValidDescription_ThenCreatesLocation(string description)
    {
        // assert
        new Location(description);
    }

    [Theory]
    [InlineData("A")]
    [InlineData("This has more than the limit of words for this description")]
    [InlineData("htp://invalid-url")]
    [InlineData("!@#abc")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Location_WhenCreatingLocationWithInvalidDescription_ThenThrowsArgumentException(string description)
    {
        var exception = Assert.Throws<ArgumentException>(() => new Location(description));

        Assert.Equal("Description must be a valid URL or a physical location (4-20 alphanumeric characters).", exception.Message);
    }

}

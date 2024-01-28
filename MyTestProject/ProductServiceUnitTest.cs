using FastEndpoints;

namespace MyTestProject;

public class ProductServiceUnitTest
{
    [Fact]
    public void TestAdd()
    {
        var service = new ProductService();

        var result = service.AddProduct("test name");

        Assert.True(result);
    }


    /// <summary>
    ///     https://fast-endpoints.com/docs/validation#throwing-adding-errors-from-anywhere
    ///     test <see cref="ValidationContext"/>, throw error from anywhere
    /// </summary>
    [Fact]
    public void TestAddWithEmptyName()
    {
        var service = new ProductService();

        Assert.ThrowsAny<ValidationFailureException>(() =>
        {
            service.AddProduct("");
        });
    }
}

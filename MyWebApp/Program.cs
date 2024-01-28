using FastEndpoints;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFastEndpoints()
    .SwaggerDocument();
builder.Services.AddScoped<ProductService>();

var app = builder.Build();
app.UseFastEndpoints()
    .UseSwaggerGen();

app.MapGet("/", () => "Hello World!");

app.Run();


public class Product
{
    public string Name { get; set; }
}

public class ProductService
{
    private readonly List<Product> _products = new();

    public bool AddProduct(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            // throw new ArgumentException("Product name cannot be empty", nameof(name));
            ValidationContext.Instance.ThrowError("Product name cannot be empty");
        }

        var product = new Product { Name = name };

        _products.Add(product);

        return true;
    }
}


public class MyEndPoint: Endpoint<string, string>
{
    private readonly ProductService _productService;

    public MyEndPoint(ProductService productService)
    {
        _productService = productService;
    }

    public override void Configure()
    {
        Verbs(Http.POST);
        Routes("/product");
        AllowAnonymous();
    }

    public override async Task<string> HandleAsync(string request, CancellationToken cancellationToken = default)
    {
         _productService.AddProduct(request);

        // ThrowIfAnyErrors();

        return "Product added";
    }
}

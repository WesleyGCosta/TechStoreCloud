using Api.Models.Requests;

namespace Api.Endpoints
{
    public static class ProductEndpoint
    {
        public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder builder)
        {
            var group = builder.MapGroup("/products").WithTags("Products");

            group.MapGet("", () =>
            {
                return TypedResults.Ok();
            });

            group.MapGet("/{id}", (Guid id) =>
            {
                return TypedResults.Ok();
            });

            group.MapPost("", (CreateProductRequest createProductRequest) =>
            {
                return Results.Ok(createProductRequest);
            });

            group.MapPut("", () =>
            {
                return TypedResults.Ok();
            });


            group.MapDelete("/{id}", (Guid id) =>
            {
                return TypedResults.NoContent();
            });


            return builder;
        }
    }
}

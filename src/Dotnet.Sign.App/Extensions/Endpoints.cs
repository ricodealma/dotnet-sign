using Dotnet.Sign.Domain.Aggregates.Sign.Entities.Requests;
using Dotnet.Sign.Domain.Aggregates.Sign;
using Dotnet.Sign.Domain.SeedWork.ErrorResult;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using Dotnet.Sign.Domain.Aggregates.Sign.Entities.Database;

namespace Dotnet.Sign.App.Extensions
{
    public static class EndpointsExtensions
    {
        public static void AddEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapGet("/health", Health);
            endpointRouteBuilder.MapPost("/v1/contract", PostContract);
            endpointRouteBuilder.MapPost("/v1/contract/{id}/callbacks/signed", PostSignedCallback);
            endpointRouteBuilder.MapGet("/v1/contract/{id}", GetContractById);
        }

        [SwaggerOperation(
             Summary = "Application Health Check Endpoint",
             Description = "This endpoint validates whether the application is working correctly according to internally established standards.",
             OperationId = "Health",
             Tags = ["Health"]
        )]
        public static IResult Health() => Results.Ok();

        [SwaggerOperation(
             Summary = "PostContract - Inserts a new contract into the service provider's contract service.",
             Description = "This endpoint is responsible for utilizing the service provider's contract service to insert a new contract.",
             OperationId = "PostContract",
             Tags = ["Contract"]
        )]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> PostContract([FromBody] ContractRequest contract, [FromServices] ISignService crmService, [FromHeader(Name = "Idempotency-Key")] string idempotencyKey)
        {
            var result = await crmService.InsertContractAsync(contract, idempotencyKey);

            if (result.Item1 is null || result.Item2.Error)
                return GenerateErrorResult(result.Item2);

            return Results.Created($"/v1/contract/single/{result.Item1.Id}", result.Item1);
        }
        [SwaggerOperation(
             Summary = "PostContract - Inserts a new contract into the service provider's contract service.",
             Description = "This endpoint is responsible for utilizing the service provider's contract service to insert a new contract.",
             OperationId = "PostContract",
             Tags = ["Contract"]
        )]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> PostSignedCallback([FromRoute] Guid id, [FromServices] ISignService crmService)
        {
            Tuple<ContractModel?, ErrorResult> result = await crmService.PostContractSigned(id);

            if (result.Item1 is null || result.Item2.Error)
                return GenerateErrorResult(result.Item2);

            return Results.Created($"/v1/contract/single/{result.Item1.Id}", result.Item1);
        }

        [SwaggerOperation(
             Summary = "GetContractById - Retrieves a list of contracts filtered by the specified parameters.",
             Description = "This endpoint utilizes the service provider's contract service to select a list of contracts, filtering them based on the parameters provided.",
             OperationId = "GetContractById",
             Tags = ["Contract"]
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> GetContractById([FromServices] ISignService crmService, [FromRoute] Guid id)
        {
            var result = await crmService.SelectContractByIdAsync(id);

            if (result.Item2 is not null && result.Item2.Error)
                return GenerateErrorResult(result.Item2);

            return Results.Ok(result.Item1);
        }

        private static IResult GenerateErrorResult(ErrorResult errorResult) => errorResult.StatusCode switch
        {
            ErrorCode.Undefined => Results.Problem(
                detail: JsonConvert.SerializeObject(errorResult),
                statusCode: 500
            ),
            ErrorCode.NotFound => Results.NotFound(errorResult),
            ErrorCode.BadRequest => Results.BadRequest(errorResult),
            ErrorCode.Unauthorized => Results.Unauthorized(),
            ErrorCode.Forbidden => Results.Forbid(null),
            ErrorCode.InternalServerError => Results.Problem(
                detail: JsonConvert.SerializeObject(errorResult),
                statusCode: 500
            ),
            ErrorCode.UnprocessableEntity => Results.UnprocessableEntity(errorResult),
            _ => Results.Problem(
                detail: JsonConvert.SerializeObject(errorResult),
                statusCode: 422
            )
        };
    }
}
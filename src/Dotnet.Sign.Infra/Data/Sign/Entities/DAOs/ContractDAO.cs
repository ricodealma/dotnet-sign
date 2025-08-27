using Dotnet.Sign.Domain.SeedWork.EnumExtensions;
using Dotnet.Sign.Domain.SeedWork.ErrorResult;
using Dotnet.Sign.Infra.Data.Sign.Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Dotnet.Sign.Infra.Data.Sign.Entities.DAOs
{
    public class ContractDAO(ILogger<ContractDAO> logger, ISignContext crmContext) : IContractDAO
    {
        private readonly ILogger<ContractDAO> _logger = logger;
        private readonly ISignContext _crmContext = crmContext;
        public async Task<Tuple<ContractDTO?, ErrorResult>> SelectByIdAsync(Guid id)
        {
            try
            {
                var contractDTO = await _crmContext.Contract
                    .Include(contract => contract.Status)
                    .FirstOrDefaultAsync(contract => contract.Id == id);

                if (contractDTO == null)
                {
                    return new(null, new ErrorResult
                    {
                        Error = true,
                        StatusCode = ErrorCode.NotFound,
                        Id = id.ToString(),
                        Message = "Contract not found for the given ID"
                    });
                }

                return new(contractDTO, new());
            }
            catch (Exception e)
            {
                string error = JsonConvert.SerializeObject(e);
                _logger.LogError(error);

                return new(null, new ErrorResult
                {
                    Error = true,
                    Message = error,
                    StatusCode = ErrorCode.InternalServerError,
                    Id = id.ToString()
                });
            }
        }

        public async Task<Tuple<ContractDTO?, ErrorResult>> InsertAsync(ContractDTO contract)
        {
            await using var transaction = await _crmContext.Database.BeginTransactionAsync();
            try
            {
                var result = await _crmContext.Contract.AddAsync(contract);
                await _crmContext.SaveChangesAsync();

                if (result.Entity.Id == default)
                {
                    await transaction.RollbackAsync();
                    return new(null, new()
                    {
                        Error = true,
                        StatusCode = ErrorCode.InternalServerError,
                        Message = $"Unexpected Error While inserting contract: {JsonConvert.SerializeObject(contract)}"
                    });
                }

                await transaction.CommitAsync();
                return new(result.Entity, new());

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError($"Unexpected error: {ex.Message} - {JsonConvert.SerializeObject(contract)}");
                return new(null, new()
                {
                    Error = true,
                    StatusCode = ErrorCode.InternalServerError,
                    Message = $"{JsonConvert.SerializeObject(contract)}"
                });
            }
        }

        public async Task<Tuple<ContractDTO?, ErrorResult>> PatchContractStatusAsync(Guid contractId, ContractStatusEnum status)
        {
            try
            {
                var contract = await _crmContext.Contract.FindAsync(contractId);

                if (contract == null)
                    return Tuple.Create<ContractDTO?, ErrorResult>(null, new()
                    {
                        Error = true,
                        Id = contractId.ToString(),
                        Message = "Couldn't find contract for that id",
                        StatusCode = ErrorCode.NotFound
                    });

                contract.StatusId = (int)status;

                await _crmContext.SaveChangesAsync();

                return Tuple.Create<ContractDTO?, ErrorResult>(contract, new());
            }
            catch (Exception e)
            {
                _logger.LogError(JsonConvert.SerializeObject(e));
                return Tuple.Create<ContractDTO?, ErrorResult>(null, new()
                {
                    Error = true,
                    StatusCode = ErrorCode.InternalServerError,
                    Message = $"Failed to update contract status with error: {JsonConvert.SerializeObject(e)}"
                });
            }
        }
    }
}

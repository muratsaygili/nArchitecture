using Application.Features.OperationClaims.Dtos;

namespace Application.Features.OperationClaims.Models
{
    public class OperationClaimListModel // no need to paginate
    {
        public IList<OperationClaimDto> Items { get; set; }
    }
}

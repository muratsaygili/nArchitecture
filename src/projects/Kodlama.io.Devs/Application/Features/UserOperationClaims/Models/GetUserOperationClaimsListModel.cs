using Application.Features.UserOperationClaims.Dtos;

namespace Application.Features.UserOperationClaims.Models
{
    public class GetUserOperationClaimsListModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public IList<UserOperationClaimDto> UserOperationClaimsDtoList { get; set; }
    }
}

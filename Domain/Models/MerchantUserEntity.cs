using SurfTicket.Application.Exceptions;
using SurfTicket.Domain.Enums;

namespace SurfTicket.Domain.Models
{
    public class MerchantUserEntity : BaseEntity
    {
        public int MerchantId { get; set; }
        public MerchantEntity Merchant { get; set; }
        public string UserId { get; set; }
        public UserEntity User { get; set; }
        public MerchantRole Role { get; set; }
        public List<PermissionMenuEntity> PermissionMenus { get; set; }

        public void EnsureHasPermission(PermissionAdminEntity? permission, PermissionAccess access)
        {
            bool pass = false;

            if (Role == MerchantRole.OWNER)
            {
                pass = true;
            }
            else if (Role == MerchantRole.COLLABORATOR)
            {
                if (permission == null)
                {
                    pass = false;
                }
                else
                {
                    pass = PermissionMenus.Any(pm => pm.PermissionAdminId == permission.Id && pm.Access == access);
                }
            }

            if (!pass)
            {
                throw new BadRequestSurfException(SurfErrorCode.MERCHANT_VIOLATE_PERMISSION, "Merchant user does not have access");
            }
        }
    }
}

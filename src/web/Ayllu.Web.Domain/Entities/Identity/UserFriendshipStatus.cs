namespace Ayllu.Web.Domain.Entities.Identity;

public enum UserFriendshipStatus
{
    PENDING         = 0,
    DEFAULT         = PENDING,
    ACCEPTED        = 1,
    REFUSED         = 2,
    BLOCKED         = 4
}

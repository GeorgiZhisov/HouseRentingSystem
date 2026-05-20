namespace House_renting_system_Project.Extensions;

using System.Security.Claims;

using static Common.Constants.RoleNames;

public static class UserExtensions
{
    public static string? GetId(this ClaimsPrincipal user)
        => user.FindFirstValue(ClaimTypes.NameIdentifier);

    public static bool IsClient(this ClaimsPrincipal user)
        => user.IsInRole(Client);

    public static bool IsAgent(this ClaimsPrincipal user)
        => user.IsInRole(Agent);
}

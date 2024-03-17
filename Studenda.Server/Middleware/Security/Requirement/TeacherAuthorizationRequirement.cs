using Studenda.Server.Configuration.Static;

namespace Studenda.Server.Middleware.Security.Requirement;

/// <summary>
///     Требование авторизации для доступа преподавателя.
/// </summary>
public class TeacherAuthorizationRequirement : IPermissionAuthorizationRequirement
{
    public const string PolicyCode = "TeacherAuthorizationPolicy";

    /// <summary>
    ///     Получить требуемый доступ.
    /// </summary>
    /// <returns>Доступ.</returns>
    public string GetRequiredPermission()
    {
        return PermissionConfiguration.TeacherPermission;
    }
}
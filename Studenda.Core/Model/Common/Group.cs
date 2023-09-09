using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Studenda.Core.Data.Configuration;
using Studenda.Core.Model.Schedule;
using Studenda.Core.Model.Security;

namespace Studenda.Core.Model.Common;

/// <summary>
///     Группа.
/// </summary>
public class Group : Identity
{
    /*                   __ _                       _   _
     *   ___ ___  _ __  / _(_) __ _ _   _ _ __ __ _| |_(_) ___  _ __
     *  / __/ _ \| '_ \| |_| |/ _` | | | | '__/ _` | __| |/ _ \| '_ \
     * | (_| (_) | | | |  _| | (_| | |_| | | | (_| | |_| | (_) | | | |
     *  \___\___/|_| |_|_| |_|\__, |\__,_|_|  \__,_|\__|_|\___/|_| |_|
     *                        |___/
     * Константы, задающие базовые конфигурации полей
     * и ограничения модели.
     */

    #region Configuration

    /// <summary>
    ///     Максимальная длина поля <see cref="Name" />.
    /// </summary>
    public const int NameLengthMax = 128;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="CourseId" />.
    /// </summary>
    public const bool IsCourseIdRequired = true;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="DepartmentId" />.
    /// </summary>
    public const bool IsDepartmentIdRequired = true;

    /// <summary>
    ///     Статус необходимости наличия значения в поле <see cref="Name" />.
    /// </summary>
    public const bool IsNameRequired = true;

    /// <summary>
    ///     Конфигурация модели <see cref="Group" />.
    /// </summary>
    internal class Configuration : Configuration<Group>
    {
        /// <summary>
        ///     Конструктор.
        /// </summary>
        /// <param name="configuration">Конфигурация базы данных.</param>
        public Configuration(ContextConfiguration configuration) : base(configuration)
        {
            // PASS.
        }

        /// <summary>
        ///     Задать конфигурацию для модели.
        /// </summary>
        /// <param name="builder">Набор интерфейсов настройки модели.</param>
        public override void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.Property(group => group.Name)
                .HasMaxLength(NameLengthMax)
                .IsRequired();

            builder.HasOne(group => group.Course)
                .WithMany(course => course.Groups)
                .HasForeignKey(group => group.CourseId)
                .IsRequired();

            builder.HasOne(group => group.Department)
                .WithMany(course => course.Groups)
                .HasForeignKey(group => group.DepartmentId)
                .IsRequired();

            builder.HasMany(group => group.Users)
                .WithOne(user => user.Group)
                .HasForeignKey(course => course.GroupId);

            builder.HasMany(group => group.StaticSchedules)
                .WithOne(schedule => schedule.Group)
                .HasForeignKey(schedule => schedule.GroupId);

            base.Configure(builder);
        }
    }

    #endregion

    /*             _   _ _
     *   ___ _ __ | |_(_) |_ _   _
     *  / _ \ '_ \| __| | __| | | |
     * |  __/ | | | |_| | |_| |_| |
     *  \___|_| |_|\__|_|\__|\__, |
     *                       |___/
     * Поля данных, соответствующие таковым в таблице
     * модели в базе данных.
     */

    #region Entity

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="Common.Course" />.
    /// </summary>
    public int CourseId { get; set; }

    /// <summary>
    ///     Идентификатор связанного объекта <see cref="Common.Department" />.
    /// </summary>
    public int DepartmentId { get; set; }

    /// <summary>
    ///     Название.
    /// </summary>
    public string Name { get; set; } = null!;

    #endregion

    /// <summary>
    ///     Связанный объект <see cref="Common.Course" />.
    /// </summary>
    public Course Course { get; set; } = null!;

    /// <summary>
    ///     Связанный объект <see cref="Common.Department" />.
    /// </summary>
    public Department Department { get; set; } = null!;

    /// <summary>
    ///     Связанные объекты <see cref="User" />.
    /// </summary>
    public List<User> Users { get; set; } = null!;

    /// <summary>
    ///     Связанные объекты <see cref="StaticSchedule" />.
    /// </summary>
    public List<StaticSchedule> StaticSchedules { get; set; } = null!;
}
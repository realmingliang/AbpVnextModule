using System.ComponentModel.DataAnnotations;

namespace Tudou.Abp.SettingManagement
{
    public class IdentitySettingsDto
    {
        public class Password
        {
            [MaxLength(128)]
            [MinLength(2)]
            public int RequiredLength { get; set; }
            [MaxLength(128)]
            [MinLength(1)]
            public int RequiredUniqueChars { get; set; }
            public bool RequireNonAlphanumeric { get; set; }
            public bool RequireLowercase { get; set; }
            public bool RequireUppercase { get; set; }
            public bool RequireDigit { get; set; }
        }

        public class LockOut
        {
            public int AllowedForNewUsers { get; set; }
            public int LockoutDuration { get; set; }
            public bool MaxFailedAccessAttempts { get; set; }
        }

        public class SignIn
        {
            public bool RequireConfirmedEmail { get; set; }
            public bool RequireConfirmedPhoneNumber { get; set; }

        }
        public class User
        {
            public bool IsUserNameUpdateEnabled { get; set; }
            public bool IsEmailUpdateEnabled { get; set; }

        }
    }
}
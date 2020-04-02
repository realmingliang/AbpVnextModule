namespace Tudou.Abp.Identity
{
    public class ChangePasswordInput
    {
        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }
    }
}

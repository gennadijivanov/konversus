namespace Conversus.Core.Infrastructure.Repository
{
    public class UserFilterParameters : IFilterParameters
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
}
using Microsoft.AspNetCore.Identity;

namespace MermaidSharp.EntityFrameworkCore.Tests.Mocks.Entities
{
    internal class User : IdentityUser<int>
    {
        public UserAuditInfo<int> Audit { get; set; } = null!;
    }
}

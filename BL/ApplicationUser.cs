using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using OnlineExamSystem.Models;

namespace OnlineExamSystem.BL
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            TbSubmissin = new HashSet<submission>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<submission> TbSubmissin { get; set; }
    }
}

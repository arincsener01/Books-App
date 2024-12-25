using BLL.DAL;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class UsersModel
    {
        public User Record { get; set; }
        [Display(Name = "User Name")]
        public string UserName => Record.UserName;
        public string Password => Record.Password;
        [Display(Name = "Status")]
        public string IsActive => Record.IsActive ? "Active" : "Inactive";
        public string RoleId => Record.Role?.Name;
    }
}

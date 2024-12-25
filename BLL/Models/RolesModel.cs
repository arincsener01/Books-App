// No need for that class, it's just a wrapper for the Role class, and it's not used anywhere in the project
using BLL.DAL;

namespace BLL.Models
{
    public class RolesModel
    {
        public Role Record { get; set; }
        public string Name => Record.Name;
    }
}

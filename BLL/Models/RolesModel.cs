using BLL.DAL;

namespace BLL.Models
{
    public class RolesModel
    {
        public Role Record { get; set; }
        public string Name => Record.Name;
    }
}

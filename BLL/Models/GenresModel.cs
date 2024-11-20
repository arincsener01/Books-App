using BLL.DAL;

namespace BLL.Models
{
    public class GenresModel
    {
        public Genre Record { get; set; }
        public string Name => Record.Name;
    }
}

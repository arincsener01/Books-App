
using BLL.DAL;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class AuthorsModel
    {
        public Author Record { get; set; }
        public string Name => Record.Name;
        public string Surname => Record.Surname;
        [Display(Name = "Full Name")]
        public string FullName => $"{Record.Name} {Record.Surname}";

    }
}

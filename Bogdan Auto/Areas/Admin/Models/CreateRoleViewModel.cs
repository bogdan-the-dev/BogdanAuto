using System.ComponentModel.DataAnnotations;

namespace Bogdan_Auto.Areas.Admin.Models
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace W1001_ABP_With_Zero.Configuration.Dto
{
    public class ChangeUiThemeInput
    {
        [Required]
        [MaxLength(32)]
        public string Theme { get; set; }
    }
}
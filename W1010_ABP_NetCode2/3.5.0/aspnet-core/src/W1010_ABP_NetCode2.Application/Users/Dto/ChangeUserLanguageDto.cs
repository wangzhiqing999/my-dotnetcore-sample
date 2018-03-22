using System.ComponentModel.DataAnnotations;

namespace W1010_ABP_NetCode2.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
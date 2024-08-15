using System.ComponentModel.DataAnnotations;

namespace ETicaret.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
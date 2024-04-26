using Microsoft.AspNetCore.Mvc.Rendering;

namespace FA.JustBlog.Models
{
    public class ChangeLanguageViewModel
    {
        //model
        public string? SelectedLanguage { get; set; } = "vn";

        public bool IsSubmit { get; set; } = false;

        //view model
        public List<SelectListItem>? ListOfLanguages { get; set; }
    }
}

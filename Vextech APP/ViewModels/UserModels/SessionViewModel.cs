using System.ComponentModel.DataAnnotations;

namespace Vextech_APP.ViewModels.UserModels
{
    public class SessionViewModel
    {
        [RegularExpression(@"^[a-z''-'\s]{100}$")]
        public string userSession { get; set; }
    }
}

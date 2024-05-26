namespace CanvasIdentity.Models
{
    public class CanvasIdentityOption
    { 
        public string ConsumerSecret { get; set; }
        public string LtiCourseLoginPath { get; set; }
        public string RedirectAfterLogin { get; set; }
        public string CanvasSessionQueryName { get; set; } = "session";
    }
}

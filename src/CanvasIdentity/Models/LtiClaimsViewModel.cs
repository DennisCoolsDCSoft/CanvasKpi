
namespace CanvasIdentity.Models
{
    public class LtiClaimsViewModel
    {
        public struct ClaimName
        {
            public const string CourseRequest = "CourseRequest";
            public const string CustomCanvasUserLoginId = "CustomCanvasUserLoginId";
            public const string LisPersonNameFull = "LisPersonNameFull";
            public const string CustomCanvasCourseId = "CustomCanvasCourseId";
            public const string CustomCanvasUserId = "CustomCanvasUserId";
            public const string Roles = "Roles";
            public const string CustomCanvasCourseName = "CustomCanvasCourseName";
            public const string LisPersonContactEmailPrimary = "LisPersonContactEmailPrimary";
            public const string LisPersonSisId = "LisPersonSisId";
        }
        public struct CanvasName
        {
            public const string CustomCanvasUserLoginId = "custom_canvas_user_login_id";
            public const string LisPersonNameFull = "lis_person_name_full";
            public const string CustomCanvasCourseId = "custom_canvas_course_id";
            public const string CustomCanvasUserId = "custom_canvas_user_id";
            public const string Roles = "roles";
            public const string CustomCanvasCourseName = "custom_canvas_course_name";
            public const string LisPersonContactEmailPrimary = "lis_person_contact_email_primary";
            public const string LisPersonSisId = "lis_person_sourcedid";
        }

        public string CustomCanvasUserLoginId { get; set; }
        public string LisPersonNameFull { get; set; }
        public string CustomCanvasCourseId { get; set; }
        public string CustomCanvasUserId { get; set; }
        public string Roles { get; set; }
        public string CustomCanvasCourseName { get; set; }
        public string LisPersonContactEmailPrimary { get; set; }
        public string LisPersonSisId { get; set; }
    }
}

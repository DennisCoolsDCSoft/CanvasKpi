using System;

namespace CanvasIdentity.Helpers
{
    public class CourseClaims
    {
        private readonly string _canvasCourseId;
        private readonly string _canvasUserId;
 
        public CourseClaims(string canvasUserLoginId,string lisPersonNameFull, string canvasCourseId, string canvasUserId, string roles,string canvasCourseName, string lisPersonContactEmailPrimary, string lisPersonSisId)
        {
            CanvasUserLoginId = canvasUserLoginId;
            LisPersonNameFull = lisPersonNameFull;
            _canvasCourseId = canvasCourseId;
            _canvasUserId = canvasUserId;
            Roles = roles;
            CanvasCourseName = canvasCourseName;
            LisPersonContactEmailPrimary = lisPersonContactEmailPrimary;
            LisPersonSisId = lisPersonSisId;
        }

        public string CanvasUserLoginId { get;  }
        public string LisPersonNameFull { get;  }
        public string Roles { get; }
        public string CanvasCourseName { get;}
        public string LisPersonContactEmailPrimary { get; }
        public string LisPersonSisId { get; }

        public bool IsCanvasInstructor()
        {  // urn:lti:instrole:ims/lis/Administrator
            string[] rol = Roles.Split(',');
            foreach (var r in rol)
            {
                if (r == "Learner") return false;
                if (r == "Instructor") return true;
                if (r == "urn:lti:instrole:ims/lis/Administrator") return true;
            }
            return false;
        }

        public bool IsCanvasLearner()
        { 
            string[] rolls = Roles.Split(',');
            foreach (var rol in rolls)
            {
                if (rol == "Learner") return true;
            }
            return false;
        }

        public bool HasCanvasUserId()
        {
            if (_canvasUserId == "") return false;
            if (!int.TryParse(_canvasUserId, out _)) return false;
            return true;
        }

        public int CanvasUserId
        {
            get
            {
                if (_canvasUserId == "") throw new Exception("No Canvas user login");
                if (!int.TryParse(_canvasUserId, out var userId)) throw new Exception("No Canvas user login");
                return userId;
            }
        }

        public bool HasCourseId()
        {
            if (_canvasCourseId == "") return false;
            return int.TryParse(_canvasCourseId, out _);
        }

        public int CanvasCourseId
        {
            get
            {
                if (_canvasCourseId == "") throw new Exception("No Canvas Course id");
                if (!int.TryParse(_canvasCourseId, out var courseId)) throw new Exception("No Canvas Course id");
                return courseId;
            }
        }
    }
}

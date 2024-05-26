using System;
using System.Linq;
using System.Security.Claims;
using CanvasIdentity.Helpers;
using CanvasIdentity.Models;

namespace CanvasIdentity.Extensions
{
    public static class UserCanvasClaimsExtension
    {
        public static CourseClaims CanvasClaims(this ClaimsPrincipal user)
        {
            var courseRequestSession = user.Claims.FirstOrDefault(s => s.Type == LtiClaimsViewModel.ClaimName.CourseRequest)?.Value;
            if (courseRequestSession == null) throw new Exception("CourseRequest not in User Claims, is the session missing or cookie problem");
            var ltiPar = new CourseClaims(
                 user.Claims.Single(s => s.Type == LtiClaimsViewModel.ClaimName.CustomCanvasUserLoginId && s.Issuer == "0").Value,
                 user.Claims.Single(s => s.Type == LtiClaimsViewModel.ClaimName.LisPersonNameFull && s.Issuer == "0").Value,
                 user.Claims.Single(s => s.Type == LtiClaimsViewModel.ClaimName.CustomCanvasCourseId && s.Issuer == courseRequestSession).Value,
                 user.Claims.Single(s => s.Type == LtiClaimsViewModel.ClaimName.CustomCanvasUserId && s.Issuer == courseRequestSession).Value,
                 user.Claims.Single(s => s.Type == LtiClaimsViewModel.ClaimName.Roles && s.Issuer == courseRequestSession).Value,
                 user.Claims.Single(s => s.Type == LtiClaimsViewModel.ClaimName.CustomCanvasCourseName && s.Issuer == courseRequestSession).Value,
                 user.Claims.Single(s => s.Type == LtiClaimsViewModel.ClaimName.LisPersonContactEmailPrimary && s.Issuer == "0").Value,
                 user.Claims.Single(s => s.Type == LtiClaimsViewModel.ClaimName.LisPersonSisId && s.Issuer == "0").Value
                );
            return ltiPar;
        }
    }
}

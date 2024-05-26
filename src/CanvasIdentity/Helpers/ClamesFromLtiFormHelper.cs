using System.Collections.Generic;
using System.Security.Claims;
using CanvasIdentity.Models;
using Microsoft.AspNetCore.Http;

namespace CanvasIdentity.Helpers
{
    internal static class ClamesFromLtiFormHelper
        {
            public static List<Claim> CreateLtiClaimsFromForm(IFormCollection form, string session)
            {
                var ltiPar = new LtiClaimsViewModel
                {
                    CustomCanvasUserLoginId = (string)form[LtiClaimsViewModel.CanvasName.CustomCanvasUserLoginId] ?? "",
                    LisPersonNameFull = (string)form[LtiClaimsViewModel.CanvasName.LisPersonNameFull] ?? "",
                    CustomCanvasCourseId = (string)form[LtiClaimsViewModel.CanvasName.CustomCanvasCourseId] ?? "",
                    CustomCanvasCourseName = (string)form[LtiClaimsViewModel.CanvasName.CustomCanvasCourseName] ?? "",
                    CustomCanvasUserId = (string)form[LtiClaimsViewModel.CanvasName.CustomCanvasUserId] ?? "",
                    Roles = (string)form[LtiClaimsViewModel.CanvasName.Roles] ?? "",
                    LisPersonContactEmailPrimary = (string)form[LtiClaimsViewModel.CanvasName.LisPersonContactEmailPrimary] ?? "",
                    LisPersonSisId = (string)form[LtiClaimsViewModel.CanvasName.LisPersonSisId] ?? ""
                };

                var claims = new List<Claim>
                {
                    // user
                    new Claim(LtiClaimsViewModel.ClaimName.CustomCanvasUserLoginId, ltiPar.CustomCanvasUserLoginId,null,"0"),
                    new Claim(LtiClaimsViewModel.ClaimName.LisPersonNameFull, ltiPar.LisPersonNameFull,null,"0"),
                    new Claim(LtiClaimsViewModel.ClaimName.LisPersonSisId, ltiPar.LisPersonSisId, null, "0"),
                    new Claim(LtiClaimsViewModel.ClaimName.LisPersonContactEmailPrimary, ltiPar.LisPersonContactEmailPrimary, null, "0"),

                    // session
                    new Claim(LtiClaimsViewModel.ClaimName.CustomCanvasUserId, ltiPar.CustomCanvasUserId, null, session),
                    new Claim(LtiClaimsViewModel.ClaimName.CustomCanvasCourseId, ltiPar.CustomCanvasCourseId, null, session),
                    new Claim(LtiClaimsViewModel.ClaimName.CustomCanvasCourseName, ltiPar.CustomCanvasCourseName, null, session),
                    new Claim(LtiClaimsViewModel.ClaimName.Roles, ltiPar.Roles, null, session),
                };
                return claims;
            }
        }
}

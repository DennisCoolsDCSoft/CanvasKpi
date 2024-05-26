using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;


namespace CanvasIdentity.Helpers
{
    internal static class ClaimsHelper
    {
        internal static async Task MergeUserClaimsAndSignIn(HttpContext context, List<Claim> addClaim)
        {
            var claimsInCookie = new List<Claim>();
            if (context.User.Claims.Any())
                claimsInCookie.AddRange(context.User.Claims.Select(item =>
                    new Claim(item.Type, item.Value, null, item.Issuer)));

            var claims = MergeClaims(claimsInCookie, addClaim);

            var identity = context.User.Identities.FirstOrDefault(f =>
                f.AuthenticationType == CookieAuthenticationDefaults.AuthenticationScheme);

            if (identity != null)
            {
                foreach (var c in identity.Claims.ToList())
                {
                    identity.RemoveClaim(c);
                }

                foreach (var c in claims)
                {
                    identity.AddClaim(c);
                }

                await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity));
            }
            else
            {
                // non identity create one and add
                var claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);
                context.User.AddIdentity(claimsIdentity);
                await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));
            }
        }
        
        private static List<Claim> MergeClaims(List<Claim> claimsInCookie, List<Claim> addClaims)
        {
            foreach (var item in addClaims)
            {
                if (!claimsInCookie.Exists(e => e.Issuer == item.Issuer && e.Type == item.Type))
                {
                    claimsInCookie.Add(item);
                }
                else
                {
                    var claim = claimsInCookie.First(e => e.Issuer == item.Issuer && e.Type == item.Type);
                    if (claim.Value != item.Value)
                    {
                        claimsInCookie.Remove(claim);
                        claimsInCookie.Add(item);
                    }
                }
            }
            return claimsInCookie;
        }
    }
}

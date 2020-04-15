using IdentityModel;
using ReportViewer.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ReportViewer.Domain.Models
{
    public class UserContext : IUserContext
    {
        private readonly ClaimsPrincipal _claimsPrincipal;
        public UserContext(ClaimsPrincipal claimsPrincipal)
        {
            _claimsPrincipal = claimsPrincipal;
        }
        public string Sub
        {
            get
            {
                var claims = _claimsPrincipal.Claims;
                return claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Subject)?.Value
                    ?? claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            }
        }

        public string FirstName
        {
            get
            {
                var claims = _claimsPrincipal.Claims;
                return claims.FirstOrDefault(c => c.Type == JwtClaimTypes.GivenName)?.Value
                    ?? claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value;
            }
        }

        public string LastName
        {
            get
            {
                var claims = _claimsPrincipal.Claims;
                return claims.FirstOrDefault(c => c.Type == JwtClaimTypes.FamilyName)?.Value
                    ?? claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;
            }
        }

        public string Name
        {
            get
            {
                var claims = _claimsPrincipal.Claims;
                return claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Name)?.Value
                    ?? claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value
                    ?? $"{FirstName} {LastName}";
            }
        }

        public string Email
        {
            get
            {
                var claims = _claimsPrincipal.Claims;
                return claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Email)?.Value
                    ?? claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            }
        }

        public string Phone
        {
            get
            {
                var claims = _claimsPrincipal.Claims;
                return claims.FirstOrDefault(c => c.Type == JwtClaimTypes.PhoneNumber)?.Value
                    ?? claims.FirstOrDefault(c => c.Type == ClaimTypes.MobilePhone)?.Value;
            }
        }
    }
}

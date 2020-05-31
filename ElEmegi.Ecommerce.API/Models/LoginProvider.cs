using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using ElEmegi.Ecommerce.Model.Entity;
using Microsoft.Owin.Security.OAuth;
using WebGrease.Css.Extensions;

namespace ElEmegi.Ecommerce.API.Models
{
    public partial class LoginProvider : OAuthAuthorizationServerProvider
    {
        private DataContext db = new DataContext();
        public Member Login(string email, string password)
        {
            var member = db.Members.Where(x => x.Email == email && x.Password == password && x.IsActive == true)
                .ToList();
            if (member.Count > 0)
            {
                return member.FirstOrDefault();
            }
            else
            {
                return null;   
            }
        }

        public override Task ValidateAuthorizeRequest(OAuthValidateAuthorizeRequestContext context)
        {
            context.Validated();
            return base.ValidateAuthorizeRequest(context);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var member = Login(context.UserName, context.Password);
            if (member == null)
            {
                //hatalı giriş
                context.SetError("invalid_grant","Hatalı kullanıcı bilgisi");
            }
            else
            {
                //başarılı
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("UserName",context.UserName));
                identity.AddClaim(new Claim("Password",context.Password));
                identity.AddClaim(new Claim("MemberID", context.ClientId));

                context.Validated(identity);
            }
            return base.GrantResourceOwnerCredentials(context);
        }
    }
}
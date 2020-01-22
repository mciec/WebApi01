using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace WebApi01
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var SecretKey = Encoding.ASCII.GetBytes
                 ("YourKey-2374-OFFKDI940NG7:56753253-tyuw-5769-0921-kfirox29zoxv");
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(token =>
            {
                token.RequireHttpsMetadata = false;
                token.SaveToken = true;
                token.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    //Same Secret key will be used while creating the token
                    IssuerSigningKey = new SymmetricSecurityKey(SecretKey),
                    ValidateIssuer = true,
                    //Usually, this is your application base URL
                    ValidIssuer = "proper issuer",
                    ValidateAudience = true,
                    //Here, we are creating and using JWT within the same application.
                    //In this case, base URL is fine.
                    //If the JWT is created using a web service, then this would be the consumer URL.
                    ValidAudience = "michalC",
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            //services.AddAuthentication(options =>
            //    {
            //        options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //        options.DefaultChallengeScheme = "MyLoginScheme";
            //    })
            //    .AddCookie()
            //    .AddOAuth("MyLoginScheme", options =>
            //    {
            //        options.ClientId = "mciec";
            //        options.ClientSecret = "password";
            //        options.CallbackPath = "/OAuth2/Autorize";
            //        options.AuthorizationEndpoint = "https://localhost:44371";
            //        options.TokenEndpoint = "https://localhost:44371";
            //        options.UserInformationEndpoint = "https://localhost:44371";
            //        options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
            //        options.ClaimActions.MapJsonKey(ClaimTypes.Role, "role");
            //        options.Events = new OAuthEvents
            //        {
            //            OnCreatingTicket = async (context) =>
            //            {
            //                var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
            //                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);

            //                var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
            //                response.EnsureSuccessStatusCode();

            //                var user = JObject.Parse(await response.Content.ReadAsStringAsync());

            //                context.RunClaimActions(user);
            //            }
            //        };
            //    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                var principal = context.User as ClaimsPrincipal;
                var accessToken = principal?.Claims
                  .FirstOrDefault(c => c.Type == "access_token");
                await next();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

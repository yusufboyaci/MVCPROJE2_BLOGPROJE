using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MVCPROJE2_BLOGPROJE.CORE.Entities;
using MVCPROJE2_BLOGPROJE.DATAACCESS.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCPROJE2_BLOGPROJE.DATAACCESS.SeedMethods
{
    public class SeedData
    {
        private string CreatePasswordHash(IdentityUser user, string password)
        {
            var passwordHasher = new PasswordHasher<IdentityUser>();
            return passwordHasher.HashPassword(user, password);
        }
        public static void Seed(IApplicationBuilder app)
        {
            using (IServiceScope servicesScope = app.ApplicationServices.CreateScope())
            {
                ApplicationDbContext context = servicesScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.Migrate();


                if (!context.Users.Any(x => x.UserName == "admin@gmail.com") && !context.Roles.Any(x => x.Name == "Admin"))
                {
                    var adminUser = new IdentityUser
                    {
                        UserName = "admin@gmail.com",
                        NormalizedUserName = "ADMIN@GMAIL.COM",
                        Email = "admin@gmail.com",
                        NormalizedEmail = "ADMIN@GMAIL.COM",
                        PhoneNumber = null,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString(),
                    };
                    var passwordHashConverter = new SeedData();
                    adminUser.PasswordHash = passwordHashConverter.CreatePasswordHash(adminUser, "admin");
                    context.Users.AddRange(adminUser);


                    var role = new IdentityRole
                    {
                        Name = "Admin",
                        NormalizedName = "ADMIN",
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    };

                    context.Roles.AddRange(role);
                }
                context.SaveChanges();
                if (!context.UserRoles.Any())
                {
                    context.UserRoles.AddRange(new IdentityUserRole<string>
                    {
                        UserId = context.Users.FirstOrDefault().Id,
                        RoleId = context.Roles.FirstOrDefault().Id

                    });

                }
                context.SaveChanges();

                if (!context.Uyeler.Any(x => x.Ad == "Admin"))
                {
                    context.Uyeler.AddRange(
                        new Uye
                        {
                            Ad = "Admin",
                            Soyad = "Admin",
                            KullaniciAdi = "Admin",
                            MailAdresi = "admin@gmail.com",
                            KullaniciAciklama = null,
                            IsActive = true,
                        }
                        );
                }
                context.SaveChanges();
                if (!context.Konular.Any())
                {
                    context.Konular.AddRange(
                        new Konu()
                        {
                            KonuBasliklari = "Resim - Sanat"
                        },
                        new Konu()
                        {
                            KonuBasliklari = "Popüler Kültür"
                        },
                        new Konu()
                        {
                            KonuBasliklari = "Sağlık"
                        },
                        new Konu()
                        {
                            KonuBasliklari = "Sanayi"
                        },
                        new Konu()
                        {
                            KonuBasliklari = "Organik Tarım"
                        },
                        new Konu()
                        {
                            KonuBasliklari = "Siyaset",
                        },
                        new Konu()
                        {
                            KonuBasliklari = "Yemek Tarifleri",
                        },
                        new Konu()
                        {
                            KonuBasliklari = "Botanik",
                        },
                        new Konu()
                        {
                            KonuBasliklari = "Spor",
                        },
                        new Konu()
                        {
                            KonuBasliklari = "Moda",
                        },
                        new Konu()
                        {
                            KonuBasliklari = "Kişisel Gelişim",
                        },
                        new Konu()
                        {
                            KonuBasliklari = "Turizm",
                        }


                  );
                }
                context.SaveChanges();
            }
        }

    }
}

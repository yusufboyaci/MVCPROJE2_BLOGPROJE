using Microsoft.AspNetCore.Builder;
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
        public static void Seed(IApplicationBuilder app)
        {
            using (IServiceScope servicesScope = app.ApplicationServices.CreateScope())
            {
                ApplicationDbContext context = servicesScope.ServiceProvider.GetService<ApplicationDbContext>();
                context.Database.Migrate();
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

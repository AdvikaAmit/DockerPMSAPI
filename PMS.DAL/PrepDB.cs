using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PMS.DAL
{
    public class PrepDB
    {
        public static void PrepPopution(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SendData(serviceScope.ServiceProvider.GetService<ApplicationDBContext>());
            }
        }

        public static void SendData(ApplicationDBContext context)
        {
            Console.WriteLine("Applying migration");
            context.Database.Migrate();

            if (context.Registration.Any())
            {
                context.Registration.AddRange(new Domain.Entiites.Registration() 
                { 
                    UserId =1,
                    Title="Mr.",
                    FirstName="Amit",
                    LastName="Shinde",
                    EmailId="amit.shinde@citiustech.com",
                    Password="Admin@1234",
                    IsActive=true,
                    Is_SetDefault=false,
                    Status="Active"
                });
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Already have data not seeding");
            }


        }
    }
}

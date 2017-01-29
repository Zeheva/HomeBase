using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HomeBase.Data;

namespace HomeBase.Models
{
    public static class DbInitializer
    {
        public static void Initialize(HomeBaseContext context)
        {
            context.Database.EnsureCreated();

            // Look for any Players.
            if (context.Players.Any())
            {
                return;   // DB has been seeded
            }

            var players = new Player[]
            {
            new Player{FirstName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
            new Player{FirstName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Player{FirstName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01")},
            new Player{FirstName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Player{FirstName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Player{FirstName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
            new Player{FirstName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01")},
            new Player{FirstName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01")}
            };
            foreach (Player p in players)
            {
                context.Players.Add(p);
            }
            context.SaveChanges();

            var teams = new Team[]
            {
            new Team{TeamID=1050,TeamName="Chemistry",},
            new Team{TeamID=4022,TeamName="Microeconomics",},
            new Team{TeamID=4041,TeamName="Macroeconomics",},
            new Team{TeamID=1045,TeamName="Calculus",},
            new Team{TeamID=3141,TeamName="Trigonometry",},
            new Team{TeamID=2021,TeamName="Composition",},
            new Team{TeamID=2042,TeamName="Literature",}
            };       

            foreach (Team t in teams)
            {
                context.Teams.Add(t);
            }
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
            new Enrollment{PlayerID=1,TeamID=1050,Position=Position.Pitcher},
            new Enrollment{PlayerID=1,TeamID=4022,Position=Position.Catcher},
            new Enrollment{PlayerID=1,TeamID=4041,Position=Position.FirstBaseman},
            new Enrollment{PlayerID=2,TeamID=1045,Position=Position.SecondBaseman},
            new Enrollment{PlayerID=2,TeamID=3141,Position=Position.ThirdBaseman},
            new Enrollment{PlayerID=2,TeamID=2021,Position=Position.ShrotStop},
            new Enrollment{PlayerID=3,TeamID=1050},
            new Enrollment{PlayerID=4,TeamID=1050,},
            new Enrollment{PlayerID=4,TeamID=4022,Position=Position.LeftFielder},
            new Enrollment{PlayerID=5,TeamID=4041,Position=Position.CenterFielder},
            new Enrollment{PlayerID=6,TeamID=1045},
            new Enrollment{PlayerID=7,TeamID=3141,Position=Position.RightFielder},
            };
            foreach (Enrollment e in enrollments)
            {
                context.Enrollments.Add(e);
            }
            context.SaveChanges();
        }
    }
}
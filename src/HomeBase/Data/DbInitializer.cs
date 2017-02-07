using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HomeBase.Data;
using HomeBase.Models;

namespace HomeBase.Models
{
    public static class DbInitializer
    {
        public static void Initialize(HomeBaseContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Players.Any())
            {
                return;   // DB has been seeded
            }

            var players = new Player[]
            {
                new Player { FirstName = "Carson",   LastName = "Alexander",
                    EnrollmentDate = DateTime.Parse("2010-09-01") },
                new Player { FirstName = "Meredith", LastName = "Alonso",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Player { FirstName = "Arturo",   LastName = "Anand",
                    EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Player { FirstName = "Gytis",    LastName = "Barzdukas",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Player { FirstName = "Yan",      LastName = "Li",
                    EnrollmentDate = DateTime.Parse("2012-09-01") },
                new Player { FirstName = "Peggy",    LastName = "Justice",
                    EnrollmentDate = DateTime.Parse("2011-09-01") },
                new Player { FirstName = "Laura",    LastName = "Norman",
                    EnrollmentDate = DateTime.Parse("2013-09-01") },
                new Player { FirstName = "Nino",     LastName = "Olivetto",
                    EnrollmentDate = DateTime.Parse("2005-09-01") }
            };

            foreach (Player p in players)
            {
                context.Players.Add(p);
            }
            context.SaveChanges();

            var captains = new Captain[]
            {
                new Captain { FirstName = "Kim",     LastName = "Abercrombie",
                    StartDate = DateTime.Parse("1995-03-11") },
                new Captain { FirstName = "Fadi",    LastName = "Fakhouri",
                    StartDate = DateTime.Parse("2002-07-06") },
                new Captain { FirstName = "Roger",   LastName = "Harui",
                    StartDate = DateTime.Parse("1998-07-01") },
                new Captain { FirstName = "Candace", LastName = "Kapoor",
                    StartDate = DateTime.Parse("2001-01-15") },
                new Captain { FirstName = "Roger",   LastName = "Zheng",
                    StartDate = DateTime.Parse("2004-02-12") }
            };

            foreach (Captain c in captains)
            {
                context.Captains.Add(c);
            }
            context.SaveChanges();
           
       
            var teams = new Team[]
            {
                new Team {TeamID = 1050, TeamName = "Chemistry"},

                new Team {TeamID = 4022, TeamName = "Microeconomics"},
                new Team {TeamID = 4041, TeamName = "Macroeconomics",},
                new Team {TeamID = 1045, TeamName = "Calculus"},
                new Team {TeamID = 3141, TeamName = "Trigonometry"},
                new Team {TeamID = 2021, TeamName = "Composition"},
                new Team {TeamID = 2042, TeamName = "Literature"},
            };

            foreach (Team t in teams)
            {
                context.Teams.Add(t);
            }
            context.SaveChanges();

            var teamCaptains = new TeamAssignment[]
            {
                new TeamAssignment {
                    TeamID = teams.Single(c => c.TeamName == "Chemistry" ).TeamID,
                    CaptainID = captains.Single(i => i.LastName == "Kapoor").CaptainID
                    },
                new TeamAssignment {
                    TeamID = teams.Single(c => c.TeamName == "Chemistry" ).TeamID,
                    CaptainID = captains.Single(i => i.LastName == "Harui").CaptainID
                    },
                new TeamAssignment {
                    TeamID = teams.Single(c => c.TeamName == "Microeconomics" ).TeamID,
                    CaptainID = captains.Single(i => i.LastName == "Zheng").CaptainID
                    },
                new TeamAssignment {
                    TeamID = teams.Single(c => c.TeamName == "Macroeconomics" ).TeamID,
                    CaptainID = captains.Single(i => i.LastName == "Zheng").CaptainID
                    },
                new TeamAssignment {
                    TeamID = teams.Single(c => c.TeamName == "Calculus" ).TeamID,
                    CaptainID = captains.Single(i => i.LastName == "Fakhouri").CaptainID
                    },
                new TeamAssignment {
                    TeamID = teams.Single(c => c.TeamName == "Trigonometry" ).TeamID,
                    CaptainID = captains.Single(i => i.LastName == "Harui").CaptainID
                    },
                new TeamAssignment {
                    TeamID = teams.Single(c => c.TeamName == "Composition" ).TeamID,
                    CaptainID = captains.Single(i => i.LastName == "Abercrombie").CaptainID
                    },
                new TeamAssignment {
                    TeamID = teams.Single(c => c.TeamName == "Literature" ).TeamID,
                    CaptainID = captains.Single(i => i.LastName == "Abercrombie").CaptainID
                    },
            };

            foreach (TeamAssignment ci in teamCaptains)
            {
                context.TeamAssignment.Add(ci);
            }
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
                new Enrollment {
                    PlayerID = players.Single(s => s.LastName == "Alexander").PlayerID,
                    TeamID = teams.Single(c => c.TeamName == "Chemistry" ).TeamID,
                    Position = Position.ShortStop
                },
            };
                    

            foreach (Enrollment e in enrollments)
            {
                var enrollmentInDataBase = context.Enrollments.Where(
                    s =>
                            s.Player.PlayerID == e.PlayerID &&
                            s.Team.TeamID == e.TeamID).SingleOrDefault();
                if (enrollmentInDataBase == null)
                {
                    context.Enrollments.Add(e);
                }
            }
            context.SaveChanges();
        }
    }
}
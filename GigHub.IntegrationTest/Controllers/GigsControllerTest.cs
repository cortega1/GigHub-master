using FluentAssertions;
using GigHub.Controllers;
using GigHub.Core.Models;
using GigHub.IntegrationTest.Extensions;
using GigHub.Persistence;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.IntegrationTest.Controllers
{
    [TestFixture]
    public class GigsControllerTest
    {
        private GigsController _controller;
        private ApplicationDbContext _context;

        [OneTimeSetUp]
        public void SetUp()
        {
            _context = new ApplicationDbContext();
            _controller = new GigsController(new UnitOfWork(_context));
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test, Isolated]
        public void Mine_WhenCalled_ShouldReturnUpcomingGigs()
        {
            //Arrange
            var user = _context.Users.First();
            _controller.MockCurrentUserForControllers(user.Id, user.UserName);

            var genre = _context.Genres.First();
            var gig = new Gig { Artist = user, DateTime = DateTime.Now.AddDays(1), Genre = genre, Venue = "-" };
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            //Act
            var result = _controller.Mine();

            //Assert

            (result.ViewData.Model as IEnumerable<Gig>).Should().HaveCount(1);
        }

        [Test, Isolated]
        public void Update_WhenCalled_ShouldUpdateTheGiveGig()
        {
            //Arrange
            var user = _context.Users.First();
            _controller.MockCurrentUserForControllers(user.Id, user.UserName);

            var genre = _context.Genres.Single(g => g.ID == 1);
            var gig = new Gig { Artist = user, DateTime = DateTime.Now.AddDays(1), Genre = genre, Venue = "-" };
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            //Act
            var result = _controller.Update(new Core.ViewModels.GigFormViewModel
            {
                Id = gig.ID,
                Date = DateTime.Today.AddMonths(1).ToString("d MMM yyyy"),
                Time= "20:00",
                Venue ="Somewhere place",
                Genre = 2
            });

            //Assert

            _context.Entry(gig).Reload();
            gig.DateTime.Should().Be(DateTime.Today.AddMonths(1).AddHours(20));
            gig.Venue.Should().Be("Somewhere place");
            gig.GenreId.Should().Be(2);

            //(result.ViewData.Model as IEnumerable<Gig>).Should().HaveCount(1);
        }
    }
}
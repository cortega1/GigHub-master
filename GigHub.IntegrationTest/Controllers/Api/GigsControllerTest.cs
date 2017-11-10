using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core.Models;
using GigHub.IntegrationTest.Extensions;
using GigHub.Persistence;
using NUnit.Framework;
using System;
using System.Linq;

namespace GigHub.IntegrationTest.Controllers.Api
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
        public void Update_WhenCalled_ShouldUpdateTheGiveGig()
        {
            //Arrange

            var user = _context.Users.First();
            _controller.MockCurrentUserForApis(user.Id, user.UserName);

            var genre = _context.Genres.Single(g => g.ID == 1);
            var gig = new Gig { Artist = user, DateTime = DateTime.Now.AddDays(1), Genre = genre, Venue = "-" };
            _context.Gigs.Add(gig);
            _context.SaveChanges();

            //Act

            var result = _controller.Cancel(gig.ID);

            //Assert

            _context.Entry(gig).Reload();
            gig.IsCanceled.Should().Be(true);
        }
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GigHub.Persistence.Repositories;
using GigHub.Persistence;
using Moq;
using GigHub.Core.Models;
using System.Data.Entity;
using FluentAssertions;

namespace GigHub.Test.Persistence.Repositories
{

    [TestClass]
    public class GigRepositoryTest
    {
        private GigRepository _repository;
        private Mock<DbSet<Gig>> _mockGigs;
        private Mock<DbSet<Attendance>> _mockAttendance;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockGigs = new Mock<DbSet<Gig>>();
            _mockAttendance = new Mock<DbSet<Attendance>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.Gigs).Returns(_mockGigs.Object);
            mockContext.SetupGet(c => c.Attendances).Returns(_mockAttendance.Object);

            _repository = new GigRepository(mockContext.Object);
        }

        [TestMethod]
        public void GetMyUpcommingGigs_GigIsInThePast_ShouldNotBeReturn()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(-1), ArtistId = "1" };

            _mockGigs.SetSourse(new[] { gig });

            var gigs = _repository.GetMyUpcommingGigs("1");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetMyUpcommingGigs_GigIsCanceled_ShouldNotBeReturn()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };
            gig.Cancel();

            _mockGigs.SetSourse(new[] { gig });

            var gigs = _repository.GetMyUpcommingGigs("1");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetMyUpcommingGigs_GigIsForADifferentArtist_ShouldNotBeReturn()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };

            _mockGigs.SetSourse(new[] { gig });

            var gigs = _repository.GetMyUpcommingGigs(gig.ArtistId+"-");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetMyUpcommingGigs_GigIsForTheGivenArtistAndIsInTheFuture_ShouldBeReturn()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "1" };

            _mockGigs.SetSourse(new[] { gig });

            var gigs = _repository.GetMyUpcommingGigs(gig.ArtistId);

            gigs.Should().Contain(gig);
        }

        [TestMethod]
        public void GetGigsUserAttending_GigsForADifferentUser_ShouldNotBeReturn()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "3", ID = 1 };
            var gig2 = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "3", ID = 2 };
            var attendance = new Attendance() { AttendeeId = "1", GigId = gig.ID };
            var attendance2 = new Attendance() { AttendeeId = "1", GigId = gig2.ID };

            _mockGigs.SetSourse(new[] { gig, gig2 });
            _mockAttendance.SetSourse(new[] { attendance, attendance2 });

            var gigs = _repository.GetGigsUserAttending("2");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetGigsUserAttending_GigsForTheGivenUser_ShouldBeReturn()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "2", ID = 1 };
            var gig2 = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "2", ID = 2 };
            var attendance = new Attendance() { AttendeeId = "1", Gig = gig };
            var attendance2 = new Attendance() { AttendeeId = "1", Gig = gig2 };

            _mockGigs.SetSourse(new[] { gig, gig2 });
            _mockAttendance.SetSourse(new[] { attendance, attendance2 });

            var gigs = _repository.GetGigsUserAttending("1");

            gigs.Should().Contain(gigs);
        }

        [TestMethod]
        public void GetGigsUserAttending_GigsForTheGivenUser_ShouldNotBeReturn()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "2", ID = 1 };
            var gig2 = new Gig() { DateTime = DateTime.Now.AddDays(1), ArtistId = "2", ID = 2 };
            var attendance = new Attendance() { AttendeeId = "2", GigId = gig.ID };
            var attendance2 = new Attendance() { AttendeeId = "2", GigId = gig2.ID };

            _mockGigs.SetSourse(new[] { gig, gig2 });
            _mockAttendance.SetSourse(new[] { attendance, attendance2 });

            var gigs = _repository.GetGigsUserAttending("1");

            gigs.Should().BeEmpty();
        }

        [TestMethod]
        public void GetGigsUserAttending_GigIsInThePast_ShouldNotBeReturn()
        {
            var gig = new Gig() { DateTime = DateTime.Now.AddDays(-1) };
            var attendance = new Attendance() { Gig = gig, AttendeeId = "1" };

            _mockAttendance.SetSourse(new[] { attendance });

            var gigs = _repository.GetGigsUserAttending(attendance.AttendeeId);

            gigs.Should().BeEmpty();
        }
    }
}

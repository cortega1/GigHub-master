using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GigHub.Persistence.Repositories;
using GigHub.Persistence;
using Moq;
using GigHub.Core.Models;
using System.Data.Entity;
using System.Collections.Generic;

namespace GigHub.Test.Persistence.Repositories
{
    [TestClass]
    public class GigRepositoryTest
    {
        private GigRepository _repository;

        [TestInitialize]
        public void TestInitialize()
        {
            var mockGigs = new Mock<DbSet<Gig>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.Gigs).Returns(mockGigs.Object);

            _repository = new GigRepository(mockContext.Object);
        }
    }
}

using Moq;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Test.Persistence.Repositories
{
    public static class MockDbSetExtensions
    {
        public static void SetSourse<T>(this Mock<DbSet<T>> mockSet, IList<T> source) where T : class
        {
            var data = source.AsQueryable();

            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns( data.GetEnumerator());
        }
    }
}

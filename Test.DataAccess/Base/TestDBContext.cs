namespace Test.DataAccess
{
    using System.Data.Entity;
    using Test.DataAccess.Base;
    using Test.DataAccess.Interfaces;
    using Test.DataAccess.BaseInterfaces;

    public partial class TestDBContext : BaseDbContext, ITestDBContext
    {
        public TestDBContext(IReflectionHelper reflectionHelper, IDbObjectsFactory dbObjectFactory) 
            : base(reflectionHelper, dbObjectFactory)
        {
        }

        public TestDBContext(string connectionString, IReflectionHelper reflectionHelper, IDbObjectsFactory dbObjectFactory) 
            : base(connectionString, reflectionHelper, dbObjectFactory)
        {           
        }
        
        public IDbSet<Mark> Marks { get; set; }
        public IDbSet<Person> People { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasOptional(e => e.Mark)
                .WithRequired(e => e.Person);
        }
    }
}

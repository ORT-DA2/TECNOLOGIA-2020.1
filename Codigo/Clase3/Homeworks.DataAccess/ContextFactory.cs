using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace Homeworks.DataAccess
{
    public enum ContextType {
        MEMORY, SQL
    }

    public class ContextFactory
    {
        public HomeworksContext CreateDbContext(string[] args) {
            return GetNewContext();
        }

        public static HomeworksContext GetNewContext(ContextType type = ContextType.SQL) {
            var builder = new DbContextOptionsBuilder<HomeworksContext>();
            DbContextOptions options = null;
            if (type == ContextType.MEMORY) {
                options = GetMemoryConfig(builder);
            } else {
                options = GetSqlConfig(builder);
            }
            return new HomeworksContext(options);
        }

        private static DbContextOptions GetMemoryConfig(DbContextOptionsBuilder builder) {
            builder.UseInMemoryDatabase("HomeworksDB");
            return builder.Options;
        }

        private static DbContextOptions GetSqlConfig(DbContextOptionsBuilder builder) {
            //TODO: Se puede mejorar esto colocando en un archivo externo y obteniendo
            //      desde allí la información.
            builder.UseSqlServer(@"Server=127.0.0.1,1433;Database=HomeworksDB;User Id=sa;Password=Abcd1234;");
            return builder.Options;
        }   
    }
}
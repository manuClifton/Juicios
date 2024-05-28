using Microsoft.EntityFrameworkCore;

namespace VEJuicios.Infrastructure
{
    public static class DbInitializer
    {
        public static void Initialize(SQLServerContext context)
        {
            context.Database.Migrate();
        }
    }
}
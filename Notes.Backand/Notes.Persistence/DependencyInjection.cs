using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Notes.Aplication.Interfaces;

namespace Notes.Persistence
{
    public static class DependencyInjection
    {
        public static void AddPersistense(this IServiceCollection servises,
           IConfiguration configuration )
        {
            var connectionStringDb = configuration["DbConnection"];
            servises.AddDbContext<NotesDbContext> (options =>
            {
                options.UseSqlite (connectionStringDb);
            } );
            servises.AddScoped<INotesDbContext> (provider => provider
            .GetService<NotesDbContext> ( ));
        }
    }
}

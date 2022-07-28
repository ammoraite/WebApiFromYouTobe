using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

using Notes.Aplication;
using Notes.Aplication.Common.Mappings;
using Notes.Aplication.Interfaces;
using Notes.Persistence;

try
{
    #region Builder
    var builder = WebApplication.CreateBuilder (args);

    //builder.Host.UseSerilog (( _, conf ) => conf.WriteTo.Console ( ));

    //builder.Host.UseSerilog (( ctx, conf ) =>
    //{
    //    conf
    //        .MinimumLevel.Debug ( ) //<- Минимальный уровень для всех приемников
    //        .WriteTo.File ("log-.txt", rollingInterval: RollingInterval.Day)
    //        .WriteTo.Console (restrictedToMinimumLevel: LogEventLevel.Information)
    //        .ReadFrom.Configuration (ctx.Configuration)
    //    ;
    //});
    

    builder.Services.AddAutoMapper (config =>
    {
        config.AddProfile (new AssemblyMappingProfile (
            Assembly.GetExecutingAssembly ( )));
        config.AddProfile (new AssemblyMappingProfile (
            typeof(INotesDbContext).Assembly));
    });

    builder.Services.AddApplication();
    
    builder.Services.AddPersistense(builder.Configuration);
    builder.Services.AddControllers ( );
    builder.Services.AddCors (setupOptions =>
    {
        setupOptions.AddPolicy ("AllowAll", policy =>
        {
            policy.AllowAnyHeader ( );
            policy.AllowAnyMethod ( );
            policy.AllowAnyOrigin ( );
        });
    });

    var app = builder.Build ( );
    #endregion

    #region Configure

    using (var scope = app.Services.CreateScope ( ))
    {
        var serviceProvider = scope.ServiceProvider;
        try
        {
            var context = serviceProvider.GetRequiredService<NotesDbContext> ( );
            DbInitializer.Initialize (context);
        }
        catch (Exception exception)
        {
            throw;
        }
    }

    
    //app.UseSerilogRequestLogging ( );

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment ( ))
    {
        app.UseDeveloperExceptionPage ( );
    }
    app.UseRouting ( );
    app.UseHttpsRedirection ( );
    app.UseCors ("AllowAll");
    app.UseEndpoints (endpoints =>
    {
        endpoints.MapControllers ( );
    });
    //app.UseRouting ( );

    //app.UseAuthorization ( );

    //app.MapControllerRoute (
    //    name: "default",
    //    pattern: "{controller=Home}/{action=Index}/{id?}");

    #endregion

    app.Run ( );
}
catch (Exception E)
{
    //Log.Fatal (E, "Сервер рухнул!");
}
finally
{
    //Log.Information ("Shut down complete");
    //Log.CloseAndFlush ( );
}





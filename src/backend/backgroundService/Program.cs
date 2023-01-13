using backgroundService;
using backgroundService.Services;
using infrastructure;
using interfaces;

var host = Host
    .CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddTransient<IRatingService, RatingService>();

        services.AddInfrastructure(context.Configuration);
        
        services.AddHostedService<GameEndedHandler>();
    })
    .Build();

await host.RunAsync();
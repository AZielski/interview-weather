using DataDownloader;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => { services.AddHostedService<Worker>(); })
    .Build();

IConfiguration configuration = host.Services.GetRequiredService<IConfiguration>();

await host.RunAsync();
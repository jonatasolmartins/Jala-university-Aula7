using System.Collections.Generic;
using System.Threading.Tasks;
using Aula7.Api.Models;
using AutoFixture;
using AutoFixture.AutoMoq;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Aula7.Api.Test.Setup;

public class GenericFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly IFixture _fixture;
    private readonly PostgreSqlTestcontainer dbCOntainer;
    public TestRepository<User> TestRepository;
    public GenericFactory()
    {
        _fixture = new Fixture().Customize(new AutoMoqCustomization());
        dbCOntainer = new TestcontainersBuilder<PostgreSqlTestcontainer>()
            .WithDatabase(new PostgreSqlTestcontainerConfiguration()
            {
                Database = "Aula7",
                Username = "sa",
                Password = "sa"
            }).WithImage("postgres:9.6")
            .Build();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(conf =>
        {
            conf.AddInMemoryCollection(new Dictionary<string, string>()
            {
                {"AppSettings:Database:ConnectionString", dbCOntainer.ConnectionString}
            });
        });

        builder.ConfigureServices(services =>
        {
            services.AddSingleton(x =>
            {
                var context = x.GetService<EfContext>();
                return new TestRepository<User>(context);
            });

            var sp = services.BuildServiceProvider();
            using (var scope = sp.CreateScope())
            {
                var scopedService = scope.ServiceProvider;
                TestRepository = scopedService.GetRequiredService<TestRepository<User>>();
            }
        });

    }

    public IFixture GetFixture() => _fixture;

    public async Task InitializeAsync()
    {
       await  dbCOntainer.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await dbCOntainer.DisposeAsync();
        Dispose();
    }
}
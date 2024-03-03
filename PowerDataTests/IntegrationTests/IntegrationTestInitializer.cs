using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PowerDataNet.Infr;
using System;
using System.IO;

namespace PowerDataTests.IntegrationTests
{
    public class IntegrationTestInitializer
    {
        IServiceCollection _serviceCollection;
        IServiceProvider _serviceProvider;
        IConfiguration _configuration;

        public IntegrationTestInitializer()
        {

        }
        public void Init()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            _serviceCollection = new ServiceCollection();
            _serviceCollection.AddSingleton(_configuration);

            _serviceCollection.Bind();

            _serviceProvider = _serviceCollection.BuildServiceProvider();
        }

        public void Init(Action<IServiceCollection> overrideCollection = null)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            _serviceCollection = new ServiceCollection();
            _serviceCollection.AddSingleton(_configuration);

            overrideCollection?.Invoke(_serviceCollection);

            _serviceCollection.Bind();

            _serviceProvider = _serviceCollection.BuildServiceProvider();
        }

        public T GetService<T>()
        {
            return _serviceProvider.GetService<T>();
        }

    }
}

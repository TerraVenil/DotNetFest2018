using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Consul;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace DotNetFest2018.Demo5
{
    public class ConsulConfigurationProvider : ConfigurationProvider
    {
        public ConsulConfigurationProvider(bool reloadOnChange)
        {
            Console.WriteLine($"Initialize of {nameof(ConsulConfigurationProvider)}");

            if (!reloadOnChange)
                return;

            var changeToken = new ConfigurationReloadToken();

            ChangeToken.OnChange(() =>
            {
                Console.WriteLine("Register change token consumer");

                Task.Run(() =>
                {
                    var queryResult1 = QueryResult();

                    Console.WriteLine("Listening to changes.");

                    var queryResult2 = QueryResult(queryResult1.LastIndex);

                    Console.WriteLine("Value changed!");

                    var shouldBeTrue = StructuralComparisons.StructuralEqualityComparer.Equals(
                        queryResult1.Response.Value, queryResult2.Response.Value);

                    Console.WriteLine($"Is previous and current value equal? {shouldBeTrue}.");

                    var previousToken = Interlocked.Exchange(ref changeToken, new ConfigurationReloadToken());

                    Console.WriteLine("[START] Call previous token to reload.");

                    previousToken.OnReload();

                    Console.WriteLine("[END] Call previous token to reload.");
                }); 

                return changeToken;
            }, () =>
            {
                Console.WriteLine("Callback consumer raised.");

                ExecuteLoad(reload: true);

                Console.WriteLine("Trigger tree reloading.");

                OnReload();
            });
        }

        public override void Load() => ExecuteLoad();

        private void ExecuteLoad(bool reload = false)
        {
            if (reload)
                Data = new Dictionary<string, string>();

            var queryResult = QueryResult();

            Data = new Dictionary<string, string>
            {
                { queryResult.Response.Key, Encoding.UTF8.GetString(queryResult.Response.Value) }
            };
        }

        private static QueryResult<KVPair> QueryResult(ulong waitIndex = 0)
        {
            using (var client = new ConsulClient())
            {
                var queryResult = client
                    .KV
                    .Get(ConsulConstants.MinimumLevelKey, new QueryOptions { WaitIndex = waitIndex })
                    .GetAwaiter()
                    .GetResult();

                return queryResult;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using System.IO;

namespace GoogleLocationsReader.CosmosDb
{ 
    public class CosmosDbClient
    {
        private readonly CosmosDbClientConfiguration configuration;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The access token
        /// </summary>
        private readonly string accessToken;

        private DocumentClient client;

        public CosmosDbClient(CosmosDbClientConfiguration configuration)
        {
            this.configuration = configuration;
            client = new DocumentClient(new Uri(configuration.EndpointUrl), configuration.PrimaryKey);


        }

        public async Task<ReturnType> GetDocumentAsync<ReturnType>(string databaseName, string collectionName, string documentId)
        {

            var uri = UriFactory.CreateDocumentUri(databaseName, collectionName, documentId);
            var doc = await client.ReadDocumentAsync(uri);

            return JsonConvert.DeserializeObject<ReturnType>(doc.Resource.ToString());
        }



        public async Task<List<ReturnType>> GetAllDocumentsAsync<ReturnType>(string databaseName, string collectionName)
        {
            try
            {

                var uri = UriFactory.CreateDocumentCollectionUri(databaseName, collectionName);


                //this should give me the ids
                var test = client.CreateDocumentQuery(uri)
                                    .Select(doc => doc.Id)
                                    .AsEnumerable();


                var documents = await client.ReadDocumentFeedAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName));
                var entries = new List<ReturnType>();
                foreach (var d in documents)
                {
                    entries.Add(JsonConvert.DeserializeObject<ReturnType>(d.ToString()));

                }
                return entries;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }


        }
        public ReturnType DeserializeDoc<ReturnType>(string document)
        {
            var doc = JsonConvert.DeserializeObject < ReturnType > (document);
            return doc;
        }

        public ReturnType ReadJson<ReturnType>(string filename)
        {
            using (StreamReader r = new StreamReader(filename))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<ReturnType>(json);
            }

        }



    }
}

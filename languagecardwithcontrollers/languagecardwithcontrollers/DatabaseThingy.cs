using Google.Cloud.Datastore.V1;
using Grpc.Core;
using languagecardwithcontrollers.Models;

namespace languagecardwithcontrollers
{
    public class DatabaseThingy
    {
        private readonly DatastoreDb _db;
        public DatabaseThingy()
        {
            var host = "datastore";
            var port = 8081;

            var dsBuilder = new DatastoreClientBuilder();
            dsBuilder.ChannelCredentials = ChannelCredentials.Insecure;
            dsBuilder.Endpoint = $"{host}:{port}";
            var client = dsBuilder.Build();
            _db = DatastoreDb.Create("langproject", "", client);
        }

        public List<Card> Select()
        {
            List<Card> cards = new List<Card>();
            var results = _db.RunQuery(new Query("Cards"));
            foreach (var item in results.Entities)
            {
                Card c = new Card();
                c.English = item["English"].StringValue;
                c.Welsh = item["Welsh"].StringValue;
                cards.Add(c);
            }

            return cards;
        }

        public void Insert(Card card)
        {
            var entity = new Entity();
            entity["English"] = card.English;
            entity["Welsh"] = card.Welsh;
            entity.Key = _db.CreateKeyFactory("Cards").CreateKey(Guid.NewGuid().ToString());

            _db.Insert(entity);
        }


    }
}

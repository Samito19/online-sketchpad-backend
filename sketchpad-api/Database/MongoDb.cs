
using MongoDB.Driver;
using MongoDB.Bson;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using sketchpad_api.Database.Models;

namespace sketchpad_api.Database;

public class Database
{
    static string connectionUri = "mongodb+srv://samsaf:samisami2460@cluster0.1yonq40.mongodb.net/?retryWrites=true&w=majority";
    static MongoClient? cachedClient;

    public static List<List<long>> getDrawings()
    {
        try
        {
            if (cachedClient == null)
            {
                var settings = MongoClientSettings.FromConnectionString(connectionUri);
                settings.ServerApi = new ServerApi(ServerApiVersion.V1);
                MongoClient client = new MongoClient(settings);

                var result = client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));

                var canvasCollection = client.GetDatabase("sketchpad").GetCollection<Canvas>("canvas");
                var document = canvasCollection.Find(Builders<Canvas>.Filter.Eq("_id", ObjectId.Parse("647dd92367cc32aedbedd487"))).First()!;

                return document.drawings!;
            }
            return new List<List<long>> { };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return new List<List<long>> { };
        }
    }

    public static void addDrawings(List<long> newDrawing)
    {
        try
        {
            if (cachedClient == null)
            {
                var settings = MongoClientSettings.FromConnectionString(connectionUri);
                settings.ServerApi = new ServerApi(ServerApiVersion.V1);
                MongoClient client = new MongoClient(settings);

                var result = client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));

                var canvasCollection = client.GetDatabase("sketchpad").GetCollection<Canvas>("canvas");
                var document = canvasCollection.Find(Builders<Canvas>.Filter.Eq("_id", ObjectId.Parse("647dd92367cc32aedbedd487"))).First()!;

                document.drawings!.AddRange(new List<List<long>> { newDrawing });
                var update = Builders<Canvas>.Update.Set(canvas => canvas.drawings, document.drawings);
                canvasCollection.UpdateOne(Builders<Canvas>.Filter.Eq("_id", ObjectId.Parse("647dd92367cc32aedbedd487")), update);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }

    public static void clearDrawings()
    {
        try
        {
            if (cachedClient == null)
            {
                var settings = MongoClientSettings.FromConnectionString(connectionUri);
                settings.ServerApi = new ServerApi(ServerApiVersion.V1);
                MongoClient client = new MongoClient(settings);

                var result = client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));

                var canvasCollection = client.GetDatabase("sketchpad").GetCollection<Canvas>("canvas");
                var update = Builders<Canvas>.Update.Set(canvas => canvas.drawings, new List<List<long>> { });
                canvasCollection.UpdateOne(Builders<Canvas>.Filter.Eq("_id", ObjectId.Parse("647dd92367cc32aedbedd487")), update);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }
}


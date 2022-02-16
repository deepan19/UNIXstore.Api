using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UNIXstore.Api.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using UNIXstore.Api.Entities;

namespace UNIXstore.Api
{
    public class MongoDbCommandsRepository : ICommandsRepository
    {
        private readonly IMongoCollection<Command> commandsCollection;
        private const string databaseName = "UNIXstore";
        private const string collectionName = "Commands";

        private readonly FilterDefinitionBuilder<Command> filterBuilder = Builders<Command>.Filter;

        public MongoDbCommandsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);

            commandsCollection = database.GetCollection<Command>(collectionName);

        }

        public async Task CreateCommandAsync(Command command)
        {
            await commandsCollection.InsertOneAsync(command);
        }

        public async Task DeleteCommandAsync(Guid id)
        {
            var filter = filterBuilder.Eq(command => command.Id, id);
            await commandsCollection.DeleteOneAsync(filter);
        }

        public async Task<Command> GetCommandAsync(Guid id)
        {
            var filter = filterBuilder.Eq(command => command.Id, id);
            return await commandsCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Command>> GetCommandsAsync()
        {
            return await commandsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task UpdateCommandAsync(Command command)
        {
            var filter = filterBuilder.Eq(existingcommand => existingcommand.Id, command.Id);
            await commandsCollection.ReplaceOneAsync(filter, command);
        }
    }

}
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.Cosmos.Table;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System.Data;
using System.Diagnostics;
using Microsoft.Azure.Documents.Linq;
using Xamarin.Forms;
using BATTS.Helper;
using BATTS.Models;

namespace BATTS.Services
{
    public class AzuCosmoDBManager
    {
        static DocumentClient docClient = null;
     
        static readonly string databaseName = "UserRegistry";
        //Various Collections for CosmoDB
        static readonly string collectionName = "UserData";
        static readonly string collectionTeam = "TeamsData";  
        static readonly string collectionPlayers = "PlayersData";
        static readonly string collectionGames = "GameData";

        static async Task<bool> Initialize()
        {
            if (docClient != null)
                return true;

            try
            {
                docClient = new DocumentClient(new Uri(APIKeys.CosmosEndpointUrl), APIKeys.CosmosAuthKey);

                // Create the database - this can also be done through the portal
                await docClient.CreateDatabaseIfNotExistsAsync(new Database { Id = databaseName });

                // Create the collection - make sure to specify the RUs - has pricing implications
                // This can also be done through the portal

                await docClient.CreateDocumentCollectionIfNotExistsAsync(
                    UriFactory.CreateDatabaseUri(databaseName),
                    new DocumentCollection { Id = collectionName },
                    new RequestOptions { OfferThroughput = 400 }
                );

                await docClient.CreateDocumentCollectionIfNotExistsAsync(
                   UriFactory.CreateDatabaseUri(databaseName),
                   new DocumentCollection { Id = collectionTeam },
                   new RequestOptions { OfferThroughput = 400 }
               );
                await docClient.CreateDocumentCollectionIfNotExistsAsync(
                   UriFactory.CreateDatabaseUri(databaseName),
                   new DocumentCollection { Id = collectionPlayers },
                   new RequestOptions { OfferThroughput = 400 }
               );
                await docClient.CreateDocumentCollectionIfNotExistsAsync(
                   UriFactory.CreateDatabaseUri(databaseName),
                   new DocumentCollection { Id = collectionGames },
                   new RequestOptions { OfferThroughput = 400 }
               );

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                docClient = null;

                return false;
            }

            return true;
        }

        #region User Data Models
        // <GetUserData>
        /// <summary>
        /// Pulls the data from the User Data Model databse and stores into a list
        /// </summary>
        /// <returns></returns>
        public async static Task<List<UserDataModel>> GetUserData()
        {
            var items = new List<UserDataModel>();

            if (!await Initialize())
                return items;

            var itemQuery = docClient.CreateDocumentQuery<UserDataModel>(
                UriFactory.CreateDocumentCollectionUri(databaseName, collectionName),
                new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
                .Where(item => item.ActiveUser == true)
                .AsDocumentQuery();

            while (itemQuery.HasMoreResults)
            {
                var queryResults = await itemQuery.ExecuteNextAsync<UserDataModel>();

                items.AddRange(queryResults);
            }

            return items;
        }
        // <InsertUserData>        
        /// <summary> 
        /// </summary>
        /// <returns></returns>
        public async static Task InsertUserData(UserDataModel item)
        {
            if (!await Initialize())
                return;

            await docClient.CreateDocumentAsync(
                UriFactory.CreateDocumentCollectionUri(databaseName, collectionName),
                item);
        }
        // </InsertToDoItem>  

        // <DeleteUserData>        
        /// <summary> 
        /// </summary>
        /// <returns></returns>
        public async static Task DeleteUserData(UserDataModel item)
        {
            if (!await Initialize())
                return;

            var docUri = UriFactory.CreateDocumentUri(databaseName, collectionName, item.Id);
            await docClient.DeleteDocumentAsync(docUri);
        }
        // </DeleteToDoItem>  

        // <UpdateUserItem>        
        /// <summary> 
        /// </summary>
        /// <returns></returns>
        public async static Task UpdateUserData(UserDataModel item)
        {
            if (!await Initialize())
                return;

            var docUri = UriFactory.CreateDocumentUri(databaseName, collectionName, item.Id);
            await docClient.ReplaceDocumentAsync(docUri, item);
        }
        // </UpdateToDoItem>  


        #endregion       

        #region Team Data Models
        // <GetUserData>

        /// <summary>
        /// Pulls the data from the User Data Model databse and stores into a list
        /// </summary>
        /// <returns></returns>
        public async static Task<List<TeamDataModel>> GetTeamData()
        {
            var items = new List<TeamDataModel>();

            if (!await Initialize())
                return items;

            var itemQuery = docClient.CreateDocumentQuery<TeamDataModel>(
                UriFactory.CreateDocumentCollectionUri(databaseName, collectionTeam),
                new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
                .Where(item => item.ActiveTeam == true)
                .AsDocumentQuery();

            while (itemQuery.HasMoreResults)
            {
                var queryResults = await itemQuery.ExecuteNextAsync<TeamDataModel>();

                items.AddRange(queryResults);
            }

            return items;
        }
        /// <summary>
        /// Pulls the data from the User Data Model databse and stores into a list
        /// </summary>
        /// <returns></returns>
        public async static Task<List<TeamDataModel>> GetTeamDataByID(string ownerID)
        {
            var items = new List<TeamDataModel>();

            if (!await Initialize())
                return items;

            var itemQuery = docClient.CreateDocumentQuery<TeamDataModel>(
                UriFactory.CreateDocumentCollectionUri(databaseName, collectionTeam),
                new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
                .Where(item => item.OwnerID == ownerID)
                .AsDocumentQuery();

            while (itemQuery.HasMoreResults)
            {
                var queryResults = await itemQuery.ExecuteNextAsync<TeamDataModel>();

                items.AddRange(queryResults);
            }

            return items;
        }

        // <InsertUserData>        
        /// <summary> 
        /// </summary>
        /// <returns></returns>
        public async static Task InsertTeamData(TeamDataModel item)
        {
            if (!await Initialize())
                return;

            await docClient.CreateDocumentAsync(
                UriFactory.CreateDocumentCollectionUri(databaseName, collectionTeam),
                item);
        }
        // </InsertToDoItem>  

        // <DeleteUserData>        
        /// <summary> 
        /// </summary>
        /// <returns></returns>
        public async static Task DeleteTeamData(TeamDataModel item)
        {
            if (!await Initialize())
                return;

            var docUri = UriFactory.CreateDocumentUri(databaseName, collectionTeam, item.Id);
            await docClient.DeleteDocumentAsync(docUri);
        }
        // </DeleteToDoItem>  

        // <UpdateUserItem>        
        /// <summary> 
        /// </summary>
        /// <returns></returns>
        public async static Task UpdateTeamData(TeamDataModel item)
        {
            if (!await Initialize())
                return;

            var docUri = UriFactory.CreateDocumentUri(databaseName, collectionTeam, item.Id);
            await docClient.ReplaceDocumentAsync(docUri, item);
        }
        // </UpdateToDoItem>  


        #endregion

        #region Player Data Models
        // <GetUserData>

        /// <summary>
        /// Pulls the data from the User Data Model databse and stores into a list
        /// </summary>
        /// <returns></returns>
        public async static Task<List<PlayerDataModel>> GetPlayerData(string TeamID)
        {
            var items = new List<PlayerDataModel>();

            if (!await Initialize())
                return items;

            var itemQuery = docClient.CreateDocumentQuery<PlayerDataModel>(
                UriFactory.CreateDocumentCollectionUri(databaseName, collectionPlayers),
                new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
                .Where(item => item.TeamID == TeamID)
                .AsDocumentQuery();

            while (itemQuery.HasMoreResults)
            {
                var queryResults = await itemQuery.ExecuteNextAsync<PlayerDataModel>();

                items.AddRange(queryResults);
            }

            return items;
        }


        /// <summary>
        /// Pulls the data from the User Data Model databse and stores into a list
        /// </summary>
        /// <returns></returns>
        public async static Task<List<PlayerDataModel>> GetPlayerDataByID(string PlayerID)
        {
            var items = new List<PlayerDataModel>();

            if (!await Initialize())
                return items;

            var itemQuery = docClient.CreateDocumentQuery<PlayerDataModel>(
                UriFactory.CreateDocumentCollectionUri(databaseName, collectionPlayers),
                new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
                .Where(item => item.Id == PlayerID)
                .AsDocumentQuery();

            while (itemQuery.HasMoreResults)
            {
                var queryResults = await itemQuery.ExecuteNextAsync<PlayerDataModel>();

                items.AddRange(queryResults);
            }

            return items;
        }

        // <InsertUserData>        
        /// <summary> 
        /// This function 
        /// </summary>
        /// <returns></returns>
        public async static Task InsertPlayerData(PlayerDataModel item)
        {
            if (!await Initialize())
                return;

            await docClient.CreateDocumentAsync(
                UriFactory.CreateDocumentCollectionUri(databaseName, collectionPlayers),
                item);
        }
        // </InsertToDoItem>  

        // <DeleteUserData>        
        /// <summary> 
        /// </summary>
        /// <returns></returns>
        public async static Task DeletePlayerData(PlayerDataModel item)
        {
            if (!await Initialize())
                return;

            var docUri = UriFactory.CreateDocumentUri(databaseName, collectionPlayers, item.Id);
            await docClient.DeleteDocumentAsync(docUri);
        }
        // </DeleteToDoItem>  

        // <UpdateUserItem>        
        /// <summary> 
        /// </summary>
        /// <returns></returns>
        public async static Task UpdatePlayerData(PlayerDataModel item)
        {
            if (!await Initialize())
                return;

            var docUri = UriFactory.CreateDocumentUri(databaseName, collectionPlayers, item.Id);
            await docClient.ReplaceDocumentAsync(docUri, item);
        }
        // </UpdateToDoItem>  


        #endregion

        #region Game Models
        // <GetUserData>

        /// <summary>
        /// Pulls the data from the User Data Model databse and stores into a list
        /// </summary>
        /// <returns></returns>
        public async static Task<List<GameModel>> GetGameData(string GameID)
        {
            var items = new List<GameModel>();

            if (!await Initialize())
                return items;

            var itemQuery = docClient.CreateDocumentQuery<GameModel>(
                UriFactory.CreateDocumentCollectionUri(databaseName, collectionGames),
                new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
                .Where(item => item.GameId == GameID)
                .AsDocumentQuery();

            while (itemQuery.HasMoreResults)
            {
                var queryResults = await itemQuery.ExecuteNextAsync<GameModel>();

                items.AddRange(queryResults);
            }

            return items;
        }

        /// <summary>
        /// Pulls the data from the User Data Model databse and stores into a list
        /// </summary>
        /// <returns></returns>
        public async static Task<List<GameModel>> GetGameDataByPlayer(string PlayerID)
        {
            var items = new List<GameModel>();

            if (!await Initialize())
                return items;

            var itemQuery = docClient.CreateDocumentQuery<GameModel>(
                UriFactory.CreateDocumentCollectionUri(databaseName, collectionGames),
                new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
                .Where(item => item.PlayerIDs == PlayerID)
                .AsDocumentQuery();

            while (itemQuery.HasMoreResults)
            {
                var queryResults = await itemQuery.ExecuteNextAsync<GameModel>();

                items.AddRange(queryResults);
            }

            return items;
        }



        // <InsertUserData>        
        /// <summary> 
        /// This function 
        /// </summary>
        /// <returns></returns>
        public async static Task InsertGameData(GameModel item)
        {
            if (!await Initialize())
                return;

            await docClient.CreateDocumentAsync(
                UriFactory.CreateDocumentCollectionUri(databaseName, collectionGames),
                item);
        }
        // </InsertToDoItem>  

        // <DeleteUserData>        
        /// <summary> 
        /// </summary>
        /// <returns></returns>
        public async static Task DeleteGameData(GameModel item)
        {
            if (!await Initialize())
                return;

            var docUri = UriFactory.CreateDocumentUri(databaseName, collectionGames, item.Id);
            await docClient.DeleteDocumentAsync(docUri);
        }
        // </DeleteToDoItem>  

        // <UpdateUserItem>        
        /// <summary> 
        /// </summary>
        /// <returns></returns>
        public async static Task UpdateGameData(GameModel item)
        {
            if (!await Initialize())
                return;

            var docUri = UriFactory.CreateDocumentUri(databaseName, collectionGames, item.Id);
            await docClient.ReplaceDocumentAsync(docUri, item);
        }
        // </UpdateToDoItem>  


        #endregion



    }
}

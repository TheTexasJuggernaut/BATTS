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
        static DocumentClient docClient2 = null;
        static DocumentClient docClient3 = null;
        static DocumentClient docClient4 = null;


        static readonly string databaseName = "UserRegistry";
        static readonly string collectionName = "UserData";

        static readonly string databaseTeam = "UserRegistry";
        static readonly string collectionTeam = "TeamsData";

        static readonly string databasePlayers = "UserRegistry";
        static readonly string collectionPlayers= "PlayersData";
        static readonly string databaseGames = "UserRegistry";
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
                .Where(item => item.ActiveUser == true )
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

        static async Task<bool> InitializeTeams()
        {
            if (docClient2 != null)
                return true;

            try
            {
                docClient2 = new DocumentClient(new Uri(APIKeys.CosmosEndpointUrl), APIKeys.CosmosAuthKey);

                // Create the database - this can also be done through the portal
                await docClient2.CreateDatabaseIfNotExistsAsync(new Database { Id = databaseTeam });

                // Create the collection - make sure to specify the RUs - has pricing implications
                // This can also be done through the portal

                await docClient2.CreateDocumentCollectionIfNotExistsAsync(
                    UriFactory.CreateDatabaseUri(databaseTeam),
                    new DocumentCollection { Id = collectionTeam },
                    new RequestOptions { OfferThroughput = 400 }
                );

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                docClient2 = null;

                return false;
            }

            return true;
        }

        #region Team Data Models
        // <GetUserData>

        /// <summary>
        /// Pulls the data from the User Data Model databse and stores into a list
        /// </summary>
        /// <returns></returns>
        public async static Task<List<TeamDataModel>> GetTeamData()
        {
            var items = new List<TeamDataModel>();

            if (!await InitializeTeams())
                return items;

            var itemQuery = docClient2.CreateDocumentQuery<TeamDataModel>(
                UriFactory.CreateDocumentCollectionUri(databaseTeam, collectionTeam),
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

            if (!await InitializeTeams())
                return items;

            var itemQuery = docClient2.CreateDocumentQuery<TeamDataModel>(
                UriFactory.CreateDocumentCollectionUri(databaseTeam, collectionTeam),
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
            if (!await InitializeTeams())
                return;

            await docClient2.CreateDocumentAsync(
                UriFactory.CreateDocumentCollectionUri(databaseTeam, collectionTeam),
                item);
        }
        // </InsertToDoItem>  

        // <DeleteUserData>        
        /// <summary> 
        /// </summary>
        /// <returns></returns>
        public async static Task DeleteTeamData(TeamDataModel item)
        {
            if (!await InitializeTeams())
                return;

            var docUri = UriFactory.CreateDocumentUri(databaseTeam, collectionTeam, item.Id);
            await docClient2.DeleteDocumentAsync(docUri);
        }
        // </DeleteToDoItem>  

        // <UpdateUserItem>        
        /// <summary> 
        /// </summary>
        /// <returns></returns>
        public async static Task UpdateTeamData(TeamDataModel item)
        {
            if (!await InitializeTeams())
                return;

            var docUri = UriFactory.CreateDocumentUri(databaseTeam, collectionTeam, item.Id);
            await docClient2.ReplaceDocumentAsync(docUri, item);
        }
        // </UpdateToDoItem>  


        #endregion

        static async Task<bool> InitializePlayers()
        {
            if (docClient3 != null)
                return true;

            try
            {
                docClient3 = new DocumentClient(new Uri(APIKeys.CosmosEndpointUrl), APIKeys.CosmosAuthKey);

                // Create the database - this can also be done through the portal
                await docClient3.CreateDatabaseIfNotExistsAsync(new Database { Id = databasePlayers });

                // Create the collection - make sure to specify the RUs - has pricing implications
                // This can also be done through the portal

                await docClient3.CreateDocumentCollectionIfNotExistsAsync(
                    UriFactory.CreateDatabaseUri(databasePlayers),
                    new DocumentCollection { Id = collectionPlayers },
                    new RequestOptions { OfferThroughput = 400 }
                );

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                docClient3 = null;

                return false;
            }

            return true;
        }

        #region Player Data Models
        // <GetUserData>

        /// <summary>
        /// Pulls the data from the User Data Model databse and stores into a list
        /// </summary>
        /// <returns></returns>
        public async static Task<List<PlayerDataModel>> GetPlayerData(string TeamID)
        {
            var items = new List<PlayerDataModel>();

            if (!await InitializePlayers())
                return items;

            var itemQuery = docClient3.CreateDocumentQuery<PlayerDataModel>(
                UriFactory.CreateDocumentCollectionUri(databasePlayers, collectionPlayers),
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

            if (!await InitializePlayers())
                return items;

            var itemQuery = docClient3.CreateDocumentQuery<PlayerDataModel>(
                UriFactory.CreateDocumentCollectionUri(databasePlayers, collectionPlayers),
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
            if (!await InitializePlayers())
                return;

            await docClient3.CreateDocumentAsync(
                UriFactory.CreateDocumentCollectionUri(databasePlayers, collectionPlayers),
                item);
        }
        // </InsertToDoItem>  

        // <DeleteUserData>        
        /// <summary> 
        /// </summary>
        /// <returns></returns>
        public async static Task DeletePlayerData(PlayerDataModel item)
        {
            if (!await InitializePlayers())
                return;

            var docUri = UriFactory.CreateDocumentUri(databasePlayers, collectionPlayers, item.Id);
            await docClient3.DeleteDocumentAsync(docUri);
        }
        // </DeleteToDoItem>  

        // <UpdateUserItem>        
        /// <summary> 
        /// </summary>
        /// <returns></returns>
        public async static Task UpdatePlayerData(PlayerDataModel item)
        {
            if (!await InitializePlayers())
                return;

            var docUri = UriFactory.CreateDocumentUri(databasePlayers, collectionPlayers, item.Id);
            await docClient3.ReplaceDocumentAsync(docUri, item);
        }
        // </UpdateToDoItem>  


        #endregion

        static async Task<bool> InitializeGames()
        {
            if (docClient4 != null)
                return true;

            try
            {
                docClient4 = new DocumentClient(new Uri(APIKeys.CosmosEndpointUrl), APIKeys.CosmosAuthKey);

                // Create the database - this can also be done through the portal
                await docClient4.CreateDatabaseIfNotExistsAsync(new Database { Id = databaseGames });

                // Create the collection - make sure to specify the RUs - has pricing implications
                // This can also be done through the portal

                await docClient4.CreateDocumentCollectionIfNotExistsAsync(
                    UriFactory.CreateDatabaseUri(databaseGames),
                    new DocumentCollection { Id = collectionGames },
                    new RequestOptions { OfferThroughput = 400 }
                );

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                docClient4 = null;

                return false;
            }

            return true;
        }

        #region Game Models
        // <GetUserData>

        /// <summary>
        /// Pulls the data from the User Data Model databse and stores into a list
        /// </summary>
        /// <returns></returns>
        public async static Task<List<GameModel>> GetGameData(string GameID)
        {
            var items = new List<GameModel>();

            if (!await InitializeGames())
                return items;

            var itemQuery = docClient4.CreateDocumentQuery<GameModel>(
                UriFactory.CreateDocumentCollectionUri(databaseGames, collectionGames),
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


        

        // <InsertUserData>        
        /// <summary> 
        /// This function 
        /// </summary>
        /// <returns></returns>
        public async static Task InsertGameData(GameModel item)
        {
            if (!await InitializeGames())
                return;

            await docClient4.CreateDocumentAsync(
                UriFactory.CreateDocumentCollectionUri(databaseGames, collectionGames),
                item);
        }
        // </InsertToDoItem>  

        // <DeleteUserData>        
        /// <summary> 
        /// </summary>
        /// <returns></returns>
        public async static Task DeleteGameData(GameModel item)
        {
            if (!await InitializeGames())
                return;

            var docUri = UriFactory.CreateDocumentUri(databaseGames, collectionGames, item.Id);
            await docClient4.DeleteDocumentAsync(docUri);
        }
        // </DeleteToDoItem>  

        // <UpdateUserItem>        
        /// <summary> 
        /// </summary>
        /// <returns></returns>
        public async static Task UpdateGameData(GameModel item)
        {
            if (!await InitializeGames())
                return;

            var docUri = UriFactory.CreateDocumentUri(databaseGames, collectionGames, item.Id);
            await docClient4.ReplaceDocumentAsync(docUri, item);
        }
        // </UpdateToDoItem>  


        #endregion


       
    }
}

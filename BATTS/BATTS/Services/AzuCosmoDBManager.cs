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
        static readonly string collectionName = "UserData";

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

        //#region Item Model Calls
        //// <GetToDoItems>        
        ///// <summary> 
        ///// Hi
        ///// </summary>
        ///// <returns></returns>
        //public async static Task<List<Item>> GetItems()
        //{
        //    var items = new List<Item>();

        //    if (!await Initialize())
        //        return items;

        //    var itemQuery = docClient.CreateDocumentQuery<Item>(
        //        UriFactory.CreateDocumentCollectionUri(databaseName, collectionName),
        //        new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
        //        .Where(item => item.Completed == false)
        //        .AsDocumentQuery();

        //    while (itemQuery.HasMoreResults)
        //    {
        //        var queryResults = await itemQuery.ExecuteNextAsync<Item>();

        //       items.AddRange(queryResults);
        //    }

        //    return items;
        //}
        //// </GetToDoItems>


        //// <GetCompletedToDoItems>        
        ///// <summary> 
        ///// Sorts the list to find items that are completed
        ///// </summary>
        ///// <returns></returns>
        //public async static Task<List<Item>> GetCompletedItems()
        //{
        //    var items = new List<Item>();

        //    if (!await Initialize())
        //        return items;

        //    var completedItemQuery = docClient.CreateDocumentQuery<Item>(
        //        UriFactory.CreateDocumentCollectionUri(databaseName, collectionName),
        //        new FeedOptions { MaxItemCount = -1, EnableCrossPartitionQuery = true })
        //        .Where(item => item.Completed == true)
        //        .AsDocumentQuery();

        //    while (completedItemQuery.HasMoreResults)
        //    {
        //        var queryResults = await completedItemQuery.ExecuteNextAsync<Item>();

        //        items.AddRange(queryResults);
        //    }

        //    return items;
        //}
        //// </GetCompletedToDoItems>


        //// <CompleteToDoItem>        
        ///// <summary> 
        ///// </summary>
        ///// <returns></returns>
        //public async static Task CompleteItem(Item item)
        //{
        //    item.Completed = true;

        //    await UpdateItem(item);
        //}
        //// </CompleteToDoItem>


        //// <InsertToDoItem>        
        ///// <summary> 
        ///// </summary>
        ///// <returns></returns>
        //public async static Task InsertItem(Item item)
        //{
        //    if (!await Initialize())
        //        return;

        //    await docClient.CreateDocumentAsync(
        //        UriFactory.CreateDocumentCollectionUri(databaseName, collectionName),
        //        item);
        //}
        //// </InsertToDoItem>  

        //// <DeleteToDoItem>        
        ///// <summary> 
        ///// </summary>
        ///// <returns></returns>
        //public async static Task DeleteItem(Item item)
        //{
        //    if (!await Initialize())
        //        return;

        //    var docUri = UriFactory.CreateDocumentUri(databaseName, collectionName, item.Id);
        //    await docClient.DeleteDocumentAsync(docUri);
        //}
        //// </DeleteToDoItem>  

        //// <UpdateToDoItem>        
        ///// <summary> 
        ///// </summary>
        ///// <returns></returns>
        //public async static Task UpdateItem(Item item)
        //{
        //    if (!await Initialize())
        //        return;

        //    var docUri = UriFactory.CreateDocumentUri(databaseName, collectionName, item.Id);
        //    await docClient.ReplaceDocumentAsync(docUri, item);
        //}
        //// </UpdateToDoItem>  
        //#endregion
    }
}

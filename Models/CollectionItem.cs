using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Inventory;

namespace Inventory.Models
{
    public class CollectionItem
    {
        private int _id;
        private string _description;

        public CollectionItem(int id, string description)
        {
            _id = id;
            _description = description;
        }

        public CollectionItem(string description)
        {
            _description = description;
        }

        public int GetId()
        {
            return _id;
        }

        public string GetDescription()
        {
            return _description;
        }

        public override bool Equals(System.Object otherItem)
        {
            if (!(otherItem is CollectionItem))
            {
                return false;
            }
            else
            {
                CollectionItem newItem = (CollectionItem) otherItem;
                bool idEquality = (this.GetId() == newItem.GetId());
                bool descriptionEquality = (this.GetDescription() == newItem.GetDescription());
                return (idEquality && descriptionEquality);
            }
        }

        public void Save()
            {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO CollectionItems (description) VALUES (@ItemDescription);";

            MySqlParameter description = new MySqlParameter();
            description.ParameterName = "@ItemDescription";
            description.Value = this._description;
            cmd.Parameters.Add(description);

            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;

            conn.Close();
            if (conn !=null)
            {
                conn.Dispose();
            }
            }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM CollectionItems;";

            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<CollectionItem> GetAll()
        {
            List<CollectionItem> allCollectionItems = new List<CollectionItem> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM CollectionItems;";

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int ItemId = rdr.GetInt32(0);
                string ItemName = rdr.GetString(1);
                CollectionItem newCollectionItem = new CollectionItem(ItemId, ItemName);
                allCollectionItems.Add(newCollectionItem);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allCollectionItems;
        }

         public static List<CollectionItem> Find(string FindName)
        {
            List<CollectionItem> allCollectionItems = new List<CollectionItem> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM CollectionItems WHERE description = '" + FindName + "';";

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int ItemId = rdr.GetInt32(0);
                string ItemName = rdr.GetString(1);
                CollectionItem newCollectionItem = new CollectionItem(ItemId, ItemName);
                allCollectionItems.Add(newCollectionItem);
            }

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allCollectionItems;
        }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using CloudSpritzers.src.model.chat;

namespace CloudSpritzers.src.repository
{
	public class ChatDBRepository : IRepository<int, Chat>
	{
		private readonly string _connectionString;

        /// <summary>
        /// Helper method to map a reader row to a Chat object to respect DRY 
        /// </summary>
        /// <param name="reader"> SqlDataReader </param>
        /// <returns> Chat object </returns>
        private Chat MapRowToChat(SqlDataReader reader)
        {
            int chatId = reader.GetInt32(reader.GetOrdinal("chat_id"));
            int userId = reader.GetInt32(reader.GetOrdinal("user_id"));

            string statusStr = reader.GetString(reader.GetOrdinal("status"));
            ChatStatus status = (ChatStatus)Enum.Parse(typeof(ChatStatus), statusStr);

            return new Chat(chatId, userId, status);
        }

		public ChatDBRepository(string connectionString)
		{
			_connectionString = connectionString;
		}

        public int Add(Chat elem)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Chat (user_id, status) " +
                               "VALUES (@userId, @status); SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", elem.UserId);
                cmd.Parameters.AddWithValue("@status", elem.Status.ToString());

                conn.Open();
                
                return Convert.ToInt32(cmd.ExecuteScalar()); //id of new row
            }
        }

        public void DeleteById(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Chat WHERE chat_id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateById(int id, Chat elem)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Chat SET user_id = @userId,status = @status " +
                               "WHERE chat_id = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", elem.UserId);
                cmd.Parameters.AddWithValue("@status", elem.Status.ToString());
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows == 0) throw new KeyNotFoundException($"Chat with ID {id} not found.");
            }
        }

        public IEnumerable<Chat> GetAll()
        {
            List<Chat> chats = new List<Chat>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Chat";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) chats.Add(MapRowToChat(reader));
                }
            }
            return chats;
        }

        public Chat GetById(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Chat WHERE chat_id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) return MapRowToChat(reader);
                }
            }
            throw new KeyNotFoundException($"Chat with id {id} not found.");

        }

    }

}
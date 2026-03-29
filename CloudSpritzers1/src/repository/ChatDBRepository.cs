using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using CloudSpritzers1.src.model.chat;

namespace CloudSpritzers1.src.repository
{
	public class ChatDBRepository : DBRepository<int, Chat>, IRepository<int, Chat>
	{

        /// <summary>
        /// Helper method to map a reader row to a Chat object to respect DRY 
        /// </summary>
        /// <param name="reader"> SqlDataReader </param>
        /// <returns> Chat object </returns>
        protected override Chat MapRowToEntity(SqlDataReader reader)
        {
            int chatId = reader.GetInt32(reader.GetOrdinal("chat_id"));
            int userId = reader.GetInt32(reader.GetOrdinal("user_id"));
            string statusStr = reader.GetString(reader.GetOrdinal("status"));
            ChatStatus status = (ChatStatus)Enum.Parse(typeof(ChatStatus), statusStr);

            return new Chat(chatId, userId, status);
        }

        protected override int GetEntityId(Chat entity)
        {
            return entity.ChatId;
        }

        public int Add(Chat elem)
        {
            string query = "INSERT INTO Chat (user_id, status) " +
                               "VALUES (@userId, @status); SELECT SCOPE_IDENTITY();";

            var cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@userId", elem.UserId);
            cmd.Parameters.AddWithValue("@status", elem.Status.ToString());

            return Add(cmd, elem);
        }

        public void DeleteById(int id)
        {
            string query = "DELETE FROM Chat WHERE chat_id = @id";
            var cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@id", id);

            DeleteById(id, cmd); 
        }

        public void UpdateById(int id, Chat chat)
        {
            string query = "UPDATE Chat SET user_id = @userId, status = @status WHERE chat_id = @id";
            var cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@userId", chat.UserId);
            cmd.Parameters.AddWithValue("@status", chat.Status.ToString());
            cmd.Parameters.AddWithValue("@id", id);

            UpdateById(id, cmd, chat); 
        }

        public IEnumerable<Chat> GetAll()
        {
            string query = "SELECT * FROM Chat";
            var cmd = new SqlCommand(query);

            return GetAll(cmd);
        }

        public Chat GetById(int id)
        {
            string query = "SELECT * FROM Chat WHERE chat_id = @id";
            var cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@id", id);

            return GetById(id, cmd) ?? throw new KeyNotFoundException($"Chat with id {id} not found.");
        }

    }

}
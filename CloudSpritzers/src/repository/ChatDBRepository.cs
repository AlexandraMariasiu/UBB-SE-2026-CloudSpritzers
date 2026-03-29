using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            // TODO: check if fields in DB are the same
            int chatId = reader.GetInt32(reader.GetOrdinal("chat_id"));
            int userId = reader.GetInt32(reader.GetOrdinal("user_id"));

            int empOrdinal = reader.GetOrdinal("employee_id");
            int employeeId = reader.IsDBNull(empOrdinal) ? 0 : reader.GetInt32(empOrdinal);

            string statusStr = reader.GetString(reader.GetOrdinal("status"));
            ChatStatus status = (ChatStatus)Enum.Parse(typeof(ChatStatus), statusStr);

            return new Chat(chatId, userId, employeeId, status);
        }

		public ChatDBRepository(string connectionString)
		{
			_connectionString = connectionString;
		}

        public int Add(Chat elem)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Chat (user_id, employee_id, status) " +
                               "VALUES (@userId, @empId, @status); SELECT SCOPE_IDENTITY();";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", elem.UserId);
                cmd.Parameters.AddWithValue("@empId", elem.EmployeeId);
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
                string query = "UPDATE Chat SET user_id = @userId, employee_id = @empId, status = @status " +
                               "WHERE chat_id = @id";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", elem.UserId);
                cmd.Parameters.AddWithValue("@empId", elem.EmployeeId);
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

        public IEnumerable<Chat> GetUnresolvedChats()
		{
            List<Chat> chats = new List<Chat>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Chat WHERE status != @closedStatus";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@closedStatus", ChatStatus.Closed.ToString());

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) chats.Add(MapRowToChat(reader));
                }
            }
            return chats;
        }

        public IEnumerable<Chat> GetUnansweredChats()
        {
            List<Chat> chats = new List<Chat>();
            using (SqlConnection conn =new SqlConnection(_connectionString))
            {
                string query = @"
                    SELECT c.chat_id, c.user_id, c.employee_id, c.status
                    FROM Chat c
                    INNER JOIN (
                        SELECT chat_id, MAX(message_id) as latest_msg_id
                        FROM Message
                        GROUP BY chat_id
                    ) last_msg ON c.chat_id = last_msg.chat_id
                    INNER JOIN [Message] m ON m.message_id = last_msg.latest_msg_id
                    INNER JOIN Sender s ON m.sender_id = s.sender_id
                    WHERE s.[user_id] IS NOT NULL AND c.[status] != 'Closed'
                    ORDER BY m.timestamp DESC";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        chats.Add(MapRowToChat(reader));
                    }
                }
            }
            return chats;
        }
    }

}
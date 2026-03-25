usingusing System;
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

namespace CloudSpritzers.src.repository
{
	public class ChatDBRepository : IRepository<int, Chat>
	{
		private string _connectionString;

		public ChatDBRepository(string connectionString)
		{
			_connectionString = connectionString;
		}

		public IEnumerable<ChatDBRepository> GetUnresolvedCHats()
		{
			List<Chat> chats = new List<Chat>();

			using(SqlConnection conn = new SqlConnection(_connectionString))
			{
				conn.Open();

				// Might need to be modified
				string Query = "SELECT * FROM Chats WHERE Status != 'Closed' ";

				SqlCommand
			}
		}
	}
}
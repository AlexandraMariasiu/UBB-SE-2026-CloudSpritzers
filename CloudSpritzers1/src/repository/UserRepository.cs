using CloudSpritzers1.src.model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSpritzers1.src.repository
{
    public class UserRepository : DBRepository<int, User>, IRepository<int, User>
    {
        public int Add(User elem)
        {
            if (elem == null)
                throw new ArgumentNullException(nameof(elem), "User cannot be null.");

            string query = "INSERT INTO [User] " +
                "(name, email) " +
                "OUTPUT INSERTED.user_id " +
                "VALUES (@name, @email)";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@name", elem.GetName());
            command.Parameters.AddWithValue("@email", elem.GetEmail());

            int id = base.Add(command, elem);
            return id;
        }

        public void DeleteById(int id)
        {
            string query = "DELETE FROM [User] WHERE user_id = @id";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@id", id);

            base.DeleteById(id, command);
        }

        public IEnumerable<User> GetAll()
        {
            string query = "SELECT * FROM [User]";
            SqlCommand command = new SqlCommand(query);
            return base.GetAll(command);
        }

        public User GetById(int id)
        {
            string query = "SELECT * FROM [User] WHERE user_id = @id";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@id", id);

            User user = base.GetById(id, command);

            if (user == null)
                throw new KeyNotFoundException($"User with id {id} was not found.");

            return user;
        }

        public void UpdateById(int id, User elem)
        {
            if (elem == null)
                throw new ArgumentNullException(nameof(elem), "User cannot be null.");

            string query = "UPDATE [User] SET " +
                "name = @name, " +
                "email = @email " +
                "WHERE user_id = @id";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@name", elem.GetName());
            command.Parameters.AddWithValue("@email", elem.GetEmail());
            

            base.UpdateById(id, command, elem);
        }

        protected override int GetEntityId(User entity)
        {
            return entity.UserId;
        }

        protected override User MapRowToEntity(SqlDataReader reader)
        {
            int userId = reader.GetInt32(reader.GetOrdinal("user_id"));
            string name = reader.GetString(reader.GetOrdinal("name"));
            string email = reader.GetString(reader.GetOrdinal("email"));

            return new User(userId, name, email);
        }
    }
}

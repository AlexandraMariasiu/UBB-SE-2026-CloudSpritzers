using System;
using System.Collections.Generic;
using System.Linq;
using CloudSpritzers1.src.model;
using CloudSpritzers1.src.model.review;
using Microsoft.Data.SqlClient;
using CloudSpritzers1.src.repository.interfaces;

namespace CloudSpritzers1.src.repository
{
    public class ReviewRepository : DBRepository<int, Review>, IRepository<int, Review>
    {
        private UserRepository _userRepository = new UserRepository();

        public ReviewRepository() { }

        public Review GetById(int id)
        {
            string query = "SELECT * FROM Review WHERE review_id = @id";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@id", id);

            Review review = base.GetById(id, command);

            if (review == null)
                throw new KeyNotFoundException($"Review with id {id} was not found.");

            return review;
        }

        public IEnumerable<Review> GetAll()
        {
            string query = "SELECT * FROM Review";
            SqlCommand command = new SqlCommand(query);
            return base.GetAll(command);
        }

        public int Add(Review elem)
        {
            if (elem == null)
                throw new ArgumentNullException(nameof(elem), "Review cannot be null.");

            string query = "INSERT INTO Review " +
                "(user_id, message, duty_free_rating, flight_experience_rating, staff_friendliness_rating, cleanliness_rating) " +
                "OUTPUT INSERTED.review_id " +
                "VALUES (@userId, @message, @dutyFree, @flightExp, @staff, @clean)";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@userId", elem.GetUser().UserId);
            command.Parameters.AddWithValue("@message", elem.GetMessage());
            command.Parameters.AddWithValue("@dutyFree", elem.GetDutyFreeRating());
            command.Parameters.AddWithValue("@flightExp", elem.GetFlightExperienceRating());
            command.Parameters.AddWithValue("@staff", elem.GetStaffFriendlinessRating());
            command.Parameters.AddWithValue("@clean", elem.GetCleanlinessRating());

            int id = base.Add(command, elem);
            return id;
        }

        public void UpdateById(int id, Review elem)
        {
            if (elem == null)
                throw new ArgumentNullException(nameof(elem), "Review cannot be null.");

            string query = "UPDATE Review SET " +
                "user_id = @userId, " +
                "message = @message, " +
                "duty_free_rating = @dutyFree, " +
                "flight_experience_rating = @flightExp, " +
                "staff_friendliness_rating = @staff, " +
                "cleanliness_rating = @clean " +
                "WHERE review_id = @id";

            SqlCommand command = new SqlCommand(query);

            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@userId", elem.GetUser().UserId);
            command.Parameters.AddWithValue("@message", elem.GetMessage());
            command.Parameters.AddWithValue("@dutyFree", elem.GetDutyFreeRating());
            command.Parameters.AddWithValue("@flightExp", elem.GetFlightExperienceRating());
            command.Parameters.AddWithValue("@staff", elem.GetStaffFriendlinessRating());
            command.Parameters.AddWithValue("@clean", elem.GetCleanlinessRating());

            base.UpdateById(id, command, elem);
        }

        public void DeleteById(int id)
        {
            string query = "DELETE FROM Review WHERE review_id = @id";
            SqlCommand command = new SqlCommand(query);
            command.Parameters.AddWithValue("@id", id);

            base.DeleteById(id, command);
        }

        protected override Review MapRowToEntity(SqlDataReader reader)
        {
            int id = reader.GetInt32(reader.GetOrdinal("review_id"));
            int userId = reader.GetInt32(reader.GetOrdinal("user_id"));
            string message = reader.GetString(reader.GetOrdinal("message"));
            int dutyFreeRating = reader.GetInt32(reader.GetOrdinal("duty_free_rating"));
            int flightExperienceRating = reader.GetInt32(reader.GetOrdinal("flight_experience_rating"));
            int staffFriendlinessRating = reader.GetInt32(reader.GetOrdinal("staff_friendliness_rating"));
            int cleanlinessRating = reader.GetInt32(reader.GetOrdinal("cleanliness_rating"));

            User user = _userRepository.GetById(userId);

            return new Review(id, user, message, dutyFreeRating, flightExperienceRating, staffFriendlinessRating, cleanlinessRating);
        }

        protected override int GetEntityId(Review entity)
        {
            return entity.GetId();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using CloudSpritzers1.Src.Model.Faq;
using Microsoft.Data.SqlClient;
using CloudSpritzers1.Src.Repository.Database;
using CloudSpritzers1.Src.Repository.Interfaces;

namespace CloudSpritzers1.Src.Repository.Implementation
{
  public class FAQRepository : DatabaseRepository<int, FAQEntry>, IFAQRepository
    {
    public FAQRepository()
        {
        }

    public FAQEntry GetById(int askedQuestionId)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM FAQEntry WHERE FAQentry_id = @id");
            command.Parameters.AddWithValue("@id", askedQuestionId);

            FAQEntry frequentlyAskedQuestion = GetById(askedQuestionId, command);

            if (frequentlyAskedQuestion == null)
            {
                throw new KeyNotFoundException($"FAQ with id {askedQuestionId} was not found.");
            }

            return frequentlyAskedQuestion;
        }

    public int CreateNewEntity(FAQEntry questionEntity)
    {
            if (questionEntity == null)
            {
                throw new ArgumentNullException(nameof(questionEntity), "FAQ entry cannot be null.");
            }

            SqlCommand command = new SqlCommand(
                "INSERT INTO FAQEntry (question, answer, category) " +
                "OUTPUT INSERTED.FAQentry_id " +
                "VALUES (@question, @answer, @category)");

            command.Parameters.AddWithValue("@question", questionEntity.Question);
            command.Parameters.AddWithValue("@answer", questionEntity.Answer);
            command.Parameters.AddWithValue("@category", questionEntity.Category.ToString());

            int addedEntityId = Add(command, questionEntity);
            InvalidateCacheEntry(addedEntityId);
            return addedEntityId;
        }

    public void UpdateById(int identificationNumber, FAQEntry questionEntity)
    {
            if (questionEntity == null)
            {
                throw new ArgumentNullException(nameof(questionEntity), "FAQ entry cannot be null.");
            }

        SqlCommand command = new SqlCommand(
            "UPDATE FAQEntry " +
            "SET question = @question, " +
            "answer = @answer, " +
            "category = @category, " +
            "view_count = @viewCount, " +
            "was_helpful_votes = @wasHelpfulVotes, " +
            "was_not_helpful_votes = @wasNotHelpfulVotes " +
            "WHERE FAQentry_id = @id");

        command.Parameters.AddWithValue("@id", identificationNumber);
        command.Parameters.AddWithValue("@question", questionEntity.Question);
        command.Parameters.AddWithValue("@answer", questionEntity.Answer);
        command.Parameters.AddWithValue("@category", questionEntity.Category.ToString());
        command.Parameters.AddWithValue("@viewCount", questionEntity.ViewCount);
        command.Parameters.AddWithValue("@wasHelpfulVotes", questionEntity.HelpfulVotesCount);
        command.Parameters.AddWithValue("@wasNotHelpfulVotes", questionEntity.NotHelpfulVotesCount);

        UpdateById(identificationNumber, command, questionEntity);
        InvalidateCacheEntry(identificationNumber);
    }

    public void DeleteById(int identificationNumber)
    {
            SqlCommand command = new SqlCommand("DELETE FROM FAQEntry WHERE FAQentry_id = @id");
            command.Parameters.AddWithValue("@id", identificationNumber);

            DeleteById(identificationNumber, command);
    }

    public IEnumerable<FAQEntry> GetAll()
    {
        SqlCommand command = new SqlCommand("SELECT * FROM FAQEntry");
        return GetAll(command);
    }

    public List<FAQEntry> GetByCategory(FAQCategoryEnum category)
    {
            SqlCommand command;

            if (category == FAQCategoryEnum.All)
            {
                command = new SqlCommand("SELECT * FROM FAQEntry");
            }
            else
            {
                command = new SqlCommand("SELECT * FROM FAQEntry WHERE category = @category");
                command.Parameters.AddWithValue("@category", category.ToString());
            }

            return GetAll(command).ToList();
    }

    public void IncrementViewCount(int identificationNumber)
    {
            SqlCommand command = new SqlCommand(
                "UPDATE FAQEntry SET view_count = view_count + 1 WHERE FAQentry_id = @id");
            command.Parameters.AddWithValue("@id", identificationNumber);

            ExecuteNonQuery(command);
            InvalidateCacheEntry(identificationNumber);
        }

    public void IncrementWasHelpfulVotes(int identificationNumber)
    {
            SqlCommand command = new SqlCommand(
                "UPDATE FAQEntry SET was_helpful_votes = was_helpful_votes + 1 WHERE FAQentry_id = @id");
            command.Parameters.AddWithValue("@id", identificationNumber);

            ExecuteNonQuery(command);
            InvalidateCacheEntry(identificationNumber);
        }

    public void IncrementWasNotHelpfulVotes(int identificationNumber)
    {
            SqlCommand command = new SqlCommand(
                     "UPDATE FAQEntry SET was_not_helpful_votes = was_not_helpful_votes + 1 WHERE FAQentry_id = @id");
            command.Parameters.AddWithValue("@id", identificationNumber);

            ExecuteNonQuery(command);
            InvalidateCacheEntry(identificationNumber);
        }

    protected override FAQEntry MapRowToEntity(SqlDataReader reader)
        {
            int identificationNumber = (int)reader["FAQentry_id"];
            string question = reader["question"].ToString();
            string answer = reader["answer"].ToString();
            FAQCategoryEnum category = Enum.Parse<FAQCategoryEnum>(reader["category"].ToString());
            int viewCount = (int)reader["view_count"];
            int helpfulVotesCount = (int)reader["was_helpful_votes"];
            int notHelpfulVotesCount = (int)reader["was_not_helpful_votes"];

            return new FAQEntry(identificationNumber, question, answer, category, viewCount, helpfulVotesCount, notHelpfulVotesCount);
        }

    protected override int GetEntityId(FAQEntry questionEntity)
    {
            return questionEntity.Id;
    }
}
}
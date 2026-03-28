using System;
using System.Collections.Generic;
using System.Linq;
using CloudSpritzers1.src.model.faq;
using Microsoft.Data.SqlClient;
using CloudSpritzers1.src.repository.database;

namespace CloudSpritzers1.src.repository
{
  public class FAQRepository : DBRepository<int, FAQEntry>, IRepository<int, FAQEntry>
    {
    public FAQRepository() { }

    public FAQEntry GetById(int id)
    {
        SqlCommand command = new SqlCommand("SELECT * FROM FAQEntries WHERE Id = @id");
        command.Parameters.AddWithValue("@id", id);

        FAQEntry faq = base.GetById(id, command);

        if (faq == null)
            throw new KeyNotFoundException($"FAQ with id {id} was not found.");

        return faq;
    }

    public int Add(FAQEntry elem)
    {
        if (elem == null)
            throw new ArgumentNullException(nameof(elem), "FAQ entry cannot be null.");

        SqlCommand command = new SqlCommand(
             "INSERT INTO FAQEntries (Question, Answer, Category) " +
             "OUTPUT INSERTED.Id " +
             "VALUES (@question, @answer, @category)"
         );

         command.Parameters.AddWithValue("@question", elem.GetQuestion());
         command.Parameters.AddWithValue("@answer", elem.GetAnswer());
         command.Parameters.AddWithValue("@category", elem.GetCategory().ToString());

         int id = base.Add(command, elem);
         InvalidateCacheEntry(id);
         return id;
    }

    public void UpdateById(int id, FAQEntry elem)
    {
        if (elem == null)
            throw new ArgumentNullException(nameof(elem), "FAQ entry cannot be null.");

        SqlCommand command = new SqlCommand(
            "UPDATE FAQEntries " +
            "SET Question = @question, Answer = @answer, Category = @category, " +
            "ViewCount = @viewCount, WasHelpfulVotes = @wasHelpfulVotes, WasNotHelpfulVotes = @wasNotHelpfulVotes " +
            "WHERE Id = @id"
        );

        command.Parameters.AddWithValue("@id", id);
        command.Parameters.AddWithValue("@question", elem.GetQuestion());
        command.Parameters.AddWithValue("@answer", elem.GetAnswer());
        command.Parameters.AddWithValue("@category", elem.GetCategory().ToString());
        command.Parameters.AddWithValue("@viewCount", elem.GetViewCount());
        command.Parameters.AddWithValue("@wasHelpfulVotes", elem.GetWasHelpfulVotes());
        command.Parameters.AddWithValue("@wasNotHelpfulVotes", elem.GetWasNotHelpfulVotes());

        base.UpdateById(id, command, elem);
    }

    public void DeleteById(int id)
    {
        SqlCommand command = new SqlCommand("DELETE FROM FAQEntries WHERE Id = @id");
        command.Parameters.AddWithValue("@id", id);

        base.DeleteById(id, command);
    }

    public IEnumerable<FAQEntry> GetAll()
    {
        SqlCommand command = new SqlCommand("SELECT * FROM FAQEntries");
        return base.GetAll(command);
    }

    public List<FAQEntry> GetByCategory(FAQCategoryEnum category)
    {
        SqlCommand command;

        if (category == FAQCategoryEnum.All)
        {
            command = new SqlCommand("SELECT * FROM FAQEntries");
        }
        else
        {
            command = new SqlCommand("SELECT * FROM FAQEntries WHERE Category = @category");
            command.Parameters.AddWithValue("@category", category.ToString());
        }

        return base.GetAll(command).ToList();
    }

    public void IncrementViewCount(int id)
    {
        SqlCommand command = new SqlCommand(
            "UPDATE FAQEntries SET ViewCount = ViewCount + 1 WHERE Id = @id"
        );
        command.Parameters.AddWithValue("@id", id);

        ExecuteNonQuery(command);
        InvalidateCacheEntry(id);
    }

    public void IncrementWasHelpfulVotes(int id)
    {
        SqlCommand command = new SqlCommand(
            "UPDATE FAQEntries SET WasHelpfulVotes = WasHelpfulVotes + 1 WHERE Id = @id"
        );
        command.Parameters.AddWithValue("@id", id);

        ExecuteNonQuery(command);
        InvalidateCacheEntry(id);
    }

    public void IncrementWasNotHelpfulVotes(int id)
    {
        SqlCommand command = new SqlCommand(
            "UPDATE FAQEntries SET WasNotHelpfulVotes = WasNotHelpfulVotes + 1 WHERE Id = @id"
        );
        command.Parameters.AddWithValue("@id", id);

        ExecuteNonQuery(command);
        InvalidateCacheEntry(id);
    }

    protected override FAQEntry MapRowToEntity(SqlDataReader reader)
    {
        int id = (int)reader["Id"];
        string question = reader["Question"].ToString();
        string answer = reader["Answer"].ToString();
        FAQCategoryEnum category = Enum.Parse<FAQCategoryEnum>(reader["Category"].ToString());
        int viewCount = (int)reader["ViewCount"];
        int wasHelpfulVotes = (int)reader["WasHelpfulVotes"];
        int wasNotHelpfulVotes = (int)reader["WasNotHelpfulVotes"];

        return new FAQEntry(id, question, answer, category, viewCount, wasHelpfulVotes, wasNotHelpfulVotes);
    }

    protected override int GetEntityId(FAQEntry entity)
    {
        return entity.GetId();
    }
}
}
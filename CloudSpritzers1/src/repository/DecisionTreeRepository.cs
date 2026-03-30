using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using CloudSpritzers1.src.model.faq.bot;
using Microsoft.Data.SqlClient;

namespace CloudSpritzers1.src.repository
{
    public class DecisionTreeRepository : DBRepository<int, FAQNode>, IRepository<int, FAQNode>
    {
        private ImmutableArray<FAQOption> LoadOptions(int nodeId)
        {
            using var conn = CreateConnection();
            conn.Open();

            using var cmd = new SqlCommand(
                "SELECT label, next_option_id FROM FAQOption WHERE node_id = @NodeId", conn);
            cmd.Parameters.AddWithValue("@NodeId", nodeId);

            using var reader = cmd.ExecuteReader();
            var options = new List<FAQOption>();
            while (reader.Read())
            {
                options.Add(new FAQOption(
                    reader.GetString(reader.GetOrdinal("label")),
                    reader.GetInt32(reader.GetOrdinal("next_option_id"))
                ));
            }
            return options.ToImmutableArray();
        }

        private Dictionary<int, ImmutableArray<FAQOption>> LoadAllOptions()
        {
            using var conn = CreateConnection();
            conn.Open();

            using var cmd = new SqlCommand(
                "SELECT node_id, label, next_option_id FROM FAQOption", conn);

            using var reader = cmd.ExecuteReader();
            var grouped = new Dictionary<int, List<FAQOption>>();

            while (reader.Read())
            {
                int nodeId = reader.GetInt32(reader.GetOrdinal("node_id"));
                var option = new FAQOption(
                    reader.GetString(reader.GetOrdinal("label")),
                    reader.GetInt32(reader.GetOrdinal("next_option_id"))
                );

                if (!grouped.ContainsKey(nodeId))
                    grouped[nodeId] = new List<FAQOption>();

                grouped[nodeId].Add(option);
            }

            return grouped.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.ToImmutableArray()
            );
        }

        public FAQNode GetById(int id)
        {
            using var cmd = new SqlCommand(
                "SELECT node_id, question_text, is_final_answer FROM FAQNode WHERE node_id = @Id");
            cmd.Parameters.AddWithValue("@Id", id);

           
            var node = base.GetById(id, cmd);
            if (node == null) return null;

            var options = LoadOptions(id);
            return node with { Options = options };
        }

        public int Add(FAQNode elem)
        {
            using var addSqlCommand = new SqlCommand(@"
                INSERT INTO FAQNode (question_text, is_final_answer)
                OUTPUT INSERTED.node_id
                VALUES (@QuestionText, @IsFinalAnswer)");

            addSqlCommand.Parameters.AddWithValue("@QuestionText", elem.QuestionText);
            addSqlCommand.Parameters.AddWithValue("@IsFinalAnswer", elem.IsFinalAnswer);

            int newId = base.Add(addSqlCommand, elem);

            foreach (var option in elem.Options)
            {
                using var optCmd = new SqlCommand(@"
                    INSERT INTO FAQOption (node_id, label, next_option_id)
                    VALUES (@NodeId, @Label, @NextOptionId)");

                optCmd.Parameters.AddWithValue("@NodeId", newId);
                optCmd.Parameters.AddWithValue("@Label", option.Label);
                optCmd.Parameters.AddWithValue("@NextOptionId", option.NextOptionId);

                base.ExecuteNonQuery(optCmd);
            }

            return newId;
        }

        public void DeleteById(int id)
        {
            using var deleteOptions = new SqlCommand(
                "DELETE FROM FAQOption WHERE node_id = @Id");
            deleteOptions.Parameters.AddWithValue("@Id", id);
            base.ExecuteNonQuery(deleteOptions);

            using var deleteNode = new SqlCommand(
                "DELETE FROM FAQNode WHERE node_id = @Id");
            deleteNode.Parameters.AddWithValue("@Id", id);
            base.DeleteById(id, deleteNode);
        }

        public void UpdateById(int id, FAQNode elem)
        {
            using var updateByIdSqlCommand = new SqlCommand(@"
                UPDATE FAQNode
                SET question_text = @QuestionText,
                    is_final_answer = @IsFinalAnswer
                WHERE node_id = @Id");

            updateByIdSqlCommand.Parameters.AddWithValue("@Id", id);
            updateByIdSqlCommand.Parameters.AddWithValue("@QuestionText", elem.QuestionText);
            updateByIdSqlCommand.Parameters.AddWithValue("@IsFinalAnswer", elem.IsFinalAnswer);

            base.UpdateById(id, updateByIdSqlCommand, elem);

            using var deleteOptions = new SqlCommand(
                "DELETE FROM FAQOption WHERE node_id = @Id");
            deleteOptions.Parameters.AddWithValue("@Id", id);
            base.ExecuteNonQuery(deleteOptions);

            foreach (var option in elem.Options)
            {
                using var optionSqlCommand = new SqlCommand(@"
                    INSERT INTO FAQOption (node_id, label, next_option_id)
                    VALUES (@NodeId, @Label, @NextOptionId)");

                optionSqlCommand.Parameters.AddWithValue("@NodeId", id);
                optionSqlCommand.Parameters.AddWithValue("@Label", option.Label);
                optionSqlCommand.Parameters.AddWithValue("@NextOptionId", option.NextOptionId);

                base.ExecuteNonQuery(optionSqlCommand);
            }
        }

        public IEnumerable<FAQNode> GetAll()
        {
            using var getAllSqlCommand = new SqlCommand(
                "SELECT node_id, question_text, is_final_answer FROM FAQNode");

            var nodes = base.GetAll(getAllSqlCommand).ToList();

            var allOptions = LoadAllOptions();

            return nodes.Select(node =>
                node with
                {
                    Options = allOptions.TryGetValue(node.FaqNodeId, out var options)
                        ? options
                        : ImmutableArray<FAQOption>.Empty
                }
            ).ToList();
        }

        protected override FAQNode MapRowToEntity(SqlDataReader reader)
        {
            return new FAQNode(
                reader.GetInt32(reader.GetOrdinal("node_id")),
                reader.GetString(reader.GetOrdinal("question_text")),
                new ImmutableArray<FAQOption>(),
                reader.GetBoolean(reader.GetOrdinal("is_final_answer"))
            );
        }

        protected override int GetEntityId(FAQNode entity) => entity.FaqNodeId;
    }
}
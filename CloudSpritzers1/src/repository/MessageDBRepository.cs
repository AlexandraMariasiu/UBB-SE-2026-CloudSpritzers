using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using CloudSpritzers1.Src.Model.Message;
using CloudSpritzers1.Src.Model.Chats;

namespace CloudSpritzers1.Src.Repository.Database
{
    public class MessageDatabaseRepository : DatabaseRepository<int, Message>, IRepository<int, Message>
    {
        protected override Message MapRowToEntity(SqlDataReader reader)
        {
            int messageId = reader.GetInt32(reader.GetOrdinal("message_id"));
            int chatId = reader.GetInt32(reader.GetOrdinal("chat_id"));
            int senderId = reader.GetInt32(reader.GetOrdinal("sender_id"));
            string text = reader.GetString(reader.GetOrdinal("text"));
            /// DateTimeOffset timestamp = reader.GetDateTimeOffset(reader.GetOrdinal("timestamp"));
            // Read as DateTime, then convert to DateTimeOffset
            DateTime dbDt = reader.GetDateTime(reader.GetOrdinal("timestamp"));
            DateTimeOffset timestamp = new DateTimeOffset(dbDt);

            var senderStub = new SenderStub(senderId);
            var chatStub = new ChatStub(chatId);

            return new Message(messageId, senderStub, chatStub, text, timestamp);
        }

        protected override int GetEntityId(Message entity) => entity.GetId();

        public int CreateNewEntity(Message newMessage)
        {
            const string query =
                "INSERT INTO Message (sender_id, chat_id, timestamp, text, is_read) " +
                "VALUES (@senderId, @chatId, @timestamp, @text, @isRead); " +
                "SELECT CAST(SCOPE_IDENTITY() AS INT);";

            var commandForCreatingMessage = new SqlCommand(query);
            commandForCreatingMessage.Parameters.AddWithValue("@senderId", newMessage.GetSender().RetrieveUniqueDatabaseIdentifierForBot());
            commandForCreatingMessage.Parameters.AddWithValue("@chatId", ((IMessage)newMessage).GetChat().ChatId);
            commandForCreatingMessage.Parameters.AddWithValue("@timestamp", DateTimeOffset.UtcNow);
            commandForCreatingMessage.Parameters.AddWithValue("@text", newMessage.GetMessage());
            commandForCreatingMessage.Parameters.AddWithValue("@isRead", false);

            return Add(commandForCreatingMessage, newMessage);
        }

        public void DeleteById(int messageDbId)
        {
            const string query = "DELETE FROM Message WHERE message_id = @id";
            var commandForDeletingMessage = new SqlCommand(query);
            commandForDeletingMessage.Parameters.AddWithValue("@id", messageDbId);

            DeleteById(messageDbId, commandForDeletingMessage);
        }

        public void UpdateById(int messageDbId, Message newMessage)
        {
            const string query = "UPDATE Message SET text = @text WHERE message_id = @id";
            var commandForUpdateDb = new SqlCommand(query);
            commandForUpdateDb.Parameters.AddWithValue("@text", newMessage.GetMessage());
            commandForUpdateDb.Parameters.AddWithValue("@id", messageDbId);

            UpdateById(messageDbId, commandForUpdateDb, newMessage);
        }

        public IEnumerable<Message> GetAll()
        {
            const string query = "SELECT * FROM Message";
            return GetAll(new SqlCommand(query));
        }

        public Message GetById(int messageDbId)
        {
            const string query = "SELECT * FROM Message WHERE message_id = @id";
            var commandForGettingMessage = new SqlCommand(query);
            commandForGettingMessage.Parameters.AddWithValue("@id", messageDbId);

            return GetById(messageDbId, commandForGettingMessage)
                ?? throw new KeyNotFoundException($"Message with id {messageDbId} not found.");
        }

        public IEnumerable<Message> GetByChatId(int chatId)
        {
            const string query =
                "SELECT * FROM Message WHERE chat_id = @chatId ORDER BY timestamp ASC";
            var commandForGettingMessageByChatId = new SqlCommand(query);
            commandForGettingMessageByChatId.Parameters.AddWithValue("@chatId", chatId);
            return GetAll(commandForGettingMessageByChatId);
        }

        public IEnumerable<Message> GetMessagesSince(int chatId, int firstMessageId)
        {
            const string query =
                "SELECT * FROM Message " +
                "WHERE chat_id = @chatId AND message_id >= @firstMessageId " +
                "ORDER BY timestamp ASC";
            var commandForGettingMessagesSince = new SqlCommand(query);
            commandForGettingMessagesSince.Parameters.AddWithValue("@chatId", chatId);
            commandForGettingMessagesSince.Parameters.AddWithValue("@firstMessageId", firstMessageId);

            return GetAll(commandForGettingMessagesSince);
        }

        public void MarkAsRead(int messageId)
        {
            const string query = "UPDATE Message SET is_read = 1 WHERE message_id = @id";
            var commandForMarkingAsRead = new SqlCommand(query);
            commandForMarkingAsRead.Parameters.AddWithValue("@id", messageId);

            ExecuteNonQuery(commandForMarkingAsRead);
            InvalidateCacheEntry(messageId);
        }

        // TODO: I swear I wanted to remove stubs, not end more. I hope God and Mihai will forgive me, at least Mihai.
        private sealed class SenderStub : ISender
        {
            private readonly int identificationNumber;
            public SenderStub(int senderId) => identificationNumber = senderId;
            public int RetrieveUniqueDatabaseIdentifierForBot() => identificationNumber;
            public string RetrieveConfiguredDisplayFullNameForBot() => string.Empty;
            public string RetrieveConfiguredEmailAddressForBotContact() => string.Empty;
        }
       private sealed class ChatStub : Chat
        {
            public ChatStub(int chatId) : base(chatId, userId: 0, ChatStatus.Active)
            {
            }
        }
    }
}
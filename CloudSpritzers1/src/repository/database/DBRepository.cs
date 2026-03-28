using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CloudSpritzers1.src.repository;
using CloudSpritzers1.src.repository;
using CloudSpritzers1.src.repository.database;
using Microsoft.Data.SqlClient;

public abstract class DBRepository<K, E>
    where E : class
{
    private readonly string _connectionString;
    private readonly Dictionary<K, E> _cache = new();

    protected DBRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected SqlConnection CreateConnection() => DBConnectionHandler.Instance.Connection;
    protected abstract E MapRowToEntity(SqlDataReader reader);
    protected abstract K GetEntityId(E entity);

    /// <summary>
    /// Gets an entity by its id OR returns null.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    protected E GetById(K id, SqlCommand command)
    {
        if (_cache.TryGetValue(id, out var cached))
            return cached;

        var entity = ExecuteQuerySingle(command);
        if (entity != null)
            _cache[id] = entity;

        return entity;
    }

    protected IEnumerable<E> GetAll(SqlCommand command)
    {
        var results = ExecuteQueryMany(command).ToList();
        foreach (var entity in results)
            _cache[GetEntityId(entity)] = entity;

        return results;
    }

    protected K Add(SqlCommand command, E elem)
    {
        var id = ExecuteScalar<K>(command);
        _cache[id] = elem;
        return id;
    }

    protected void DeleteById(K id, SqlCommand command)
    {
        ExecuteNonQuery(command);
        _cache.Remove(id);
    }

    protected void UpdateById(K id, SqlCommand command, E elem)
    {
        ExecuteNonQuery(command);
        _cache[id] = elem;
    }

    /// <summary>
    /// Returns one entity matching the query. If no matching row in db is found => null!
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    protected E ExecuteQuerySingle(SqlCommand command)
    {
        using var conn = CreateConnection();
        command.Connection = conn;
        conn.Open();
        using var reader = command.ExecuteReader();
        return reader.Read() ? MapRowToEntity(reader) : null;
    }

    protected IEnumerable<E> ExecuteQueryMany(SqlCommand command)
    {
        using var conn = CreateConnection();
        command.Connection = conn;
        conn.Open();
        using var reader = command.ExecuteReader();
        var results = new List<E>();
        while (reader.Read())
            results.Add(MapRowToEntity(reader));
        return results;
    }

    protected void ExecuteNonQuery(SqlCommand command)
    {
        using var conn = CreateConnection();
        command.Connection = conn;
        conn.Open();
        command.ExecuteNonQuery();
    }

    protected T ExecuteScalar<T>(SqlCommand command)
    {
        using var conn = CreateConnection();
        command.Connection = conn;
        conn.Open();
        return (T)command.ExecuteScalar();
    }


    protected void InvalidateCache() => _cache.Clear();
    protected void InvalidateCacheEntry(K id) => _cache.Remove(id);
}
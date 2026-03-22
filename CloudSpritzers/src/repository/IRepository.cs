

public interface IRepository<K, V> where V : class
{
    V GetById(K id);
    K Add(V elem);
    void DeleteById(K id);
    void UpdateById(K id, V elem);
}

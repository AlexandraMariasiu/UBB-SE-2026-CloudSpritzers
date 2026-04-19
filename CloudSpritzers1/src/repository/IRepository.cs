using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSpritzers1.src.repository
{
    public interface IRepository<K, V> where V : class
    {
        V GetById(K id);
        K CreateReview(V elem);
        void DeleteById(K id);
        void UpdateById(K id, V elem);
        IEnumerable<V> GetAll();
    }
}

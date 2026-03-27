using System.Collections.Generic;
using System.Linq;
using CloudSpritzers.src.model.review;

namespace CloudSpritzers.src.repository
{
    public class ReviewRepository : IRepository<int, Review>
    {
        private Dictionary<int, Review> reviews = new Dictionary<int, Review>();

        public Review GetById(int id)
        {
            return reviews.ContainsKey(id) ? reviews[id] : null;
        }

        public int Add(Review elem)
        {
            if(reviews.ContainsKey(elem.GetId()))
            {
                return -1; // or throw an exception
            }
            reviews[elem.GetId()] = elem;
            return elem.GetId();
        }

        public void UpdateById(int id, Review elem)
        {
            if (reviews.ContainsKey(id))
            {
                reviews[id] = elem;
            }
        }

        public void DeleteById(int id)
        {
            if (reviews.ContainsKey(id))
            {
                reviews.Remove(id);
            }
        }


        // same as FindAll() from UML
        public IEnumerable<Review> GetAll()
        {
            return reviews.Values.ToList();
        }

    }
}
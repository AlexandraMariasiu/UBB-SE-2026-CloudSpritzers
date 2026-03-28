using System;
using System.Collections.Generic;
using System.Linq;
using CloudSpritzers1.src.model.review;

namespace CloudSpritzers1.src.repository
{
    public class ReviewRepository : IRepository<int, Review>
    {
        private Dictionary<int, Review> _reviews = new Dictionary<int, Review>();

        public Review GetById(int id)
        {
            if (!_reviews.ContainsKey(id))
                throw new KeyNotFoundException($"Review with id {id} was not found.");
            return _reviews[id];
        }

        public int Add(Review elem)
        {
            if (_reviews.ContainsKey(elem.GetId()))
                throw new InvalidOperationException($"Review with id {elem.GetId()} already exists.");
            if (elem == null)
                throw new ArgumentNullException(nameof(elem), "Review cannot be null.");
            _reviews[elem.GetId()] = elem;
            return elem.GetId();
        }

        public void UpdateById(int id, Review elem)
        {
            if (!_reviews.ContainsKey(id))
                throw new KeyNotFoundException($"Review with id {id} was not found.");

            if (elem == null)
                throw new ArgumentNullException(nameof(elem), "Review cannot be null.");

            _reviews[id] = elem;

        }

        public void DeleteById(int id)
        {
            if (!_reviews.ContainsKey(id))
                throw new KeyNotFoundException($"Review with id {id} was not found.");

            _reviews.Remove(id);

        }

        // same as FindAll() from UML
        public IEnumerable<Review> GetAll()
        {
            return _reviews.Values.ToList();
        }

    }
}
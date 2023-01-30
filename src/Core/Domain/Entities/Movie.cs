using Domain.Contracts;
using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Movie : BaseEntity, IMustHaveTenant
    {
        public Movie() { }
        public Movie(string title, string description, float imdb)
        {
            Title = title;
            Description = description;
            IMDb = imdb;
        }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public float IMDb { get; private set; }
        public string TenantId { get; set; }
    }
}

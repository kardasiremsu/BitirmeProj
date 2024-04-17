using BitirmeProj.Data;
using BitirmeProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BitirmeProj.Services
{
    public class JobService
    {
        private readonly ApplicationDBContext _context;

        public JobService(ApplicationDBContext context)
        {
            _context = context;
        }

        public List<JobListing> GetMoreJobListings(int skip, int take)
        {
            return _context.JobListings
                           .OrderByDescending(j => j.JobCreatedDate)
                           .Skip(skip)
                           .Take(take)
                           .ToList();
        }

        // You can add more methods for creating, updating, or deleting job listings as needed
    }
}

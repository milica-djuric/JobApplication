using JobApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplication.Data
{
    public class Repository : IRepository
    {
        private readonly JobApplicationDbContext _context;
        public Repository(JobApplicationDbContext context)
        {
            _context = context;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        
        public void DeleteCandidate(Candidate candidate)
        {
            //mozda ne radi
            var applications = _context.Applications.Where(c => c.CandidateID == candidate.CandidateID);
            foreach (var a in applications)
            {
                _context.Applications.Remove(a);
            }
            _context.Candidates.Remove(candidate);
        }

        public IList<Application> GetApplications()
        {
            return _context.Applications.Include(a => a.Candidate).Include(a => a.Position).ToList();
        }

        public Candidate GetCandidate(int? id)
        {
            var candidate = _context.Candidates.Find(id);
            candidate.Applications = _context.Applications.Where(c => c.CandidateID == id).ToList();
            return candidate;
        }

        public IList<Candidate> GetAllCandidates()
        {
            var candidates = _context.Candidates.Include(c => c.Applications).ToList();

            foreach (var c in candidates)
            {
                foreach (var a in c.Applications)
                {
                    a.Position = _context.Positions.Find( a.PositionID, a.CompanyID );
                }
            }

           return candidates;
        }

        public IList<Position> GetAllPositions()
        {
            return _context.Positions.ToList();
        }

        public void AddCandidatesApplication(Candidate candidate)
        {
            _context.Candidates.Add(candidate);
            foreach (var a in candidate.Applications)
            {
                _context.Applications.Add(a);
            }
        }

        public void UpdateCandidatesApplications(Candidate candidate)
        {
            var applicationsFromDb =
                _context.Applications.Where(a => a.CandidateID == candidate.CandidateID).ToList();

            foreach (var a in applicationsFromDb)
                _context.Applications.Remove(a);

            _context.SaveChanges();

            foreach (var a in candidate.Applications)
                _context.Applications.Add(a);

            _context.Candidates.Update(candidate);
        }
    }
}

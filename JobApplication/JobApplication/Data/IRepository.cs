using JobApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplication.Data
{
    public interface IRepository
    {
        void SaveChanges();
        IList<Application> GetApplications();
        Candidate GetCandidate(int? id);
        IList<Candidate> GetAllCandidates();
        void DeleteCandidate(Candidate candidate);
        IList<Position> GetAllPositions();
        void AddCandidatesApplication(Candidate candidate);
        void UpdateCandidatesApplications(Candidate candidate);
    }
}

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JobApplication.Data;
using JobApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.Controllers
{
    public class JobApplicationController : Controller
    {
        private readonly IRepository _repository;

        public Candidate Candidate { get; set; }
        [BindProperty]
        public ViewModel ViewModel { get; set; }
        public JobApplicationController(IRepository repository)
        {
            _repository = repository;
            ViewModel = new ViewModel();
        }
        public IActionResult Index()
        {
            return View();
        }        

        public ActionResult Upsert(int? id)
        {
            ViewModel.Positions = _repository.GetAllPositions();
            var checkBoxList = new List<CheckBoxViewModel>();

            Candidate = new Candidate();

            if (id == null)
            {            
                foreach (var p in ViewModel.Positions)
                {
                    checkBoxList.Add(new CheckBoxViewModel()
                    {
                        PositionID = p.PositionID,
                        CompanyID = p.CompanyID,
                        Display = p.PositionName,
                        IsActive = false
                    });
                }
                
                ViewModel.PositionTypesCheckBoxes = checkBoxList;

                ViewModel.Candidate = Candidate;
                return View(ViewModel);
            }

            Candidate = _repository.GetCandidate(id);

            foreach (var p in ViewModel.Positions)
            {
                CheckBoxViewModel cb = new CheckBoxViewModel()
                {
                    PositionID = p.PositionID,
                    CompanyID = p.CompanyID,
                    Display = p.PositionName
                };
                Application a = new Application()
                {
                    CandidateID = Candidate.CandidateID,
                    PositionID = p.PositionID,
                    CompanyID = p.CompanyID
                };
                if (Candidate.Applications.Contains(a)) cb.IsActive = true;

                checkBoxList.Add(cb);
            }

            ViewModel.PositionTypesCheckBoxes = checkBoxList;

            if (Candidate == null)
                return NotFound();

            ViewModel.Candidate = Candidate;
            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                Candidate = ViewModel.Candidate;
                Candidate.Applications = new List<Application>();

                foreach (var positionType in ViewModel.PositionTypesCheckBoxes)
                {
                    if (positionType.IsActive)
                    {
                        Candidate.Applications.Add(new Application()
                        {
                            CandidateID = Candidate.CandidateID,
                            PositionID = positionType.PositionID,
                            CompanyID = positionType.CompanyID
                        });
                    }
                }

                if (Candidate.Applications.Count == 0)
                {
                    ViewBag.Message = "At least one job position must be selected!";
                    return View(ViewModel);
                }
                

                if (Candidate.CandidateID == 0)
                {
                    _repository.AddCandidatesApplication(Candidate);
                    
                }
                else
                {
                    _repository.UpdateCandidatesApplications(Candidate);
                }

                _repository.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ViewModel);
        }

        [HttpGet]
        public ActionResult <IList<Candidate>> GetAllCandidates()
        {
            return Json(new { data = _repository.GetAllCandidates() });
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Candidate = _repository.GetCandidate(id);

            if (Candidate == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _repository.DeleteCandidate(Candidate);
            _repository.SaveChanges();
            return Json(new { success = true, message = "Delete successful" });
        }
    }
}

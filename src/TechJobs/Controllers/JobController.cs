using System;
using Microsoft.AspNetCore.Mvc;
using TechJobs.Data;
using TechJobs.ViewModels;
using TechJobs.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace TechJobs.Controllers
{
    public class JobController : Controller
    {

        // Our reference to the data store
        private static JobData jobData;

        static JobController()
        {
            jobData = JobData.GetInstance();
        }

        // The detail display for a given Job at URLs like /Job?id=17
        public IActionResult Index(int id)
        {
            // TODO #1 - get the Job with the given ID and pass it into the view
            var job = JobData.GetInstance();
            Models.Job jobView = job.Find(id);
            return View(jobView);
        }

        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
            // TODO #6 - Validate the ViewModel and if valid, create a 
            // new Job and add it to the JobData data store. Then
            // redirect to the Job detail (Index) action/view for the new Job.
            if (ModelState.IsValid)
            {
                var jobs = JobData.GetInstance();
                JobFieldData<Employer> field = jobs.Employers;
                JobFieldData<Location> field2 = jobs.Locations;
                JobFieldData<PositionType> field3 = jobs.PositionTypes;
                JobFieldData<CoreCompetency> field4 = jobs.CoreCompetencies;
                Job newJob = new Job
                {
                    Name = newJobViewModel.Name,
                    Employer = field.Find(newJobViewModel.EmployerID),
                    Location = field2.Find(newJobViewModel.LocationID),
                    PositionType = field3.Find(newJobViewModel.PositionID),
                    CoreCompetency = field4.Find(newJobViewModel.SkillID)
                };
                jobData.Jobs.Add(newJob);
                
                var id = jobs.Jobs.Count;
                return Redirect("/Job?id=" + id);
            }
            return View(newJobViewModel);
        }
    }
}

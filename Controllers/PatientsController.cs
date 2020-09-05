using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClinicManagement.Models;

namespace ClinicManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        readonly log4net.ILog _log4net;
        private readonly ClinicContext _context;

        public PatientsController(ClinicContext context)
        {
            _context = context;
            _log4net = log4net.LogManager.GetLogger(typeof(PatientsController));

        }

        // GET: api/Patients
        [HttpGet]
        public IEnumerable <Patient> GetPatients()
        {
            return _context.Patients.ToList();
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public  Patient GetPatient(int id)
        {
            var patient = _context.Patients.Find(id);

            

            return patient;
        }

        // PUT: api/Patients/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutPatient(int id, Patient patient)
        {
            Patient p = _context.Patients.Find(id);
            p.Name = patient.Name;
            p.Email = patient.Email;
            p.Password = patient.Password;

           // _context.Entry(patient).State = EntityState.Modified;

            try
            { 
                _context.SaveChanges();
                return Ok("Patient Updation Success");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            
        }

        // POST: api/Patients        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Patient>> PostPatient(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPatient", new { id = patient.PatientId }, patient);
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Patient>> DeletePatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return patient;
        }

        private bool PatientExists(int id)
        {
            return _context.Patients.Any(e => e.PatientId == id);
        }
    }
}

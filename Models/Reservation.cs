using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicManagement.Models
{
    public class Reservation
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
       
        public int ReservationId { get; set; }
       
    }
}

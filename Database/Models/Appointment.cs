using System.ComponentModel.DataAnnotations.Schema;
using System;
using Microsoft.EntityFrameworkCore;

namespace Database.Models
{
    public class Appointment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime StartDate { get; set; } = DateTime.MinValue;
        public DateTime EndDate { get; set; } = DateTime.MinValue;
    }
}
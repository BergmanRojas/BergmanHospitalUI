namespace BergmanHospitalUI.Models
{
    using BergmanHospitalUI.Enums;

    public class AppointmentDto
    {
        public int Id { get; set; }
        public string DoctorId { get; set; } = string.Empty;
        public DateTime ScheduledDateTime { get; set; }
        public string Notes { get; set; } = string.Empty;
        public AppointmentStatus Status { get; set; }
    }
}

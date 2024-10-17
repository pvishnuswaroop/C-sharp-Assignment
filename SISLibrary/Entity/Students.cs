namespace SISLibrary.Entity
{
    public class Students
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public decimal OutstandingBalance { get; set; }

        public List<Enrollments> Enrollments { get; set; } = new List<Enrollments>();
        public List<Payments> PaymentHistory { get; set; } = new List<Payments>();

        public Students(int studentID, string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber)
        {
            StudentID = studentID;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
            PhoneNumber = phoneNumber;
            OutstandingBalance = 0m;
        }
    }
}

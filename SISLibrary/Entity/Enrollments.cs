namespace SISLibrary.Entity
{
    public class Enrollments
    {
        public int EnrollmentID { get; set; }
        public Students Student { get; set; }
        public Courses Course { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public Enrollments(int enrollmentID, Students student, Courses course, DateTime enrollmentDate)
        {
            EnrollmentID = enrollmentID;
            Student = student;
            Course = course;
            EnrollmentDate = enrollmentDate;
        }
    }
}

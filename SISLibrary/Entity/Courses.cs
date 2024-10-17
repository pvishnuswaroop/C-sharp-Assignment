namespace SISLibrary.Entity
{
    public class Courses
    {
        public int CourseID { get; set; }
        public string CourseName { get; set; }
        public int Credits { get; set; } 
        public int? TeacherId { get; set; }
        public List<Enrollments> Enrollments { get; set; }

        public Courses(int courseID, string courseName, int credits, int? teacherId)
        {
            CourseID = courseID;
            CourseName = courseName;
            Credits = credits;
            TeacherId = teacherId;
            Enrollments = new List<Enrollments>();
        }
    }
}

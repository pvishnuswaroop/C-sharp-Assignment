using SISLibrary.Entity;
using System.Collections.Generic;

namespace SISLibrary.DAO
{
    public interface ICourseDAO
    {
        Courses GetCourseById(int courseId);
        List<Courses> GetAllCourses(); 
        Courses GetCourseByName(string courseName);
        void AddCourse(Courses course);
        void UpdateCourse(Courses course);
        void DeleteCourse(int courseId);
        void AssignTeacher(int courseId, int teacherId);
    }
}

using SISLibrary.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;

namespace SISLibrary.DAO
{
    public interface ITeacherDAO
    {
        Teacher GetTeacherById(int teacherId);
        List<Teacher> GetAllTeachers();
    }
}



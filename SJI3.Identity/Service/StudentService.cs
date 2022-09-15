namespace SJI3.Identity.Service
{
    public class StudentService : IStudentService
    {
        private readonly StudentDbContext _studentDbContext;
        public StudentService(StudentDbContext studentDbContext)
        {
            _studentDbContext = studentDbContext;
        }
        public List<Student> GetAll()
        {
            var students = _studentDbContext.Student.ToList();

            return students;
        }
    }
}

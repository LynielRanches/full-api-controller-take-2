using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using September012026.Models.Domain;
using September012026.Models.DTO;
using September012026.Mapper;

namespace September012026.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        static List<Student> students = new List<Student>();

        [HttpGet("{id:int}")]
        public IActionResult GetStudent([FromRoute]int id)
        {
            var student = students.Where(m => m.Id == id).FirstOrDefault();
            if (student != null)
            {
                return Ok(student);
            }
            return BadRequest();
        }

        [HttpGet("search")]
        public IActionResult SearchStudent([FromQuery]string name)
        {
            var results = students.Where(m => m.LastName.Contains(name) || m.FirstName.Contains(name));
            if (results != null)
            {
                return Ok(results);
            }
            return BadRequest();
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            return Ok(students);
        }

        [HttpPost]
        public IActionResult AddStudent([FromBody]AddStudentDto student)
        {
            var newStudent = student.MapToStudent();
            newStudent.Id = students.Count + 1; // Autogenerate sequential ID
            students.Add(newStudent);

            //students.Add(StudentMapper.MapToStudent(student));
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult EditStudent(int id, EditStudentDto student)
        {
            var studentForEdit = students.Where(m => m.Id == id).FirstOrDefault();
            if (studentForEdit != null)
            {
                studentForEdit.LastName = student.LastName;
                studentForEdit.FirstName = student.FirstName;
                studentForEdit.Gender = student.Gender;
                studentForEdit.Birthday = student.Birthday;
                studentForEdit.Address = student.Address;
                studentForEdit.Birthplace = student.Birthplace;
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var studentForDelete = students.Where(m => m.Id == id).FirstOrDefault();
            if (studentForDelete != null)
            {
                students.Remove(studentForDelete);
            }
            return Ok();
        }
    }
}
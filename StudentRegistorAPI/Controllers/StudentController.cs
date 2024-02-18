using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentRegistorAPI.Models;

[ApiController]
public class StudentController : ControllerBase
{
    private readonly StudentDbContext _context;

    public StudentController(StudentDbContext context){
        _context = context;
    }

    [HttpGet]
    [Route("GetAllStudents")]
    public IActionResult GetAllStudents(){
        try {
            var students = _context.students.OrderBy(x => x.StudentId).ToList();
            return Ok(students);
        }
        catch (Exception ex){
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpGet]
    [Route("GetStudentById")]
    public IActionResult GetStudentById(int id){
        try{
            var student = _context.students.FirstOrDefault(x => x.StudentId == id);
            if (student == null){
                return NotFound("Student not found");
            }
            return Ok(student);
        }
        catch (Exception ex){
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpPost]
    [Route("SaveStudentData")]
    public async Task<IActionResult> SaveStudentDataAsync([FromForm] Student student) {
        try {
            if (student.Image != null)
            {
                var fileSavePaths = Path.Combine("wwwroot", "Uploads", "StudentAttachment");
                var filePaths = Path.Combine(fileSavePaths, $"{student.LastName}.png");


                if (!Directory.Exists(fileSavePaths))
                {
                    Directory.CreateDirectory(fileSavePaths);
                }

                if (System.IO.File.Exists(filePaths))
                {
                    System.IO.File.Delete(filePaths);
                }

                using (var stream = new FileStream(filePaths, FileMode.Create))
                    await student.Image.CopyToAsync(stream);

                student.ImagePath = filePaths;
            }

            if (student == null) {
                return BadRequest("Invalid student data.");
            }

            if (student.StudentId == 0){
                _context.Add(student);
            }
            else{
                _context.Update(student);
            }

            _context.SaveChanges();

            return new ObjectResult(student.StudentId);
        }
        catch (Exception ex){
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }


    [HttpDelete]
    [Route("DeleteStudent")]
    public IActionResult DeleteStudent(int id){
        try{
            var studentToDelete = _context.students.FirstOrDefault(x => x.StudentId == id);
            if (studentToDelete == null){
                return NotFound("Student not found");
            }
            _context.students.Remove(studentToDelete);
            _context.SaveChanges();

            return Ok(studentToDelete.StudentId);
        }
        catch (Exception ex){
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }


}

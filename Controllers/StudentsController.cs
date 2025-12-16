using Microsoft.AspNetCore.Mvc;

namespace NamPractice.API.Controllers;

// https://localhost:portnumber/api/students
[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    // GET: https://localhost:portnumber/api/students
    [HttpGet]
    public IActionResult GetAllStudents()
    {
        var students = new[]
        {
            "Alice",
            "Bob",
            "Charlie",
            "Diana"
        };
        return Ok(students);
    }
}
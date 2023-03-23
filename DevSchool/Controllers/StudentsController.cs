using DevSchool.Models;
using DevSchool.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevSchool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsRepository _studentsRepository;
        public StudentsController(IStudentsRepository studentsRepository)
        {
            _studentsRepository = studentsRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _studentsRepository.GetAll();
            return Ok(students);
        }

        [HttpGet("Id")]
        public async Task<IActionResult> GetById(String Id)
        {
            var student = await _studentsRepository.GetById(Id);
            if(student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpPost()]
        public async Task<IActionResult> Post(StudentsInputModel studentsInputModel)
        {
            var students = await _studentsRepository.Add(studentsInputModel);


            return Ok(students);
        }

        [HttpPut("Id")]
        public async Task<IActionResult> Put(String Id, StudentsInputModel studentsInputModel)
        {
            if(studentsInputModel == null)
            {
                return BadRequest(new {message = "Requisição Invalida"});
            }

            if (String.IsNullOrEmpty(Id))
            {
                return BadRequest(new { message = "Informe o Id do Objeto a ser atualizado." });
            }
            await _studentsRepository.Update(Id,studentsInputModel);
            var student = await _studentsRepository.GetById(Id);

            return Ok(student);
        }

        [HttpDelete("Id")]
        public async Task<IActionResult> Delete(String Id)
        {
  

            if (String.IsNullOrEmpty(Id))
            {
                return BadRequest(new { message = "Informe o Id do Objeto a ser atualizado." });
            }

            await _studentsRepository.Delete(Id);

            return NoContent();
        }
    }
}

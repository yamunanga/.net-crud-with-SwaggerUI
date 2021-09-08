using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using newApp.models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace newApp.Controllers
{   [Route("api/[controller]")]
    [ApiController]
    public class studentController:Controller
    {
    private StudentContext _studentContext;
    
    public studentController(StudentContext context)
    {
        _studentContext=context;
    }
    

     [HttpGet]
        public ActionResult <IEnumerable<Student>> Get()
        {
            return _studentContext.Students.ToList();
        }

    [HttpGet]
    [Route("getById/{id}")]
        public ActionResult<Student> GetById(int id)
        {
            if(id <=0){
                return NotFound("Student Id must be greater than zero");
            }
            Student student=_studentContext.Students.FirstOrDefault(s=>s.StudentId==id);
            if(student==null){
                return NotFound("Student Id Not found !");
            }
            return Ok(student);
        }

    [HttpPost]
        public async Task<ActionResult> Post([FromBody]Student student)
        {
            if(student==null){
                return NotFound("Student Id Not found !");
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _studentContext.Students.AddAsync(student);
            await _studentContext.SaveChangesAsync();
            return Ok(student);
        }
    [HttpDelete]
    [Route("getDeleteById/{id}")]
        public async Task <ActionResult<Student>> DeleteStudent(int id)
    {
        var result = await _studentContext.Students
            .FirstOrDefaultAsync(s => s.StudentId == id);
        if (result != null)
        {
            _studentContext.Students.Remove(result);
            await _studentContext.SaveChangesAsync();
            return Ok(result);
        }

        return NotFound("Student id not found !");
    }


    [HttpPut("{id:int}")]
         public async Task<ActionResult<Student>> UpdateStudent(int id,[FromBody] Student student){
                if(id <=0){
                    return NotFound("Student Id must be greater than zero");
                }
                var studentToUpdate = await _studentContext.Students.FirstOrDefaultAsync(s => s.StudentId == id);
                if(studentToUpdate != null)
                {
                    //studentToUpdate.StudentId=student.StudentId;
                    studentToUpdate.FirstName=student.FirstName;
                    studentToUpdate.LastName=student.LastName;
                    studentToUpdate.City=student.City;
                    studentToUpdate.State=student.State;
                    await _studentContext.SaveChangesAsync();

                    return Ok("Student "+id+" updated !");
                }else{
                    return NotFound();
                }
        }

        ~studentController(){
            _studentContext.Dispose();
        }
    }
}
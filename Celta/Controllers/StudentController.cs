using Celta.Aplicacion;
using Celta.Modelo;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Celta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator mediator;
        public StudentController(IMediator mediator)
        {
            this.mediator = mediator;
        }


        [HttpPost]
        public async Task<ActionResult<Unit>> Create(New.Ejecuta data)
        {
            return await mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> GetAll()
        {
            return await mediator.Send(new GetStudents.Ejecuta());
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Student>> GetOne(string name)
        {
            return await mediator.Send(new FindStudent.Ejecuta { Name = name });
        }

        [HttpDelete("{name}")]
        public async Task<ActionResult<Unit>> Delete(string name)
        {
            return await mediator.Send(new DeleteStudent.Ejecuta { Name = name });
        }

        [HttpPut]
        public async Task<ActionResult<Unit>> Update(UpdateStudent.Ejecuta data)
        {
            return await mediator.Send(data);
        }
    }
}

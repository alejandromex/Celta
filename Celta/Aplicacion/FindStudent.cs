using Celta.Modelo;
using Celta.Persistencia;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Celta.Aplicacion
{
    public class FindStudent
    {

        public class Ejecuta : IRequest<Student>
        {
            public string Name { get; set; }
        }

        public class Validador : AbstractValidator<Ejecuta>
        {
            public Validador()
            {
                RuleFor(x => x.Name).NotEmpty();
            }
        }


        public class Manejador : IRequestHandler<Ejecuta, Student>
        {
            private readonly ContextoCelta contexto;
            public Manejador(ContextoCelta contexto)
            {
                this.contexto = contexto;
            }
            public async Task<Student> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var student = await contexto.Student.Where(x => x.Name.Equals(request.Name)).FirstOrDefaultAsync();
                if(student == null)
                {
                    throw new Exception("Estudiante no encontrado");
                }

                return student;
            }
        }
    }
}

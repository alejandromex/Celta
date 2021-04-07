using Celta.Modelo;
using Celta.Persistencia;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Celta.Aplicacion
{
    public class New
    {

        public class Ejecuta : IRequest
        {
            public string m_Name { get; set; }
            public string m_Surname { get; set; }
            public int m_Age { get; set; }
            public string m_School { get; set; }
        }

        public class Valida : AbstractValidator<Ejecuta>
        {
            public Valida()
            {
                RuleFor(x => x.m_Name).NotEmpty();
                RuleFor(x => x.m_Surname).NotEmpty();
                RuleFor(x => x.m_Age).NotEmpty();
                RuleFor(x => x.m_School).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {

            private readonly ContextoCelta contexto;
            public Manejador(ContextoCelta contexto)
            {
                this.contexto = contexto;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var student = new Student
                {
                    Age = request.m_Age,
                    Name = request.m_Name,
                    Surname = request.m_Surname,
                    School = request.m_School
                };

                contexto.Student.Add(student);
                var response = await contexto.SaveChangesAsync();
                if(response > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("Error al registrar al estudiante");

            }
        }

    }
}

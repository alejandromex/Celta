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
    public class UpdateStudent
    {
        public class Ejecuta : IRequest
        {
            public string m_Name { get; set; }
            public string m_Surname { get; set; }
            public int m_Age { get; set; }
            public string m_School { get; set; }
            public int m_id { get; set; }
        }

        public class Validador : AbstractValidator<Ejecuta>
        {
            public Validador()
            {
                RuleFor(x => x.m_Name).NotEmpty();
                RuleFor(x => x.m_Surname).NotEmpty();
                RuleFor(x => x.m_School).NotEmpty();
                RuleFor(x => x.m_Age).NotEmpty();
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
                var oldStudent = await contexto.Student.Where(x => x.StudentId == request.m_id).FirstOrDefaultAsync();
                if(oldStudent == null)
                {
                    throw new Exception("No se encontro estudiante a actualizar");
                }

                oldStudent.Name = request.m_Name;
                oldStudent.Age = request.m_Age;
                oldStudent.School = request.m_School;
                oldStudent.Surname = request.m_Surname;

                contexto.Student.Update(oldStudent);
                var response = await contexto.SaveChangesAsync();

                if (response > 0)
                    return Unit.Value;

                throw new Exception("Error al actualizar");
            }
        }
    }
}

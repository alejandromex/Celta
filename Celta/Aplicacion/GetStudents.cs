using Celta.Modelo;
using Celta.Persistencia;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Celta.Aplicacion
{
    public class GetStudents
    {
        public class Ejecuta : IRequest<List<Student>>
        {

        }

        public class Manejador : IRequestHandler<Ejecuta, List<Student>>
        {
            private readonly ContextoCelta contexto;
            public Manejador(ContextoCelta contexto)
            {
                this.contexto = contexto;
            }

            public async Task<List<Student>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var students = await contexto.Student.ToListAsync();
                if(students == null)
                {
                    throw new Exception("No se encontraron estudiantes");
                }

                return students;
            }
        }
    }
}

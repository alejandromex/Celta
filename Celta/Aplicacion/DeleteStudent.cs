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
    public class DeleteStudent
    {
        public class Ejecuta : IRequest
        {
            public string Name { get; set; }
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
                var student = await contexto.Student.Where(x => x.Name.Equals(request.Name)).FirstOrDefaultAsync();
                if(student == null)
                {
                    throw new Exception("No existe alumno a eliminar");
                }

                contexto.Student.Remove(student);
                var response = await contexto.SaveChangesAsync();
                if(response > 0)
                {
                    return Unit.Value;
                }

                throw new Exception("Error al eliminar el estudiante");
            }
        }
    }
}

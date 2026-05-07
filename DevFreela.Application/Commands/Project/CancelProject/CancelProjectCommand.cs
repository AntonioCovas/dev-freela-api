using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.Project.CancelProject
{
    public class CancelProjectCommand : IRequest<Unit>
    {
        public CancelProjectCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}

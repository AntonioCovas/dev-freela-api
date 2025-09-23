using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.CreateComment
{
    public class CreateCommentCommand : IRequest<Unit>
    {
        public string? Content { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
    }
}

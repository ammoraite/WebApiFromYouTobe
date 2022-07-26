
using MediatR;

using Notes.Aplication.Interfaces;
using Notes.Domain;

namespace Notes.Aplication.NotesCommands.CreateNote
{
    public class CreateNoteCommandHandler
         : IRequestHandler<CreateNoteCommand, Guid>
    {
        private readonly INotesDbContext _dbContext;
        public CreateNoteCommandHandler ( INotesDbContext dbContext )
        {
            _dbContext=dbContext;
        }
        public async Task<Guid> Handle ( CreateNoteCommand request,
            CancellationToken cancellationToken )
        {
            var note = new Note
            {
                UserId=request.UserId,
                Title=request.Title,
                Details=request.Details,
                Id=Guid.NewGuid ( ),
                CreatedDate=DateTime.Now,
                EditDate=null
            };
            await _dbContext.Notes.AddAsync (note, cancellationToken);
            await _dbContext.SaveChangesAsync (cancellationToken);
            return note.Id;
        }
    }
}

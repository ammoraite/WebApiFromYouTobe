﻿
using MediatR;

using Microsoft.EntityFrameworkCore;

using Notes.Aplication.Common.Exeptions;
using Notes.Aplication.Interfaces;
using Notes.Domain;

namespace Notes.Aplication.NotesCommands.UpdateNote
{
    public class UpdateNoteCommandHandler :
        IRequestHandler<UpdateNoteCommand>
    {
        private readonly INotesDbContext _dbContext;
        public UpdateNoteCommandHandler ( INotesDbContext dbContex )
        {
            _dbContext=dbContex;
        }
        public async Task<Unit> Handle ( UpdateNoteCommand request, CancellationToken cancellationToken )
        {
            var entity =
                await _dbContext.Notes.FirstOrDefaultAsync (note =>
                note.Id==request.Id, cancellationToken);

            if (entity==null||entity.UserId!=request.UserId)
                throw new NotFoundException (nameof (Note), request.Id);

            entity.Details=request.Details;
            entity.Title=request.Title;
            entity.EditDate=DateTime.Now;

            await _dbContext.SaveChangesAsync (cancellationToken);

            return Unit.Value;
        }
    }
}

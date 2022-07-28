using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using Notes.Aplication.Common.Mappings;
using Notes.Aplication.NotesCommands.CreateNote;

namespace Notes.WebApi.Models
{
    public class CreateNoteDto:IMapWiht<CreateNoteCommand>
    {
        public string Title { get; set; }
        public string Details { get; set; }
        public void Mapping(Profile profile )
        {
            profile.CreateMap<CreateNoteDto, CreateNoteCommand> ( )
                .ForMember (noteCommand => noteCommand.Title,
                opt => opt.MapFrom (noteDto => noteDto.Title))
                .ForMember (noteCommand => noteCommand.Details,
                opt => opt.MapFrom (noteDto => noteDto.Details));
        }
    }
}

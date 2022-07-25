﻿using AutoMapper;
using CleanArch.Application.Interfaces.Repositories;
using CleanArch.Application.Interfaces.UnitOfWork;
using CleanArch.Application.Validations.Note;
using CleanArch.Domain.Common;
using CleanArch.Domain.Constants;
using CleanArch.Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArch.Application.Features.Commands.UpdateEvent
{
    public class UpdateNoteCommandRequest: IRequest<AppResponse>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsStarred { get; set; }
    }

    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommandRequest, AppResponse>
    {
        private readonly IMapper mapper;
        private readonly INoteRepository noteRepository;
        //private readonly CreateNoteValidator validator;

        public UpdateNoteCommandHandler(/*CreateNoteValidator validator,*/IMapper mapper, INoteRepository noteRepository)
        {
            //this.validator = validator;
            this.noteRepository = noteRepository;
            this.mapper = mapper;
        }

        public async Task<AppResponse> Handle(UpdateNoteCommandRequest request, CancellationToken cancellationToken)
        {
            var entity = mapper.Map<Note>(request);
            var result = await noteRepository.Update(entity);
            return result == null
                ? new ErrorResponse(Messages.ERROR_OCCURRED)
                : new SuccessResponse(Messages.CREATED_TAG_SUCCESSFULLY);
        }
    }
}

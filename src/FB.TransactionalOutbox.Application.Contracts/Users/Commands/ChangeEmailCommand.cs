using System;
using AutoMapper;
using FB.TransactionalOutbox.Application.Contracts.Users.Dtos.Request;
using MediatR;

namespace FB.TransactionalOutbox.Application.Contracts.Users.Commands
{
    [AutoMap(typeof(ChangeEmailRequestDto))]
    public class ChangeEmailCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FB.TransactionalOutbox.Application.Contracts.Events;
using FB.TransactionalOutbox.Application.Contracts.Events.Dtos;
using FB.TransactionalOutbox.Application.Users;
using FB.TransactionalOutbox.Infrastructure;
using FB.TransactionalOutbox.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FB.TransactionalOutbox.Application.Events
{
    public class EventAppService : BaseAppService, IEventAppService
    {
        private readonly ApplicationDbContext _dbContext;

        public EventAppService(IMapper mapper, ILogger<UserAppService> logger, 
                ApplicationDbContext dbContext) : base(mapper, logger)
        {
            _dbContext = dbContext;
        }

        public async Task<IList<EventDto>> GetUndeletedEventsAsync()
        {
            var events = await _dbContext.Events.AsNoTracking().Where(e => !e.IsDeleted).ToListAsync();
            var mapped = Mapper.Map<IList<EventDto>>(events);
            return mapped;
        }

        public async Task<IList<EventDto>> GetEventsAsync()
        {
            var events = await _dbContext.Events.AsNoTracking().ToListAsync();
            var mapped = Mapper.Map<IList<EventDto>>(events).ToList();
            mapped.ForEach(e => e.EventBody = JsonConvert.DeserializeObject(e.EventBody?.ToString()));
            return mapped;
        }

        public async Task<bool> Delete(IList<Guid> ids)
        {
            var events = await _dbContext.Events.Where(e => ids.Contains(e.Id)).ToListAsync();
            events.ForEach(e => e.Delete());

            _dbContext.Events.UpdateRange(events);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
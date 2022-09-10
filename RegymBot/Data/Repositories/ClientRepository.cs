using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RegymBot.Data.Base;
using RegymBot.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegymBot.Data.Repositories
{
    public class ClientRepository : BaseRepository<ClientEntity>
    {
        public ClientRepository(AppDbContext context, ILogger<ClientRepository> logger)
            : base(context, logger) { }

        public async Task<ClientEntity> AddNewClientAsync(ClientEntity client)
        {
            try
            {
                var addedClient = await Insert(client);
                _logger.LogInformation($"Inserted new record in client table with guid: {addedClient.ClientGuid}");

                return addedClient;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error on insert {typeof(ClientEntity)}");
                throw;
            }
        }

        public async Task<IEnumerable<ClientEntity>> LoadAllAsync()
        {
            try
            {
                var clients = await _context.Clients.Where(e => e.Finished).ToListAsync();
                _logger.LogInformation($"Loaded all enrolls");

                return clients;
            }
            catch(Exception e)
            {
                _logger.LogError(e, $"Error on loading all {typeof(ClientEntity)}");
                throw;
            }
        }

        public async Task<ClientEntity> GetByGuidAsync(Guid clientGuid)
        {
            try
            {
                var client = await _context.Clients.FirstOrDefaultAsync(c => c.ClientGuid == clientGuid);
                _logger.LogInformation($"Loaded client with guid: {client.ClientGuid}");

                return client;
            }
            catch(Exception e)
            {
                _logger.LogError(e, $"Error on loading {typeof(ClientEntity)}, guid: {clientGuid}");
                throw;
            }
        }

        public async Task UpdateEnrollAsync(ClientEntity client)
        {
            try
            {
                await Update(client);

                _logger.LogInformation($"Updated client with guid: {client.ClientGuid}");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error on updating {typeof(ClientEntity)}, guid: {client.ClientGuid}");
                throw;
            }
        }
    }
}

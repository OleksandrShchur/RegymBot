using Microsoft.Extensions.Logging;
using RegymBot.Data.Base;
using RegymBot.Data.Entities;
using System;
using System.Threading.Tasks;

namespace RegymBot.Data.Repositories
{
    public class CredentialsRepository : BaseRepository<CredentialsEntitiy>
    {
        public CredentialsRepository(AppDbContext context, ILogger<FeedbackRepository> logger)
            : base(context, logger) { }
    }
}

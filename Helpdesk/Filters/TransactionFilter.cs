using Database;
using Microsoft.EntityFrameworkCore.Storage;

namespace Helpdesk.Filters;

internal class DbTransactionFilter : IActionFilter
{
    private readonly HelpdeskDbContext _dbContext;
    private IDbContextTransaction? _transaction;

    public DbTransactionFilter(HelpdeskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _transaction = _dbContext.Database.BeginTransaction();
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception == null)
        {
            _transaction?.Commit();
        }
        else
        {
            _transaction?.Rollback();
        }

        _transaction?.Dispose();
    }
}
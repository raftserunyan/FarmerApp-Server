using System.Collections;
using FarmerApp.Data.DAO;
using FarmerApp.Data.Entities.Base;
using FarmerApp.Data.Repositories;
using FarmerApp.Shared.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FarmerApp.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FarmerDbContext _context;
        private Hashtable _repositories;

        public UnitOfWork(FarmerDbContext context)
        {
            _context = context;
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(BaseRepository<>);

                var repositoryInstance =
                    Activator.CreateInstance(repositoryType
                        .MakeGenericType(typeof(TEntity)), _context);

                _repositories.Add(type, repositoryInstance);
            }

            return (IRepository<TEntity>)_repositories[type];
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw;// new EntityUpdateConcurrencyException(ex.Entries);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null)
                {
                    var exception = ex.InnerException as SqlException;
                    if (exception != null && exception.Number == 547)
                        throw new BadRequestException("Related entity not found.");
                }
            }
        }
    }
}

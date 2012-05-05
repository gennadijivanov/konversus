using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Conversus.Core.Infrastructure.UnitOfWork
{
    public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
    {
        private static readonly System.TimeSpan DefaultTimeout = new System.TimeSpan(0, 2, 0);

        public override void Commit()
        {
            if (_operations.Count == 0)
                return;

            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions() { IsolationLevel = IsolationLevel.RepeatableRead, Timeout = DefaultTimeout }))
            {
                    foreach (var operation in _operations)
                    {
                        IUnitOfWorkStorageRepository repository = (IUnitOfWorkStorageRepository)operation.Repository;
                        switch (operation.Type)
                        {
                            case TransactionType.Insert:
                                repository.PersistNewItemInStorage(operation.Entity);
                                break;
                            case TransactionType.Delete:
                                repository.PersistDeletedItemInStorage(operation.Entity);
                                break;
                            case TransactionType.Update:
                                repository.PersistUpdatedItemInStorage(operation.Entity);
                                break;
                        }
                    }

                    // Commit the transaction
                    scope.Complete();
            }

            // Clear everything
            _operations.Clear();
        }
    }
}

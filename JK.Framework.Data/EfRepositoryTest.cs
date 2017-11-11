using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JK.Framework.Data
{
    //internal class EfRepositoryTest<T> where T : class
    //{
    //    DbContext _dbContext;

    //    public EfRepositoryTest(DbContext dbContext)
    //    {
    //        _dbContext = dbContext;
    //    }

    //    public T Add(T entity)
    //    {
    //        try
    //        {
    //            _dbContext.Set<T>().Add(entity);
    //            _dbContext.SaveChanges();
    //            return entity;
    //        }
    //        catch (DbEntityValidationException dbEx)
    //        {
    //            throw new Exception(GetFullErrorText(dbEx), dbEx);
    //        }

    //    }

    //    public T Update(T entity)
    //    {
    //        try
    //        {
    //            if (_dbContext.Entry<T>(entity).State == EntityState.Modified)
    //            {
    //                _dbContext.SaveChanges();
    //            }
    //            else if (_dbContext.Entry<T>(entity).State == EntityState.Detached)
    //            {
    //                _dbContext.Set<T>().Attach(entity);
    //                _dbContext.Entry<T>(entity).State = EntityState.Modified;
    //                _dbContext.SaveChanges();
    //            }
    //            return entity;
    //        }
    //        catch (DbEntityValidationException dbEx)
    //        {
    //            throw new Exception(GetFullErrorText(dbEx), dbEx);
    //        }

    //    }


    //    public T Find(params object[] keyValues)
    //    {
    //        return _dbContext.Set<T>().Find(keyValues);
    //    }

    //    public List<T> FindAll()
    //    {
    //        return _dbContext.Set<T>().ToList();
    //    }


    //    protected string GetFullErrorText(DbEntityValidationException exc)
    //    {
    //        var msg = string.Empty;
    //        foreach (var validationErrors in exc.EntityValidationErrors)
    //            foreach (var error in validationErrors.ValidationErrors)
    //                msg += string.Format("Property: {0} Error: {1}", error.PropertyName, error.ErrorMessage) + Environment.NewLine;
    //        return msg;
    //    }

    //    public void Delete(T model)
    //    {
    //        _dbContext.Set<T>().Remove(model);
    //        _dbContext.SaveChanges();
    //    }

    //    public void Delete(params object[] keyValues)
    //    {
    //        T model = Find(keyValues);
    //        if (model != null)
    //        {
    //            _dbContext.Set<T>().Remove(model);
    //            _dbContext.SaveChanges();
    //        }
    //    }
    //}


 
   

    //public partial class UserAccount : EfRepositoryTest<UserAccount>
    //{
    //    public UserAccount(DbContext dbContext) : base(dbContext)
    //    {
    //    }
    //}

    //public class Test
    //{
    //    public UserAccount CreatedUserAccountTwo()
    //    {
    //        UserAccount account = new UserAccount(new AccountEntities());
    //        return account.Add(account);
    //    }
    //}
}

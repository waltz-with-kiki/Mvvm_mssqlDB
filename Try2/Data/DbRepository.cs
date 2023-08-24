using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Try2.Context;
using Try2.Interfaces;

namespace Try2.Data
{
    internal class DbRepository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly UsersContext _db;
        private readonly DbSet<T> _Set;

        private bool AutoSaveChanges { get; set; } = true;

        public DbRepository(UsersContext db)
        {
            _db = db;
            _Set = db.Set<T>();
        }


        public virtual IQueryable<T> Items => _Set;


        public T Add(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

           _db.Entry(item).State = EntityState.Added;
            if(AutoSaveChanges)
            _db.SaveChanges();
            return item;
        }

        public async Task<T> AddAsync(T item, CancellationToken cancel = default)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            _db.Entry(item).State = EntityState.Added;
            if (AutoSaveChanges)
                await _db.SaveChangesAsync();
            return item;
        }

        public T Get(int id) => Items.SingleOrDefault(x => x.Id == id);


        public async Task<T> GetAsync(int id, CancellationToken cancel = default) => await Items.SingleOrDefaultAsync(x => x.Id == id, cancel).ConfigureAwait(false);

        public void Remove(int id)
        {
            var item = _Set.Local.FirstOrDefault(i => i.Id == id) ?? new T { Id = id };

            _db.Remove(item);

            if (AutoSaveChanges)
                _db.SaveChanges();
        }

        public async Task RemoveAsync(int id, CancellationToken cancel = default)
        {
            _db.Remove(new T { Id = id });
            if (AutoSaveChanges)
               await _db.SaveChangesAsync();
        }

        public void Update(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            _db.Entry(item).State = EntityState.Modified;
            if (AutoSaveChanges)
                _db.SaveChanges();
        }

        public async Task UpdateAsync(T item, CancellationToken cancel = default)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            _db.Entry(item).State = EntityState.Modified;
            if (AutoSaveChanges)
                await _db.SaveChangesAsync();
        }
    }

    class UsersRepository : DbRepository<User>
    {

        public override IQueryable<User> Items => base.Items.Include(item => item.Right);

        public UsersRepository(UsersContext db) : base(db)
        {
        }
    }

    class Checking_accountsRepository : DbRepository<Checking_account>
    {

        public override IQueryable<Checking_account> Items => base.Items.Include(item => item.Bank);

        public Checking_accountsRepository(UsersContext db) : base(db)
        {
        }
    }

    class ClientsRepository : DbRepository<Client>
    {
        public override IQueryable<Client> Items => base.Items.Include(item => item.Checking_Account);

        public ClientsRepository(UsersContext db) : base(db)
        {
        }

    }

    class OrdersRepository : DbRepository<Order>
    {
        public override IQueryable<Order> Items => base.Items.Include(item => item.Client).Include(item => item.Flight);

        public OrdersRepository(UsersContext db) : base(db)
        {
        }

    }

    class CargosRepository : DbRepository<Cargo>
    {
        public override IQueryable<Cargo> Items => base.Items.Include(item => item.Unit).Include(item => item.Order);

        public CargosRepository(UsersContext db) : base(db)
        {
        }

    }

    class FlightsRepository : DbRepository<Flight>
    {
        public override IQueryable<Flight> Items => base.Items.Include(item => item.Crew).Include(item => item.Automobile);

        public FlightsRepository(UsersContext db) : base(db)
        {
        }

    }

    class DriversRepository : DbRepository<Driver>
    {
        public override IQueryable<Driver> Items => base.Items.Include(item => item.Crew).Include(item => item.Category).Include(item => item.Class);

        public DriversRepository(UsersContext db) : base(db)
        {
        }

    }


    class AutomobilesRepository : DbRepository<Automobile>
    {
        public override IQueryable<Automobile> Items => base.Items.Include(item => item.Brand);

        public AutomobilesRepository(UsersContext db) : base(db)
        {
        }

    }

}

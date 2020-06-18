using PostSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PostSystem.Data
{
    public class UnitOfWork : IDisposable
    {
        private readonly PostSystemDbContext dbContext;
        private BaseRepository<City> cityRepository;
        private BaseRepository<MailItem> mailRepository;
        private BaseRepository<PostOffice> postOfficeRepository;
        private BaseRepository<Delivery> deliveryRepository;
        private BaseRepository<User> userRepository;
        private bool disposed = false;

        public UnitOfWork()
        {
            this.dbContext = new PostSystemDbContext();
        }

        public BaseRepository<City> CityRepository
        {
            get
            {
                if (this.cityRepository == null)
                {
                    this.cityRepository = new BaseRepository<City>(dbContext);
                }

                return cityRepository;
            }
        }

        public BaseRepository<MailItem> MailRepository
        {
            get
            {
                if (this.mailRepository == null)
                {
                    this.mailRepository = new BaseRepository<MailItem>(dbContext);
                }

                return mailRepository;
            }
        }

        public BaseRepository<PostOffice> PostOfficeRepository
        {
            get
            {
                if (this.postOfficeRepository == null)
                {
                    this.postOfficeRepository = new BaseRepository<PostOffice>(dbContext);
                }

                return postOfficeRepository;
            }
        }

        public BaseRepository<Delivery> DeliveryRepository
        {
            get
            {
                if (this.deliveryRepository == null)
                {
                    this.deliveryRepository = new BaseRepository<Delivery>(dbContext);
                }

                return deliveryRepository;
            }
        }

        public BaseRepository<User> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new BaseRepository<User>(dbContext);
                }

                return userRepository;
            }
        }

        public bool Save()
        {
            try
            {
                dbContext.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }

                disposed = true;
            }
        }
    }
}

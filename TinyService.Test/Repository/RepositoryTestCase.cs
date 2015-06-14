using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.Domain.Repository;
using TinyService.Test.Entity;
using TinyService.Extension.Repository;
using TinyService.Infrastructure;
using TinyService.Service;
using TinyService.autofac;
using Autofac;
using TinyService.EntityFramework;
using System.Data.Entity;

namespace TinyService.Test.Repository
{
    [TestFixture]
    public class RepositoryTestCase
    {
        private IRepository<string, User> _repository;

         IEFRepository<int, Product> _efrepository;
        [SetUp]
        public void Init()
        {

            TinyServiceSetup.Start((configbulider) =>
            {
                configbulider.InitConfig((containerbuilder) =>
                {
                    containerbuilder.RegisterType<UserRepository>().As<IRepository<string, User>>().InstancePerDependency();
                    containerbuilder.RegisterGeneric(typeof(EFRepository<,>)).As(typeof(IEFRepository<,>))
                                    .WithParameter("dbcontext", new pmContext()).InstancePerDependency();
                });
            });
            this._repository = ObjectFactory.GetService<IRepository<string, User>>();
            this._efrepository = ObjectFactory.GetService<IEFRepository<int, Product>>();
        }


        [Test]
        public async void CRUD_TestCase()
        {
            var user = new User("fzf01", 19);
            this._repository.Insert(user);
            user.Name = "fzf03";
            await this._repository.InsertOrUpdateAsync(user);
            Assert.IsTrue(this._repository.Count() > 0);
            Assert.AreEqual(user.Name, "fzf03");
        }


        [Test]
        public async void Ef_Repository_TestCase()
        {

            _efrepository.Insert(new Product()
            {
                Name = "EntityFreamework4"
            });

            await this._efrepository.InsertOrUpdateAsync<int, Product>(new Product()
               {
                   Name = "EntityFreamework1"
               });

            await _efrepository.Context.SaveChangesAsync();

            Console.WriteLine(await _efrepository.CountAsync());
            



        }

        [TearDown]
        public void End()
        {
            this._efrepository.Dispose();
            TinyServiceSetup.Container.Dispose();
        }
    }
}

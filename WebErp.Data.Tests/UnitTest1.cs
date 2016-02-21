using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using WebErp.Data.Infrastructure;
using WebErp.Data.Repositories;
using Ninject.Activation;
using WebErp.Models;
using WebErp.Common;
using System.Data.Entity;

namespace WebErp.Data.Tests
{
    [TestClass]
    public class UnitTest1
    {

        private TestContext testContextInstance;

        /// <summary>
        ///Obtient ou définit le contexte de test qui fournit
        ///des informations sur la série de tests active, ainsi que ses fonctionnalités.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Attributs de tests supplémentaires
        //
        // Vous pouvez utiliser les attributs supplémentaires suivants lorsque vous écrivez vos tests :
        //
        // Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test de la classe
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Utilisez ClassCleanup pour exécuter du code une fois que tous les tests d'une classe ont été exécutés
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
        static IKernel kernel;
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
             kernel = new StandardKernel();

            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind(typeof(IModelBaseRepository<>)).To(typeof(ModelBaseRepository<>));
            kernel.Bind<IDbContextOptions>().ToProvider<DbContextOptionsProvider>().InSingletonScope();
            kernel.Bind(typeof(IDbSet<>)).To(typeof(DbSet<>)).When(_ =>kernel.Get<IDbContextOptions>().InMemory == false);
            kernel.Bind(typeof(IDbSet<>)).To(typeof(FakeDbSet<>)).When(_ =>kernel.Get<IDbContextOptions>().InMemory==true );
            kernel.Bind<WebErpContext>().ToSelf();


             
        }


        [TestMethod]
        public void TestCreateArticleInContext()
        {
           

            using (var ctx=kernel.Get<WebErpContext>())
            {
                Article art = new Article() { ID ="1", Code = "XC10-3.0PET", Societe = 999, Libelle = "Tole Xc10 ep3 petit format" };
                Assert.IsTrue(ctx.ArticleSet is FakeDbSet<Article>);
                ctx.ArticleSet.Add(art);
                ctx.Commit();
                

            }

        }

        [TestMethod]
        public void TestCreateArticleInRepository()
        {
            using (var ctx = kernel.Get<WebErpContext>())
            {
                using (var repo = kernel.Get<IModelBaseRepository<Article>>())
                {
                    Article art0 = new Article() { ID = "1", Code = "XC10-3.0PET", Societe = 999, Libelle = "Tole Xc10 ep3 petit format" };
                    repo.Add(art0);
                    ctx.Commit();

                    var art1=repo.FindBy(a => a.ID == "1").FirstOrDefaultAsync().Result;
                    Assert.AreEqual(art0.Code, art1.Code);
                }
            }
        }

       
    }
}

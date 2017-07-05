using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using SchneiderElectric.InvoicingSystem.Application;
using SchneiderElectric.InvoicingSystem.Presistence;
using SchneiderElectric.InvoicingSystem.Application.Commands;
using SchneiderElectric.InvoicingSystem.Application.Commands.Interfaces;
using SchneiderElectric.InvoicingSystem.Application.Queries;
using SchneiderElectric.InvoicingSystem.Application.Queries.Interfaces;
using SchneiderElectric.InvoicingSystem.Presistence.Shared;
using SchneiderElectric.InvoicingSystem.Application.Interfaces;
using SchneiderElectric.InvoicingSystem.Application.IRepositories;
using SchneiderElectric.InvoicingSystem.Presistence.Repositories;

namespace SchneiderElectric.InvoicingSystem.Presentation.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here

            container.RegisterType<IDatabaseContext, DBInvoice>(new PerRequestLifetimeManager());
            container.RegisterType<IUnitOFWork, UnitOfWork>();

            container.RegisterType<IFileRepository, FileRepository>();

            container.RegisterType<ICommentRepository, CommentRepository>();
            container.RegisterType<ICountryRepository, CountryRepository>();
            container.RegisterType<ICurrencyRepository, CurrencyRepository>();
            container.RegisterType<IDepartmentRepository, DepartmentRepository>();
            container.RegisterType<IEmployeeRepository, EmployeeRepository>();
            container.RegisterType<IProjectRepository, ProjectRepository>();

            container.RegisterType<IExpenseRepository, ExpenseRepository>();
            container.RegisterType<ISelfExpenseRepository, SelfExpenseRepository>();
            container.RegisterType<IPerdiemExpenseRepository, PerdiedmExpenseRepository>();
            container.RegisterType<IOverTimeExpenseRepository, OverTimeExpenseRepository>();
            container.RegisterType<IRateRepository, RateRepository>();

            container.RegisterType<IExpenseCommands, ExpenseCommands>();

            container.RegisterType<IExpensesQueries, ExpensesQueries>();
            container.RegisterType<IValueElementsQueries, ValueElementsQueries>();
            container.RegisterType<ISearchExpensesQueries, SearchExpensesQueries>();

        }
    }
}

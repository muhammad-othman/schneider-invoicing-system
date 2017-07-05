namespace SchneiderElectric.InvoicingSystem.Presistence
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Domain;
    using ConfigEntities;
    using Application;
    using Shared;
    public class InvoicingDBInitializer : DropCreateDatabaseIfModelChanges<DBInvoice>
    {
        protected override void Seed(DBInvoice context)
        {

            context.Departments.Add(new MockDepartment { DepartmentName = "Finance"});
            context.Departments.Add(new MockDepartment { DepartmentName = "PLC" ,EMId = "33333"});

            context.MockEmployees.Add(new MockEmployee {EmployeeSapId= "11111" ,EmployeeName= "Ibrahem Atef"});
            context.MockEmployees.Add(new MockEmployee {EmployeeSapId= "22222", EmployeeName= "Muhammad Othman" });
            context.MockEmployees.Add(new MockEmployee {EmployeeSapId= "33333", EmployeeName= "Mr EM" });
            context.MockEmployees.Add(new MockEmployee {EmployeeSapId= "44444", EmployeeName= "Mr Finance"});

            context.SaveChanges();

            context.MockEmployees.Where(e => e.EmployeeSapId == "11111").First().DepartmentId = 2;
            context.MockEmployees.Where(e => e.EmployeeSapId == "44444").First().DepartmentId = 1;
            context.Departments.Where(e => e.DepartmentName == "PLC").First().EMId = "33333";

            context.MockProjects.Add(new MockProject {ProjectSapId = "EG_11111", ProjectName= "Oil Rig", PAId="22222"});

            context.Rates.Add(new Rate { RateType = "As per assignment letter USD rate 1" });
            context.Rates.Add(new Rate { RateType = "As per assignment letter USD rate 2" });
            context.Rates.Add(new Rate { RateType = "As per assignment letter USD rate 3" });
            context.Rates.Add(new Rate { RateType = "As per assignment letter USD rate 4" });
            context.Rates.Add(new Rate { RateType = "As per assignment letter EGP rate 1" });
            context.Rates.Add(new Rate { RateType = "As per assignment letter EGP rate 2" });
            context.Rates.Add(new Rate { RateType = "Perdiem portion paid directly by SE Front Office" });

            context.Countries.Add(new CountryList { ID = 1, City = "Washington", Country = "USA" });
            context.Countries.Add(new CountryList { ID = 2, City = "Cairo", Country = "ARE" });
            context.Countries.Add(new CountryList { ID = 3, City = "Abu Dhabi", Country = "UAE" });
            context.Countries.Add(new CountryList { ID = 4, City = "Gedda", Country = "KSA" });
            context.Countries.Add(new CountryList { ID = 5, City = "Stockholm", Country = "SWE" });

            context.Currencies.Add(new Currency { CurrencyName = "US Dollar", CurrencySymbol = "USD", RateToUS = 1, CountryId = 1 });
            context.Currencies.Add(new Currency { CurrencyName = "Egyptian Pound", CurrencySymbol = "EGP", RateToUS = 18, CountryId = 2 });
            context.Currencies.Add(new Currency { CurrencyName = "United Arab Emirates Dirham", CurrencySymbol = "AED", RateToUS =3.67m, CountryId = 3 });
            context.Currencies.Add(new Currency { CurrencyName = "Saudi Riyal", CurrencySymbol = "SAR", RateToUS =3.75m, CountryId = 4 });

            context.SaveChanges();

            Guid firstExpenseID = Guid.NewGuid();
            Guid secondExpenseID = Guid.NewGuid();
            Guid thirdExpenseID = Guid.NewGuid();

            context.Expenses.Add(new Expense { ExpenseId = firstExpenseID, ExpenseDescription = "first test expense", EmployeeSapId = "11111", ProjectSapId = "EG_11111", CountryListID = 2, ExpenseState = ExpenseState.AtEmployeeSaved });
            context.Expenses.Add(new Expense { ExpenseId = secondExpenseID, ExpenseDescription = "second test expense", EmployeeSapId = "11111", ProjectSapId = "EG_11111", CountryListID = 4, ExpenseState = ExpenseState.AtPARejected});
            context.Expenses.Add(new Expense { ExpenseId = thirdExpenseID, ExpenseDescription = "third test expense", EmployeeSapId = "11111", ProjectSapId = "EG_11111", CountryListID = 3, ExpenseState = ExpenseState.AtEM });

            context.SaveChanges();

            context.SelfExpenses.Add(new SelfExpense { SelfExpenseId = Guid.NewGuid(),ExpenseId = firstExpenseID,CurrencyID = 2,Amount=15454,Description="first self testtt",RateToUS=19,ExpenseType = ExpenseType.Accomodation});
            context.SelfExpenses.Add(new SelfExpense { SelfExpenseId = Guid.NewGuid(),ExpenseId = firstExpenseID,CurrencyID = 3,Amount=5452,Description="second self testtt",RateToUS=12,ExpenseType = ExpenseType.Medical});
            context.SelfExpenses.Add(new SelfExpense { SelfExpenseId = Guid.NewGuid(),ExpenseId = firstExpenseID,CurrencyID = 4,Amount=320,Description="third self testtt",RateToUS=3,ExpenseType = ExpenseType.Accomodation});
            context.OverTimeExpenses.Add(new OverTimeExpense {OverTimeExpenseId = Guid.NewGuid(),ExpenseId = firstExpenseID,NumberOfHours=40,Amount=5452,ExpenseCategory = ExpenseCategory.Weekend_Holiday,Description="first OverTimeeeee" });
            context.PerdiemExpenses.Add(new PerdiemExpense {PerdiemExpenseId= Guid.NewGuid(),ExpenseId = firstExpenseID,NumberOfDays=50,Amount=5452,RateId = 2,Description="first perDieeeeeem" });



            context.SelfExpenses.Add(new SelfExpense { SelfExpenseId = Guid.NewGuid(), ExpenseId = secondExpenseID, CurrencyID = 2, Amount = 15454, Description = "third self testtt", RateToUS = 19, ExpenseType = ExpenseType.Other });
            context.SelfExpenses.Add(new SelfExpense { SelfExpenseId = Guid.NewGuid(), ExpenseId = secondExpenseID, CurrencyID = 3, Amount = 5452, Description = "forth self testtt", RateToUS = 12, ExpenseType = ExpenseType.Medical });
            context.SelfExpenses.Add(new SelfExpense { SelfExpenseId = Guid.NewGuid(), ExpenseId = secondExpenseID, CurrencyID = 4, Amount = 320, Description = "fifth self testtt", RateToUS = 3, ExpenseType = ExpenseType.Transportation });
            context.OverTimeExpenses.Add(new OverTimeExpense { OverTimeExpenseId = Guid.NewGuid(), ExpenseId = secondExpenseID, NumberOfHours = 40, Amount = 5452, ExpenseCategory = ExpenseCategory.Weekend_Holiday, Description = "second OverTimeeeee" });
            context.PerdiemExpenses.Add(new PerdiemExpense { PerdiemExpenseId = Guid.NewGuid(), ExpenseId = secondExpenseID, NumberOfDays = 50, Amount = 5452, RateId = 2, Description = "second perDieeeeeem" });



            context.SelfExpenses.Add(new SelfExpense { SelfExpenseId = Guid.NewGuid(), ExpenseId = thirdExpenseID, CurrencyID = 2, Amount = 15454, Description = "sixth self testtt", RateToUS = 19, ExpenseType = ExpenseType.Medical });
            context.SelfExpenses.Add(new SelfExpense { SelfExpenseId = Guid.NewGuid(), ExpenseId = thirdExpenseID, CurrencyID = 3, Amount = 5452, Description = "seventh self testtt", RateToUS = 12, ExpenseType = ExpenseType.Medical });
            context.SelfExpenses.Add(new SelfExpense { SelfExpenseId = Guid.NewGuid(), ExpenseId = thirdExpenseID, CurrencyID = 4, Amount = 320, Description = "eighth self testtt", RateToUS = 3, ExpenseType = ExpenseType.Accomodation });
            context.OverTimeExpenses.Add(new OverTimeExpense { OverTimeExpenseId = Guid.NewGuid(), ExpenseId = thirdExpenseID, NumberOfHours = 40, Amount = 5452, ExpenseCategory = ExpenseCategory.Weekend_Holiday, Description = "third OverTimeeeee" });
            context.PerdiemExpenses.Add(new PerdiemExpense { PerdiemExpenseId = Guid.NewGuid(), ExpenseId = thirdExpenseID, NumberOfDays = 50, Amount = 5452, RateId = 2, Description = "third perDieeeeeem" });

            base.Seed(context);
        }
    }
    public partial class DBInvoice : DbContext,IDatabaseContext
    {
        public void Save()
        {
            this.SaveChanges();
        }
        public DBInvoice()
            : base("name=DBInvoice")
        {
            Database.SetInitializer(new InvoicingDBInitializer());
        }
        public virtual IDbSet<MockEmployee> MockEmployees { get; set; }
        public virtual IDbSet<MockProject> MockProjects { get; set; }
        public virtual IDbSet<CountryList> Countries { get; set; }
        public virtual IDbSet<Currency> Currencies { get; set; }
        public virtual IDbSet<Expense> Expenses { get; set; }
        public virtual IDbSet<OverTimeExpense> OverTimeExpenses { get; set; }
        public virtual IDbSet<PerdiemExpense> PerdiemExpenses { get; set; }
        public virtual IDbSet<Rate> Rates { get; set; }
        public virtual IDbSet<MockDepartment> Departments { get; set; }
        public virtual IDbSet<RejectedComment> RejctedComments { get; set; }
        public virtual IDbSet<SelfExpense> SelfExpenses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            // Moved all 
            modelBuilder.Configurations.Add(new ConfigExpenseEntity());

            modelBuilder.Configurations.Add(new ConfigSelfExpenseEntity());

            modelBuilder.Configurations.Add(new ConfigOverTimeExpenseEntity());

            modelBuilder.Configurations.Add(new ConfigPerdiemExpenseEntity());

            modelBuilder.Configurations.Add(new ConfigCurrencyEntity());

            modelBuilder.Configurations.Add(new ConfigCountryListEntity());

            modelBuilder.Configurations.Add(new ConfigMockEmployeeEntity());

            modelBuilder.Configurations.Add(new ConfigMockProjectEntity());

            modelBuilder.Configurations.Add(new ConfigRateEntity());

            modelBuilder.Configurations.Add(new ConfigRejctedCommentEntity());

            modelBuilder.Configurations.Add(new ConfigMockDepartmentEntity());

            modelBuilder.Configurations.Add(new ConfigFileEntity());
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

    }
}

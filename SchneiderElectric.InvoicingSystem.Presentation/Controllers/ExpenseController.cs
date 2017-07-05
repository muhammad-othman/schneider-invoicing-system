using SchneiderElectric.InvoicingSystem.Application.Commands.Interfaces;
using SchneiderElectric.InvoicingSystem.Application.Queries.Interfaces;
using SchneiderElectric.InvoicingSystem.Domain;
using SchneiderElectric.InvoicingSystem.Domain.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace SchneiderElectric.InvoicingSystem.Presentation.Controllers
{
    public class ExpenseController : ApiController
    {

        private IExpensesQueries _expensesQueries;
        private ISearchExpensesQueries _SearchExpensesQueries;
        private IExpenseCommands    _ExpenseCommands;

        public ExpenseController(IExpensesQueries queries, ISearchExpensesQueries search, IExpenseCommands expenseCommands)
        {
            _expensesQueries = queries;
            _SearchExpensesQueries = search;
            _ExpenseCommands = expenseCommands;
        }

        [HttpGet]
        [Route("api/expense/getByID")]
        public ExpenseDTO GetByID(Guid id)
        {
            return _expensesQueries.GetDTOById(id);
        }
        [HttpGet]
        [Route("api/expense/getall")]
        public List<ExpenseDTO> GetAllExpenses()
        {
            return _expensesQueries.GetAll();
        }
        /// <summary>
        /// Returns Saved for Employee
        /// Returns At project admin for PA
        /// Returns AtEM for EM
        /// Returns ATFinance for Finance
        /// </summary>
        /// 

        public List<ExpenseDTO> Get()
        {
            return _expensesQueries.GetActiveExpenses(GetUserSapId());
        }

        [HttpGet]
        [Route("api/expense/all")]
        public List<ExpenseDTO> GetAll()
        {
            return _expensesQueries.GetAllAvailableExpenses(GetUserSapId());
        }
        [HttpGet]
        [Route("api/expense/canceled")]
        public List<ExpenseDTO> Canceled()
        {
            return _expensesQueries.GetCanceled(GetUserSapId());
        }
        [HttpGet]
        [Route("api/expense/rejected")]
        public List<ExpenseDTO> Rejected()
        {
            return _expensesQueries.GetRejected(GetUserSapId());
        }

        [HttpPost]
        [Route("api/expense/create")]
        public IHttpActionResult Post(Expense e)
        {
            if (_ExpenseCommands.Create(e))
                return SaveFiles(e);
            else
                return BadRequest();
        }

        public IHttpActionResult SaveFiles(Expense expense)
        {
            try
            {
                string p1 = "~/Temp/" + GetUserSapId() + "/";
                string p2 = "~/Locker/";

                string tempPath = System.Web.Hosting.HostingEnvironment.MapPath(p1);
                string destinationPath = System.Web.Hosting.HostingEnvironment.MapPath(p2);

                foreach (Domain.File file in expense.Files)
                {
                    // To move a file or folder to a new location:
                    string t1 = tempPath + file.Name;
                    string t2 = destinationPath + file.FileId + "_" + file.Name;

                    if (System.IO.File.Exists(t1) && !System.IO.File.Exists(t2))
                        System.IO.File.Move(t1, t2);
                }
                //clear Temp Folder
                System.IO.DirectoryInfo di = new DirectoryInfo(tempPath);
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                return Ok(expense.ExpenseId);
            }
            catch
            {
                return BadRequest();
            }
        }

        public bool Put(Guid expenseId, [FromBody] Expense expense)
        {
             return _ExpenseCommands.Edit(expense, expenseId);
        }


        [HttpPost]
        [Route("api/expense/cancel")]
        public IHttpActionResult Cancel(Guid expenseId)
        {
            if (_ExpenseCommands.Cancel(expenseId, GetUserSapId()))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/expense/recall")]
        public IHttpActionResult Recall(Guid expenseId)
        {
            if (_ExpenseCommands.Recall(expenseId, GetUserSapId()))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/expense/submit")]
        public IHttpActionResult Submit([FromBody]Expense expense)
        {
            if (_ExpenseCommands.Submit(expense, GetUserSapId()))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("api/expense/getRejectedComments")]
        public List<RejectedComment> RejectedComments(Guid expenseId)
        {
            return _expensesQueries.GetRejctedComments(expenseId);
        }
        [NonAction]
        string GetUserSapId()
        {
            return HttpContext.Current.Session["userSapId"].ToString();
        }

        #region NewUpload

        [HttpPost]
        [Route("api/expense/new_create")]
        public IHttpActionResult NewPost(Expense expense)
        {
            if (_ExpenseCommands.Create(expense, HttpContext.Current.Session["tempFilesList"] as List<Tuple<Guid, string>>))
            {
                if (MoveFilesFromTempToLocker(expense))
                    return Ok();
            }

            return BadRequest();
        }

        public bool MoveFilesFromTempToLocker(Expense expense)
        {
            try
            {
                string path1 = "~/Temp/" + GetUserSapId() + "/";
                string path2 = "~/Locker/" + expense.ExpenseId + "/";

                var tempFolderPath = System.Web.Hosting.HostingEnvironment.MapPath(path1);

                // the folder where files will be saved permanently
                string destinationPath = System.Web.Hosting.HostingEnvironment.MapPath(path2);
                Directory.CreateDirectory(destinationPath);

                MoveFiles(expense.Files, tempFolderPath, destinationPath);

                //clear Temp Folder
                DirectoryInfo directory = new DirectoryInfo(tempFolderPath);
                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void MoveFiles(ICollection<Domain.File> files, string tempFolderPath, string destinationPath)
        {
            foreach (var file in files)
            {
                // To move a file or folder to a new location:
                var fileInTempLocation = tempFolderPath + file.Name;
                var fileInDestinationLocation = destinationPath + file.FileId + "_" + file.Name;

                if (System.IO.File.Exists(fileInTempLocation) && !System.IO.File.Exists(fileInDestinationLocation))
                    System.IO.File.Move(fileInTempLocation, fileInDestinationLocation);
            }
        }

        #endregion
    }
}

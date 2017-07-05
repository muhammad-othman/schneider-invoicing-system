using SchneiderElectric.InvoicingSystem.Application.IRepositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace SchneiderElectric.InvoicingSystem.Presentation.Controllers
{
    public class FilesController : ApiController
    {

        IFileRepository _fileRepository;

        public FilesController(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        [HttpGet]
        [Route("api/files/getfiles")]
        public List<Domain.File> getFilesByExpenseID(Guid ExpenseId)
        {
            List<Domain.File> filesExpenses = _fileRepository.FindBy(f => f.ExpenseId == ExpenseId).ToList();
            return filesExpenses;
        }

        [HttpPost]
        [Route("api/files/upload")]
        public string Upload()
        {
            int iUploadedCnt = 0;

            // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
            string sPath = "";
            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Temp/" + GetUserSapId()+"/") ;

            Directory.CreateDirectory(sPath);


            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;

            // CHECK THE FILE COUNT.
            for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++)
            {
                System.Web.HttpPostedFile hpf = hfc[iCnt];

                if (hpf.ContentLength > 0)
                {
                    // CHECK IF THE SELECTED FILE(S) ALREADY EXISTS IN FOLDER. (AVOID DUPLICATE)
                    if (!System.IO.File.Exists(sPath + Path.GetFileName(hpf.FileName)))
                    {
                        // SAVE THE FILES IN THE FOLDER.
                        hpf.SaveAs(sPath + Path.GetFileName(hpf.FileName));
                        iUploadedCnt = iUploadedCnt + 1;
                    }
                }
            }

            // RETURN A MESSAGE (OPTIONAL).
            if (iUploadedCnt > 0)
            {
                return iUploadedCnt + " Files Uploaded Successfully";
            }
            else
            {
                return "Upload Failed";
            }
        }

        [HttpGet]
        [Route("api/files/download")]
        public HttpResponseMessage DownloadFile(Guid fileId)
        {
            var file = _fileRepository.FindById(fileId);
            //https://stackoverflow.com/questions/24080018/download-file-from-an-asp-net-web-api-method-using-angularjs
            string fileName = file.FileId + "_" + file.Name;

            HttpResponseMessage result = new HttpResponseMessage();

            var localFilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/Locker/" + fileName);

            if (!System.IO.File.Exists(localFilePath))
            {
                result.StatusCode = HttpStatusCode.Gone;
            }
            else
            {
                // Serve the file to the client
                result.StatusCode = HttpStatusCode.OK;
                var fileStream = new FileStream(localFilePath, FileMode.Open, FileAccess.Read);
                var byteArray = new Byte[fileStream.Length];
                fileStream.Read(byteArray, 0, byteArray.Length);
                result.Content = new ByteArrayContent(byteArray);
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = file.Name;
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            }
            return result;
        }

        [HttpGet]
        [Route("api/files/cleartemp")]
        public bool clearTemp()
        {
            string p1 = "~/Temp/"+GetUserSapId()+"/";
            string tempPath = System.Web.Hosting.HostingEnvironment.MapPath(p1);

            System.IO.DirectoryInfo di = new DirectoryInfo(tempPath);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            return true;
        }

        [HttpPost]
        [Route("api/files/unload")]
        public bool unloadFile(string FileName)
        {
            //string FileName= "beads.txt";

            string p1 = "~/Temp/"+ GetUserSapId()+"/";
            string tempPath = System.Web.Hosting.HostingEnvironment.MapPath(p1) + FileName;

            if (System.IO.File.Exists(tempPath))
                System.IO.File.Delete(tempPath);
            else
                return false;
            return true;
        }

        string GetUserSapId()
        {
            return HttpContext.Current.Session["userSapId"].ToString();
        }

        #region NewUpload


        [HttpPost]
        [Route("api/files/new_upload")]
        public IHttpActionResult NewUpload()
        {

            try
            {
                string path = "~/Temp/" + GetUserSapId() + "/";

                // this dictionary contains the file names along with their sub-expenses
                List<Tuple<Guid, string>> tempFilesList = new List<Tuple<Guid, string>>();

                // DEFINE THE PATH WHERE WE WANT TO SAVE THE FILES.
                var tempFolderPath = System.Web.Hosting.HostingEnvironment.MapPath(path);

                Directory.CreateDirectory(tempFolderPath);

                var files = HttpContext.Current.Request.Files;

                // CHECK THE FILE COUNT.
                for (int i = 0; i < files.Count; i++)
                {
                    var file = files[0];

                    if (file.ContentLength > 0)
                    {
                        // SAVE THE FILES IN THE FOLDER.
                        // prevent naming collision
                        var fileName = GetNonCollidingName(tempFolderPath, file.FileName);
                        file.SaveAs(tempFolderPath + fileName);

                        tempFilesList.Add(Tuple.Create(new Guid(files.AllKeys[i]), fileName));
                    }
                }

                // store the tempList in the Session
                HttpContext.Current.Session["tempFilesList"] = tempFilesList;

                return Ok();
            }
            catch (Exception ex)
            {
                clearTemp();
                return BadRequest(ex.Message);
            }

        }

        private string GetNonCollidingName(string tempFolderPath, string fileName)
        {
            string original_name_copy = String.Empty;
            int counter = 0;

            while (File.Exists(fileName))
                fileName = original_name_copy + " copy(" + (++counter) + ")";

            return fileName;
        }

        [HttpGet]
        [Route("api/files/new_download")]
        public HttpResponseMessage NewDownloadFile(Guid fileId, Guid expenseId)
        {
            var file = _fileRepository.FindById(fileId);

            string fileName = file.FileId + "_" + file.Name;

            HttpResponseMessage result = new HttpResponseMessage();

            var localFilePath = System.Web.Hosting.HostingEnvironment.MapPath("~/Locker/" + expenseId + "/" + fileName);

            if (!System.IO.File.Exists(localFilePath))
            {
                result.StatusCode = HttpStatusCode.Gone;
            }
            else
            {
                // Serve the file to the client
                result.StatusCode = HttpStatusCode.OK;
                var fileStream = new FileStream(localFilePath, FileMode.Open, FileAccess.Read);
                var byteArray = new Byte[fileStream.Length];
                fileStream.Read(byteArray, 0, byteArray.Length);
                result.Content = new ByteArrayContent(byteArray);
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = file.Name;
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            }
            return result;
        }

        #endregion
    }
}

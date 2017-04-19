
using System.Web;

namespace ZipfItUp.DataTranfer
{
    using System.IO;
    using ZipfItUp.Models;
    public class FileUpload
    {
        public string Name { get; set; }
        public HttpPostedFileBase UploadedFile { get; set; }
        public string UserText { get; set; }
        public DocumentType DocType { get; set; }
    }
}
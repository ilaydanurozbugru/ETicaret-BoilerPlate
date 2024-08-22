using Abp.Web.Models;

namespace ETicaret.Common.Dto
{
    public class UploadFileOutput : ErrorInfo
    {
        public string FileName { get; set; }

        public string FileType { get; set; }

        public string FileToken { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public UploadFileOutput()
        {

        }

        public UploadFileOutput(ErrorInfo error)
        {
            Code = error.Code;
            Details = error.Details;
            Message = error.Message;
            ValidationErrors = error.ValidationErrors;
        }
    }
}

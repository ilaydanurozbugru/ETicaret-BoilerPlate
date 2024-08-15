using Abp.AutoMapper;
using ETicaret.Products.Dto;
using ETicaret.Web.Models.Common;

namespace ETicaret.Web.Models.Products
{
    [AutoMapFrom(typeof(GetProductForEditOutput))]
    public class EditProductModalViewModel : GetProductForEditOutput
    {

    }
}
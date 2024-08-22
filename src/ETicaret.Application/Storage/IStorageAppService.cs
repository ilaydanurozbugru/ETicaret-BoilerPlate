using System;
using System.Threading.Tasks;

namespace ETicaret.Storage
{
    public  interface IStorageAppService
    {
        Task<Guid> UpdateFile(string token, Guid? oldFile);
    }
}

using Abp.UI;
using ETicaret.Storage.Dto;
using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks; 

namespace ETicaret.Storage
{
    public class StorageAppService: ETicaretAppServiceBase, IStorageAppService
    {
        private const int MaxProfilPictureBytes = 5242880; //5MB
        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly IBinaryObjectManager _binaryObjectManager;

        public StorageAppService(ITempFileCacheManager tempFileCacheManager, IBinaryObjectManager binaryObjectManager)
        {
            _tempFileCacheManager = tempFileCacheManager;
            _binaryObjectManager = binaryObjectManager;
        }
        public async Task<Guid> UpdateFile(string token, Guid? oldFile)
        {
            var bytes = _tempFileCacheManager.GetFile(token);

            if (bytes == null)
            {
                throw new UserFriendlyException("There is no such image file with the token: " + token);
            }

            if (bytes.Length > MaxProfilPictureBytes)
            {
                throw new UserFriendlyException(L("ResizedProfilePicture_Warn_SizeLimit", 5));
            }

            if (oldFile.HasValue)
            {
                await _binaryObjectManager.DeleteAsync(oldFile.Value);
            }

            var storedFile = new BinaryObject(AbpSession.TenantId, bytes);
            await _binaryObjectManager.SaveAsync(storedFile);

            return storedFile.Id;
        }

        public async Task<Guid> UpdateImage(UploadImage input)
        {

            var bytes = _tempFileCacheManager.GetFile(input.FileToken);

            if (bytes == null)
            {
                throw new UserFriendlyException("There is no such image file with the token: " + input.FileToken);
            }

            byte[] byteArray;

            using (var bmpImage = new Bitmap(new MemoryStream(bytes)))
            {
                int width = Math.Min(input.Width == 0 ? bmpImage.Width : input.Width, bmpImage.Width);
                int height = Math.Min(input.Height == 0 ? bmpImage.Height : input.Height, bmpImage.Height);

                var cropArea = new Rectangle(input.X, input.Y, width, height);

                using (var bmCrop = bmpImage.Clone(cropArea, bmpImage.PixelFormat))
                using (var stream = new MemoryStream())
                {
                    bmCrop.Save(stream, bmpImage.RawFormat);
                    byteArray = stream.ToArray();
                }
            }


            if (byteArray.Length > MaxProfilPictureBytes)
            {
                throw new UserFriendlyException(L("ResizedProfilePicture_Warn_SizeLimit", 5));
            }

            if (input.OldFile.HasValue)
            {
                await _binaryObjectManager.DeleteAsync(input.OldFile.Value);
            }

            var storedFile = new BinaryObject(AbpSession.TenantId, byteArray);
            await _binaryObjectManager.SaveAsync(storedFile);

            return storedFile.Id;
        }
    }
}

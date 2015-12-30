﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using TaskLogger.Data.Abstract;
using TaskLogger.Data.Models;
using TaskLogger.Data.Responses;
using TaskLogger.Models;

namespace TaskLogger.Controllers
{
    [RoutePrefix("api/userimage")]
    public class UserImageController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserImageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Route("get/{id}")]
        public async Task<IHttpActionResult> GetUserImageAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return InternalServerError(new Exception("user id is null"));
            }
            try
            {
                var userImageEntry =
                    (await _unitOfWork.UserImageRepository.GetAsync(x => x.UserId == id)).FirstOrDefault();

                if (userImageEntry == null)
                {
                    return Ok(new UserImageResponse() {ErrorMessage = "Could not find the image for user"});
                }

                return Ok(new UserImageResponse() {ImageBytes = userImageEntry.ImageBytes});
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("update")]
        public async Task<IHttpActionResult> UpdateUserImageAsync(UserImageModel model)
        {
            if (model == null)
            {
                return InternalServerError(new Exception("UserModel is null"));
            }

            try
            {
                var userImageEntry = (await _unitOfWork.UserImageRepository.GetAsync(u => u.UserId == model.Id)).FirstOrDefault();
                if (userImageEntry != null)
                {
                    userImageEntry.ImageBytes = model.ImageBytes;
                    await _unitOfWork.UserImageRepository.UpdateAsync(userImageEntry);
                    await _unitOfWork.SaveAsync();
                    return Ok(new UserImageResponse() {InfoMessage = "Image updated"});
                }

                userImageEntry = new UserImage() {ImageBytes = model.ImageBytes, UserId = model.Id};
                await _unitOfWork.UserImageRepository.InsertAsync(userImageEntry);
                await _unitOfWork.SaveAsync();
                return Ok(new UserImageResponse() { InfoMessage = "Image added" });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Route("delete/{id}")]
        public async Task<IHttpActionResult> DeleteUserImage(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return InternalServerError(new Exception("user id is null"));
            }
            try
            {
                var exists = await _unitOfWork.UserImageRepository.ExistsAsync(id);

                if (!exists)
                {
                    return Ok(new UserImageResponse() { ErrorMessage = "Could not find the image for user" });
                }

                await _unitOfWork.UserImageRepository.DeleteAsync(id);
                return Ok(new UserImageResponse() { InfoMessage = "Image deleted"});
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        } 
    }
}

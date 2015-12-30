using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using TaskLogger.Data.Abstract;
using TaskLogger.Data.Models;

namespace TaskLogger.Controllers
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity;

    [RoutePrefix("api/accounts")]
    public class UserController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //[Authorize]
        [Route("users")]
        public async Task<IHttpActionResult> GetUsersAsync()
        {
            try
            {
                var users = await UserManager.Users.ToListAsync();

                return Ok(users);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        //[Authorize]
        [Route("user/{id:guid}", Name = "GetUserById")]
        public async Task<IHttpActionResult> GetUser(string id)
        {
            var user = await UserManager.FindByIdAsync(id);

            //user.UserImage = _unitOfWork.UserImageRepository.Get(x => x.User.UserId == id).FirstOrDefault();

            return this.Ok(user);
        }

        //[Authorize]
        [Route("user/{username}")]
        public async Task<IHttpActionResult> GetUserByName(string username)
        {
            var user = await UserManager.FindByNameAsync(username);
            //user.UserImage = (await _unitOfWork.UserImageRepository.GetAsync(x => x.UserId == user.UserId)).FirstOrDefault();

            return this.Ok(user);
        }

        [AllowAnonymous]
        [Route("create")]
        public async Task<IHttpActionResult> CreateUser(CreateUserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var user = new User()
                           {
                               UserName = userModel.Username,
                               Email = userModel.Email,
                           };

            var addUserResult = await UserManager.CreateAsync(user, userModel.Password);

            if (!addUserResult.Succeeded)
            {
                return this.GetErrorResult(addUserResult);
            }

            var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            var callbackUrl = new Uri(Url.Link("ConfirmEmailRoute", new { id = user.Id, code = code }));

            await UserManager.SendEmailAsync(user.Id,"Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");


            var locationHeader = new Uri(Url.Link("GetUserById", new{ id = user.Id}));

            return Created(locationHeader, user);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("ConfirmEmail", Name = "ConfirmEmailRoute")]
        public async Task<IHttpActionResult> ConfirmEmail(string userId = "", string code = "")
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
            {
                ModelState.AddModelError("", "User Id and code is required");
                return this.BadRequest(ModelState);
            }

            var result = await UserManager.ConfirmEmailAsync(userId, code);

            return result.Succeeded ? this.Ok() : this.GetErrorResult(result);
        }

        //[Authorize]
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var result =
                await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return this.Ok();
        }

        //[Authorize]
        [Route("user/{id:guid}")]
        public async Task<IHttpActionResult> DeleteUser(string id)
        {
            var user = await UserManager.FindByIdAsync(id);

            if (user != null)
            {
                var result = await UserManager.DeleteAsync(user);

                if (!result.Succeeded)
                {
                    return this.GetErrorResult(result);
                }

                return this.Ok();
            }

            return this.NotFound();
        }


    }
}
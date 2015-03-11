using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TaskLogger.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http.Description;
    using System.Web.Http.Routing;

    using TaskLogger.Data.Abstract;
    using TaskLogger.Data.Models;
    using TaskLogger.Data.Responses;

    [RoutePrefix("api/tasks")]
    public class UserTaskController : BaseApiController
    {
        private readonly IUnitOfWork _uow;

        public UserTaskController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [Route("all")]
        public async Task<IHttpActionResult> GetAllUserTasks()
        {
            try
            {
                var userTasks = await _uow.UserTaskRepository.GetAsync();

                return this.Ok(new UserTasksResponse{UserTasks = userTasks});
            }
            catch (Exception ex)
            {
                return this.Ok(new UserTasksResponse
                                   {
                                       ErrorMessage = string.Format("Error: {0} : {1}",ex.Message, ex.StackTrace),
                                       UserTasks = null
                                   });
            }
          
        }
        [Route("usertasks/{userId:string}")]
        public async Task<IHttpActionResult> GetUserTasksForUser(string userId, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                IList<UserTask> userTasksForUser;
                if (fromDate.HasValue && toDate.HasValue)
                {
                    userTasksForUser = await _uow.UserTaskRepository.GetAsync(x => x.UserId == userId && x.DateCreated >= fromDate && x.DateCreated <= toDate);
                }
                else
                {
                    userTasksForUser = await _uow.UserTaskRepository.GetAsync(x => x.UserId == userId);
                }
                

                return Ok(new UserTasksResponse { UserTasks = userTasksForUser });
            }
            catch (Exception ex)
            {

                return Ok(new UserTasksResponse
                            {
                                ErrorMessage = string.Format("Error: {0} : {1}", ex.Message, ex.StackTrace),
                                UserTasks = null
                            });
            }
        }

        //todo not sure about ResponseType
        [Route("create")]
        //[ResponseType(typeof(UserTask))]
        [HttpPost]
        public async Task<IHttpActionResult> CreateUserTask(UserTask userTask)
        {
            try
            {
                var existingUserTasks =
                    await _uow.UserTaskRepository.GetAsync(
                        x => x.Name == userTask.Name && x.UserId == userTask.UserId && x.UnitPrice == userTask.UnitPrice);
                if (existingUserTasks.Any())
                {
                    return Ok(new UserTaskResponse
                            {
                                ErrorMessage = string.Format("Error: Task {0} with price {1} already exists", userTask.Name, userTask.UnitPrice)
                            });
                }

                if (!ModelState.IsValid)
                {
                    return this.BadRequest(ModelState);
                }

                await _uow.UserTaskRepository.InsertAsync(userTask);

                await _uow.SaveAsync();

                //todo do we really need to return the userTask itself??
                return this.Ok(new UserTaskResponse
                                   {
                                       InfoMessage = string.Format("User Task {0} created successfully", userTask.Name),
                                       //UserTask = userTask
                                   });
            }
            catch (Exception ex)
            {
                return Ok(new UserTasksResponse
                {
                    ErrorMessage = string.Format("Error: {0} : {1}", ex.Message, ex.StackTrace),
                    UserTasks = null
                });
            }          
        }
    }
}

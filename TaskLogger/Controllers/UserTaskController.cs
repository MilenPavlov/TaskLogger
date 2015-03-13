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
        //todo do we need viewmodel factory??
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
                                       ErrorMessage = string.Format("Error: {0} : {1}",ex.Message, ex.StackTrace)
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
                        x => x.Name == userTask.Name && x.UserId == userTask.UserId);
                if (existingUserTasks.Any())
                {
                    return Ok(new UserTaskResponse
                            {
                                ErrorMessage = string.Format("Error: Task {0} already exists", userTask.Name)
                            });
                }

                if (!ModelState.IsValid)
                {
                    return this.BadRequest(ModelState);
                }

                await _uow.UserTaskRepository.InsertAsync(userTask);

                await _uow.SaveAsync();

                return this.Ok(new UserTaskResponse
                                   {
                                       InfoMessage = string.Format("User Task {0} created successfully", userTask.Name)                                   
                                   });
            }
            catch (Exception ex)
            {
                return Ok(new UserTaskResponse
                {
                    ErrorMessage = string.Format("Error: {0} : {1}", ex.Message, ex.StackTrace),
                    UserTask = null
                });
            }          
        }

        [Route("edit")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateUserTask(UserTask userTask)
        {
            try
            {
                var existingUserTask = (await _uow.UserTaskRepository.GetAsync(x => x.UserTaskId == userTask.UserTaskId)).FirstOrDefault();

                if (existingUserTask != null)
                {
                    if (UserTaskChanged(existingUserTask, userTask))
                    {
                        if (!await ModifiedUserTaskNameExists(userTask))
                        {
                            existingUserTask.Name = userTask.Name;
                            existingUserTask.UnitPrice = userTask.UnitPrice;
                            await _uow.UserTaskRepository.UpdateAsync(existingUserTask);
                            await _uow.SaveAsync();

                            return this.Ok(new UserTaskResponse() {InfoMessage = "Update successful" });
                        }
                       
                        return
                            this.Ok(new UserTaskResponse()
                                    {
                                        ErrorMessage = string.Format("Desired user task name: {0} already exists, please use different name",
                                                userTask.Name)
                                    });
                        
                    }

                    return this.Ok(new UserTaskResponse() { InfoMessage = "No data has been changed" });
                }

                return this.Ok(new UserTaskResponse() { ErrorMessage = string.Format("User task not found") });
            }
            catch (Exception ex) 
            {
                return Ok(new UserTaskResponse
                {
                    ErrorMessage = string.Format("Error: {0} : {1}", ex.Message, ex.StackTrace),
                    UserTask = null
                });
            }
            
        }

        [HttpDelete]
        [Route("delete/{userId:string}")]

        public async Task<IHttpActionResult> DeleteUserTask(string userId)
        {
            try
            {
                var existingUserTask = (await _uow.UserTaskRepository.GetAsync(x => x.UserId == userId)).FirstOrDefault();

                if (existingUserTask == null)
                {
                    return this.Ok(new UserTaskResponse() { ErrorMessage = "User Task not found" });
                }

                await _uow.UserTaskRepository.DeleteAsync(userId);
                await _uow.SaveAsync();

                return this.Ok(new UserTaskResponse() { InfoMessage = "Deleted successfully" });
            }
            catch (Exception ex)
            {
                return Ok(new UserTaskResponse
                {
                    ErrorMessage = string.Format("Error: {0} : {1}", ex.Message, ex.StackTrace),
                    UserTask = null
                });
            }
        }


        private async Task<bool> ModifiedUserTaskNameExists(UserTask userTask)
        {
            return (await _uow.UserTaskRepository.GetAsync(x => x.UserId == userTask.UserId)).FirstOrDefault(
                    x => x.Name == userTask.Name) != null;
        }

        private bool UserTaskChanged(UserTask existingUserTask, UserTask userTask)
        {
            return existingUserTask.Name != userTask.Name || existingUserTask.UnitPrice != userTask.UnitPrice;
        }
    }
}

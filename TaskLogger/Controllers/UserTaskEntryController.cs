﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using TaskLogger.Data.Abstract;
using TaskLogger.Data.Models;
using TaskLogger.Data.Responses;
namespace TaskLogger.Controllers
{


    [RoutePrefix("api/usertaskentry")]
    public class UserTaskEntryController : BaseApiController
    {
        private readonly IUnitOfWork _uow;

        public UserTaskEntryController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IHttpActionResult> GetAllEntries()
        {
            try
            {
                var userTaskEntries = await _uow.UserTaskEntryRepository.GetAsync();

                return this.Ok(userTaskEntries);
            }
            catch (Exception ex)
            {
                return Ok(new UserTaskEntriesResponse()
                            {
                                ErrorMessage = $"Error: {ex.Message} : {ex.StackTrace}"
                            });
            }
        }

        [HttpGet]
        [Route("searchcriteria")]
        public async Task<IHttpActionResult> GetAllEntries(SearchCriteria criteria)
        {
            try
            {             
                if (criteria == null)
                {
                    return Ok(new UserTaskEntriesResponse()
                    {
                        ErrorMessage = "No data received"
                    });
                }

                IList<UserTaskEntry> userTaskEntries;

                if (criteria.FromDate.HasValue && criteria.ToDate.HasValue)
                {
                    userTaskEntries =
                        await
                        _uow.UserTaskEntryRepository.GetAsync(
                            x => x.UserTask.UserId == criteria.UserId 
                                && x.DateTimeCompleted >= criteria.FromDate
                                && x.DateTimeCompleted <= criteria.ToDate);
                }
                else
                {
                    userTaskEntries = await _uow.UserTaskEntryRepository.GetAsync(x => x.UserTask.UserId == criteria.UserId);
                }

                return this.Ok(userTaskEntries);

            }
            catch (Exception ex)
            {
                return Ok(new UserTaskEntriesResponse()
                {
                    ErrorMessage = $"Error: {ex.Message} : {ex.StackTrace}"
                });
            }
        }

        [Route("create")]
        [HttpPost]
        public async Task<IHttpActionResult> CreateUserTaskEntry(UserTaskEntry userTaskEntry)
        {
            try
            {
                if (userTaskEntry == null)
                {
                    return Ok(new UserTaskEntryResponse
                    {
                        ErrorMessage = "No data received"
                    });
                }

                var existingUserTaskEntries =
                    await _uow.UserTaskEntryRepository.GetAsync(
                        x => x.UserTaskId == userTaskEntry.UserTaskId && x.UnitsCompleted == userTaskEntry.UnitsCompleted && x.HoursWorked == userTaskEntry.HoursWorked);
                if (existingUserTaskEntries.Any())
                {
                    return Ok(new UserTaskEntryResponse
                    {
                        ErrorMessage = String.Intern($"Error: Task Entry for {userTaskEntry.DateTimeCompleted.ToShortDateString()} already exists")
                    });
                }

                if (!ModelState.IsValid)
                {
                    return this.BadRequest(ModelState);
                }

                await _uow.UserTaskEntryRepository.InsertAsync(userTaskEntry);

                await _uow.SaveAsync();

                return this.Ok(new UserTaskEntryResponse
                {
                    InfoMessage = "User Task Entry created successfully"
                });
            }
            catch (Exception ex)
            {
                return Ok(new UserTaskEntryResponse
                {
                    ErrorMessage = $"Error: {ex.Message} : {ex.StackTrace}"
                });
            }
        }

        [Route("edit")]
        [HttpPut]
        public async Task<IHttpActionResult> UpdateUserTask(UserTaskEntry userTaskEntry)
        {
            try
            {
                if (userTaskEntry == null)
                {
                    return Ok(new UserTaskEntryResponse
                    {
                        ErrorMessage = "No data received"
                    });
                }

                var existingUserTaskEntry = (await _uow.UserTaskEntryRepository.GetAsync(x => x.UserTaskEntryId == userTaskEntry.UserTaskEntryId)).FirstOrDefault();

                if (existingUserTaskEntry == null)
                {
                    return this.Ok(new UserTaskResponse() { ErrorMessage = string.Format("User task not found") });
                }
                if (!this.UserTaskChanged(existingUserTaskEntry, userTaskEntry))
                {
                    return this.Ok(new UserTaskResponse() { InfoMessage = "No data has been changed" });
                }
                existingUserTaskEntry.UnitsCompleted = userTaskEntry.UnitsCompleted;
                existingUserTaskEntry.HoursWorked = userTaskEntry.HoursWorked;
                await this._uow.UserTaskEntryRepository.UpdateAsync(existingUserTaskEntry);
                await this._uow.SaveAsync();

                return this.Ok(new UserTaskResponse() { InfoMessage = "Update successful" });
            }
            catch (Exception ex)
            {
                return Ok(new UserTaskResponse
                {
                    ErrorMessage = $"Error: {ex.Message} : {ex.StackTrace}",
                    UserTask = null
                });
            }

        }

        [HttpDelete]
        [Route("delete/{userTaskEntry:int}")]
        public async Task<IHttpActionResult> DeleteUserTaskEntry(int userTaskEntryId = 0)
        {
            try
            {
                if (userTaskEntryId == 0)
                {
                    return Ok(new UserTaskEntryResponse
                    {
                        ErrorMessage = "No data received"
                    });
                }

                var existingUserTaskEntry = await _uow.UserTaskEntryRepository.GetByIdAsync(userTaskEntryId);

                if (existingUserTaskEntry == null)
                {
                    return this.Ok(new UserTaskEntryResponse() { ErrorMessage = "User Task Entry not found" });
                }

                await _uow.UserTaskEntryRepository.DeleteAsync(userTaskEntryId);
                await _uow.SaveAsync();

                return this.Ok(new UserTaskEntryResponse() { InfoMessage = "Deleted successfully" });
            }
            catch (Exception ex)
            {
                return Ok(new UserTaskEntryResponse
                {
                    ErrorMessage = $"Error: {ex.Message} : {ex.StackTrace}"
                });
            }
        }

        private bool UserTaskChanged(UserTaskEntry existingUserTaskEntry, UserTaskEntry userTaskEntry)
        {
            return existingUserTaskEntry.UnitsCompleted != userTaskEntry.UnitsCompleted || existingUserTaskEntry.HoursWorked != userTaskEntry.HoursWorked;
        }
    }
}

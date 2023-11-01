﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Data;
using Studenda.Core.Model.Common;

namespace Studenda.Core.Server.Controller
{
    [Route("group")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        public GroupController(DataContext dataContext, IConfiguration configuration)
        {
            DataContext = dataContext;
            Configuration = configuration;
        }

        private DataContext DataContext { get; }
        private IConfiguration Configuration { get; }

        [Route("get")]
        [HttpGet]
        public ActionResult<List<Group>> GetAllGroups()
        {
            var departments = DataContext.Groups.ToList();
            return departments;
        }

        [Route("get/{id:int}")]
        [HttpGet]
        public ActionResult<Group> GetGroupById(int id)
        {
            var department = DataContext.Groups.FirstOrDefault(x => x.Id == id)!;
            return department;
        }

        [Route("post")]
        [HttpPost]
        public IActionResult PostGroups([FromBody] List<Group> subjects)
        {
            try
            {
                DataContext.Groups.AddRange(subjects.ToList());
                DataContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("update")]
        [HttpPut]
        public IActionResult UpdateGroups([FromBody] List<Group> subjects)
        {
            try
            {
                foreach (var subject in subjects)
                {
                    var department = DataContext.Groups.FirstOrDefault(x => x.Id == subject.Id);

                    if (department != null)
                    {
                        DataContext.Groups.Update(department);
                    }
                    else
                    {
                        DataContext.Groups.Add(department!);
                    }
                }

                DataContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("delete")]
        [HttpDelete]
        public IActionResult DeleteGroups([FromBody] List<Group> subjects)
        {
            try
            {
                foreach (var subject in subjects)
                {
                    var department = DataContext.Groups.FirstOrDefault(x => x.Id == subject.Id);

                    if (department != null)
                    {
                        DataContext.Groups.Remove(department);
                    }
                }

                DataContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

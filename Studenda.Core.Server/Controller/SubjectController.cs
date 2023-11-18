﻿using Microsoft.AspNetCore.Mvc;
using Studenda.Core.Data;
using Studenda.Core.Model.Schedule;

namespace Studenda.Core.Server.Controller;

[Route("subjects")]
[ApiController]
public class SubjectController : ControllerBase
{
    public SubjectController(DataContext dataContext, IConfiguration configuration)
    {
        DataContext = dataContext;
        Connfiguration = configuration;
    }

    private DataContext DataContext { get; }
    private IConfiguration Connfiguration { get; }

    [Route("post")]
    [HttpPost]
    public IActionResult AddSubjects([FromBody] List<Subject> subjects)
    {
        try
        {
            DataContext.Subjects.AddRange(subjects);
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
    public IActionResult UpdateSubjects([FromBody] List<Subject> subjects)
    {
        try
        {
            foreach (var subject in subjects)
            {
                if (DataContext.Subjects.Any(u=>u.Id==subject.Id))
                {
                    DataContext.Subjects.Update(subject);
                }
                else
                {
                    DataContext.Subjects.Add(subject!);
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
    public IActionResult DeleteSubjects([FromBody] List<int> subjectsId)
    {
        try
        {
            foreach(var subjectId in subjectsId)
            {
                var Subject = DataContext.Subjects.FirstOrDefault(x=>x.Id==subjectId);
                if(Subject != null)
                {
                    DataContext.Subjects.Remove(Subject);
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

    [Route("student/get/{groupId:int}/{weekType:int}")]
    [HttpGet]
    public ActionResult<List<Subject>> GetScheduleToStudent(int groupId, int weekType)
    {
        return DataContext.Subjects.Where(x => x.Group.Id == groupId && x.WeekType.Index == weekType).OrderBy(x => x.DayPosition).ThenBy(x => x.SubjectPosition).ToList();
    }

    [Route("teacher/id/{id:int}/{weekType:int}")]
    [HttpGet]
    public ActionResult<List<Subject>> GetScheduleToTeacher(int id,int weekType)
    {
        return DataContext.Subjects.Where(x => x.User!.Id == id && x.WeekType.Index == weekType).OrderBy(x => x.DayPosition).ThenBy(x => x.SubjectPosition).ToList();
    }
}
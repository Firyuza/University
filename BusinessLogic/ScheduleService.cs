using System.Data.Entity.Migrations;

namespace BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Castle.Windsor;
    using Shared.Models.Entities;
    using Shared.Models.Interfaces;
    using Storage;

    public class ScheduleService : IScheduleService, IDisposable
    {
        private ApplicationDbContext db;

        public IWindsorContainer Container { get; set; }

        public ScheduleService()
        {
            db = new ApplicationDbContext();
        }

        public IList<Schedule> GetAll()
        {
            return db.Schedules.ToList();
        }

        public Schedule Get(long id)
        {
            return db.Schedules.Find(id);
        }

        public void Add(Schedule schedule)
        {
            SetRelativeEntities(schedule);

            db.Groups.Attach(schedule.Group);
            db.Courses.Attach(schedule.Course);

            db.Schedules.Add(schedule);
            db.SaveChanges();
        }

        public void Remove(long id)
        {
            var schedule = db.Schedules.Find(id);
            db.Schedules.Remove(schedule);
            db.SaveChanges();
        }

        public void Edit(Schedule schedule)
        {
            var editingSchedule = db.Schedules.Find(schedule.id);

            SetRelativeEntities(editingSchedule, schedule);
            editingSchedule.day = schedule.day;

            db.Schedules.AddOrUpdate(editingSchedule);

            db.SaveChanges();
        }

        public IQueryable<Schedule> GetByGroup(long id)
        {
            return db.Schedules
                .Where(x => x.Group.id == id);
        } 

        public void Dispose()
        {
            db.Dispose();
        }

        private void SetRelativeEntities(Schedule schedule)
        {
            var group = db.Groups.Find(schedule.Group.id);
            schedule.Group = group;

            var course = db.Courses.Find(schedule.Course.id);
            schedule.Course = course;
        }

        private void SetRelativeEntities(Schedule oldSchedule, Schedule newSchedule)
        {
            var group = db.Groups.Find(newSchedule.Group.id);
            oldSchedule.Group = group;

            var course = db.Courses.Find(newSchedule.Course.id);
            oldSchedule.Course = course;
        }
    }
}

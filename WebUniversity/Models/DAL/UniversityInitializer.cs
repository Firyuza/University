namespace WebUniversity.Models.DAL
{
    using System.Collections.Generic;
    using Shared.Models.Entities;

    public class UniversityInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<UniversityContext>
    {
        protected override void Seed(UniversityContext context)
        {
            var people = new List<Person>
            {
                new Person{firstname = "Carson", lastname= "Alexander", middlename = "CA"},
                new Person{firstname="Meredith",lastname="Alonso", middlename = "MA"},
                new Person{firstname="Arturo",lastname="Anand", middlename = "AA"},
                new Person{firstname="Gytis",lastname="Barzdukas", middlename = "GB"},
                new Person{firstname="Yan",lastname="Li", middlename = "YL"},
                new Person{firstname="Peggy",lastname="Justice", middlename = "PJ"},
                new Person{firstname="Laura",lastname="Norman", middlename = "LN"},
                new Person{firstname="Nino",lastname="Olivetto", middlename = "NO"}
            };

            people.ForEach(x => context.People.Add(x));
            context.SaveChanges();

            var groups = new List<Group>
            {
                new Group{name = "11-207"},
                new Group{name = "11-206"}
            };

            groups.ForEach(x => context.Groups.Add(x));
            context.SaveChanges();

            var recordBooks = new List<RecordBook>
            {
                new RecordBook{number = "1"},
                new RecordBook{number = "2"},
                new RecordBook{number = "3"},
                new RecordBook{number = "4"}
            };

            recordBooks.ForEach(x => context.RecordBooks.Add(x));
            context.SaveChanges();

            var departments = new List<Department>
            {
                new Department{name = "Applied Informatics"},
                new Department{name="Applied Mathematics"}
            };

            departments.ForEach(x => context.Departments.Add(x));
            context.SaveChanges();

            var positions = new List<Position>
            {
                new Position{name="Head of Department"},
                new Position{name="Assistant of the Department"}
            };

            positions.ForEach(x => context.Positions.Add(x));
            context.SaveChanges();

            var courses = new List<Course>
            {
                new Course{name="Mathematics"},
                new Course{name="Informatics"},
                new Course{name="English"},
                new Course{name="Art"}
            };
            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();

            var students = new List<Student>
            {
                new Student{Person = people[0], RecordBook = recordBooks[0], Group = groups[0]},
                new Student{Person = people[1], RecordBook = recordBooks[1], Group = groups[1]},
                new Student{Person = people[2], RecordBook = recordBooks[2], Group = groups[2]},
                new Student{Person = people[3], RecordBook = recordBooks[3], Group = groups[3]}
            };

            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();

            var teachers = new List<Teacher>
            {
                new Teacher{Person = people[4], Course = courses[0], Department = departments[0], Position = positions[0]},
                new Teacher{Person = people[5], Course = courses[0], Department = departments[0], Position = positions[1]},
                new Teacher{Person = people[6], Course = courses[0], Department = departments[1], Position = positions[1]},
                new Teacher{Person = people[7], Course = courses[0], Department = departments[1], Position = positions[1]}
            };

            teachers.ForEach(s => context.Teachers.Add(s));
            context.SaveChanges();

            var schedules = new List<Schedule>
            {
                new Schedule{day = "Monday", Group = groups[0], Teacher = teachers[0]},
                new Schedule{day = "Thuesday", Group = groups[0], Teacher = teachers[1]},
                new Schedule{day = "Friday", Group = groups[1], Teacher = teachers[2]},
                new Schedule{day = "Friday", Group = groups[1], Teacher = teachers[3]}
            };

            schedules.ForEach(s => context.Schedules.Add(s));
            context.SaveChanges();
        }
    }
}
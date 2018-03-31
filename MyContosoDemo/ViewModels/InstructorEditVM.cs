using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ContosoUniversity.Models;

namespace MyContosoDemo.ViewModels
{
    public class InstructorEditVM
    {
        public int CourseId { get; set; }

        public string Title { get; set; }

        public bool IsChecked { get; set; }

        public IList<Course> Courses { get; set; }

    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// Using statements required for EF DB access
using COMP2007_Lesson6.Models;
using System.Web.ModelBinding;

namespace COMP2007_Lesson6
{
    public partial class StudentDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                this.GetStudent();
            }
        }

        protected void GetStudent()
        {
            // populate the form with existing data from the database
            int StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);

            // connect to the EF DB
            using (ContosoConnection db = new ContosoConnection())
            {
                // populate a student object instance with the studentID from the URL parameter
                Student updatedStudent = (from student in db.Students
                                          where student.StudentID == StudentID
                                          select student).FirstOrDefault();

                // map the student properties to the form controls
                if (updatedStudent != null)
                {
                    LastNameTextBox.Text = updatedStudent.LastName;
                    FirstNameTextBox.Text = updatedStudent.FirstMidName;
                    EnrollmentDateTextBox.Text = updatedStudent.EnrollmentDate.ToString("yyyy-MM-dd");
                }
            }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            // Use Ef to connect to the server
            using (ContosoConnection db = new ContosoConnection())
            {
                // Use the student model to create a new student object and also save a new record
                Student newStudent = new Student();

                int StudentID = 0;

                if (Request.QueryString.Count > 0) // our URL has a StudentID in it
                {
                    // get the id from the URL
                    StudentID = Convert.ToInt32(Request.QueryString["StudentID"]);

                    // get the current student from the EF database
                    newStudent = (from student in db.Students
                                  where student.StudentID == StudentID
                                  select student).FirstOrDefault();
                }


                // add data to the new student record
                newStudent.LastName = LastNameTextBox.Text;
                newStudent.FirstMidName = FirstNameTextBox.Text;
                newStudent.EnrollmentDate = Convert.ToDateTime(EnrollmentDateTextBox.Text);

                // use LINQ to ADO.NET to add / insert new student into the database

                if (StudentID == 0)
                {
                    db.Students.Add(newStudent);
                }

                // save our changes
                db.SaveChanges();

                // redirect back to the updated students page
                Response.Redirect("~/Contoso/Students.aspx");
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            // Redirect back to Students page
            Response.Redirect("~/Contoso/Students.aspx");
        }
    }
}
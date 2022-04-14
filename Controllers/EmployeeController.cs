using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebAPI.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment webHostEnvironment;

        public EmployeeController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            this.configuration = configuration;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
            select EmployeeID, EmployeeName, EmployeeDepartment, EmployeePhotoFileName,
            convert(varchar(10),EmployeeDateOfJoining,120) as EmployeeDateOfJoining
            from dbo.Employee
            ";

            DataTable datatable = new DataTable();

            string sqlDataSource = configuration.GetConnectionString("EmployeeAppCon");

            SqlDataReader sqlDataReader;

            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {

                    sqlDataReader = sqlCommand.ExecuteReader();
                    datatable.Load(sqlDataReader);

                    sqlConnection.Close();

                }
            }

            return new JsonResult(datatable);
        }

        [HttpPost]
        public JsonResult Post(Employee employee)
        {

            string query = @"
             insert into dbo.Employee 
             (EmployeeName, EmployeeDepartment, EmployeeDateOfJoining, EmployeePhotoFileName)
             values
             (
             '" + employee.EmployeeName + @"',
             '" + employee.EmployeeDepartment + @"',
             '" + employee.EmployeeDateOfJoining + @"',
             '" + employee.EmployeePhotoFileName + @"'
             )
             ";

            DataTable datatable = new DataTable();

            string sqlDataSource = configuration.GetConnectionString("EmployeeAppCon");

            SqlDataReader sqlDataReader;

            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {

                    sqlDataReader = sqlCommand.ExecuteReader();
                    datatable.Load(sqlDataReader);

                    sqlConnection.Close();

                }
            }

            return new JsonResult("Added Successfully");

        }


        [HttpPut]
        public JsonResult Put(Employee employee)
        {

            string query = @"
            update dbo.Employee set 
            EmployeeName = '" + employee.EmployeeName + @"',
            EmployeeDepartment = '" + employee.EmployeeDepartment + @"',
            EmployeeDateOfJoining = '" + employee.EmployeeDateOfJoining + @"',
            EmployeePhotoFileName = '" + employee.EmployeePhotoFileName + @"'
            where EmployeeID = '" + employee.EmployeeID + @"'
            ";

            DataTable datatable = new DataTable();

            string sqlDataSource = configuration.GetConnectionString("EmployeeAppCon");

            SqlDataReader sqlDataReader;

            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {

                    sqlDataReader = sqlCommand.ExecuteReader();
                    datatable.Load(sqlDataReader);

                    sqlConnection.Close();

                }
            }

            return new JsonResult("Updated Successfully");

        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {

            string query = @"delete from dbo.Employee 
            where EmployeeID = '" + id + @"'
            ";

            DataTable datatable = new DataTable();

            string sqlDataSource = configuration.GetConnectionString("EmployeeAppCon");

            SqlDataReader sqlDataReader;

            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {

                    sqlDataReader = sqlCommand.ExecuteReader();
                    datatable.Load(sqlDataReader);

                    sqlConnection.Close();

                }
            }

            return new JsonResult("Deleted Successfully");

        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];

                string fileName = postedFile.FileName;
                var physicalPath = webHostEnvironment.ContentRootPath + "/Photos/" + fileName;

                using(var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(fileName);

            }

            catch (Exception ex)
            {
                return new JsonResult("anonymous.png");
            }
        }

        [Route("GetAllDepartmentNames")]
        [HttpGet]
        public JsonResult getAllDepartmentNames()
        {
            string query = @"
            select DepartmentName from dbo.Department
            ";

            DataTable datatable = new DataTable();

            string sqlDataSource = configuration.GetConnectionString("EmployeeAppCon");

            SqlDataReader sqlDataReader;

            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {

                    sqlDataReader = sqlCommand.ExecuteReader();
                    datatable.Load(sqlDataReader);

                    sqlConnection.Close();

                }
            }

            return new JsonResult(datatable);

        }

    }
}

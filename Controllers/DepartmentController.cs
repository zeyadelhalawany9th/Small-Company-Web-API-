using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public DepartmentController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select DepartmentID, DepartmentName from dbo.Department";

            DataTable datatable = new DataTable();

            string sqlDataSource = configuration.GetConnectionString("EmployeeAppCon");

            SqlDataReader sqlDataReader;

            using(SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
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
        public JsonResult Post(Department department)
        {

            string query = @"insert into dbo.Department values
             ('"+department.DepartmentName+@"')
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
        public JsonResult Put(Department department)
        {

            string query = @"update dbo.Department set 
            DepartmentName = '"+department.DepartmentName+@"'
            where DepartmentID = '"+department.DepartmentID+@"'
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

            string query = @"delete from dbo.Department 
            where DepartmentID = '" + id + @"'
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
    }
}

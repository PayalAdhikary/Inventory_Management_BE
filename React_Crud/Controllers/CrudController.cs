﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace React_Crud.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CrudController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CrudController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public class getEmployee
        {
            public Guid id { get; set; }
            public string name { get; set; }
            public int age { get; set; }
            public string  dept { get; set; }
            public decimal salary { get; set; }
            public string email { get; set; }
            public string pass { get; set; }
        }

        public class getUsers
        {
            public Guid id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string mobile { get; set; }
            public string address { get; set; }
            public string date_joined { get; set; }
            public string role { get; set; }
            public string img { get; set; }
        }

        public class postEmployee
        {
            public string name { get; set; }
            public int age { get; set; }
            public string dept { get; set; }
            public decimal salary { get; set; }
            public string email { get; set; }
            public string pass { get; set; }
        }
        public class editEmployee
        {
            public Guid id { get; set; }
            public string name { get; set; }
            public int age { get; set; }
            public string dept { get; set; }
            public decimal salary { get; set; }
        }

        public class deleteEmployee
        {
            public Guid id { get; set; }
        }

        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            try
            {

                List<getUsers> employees = new List<getUsers>();

                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    using (var command = new SqlCommand("dbo.UsersGet", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        //command.Parameters.AddWithValue("@user_id", userId);

                        // Add OUTPUT parameter to capture the stored procedure message
                        // var outputParam = new SqlParameter("@Message", SqlDbType.NVarChar, 1000);
                        // outputParam.Direction = ParameterDirection.Output;
                        // command.Parameters.Add(outputParam);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    getUsers color = new getUsers
                                    {
                                        id = (Guid)reader["id"],
                                        name = (string)reader["name"],
                                        email = (string)reader["email"],
                                        mobile = (string)reader["mobile"],
                                        address = (string)reader["address"],
                                        date_joined = (string)reader["date_joined"],
                                        role = (string)reader["role"],
                                        img = (string)reader["img"]
                                    };

                                    employees.Add(color);
                                }
                            }

                        }
                    }
                }


                if (employees.Any())
                {
                    return Ok(employees);
                }
                else
                {
                    return NotFound("No employees found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
            try
            {

                List<getEmployee> employees = new List<getEmployee>();

                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    using (var command = new SqlCommand("dbo.EmployeeGet", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        //command.Parameters.AddWithValue("@user_id", userId);

                        // Add OUTPUT parameter to capture the stored procedure message
                        // var outputParam = new SqlParameter("@Message", SqlDbType.NVarChar, 1000);
                        // outputParam.Direction = ParameterDirection.Output;
                        // command.Parameters.Add(outputParam);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    getEmployee color = new getEmployee
                                    {
                                        id = (Guid)reader["id"],
                                        name = (string)reader["name"],
                                        age = (int)reader["age"],
                                        dept = (string)reader["dept"],
                                        salary = (decimal)reader["salary"],
                                        email = (string)reader["email"],
                                        pass = (string)reader["password"]
                                    };

                                    employees.Add(color);
                                }
                            }

                        }
                    }
                }


                if (employees.Any())
                {
                    return Ok(employees);
                }
                else
                {
                    return NotFound("No employees found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(Guid id)
        {
            try
            {

                List<getEmployee> employees = new List<getEmployee>();

                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    using (var command = new SqlCommand("dbo.EmployeeGetById", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", id);

                        // Add OUTPUT parameter to capture the stored procedure message
                        // var outputParam = new SqlParameter("@Message", SqlDbType.NVarChar, 1000);
                        // outputParam.Direction = ParameterDirection.Output;
                        // command.Parameters.Add(outputParam);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    getEmployee color = new getEmployee
                                    {
                                        id = (Guid)reader["id"],
                                        name = (string)reader["name"],
                                        age = (int)reader["age"],
                                        dept = (string)reader["dept"],
                                        salary = (decimal)reader["salary"],
                                        email = (string)reader["email"],
                                        pass = (string)reader["pass"],
                                        
                                    };

                                    employees.Add(color);
                                }
                            }

                        }
                    }
                }


                if (employees.Any())
                {
                    return Ok(employees);
                }
                else
                {
                    return NotFound("No employees found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost("addEmployee")]
        public IActionResult AddEmployee(postEmployee postEmployee)
        {
            try
            {
                string message;
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    using (var command = new SqlCommand("dbo.EmployeeInsert", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        //command.Parameters.AddWithValue("@user_id", postColor.user_id);
                        command.Parameters.AddWithValue("@name", postEmployee.name);
                        command.Parameters.AddWithValue("@age", postEmployee.age);
                        command.Parameters.AddWithValue("@dept", postEmployee.dept);
                        command.Parameters.AddWithValue("@salary", postEmployee.salary);
                        command.Parameters.AddWithValue("@email", postEmployee.email);
                        command.Parameters.AddWithValue("@pass", postEmployee.pass);



                        // Add OUTPUT parameter to capture the stored procedure message
                        var outputParam = new SqlParameter("@Message", SqlDbType.NVarChar, 1000);
                        outputParam.Direction = ParameterDirection.Output;
                        command.Parameters.Add(outputParam);

                        // Execute the stored procedure
                        command.ExecuteNonQuery();

                        // Get the message from the output parameter
                        message = command.Parameters["@Message"].Value.ToString();
                    }
                }

                // Check the message returned by the stored procedure
                if (message.StartsWith("Employee inserted successfully."))
                {
                    return Ok(new { ExecuteMessage = message });
                }
                else
                {
                    return BadRequest(message); // Return error message
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPut("edit")]
        public IActionResult EditItemCategory( [FromBody] editEmployee editEmp)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    using (var command = new SqlCommand("dbo.EmployeeUpdate", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", editEmp.id);
                        command.Parameters.AddWithValue("@name", editEmp.name);
                        command.Parameters.AddWithValue("@age", editEmp.age);
                        command.Parameters.AddWithValue("@dept", editEmp.dept);
                        command.Parameters.AddWithValue("@salary", editEmp.salary);

                        // Execute the stored procedure
                        var successMessageParam = new SqlParameter("@Message", SqlDbType.NVarChar, 500)
                        {
                            Direction = ParameterDirection.Output
                        };
                        var errorMessageParam = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 500)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(successMessageParam);
                        command.Parameters.Add(errorMessageParam);

                        command.ExecuteNonQuery();

                        string successMessage = successMessageParam.Value?.ToString();
                        string errorMessage = errorMessageParam.Value?.ToString();

                        if (!string.IsNullOrEmpty(errorMessage))
                        {
                            return StatusCode(400, errorMessage); // Return bad request with error message
                        }
                        else if (!string.IsNullOrEmpty(successMessage))
                        {
                            return Ok(new { ExecuteMessage = successMessage }); // Return success message
                        }
                        else
                        {
                            return StatusCode(500, "Error: No response from the database."); // No response from database
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}"); // Internal server error
            }
        }

        [HttpPut("delete")]
        public IActionResult DeleteEmployee([FromBody] deleteEmployee delEmp)
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    using (var command = new SqlCommand("dbo.EmployeeDelete", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id", delEmp.id);

                        // Define output parameter for message
                        var messageParam = new SqlParameter("@Message", SqlDbType.NVarChar, 1000)
                        {
                            Direction = ParameterDirection.Output
                        };

                        command.Parameters.Add(messageParam);

                        command.ExecuteNonQuery();

                        string message = messageParam.Value?.ToString();

                        if (!string.IsNullOrEmpty(message) && message.StartsWith("Employee deleted successfully"))
                        {
                            return Ok(new { ExecuteMessage = message }); // Return success message
                        }
                        else
                        {
                            return StatusCode(400, message); // Return bad request with error message
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}"); // Internal server error
            }
        }


    }
}

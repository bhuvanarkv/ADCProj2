using JWT.Algorithms;
using JWT.Builder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ImplementingREST.Controllers
{
    public class StudentController : ApiController
    {
        private const string secret= "test123";
        // GET api/values
        public List<Student> Get()
        {
            string connectionString =
          "Server=tcp:adcproj.database.windows.net,1433;" +
          "Initial Catalog=adc;Persist Security Info=False;" +
          "User ID=adcproj;Password=Test1234;MultipleActiveResultSets=False;" +
          "Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


            string queryString = string.Format("select *  from dbo.student  ");

            using (SqlConnection connection =
         new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                SqlCommand command = new SqlCommand(queryString, connection);

                // await connection.OpenAsync();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    List<Student> result = new List<Student>();
                    while (reader.Read())
                    {
                        Student stud = new Student();
                        stud.student_id = reader["student_id"].ToString();
                        stud.first_name = reader["first_name"].ToString();
                        stud.last_name = reader["last_name"].ToString();
                        stud.email_id = reader["email_id"].ToString();
                        stud.phone_no = reader["phone_no"].ToString();
                        stud.address = reader["address"].ToString();
                        stud.city = reader["city"].ToString();
                        stud.state = reader["state"].ToString();
                        stud.country = reader["country"].ToString();
                        stud.emergency_contact = reader["emergency_contact"].ToString();
                        stud.relationship = reader["relationship"].ToString();
                        result.Add(stud);

                    }
                    return result;
                }
                else
                {
                    return null;
                }

            }
        }

        // GET api/values/5
        public List<string> Get(int id)
        {
            string student_id = id.ToString();
            string connectionString =
       "Server=tcp:adcproj.database.windows.net,1433;" +
       "Initial Catalog=adc;Persist Security Info=False;" +
       "User ID=adcproj;Password=Test1234;MultipleActiveResultSets=False;" +
       "Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


            string queryString = string.Format("select *  from dbo.student Where student_id={0} ", student_id);

            using (SqlConnection connection =
         new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                SqlCommand command = new SqlCommand(queryString, connection);

                // await connection.OpenAsync();
                SqlDataReader reader = command.ExecuteReader();

                List<string> result = new List<string>();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        result.Add("First Name" + reader["first_name"].ToString());
                        result.Add("Last Name" + reader["last_name"].ToString());
                        result.Add("Email ID" + reader["email_id"].ToString());
                        result.Add("Phone Number" + reader["phone_no"].ToString());
                        result.Add("Address" + reader["address"].ToString());
                        result.Add("City" + reader["city"].ToString());
                        result.Add("State" + reader["state"].ToString());
                        result.Add("Country" + reader["country"].ToString());
                        result.Add("Emergency Contact" + reader["emergency_contact"].ToString());
                        result.Add("Relationship" + reader["relationship"].ToString());

                    }
                }
                else
                {
                    result.Add("Error Performing this action");                    
                }
                return result;

            }

        }

        // POST api/values/Login
        [Route("api/Student/Login")]
        public string Login([FromBody] UserModel model)
        {
            string student_id = model.stud_id.Trim();
            string password = model.password.ToString();
            string connectionString =
       "Server=tcp:adcproj.database.windows.net,1433;" +
       "Initial Catalog=adc;Persist Security Info=False;" +
       "User ID=adcproj;Password=Test1234;MultipleActiveResultSets=False;" +
       "Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


            string queryString = string.Format("select student_id,password from dbo.student Where student_id={0} AND password ='{1}'", student_id, password);

            using (SqlConnection connection =
         new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                SqlCommand command = new SqlCommand(queryString, connection);

                // await connection.OpenAsync();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine("true");
                    return  new JwtBuilder()
      .WithAlgorithm(new HMACSHA256Algorithm())
      .WithSecret(secret)
      .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
      .Build();

                }
                else
                {
                    return "";
                }


            }
        }
        // PUT api/values/5
       // [Route("User/CompleteProfile")]
        public bool Put(int id, [FromBody] Person personmodel)
        {
            string phone_no = personmodel.phone_no;
            string address = personmodel.address; ;
            string city = personmodel.city; ;
            string country = personmodel.country;
            string state = personmodel.state;
            string emergency_contact = personmodel.emergency_contact;
            string relationship = personmodel.relationship;
            string email_id = personmodel.email;
            string student_id = id.ToString(); ;
            string connectionString = "Server=tcp:adcproj.database.windows.net,1433;" +
"Initial Catalog=adc;Persist Security Info=False;" +
"User ID=adcproj;Password=Test1234;MultipleActiveResultSets=False;" +
"Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            // Provide the query string with a parameter placeholder.

            string queryString = string.Format("UPDATE dbo.student SET phone_no ={0}, address = '{1}', city = '{2}', state = '{3}', country = '{4}', emergency_contact ={5}, email_id ='{6}', relationship ='{7}' WHERE student_id = {8}", phone_no, address, city, state, country, emergency_contact, email_id, relationship, student_id);
            try
            {
                using (SqlConnection cnn = new SqlConnection(connectionString))

                {
                    using (SqlCommand cmd = new SqlCommand(queryString, cnn))
                    {
                        cnn.Open();

                        int rows = cmd.ExecuteNonQuery();
                        cnn.Close();
                        if (rows >= 1)
                            return true;
                        else
                            return false;

                    }

                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }


        //POST/api/values/SignUp
        //[Route("User")]
        public bool Post([FromBody] SignUpModel model)
        {
            string student_id = model.stud_id.Trim();
            string first_name = model.first_name.ToString();
            string last_name = model.last_name.ToString();
            string password = model.password.ToString();
            string connectionString =
     "Server=tcp:adcproj.database.windows.net,1433;" +
     "Initial Catalog=adc;Persist Security Info=False;" +
     "User ID=adcproj;Password=Test1234;MultipleActiveResultSets=False;" +
     "Encrypt=True;TrustServerCertificate=False;Connection Timeout=60;";

            // Provide the query string with a parameter placeholder.
            //string queryString = "INSERT INTO dbo.student (first_name,last_name, student_id,password) VALUES (@val1, @val2, @val3,@val4)";

            try
            {
                using (SqlConnection cnn = new SqlConnection(connectionString))

                {
                    using (SqlCommand cmd = new SqlCommand("studentSignUp", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@student_id", student_id));
                        cmd.Parameters.Add(new SqlParameter("@first_name", first_name));
                        cmd.Parameters.Add(new SqlParameter("@last_name", last_name));
                        cmd.Parameters.Add(new SqlParameter("@email_id", DBNull.Value));
                        cmd.Parameters.Add(new SqlParameter("@phone_no", DBNull.Value));
                        cmd.Parameters.Add(new SqlParameter("@password", password));
                        cmd.Parameters.Add(new SqlParameter("@address", DBNull.Value));
                        cmd.Parameters.Add(new SqlParameter("@city", DBNull.Value));
                        cmd.Parameters.Add(new SqlParameter("@state", DBNull.Value));
                        cmd.Parameters.Add(new SqlParameter("@country", DBNull.Value));
                        cmd.Parameters.Add(new SqlParameter("@emergency_contact", DBNull.Value));
                        cmd.Parameters.Add(new SqlParameter("@relationship", DBNull.Value));

                        cnn.Open();
                        SqlParameter returnParameter = cmd.Parameters.Add("Return Value", SqlDbType.Int);
                        returnParameter.Direction = ParameterDirection.ReturnValue;

                        cmd.ExecuteNonQuery();
                        int id = (int)returnParameter.Value;

                        cnn.Close();

                        if (id >= 0)
                            return true;
                        else
                            return false;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }


        // DELETE api/values/5
        public string Delete(int id)
        {
            string student_id = id.ToString();
            string connectionString =
       "Server=tcp:adcproj.database.windows.net,1433;" +
       "Initial Catalog=adc;Persist Security Info=False;" +
       "User ID=adcproj;Password=Test1234;MultipleActiveResultSets=False;" +
       "Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";


            string queryString = string.Format("Delete  from dbo.student Where student_id={0} ", student_id);

            using (SqlConnection connection =
         new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                SqlCommand command = new SqlCommand(queryString, connection);

                // await connection.OpenAsync();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {

                    return string.Format("Cannot Delete Student Record with Studemt ID " + student_id);
                }
                else
                {
                    return string.Format("Student Record ( Studemt ID = {0})  Successfully deleted", student_id);
                }

            }
        }
    }
}

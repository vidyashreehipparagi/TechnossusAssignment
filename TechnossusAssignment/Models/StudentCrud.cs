using System.Data.SqlClient;

namespace TechnossusAssignment.Models
{
    public class StudentCrud
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        IConfiguration configuration;
        public StudentCrud(IConfiguration configuration)
        {
            this.configuration = configuration;
            con = new SqlConnection(this.configuration.GetConnectionString("defaultConnection"));
        }
        
        public IEnumerable<Student> GetAllStudents(string searchString, DateTime? registrationDate)
        {
            List<Student> list = new List<Student>();

            string query = "SELECT * FROM Student WHERE (home_address LIKE @searchString OR student_name LIKE @searchString) AND isActive = 1";

            // Check if registrationDate is provided and not null
            if (registrationDate.HasValue)
            {
                query += " AND CONVERT(date, registration_date) = @registrationDate";
            }

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@searchString", "%" + searchString + "%");

            // Add the registrationDate parameter only if it's provided
            if (registrationDate.HasValue)
            {
                cmd.Parameters.AddWithValue("@registrationDate", registrationDate.Value.Date);
            }

            con.Open();

            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Student student = new Student();
                    student.Student_Id = Convert.ToInt32(dr["student_id"]);
                    student.StudentName = dr["student_name"].ToString();
                    student.MotherName = dr["mothers_name"].ToString();
                    student.FatherName = dr["fathers_name"].ToString();
                    student.Age = Convert.ToInt32(dr["age"]);
                    student.HomeAddress = dr["home_address"].ToString();
                    student.RegistrationDate = Convert.ToDateTime(dr["registration_date"]);
                    student.IsActive = Convert.ToInt32(dr["isActive"]);
                    list.Add(student);
                }
            }

            con.Close();
            return list;
        }

        public int AddStudent(Student student)
        {
            student.IsActive = 1;

            int result = 0;
            string qry = "insert into student values(@student_name,@fathers_name,@mothers_name,@age,@home_address,@registration_date,@isActive)";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("student_name", student.StudentName);
            cmd.Parameters.AddWithValue("fathers_name", student.FatherName);
            cmd.Parameters.AddWithValue("mothers_name", student.MotherName);
            cmd.Parameters.AddWithValue("age", student.Age);
            cmd.Parameters.AddWithValue("home_address", student.HomeAddress);
            cmd.Parameters.AddWithValue("registration_date", student.RegistrationDate);
            cmd.Parameters.AddWithValue("@isActive", student.IsActive);

            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;

        }
        public int UpdateStudent(Student student)
        {
            student.IsActive = 1;

            int result = 0;
            string qry = "update Student set student_name=@student_name,fathers_name=@fathers_name,mothers_name=@mothers_name,age=@age,home_address=@home_address,registration_date=@registration_date,isActive=@isActive where student_id=@student_id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("student_id", student.Student_Id);
            cmd.Parameters.AddWithValue("student_name", student.StudentName);
            cmd.Parameters.AddWithValue("fathers_name", student.FatherName);
            cmd.Parameters.AddWithValue("mothers_name", student.MotherName);
            cmd.Parameters.AddWithValue("age", student.Age);
            cmd.Parameters.AddWithValue("home_address", student.HomeAddress);
            cmd.Parameters.AddWithValue("registration_date", student.RegistrationDate);
            cmd.Parameters.AddWithValue("@isActive", student.IsActive);

            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;

        }
        public Student GetStudentById(int id)
        {
            Student s = new Student();
            string qry = "select * from Student where student_id=@student_id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@student_id", id);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    s.Student_Id = Convert.ToInt32(dr["student_id"]);
                    s.StudentName = dr["student_name"].ToString();
                    s.FatherName = dr["fathers_name"].ToString();
                    s.MotherName = dr["mothers_name"].ToString();
                    s.Age = Convert.ToInt32(dr["age"]);
                    s.HomeAddress = dr["home_address"].ToString();
                    s.RegistrationDate = Convert.ToDateTime(dr["registration_date"]);
                }
            }
            con.Close();
            return s;
        }
        public int DeleteStudent(int id)
        {
            int result = 0;
            string qry = "update Student set isActive=0 where student_id=@student_id";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@student_id", id);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

            }
}


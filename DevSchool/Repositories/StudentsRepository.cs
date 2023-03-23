using Dapper;
using DevSchool.Entities;
using DevSchool.Models;
using System.Data.SqlClient;

namespace DevSchool.Repositories
{
    public class StudentsRepository : IStudentsRepository
    {
        private readonly string _connectionString;

        public StudentsRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<string> Add(StudentsInputModel model)
        {
            try
            {
                String Id = "";
                Students students = new Students();
                students.Id = Guid.NewGuid().ToString();
                students.Nome = model.Nome;
                students.Telefone = model.Telefone;
                students.Idade = model.Idade;
                students.IsActive = true;
                var parameters = new
                {
                    students.Id,
                    students.Nome,
                    students.Telefone,
                    students.Idade,
                    students.IsActive
                };
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    var query = "INSERT INTO STUDENTS(Id, Nome, Telefone, Idade, IsActive) OUTPUT INSERTED.Id values (@Id, @Nome, @Telefone, @Idade, @IsActive )";
                    Id = await sqlConnection.ExecuteScalarAsync<String>(query, parameters);
                }
                return Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public async Task Delete(string Id)
        {
            try
            {

                var parameters = new
                {
                   Id
                };
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    var query = "UPDATE STUDENTS SET IsActive = 0 where Id = @Id";
                    await sqlConnection.ExecuteAsync(query, parameters);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Students>> GetAll()
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var query = "Select * from Students where IsActive = 1";
                var students = await sqlConnection.QueryAsync<Students>(query);
                return students;
            }
        }

        public async Task<Students> GetById(string Id)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                var parameTers = new
                {
                    Id
                };

                var query = "Select * from Students where IsActive = 1 and Id = @Id";
                var students = await sqlConnection.QueryFirstOrDefaultAsync<Students>(query, parameTers);

                return students;
            }
        }

        public async Task Update(string Id, StudentsInputModel model)
        {
            try
            {
            
                var parameters = new
                {
                    Id,
                    model.Nome,
                    model.Telefone,
                    model.Idade
                };
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    var query = "UPDATE STUDENTS SET Nome = @Nome, Telefone = @Telefone, Idade = @Idade where Id = @Id";
                    await sqlConnection.ExecuteAsync(query, parameters);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

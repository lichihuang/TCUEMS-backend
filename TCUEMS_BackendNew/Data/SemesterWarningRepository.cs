using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using TCUEMS_BackendNew.Models;

namespace TCUEMS_BackendNew.Data
{
    public class SemesterWarningRepository : ISemesterWarningRepository
    {
        private readonly string _connectionString;

        public SemesterWarningRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<SemesterWarning>> GetAllSemesterWarnings()
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            return await dbConnection.QueryAsync<SemesterWarning>("SELECT * FROM SW");
        }

        public async Task<IEnumerable<SemesterWarning>> GetWarningsByStudentId(int studentId)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            return await dbConnection.QueryAsync<SemesterWarning>("SELECT * FROM SemesterWarning WHERE w_std_no = @StudentId", new { StudentId = studentId });
        }
    }
}

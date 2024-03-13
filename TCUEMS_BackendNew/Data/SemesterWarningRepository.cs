using System;
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
            return await dbConnection.QueryAsync<SemesterWarning>("SELECT * FROM SW WHERE w_Std_No = @studentId", new { studentId });
        }

        public async Task<IEnumerable<SemesterWarning>> GetSemesterWarningsByCriteria(SemesterWarning criteria)
        {
            using SqlConnection dbConnection = new SqlConnection(_connectionString);
            await dbConnection.OpenAsync();

            string sqlQuery = "SELECT * FROM SW WHERE 1 = 1";

            if (!string.IsNullOrEmpty(criteria.dept_name_s))
                sqlQuery += " AND dept_Name_S = @DeptNameS";

            if (!string.IsNullOrEmpty(criteria.w_smtr))
                sqlQuery += " AND w_Smtr = @WSmtr";

            if (!string.IsNullOrEmpty(criteria.w_std_no))
                sqlQuery += " AND w_Std_No = @WStdNo";

            return await dbConnection.QueryAsync<SemesterWarning>(sqlQuery, new
            {
                DeptNameS = criteria.dept_name_s,
                WSmtr = criteria.w_smtr,
                WStdNo = criteria.w_std_no
            });
        }

    }
}

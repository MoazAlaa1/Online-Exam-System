using Microsoft.EntityFrameworkCore;
using OnlineExamSystem.Classes;
using OnlineExamSystem.Models;
using System.Data;

namespace OnlineExamSystem.BL
{
    public interface IDashBoard
    {
        public Task<DashboardResult> GetDashboardDataAsync();
    }
    public class ClsDashBoard : IDashBoard
    {
        ExamSystemContext context;
        public ClsDashBoard(ExamSystemContext ctx)
        {
            context = ctx;
        }
        public List<VwSubmission> GetAll()
        {
            try
            {
                return context.VwSubmissions.Where(a => a.CurrentState == 1).ToList();
            }
            catch (Exception ex)
            {
                return new List<VwSubmission>();
            }
        }

        public async Task<DashboardResult> GetDashboardDataAsync()
        {
            var result = new DashboardResult();

            // Execute the stored procedure
            var command = context.Database.GetDbConnection().CreateCommand();
            command.CommandText = "SpDashboardExamSystem";
            command.CommandType = CommandType.StoredProcedure;

            if (command.Connection.State != ConnectionState.Open)
                await command.Connection.OpenAsync();

            await using var reader = await command.ExecuteReaderAsync();

            // Read first result set (statistics)
            if (await reader.ReadAsync())
            {
                result.Kpis = new KPIs
                {
                    Exams = reader.GetInt32(reader.GetOrdinal("Exams")),
                    Submissions = reader.GetInt32(reader.GetOrdinal("Submissions")),
                    Users = reader.GetInt32(reader.GetOrdinal("Users"))
                };
            }

            
            await reader.NextResultAsync();
            int counter = 0;
            result.Submissions = new List<VwSubmission>();
            while (await reader.ReadAsync() && counter < 5)
            {
                result.Submissions.Add(new VwSubmission
                {
                    SubmissionId = reader.GetInt32(reader.GetOrdinal("SubmissionId")),
                    ExamId = reader.GetInt32(reader.GetOrdinal("ExamId")),
                    UserId = reader.GetString(reader.GetOrdinal("UserId")),
                    Score = reader.GetFloat(reader.GetOrdinal("Score")),
                    Status = reader.GetString(reader.GetOrdinal("Status")),
                    SubmissionDate = reader.GetDateTime(reader.GetOrdinal("SubmissionDate")),
                    ExamTitle = reader.GetString(reader.GetOrdinal("ExamTitle")),
                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                    CurrentState = 1
                    
                });
                counter++;
            }

            return result;
        }
    }
}

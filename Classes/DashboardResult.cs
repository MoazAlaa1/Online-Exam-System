using OnlineExamSystem.Models;

namespace OnlineExamSystem.Classes
{
    public class DashboardResult
    {
        public DashboardResult()
        {
            Kpis = new KPIs();
            Submissions = new List<VwSubmission>();
        }
        public KPIs Kpis { get; set; }
        public List<VwSubmission> Submissions { get; set; }
    }
    public class KPIs
    {
        public int Exams { get; set; }
        public int Submissions { get; set; }
        public int Users { get; set; }
    }
}

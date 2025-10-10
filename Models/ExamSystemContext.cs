using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using OnlineExamSystem.BL;
namespace OnlineExamSystem.Models
{
    public partial class ExamSystemContext : IdentityDbContext<ApplicationUser>
    {
        public ExamSystemContext()
        {
            
        }

        public ExamSystemContext(DbContextOptions<ExamSystemContext> options)
        : base(options)
        {
        }

        public virtual DbSet<Exam> TbExams { get; set; }
        public virtual DbSet<Question> TbQuestions { get; set; }
        public virtual DbSet<Choice> TbChoices { get; set; }
        public virtual DbSet<submission> TbSubmissions { get; set; }
        public virtual DbSet<VwQuestionWithChoices> Vw_Question_Choices { get; set; }
        public virtual DbSet<VwSubmission> VwSubmissions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasOne(a => a.TbExam).WithMany(s => s.TbQuestion).HasForeignKey(a => a.ExamId);
            });
            modelBuilder.Entity<Choice>(entity =>
            {
                entity.HasOne(a => a.TbQuestion).WithMany(s => s.TbChoice).HasForeignKey(a => a.QuestionId);
            });
            modelBuilder.Entity<submission>(entity =>
            {
                entity.HasOne(a => a.TbUser).WithMany(b => b.TbSubmissin).HasForeignKey(a => a.UserId);
                entity.HasOne(a => a.TbExam).WithMany(b => b.TbSubmissin).HasForeignKey(a => a.ExamId);
            });
            modelBuilder.Entity<VwQuestionWithChoices>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("Vw_Question_Choices");
            });
            modelBuilder.Entity<VwSubmission>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("VwSubmissions");
            });
        }
    }
}

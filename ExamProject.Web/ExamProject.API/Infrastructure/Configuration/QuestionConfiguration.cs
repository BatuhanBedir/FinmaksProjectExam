using ExamProject.API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ExamProject.API.Infrastructure.Configuration;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(x => x.Choices).WithOne(x => x.Question).HasForeignKey(x => x.QuestionId);
        builder.Property(x => x.QuestionContent).IsRequired();
    }
}

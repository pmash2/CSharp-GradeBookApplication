using GradeBook.Enums;
using System;
using System.Linq;

namespace GradeBook.GradeBooks
{
  public class RankedGradeBook : BaseGradeBook
  {
    public RankedGradeBook(string name) : base(name)
    {
      Type = GradeBookType.Ranked;
    }

    public override void CalculateStatistics()
    {
      if (Students.Count < 5)
      {
        System.Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
        return;
      }
      base.CalculateStatistics();
    }

    public override char GetLetterGrade(double averageGrade)
    {
      if (Students.Count < 5)
      {
        throw new InvalidOperationException("Ranked grading requires at least 5 students");
      }

      // Number of students that should receive each letter grade
      var threshold = (int)Math.Ceiling(Students.Count * 0.2);

      //                Order students by       their avg grade   and take their avg grade val  to a list
      var grades = Students.OrderByDescending(e => e.AverageGrade).Select(e => e.AverageGrade).ToList();
      
      if (grades[threshold - 1] <= averageGrade)
        return 'A';
      else if (grades[(threshold * 2) - 1] <= averageGrade)
        return 'B';
      else if (grades[(threshold * 3) - 1] <= averageGrade)
        return 'C';
      else if (grades[(threshold * 4) - 1] <= averageGrade)
        return 'D';
      else
        return 'F';
    }
  }
}
using Education.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Education.Cases.Patterns.Behavioral.Interpreter
{
    public class InterpreterCase : ICase
    {
        public async Task RunAsync() {
            var evaluators = new List<IExamEvaluator<PersonExamsResults, PersonExamsInterpretation>>();
            evaluators.Add(new MathExamEvaluator());
            evaluators.Add(new EnglishExamEvaluator());
            var examResults = new PersonExamsResults {
                PersonId = 1,
                MathGrade = 6,
                EnglishGrade = 4
            };
            var examInterpretationInTermsOfDescriptions = new ExamInterpretationHost<PersonExamsResults, PersonExamsInterpretation>();
            examInterpretationInTermsOfDescriptions.ExamsResults = examResults;

            evaluators.ForEach(item => item.Evaluate(examInterpretationInTermsOfDescriptions));
            Console.WriteLine(examInterpretationInTermsOfDescriptions.ExamsInterpretation.EnglishRecall);
            Console.WriteLine(examInterpretationInTermsOfDescriptions.ExamsInterpretation.MathRecall);
        }
    }

    public class PersonExamsResults
    {
        public int PersonId { get; set; }
        public int MathGrade { get; set; }
        public int EnglishGrade { get; set; }
    }

    public class PersonExamsInterpretation
    {
        public string MathRecall { get; set; }
        public string EnglishRecall { get; set; }
    }

    public class ExamInterpretationHost<T, U>
        where T : PersonExamsResults
        where U : PersonExamsInterpretation, new()
    {
        private T examsResults;
        private U examsInterpretation;

        public T ExamsResults {
            get => examsResults;
            set {
                this.examsResults = value;
                this.ExamsInterpretation = null;
            }
        }
        public U ExamsInterpretation {
            get {
                return examsInterpretation ?? (examsInterpretation = Activator.CreateInstance<U>());
            }
            set {
                examsInterpretation = value;
            }
        }
    }

    public interface IExamEvaluator<T, U>
            where T : PersonExamsResults
            where U : PersonExamsInterpretation, new()
    {
        void Evaluate(ExamInterpretationHost<T, U> examInterpretationHost);
    }

    public class MathExamEvaluator : IExamEvaluator<PersonExamsResults, PersonExamsInterpretation>
    {
        public void Evaluate(ExamInterpretationHost<PersonExamsResults, PersonExamsInterpretation> examInterpretationHost) {
            examInterpretationHost.ExamsInterpretation.MathRecall = examInterpretationHost.ExamsResults?.MathGrade switch {
                < 4 => "Did not succeed",
                10 or 9 => "Very nice",
                < 9 => "Acceptable",
                _ => "Uknown mark"
            };
        }
    }

    public class EnglishExamEvaluator : IExamEvaluator<PersonExamsResults, PersonExamsInterpretation>
    {
        public void Evaluate(ExamInterpretationHost<PersonExamsResults, PersonExamsInterpretation> examInterpretationHost) {
            examInterpretationHost.ExamsInterpretation.EnglishRecall = examInterpretationHost.ExamsResults?.EnglishGrade switch {
                < 6 => "Did not succeed",
                10 or 9 => "Very nice",
                < 9 => "Acceptable",
                _ => "Uknown mark"
            };
        }
    }
}

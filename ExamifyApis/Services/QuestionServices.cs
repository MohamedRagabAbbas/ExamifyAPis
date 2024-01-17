using ExamifyApis.DB;
using ExamifyApis.Models;
using ExamifyApis.ModelServices;
using ExamifyApis.Response;
using Microsoft.EntityFrameworkCore;

namespace ExamifyApis.Services
{
    public class QuestionServices
    {
        private readonly DBContextClass _context;
        public QuestionServices(DBContextClass context)
        {
            _context = context;
        }
        public async Task<ResponseClass<Question>> AddQuestion(QuestionInfo questionInfo)
        {
            var question = new Question
            {
                QuestionNumber = questionInfo.QuestionNumber,
                QuestionText = questionInfo.QuestionText,
                Option1 = questionInfo.Option1,
                Option2 = questionInfo.Option2,
                Option3 = questionInfo.Option3,
                Option4 = questionInfo.Option4,
                CorrectAnswer = questionInfo.CorrectAnswer,
                Weight = questionInfo.Weight,
                ExamId = questionInfo.ExamId
            };
            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();
            var response = new ResponseClass<Question>()
            {
                Data = question,
                Message = "Question Added Successfully",
                Status = true
            };
            return response;
        }
        public async Task<ResponseClass<Question>> UpdateQuestion(int id, QuestionInfo questionInfo)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return new ResponseClass<Question>()
                {
                    Data = null,
                    Message = "Question Not Found",
                    Status = false
                };
            }
            question.QuestionNumber = questionInfo.QuestionNumber;
            question.QuestionText = questionInfo.QuestionText;
            question.Option1 = questionInfo.Option1;
            question.Option2 = questionInfo.Option2;
            question.Option3 = questionInfo.Option3;
            question.Option4 = questionInfo.Option4;
            question.CorrectAnswer = questionInfo.CorrectAnswer;
            question.Weight = questionInfo.Weight;
            question.ExamId = questionInfo.ExamId;
            await _context.SaveChangesAsync();
            var response = new ResponseClass<Question>()
            {
                Data = question,
                Message = "Question Updated Successfully",
                Status = true
            };
            return response;
        }
        public async Task<ResponseClass<Question>> DeleteQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return new ResponseClass<Question>()
                {
                    Data = null,
                    Message = "Question Not Found",
                    Status = false
                };
            }
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            var response = new ResponseClass<Question>()
            {
                Data = question,
                Message = "Question Deleted Successfully",
                Status = true
            };
            return response;
        }

        public async Task<ResponseClass<List<Question>>> GetQuestionsByExamId(int id)
        {
            var questions = await _context.Questions.Where(q => q.ExamId == id).ToListAsync();
            if (questions == null)
            {
                return new ResponseClass<List<Question>>()
                {
                    Data = null,
                    Message = "Questions Not Found",
                    Status = false
                };
            }
            var response = new ResponseClass<List<Question>>()
            {
                Data = questions,
                Message = "Questions Found Successfully",
                Status = true
            };
            return response;
        }

        public async Task<ResponseClass<Question>>GgetQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return new ResponseClass<Question>()
                {
                    Data = null,
                    Message = "Question Not Found",
                    Status = false
                };
            }
            var response = new ResponseClass<Question>()
            {
                Data = question,
                Message = "Question Found Successfully",
                Status = true
            };
            return response;
        }
        
    }
}

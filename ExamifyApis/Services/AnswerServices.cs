using ExamifyApis.DB;
using ExamifyApis.Models;
using ExamifyApis.ModelServices;
using ExamifyApis.Response;
using Microsoft.EntityFrameworkCore;

namespace ExamifyApis.Services
{
    public class AnswerServices
    {
        private readonly DBContextClass _dbContext;
        public AnswerServices(DBContextClass dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ResponseClass<Answer>> AddAnswer(AnswerInfo answerInfo)
        {
            var response = new ResponseClass<Answer>();
            try
            {
                var answer = new Answer
                {
                    StudentId = answerInfo.StudentId,
                    QuestionId = answerInfo.QuestionId,
                    AnswerOption = answerInfo.AnswerOption,
                    IsCorrect = answerInfo.IsCorrect,
                    Grade = answerInfo.Grade
                };
                await _dbContext.Answers.AddAsync(answer);
                await _dbContext.SaveChangesAsync();
                response.Data = answer;
                response.Message = "Answer Added Successfully";
                response.Status = true;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Status = false;
            }
            return response;
        }
        public async Task<ResponseClass<List<Answer>>> GetAnswers()
        {
            var response = new ResponseClass<List<Answer>>();
            try
            {
                var answers = await _dbContext.Answers.ToListAsync();
                response.Data = answers;
                response.Message = "Answers Fetched Successfully";
                response.Status = true;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Status = false;
            }
            return response;
        }
        public async Task<ResponseClass<Answer>> GetAnswerById(int id)
        {
            var response = new ResponseClass<Answer>();
            try
            {
                var answer = await _dbContext.Answers.FindAsync(id);
                response.Data = answer;
                response.Message = "Answer Fetched Successfully";
                response.Status = true;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Status = false;
            }
            return response;
        }
        public async Task<ResponseClass<Answer>> UpdateAnswer(int id, AnswerInfo answerInfo)
        {
            var response = new ResponseClass<Answer>();
            try
            {
                var answer = await _dbContext.Answers.FindAsync(id);
                answer.StudentId = answerInfo.StudentId;
                answer.QuestionId = answerInfo.QuestionId;
                answer.AnswerOption = answerInfo.AnswerOption;
                answer.IsCorrect = answerInfo.IsCorrect;
                answer.Grade = answerInfo.Grade;
                await _dbContext.SaveChangesAsync();
                response.Data = answer;
                response.Message = "Answer Updated Successfully";
                response.Status = true;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Status = false;
            }
            return response;
        }
        public async Task<ResponseClass<Answer>> DeleteAnswer(int id)
        {
            var response = new ResponseClass<Answer>();
            try
            {
                var answer = await _dbContext.Answers.FindAsync(id);
                _dbContext.Answers.Remove(answer);
                await _dbContext.SaveChangesAsync();
                response.Data = answer;
                response.Message = "Answer Deleted Successfully";
                response.Status = true;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Status = false;
            }
            return response;
        }

        // get answers by student id
        public async Task<ResponseClass<List<Answer>>> GetAnswersByStudentId(int id)
        {
            var response = new ResponseClass<List<Answer>>();
            try
            {
                var answers = await _dbContext.Answers.Where(a => a.StudentId == id).ToListAsync();
                response.Data = answers;
                response.Message = "Answers Fetched Successfully";
                response.Status = true;
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                response.Status = false;
            }
            return response;
        }

    }
}

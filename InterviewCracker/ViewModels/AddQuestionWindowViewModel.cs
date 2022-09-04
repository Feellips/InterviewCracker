using InterviewCracker.Models;
using InterviewCracker.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Tmds.DBus;

namespace InterviewCracker.ViewModels
{
    public class AddQuestionWindowViewModel : ViewModelBase
    {
        private readonly string _questionsPath = "./questions.json";
        public Question NewQuestion { get; set; } = new();

        public void AnswerQuestion(int answerIndex)
        {
            NewQuestion.AnswerIndex = answerIndex;

            LinkedList<Question> questions;

            if (File.Exists(_questionsPath))
            {
                questions = JsonSerializer.Deserialize(File.ReadAllText(_questionsPath), typeof(LinkedList<Question>)) as LinkedList<Question>;

            }
            else
            {
                questions = new();
                var stream = File.Create(_questionsPath);
                stream.Dispose();
            }


            questions.AddLast(NewQuestion);

            var questionsJson = JsonSerializer.Serialize(questions);
            File.WriteAllText(_questionsPath, questionsJson);
        }
    }
}

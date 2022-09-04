using Avalonia.Media;
using Avalonia.OpenGL.Angle;
using InterviewCracker.Models;
using InterviewCracker.Views;
using JetBrains.Annotations;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows.Input;

namespace InterviewCracker.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly string _questionsPath = "./questions.json";
        private readonly LinkedList<Question> _questions = new();
        private readonly Random _random = new();
        private bool _correct;

        public bool Correct
        {
            get => _correct;
            set
            {
                this.RaiseAndSetIfChanged(ref _correct, value);
                this.RaisePropertyChanged(nameof(CorrectColor));
            }
        }

        public string CorrectColor => Correct ? "Green" : "Red";
        public LinkedListNode<Question> CurrentNode { get; set; }
        public Question CurrentQuestion { get; set; } = new();

        public MainWindowViewModel()
        {
            if (File.Exists(_questionsPath))
                _questions = JsonSerializer.Deserialize<LinkedList<Question>>(File.ReadAllText(_questionsPath));

            var array = _questions.ToArray();
            Shuffle(_random, array);

            _questions = new LinkedList<Question>(array);
            CurrentNode = _questions.First;
            CurrentQuestion.Text = CurrentNode.Value.Text;
            CurrentQuestion.AnswerIndex = CurrentNode.Value.AnswerIndex;
        }

        public void AnswerQuestion(int answerIndex)
        {
            Correct = answerIndex == CurrentQuestion.AnswerIndex;
            CurrentNode = CurrentNode.Next ?? _questions.First;

            CurrentQuestion.Text = CurrentNode.Value.Text;
            CurrentQuestion.AnswerIndex = CurrentNode.Value.AnswerIndex;
        }

        public void AddQuestion()
        {
            var addQuestionWindow = new AddQuestionWindow();
            addQuestionWindow.DataContext = new AddQuestionWindowViewModel();
            addQuestionWindow.Show();
        }

        public static void Shuffle(Random rng, Question[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                var temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }
    }
}

using InterviewCracker.ViewModels;
using ReactiveUI;
using System;

namespace InterviewCracker.Models
{
    [Serializable]
    public class Question : ReactiveObject
    {
        private string _text;
        private int _answerIndex;

        public string Text
        {
            get => _text;
            set => this.RaiseAndSetIfChanged(ref _text, value);
        }
        public int AnswerIndex 
        {
            get => _answerIndex;
            set => this.RaiseAndSetIfChanged(ref _answerIndex, value);
        }
    }
}

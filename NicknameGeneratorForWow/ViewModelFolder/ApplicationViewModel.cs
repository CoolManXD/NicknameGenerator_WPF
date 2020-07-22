using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace NicknameGeneratorForWow
{
    public enum Races : byte
    {
        Orc = 0,
        Elf
    }
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        
        private GenerateData currentData;
        private Races race;
        private string outputNickname = string.Empty;
        private RelayCommand generateCommand;
        public GenerateData CurrentData
        {
            get { return currentData; }
            set
            {
                currentData = value;
                OnPropertyChanged("CurrentData");
            }
        }     
        public Races Race
        {
            get { return race; }
            set
            {
                race = value;
                OnPropertyChanged("Race");
            }
        }
        public string OutputNickname
        {
            get { return outputNickname; }
            set
            {
                outputNickname = value;
                OnPropertyChanged("OutputNickname");
            }
        }      
        public RelayCommand GenerateCommand
        {
            get
            {
                return generateCommand;
            }
        }        
        public ApplicationViewModel()
        {
            currentData = new GenerateData();

            // Action<object> execute = obj => OutputNickname = new Generator().generateNickname(obj as GenerateData);
            Predicate<object> canExecuted = obj => (obj as GenerateData).IsValid;
            Action<object> execute = obj =>
            {
                switch (race)
                {
                    case Races.Orc:
                        OutputNickname = new GenaratorOrcNames().generateNickname(currentData as GenerateData);
                        break;
                    case Races.Elf:
                        OutputNickname = new GenaratorElfNames().generateNickname(currentData as GenerateData);
                        break;
                }
            };
            generateCommand = new RelayCommand(execute, canExecuted);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
 
namespace NicknameGeneratorForWow
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private GenerateData currentData;
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

            Action<object> execute = obj => OutputNickname = new Generator().generateNickname(obj as GenerateData);
            //Predicate<object> canExecuted = obj => (obj as GenerateData).IsValid;
            //generateCommand = new RelayCommand(execute, canExecuted);
            generateCommand = new RelayCommand(execute);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
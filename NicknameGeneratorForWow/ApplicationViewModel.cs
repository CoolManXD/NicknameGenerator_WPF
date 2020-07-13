using System.ComponentModel;
using System.Runtime.CompilerServices;
 
namespace NicknameGeneratorForWow
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private GenerateData currentData;

        public GenerateData CurrentData
        {
            get { return currentData; }
            set
            {
                currentData = value;
                OnPropertyChanged("CurrentData");
            }
        }

        public ApplicationViewModel()
        {
            currentData = new GenerateData();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
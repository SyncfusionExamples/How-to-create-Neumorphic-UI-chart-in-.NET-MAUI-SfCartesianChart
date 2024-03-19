using System.Collections.ObjectModel;

namespace NeumorphismUISample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
    }

    public class Model
    {
        public string Month { get; set; } = string.Empty;
        public double Value { get; set; }
    }

    public class ViewModel
    {
        public ObservableCollection<Model> NeumorphismColumnData { get; set; }

        public ViewModel()
        {
            NeumorphismColumnData = new ObservableCollection<Model>
            {
                new Model(){Month = "Jan",Value = 51},
                new Model(){Month = "Feb",Value = 81},
                new Model(){Month = "Mar",Value = 45},
                new Model(){Month = "Apr",Value = 33},
                new Model(){Month = "May",Value = 61},        
#if WINDOWS || MACCATALYST
                new Model(){Month = "Jun",Value = 81},  
                new Model(){Month = "Jul",Value = 45},  
                new Model(){Month = "Aug",Value = 33},  
                new Model(){Month = "Sep",Value = 60},  
#endif

           };
        }
    }

}

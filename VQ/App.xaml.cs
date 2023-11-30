using VQ.Pages;
namespace VQ {
    public partial class App: Application {
        public App() {
            InitializeComponent();

            MainPage = new BusinessPage();
        }
    }
}
using Firebase.Database;
using Firebase.Database.Query;
using VQ.Models;
using VQ.ViewModel;

namespace VQ {
    public partial class MainPage : ContentPage
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://fantastic-four-vq-default-rtdb.firebaseio.com/");
        public MainPage(MainViewModel vm)
        {
            InitializeComponent();

            //Set the context on the main page 
            BindingContext = vm;
        }
        private async void OnButtonClicked(object sender, EventArgs e)
        {
            QueueItemModel queueItem = new QueueItemModel
            {
                BusinessId = 2,
                UserId = 3,
                JoinTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                ExitTimestamp = null // You may set this as needed
            };

            // Add QueueItem to Database
            FirebaseObject<QueueItemModel> response = await firebaseClient.Child("QueueItems").PostAsync(queueItem);
            InsertBtn.Text = $"Inserted with Key: {response.Key}";

        }
    }

}
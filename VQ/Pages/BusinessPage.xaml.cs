using Firebase.Database;
using Firebase.Database.Query;
using VQ.Models;
namespace VQ.Pages;

public partial class BusinessPage : ContentPage
{
    readonly FirebaseClient firebaseClient = new("https://fantastic-four-vq-default-rtdb.firebaseio.com/");
    readonly int userId = 1; // PlaceHolder until auth

    public BusinessPage()
	{
		InitializeComponent();
        Appearing += Business_Appearing;
    }

    private async void Business_Appearing(object sender, EventArgs e)
    {
        var businessInQueue = await GetBusinessesInQueue(userId);
        waitingView.ItemsSource = businessInQueue;

        var businessNotInQueue = await GetBusinessesNotInQueue(userId);
        notWaitingView.ItemsSource = businessNotInQueue;
    }

    private async Task RefreshPage()
    {
        // TODO: add scroll refresh
        var businessInQueue = await GetBusinessesInQueue(userId);
        waitingView.ItemsSource = businessInQueue;

        var businessNotInQueue = await GetBusinessesNotInQueue(userId);
        notWaitingView.ItemsSource = businessNotInQueue;
    }

    // Example: AddBusiness(new BusinessModel { Id = 1, WaitingList = 8, BusinessName = "McD", BusinessInformation = "A food Company", BusinessLogo = "data:image/png;base64,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" });
    private async void AddBusiness(BusinessModel business)
    {
        FirebaseObject<BusinessModel> response = await firebaseClient.Child("Businesses").PostAsync(business);
        Console.WriteLine($"Inserted Business with Key: {response.Key}");
    }

    private async Task<List<BusinessModel>> GetBusinesses()
    {
        // Get all businesses
        var response = await firebaseClient.Child("Businesses").OnceAsync<BusinessModel>();
        var businessList = response.Select(business => business.Object).ToList();

        // Sort the businessList by name
        businessList.Sort((b1, b2) => string.Compare(b1.BusinessName, b2.BusinessName, StringComparison.Ordinal));

        return businessList;
    }

    private async Task<BusinessModel> GetBusinessById(int businessId)
    {
        var response = await firebaseClient.Child("Businesses").OnceAsync<BusinessModel>();

        var business = response
            .Select(b => b.Object)
            .FirstOrDefault(b => b.Id == businessId);

        return business;
    }

    private async Task<List<BusinessModel>> GetBusinessesInQueue(int userId)
    {
        var response = await firebaseClient.Child("QueueItems")
            .OnceAsync<QueueItemModel>();

        // Get all businesses where the user is in the queue
        var businessIdsInQueue = response
            .Where(item => item.Object.UserId == userId)
            .Select(item => item.Object.BusinessId)
            .ToList();

        var businessesInQueue = await Task.WhenAll(businessIdsInQueue.Select(GetBusinessById));

        // Sort the businessList by name
        var businessList = businessesInQueue.ToList();
        businessList.Sort((b1, b2) => string.Compare(b1.BusinessName, b2.BusinessName, StringComparison.Ordinal));

        return businessList;
    }

    private async Task<List<BusinessModel>> GetBusinessesNotInQueue(int userId)
    {
        var allBusinesses = await GetBusinesses();

        var businessesInQueue = await GetBusinessesInQueue(userId);

        // Get all businesses where the user is not in queue
        var businessesNotInQueue = allBusinesses
            .Where(business => !businessesInQueue.Any(b => b.Id == business.Id))
            .ToList();

        // Sort the businessList by name
        businessesNotInQueue.Sort((b1, b2) => string.Compare(b1.BusinessName, b2.BusinessName, StringComparison.Ordinal));

        return businessesNotInQueue;
    }

    private async void JoinQueueButton(object sender, EventArgs e)
    {
        if (sender is Button JoinButton && JoinButton.CommandParameter is int Id)
        {
            // TODO: Check if user already in queue for the business

            QueueItemModel queueItem = new QueueItemModel
            {
                BusinessId = Id,
                UserId = userId,
                JoinTimestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                ExitTimestamp = "NA" // You may set this as needed
            };

            // Add QueueItem to Database
            var response = await firebaseClient.Child("QueueItems").PostAsync(queueItem);
            Console.WriteLine($"Inserted with Key: {response.Key}");
        }

        await RefreshPage();
    }

    private async void LeaveQueueButton(object sender, EventArgs e)
    {
        if (sender is Button leaveButton && leaveButton.CommandParameter is int businessId)
        {
            // Find and remove the queue item
            var response = await firebaseClient.Child("QueueItems")
                .OnceAsync<QueueItemModel>();

            var queueItem = response
                .Where(item => item.Object.UserId == userId && item.Object.BusinessId == businessId)
                .FirstOrDefault();

            if (queueItem != null)
            {
                // Remove the queue item from the database
                await firebaseClient.Child("QueueItems").Child(queueItem.Key).DeleteAsync();
                Console.WriteLine($"Removed from the queue (Key: {queueItem.Key})");
            }
            else
            {
                Console.WriteLine("User is not in the queue for this business.");
            }
        }

        await RefreshPage();
    }
}

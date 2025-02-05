using System.Threading.Tasks;
using CodeShellCore.Integration.Firebase.Flutter;
using CodeShellCore.Integration.Firebase.Results;

namespace CodeShellCore.Http.Pushing
{
    public interface IFirebaseNotificationService
    {
        string Lang { get; set; }

        Task<FirebasePushResult> SendNotification(FirebaseMessage message, string to = null, string[] topics = null, object data = null);
        Task<FirebasePushResult> SendNotification(FirebaseRequest data);
        Task<FirebasePushResult> SendNotificationToFlutter(FirebaseFlutterRequest request);
    }
}
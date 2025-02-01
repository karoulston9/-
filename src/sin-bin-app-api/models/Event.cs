using Google.Cloud.Firestore;

namespace sin_bin_app_api.Models
{
    [FirestoreData]
    public class Event
    {
        [FirestoreDocumentId]
        public string EventId { get; set; } = string.Empty;
        [FirestoreProperty]
        public string TeamId { get; set; } = string.Empty;
        [FirestoreProperty]
        public Team? Team { get; set; }
        [FirestoreProperty]
        public string PlayerName { get; set; } = string.Empty;
        [FirestoreProperty]
        public string EventType { get; set; } = string.Empty;
        [FirestoreProperty]
        public string Description { get; set; } = string.Empty;
        [FirestoreProperty]
        public int PenaltyMinutes { get; set; }
        [FirestoreProperty]
        public DateTime EventTime { get; set; }
        [FirestoreProperty]
        public DateTime CreatedAt { get; set; }
        [FirestoreProperty]
        public DateTime UpdatedAt { get; set; }
    }
}

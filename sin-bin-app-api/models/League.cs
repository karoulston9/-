using Google.Cloud.Firestore;

namespace sin_bin_app_api.Models
{
    [FirestoreData]
    public class League
    {
        [FirestoreDocumentId]
        public string LeagueId { get; set; } = string.Empty;
        [FirestoreProperty]
        public string Name { get; set; } = string.Empty;
        [FirestoreProperty]
        public string Sport { get; set; } = string.Empty;
        [FirestoreProperty]
        public string Country { get; set; } = string.Empty;
        [FirestoreProperty]
        public string Season { get; set; } = string.Empty;
        [FirestoreProperty]
        public DateTime CreatedAt { get; set; }
        [FirestoreProperty]
        public DateTime UpdatedAt { get; set; }
    }
}

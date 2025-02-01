using Google.Cloud.Firestore;

namespace sin_bin_app_api.Models
{
    [FirestoreData]
    public class Team
    {
        [FirestoreDocumentId]
        public string TeamId { get; set; } = string.Empty;
        [FirestoreProperty]
        public string Name { get; set; } = string.Empty;
        [FirestoreProperty]
        public string LeagueId { get; set; } = string.Empty;
        [FirestoreProperty]
        public League? League { get; set; }
        [FirestoreProperty]
        public string Location { get; set; } = string.Empty;
        [FirestoreProperty]
        public string Logo { get; set; } = string.Empty;
        [FirestoreProperty]
        public DateTime CreatedAt { get; set; }
        [FirestoreProperty]
        public DateTime UpdatedAt { get; set; }
    }
}

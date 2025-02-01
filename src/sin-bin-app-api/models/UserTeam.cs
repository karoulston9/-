using Google.Cloud.Firestore;

namespace sin_bin_app_api.Models
{
    [FirestoreData]
    public class UserTeam
    {
        [FirestoreDocumentId]
        public string UserTeamId { get; set; } = string.Empty;
        
        [FirestoreProperty]
        public string UserId { get; set; } = string.Empty;
        
        [FirestoreProperty]
        public string TeamId { get; set; } = string.Empty;
        
        [FirestoreProperty]
        public string Role { get; set; } = "player"; // possible values: admin, player
        
        [FirestoreProperty]
        public DateTime CreatedAt { get; set; }
        
        [FirestoreProperty]
        public DateTime UpdatedAt { get; set; }
    }
}

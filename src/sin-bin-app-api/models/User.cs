using Google.Cloud.Firestore;

namespace sin_bin_app_api.Models
{
    [FirestoreData]
    public class User
    {
        [FirestoreDocumentId]
        public string UserId { get; set; } = string.Empty;
        [FirestoreProperty]
        public string FirebaseUid { get; set; } = string.Empty;
        
        [FirestoreProperty]
        public string Email { get; set; } = string.Empty;
        
        [FirestoreProperty]
        public string Username { get; set; } = string.Empty;
        
        [FirestoreProperty]
        public string Role { get; set; } = string.Empty;
        [FirestoreProperty]
        public bool EmailVerified { get; set; } = false;
        
        [FirestoreProperty]
        public bool IsActive { get; set; } = true;
        
        [FirestoreProperty]
        public string PhotoUrl { get; set; } = string.Empty;
    
        [FirestoreProperty]
        public DateTime CreatedAt { get; set; }
        [FirestoreProperty]
        public DateTime UpdatedAt { get; set; }
        [FirestoreProperty]
        public DateTime? LastSignInAt { get; set; }
    }
}

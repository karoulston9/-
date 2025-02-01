using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using sin_bin_app_api.Models;

namespace sin_bin_app_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamUsersController : ControllerBase
    {
        private readonly FirestoreDb _firestoreDb;
        private readonly string _collectionName = "teamUsers";

        public TeamUsersController(FirestoreDb firestoreDb)
        {
            _firestoreDb = firestoreDb;
        }

        [HttpPost]
        public async Task<ActionResult<UserTeam>> AddUserToTeam(UserTeam userTeam)
        {
            var collection = _firestoreDb.Collection(_collectionName);
            userTeam.CreatedAt = DateTime.UtcNow;
            userTeam.UpdatedAt = DateTime.UtcNow;
            
            var docRef = await collection.AddAsync(userTeam);
            userTeam.UserTeamId = docRef.Id;
            
            return Created($"/api/teamUsers/{docRef.Id}", userTeam);
        }

        [HttpGet("team/{teamId}")]
        public async Task<ActionResult<IEnumerable<UserTeam>>> GetTeamMembers(string teamId)
        {
            var collection = _firestoreDb.Collection(_collectionName);
            var query = collection.WhereEqualTo("TeamId", teamId);
            var snapshot = await query.GetSnapshotAsync();
            
            return Ok(snapshot.Documents.Select(d => d.ConvertTo<UserTeam>()));
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<UserTeam>>> GetUserTeams(string userId)
        {
            var collection = _firestoreDb.Collection(_collectionName);
            var query = collection.WhereEqualTo("UserId", userId);
            var snapshot = await query.GetSnapshotAsync();
            
            return Ok(snapshot.Documents.Select(d => d.ConvertTo<UserTeam>()));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeamMembership(string id, UserTeam userTeam)
        {
            var docRef = _firestoreDb.Collection(_collectionName).Document(id);
            userTeam.UpdatedAt = DateTime.UtcNow;
            await docRef.SetAsync(userTeam);
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveUserFromTeam(string id)
        {
            var docRef = _firestoreDb.Collection(_collectionName).Document(id);
            await docRef.DeleteAsync();
            
            return NoContent();
        }
    }
}

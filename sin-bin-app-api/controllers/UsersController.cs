using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using sin_bin_app_api.Models;

namespace sin_bin_app_api.Controllers
{
    public class UsersController : BaseApiController
    {
        private readonly FirestoreDb _firestore;
        private const string CollectionName = "users";

        public UsersController(FirestoreDb firestore)
        {
            _firestore = firestore;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                var snapshot = await _firestore.Collection(CollectionName).GetSnapshotAsync();
                var users = snapshot.Documents.Select(d => d.ConvertTo<User>());
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            try
            {
                var docRef = _firestore.Collection(CollectionName).Document(id);
                var snapshot = await docRef.GetSnapshotAsync();

                if (!snapshot.Exists)
                    return NotFound();

                return snapshot.ConvertTo<User>();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            try
            {
                var docRef = _firestore.Collection(CollectionName).Document();
                user.UserId = docRef.Id;
                await docRef.SetAsync(user);
                return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, User user)
        {
            try
            {
                var docRef = _firestore.Collection(CollectionName).Document(id);
                var snapshot = await docRef.GetSnapshotAsync();

                if (!snapshot.Exists)
                    return NotFound();

                user.UserId = id;
                await docRef.SetAsync(user);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                var docRef = _firestore.Collection(CollectionName).Document(id);
                var snapshot = await docRef.GetSnapshotAsync();

                if (!snapshot.Exists)
                    return NotFound();

                await docRef.DeleteAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

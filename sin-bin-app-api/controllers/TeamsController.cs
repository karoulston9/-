using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using sin_bin_app_api.Models;

namespace sin_bin_app_api.Controllers
{
    public class TeamsController : BaseApiController
    {
        private readonly FirestoreDb _firestore;
        private const string CollectionName = "teams";

        public TeamsController(FirestoreDb firestore)
        {
            _firestore = firestore;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            try
            {
                var snapshot = await _firestore.Collection(CollectionName).GetSnapshotAsync();
                var teams = snapshot.Documents.Select(d => d.ConvertTo<Team>());
                return Ok(teams);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(string id)
        {
            try
            {
                var docRef = _firestore.Collection(CollectionName).Document(id);
                var snapshot = await docRef.GetSnapshotAsync();

                if (!snapshot.Exists)
                    return NotFound();

                return snapshot.ConvertTo<Team>();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Team>> CreateTeam(Team team)
        {
            try
            {
                var docRef = _firestore.Collection(CollectionName).Document();
                team.TeamId = docRef.Id;
                await docRef.SetAsync(team);
                return CreatedAtAction(nameof(GetTeam), new { id = team.TeamId }, team);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeam(string id, Team team)
        {
            try
            {
                var docRef = _firestore.Collection(CollectionName).Document(id);
                var snapshot = await docRef.GetSnapshotAsync();

                if (!snapshot.Exists)
                    return NotFound();

                team.TeamId = id;
                await docRef.SetAsync(team);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(string id)
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

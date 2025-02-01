using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using sin_bin_app_api.Models;

namespace sin_bin_app_api.Controllers
{
    public class LeaguesController : BaseApiController
    {
        private readonly FirestoreDb _firestore;
        private const string CollectionName = "leagues";

        public LeaguesController(FirestoreDb firestore)
        {
            _firestore = firestore;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<League>>> GetLeagues()
        {
            try
            {
                var snapshot = await _firestore.Collection(CollectionName).GetSnapshotAsync();
                var leagues = snapshot.Documents.Select(d => d.ConvertTo<League>());
                return Ok(leagues);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<League>> GetLeague(string id)
        {
            try
            {
                var docRef = _firestore.Collection(CollectionName).Document(id);
                var snapshot = await docRef.GetSnapshotAsync();

                if (!snapshot.Exists)
                    return NotFound();

                return snapshot.ConvertTo<League>();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<League>> CreateLeague(League league)
        {
            try
            {
                var docRef = _firestore.Collection(CollectionName).Document();
                league.LeagueId = docRef.Id;
                await docRef.SetAsync(league);
                return CreatedAtAction(nameof(GetLeague), new { id = league.LeagueId }, league);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLeague(string id, League league)
        {
            try
            {
                var docRef = _firestore.Collection(CollectionName).Document(id);
                var snapshot = await docRef.GetSnapshotAsync();

                if (!snapshot.Exists)
                    return NotFound();

                league.LeagueId = id;
                await docRef.SetAsync(league);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLeague(string id)
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

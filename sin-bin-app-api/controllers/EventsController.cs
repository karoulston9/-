using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using sin_bin_app_api.Models;

namespace sin_bin_app_api.Controllers
{
    public class EventsController : BaseApiController
    {
        private readonly FirestoreDb _firestore;
        private const string CollectionName = "events";

        public EventsController(FirestoreDb firestore)
        {
            _firestore = firestore;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
        {
            try
            {
                var snapshot = await _firestore.Collection(CollectionName).GetSnapshotAsync();
                var events = snapshot.Documents.Select(d => d.ConvertTo<Event>());
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(string id)
        {
            try
            {
                var docRef = _firestore.Collection(CollectionName).Document(id);
                var snapshot = await docRef.GetSnapshotAsync();

                if (!snapshot.Exists)
                    return NotFound();

                return snapshot.ConvertTo<Event>();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Event>> CreateEvent(Event @event)
        {
            try
            {
                var docRef = _firestore.Collection(CollectionName).Document();
                @event.EventId = docRef.Id;
                await docRef.SetAsync(@event);
                return CreatedAtAction(nameof(GetEvent), new { id = @event.EventId }, @event);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(string id, Event @event)
        {
            try
            {
                var docRef = _firestore.Collection(CollectionName).Document(id);
                var snapshot = await docRef.GetSnapshotAsync();

                if (!snapshot.Exists)
                    return NotFound();

                @event.EventId = id;
                await docRef.SetAsync(@event);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(string id)
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

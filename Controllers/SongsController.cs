﻿namespace MyMusicList_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly ISong_Repository _repository;

        public SongsController(DbInteractorSqlite context, ISong_Repository repository)
        {
            _repository = repository;
        }

        // GET: api/Songs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
        {
            return Ok(await _repository.GetSongs());
        }

        [HttpGet("favorite")]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongsFavorite()
        {
            return Ok(await _repository.GetSongsFavorite());
        }

        // GET: api/SongsByCategory
        [HttpGet("byCategory/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongsByCategory(string categoryId)
        {
            return Ok(await _repository.GetSongsByCategory(categoryId));
        }
        // GET: api/Songs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Song_Response_Model>> GetSong(string id)
        {
            var song = await _repository.GetSong(id);
            if (song == null) return NotFound();
            return song.SongResponse();
        }

        // PUT: api/Songs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutSong(Song_Response_Model song)
        {
            if (song.SongId == null) return BadRequest();
            var response = await _repository.PutSong(song);
            if (response != null) return BadRequest();
            return NoContent();
        }

        // POST: api/Songs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Song_Response_Model>> PostSong(Song_Request_Model song)
        {
            var songRes = await _repository.PostSong(song);
            if (songRes == null) return BadRequest();
            return CreatedAtAction("GetSong", new { id = songRes.SongId }, songRes.SongResponse());
        }

        // DELETE: api/Songs/5
        [HttpDelete]
        public async Task<IActionResult> DeleteSong(Song_Response_Model songRes)
        {
            var response = await _repository.DeleteSong(songRes.SongId);
            if (!response) return BadRequest();
            return NoContent();
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMusicList_Server.Data;
using MyMusicList_Server.Models;
using MyMusicList_Server.RequestModel;
using MyMusicList_Server.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMusicList_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly DbInteractorSqlite _context;

        public SongsController(DbInteractorSqlite context)
        {
            _context = context;
        }

        // GET: api/Songs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
        {
            return await _context.Songs.ToListAsync();
        }

        // GET: api/Songs/5
        [HttpGet("{id}")]
        public ActionResult<SongResponseModel> GetSong(string id)
        {
            var song = _context.Songs.Where(x => x.SongId == id).FirstOrDefault();

            if (song == null)
            {
                return NotFound();
            }

            var songRes = new SongResponseModel
            {
                SongId = song.SongId,
                SongName = song.SongName,
                SongRating = song.SongRating,
                Artist = song.Artist,
                IsAFavorite = song.IsAFavorite,
                CategoryId = song.CategoryId,
                Url = song.Url,
                CreatedDate = song.CreatedDate,
                UpdatedDate = song.UpdatedDate,
            };

            return songRes;
        }

        // PUT: api/Songs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutSong(SongResponseModel song)
        {
            if (song.SongId == null)
            {
                return BadRequest();
            }

            if (_context.Songs.Where(x => x.SongId == song.SongId).Any())
            {
                var songF = _context.Songs.Where(x => x.SongId == song.SongId).FirstOrDefault();
                songF.SongName = song.SongName;
                songF.SongRating = song.SongRating;
                songF.Artist = song.Artist;
                songF.Url = song.Url;
                songF.IsAFavorite = song.IsAFavorite;
                songF.CategoryId = song.CategoryId;
                songF.UpdatedDate = DateTime.UtcNow;
                songF.CreatedDate = songF.CreatedDate;
                _context.Update(songF);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(song.SongId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Songs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SongResponseModel>> PostSong(SongRequestModel song)
        {
            var dbSong = new Song
            {
                SongId = Guid.NewGuid().ToString(),
                SongName = song.SongName,
                SongRating = song.SongRating,
                Artist = song.Artist,
                IsAFavorite = song.IsAFavorite,
                CategoryId = song.CategoryId,
                CreatedDate = DateTime.UtcNow,
                Url = song.Url,
            };
            _context.Songs.Add(dbSong);

            var songRes = new SongResponseModel
            {
                SongId = dbSong.SongId,
                SongName = dbSong.SongName,
                Artist = dbSong.Artist,
                IsAFavorite = dbSong.IsAFavorite,
                CategoryId = dbSong.CategoryId,
                CreatedDate = dbSong.CreatedDate,
                Url = dbSong.Url,
                SongRating = dbSong.SongRating,
            };
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SongExists(dbSong.SongId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSong", new { id = dbSong.SongId }, songRes);
        }

        // DELETE: api/Songs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(string id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SongExists(string id)
        {
            return _context.Songs.Any(e => e.SongId == id);
        }
    }
}

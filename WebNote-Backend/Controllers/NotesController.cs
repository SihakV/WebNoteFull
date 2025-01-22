using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class NotesController : ControllerBase
{
    private readonly INoteService _noteService;

    public NotesController(INoteService noteService)
    {
        _noteService = noteService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Note>>> GetNotes()
    {
        var notes = await _noteService.GetAllNotesAsync();
        return Ok(notes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Note>> GetNote(int id)
    {
        var note = await _noteService.GetNoteByIdAsync(id);
        if (note == null)
            return NotFound();
        return Ok(note);
    }

    [HttpPost]
    public async Task<ActionResult<Note>> CreateNote(CreateNoteDto noteDto)
    {
        var note = new Note
        {
            Title = noteDto.Title,
            Content = noteDto.Content
        };

        var createdNote = await _noteService.CreateNoteAsync(note);
        return CreatedAtAction(nameof(GetNote), new { id = createdNote.Id }, createdNote);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNote(int id, UpdateNoteDto noteDto)
    {
        var existingNote = await _noteService.GetNoteByIdAsync(id);
        if (existingNote == null)
            return NotFound();

        existingNote.Title = noteDto.Title;
        existingNote.Content = noteDto.Content;

        await _noteService.UpdateNoteAsync(existingNote);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNote(int id)
    {
        var result = await _noteService.DeleteNoteAsync(id);
        if (!result)
            return NotFound();
        return NoContent();
    }
}
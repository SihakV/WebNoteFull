using Dapper;
using Microsoft.Data.SqlClient;

public class NoteService : INoteService
{
    private readonly string _connectionString;

    public NoteService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<IEnumerable<Note>> GetAllNotesAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<Note>("SELECT * FROM Notes ORDER BY CreatedAt DESC");
    }

    public async Task<Note> GetNoteByIdAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<Note>(
            "SELECT * FROM Notes WHERE Id = @Id",
            new { Id = id }
        );
    }

    public async Task<Note> CreateNoteAsync(Note note)
    {
        using var connection = new SqlConnection(_connectionString);
        note.CreatedAt = DateTime.UtcNow;
        note.UpdatedAt = DateTime.UtcNow;

        var sql = @"
            INSERT INTO Notes (Title, Content, CreatedAt, UpdatedAt)
            VALUES (@Title, @Content, @CreatedAt, @UpdatedAt);
            SELECT CAST(SCOPE_IDENTITY() as int)";

        var id = await connection.QuerySingleAsync<int>(sql, note);
        note.Id = id;
        return note;
    }

    public async Task<Note> UpdateNoteAsync(Note note)
    {
        using var connection = new SqlConnection(_connectionString);
        note.UpdatedAt = DateTime.UtcNow;

        await connection.ExecuteAsync(
            @"UPDATE Notes 
            SET Title = @Title, Content = @Content, UpdatedAt = @UpdatedAt
            WHERE Id = @Id",
            note
        );
        return note;
    }

    public async Task<bool> DeleteNoteAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var affected = await connection.ExecuteAsync(
            "DELETE FROM Notes WHERE Id = @Id",
            new { Id = id }
        );
        return affected > 0;
    }
}

# Notes Application

A full-stack note-taking application built with Vue.js, .NET Core, and SQL Server.

## Features

- Create, read, update, and delete notes
- Search functionality
- Sort by creation/update date
- Responsive design
- Full note details view

## Tech Stack

### Frontend
- Vue 3
- TypeScript
- Pinia (State Management)
- Tailwind CSS
- Axios

### Backend
- ASP.NET Core Web API
- Dapper ORM
- SQL Server

## Prerequisites

- Node.js (v16+)
- .NET 7.0 SDK
- SQL Server 2019+
- SQL Server Management Studio (SSMS)

## Installation

### Database Setup

1. Open SQL Server Management Studio
2. Connect to your local SQL Server instance
3. Execute the following SQL script:

```sql
CREATE DATABASE NotesDb;
GO

USE NotesDb;
GO

CREATE TABLE Notes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Content NVARCHAR(MAX),
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2 NOT NULL
);
GO
```

### Backend Setup

1. Clone the repository
```bash
git clone [your-repo-url]
cd [backend-folder]
```

2. Update connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=NotesDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

3. Run the backend
```bash
dotnet run
```
The API will be available at `https://localhost:7089`

### Frontend Setup

1. Navigate to frontend directory
```bash
cd [frontend-folder]
```

2. Install dependencies
```bash
npm install
```

3. Update API URL
- Open `src/stores/noteStore.ts`
- Update `API_URL` constant to match your backend URL

4. Run the development server
```bash
npm run dev
```

## Project Structure

### Backend
```
NotesAPI/
├── Controllers/
│   └── NotesController.cs
├── Models/
│   └── Note.cs
├── Services/
│   ├── INoteService.cs
│   └── NoteService.cs
└── Program.cs
```

### Frontend
```
src/
├── components/
│   ├── NoteList.vue
│   ├── NoteModal.vue
│   └── ViewNoteModal.vue
├── stores/
│   └── noteStore.ts
├── types/
│   └── Note.ts
└── App.vue
```

## API Endpoints

- GET /api/notes - Get all notes
- GET /api/notes/{id} - Get note by ID
- POST /api/notes - Create new note
- PUT /api/notes/{id} - Update note
- DELETE /api/notes/{id} - Delete note

## Contributing

1. Fork the repository
2. Create your feature branch
3. Commit your changes
4. Push to the branch
5. Create a new Pull Request

## License

[Your chosen license]

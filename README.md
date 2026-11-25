# Lingua Learn API

The backend for a language-learning app. Users build vocabulary in **English, Portuguese and Persian**: you add a word in one language, and the API uses Google Gemini to fill in the other two languages, the word type, the Portuguese gender, a category and an example sentence.

Built with ASP.NET Core (.NET 10), Entity Framework Core with SQL Server, ASP.NET Identity with JWT tokens, and Swagger.

## Projects

| Project | What it contains |
|---|---|
| `Language.Learning` | The Web API: controllers, error middleware, `Program.cs` |
| `LanguageLearning.Application` | DTOs, interfaces and services (the app logic) |
| `LanguageLearning.Domain` | Entities and enums |
| `LanguageLearning.Infrastructure` | Database (EF Core), repositories, JWT, Gemini |
| `LanguageLearning.Tests` | NUnit tests for the services |

## Getting started

You need the .NET 10 SDK, a local SQL Server and a Gemini API key.

**1. Store your secrets.** They are not in `appsettings.json`, so they never go to GitHub.

```bash
cd Language.Learning
dotnet user-secrets set "Gemini:ApiKey" "<your Gemini API key>"
dotnet user-secrets set "JwtSettings:Secret" "<a long random text, at least 32 characters>"
```

**2. Create the database.** The connection string is in `Language.Learning/appsettings.json` (default: local SQL Server, database `LanguageLearningDb`).

```bash
dotnet tool install -g dotnet-ef
dotnet ef database update --project LanguageLearning.Infrastructure --startup-project Language.Learning
```

**3. Run the API.**

```bash
dotnet run --project Language.Learning
```

Open `https://localhost:53638/swagger`. Register, log in, click **Authorize** and paste the token.

**4. Run the tests.**

```bash
dotnet test
```

## Endpoints

"Login" means the request needs the header `Authorization: Bearer <token>`.

| Method | URL | Login | What it does |
|---|---|---|---|
| POST | `/api/auth/register` | | Create an account. Returns a token. |
| POST | `/api/auth/login` | | Log in. Returns a token. |
| GET | `/api/words?search=&categoryId=` | | List words. `search` looks in all 3 languages. |
| GET | `/api/words/{id}` | | Get one word |
| POST | `/api/words` | Yes | Add a word. Gemini fills in the rest. |
| DELETE | `/api/words/{id}` | Yes | Delete a word |
| GET | `/api/categories` | | List categories |
| GET | `/api/my-words?isLearned=` | Yes | My saved words. `isLearned=false` shows only words still to learn. |
| POST | `/api/my-words/{wordId}` | Yes | Save a word to my list |
| PUT | `/api/my-words/{wordId}` | Yes | Mark as learned or not: `{ "isLearned": true }` |
| DELETE | `/api/my-words/{wordId}` | Yes | Remove a word from my list |
| GET | `/api/my-words/progress` | Yes | My progress: total, learned, not learned |
| GET | `/api/sentences` | | List common sentences |
| GET | `/api/sentences/{id}` | | Get one sentence |
| POST | `/api/sentences` | Yes | Add a sentence in one language. Gemini translates it. |
| PUT | `/api/sentences/{id}` | Yes | Edit the 3 texts or `isActive` |
| DELETE | `/api/sentences/{id}` | Yes | Delete a sentence |

### Example: add a word

```json
POST /api/words
{
  "word": "gato",
  "language": 2
}
```

Only `word` and `language` are required. You can also send `type`, `portugueseGender`, `category` and `exampleSentence`; anything you leave out, Gemini fills in.

### Example: add a sentence

```json
POST /api/sentences
{
  "text": "How are you?",
  "language": 1
}
```

### Number values

| Field | Values |
|---|---|
| `language` | 1 = English, 2 = Portuguese, 3 = Persian |
| `type` | 1 = Noun, 2 = Verb, 3 = Adjective, 4 = Adverb, 5 = Pronoun, 6 = Preposition, 7 = Conjunction, 8 = Interjection |
| `portugueseGender` | 0 = None, 1 = Masculine, 2 = Feminine |

### Errors

Errors come back as `{ "error": "message" }` with status:

- **400**: bad input (for example "This word is already in your list.")
- **401**: not logged in, or wrong email/password
- **404**: not found
- **500**: unexpected error (details are only in the server log)

Missing or invalid fields (for example an empty `word`) return ASP.NET's standard validation response, also with status 400.

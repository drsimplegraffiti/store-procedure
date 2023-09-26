##### Create a database
```sql
CREATE DATABASE spdb;
USE spdb;
```

##### Create a table
```sql
CREATE TABLE Team(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FirstName VARCHAR(50),
    LastName VARCHAR(50),
    Email VARCHAR(100),
    Age INT,
    Password VARCHAR(100)
    );
```

##### Create stored procedure

```sql
-- SELECT ALL
-- SELECT BY ID
-- INSERT NEW RECORD
-- UPDATE EXISTING RECORDS
-- DELETE EXISTING RECORDS

```

##### Models
```csharp
namespace StoreProcedureApi.Models;

public class Team
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int Age { get; set; }
}
```


###### Create connection string
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=spdb;User Id=SA;Password=Bassguitar1;Encrypt=false;TrustServerCertificate=True;"
  }
}

```

##### Store procedure for CREATE
```sql
CREATE PROCEDURE usp_AddTeam(@FirstName VARCHAR(50),@LastName VARCHAR(50),@Email VARCHAR(100),@Age INT, @Password VARCHAR(100))
AS 
BEGIN
    INSERT INTO Team(FirstName, LastName, Email, Age, Password)
    VALUES (@FirstName, @LastName, @Email,@Age, @Password);
END;
```

##### Store procedure for GET ALL TEAM
```sql
CREATE PROCEDURE usp_GetAllTeam
AS
BEGIN
    SELECT * FROM Team;
END;
```

##### Store procedure for GET Team by Id
```sql
CREATE PROCEDURE usp_GetTeamById(@Id INT)
AS
BEGIN
    SELECT * FROM Team WHERE Id = @Id;
END;
```

##### Store procedure for Update Team by Id
```sql
CREATE PROCEDURE usp_UpdateTeam
(
    @Id INT,
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @Email VARCHAR(100),
    @Age INT,
    @Password VARCHAR(100)
)
    AS
BEGIN
UPDATE Team SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Age = @Age, Password = @Password WHERE Id = @Id;
END;

```

##### Store procedure for Delete Team by Id
```sql
CREATE PROCEDURE usp_DeleteTeam(@Id INT)
AS
BEGIN
    DELETE FROM Team WHERE Id = @Id;
END;
```

##### Find particular item
```sql
DECLARE @RC int
DECLARE @Id int

-- TODO: Set parameter values here.
SET @Id = 7

EXECUTE @RC = [dbo].[usp_GetTeamById] 
   @Id
GO
```

---

##### Get by email
```sql
CREATE PROCEDURE usp_GetTeamByEmail
(
    @Email VARCHAR(100)
)
AS
BEGIN
    SELECT * FROM Team WHERE Email = @Email;
END;
```
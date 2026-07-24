using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/api/users", (string username) =>
{
    string connectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;";

    // VULNERABILIDAD INTENCIONAL: Concatenación directa 
    string query = "SELECT * FROM Users WHERE Username = '" + username + "'";

    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        SqlCommand command = new SqlCommand(query, connection);
    }

    return Results.Ok(new { Message = "Consulta ejecutada", Query = query });
});

app.Run();

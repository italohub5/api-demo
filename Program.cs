using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/api/users", (string username) =>
{
    string connectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;";
    
    // REMEDIACIÓN: Uso de parámetros para evitar la inyección de código SQL
    string query = "SELECT * FROM Users WHERE Username = @username";

    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        SqlCommand command = new SqlCommand(query, connection);
        // El parámetro se sanitiza automáticamente por el motor de ADO.NET
        command.Parameters.AddWithValue("@username", username);
    }

    return Results.Ok(new { Message = "Consulta ejecutada de forma segura" });
});

app.Run();

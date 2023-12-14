using System.Data.SqlClient;
using Dapper;
using SimpleTodoApp.Model;

var builder = WebApplication.CreateBuilder(args); 
var app = builder.Build();
const string connStr = @"Data Source=(localdb)\local;Initial Catalog=Todo;Integrated Security=True";


app.MapGet("/todo", async() =>
{
    var conn = new SqlConnection(connStr);
    const string sql = "SELECT Id, Text, Done FROM Todo";
    var todoItems = await conn.QueryAsync<TodoItem>(sql);
    return todoItems;
});
app.MapPost("/todo", async(TodoItem todoItem) =>
{
    todoItem.Id = Guid.NewGuid();   // Her lager jeg en unik ID til hvert enkelt item, slik at jeg ikke får error når jeg sletter 
                                    // et item jeg har satt til done og prøver å sette det neste itemet på samme index til done.

    using (var conn = new SqlConnection(connStr))
    {
        const string sql = "INSERT INTO Todo (Id, Text) VALUES (@Id, @Text)";
        await conn.ExecuteAsync(sql, new { todoItem.Id, todoItem.Text });
    }

    return todoItem;
});
app.MapPut("/todo/{id}", async (Guid id) =>
{
    using (var conn = new SqlConnection(connStr))
    {
        const string sql = "UPDATE Todo SET Done = @Done WHERE Id = @Id";
        await conn.ExecuteAsync(sql, new { Done = DateTime.Today, Id = id });
    }
    return Results.Ok();
});

app.MapDelete("/todo/{id}", async (Guid id) =>
{
    using (var conn = new SqlConnection(connStr))
    {
        const string sql = "DELETE FROM Todo WHERE Id = @Id";
        await conn.ExecuteAsync(sql, new { Id = id });
    }
    return Results.Ok();
});

app.UseStaticFiles();   // Må være med for å bruke "wwwroot" mappen og de statiske filene i den.
app.Run();
        

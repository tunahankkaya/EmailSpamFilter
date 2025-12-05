using System.Text.Json;
using eMailSpam;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();
var list= JsonSerializer.Deserialize<List<EmailData>>(File.ReadAllText("spam_data.json"));
foreach(var email in list)
{
    Console.WriteLine($"ID: {email.Id}, IsSpam: {email.IsThisSpam}, Content: {email.EmailContent.Substring(0, Math.Min(50, email.EmailContent.Length))}...");
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.Run();


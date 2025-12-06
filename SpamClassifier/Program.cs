using System.Text.Json;
using eMailSpam;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();


string jsonContent = File.ReadAllText("spam_data.json");

// String'i List<EmailData> nesnesine çevir (Deserialize)
var emailList = JsonSerializer.Deserialize<List<EmailData>>(jsonContent);

Console.WriteLine($"Toplam {emailList.Count} adet e-posta yüklendi.");

//Training
Console.WriteLine("Yapay zeka eğitiliyor...");
SpamClassifier classifier = new SpamClassifier();
classifier.Train(emailList);
Console.WriteLine("Eğitim tamamlandı! 🚀");
Console.WriteLine("--------------------------------------------------");

// spam mail
string spamTest = "Congratulations! You have won a free ticket. Click here to claim your prize immediately.";
Console.WriteLine($"\nTest Mesajı: \"{spamTest}\"");
bool isSpam1 = classifier.Predict(spamTest);
Console.WriteLine($"SONUÇ: {(isSpam1 ? "SPAM!" : "TEMİZ (HAM)")}");

//  ham mail
string hamTest = "Hey, are we still meeting tomorrow for the project update? Let me know.";
Console.WriteLine($"\nTest Mesajı: \"{hamTest}\"");
bool isSpam2 = classifier.Predict(hamTest);
Console.WriteLine($"SONUÇ: {(isSpam2 ? "SPAM!" : "TEMİZ (HAM)")}");

while (true)
{
    Console.WriteLine("\n--------------------------------------------------");
    Console.WriteLine("(Çıkmak için 'exit' yaz):");
    string input = Console.ReadLine();

    if (input.ToLower() == "exit") break;

    bool result = classifier.Predict(input);
    Console.WriteLine($"Tahmin: {(result ? "SPAM!" : "TEMİZ (HAM)")}");
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.Run();


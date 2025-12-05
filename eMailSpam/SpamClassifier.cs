using System;

namespace eMailSpam;

public class SpamClassifier
{
    //bellekte tutmak için
    Dictionary<string, int> spamWordCounts = new Dictionary<string, int>();
    Dictionary<string, int> hamWordCounts = new Dictionary<string, int>();
    int spamEmailCount = 0;
    int hamEmailCount = 0;

    public void Train(List<EmailData> trainingData)
    {
        foreach (var email in trainingData)
        {
            var words = email.EmailContent.Split(new char[] { ' ', ',', '.', ';', ':', '!', '?', '\r', '\n', '\t', '(', ')', '[', ']', '{', '}', '<', '>', '/', '\\', '|', '-', '_', '=', '+', '"', '\'' }, StringSplitOptions.RemoveEmptyEntries);
            words = words.Select(w => w.ToLower()).ToArray().DistinctBy(w => w).ToArray();
            //distinctBy ile aynı kelimenin birden fazla sayılmasını engelliyoruz
            if (email.IsThisSpam==1)
            {
                spamEmailCount++;
                foreach (var word in words)
                {
                    if (spamWordCounts.ContainsKey(word))
                        spamWordCounts[word]++;
                    else
                        spamWordCounts[word] = 1;
                }
            }
            else
            {
                hamEmailCount++;
                foreach (var word in words)
                {
                    if (hamWordCounts.ContainsKey(word))
                        hamWordCounts[word]++;
                    else
                        hamWordCounts[word] = 1;
                }
            }
        }
    }

    /*
        Tahmin(Prediction) fazı.
        P(Kelime|Spam)
     */
    public bool Predict(string emailContent)
    {
        var words = emailContent.Split(new char[] { ' ', ',', '.', ';', ':', '!', '?', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                           .Select(w => w.ToLower())
                           .Distinct()
                           .ToArray();

        // P(spam) ve P(ham)
        //sayılar kaybolmasın(underflow) diye logaritma ile ortalama alıyoruz
        double totalEmails = spamEmailCount + hamEmailCount;
        double spamScore = Math.Log(spamEmailCount/totalEmails);
        double hamScore = Math.Log(hamEmailCount / totalEmails);

        foreach (var word in words)
        {
            //kelime spamlı mesajlarda kaç kere geçti? => spam skoru
            int spamCount = spamWordCounts.ContainsKey(word) ? spamWordCounts[word]:0;

            // Formül: (Kelime Sayısı + 1) / (Toplam Spam Mesaj + 2)
            // Neden +2? Çünkü iki ihtimal var: Kelime ya var ya yok. (Bernoulli Smoothing)
            double pWordGivenSpam = (spamCount + 1.0) / (spamEmailCount + 2.0);
            spamScore += Math.Log(pWordGivenSpam);

            double hamCount = hamWordCounts.ContainsKey(word) ? spamWordCounts[word]:0;
            double pWordGivenHam = (hamCount + 1.0) / (hamEmailCount + 2.0);
            hamScore += Math.Log(pWordGivenHam);
        }
        Console.WriteLine($"Spam Score:{spamScore:F2} | Ham Score:{hamScore:F2}");

        return spamScore>hamScore;

    }
}

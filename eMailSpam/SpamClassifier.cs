using System;

namespace eMailSpam;

public class SpamClassifier
{
    //bellekte tutmak için
    Dictionary<string, int> spamWordCounts = new Dictionary<string, int>();
    Dictionary<string, int> hamWordCounts = new Dictionary<string, int>();
    int spamTotalTokenCount = 0;
    int hamTotalTokenCount = 0;

    //kelime dağarcağı (Vocabulary size) - Laplace smoothing için gerekli
    HashSet<string> vocabulary = new HashSet<string>();

    public void Train(List<EmailData> trainingData)
    {
        foreach (var email in trainingData)
        {
            var words = email.EmailContent.Split(new char[] { ' ', ',', '.', ';', ':', '!', '?', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                                               .Select(w => w.ToLower())
                                               // Distinct() yok frekans değerlerini sayıyoruz => fi
                                               .ToArray();
            if (email.IsThisSpam==1)
            {
                foreach (var word in words)
                {
                    if (spamWordCounts.ContainsKey(word))
                        spamWordCounts[word]++; //frekans
                    else
                    spamWordCounts[word] =1;

                    spamTotalTokenCount++; //toplam spam kelime sayısını bir arttır
                    vocabulary.Add(word); //kelimeyi genel havuza ekle
                }
            }
            else
            {
                foreach (var word in words)
                {
                    if (hamWordCounts.ContainsKey(word))
                        hamWordCounts[word]++;
                    else
                        hamWordCounts[word] = 1;

                    hamTotalTokenCount++;
                    vocabulary.Add(word);
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
                           .ToArray();

        //Başlangıç puanları(Priors) | artık kelime sayılarına göre oranlıyoruz
        double totalTokens = spamTotalTokenCount + hamTotalTokenCount;
        double spamScore = Math.Log((double)spamTotalTokenCount/totalTokens);
        double hamScore = Math.Log((double)hamTotalTokenCount / totalTokens);

        int vocabSize = vocabulary.Count; //paydaya eklenecek sayı
        foreach (var word in words)
        {
            // MULTINOMIAL NAIVE BAYES FORMÜLÜ

            int spamCount = spamWordCounts.ContainsKey(word) ? spamWordCounts[word] : 0;
            double pSpam = (spamCount + 1.0) / (spamTotalTokenCount + vocabSize);
            spamScore += Math.Log(pSpam);

            int hamCount = hamWordCounts.ContainsKey(word) ? hamWordCounts[word] : 0;
            // Payda: Toplam Ham Kelime Sayısı + Kelime Dağarcığı
            double pHam = (hamCount + 1.0) / (hamTotalTokenCount + vocabSize);
            hamScore += Math.Log(pHam);
        }
    Console.WriteLine($"Spam Skoru: {spamScore:F2} | Ham Skoru: {hamScore:F2}");

        return spamScore > hamScore;

    }
}

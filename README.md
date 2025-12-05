# 📧 C# Naive Bayes Spam Classifier (v1)

Bu proje, C# ve .NET Core kullanılarak **sıfırdan (from scratch)** geliştirilmiş bir E-Posta Spam Filtresi uygulamasıdır. Herhangi bir hazır makine öğrenmesi kütüphanesi (ML.NET, Python Scikit-learn vb.) kullanılmadan, **Naive Bayes** algoritmasının saf matematiksel mantığı kodlanmıştır.

## 🎯 Proje Amacı
İstatistiksel öğrenme yöntemlerinin yazılım dünyasında nasıl uygulandığını anlamak ve "Metin Sınıflandırma" (Text Classification) algoritmalarının temellerini kavramak.

## 🛠️ Kullanılan Teknolojiler
* **Dil:** C#
* **Veri Formatı:** JSON (CSV'den dönüştürülmüş Spam/Ham veri seti)
* **Algoritma:** Naive Bayes (Bernoulli Modeli)
* **Veri Yapıları:** `Dictionary<string, int>`, `HashSet`, `List<T>`

## 🧮 Matematiksel Arkaplan (v1 Yaklaşımı)
Bu versiyonda **Bernoulli Naive Bayes** yaklaşımı benimsenmiştir.
* Kelimelerin metin içinde kaç kere geçtiği değil, **var olup olmadığı** (1 veya 0) dikkate alınır.
* **Laplace Smoothing:** Sıfır frekans hatasını (Zero Probability Problem) engellemek için tüm olasılıklara `+1` eklenmiştir.
* **Log-Sum-Exp:** "Underflow" (sayıların sıfıra yuvarlanması) sorununu aşmak için olasılıklar çarpılmak yerine logaritmaları alınarak toplanmıştır.

Formül:
$$P(Spam | Kelime) \propto \log(P(Spam)) + \sum \log(P(Kelime_i | Spam))$$

## ⚠️ Bilinen Sorunlar (Known Issues - v1)
Bu versiyon (v1), **"Dengesiz Veri Seti" (Imbalanced Dataset)** üzerinde eğitildiğinde (Spam sayısı < Ham sayısı), nadir kelimeler içeren **Normal (Ham)** e-postaları yanlışlıkla **Spam** olarak işaretleme eğilimindedir.
<img width="1117" height="397" alt="image" src="https://github.com/user-attachments/assets/cf76db90-978f-4223-b605-de9e1fab4eef" />
**Tespit Edilen Problem:** "Rare Word Paradox". Model, eğitim setinde az rastlanan kelimeleri gördüğünde, Spam sınıfının paydası daha küçük olduğu için matematiksel olarak Spam ihtimalini daha yüksek hesaplamaktadır.

* **Planlanan Çözüm (v2):** Algoritma, kelime varlığına bakan Bernoulli modelinden, kelime frekanslarını ve toplam kelime havuzunu dikkate alan **Multinomial Naive Bayes** modeline güncellenecektir.

## 🚀 Kurulum ve Çalıştırma

1. Projeyi klonlayın:
   ```bash
   git clone [https://github.com/kullaniciadi/repo-adi.git](https://github.com/kullaniciadi/repo-adi.git)


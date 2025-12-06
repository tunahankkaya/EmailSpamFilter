# 📧 C# Spam Classifier (v2 - Multinomial Naive Bayes)

Bu proje, C# ve .NET Core kullanılarak **sıfırdan (from scratch)** geliştirilmiş, harici bir ML kütüphanesi kullanılmadan saf matematiksel mantıkla çalışan bir E-Posta Spam Filtresi uygulamasıdır.

Proje, **Bernoulli** modelinden (v1) başlayıp, dengesiz veri setlerinde daha başarılı olan **Multinomial** modele (v2) evrilen bir öğrenme sürecinin ürünüdür.

## 🎯 Proje Kazanımları ve Mühendislik Vizyonu
Bu proje sadece çalışan bir kod parçası değil, aynı zamanda temel Yapay Zeka ve Veri Mühendisliği kavramlarının derinlemesine analizidir:

* **Matematiksel Sezgi (Mathematical Intuition):** Bayes Teoremi'nin sadece bir formül olmadığı; yeni kanıtlarla (kelimelerle) mevcut inancın (Spam/Ham olasılığı) nasıl güncellendiği kod üzerinde simüle edildi.
* **Veri Mühendisliği (Data Engineering):** Ham verinin (CSV) işlenebilir formata (JSON) dönüştürülmesi, ETL süreçleri ve veri temizliği (Tokenization, Case-folding) işlemleri manuel olarak yönetildi.
* **Algoritmik Problem Çözme:**
    * **Underflow Problemi:** Çok küçük olasılıkların çarpımı sonucu oluşan veri kaybı, **Log-Sum-Exp** yöntemiyle (çarpma yerine logaritma toplama) çözüldü.
    * **Zero Probability:** Hiç görülmemiş kelimelerin sistemi çökertmemesi için **Laplace Smoothing** uygulandı.
    * **Model Optimizasyonu:** "Rare Word Paradox" hatası tespit edilerek, kelime varlığına bakan Bernoulli modelinden, kelime frekansına ve havuz yoğunluğuna bakan Multinomial modele geçiş yapıldı.

## 🛠️ Teknik Detaylar (v2)
* **Algoritma:** Multinomial Naive Bayes
* **Dil:** C# (.NET 8.0)
* **Veri Yapısı:** `Dictionary<string, int>` (Frekans Sayımı) ve `HashSet` (Vocabulary)
* **Yumuşatma (Smoothing):** Paydaya `Vocabulary Size` eklenerek nadir kelimelerin ağırlığı dengelendi.

### Formül (Multinomial)
Her bir kelimenin skor katkısı şu şekilde hesaplanır:

$$\text{Score} += \log \left( \frac{\text{Kelime Frekansı} + 1}{\text{Toplam Token Sayısı} + \text{Vocabulary Size}} \right)$$

## 🔄 Sürüm Geçmişi

### v2 (Güncel - Stable) ✅
* **Yöntem:** Kelime frekansları dikkate alınır (Frequency-based).
* **İyileştirme:** Paydaya toplam kelime dağarcığı (Vocabulary Size) eklendi.
* **Sonuç:** Dengesiz veri setlerinde (Imbalanced Dataset) oluşan "False Positive" hataları giderildi. "Project meeting" gibi nadir kelimeler içeren normal mesajlar artık doğru sınıflandırılıyor.

### v1 (Eski Sürüm) ⚠️
* **Yöntem:** Bernoulli Naive Bayes (Kelime var/yok).
* **Sorun:** Spam mesaj sayısı az olduğunda, nadir görülen kelimeler matematiksel olarak Spam ihtimalini yapay şekilde yükseltiyordu (Rare Word Paradox).


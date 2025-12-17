# 📧 C# Spam Classifier (v2 - Multinomial Naive Bayes)

Bu proje, C# ve .NET Core kullanılarak **sıfırdan (from scratch)** geliştirilmiş, harici bir ML kütüphanesi kullanılmadan saf matematiksel mantıkla çalışan bir E-Posta Spam Filtresi uygulamasıdır.

Proje, **Bernoulli** modelinden (v1) başlayıp, dengesiz veri setlerinde daha başarılı olan **Multinomial** modele (v2) evrilen bir öğrenme sürecinin ürünüdür.

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

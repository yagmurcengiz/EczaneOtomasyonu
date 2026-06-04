Eczane Otomasyonu (Pharmacy Automation)
Proje Özeti
Bu proje, müşteri ve yönetici (admin) rollerini barındıran web tabanlı bir eczane otomasyon sistemidir.

Müşteriler sisteme giriş yaparak güncel ilaç stoklarını görüntüleyebilir ve sepetlerine ürün ekleyip çıkarabilir.

Yöneticiler (Admin), arka planda müşterilerin oluşturduğu sepetleri anlık olarak takip edebilir.

Rol tabanlı yetkilendirme (Authorization) kullanılarak, kullanıcıların sadece kendi yetki alanlarındaki sayfaları görmesi sağlanmıştır.

Geliştirme Ortamı ve Teknolojiler
Programlama Dili: C#

Framework: ASP.NET Core MVC

Veritabanı Erişimi: Entity Framework Core

Geliştirme Ortamı (IDE): Visual Studio

Arayüz (Front-end): HTML, CSS, Bootstrap ve Razor View Engine

Projenin Yüklenmesi ve Çalıştırılması
Proje dosyalarını bilgisayarınıza indirin veya git üzerinden klonlayın.

Visual Studio'yu açın ve proje klasörünün içindeki EczaneOtomasyonu.sln dosyasına çift tıklayarak projeyi yükleyin.

Çözüm Gezgini (Solution Explorer) penceresinden bağımlılıkların (NuGet paketlerinin) yüklendiğinden emin olun.

Veritabanı bağlantısının sağlanması için appsettings.json dosyası içindeki veritabanı bağlantı cümlesini (Connection String) kendi yerel SQL sunucunuza göre güncelleyin.

Package Manager Console (Paket Yöneticisi Konsolu) üzerinden veritabanını oluşturmak için sırasıyla şu komutları çalıştırın:

Add-Migration InitialCreate

Update-Database

Klavyeden Ctrl + F5 (veya F5) tuşlarına basarak projeyi derleyip tarayıcı üzerinde çalıştırın.

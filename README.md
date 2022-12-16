# Rise Assessment

Rise Firması için Assessment. 

Birbiriyle REST üzerinden iletişim kuran iki mikroservis. ContactMicroservis bir telefon rehberi için gerekli bilgileri tutuyor, bu mikroserviste ekleme, silme güncelleştirme yapılabiliyor. Rapor gerekince ReportMicroservis ile iletişim kuruyor. ReportMicroservis, rapor taleplerini RabbitMQ'ya iletiyor. Hangfire ile dakikada bir defa RabbitMQ'dan biriken rapor taleplerini çekerek işleme alıyor. Rapor taleplerinin son durumunu da gerektiğinde ContactMicroservis'e iletiyor.

## Çalıştırma

1. İki ayrı PostgreSQL veritabanı hazır bulunmalı. ReportMicroservis için hazırlanan veritabanı bilgileri, ReportMicroservis projesinin appsettings.json dosyasında güncellenmeli. ContactMicroservis için hazırlanan veritabanı bilgileri de ContactMicroservis projesinin appsettings.json dosyasında güncellenmeli.

2. RabbitMQ, ReportMicroservis projesinin çalıştığı serverda kurulu olmalı. RabbitMQ link bilgisi ReportMicroservis projesinin appsettings.json dosyasında güncellenmeli.

3. ReportMicroservis ve ContactMicroservis projelerinin linkleri, iki projenin de appsettings.json dosyalarında güncellenmeli.

4. ContactMicroservis ve ReportMicroservis aynı anda çalışıyor olmalı.

5. İşlemler doğrudan ContactMicroservis projesine request atarak yapılır. ContactMicroservis projesinde swagger kurulu. Swagger ile denemeler yapılabilir.

GET /PhoneBook                   | Bütün rehberdeki kişilerin Guid, isim ve soy isimlerini döndürüyor.
PUT /PhoneBook                   | Rehbere yeni bir kişi kaydediyor.
DELETE /PhoneBook                | Rehberden bir kişiyi siliyor.
POST /PhoneBook                  | Rehberde güncelleştirme yapıyor.
GET /PhoneBook/GetPhoneBookItem  | Rehberdeki bir kişiye ait detaylı bilgi döndürür.
GET /PhoneBook​/Request           | Rapor talebinde bulunur. Hazırlanacak raporun Guid bilgisini döndürür.
GET ​/PhoneBook​/ReportInfo        | Hazırlanmış raporlar listesi döndürür.
GET /PhoneBook​/reportStatus      | Hazırlanmakta olan rapora ait bilgileri döndürür.

6. Bir rapor şu şekilde oluşturuluyor: Rapor talebinde bulunmak için /PhoneBook/Request metodu çalıştırılıyor. Bir rapor talebinde bulunuluyor. Oluşturulacak olan raporun Guid'i geri döndürülüyor ve talep kuyruğa alınıyor.(RabbitMQ). Ardından Rapor oluşana kadar /PhoneBook/ReportStatus endpoint'inde statü sorgulaması yapmak gerekiyor. Rapor tamamlandığında statüsü Completed olacak ve oluşturulan Excel dosyasının yolu iletilecek.
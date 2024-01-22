# Sınav Projesi

Bu proje N katmanlı mimari kullanılarak geliştirilmiştir. Her katmanın belirli bir sorumluluğu vardır ve aşağıdaki şekilde açıklanmıştır.

## Katmanlar ve Sorumlulukları

### EntityLayer:

- Uygulamanın veritabanındaki tabloları temsil eden entity sınıflarını içerir.

### BusinessLayer:

- DTO'ları (Data Transfer Objects) ve arayüzleri içerir.
- İş mantığı kurallarını ve uygulama katmanları arasında iletişimi sağlayan arayüzleri barındırır.

### DataAccessLayer:

- BusinessLayer'daki arayüzleri kullanarak servisleri ve veritabanı işlemlerini gerçekleştiren sınıfları içerir.
- EF Core gibi ORM araçlarını kullanarak veritabanı işlemlerini yönetir.

### ExamProjectUI:

- MVC tasarımıyla oluşturulan bu katman, uygulamanın sunum katmanını temsil eder.
- Admin ve kullanıcı kontrollerini içerir, sayfa yönlendirmelerini yönetir.
- BusinessLayer'a bağımlıdır ve iş mantığını kullanarak verileri görüntüler.

## Kurulum

### Connection String ve Context Ayarları:

1. Veritabanı oluşturun ve bağlantı dizesini alın.
2. MVC projesindeki `appsettings.json` dosyasındaki `DefaultConnection` bölümündeki bağlantı dizesini oluşturduğunuz veritabanına uygun olarak ayarlayın.
3. `DesignTimeDbContextFactory` classının içindeki `UseSqlServer` içindeki bağlantıya da veritabanından aldığımız ConnectionString’i yazıyoruz.

### Migration ve Veritabanı İşlemleri:

1. DataAccessLayer projesine sağ tıklayın ve "Open Terminal" seçeneğini seçin.
2. Terminal veya komut istemcisine şu komutları sırasıyla yazarak migration'ı oluşturun ve veritabanınızı güncelleyin:
   ```bash
   dotnet ef migrations add mig1   # migration oluşturur
   dotnet ef database update       # veritabanını günceller
## Rol Yapısı

- Roller enum şeklinde tutuldu: Admin, User.

## SeedData ve Admin Oluşturma

`DataAccessLayer`'daki `SeedData` içinde adminin otomatik olarak oluşturulduğu bölümde admin bilgileri (kullanıcı adı, şifre) bulunuyor. Bu bilgiler özelleştirilebilir. Proje ilk kez çalıştığında admin oluşacak. Sonraki kayıtlar “User” rolünde sisteme kayıt olacak.

- Kullanıcı kayıt bilgileri girilirken kullanıcı adı ve mailinin veritabanında başkasında olmamasına özen gösterilmelidir.
- Şifre oluştururken büyük-küçük harf, rakam ve özel karakter kullanılmalıdır (örneğin `User1.`). Bu durumlar karşılanmadığı takdirde kayıt oluşmaz.
- Admin giriş yaptıktan sonra yönetim paneline yönlendirilir.
- Kullanıcı kayıt olduktan giriş sayfasına yönlendirilir. Giriş yaptıktan sonra yönlendirilen sayfa kendisine ait sınavların listelendiği sayfadır.

## Projenin Çalışma Aşaması

- İlk olarak bir kategori ekliyoruz, ardından o kategoriye ait sınav ekliyoruz.
- Sınava göre soru kategorisi ekliyoruz, soru kategorisine göre soru ekliyoruz.
- Soruya şık ekliyoruz. Sonrasında sistemde kayıt olan kullanıcıya sınav ataması yapıyoruz.
- Sınav atanan kullanıcı kendi sayfasında sınavlarını liste şeklinde görebilecek. Sınav zamanı geldiğinde “Sınava Git” butonu aktif olacak, butona tıklayıp sınav hakkında bilgilerin (sınav adı, açıklaması, süresi, başlama - bitiş tarihi) bulunduğu bir sayfaya gelecek. “Sınava Başla” butonuna bastıktan sonra soruların listelendiği sayfaya gelecek. Soruları işaretledikten sonra “Sınavı Tamamla” butonuna tıklayarak sınavı bitirebilir. Sınav süresi üst tarafta yazmakta olup geriye doğru saymaktadır. Butona basamadan sınav süresi dolduğu zaman o zamana kadar işaretlediği sorular otomatik veritabanına kaydolur. Kullanıcının sınava tek giriş hakkı vardır, aynı sınava birden fazla kez giremez. Kullanıcı sınavı bitirip istenilen sonucu aldıysa listede “Başarılı”, alamadıysa “Başarısız” yazısı çıkmaktadır.

## ENDPOINTLER

### Admin Endpointleri:

- `/Categories/GetAllListCategory`: Kategorileri listeler.
- `/Categories/CreateCategory`: Kategori ekler.
- `/Categories/UpdateCategory`: Kategoriyi günceller.
- `/Categories/DeleteCategory`: Kategoriyi siler.

- `/Exams/GetAllListExam`: Sınavları listeler.
- `/Exams/CreateExam`: Sınav ekler.
- `/Exams/UpdateExam`: Sınavları siler.
- `/Exams/DeleteExam`: Sınavları siler.
- `/Exams/CreateExamAssignment`: Sınav atamasını yapar.
- `/Exams/GetAllListExamAssignments`: Sınav atamalarını listeler.

- `/QuestionCategories/GetAllListQuestionCategories`: Soru kategorilerini listeler.
- `/QuestionCategories/CreateQuestionCategory`: Yeni soru kategorisini ekler.
- `/QuestionCategories/UpdateQuestionCategory`: Soru kategorisini günceller.
- `/QuestionCategories/DeleteQuestionCategories`: Soru kategorisini siler.

- `/Questions/GetAllListQuestions`: Soruları listeler.
- `/Questions/CreateQuestion`: Soru ekler.
- `/Questions/UpdateQuestion`: Soru günceller.
- `/Questions/DeleteQuestion`: Soru siler.

- `/Choices/GetAllListQuestions`: Şıkları listeler.
- `/Choices/CreateQuestion`: Şık ekler.
- `/Choices/DeleteQuestion`: Şık siler.

### User Endpointleri:

- `/User/GetAllExams`: Kendisine atınan sınavları listeler.
- `/User/StartExam`: Sınavı başlatır.
- `/User/RemoveOrder`: Sepetteki ürünü siler.
- `/User/ExamPage`: Sınav sorularının listeler.
- `/User/SubmitExam`: Cevapları kaydeder.

### Hesap Endpointleri:

- `/Account/AddUser`: Kullanıcı oluşturur.
- `/Account/Login`: Sisteme giriş yapar.
- `/Account/Logout`: Sistemden çıkış yapar.

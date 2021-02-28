using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MuavinCode;
using OrnekMuavinCore.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OrnekMuavinCore.Controllers
{
    public class HomeController : Controller
    {

        private string _hostingEnvironment;
        public HomeController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment.WebRootPath;
        }
        //Yukarıdaki işlem sayesinde _hostingEnvironment string değeri projenizin wwwroot klasör yolunuza ulaşmanızı sağlar.



        public IActionResult Index()
        {
            //  1-) UrlDonustur();
            //    Geri size döndüreceği string değer "her-hangi-bir-haber-basligi" şeklinde olacaktır.
            var deger = Muavin.UrlDonustur("Her Hangi Bir HaBer BAŞLIĞI");

            return View();
        }

        public IActionResult DosyaEkleIslemi()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DosyaEkle(IFormFile dosya)
        {
            var dosyaadi = "";

            // Dosya ekleme işlemini yapmadan önce dosyanın seçilip seçilmediğini kontrol ediyoruz.
            if (dosya != null)
            {
                dosyaadi = Muavin.DosyaEkle(dosya.OpenReadStream(), dosya.ContentType.Split('/')[1], _hostingEnvironment + "/dosya/");
            }
            //  dosya.OpenReadStream(), IFormFile bize yüklemek istediğimiz dosyanın stream formatını veriyor.
            //  dosya.ContentType.Split('/')[1], dosyanın uzantısını alabiliyoruz. İsterseniz 'Path.GetExtension(dosya.FileName)' methodu ile de dosyanızın uzantısını alabilirsiniz.
            //  _hostingEnvironment + "/dosya/" işlemi ile de dosyamızın yüklemesini istediğimiz klasörün dosya yolunu belirtiyoruz.

            //  Eğer dosya ekleme işlemi başarılı olursa Guid methodu ile rastgele 40 karakterlik string adında dosyanızı kayıt eder ve size o string adını döndürür.

            return View();
        }

        public IActionResult DosyaGuncelleIslemi()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DosyaGuncelle(IFormFile dosya)
        {
            var dosyaadi = "";
            var eskidosyaadi = "deneme.pdf";

            // Dosya güncelleme işlemini yapmadan önce dosyanın seçilip seçilmediğini kontrol ediyoruz.
            if (dosya != null)
            {
                dosyaadi = Muavin.DosyaGuncelle(dosya.OpenReadStream(), dosya.ContentType.Split('/')[1], _hostingEnvironment + "/dosya/", eskidosyaadi);
            }
            //  dosya.OpenReadStream(), IFormFile bize yüklemek istediğimiz dosyanın stream formatını veriyor.
            //  dosya.ContentType.Split('/')[1], dosyanın uzantısını alabiliyoruz. İsterseniz 'Path.GetExtension(dosya.FileName)' methodu ile de dosyanızın uzantısını alabilirsiniz.
            //  _hostingEnvironment + "/dosya/" işlemi ile de dosyamızın yüklemesini istediğimiz klasörün dosya yolunu belirtiyoruz.
            //  eskidosyaadi, güncelleme işlemi sırasında eski dosyanız varsa onu silmeniz için yazabilirsiniz. yok ise boş geçebilirsiniz.

            //  Eğer dosya ekleme işlemi başarılı olursa Guid methodu ile rastgele 40 karakterlik string adında dosyanızı kayıt eder ve size o string adını döndürür. Eğer başarısız olursa eski dosya ismini geri döndürür.

            return View();
        }

        public IActionResult CokluDosyaEkleIslemi()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CokluDosyaEkle(List<IFormFile> dosyalar)
        {
            // Dosyaları eklemeden önce dosya seçilip seçilmediğini kontrol ediyoruz.
            if (dosyalar.Count > 0)
            {
                // Birden fazla dosya işlemi için yeni bir stream listesi ve dosya uzantıları için yeni bir string listesi oluşturuyoruz.
                List<Stream> stDosyaListesi = new List<Stream>();
                List<string> dosyauzantilari = new List<string>(); // Eğer seçili dosyalar ortak uzantı ise bunu oluşturmanıza gerek yok.

                // Seçilen dosyaları döngüye sokup tek tek stream listesine ve uzantı listesine ekliyoruz.
                foreach (var item in dosyalar)
                {
                    stDosyaListesi.Add(item.OpenReadStream());
                    dosyauzantilari.Add(item.ContentType.Split('/')[1]);
                }

                // Eğer seçili dosyalarınızın hepsi aynı uzantıda ise bu işlemi kullanabilirsiniz.
                var dosyaisimleri1 = Muavin.CokluDosyaEkle(stDosyaListesi, ".pdf", _hostingEnvironment + "/dosya/");

                // Eğer seçili dosyalarınızın uzantıları birbirinden farklı ise işlemi uygulayabilirsiniz.
                var dosyaisimleri2 = Muavin.CokluDosyaEkle(stDosyaListesi, dosyauzantilari, _hostingEnvironment + "/dosya/");

                // stDosyaListesi, seçili dosyalarınızın stream listesi
                // ".pdf", eğer ortak uzantı ise tek bir uzantı adı yazabilirsiniz.
                // dosyauzantilari, eğer uzantılar farklı ise string listesi halinde verebilirsiniz.
                // _hostingEnvironment + "/dosya/" methodu ile de dosyamızın yüklemesini istediğimiz klasörün dosya yolu.
            }

            return View();
        }

        public IActionResult ResimEkleIslemi()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResimEkle(IFormFile resim)
        {
            // Resmin seçili olup olmadığını kontrol ediyoruz.
            if (resim != null)
            {
                var resimadi = Muavin.ResimEkle(resim.OpenReadStream(), resim.ContentType.Split('/')[1], _hostingEnvironment + "/images/", true, 1000);

                // resim.InputStream, IFormFile bize yüklemek istediğimiz resmimizin stream formatını veriyor.
                // resim.ContentType.Split('/')[1], resmin uzantısını alabiliyoruz. İsterseniz 'Path.GetExtension(dosya.FileName)' methodu ile de resmin uzantısını alabilirsiniz.
                //  _hostingEnvironment + "/images/" methodu ile de resmin yüklemesini istediğimiz klasörün dosya yolunu belirtiyoruz.
                // true, yüklenecek resmin dosya boyutunda küçülme işlemi yaptırmanızı sağlar. Eğer orjinal boyutu olsun isterseniz false yazınız.
                //ÖNEMLİ NOT: Eğer yüklemek istediğiniz resim arka planı transparan ise arka planı siyah renk yapacaktır.
                // 1000, Resmin olmasını istediğiniz genişliği belirtiyoruz. Yüksekliği orantı işlemi hesaplanacaktır. Eğer orjinal genişlikte kalmasını isterseniz "0" sıfır yazınız.

                // Eğer resim ekleme işlemi başarılı olursa Guid methodu ile rastgele 40 karakterlik string adında resmi kayıt eder ve size o string adını döndürür. Başarısız olduğu durumda boş bir string değer döndürür.
            }

            return View();
        }

        public IActionResult ResimGuncelleIslemi()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResimGuncelle(IFormFile resim)
        {
            string eskiresimadi = "deneme.jpg";

            // Resmin seçili olup olmadığını kontrol ediyoruz.
            if (resim != null)
            {
                var resimadi = Muavin.ResimGuncelle(resim.OpenReadStream(), resim.ContentType.Split('/')[1], _hostingEnvironment + "/images/", eskiresimadi, true, 1000);

                // resim.InputStream, IFormFile bize yüklemek istediğimiz resmimizin stream formatını veriyor.
                // resim.ContentType.Split('/')[1], resmin uzantısını alabiliyoruz. İsterseniz 'Path.GetExtension(dosya.FileName)' methodu ile de resmin uzantısını alabilirsiniz.
                //  _hostingEnvironment + "/images/" methodu ile de resmin yüklemesini istediğimiz klasörün dosya yolunu belirtiyoruz.
                // eskiresimadi, güncelleme işlemi sırasında eski resminiz varsa onu silmeniz için yazabilirsiniz. yok ise boş geçebilirsiniz.
                // true, yüklenecek resmin dosya boyutunda küçülme işlemi yaptırmanızı sağlar. Eğer orjinal boyutu olsun isterseniz false yazınız.
                //ÖNEMLİ NOT: Eğer yüklemek istediğiniz resim arka planı transparan ise arka planı siyah renk yapacaktır.
                // 1000, Resmin olmasını istediğiniz genişliği belirtiyoruz. Yüksekliği orantı işlemi hesaplanacaktır. Eğer orjinal genişlikte kalmasını isterseniz "0" sıfır yazınız.

                // Eğer resim ekleme işlemi başarılı olursa Guid methodu ile rastgele 40 karakterlik string adında resmi kayıt eder ve size o string adını döndürür. Başarısız olduğu durumda boş bir string değer döndürür.
            }

            return View();
        }

        public IActionResult CokluResimEkleIslemi()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CokluResimEkle(List<IFormFile> resimler)
        {
            // Resimleri eklemeden önce dosya seçilip seçilmediğini kontrol ediyoruz.
            if (resimler[0] != null)
            {
                // Birden fazla resim yükleme işlemi için yeni bir stream listesi ve resim uzantıları için yeni bir string listesi oluşturuyoruz.
                List<Stream> stResimListesi = new List<Stream>();
                List<string> resimuzantilari = new List<string>(); // Eğer seçili resimler ortak uzantı ise bunu oluşturmanıza gerek yok.

                // Seçilen resimleri döngüye sokup tek tek stream listesine ve uzantı listesine ekliyoruz.
                foreach (var item in resimler)
                {
                    stResimListesi.Add(item.OpenReadStream());
                    resimuzantilari.Add(item.ContentType.Split('/')[1]);
                }

                // Eğer seçili resimlerin hepsi aynı uzantıda ise bu işlemi kullanabilirsiniz.
                var dosyaisimleri1 = Muavin.CokluDosyaEkle(stResimListesi, ".jpg", _hostingEnvironment + "/images/");

                // Eğer seçili resimlerin uzantıları birbirinden farklı ise işlemi uygulayabilirsiniz.
                var dosyaisimleri2 = Muavin.CokluDosyaEkle(stResimListesi, resimuzantilari, _hostingEnvironment + "/images/");

                // stResimListesi, seçili resimlerin stream listesi
                // ".jpg", eğer ortak uzantı ise tek bir uzantı adı yazabilirsiniz.
                // resimuzantilari, eğer uzantılar farklı ise string listesi halinde verebilirsiniz.
                // _hostingEnvironment + "/images/" methodu ile de resmin yüklemesini istediğimiz klasörün dosya yolu.
            }

            return View();
        }

        public IActionResult MailGonderIslemi()
        {
            // Öncelikle MuavinCode'dan türüyen KaynakMail sınıfı oluşturuyoruz. Bu sınıfın amacı mail gönderimi yapılacak e-mail adresinin giriş bilgilerini yazmak. İsterseniz bunu bir sınıf içerisinde tanımlayıp farklı sayfalarda da kullanabilirsiniz.
            KaynakMail km = new KaynakMail
            {
                girisemail = "gonderici@kadirocsoy.com",// Gönderim yapılacak e-mail adresi.
                girissifresi = "Deneme123",             // E-mail adresinin şifresi.
                hostadres = "mail.kadirocsoy.com",      // E-mail adresinin giriş yapıldığı host adresi.
                port = 587,                             // Port adresi. Genellikle 587 oluyor.
                kimdenisim = "Abdulkadir Öçsoy",        // Gönderici adı soyadı.
                kimdenmailadres = "info@kadirocsoy.com" // Gönderici e-mail adresi.
            };

            var sonuc1 = Muavin.MailGonder(km, "E-Mail Konusu", "E-Mail İçeriği", "alici1@kadirocsoy.com");
            var sonuc2 = Muavin.MailGonder(km, "E-Mail Konusu", "E-Mail İçeriği", "alici1@kadirocsoy.com", "alici2@kadirocsoy.com");
            var sonuc3 = Muavin.MailGonder(km, "E-Mail Konusu", "E-Mail İçeriği", "alici1@kadirocsoy.com", "alici2@kadirocsoy.com", "alici3@kadirocsoy.com");
            // E-Mail İçeriğinden sonra bir veya birden fazla e-mail adresi ekleyip daha fazla kişiye mail gönderebilirsiniz.
            // E-mail gönderim işlemi başarılı sonuçlandığında geriye true değerini başarısız olduğunda ise false değerini döndürür.

            return View();
        }

        public IActionResult ResimOrjinYonuveBoyutaDonusturIslemi()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ResimOrjinYonuveBoyutaDonustur(IFormFile resim)
        {
            var img = Muavin.ResimOrjinYonu(resim.OpenReadStream());
            // resim.OpenReadStream(), resim dosyasının stream formatı

            // Geriye Image tipinde resminizi döndürür.

            var dosyaboyutu = Muavin.BoyutaDonustur(resim.Length);
            //Geriye örneğin 786.5KB, 10.2MB gibi değerler döndürür.

            return View();
        }

        public IActionResult ListeDonustur()
        {
            DataTable dt = new DataTable(); // Tabi bu kısma yeni bir DataTable değil, select sorgusu sonucu verileriniz gelecek.

            var ogrenciler = Muavin.ListeDonustur<Ogrenci>(dt);
            // '<' ve '>' işaretleri arasındaki Ogrenci kısım DataTable'dan gelen verilerinizin dönüştürülmesini istediğiniz sınıf.
            // dt, DataTable'daki verileriniz.

            //İşlem başarılı olduğunda List<Ogrenci> tipinde öğrenci listesini size döndürecektir.

            return View();
        }
    }
}

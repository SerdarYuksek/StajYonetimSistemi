using FluentValidation;
using UserService.Api.Model;

namespace UserService.Api.ValidationRules
{
    public class UserRegisterValidation : AbstractValidator<AppUser>
    {
        //Sisteme kayıt işlemi için kullanıcıların girdiği bilgileri Fluent Validation küfüphanesi ile kurallar ve hata mesajlarının tanımlanması
        public UserRegisterValidation()
        {

            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Ad Boş Geçilemez!");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Soyad Boş Geçilemez!");
            RuleFor(x => x.TCNO).NotEmpty().WithMessage("TC Kimlik Numarası Boş Geçilemez!");
            RuleFor(x => x.StudentNo).NotEmpty().WithMessage("Öğrenci No Boş Geçilemez");
            RuleFor(x => x.PersonalNo).NotEmpty().WithMessage("Personel No Boş Geçilemez");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Telefon numarası boş Geçilemez!");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Mail adresi Boş Geçilemez!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre Boş Geçilemez!");
            RuleFor(x => x.RePassword).NotEmpty().WithMessage("Şifre Tekrar Boş Geçilemez!");

            RuleFor(x => x.FirstName).MaximumLength(15).WithMessage("Maximum 15 Karakter Giriniz!");
            RuleFor(x => x.FirstName).MinimumLength(2).WithMessage("Minimum 2 Karakter Giriniz!");

            RuleFor(x => x.Surname).MaximumLength(15).WithMessage("Maximum 15 Karakter Giriniz!");
            RuleFor(x => x.Surname).MinimumLength(2).WithMessage("Mininmum 2 Karakter Giriniz!");

            RuleFor(x => x.TCNO).Length(11).WithMessage("TC kimlik numarası 11 karakterden oluşmalıdır!");
            RuleFor(x => x.TCNO).Matches("^[0-9]*$").WithMessage("TC Kimlik numarası sadece rakamlardan(0-9) oluşmalıdır!");
            RuleFor(x => x.TCNO).Matches("^[1-9]*$").WithMessage("TC Kimlik numarası 0 ile başlayamaz!");

            RuleFor(x => x.StudentNo).Length(10).WithMessage("Öğrenci Numarası 10 karakterden oluşmalıdır!");
            RuleFor(x => x.StudentNo).Matches("^[0-9]*$").WithMessage("Öğrenci numarası sadece rakamlardan(0-9) oluşmalıdır!");
            RuleFor(x => x.StudentNo).Matches("^[1-9]*$").WithMessage("Öğrenci numarası 0 ile başlayamaz!");

            RuleFor(x => x.PersonalNo).Length(10).WithMessage("Personel No 10 karakterden oluşmalıdır!");
            RuleFor(x => x.PersonalNo).Matches("^[0-9]*$").WithMessage("Personel numarası sadece rakamlardan(0-9) oluşmalıdır!");
            RuleFor(x => x.PersonalNo).Matches("^[1-9]*$").WithMessage("Personel numarası 0 ile başlayamaz!");

            RuleFor(x => x.Gender).Must(g => g == true || g == false).WithMessage("Lütfen Bir Cinsiyet Seçiniz!");

            RuleFor(x => x.Email).MaximumLength(30).WithMessage("Mailiniz Maximum 30 Karakter İçermelidir!");
            RuleFor(x => x.Email).MinimumLength(15).WithMessage("Mailiniz Mininmum 15 Karakter İçermelidir!");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(x => x.PhoneNumber).Length(10).WithMessage("Başında 0 olmadan 10 haneli numaranızı giriniz!");
            RuleFor(x => x.PhoneNumber).Matches("^[0-9]*$").WithMessage("Telefon numarası sadece rakamlardan(0-9) oluşmalıdır!");
            RuleFor(x => x.PhoneNumber).Matches(@"^\+90[0-9]{10}$").WithMessage("Lütfen geçerli bir telefon numarası giriniz!");

            RuleFor(x => x.Class).Must(c => c == "1ndGrade" || c == "2ndGrade" || c == "3rdGrade" || c == "4tGrade" ||
            c == "GraduateStudies").WithMessage("Lütfen bir sınıf bilgisi seçiniz!");

            RuleFor(x => x.Title).Must(t => t == "Prof.Dr." || t == "Assoc.Prof.Dr." ||
            t == "Dr.Res.Asst." || t == "Lecturer" || t == "Research Asst.").WithMessage("Lütfen bir ünvan bilgisi seçiniz!");

            RuleFor(x => x.Password).MinimumLength(8).WithMessage("Minimum 8 Karakter Giriniz!");
            RuleFor(x => x.Password).MaximumLength(20).WithMessage("Maximum 20 Karakter Giriniz!");
            RuleFor(x => x.Password).Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")
            .WithMessage("Şifre en az bir küçük harf, bir büyük harf, bir sayı ve bir özel karakter içermelidir.");

            RuleFor(x => x.RePassword).Equal(x => x.Password).WithMessage("Şifreler eşleşmiyor!");

        }
    }
}

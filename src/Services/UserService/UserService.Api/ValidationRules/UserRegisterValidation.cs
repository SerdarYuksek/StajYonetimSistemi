using FluentValidation;
using UserService.Api.Model;

namespace UserService.Api.ValidationRules
{
    public class UserRegisterValidation : AbstractValidator<AppUser>
    {
        //Sisteme kayıt işlemi için kullanıcıların girdiği bilgileri Fluent Validation küfüphanesi ile kurallar ve hata mesajlarının tanımlanması
        public UserRegisterValidation()
        {
            //                            ---------Student------------

            RuleFor(x => x.StudentData.FirstName).NotEmpty().WithMessage("Ad Boş Geçilemez!");
            RuleFor(x => x.StudentData.Surname).NotEmpty().WithMessage("Soyad Boş Geçilemez!");
            RuleFor(x => x.StudentData.TCNO).NotEmpty().WithMessage("TC Kimlik Numarası Boş Geçilemez!");
            RuleFor(x => x.StudentData.StudentNo).NotEmpty().WithMessage("Öğrenci No Boş Geçilemez");
            RuleFor(x => x.StudentData.PhoneNumber).NotEmpty().WithMessage("Telefon numarası boş Geçilemez!");
            RuleFor(x => x.StudentData.Email).NotEmpty().WithMessage("Mail adresi Boş Geçilemez!");
            RuleFor(x => x.StudentData.Password).NotEmpty().WithMessage("Şifre Boş Geçilemez!");
            RuleFor(x => x.StudentData.RePassword).NotEmpty().WithMessage("Şifre Tekrar Boş Geçilemez!");

            RuleFor(x => x.StudentData.FirstName).MaximumLength(15).WithMessage("Maximum 15 Karakter Giriniz!");
            RuleFor(x => x.StudentData.FirstName).MinimumLength(2).WithMessage("Minimum 2 Karakter Giriniz!");

            RuleFor(x => x.StudentData.Surname).MaximumLength(15).WithMessage("Maximum 15 Karakter Giriniz!");
            RuleFor(x => x.StudentData.Surname).MinimumLength(2).WithMessage("Mininmum 2 Karakter Giriniz!");

            RuleFor(x => x.StudentData.TCNO).Length(11).WithMessage("TC kimlik numarası 11 karakterden oluşmalıdır!");
            RuleFor(x => x.StudentData.TCNO).Matches("^[0-9]*$").WithMessage("TC Kimlik numarası sadece rakamlardan(0-9) oluşmalıdır!");
            RuleFor(x => x.StudentData.TCNO).Matches("^[1-9]*$").WithMessage("TC Kimlik numarası 0 ile başlayamaz!");

            RuleFor(x => x.StudentData.StudentNo).Length(10).WithMessage("Öğrenci Numarası 10 karakterden oluşmalıdır!");
            RuleFor(x => x.StudentData.StudentNo).Matches("^[0-9]*$").WithMessage("Öğrenci numarası sadece rakamlardan(0-9) oluşmalıdır!");
            RuleFor(x => x.StudentData.StudentNo).Matches("^[1-9]*$").WithMessage("Öğrenci numarası 0 ile başlayamaz!");

            RuleFor(x => x.StudentData.Gender).Must(g => g == true || g == false).WithMessage("Lütfen Bir Cinsiyet Seçiniz!");

            RuleFor(x => x.StudentData.Email).MaximumLength(30).WithMessage("Mailiniz Maximum 30 Karakter İçermelidir!");
            RuleFor(x => x.StudentData.Email).MinimumLength(15).WithMessage("Mailiniz Mininmum 15 Karakter İçermelidir!");
            RuleFor(x => x.StudentData.Email).EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(x => x.StudentData.PhoneNumber).Length(10).WithMessage("Başında 0 olmadan 10 haneli numaranızı giriniz!");
            RuleFor(x => x.StudentData.PhoneNumber).Matches("^[0-9]*$").WithMessage("Telefon numarası sadece rakamlardan(0-9) oluşmalıdır!");
            RuleFor(x => x.StudentData.PhoneNumber).Matches(@"^\+90[0-9]{10}$").WithMessage("Lütfen geçerli bir telefon numarası giriniz!");

            RuleFor(x => x.StudentData.Class).Must(c => c == "1ndGrade" || c == "2ndGrade" || c == "3rdGrade"|| c == "4tGrade" ||
            c == "GraduateStudies").WithMessage("Lütfen bir sınıf bilgisi seçiniz!");

            RuleFor(x => x.StudentData.Password).MinimumLength(8).WithMessage("Minimum 8 Karakter Giriniz!");
            RuleFor(x => x.StudentData.Password).MaximumLength(20).WithMessage("Maximum 20 Karakter Giriniz!");
            RuleFor(x => x.StudentData.Password).Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")
            .WithMessage("Şifre en az bir küçük harf, bir büyük harf, bir sayı ve bir özel karakter içermelidir.");

            RuleFor(x => x.StudentData.RePassword).Equal(x => x.StudentData.Password).WithMessage("Şifreler eşleşmiyor!");


            //                                  ---------Personel------------

            RuleFor(x => x.PersonalData.FirstName).NotEmpty().WithMessage("Ad Boş Geçilemez!");
            RuleFor(x => x.PersonalData.Surname).NotEmpty().WithMessage("Soyad Boş Geçilemez!");
            RuleFor(x => x.PersonalData.TCNO).NotEmpty().WithMessage("TC Kimlik Numarası Boş Geçilemez!");
            RuleFor(x => x.PersonalData.PersonalNo).NotEmpty().WithMessage("Personel No Boş Geçilemez");
            RuleFor(x => x.PersonalData.PhoneNumber).NotEmpty().WithMessage("Telefon numarası boş Geçilemez!");
            RuleFor(x => x.PersonalData.Email).NotEmpty().WithMessage("Mail adresi Boş Geçilemez!");
            RuleFor(x => x.PersonalData.Password).NotEmpty().WithMessage("Şifre Boş Geçilemez!");
            RuleFor(x => x.PersonalData.RePassword).NotEmpty().WithMessage("Şifre Tekrar Boş Geçilemez!");

            RuleFor(x => x.PersonalData.FirstName).MaximumLength(15).WithMessage("Maximum 15 Karakter Giriniz!");
            RuleFor(x => x.PersonalData.FirstName).MinimumLength(2).WithMessage("Minimum 2 Karakter Giriniz!");

            RuleFor(x => x.PersonalData.Surname).MaximumLength(15).WithMessage("Maximum 15 Karakter Giriniz!");
            RuleFor(x => x.PersonalData.Surname).MinimumLength(2).WithMessage("Mininmum 2 Karakter Giriniz!");

            RuleFor(x => x.PersonalData.TCNO).Length(11).WithMessage("TC kimlik numarası 11 karakterden oluşmalıdır!");
            RuleFor(x => x.PersonalData.TCNO).Matches("^[0-9]*$").WithMessage("TC Kimlik numarası sadece rakamlardan(0-9) oluşmalıdır!");
            RuleFor(x => x.PersonalData.TCNO).Matches("^[1-9]*$").WithMessage("TC Kimlik numarası 0 ile başlayamaz!");

            RuleFor(x => x.PersonalData.PersonalNo).Length(10).WithMessage("Personel No 10 karakterden oluşmalıdır!");
            RuleFor(x => x.PersonalData.PersonalNo).Matches("^[0-9]*$").WithMessage("Personel numarası sadece rakamlardan(0-9) oluşmalıdır!");
            RuleFor(x => x.PersonalData.PersonalNo).Matches("^[1-9]*$").WithMessage("Personel numarası 0 ile başlayamaz!");

            RuleFor(x => x.PersonalData.Title).Must(t => t == "Prof.Dr." || t == "Assoc.Prof.Dr." ||
            t == "Dr.Res.Asst." || t == "Lecturer" || t == "Research Asst.").WithMessage("Lütfen bir ünvan bilgisi seçiniz!");

            RuleFor(x => x.PersonalData.Gender).Must(g => g == true || g == false).WithMessage("Lütfen Bir Cinsiyet Seçiniz!");

            RuleFor(x => x.PersonalData.Email).MaximumLength(30).WithMessage("Maximum 30 Karakter İçermelidir!");
            RuleFor(x => x.PersonalData.Email).MinimumLength(15).WithMessage("Mininmum 15 Karakter İçermelidir!");
            RuleFor(x => x.PersonalData.Email).EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(x => x.PersonalData.PhoneNumber).Length(10).WithMessage("Başında 0 olmadan 10 haneli numaranızı giriniz!");
            RuleFor(x => x.PersonalData.PhoneNumber).Matches("^[0-9]*$").WithMessage("Telefon numarası sadece rakamlardan(0-9) oluşmalıdır!");
            RuleFor(x => x.PersonalData.PhoneNumber).Matches(@"^\+90[0-9]{10}$").WithMessage("Lütfen geçerli bir telefon numarası giriniz!");

            RuleFor(x => x.PersonalData.Password).MinimumLength(8).WithMessage("Minimum 8 Karakter Giriniz!");
            RuleFor(x => x.PersonalData.Password).MaximumLength(20).WithMessage("Maximum 20 Karakter Giriniz!");
            RuleFor(x => x.PersonalData.Password).Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")
            .WithMessage("Şifre en az bir küçük harf, bir büyük harf, bir sayı ve bir özel karakter içermelidir.");

            RuleFor(x => x.PersonalData.RePassword).Equal(x => x.StudentData.Password).WithMessage("Şifreler eşleşmiyor!");


        }
    }
}

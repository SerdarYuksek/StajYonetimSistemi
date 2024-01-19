namespace StajYonetimGUI.Models.User
{
    //Sistem giriş yapmak için gerekli olan entityler
    public class LoginViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string StudentNo { get; set; }
        public string PersonalNo { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
    }
}

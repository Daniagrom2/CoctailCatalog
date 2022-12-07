using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;
using BLL.DTO;
using BLL.Mail;
using BLL.Module;
using BLL.Service;
using Ninject;
using UI.Infrastructure;
using UI.View;

namespace UI.ViewModel
{
    public class CoctailViewModel:BaseNotify
    {
        private Mail mail;
        private CoctailDTO _coctail;
        private CoctailService _coctailService;
        private RelayCommand _mailCommand;
        private RelayCommand _sendCommand;
        private RelayCommand _cancelCommand;
        private RelayCommand _favoriteCommand;
        private MailForm _form;
        private string _email;

        public CoctailViewModel(CoctailService coctailService)
        {
            _coctailService = coctailService;
            _coctail = TransferCoctailDTO.Data;
        }
        public CoctailDTO Coctail
        {
            get => _coctail;
            set
            {
                _coctail = value;
                Notify();
            }
        }

   

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                Notify();
            }
        }
        public ICommand FavoriteCommand => _favoriteCommand ?? (new RelayCommand(param =>
        {


            //if (TransferCoctailDTO.Data.Favorite == false)
            //{
            //    TransferCoctailDTO.Data.Favorite = true;

            //    _coctailService.Update(b);
            //}
            //else
            //{
            //    TransferCoctailDTO.Data.Favorite = false;
            //    _coctailService.Update(b);
            //}

        }));
        public ICommand MailCommand => _mailCommand ?? (new RelayCommand(param =>
        {
          _form = new MailForm();
            _form.ShowDialog();

        }));
        public ICommand Send => _sendCommand ?? (new RelayCommand(param =>
        {
            ((MailForm)param).Close();
            mail = new Mail(Email);
            string s = System.IO.File.ReadAllText(@"C:\Users\Danyil\source\repos\Internet\_NETFinalProject\BLL\Mail\index.html");
            string text = $@"
<body><div id=""demobox"" style=""background - color: #352f44 ; width: 400px; height: 400px; border: 1px;"">
                <div>
                <img style = ""width: 200px; height: 200px; padding-top: 40px; padding-left: 100px;
            "" src=""{Coctail.Image}"">
                </div>
                <div style = ""color: #dbd8e3;


            padding - right: 30px;
            padding - bottom: 0px;
            padding - left: 145px;
            "">
                <h3>{Coctail.Name}</h3>
       
                </div>
       
                <div style = ""color: #dbd8e3;padding-top: 0px;
            padding - right: 30px;
            padding - bottom: 50px;
            padding - left: 80px; "">{Coctail.Instructions}</div>


                </div>
                </body>
                </html> ";
            StringBuilder b = new StringBuilder(s);
            b.Append(text);
            s = b.ToString();
            mail.IsBodyHtml = Regex.IsMatch(s, @"<\s*([^ >]+)[^>]*>.*?<\s*/\s*\1\s*>"); ;
            mail.Subject = "Coctail";
            mail.Body = s;

            mail.SendMassage();
            
        }));
        public ICommand CancelCommand => _cancelCommand ?? (new RelayCommand(param =>
        {
            ((MailForm)param).Close();

        }));


        public string Ingridients
        {
            get
            {
                return String.Join(", ", Coctail.Ingridients);
            }
        }

        public CoctailViewModel()
        {
            Coctail = TransferCoctailDTO.Data;
            
        }

        
    }
}
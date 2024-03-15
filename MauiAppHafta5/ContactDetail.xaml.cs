using MauiAppHafta5.Model;

namespace MauiAppHafta5
{
    public partial class ContactDetail : ContentPage
    {
        public Action<MContact> AddMethod = null;
        MContact kisi;
        public ContactDetail(MContact kisi = null)
        {
            InitializeComponent();

            if(kisi != null)
            {
                BindingContext = kisi;
            }
        }

        private void OkClicked(object sender, EventArgs e)
        {
            if (kisi == null)
            {
                kisi = new MContact()
                {
                    Name = txtAd.Text,
                    Surname = txtSoyad.Text,
                    Phone = txtTel.Text,
                    Email = txtMail.Text,
                    Image = "user.png",
                };

                if (AddMethod != null)
                {
                    AddMethod(kisi);
                }
            }

            Navigation.PopModalAsync();
        }

        private void CancelClicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}

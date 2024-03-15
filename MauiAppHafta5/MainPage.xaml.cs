using MauiAppHafta5.Model;

using System.Collections.ObjectModel;
using System.Text.Json;

namespace MauiAppHafta5
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //contacts.Sort();

            if (File.Exists(Filepath))
            {
                var jsonData = File.ReadAllText(Filepath);
                contacts = JsonSerializer.Deserialize<ObservableCollection<MContact>>(jsonData);
            }

            listKisiler.ItemsSource = contacts;
        }

        ObservableCollection<MContact> contacts = new ObservableCollection<MContact> {
            new MContact() { Name = "Ali", Surname="Kara", Email="alikara@mail.com", Phone="0597 987 65 32", Image="user.png" },
            new MContact() { Name = "Ayşe", Surname="Sarı", Email="aysesari@mail.com", Phone="0589 987 56 32", Image="user.png" },
            new MContact() { Name = "Ahmet", Surname="Beyaz", Email="ahmetbeyaz@mail.com", Phone="0577 555 56 32", Image="user.png" },
            new MContact() { Name = "Mehmet", Surname="Siyah", Email="mehmetsiyah@mail.com", Phone="0577 555 56 32", Image="user.png" },
            new MContact() { Name = "Oya", Surname="Yeşil", Email="oyayesil@mail.com", Phone="0577 555 56 32", Image="user.png" },
            new MContact() { Name = "Fatma", Surname="Kırmızı", Email="fatmakirmizi@mail.com", Phone="0577 555 56 32", Image="user.png" },


        };

        string Filepath = Path.Combine(FileSystem.Current.AppDataDirectory ,"contacts.json");
        private async void BtnAddClicked(object sender, EventArgs e)
        {
            ContactDetail page = new ContactDetail()
            {
                Title= "Kişi Ekle",
                AddMethod = AddContact
                
            };

            await Navigation.PushModalAsync(page);
        }

        void AddContact(MContact contact)
        {
            contacts.Add(contact);
        }

        private async void BtnShareClicked(object sender, EventArgs e)
        {
            var file = new ShareFileRequest()
            {
                Title = "Kişiler",
                File = new ShareFile(Filepath)
            };

            await Share.RequestAsync(file);
        }

        private void BtnSaveClicked(object sender, EventArgs e)
        {
            var jsondata = JsonSerializer.Serialize(contacts);
            File.WriteAllText(Filepath, jsondata);
        }

        private async void BtnDeleteClicked(object sender, EventArgs e)
        {
            var m = sender as MenuItem;
            var contact = contacts.First(o=>o.ID ==m.CommandParameter.ToString() );

            bool b = await DisplayAlert("Silmeyi Onayla", $"{contact.FullName} kişisi silinsin mi?", "Evet", "Hayır");

            if (b)
            {
                contacts.Remove(contact);

            }

        }

        private async void BtnEditClicked(object sender, EventArgs e)
        {
            var m = sender as MenuItem;
            var contact = contacts.First(o=>o.ID ==m.CommandParameter.ToString() );

            ContactDetail page = new ContactDetail(contact)
            {
                Title = "Kişi Düzenle",
            };

            await Navigation.PushModalAsync(page);

        }

        private async void BtnAddImageClicked(object sender, EventArgs e)
        {
            var m = sender as MenuItem;
            var contact = contacts.First(o=>o.ID ==m.CommandParameter.ToString() );

            var res = await DisplayActionSheet("Resim Seç:", "Galeri", "Kamera");
            if(res == "Galeri")
            {
                var resim = await MediaPicker.Default.PickPhotoAsync();
                contact.Image = resim.FullPath;
            }
            else if(res == "Kamera")
            {
                var resim = await MediaPicker.Default.CapturePhotoAsync();
                if(resim != null)
                {
                    contact.Image = resim.FullPath;
                }
            }
        }
    }

}

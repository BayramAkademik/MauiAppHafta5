using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppHafta5.Model
{
    public class MContact : INotifyPropertyChanged
    {
        public MContact() { }
        string id,name,surname,phone,email, image;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName]string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public string ID
        {
            get
            {
                if (id == null)
                    id = Guid.NewGuid().ToString();
                return id;
            }
            set { id = value; }
        }


        public string Name
        {
            get { return name; }
            set { name = value; 
                NotifyPropertyChanged(); 
                NotifyPropertyChanged("FullName");
            }
        }

        public string Surname
        {
            get { return surname; }
            set { surname = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("FullName");
            }
        }

        public string FullName => name + " " + surname;

        public string Phone
        {
            get { return phone; }
            set { phone = value; 
                NotifyPropertyChanged();
            }
        }

        public string Email
        {
            get { return email; }
            set { email = value; 
                NotifyPropertyChanged();
            }
        }

        public string Image
        {
            get { return image; }
            set { image = value; 
                NotifyPropertyChanged();
            }
        }

    }

}

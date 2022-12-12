using System;

namespace ContactMicroservice.Dtos
{
    public class PhoneBookItemAddDto
    {
        private string _name;
        private string _surname;
        private string _firm;
        private string _phone;
        private string _mail;
        private string _country;
        private string _city;

        public Guid Guid { get; set; }
        public string Name 
        {
            get
            {
                return _name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _name = string.Empty;
                }

                _name = value.Trim();
            }
        }

        public string Surname 
        {
            get
            {
                return _surname;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _surname = string.Empty;
                }

                _surname = value.Trim();
            }
        }

        public string Firm
        {
            get
            {
                return _firm;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _firm = string.Empty;
                }

                _firm = value.Trim();
            }
        }

        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _phone = string.Empty;
                }

                _phone = value.Trim();
            }
        }

        public string Mail
        {
            get
            {
                return _mail;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _mail = string.Empty;
                }

                _mail = value.Trim();
            }
        }

        public string Country
        {
            get
            {
                return _country;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _country = string.Empty;
                }

                _country = value.Trim();
            }
        }

        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _city = string.Empty;
                }

                _city = value.Trim();
            }
        }
    }
}

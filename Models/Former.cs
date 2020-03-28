using System;
using System.ComponentModel.DataAnnotations;

namespace Enstitute
{
 
    public class Former : User
    {
        [StringLength(30)]
        public string RegistrationNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        public Former(){}
        public Former ( string username, string password, string first_name, string last_name, 
            int year, int month, int day, Gender gender, Address address, string phone, string email,
            string nic, string Id, string registrationNumber ) { 
            Username = username;
            FirstName = first_name;
            LastName = last_name;
            BirthDate = new DateTime(year, month, day).Date;
            Gender = gender;
            GenderID = gender.ID;
            Address = address;
            AddressID = address.ID;
            Phone = phone;
            Email = email;
            NIC = nic;
            SetPassword(password);
            is_former = true;
            is_secretary = false;
            is_director = false;
            is_actif = true;
            is_staff = false;
            RegistrationNumber = registrationNumber;
            ID = Id;
        }
    }
}

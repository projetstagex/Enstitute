using System;
using System.ComponentModel.DataAnnotations;

namespace Enstitute
{
 
    public class Director : User
    {
        [StringLength(15)]
        public string RegistrationNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }

        public Director(){}
        public Director ( string username, string password, string first_name, string last_name, 
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
            is_former = false;
            is_secretary = false;
            is_director = true;
            is_actif = true;
            is_staff = false;
            RegistrationNumber = registrationNumber;
            ID = Id;
        }
    }
}

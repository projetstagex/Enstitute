using System;

namespace Enstitute
{
 
    public class Staff : User
    {
        public Staff(){}
        public Staff ( string username, string password, string first_name, string last_name, 
            int year, int month, int day, Gender gender, Address address, string phone, string email,
            string nic, string Id ) { 
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
            is_director = false;
            is_actif = true;
            is_staff = true;
            ID = Id;
        }
    }
}

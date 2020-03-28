using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enstitute
{
    public class Student : User 
    {
        [StringLength(30)]
        public string InscriptionNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string Nationality { get; set; }

        [Required]
        public bool Is_training { get; set; }

        [ForeignKey("Group")]
        public int GroupID { get; set; }
        public Group Group { get; set; }

        public string GetAbsoluteUrl(){
            return "Students/Details/?SID="+InscriptionNumber;
        }

        public Student(){}
        public Student ( string username, string password, string first_name, string last_name, 
            int year, int month, int day, Gender gender, Address address, string phone, string email,
            string nic, Group group, string nationality, string Id, string inscriptionNumber ) { 
            Username = username;
            FirstName = first_name;
            LastName = last_name;
            Nationality = nationality;
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
            is_actif = false;
            is_staff = false;
            Is_training = true;
            Group = group;
            GroupID = group.ID;
            InscriptionNumber = inscriptionNumber;
            ID = Id;
        }

    }
}

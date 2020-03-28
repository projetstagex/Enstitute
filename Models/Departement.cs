﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Enstitute
{
    public class Departement
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Label { get; set; }

         public Departement(){
        }
        public Departement(string label){
            Label = label;
        }
    }
}

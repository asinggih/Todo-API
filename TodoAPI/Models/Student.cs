using System;
using System.ComponentModel.DataAnnotations;

namespace TodoAPI.Models {

    public class Student {

        public long Id { get; set; }
        public string f_name { get; set; }
        public string l_name { get; set; }

        [DataType(DataType.Date)]
        public DateTime dob { get; set; }

    }
}

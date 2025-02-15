﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ReadersAndRent.Model
{
    public class Rent
    {
        [Key]
        public int ID_History { get; set; }

        [DataType(DataType.Date)]
        public required DateTime Date_Start { get; set; }
        public DateTime? Date_End { get; set; }
        public int Srok { get; set; }
        public int ID_Book { get; set; }

        public int ID_Reader { get; set; }
        [ForeignKey("ID_Reader")]
        public virtual Readers Reader { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Interfaces
{
    public class Vote : BaseEntity
    {
        public Vote()
        {

        }

        public int Id { get; set; }
        public string country { get; set; }
        public int IdUser { get; set; }
    }
}

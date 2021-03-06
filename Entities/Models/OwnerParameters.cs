using System;

namespace Entities.Models
{
    public class OwnerParameters : QueryStringParameters
    {
        public uint MinYearOfBirth { get; set; }
        public uint MaxYearOfBirth { get; set; } = (uint)DateTime.Now.Year;
        public bool ValidYearRange => MaxYearOfBirth > MinYearOfBirth;

        public string Name { get; set; }

        public OwnerParameters()
        {
            OrderBy = "name";
        }
    }

}


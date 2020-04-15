using System;
using System.Collections.Generic;
using System.Text;

namespace ReportViewer.Domain.Models
{
    public class Organization
    {
        protected Organization() { }

        public Organization(string name, string ownerSub)
        {
            Name = name;
            OwnerSub = ownerSub;
        }

        public Organization(string name, string ownerSub, string address1, string address2, string phone)
            : this(name, ownerSub)
        {
            Address1 = address1;
            Address2 = address2;
            Phone = phone;
        }

        public Organization(int id, string ownerSub, string name, string address1, string address2, string phone)
            : this(name, ownerSub, address1, address2, phone)
        {
            Id = id;
        }

        public int Id { get; }
        public string Name { get; }
        public string Address1 { get; }
        public string Address2 { get; }
        public string Phone { get; }
        public string OwnerSub { get; }
    }
}

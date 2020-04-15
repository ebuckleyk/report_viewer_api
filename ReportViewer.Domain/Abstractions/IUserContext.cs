using System;
using System.Collections.Generic;
using System.Text;

namespace ReportViewer.Domain.Abstractions
{
    public interface IUserContext
    {
        public string Sub { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Name { get; }
        public string Email { get; }
        public string Phone { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProjects.Shared.ViewModels.Customer
{
    public class CustomerVM
    {
        public CustomerVM()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Mobile = string.Empty;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
    }
}

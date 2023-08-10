using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Shared.ViewModels.Product
{
    public class RelatedProductVM
    {
        public int Id { get; set; }

        public int ProductId1 { get; set; }

        public string ProductName1 { get; set; }

        public int ProductId2 { get; set; }

        public string ProductName2 { get; set; }

        public int DisplayOrder { get; set; }
    }
}

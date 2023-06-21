using SampleProjects.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Shared.Dtos.Product
{
    public class RelatedProductDtoForCreateOrUpdate
    {
        public int ProductId1 { get; set; }
        public int ProductId2 { get; set; }
        public int DisplayOrder { get; set; }
    }
}

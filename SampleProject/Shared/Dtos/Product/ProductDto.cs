﻿namespace SampleProject.Shared.Dtos.Product
{
    public class ProductDto
    {
        public ProductDto()
        {
            Name = string.Empty;
            ShortDescription = string.Empty;
            FullDescription = string.Empty;
            AdminComment = string.Empty;
            ShowOnHomepage = true;
            Enable = true;
            Deleted = false;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string AdminComment { get; set; }
        public bool ShowOnHomepage { get; set; }
        public bool Enable { get; set; }
        public bool Deleted { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public ICollection<RelatedProductDto> RelatedProductDtos { get; set; }
    }
}

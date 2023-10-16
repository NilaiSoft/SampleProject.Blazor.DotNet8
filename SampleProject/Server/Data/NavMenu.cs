namespace SampleProject.Server.Data
{
    public partial class NavMenu : BaseEntity
    {
        public NavMenu()
        {
            Name = string.Empty;
            Url = string.Empty;
            IsTitle = true;
            IconComponent_Name = string.Empty;
            BadgeColor = string.Empty;
            BadgeText = string.Empty;
            LinkProps_Fragment = string.Empty;
            ParentId = new int();
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string IconComponent_Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BadgeColor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BadgeText { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LinkProps_Fragment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ParentId { get; set; }
    }
}

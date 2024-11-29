using System;
using System.Collections.Generic;

namespace NorthwindRestApi.Models
{
    public partial class Documentation
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public int DocumentationId { get; set; }
        /// <summary>
        /// URI
        /// </summary>
        public string? AvailableRoute { get; set; }
        /// <summary>
        /// REST-metodi
        /// </summary>
        public string? Method { get; set; }
        /// <summary>
        /// Selitys
        /// </summary>
        public string? Description { get; set; }
    }
}

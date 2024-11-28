// Updated Model with Validations
namespace Library_Management_System_.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public partial class tblBook
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public string CategoryType { get; set; }

        [Required(ErrorMessage = "Author Name is required.")]
        public string AuthorName { get; set; }

        [Required(ErrorMessage = "Publication Name is required.")]
        public string PublicationName { get; set; }

        [Required(ErrorMessage = "ISBN is required.")]
        [RegularExpression(@"^\d{3}-\d{2}-\d{5}-\d{2}-\d{1}$", ErrorMessage = "Invalid ISBN format. Example: 978-92-95055-02-5")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "Created By is required.")]
        public string CreatedBy { get; set; }

        public System.DateTime CreatedDate { get; set; }
    }
}
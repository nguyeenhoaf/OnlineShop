using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TDShop.Web.Models
{
    public class FeedbackViewModel
    {
        
        public int ID { set; get; }

        [MaxLength(250, ErrorMessage ="Không được vượt quá 250 kí tự")]
        [Required(ErrorMessage ="Phải nhập tên")]
        public string Name { set; get; }

        [MaxLength(250, ErrorMessage = "Không được vượt quá 250 kí tự")]
        public string Email { set; get; }

        [MaxLength(500, ErrorMessage = "Không được vượt quá 500 kí tự")]
        public string Message { set; get; }

        public DateTime CreatedDate { set; get; }

        [Required(ErrorMessage = "Phải nhập trạng thái")]
        public bool Status { set; get; }

        public ContactDetailViewModel ContactDetail { set; get; }
    }
}
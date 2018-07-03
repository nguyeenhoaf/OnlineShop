using System;

namespace TDShop.Model.Abstract
{
    public interface IAuditable
    {
        DateTime? CreatedDate { get; set; }
        string CreatedBy { get; set; }
        DateTime? UpdatedDate { get; set; }
        string UpdatedBy { get; set; }

        //Seoable
        string MetaKeyword { get; set; }
        string MetaDescription { get; set; }

        //Switchable
        bool Status { get; set; }
    }
}
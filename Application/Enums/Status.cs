using System.ComponentModel.DataAnnotations;

namespace Datas.Enums
{
    public enum Status
    {
        [Display(Name = "Hoạt động")]
        Active = 0,

        [Display(Name = "Không hoạt động")]
        IsActive = 1,
    }
}

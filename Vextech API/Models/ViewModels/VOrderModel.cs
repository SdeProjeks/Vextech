using System.ComponentModel.DataAnnotations;

namespace Vextech_API.Models.ViewModels
{
    public class VOrderModel
    {
        public ulong ID { get; set; }
        [Required]
        [RegularExpression(@"^[a-zæøåA-ZÆØÅ0-9.''-'\s]{1,255}")]
        public string Address { get; set; }
        [Required]
        [RegularExpression(@"
[0-9]{4}                               | #Austria | #Belgium | #Bulgaria | #Cyprus | #Denmark | #Germany | #Hungary | #Luxembourg | #Slovenia
[0-9]{3} [0-9]{2}|[0-9]{5}             | #Czech Republic
[0-9]{2}                               | #Germany
[0-9]{5}                               | #Germany | #Estonia | #Spain | #Finland  | #France | #Italy
[0-9]{3} [0-9]{2}|[0-9]{5}             | #Greece
([ACDEFHKNPRTVWXY]{1}[0-9]{2} [A-Z0-9]{4}|[D]{1}[6]{1}[W]{1} [A-Z0-9]{4})         | #Ireland
[Ll][Tt][- ]{0,1}\[0-9]{5}             | #Lithuania
[Ll][Vv][- ]{0,1}\[0-9]{4}             | #Latvia
[A-Z]{3} [0-9]{4}|[A-Z]{2}[0-9]{2}|[A-Z]{2} [0-9]{2}|[A-Z]{3}[0-9]{4}|[A-Z]{3}[0-9]{2}|[A-Z]{3} [0-9]{2} | #Malta
[0-9]{4} [A-Z]{2}|[0-9]{4}[A-Z]{2}     | #Netherlands
[0-9]{2}[-]([0-9]){3}                  | #Poland
[0-9]{4}((-)[0-9]{3})                  | #Portugal
[0-9]{2,10}                            | #Romania
([0-9]{3} [0-9]{2})                    | #Sweden
([0-9]{3} [0-9]{2})|[0-9]{5}             #Slovakia)")]
        public string PostNumber { get; set; }
        [Required]
        [RegularExpression(@"^[a-zæøåA-ZÆØÅ]{1,200}")]
        public string Country { get; set; }
        public DateTime Date { get; set; }
        public int OrderStatusID { get; set; }
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]{1,20}")]
        public ulong UserID { get; set; }
        public string Email { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
    }
}

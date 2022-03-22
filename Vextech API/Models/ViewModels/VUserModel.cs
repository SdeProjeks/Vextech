using System.ComponentModel.DataAnnotations;

namespace Vextech_API.Models.ViewModels
{
    public class VUserModel
    {
        public ulong ID { get; set; }
        public ulong RoleID { get; set; } = 1;
        public string Name { get; set; }
        public ulong PermissionsID { get; set; }
        public string PermissionName { get; set; }
        public int AddressID { get; set; }
        public string Address { get; set; }
        public int PostNumberID { get; set; }
        public string PostNumber { get; set; }
        public string City { get; set; }
        public int CountryID { get; set; }
        public string Country { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[a-zæøåA-ZÆØÅ''-'\s]{1,40}$", ErrorMessage = "only letters")]
        public string Firstname { get; set; }

        [Required]
        [RegularExpression(@"^[a-zæøåA-ZÆØÅ''-'\s]{1,40}$", ErrorMessage = "only letters")]
        public string Lastname { get; set; }
        
        [Required]
        // hashed password
        [StringLength(256, ErrorMessage = "Password must be between 8 and 256",MinimumLength = 8), DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*?[A-ZÆØÅ])(?=.*?[a-zæøå])(?=.*?[0-9])(?=.*?[!#¤%&?@$+*^]).{8,255}$",
        ErrorMessage = "Password must contain atleast 1 number, 1 upper and lowercase letter and 1 special character")]  
        public string Password { get; set; }

        [RegularExpression(@"^(
(AT)?U[0-9]{8}                              | #Austria
(BE)?0[0-9]{9}                              | #Belgium
(BG)?[0-9]{9,10}                            | #Bulgaria
(CY)?[0-9]{8}L                              | #Cyprus
(CZ)?[0-9]{8,10}                            | #Czech Republic
(DE)?[0-9]{9}                               | #Germany
(DK)?[0-9]{8}                               | #Denmark
(EE)?[0-9]{9}                               | #Estonia
(EL|GR)?[0-9]{9}                            | #Greece
(ES)?[0-9A-Z][0-9]{7}[0-9A-Z]               | #Spain
(FI)?[0-9]{8}                               | #Finland
(FR)?[0-9A-Z]{2}[0-9]{9}                    | #France
(HU)?[0-9]{8}                               | #Hungary
(IE)?[0-9]S[0-9]{5}L                        | #Ireland
(IT)?[0-9]{11}                              | #Italy
(LT)?([0-9]{9}|[0-9]{12})                   | #Lithuania
(LU)?[0-9]{8}                               | #Luxembourg
(LV)?[0-9]{11}                              | #Latvia
(MT)?[0-9]{8}                               | #Malta
(NL)?[0-9]{9}B[0-9]{2}                      | #Netherlands
(PL)?[0-9]{10}                              | #Poland
(PT)?[0-9]{9}                               | #Portugal
(RO)?[0-9]{2,10}                            | #Romania
(SE)?[0-9]{12}                              | #Sweden
(SI)?[0-9]{8}                               | #Slovenia
(SK)?[0-9]{10}                                #Slovakia)$", ErrorMessage = "WRONG!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!")]
        public string VatID { get; set; }
    }
}

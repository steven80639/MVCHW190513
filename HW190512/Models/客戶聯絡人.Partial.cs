namespace HW190512.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            using (var db = new CustomerDBEntities())
                if (this.Id == 0)
                {
                    //Create  同一個客戶下的聯絡人 : c.客戶Id == this.客戶Id
                    if (db.客戶聯絡人.Where(c => c.客戶Id == this.客戶Id && c.Email == this.Email).Any())
                    {
                        yield return new ValidationResult("Email 已存在", new string[] { "Email" });  //new string[] {"Email"} 讓Email報錯而不是顯示在上方
                    }
                }
                else
                {
                    //Update  c.Id != this.Id : 更新時要排除自己再去判斷
                    if (db.客戶聯絡人.Where(c => c.客戶Id == this.客戶Id && c.Id != this.Id && c.Email == this.Email).Any())
                    {
                        yield return new ValidationResult("Email 已存在", new string[] { "Email" });
                    }
                }

            yield return ValidationResult.Success;
        }
    }

    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }
        
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        [StringLength(11, ErrorMessage="手機號碼為10碼")]
        [RegularExpression(@"\d{4}-\d{6}", ErrorMessage = "手機號碼格式必須為：0911-111111")]
        public string 手機 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }
    
        public virtual 客戶資料 客戶資料 { get; set; }
    }
}

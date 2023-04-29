using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PartyAppHw.Models
{
    public class DeliveryRequest
    {
        public int Id { get; set; } = 0;
        ///<summary>
        ///Наименованиетовара
        ///</summary>
        [Required]
        [MinLength(3)]
        [MaxLength(32)]
        [Display(Name = "Наименование товара")]
        public string Name { get; set; }

        ///<summary>
        /// Куда отправить уведомление
        ///</summary>
        [Required]
        [MaxLength(32)]
        [EmailAddress]
        [Display(Name = "Куда отправить уведомление")]
        public string NotificationEmail { get; set; }

        ///<summary>
        /// Телефон для обратной связи
        ///</summary>
        [Required]
        [MaxLength(32)]
        [Phone]
        [Display(Name = "Телефон для обратной связи")]
        public string Telephone { get; set; }

        ///<summary>
        /// Связаться при проблеме
        ///</summary>
        [Required]
        [Display(Name = "Связаться при проблеме?")]
        public bool? CallOnIssue { get; set; }
    }
}
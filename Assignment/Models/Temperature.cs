using System;
using System.ComponentModel.DataAnnotations;

namespace Assignment.Models
{
    public class Temperature
    {
        [Range(35, 45, ErrorMessage = "the temperature must be between 35 and 45")]
        [Required(ErrorMessage = "you must be send temperature")]
        [Display(Name = "Temperature")]
        public float Value { get; set; }

        public static string Check(float entry)
        {
            return entry == 37 ? "You are fine" : "You are sick";
        }
    }
}

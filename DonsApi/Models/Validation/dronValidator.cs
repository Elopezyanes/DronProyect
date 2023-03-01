using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DonsApi.Models
{
    [MetadataType(typeof(dron.MetaData))]
    public partial class dron
    {
        sealed class MetaData
        {
            [Required]
            public string serial_number { get; set; }

            [Required]
            public int id_model { get; set; }

            [RegularExpression("^[0-9]([.,][0-9]{1,3})?$", ErrorMessage = "This field only accepts numbers")]
            [Required]
            public double weight_limit { get; set; }


            [Range(1, 100, ErrorMessage = "The battery level should be between 1 and 100")]
            [Required]
            public int batery_capacity { get; set; }

            [Required]
            public int id_state { get; set; }
        }

    }
}
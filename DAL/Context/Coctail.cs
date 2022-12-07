namespace DAL.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Coctail")]
    public partial class Coctail
    {
        public int CoctailId { get; set; }

        [Required]
        [StringLength(100)]
        public string CoctailName { get; set; }

        [Required]
        [StringLength(100)]
        public string CoctailType { get; set; }

        [Required]
        public string Instructions { get; set; }

        [Required]
        [StringLength(100)]
        public string GlassName { get; set; }

        [Required]
        public string Ingridients { get; set; }

        [Required]
        public string CoctailImage { get; set; }

        public int Favorite { get; set; }

        public virtual Favorite Favorite1 { get; set; }
    }
}

namespace DAL.Context
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Favorite
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FavoriteId { get; set; }

        public virtual Coctail Coctail { get; set; }
    }
}

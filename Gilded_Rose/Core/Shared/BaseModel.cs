using System;
using System.ComponentModel.DataAnnotations;

namespace Gilded_Rose.Core.Shared
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}

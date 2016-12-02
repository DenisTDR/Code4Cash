using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Code4Cash.Data.Models.Entities.Base
{
    public abstract class Entity
    {
        [Index]
        public int Id { get; set; }

        [Index]
        [StringLength(36)]
        public string Selector { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
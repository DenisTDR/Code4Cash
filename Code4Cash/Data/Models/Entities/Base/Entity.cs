using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Code4Cash.Misc.Attributes;

namespace Code4Cash.Data.Models.Entities.Base
{
    public abstract class Entity:IEntity
    {
        [Index]
        [Updatable(false)]
        public int Id { get; set; }

        [Index]
        [StringLength(36)]
        [Updatable(false)]
        public string Selector { get; set; }

        [Updatable(false)]
        public DateTime Created { get; set; }
        [Updatable(false)]
        public DateTime Updated { get; set; }

    }
}
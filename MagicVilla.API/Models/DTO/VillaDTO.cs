﻿namespace MagicVilla.API.Models.DTO
{
    public class VillaDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;



    }
}

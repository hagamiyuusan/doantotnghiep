﻿namespace doan.Entities
{
    public class ImageForCaptioning
    {
        public int Id { set; get; }
        public Guid? userId { set; get; }
        public AppUser? user { set; get; }
        public string path { set; get; }
        public string caption { set; get; }
    }
}

﻿namespace Exam.Areas.Manage.ViewModels.Client
{
    public class UpdateClientVM
    {
        public string Name { get; set; }
        public IFormFile? Photo { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
    }
}

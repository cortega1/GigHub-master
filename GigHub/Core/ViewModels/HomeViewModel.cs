﻿using GigHub.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Core.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Gig> UpcommingGigs { get; set; }
        public bool ShowActions { get; set; }
        public string Heading { get; set; }
        public string SearchTerm { get; set; }
        public ILookup<int, Attendance> Attendances { get; internal set; }
    }
}
﻿using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Application
{
    public interface IExcelFileResultGenerator
    {
        public FileResult CreateFile(int year, SegmentType segmentType, string contentType);
    }
}

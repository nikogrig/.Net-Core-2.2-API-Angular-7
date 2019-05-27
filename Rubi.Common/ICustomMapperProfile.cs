using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rubi.Common
{
    public interface ICustomMapperProfile
    {
        void ConfigureMapping(Profile mapper);
    }
}

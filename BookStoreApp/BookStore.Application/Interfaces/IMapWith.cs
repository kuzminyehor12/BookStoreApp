using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Interfaces
{
    public interface IMapWith<TSource>
    {
        public void UseMap(Profile profile)
        {
            profile.CreateMap(typeof(TSource), GetType());
        }
    }
}

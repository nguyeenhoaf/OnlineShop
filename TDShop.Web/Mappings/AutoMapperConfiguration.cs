using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TDShop.Web.Models;
using TeduShop.Model.Models;

namespace TDShop.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        private static IMapper _mapper;

        public static IMapper Mapper { get => _mapper; set => _mapper = value; }

        public static void Configure()
        {
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Post, PostViewModel>();
                config.CreateMap<PostViewModel, Post>();
                config.CreateMap<Tag, TagViewModel>();
                config.CreateMap<TagViewModel, Tag>();
                config.CreateMap<PostTag, PostTagViewModel>();
                config.CreateMap<PostTagViewModel, PostTag>();
                config.CreateMap<PostCategoryViewModel, PostCategory>();
            });
            _mapper=mapperConfig.CreateMapper();
        }
    }
}
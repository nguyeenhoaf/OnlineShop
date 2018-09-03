﻿using AutoMapper;
using TDShop.Web.Models;
using TDShop.Model.Models;

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
                config.CreateMap<Tag, TagViewModel>();
                config.CreateMap<TagViewModel, Tag>();
                //Post Mapper
                config.CreateMap<Post, PostViewModel>();
                config.CreateMap<PostViewModel, Post>();               
                config.CreateMap<PostTag, PostTagViewModel>();
                config.CreateMap<PostTagViewModel, PostTag>();
                config.CreateMap<PostCategoryViewModel, PostCategory>();
                config.CreateMap<PostCategory, PostCategoryViewModel>();
                //Product Mapper
                config.CreateMap<Product, ProductViewModel>();
                config.CreateMap<ProductViewModel, Product>();
                config.CreateMap<ProductTag, ProductTagViewModel>();
                config.CreateMap<ProductTagViewModel, ProductTag>();
                config.CreateMap<ProductCategoryViewModel, ProductCategory>();
                config.CreateMap<ProductCategory, ProductCategoryViewModel>();
                //Footer
                config.CreateMap<Footer, FooterViewModel>();
                config.CreateMap<FooterViewModel, Footer>();
                //Slide
                config.CreateMap<Slide, SlideViewModel>();
                config.CreateMap<SlideViewModel, Slide>();
                //Page
                config.CreateMap<Page, PageViewModel>();
                config.CreateMap<PageViewModel, Page>();
                //Feedback 
                config.CreateMap<Feedback, FeedbackViewModel>();
                config.CreateMap<FeedbackViewModel, Feedback>();
            });
            _mapper = mapperConfig.CreateMapper();
        }
    }
}
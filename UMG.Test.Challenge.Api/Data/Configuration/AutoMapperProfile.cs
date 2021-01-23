using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movie.Api.Models;
using Movie.Api.Models.dto;

namespace Movie.Api.Data.Configuration
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            AutoMapperRegister();
        }
        private void AutoMapperRegister()
        {
            //var episode = new Episode
            //{
            //    Name = "Auto mapper..",
            //    Description = "More AutoMapper",
            //    Artist = new Artist { ArtistId=1, Name ="Elton Jones"},
            //    DeliveryPartner = new DeliveryPartner {  PartnerId=1, Name="MG Media"}
            //};

            var episodeModel = CreateMap<Episode, EpisodeModel>()
                 .ForMember(d => d.AuthorName, opt => opt.MapFrom(s => $"{s.Artist.Name}"));

            CreateMap<Tuple<Artist, Album, Contract, DeliveryPartner>, DistributorAlbum>()

                //.ForMember(dest => dest.Contracts.ArtistId, opt => opt.MapFrom(src => src.Item1.ArtistId))
                .ForMember(dest => dest.ArtistName, opt => opt.MapFrom(src => src.Item1.Name))

                //.ForMember(dest => dest.Contracts.AlbumId, opt => opt.MapFrom(src => src.Item2.AlbumId))
                //.ForMember(dest => dest.AlbumName, opt => opt.MapFrom(src => src.Item2.Title))

                //.ForMember(dest => dest.Contracts.ContractId, opt => opt.MapFrom(src => src.Item3.ContractId))
                //.ForMember(dest => dest.Contracts.StartDate, opt => opt.MapFrom(src => src.Item3.StartDate))
                //.ForMember(dest => dest.Contracts.EndDate, opt => opt.MapFrom(src => src.Item3.EndDate))

                // .ForMember(dest => dest.Contracts.ContractType, opt => opt.MapFrom(src => src.Item3.ContractType))

                //.ForMember(dest => dest.Contracts.PartnerId, opt => opt.MapFrom(src => src.Item4.PartnerId))
                //.ForMember(dest => dest.PartnerName, opt => opt.MapFrom(src => src.Item4.Name))

                .ReverseMap();
        }
    }

    //internal class Mapping
    //{

    //}
    //public class FlagToBooleanResolver: IValueResolver<Episode, EpisodeModel, string>
    //{
    //    protected override bool ResolveCore(string source)
    //    {
    //        return source.ToUpper().Equals("Y");
    //    }
    //}
}
